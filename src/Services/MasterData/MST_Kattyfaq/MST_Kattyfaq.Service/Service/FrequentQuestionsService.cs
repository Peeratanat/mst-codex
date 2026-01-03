using Database.Models;
using Database.Models.MST;
using MST_Kattyfaq.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MST_Kattyfaq.Params.Outputs;
using PagingExtensions;
using Base.DTOs;
using ErrorHandling;
using static Base.DTOs.MST.ServitudeDTO;
using static Base.DTOs.MST.QuestionFAQDTO;
using Minio.DataModel;
using System.IO;
using FileStorage;
using NPOI.HPSF;
using Common.Helper.Logging;

namespace MST_Kattyfaq.Services
{
    public class FrequentQuestionsService : IFrequentQuestionsService
    {
        private readonly DatabaseContext DB;
        private FileHelper FileHelper;
        public LogModel logModel { get; set; }

        public FrequentQuestionsService(DatabaseContext db)
        {
            logModel = new LogModel("FrequentQuestionsService", null);
            this.DB = db;

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");
            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }

        public async Task<FrequentQuestionsPaging> GetQuestionFAQListAsync(QuestopnFAQFilter filter, PageParam pageParam, QuestionFAQSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var query = from l in DB.AnswerFAQ
                        join p in DB.QuestionFAQ.Include(x => x.CreatedBy).Include(x => x.UpdatedBy) on l.QuestionFAQ.ID equals p.ID
                        join c in DB.MasterCenters on p.FAQCategoryMasterCenterID equals c.ID into c
                        from cat in c.DefaultIfEmpty()

                        select new QuestionFAQQueryResult
                        {
                            QuestionFAQ = l.QuestionFAQ,
                            AnswerFAQ = l,
                            CreatedBy = l.QuestionFAQ.CreatedBy,
                            UpdatedBy = l.QuestionFAQ.UpdatedBy,
                            FAQCategory = cat,
                        };

            #region Filter
            if (!string.IsNullOrEmpty(filter.QuestionDesc))
                query = query.Where(o => o.QuestionFAQ.QuestionDesc.Contains(filter.QuestionDesc));

            if (filter.UpdatedFrom != null)
                query = query.Where(o => o.QuestionFAQ.Updated >= filter.UpdatedFrom);
            if (filter.UpdatedTo != null)
                query = query.Where(o => o.QuestionFAQ.Updated <= filter.UpdatedTo);

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
                query = query.Where(o => o.QuestionFAQ.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));

            if (filter.IsSubmit != null)
                query = query.Where(o => o.QuestionFAQ.IsSubmit == filter.IsSubmit);

            if (!string.IsNullOrEmpty(filter.AnswerDesc))
                query = query.Where(o => o.AnswerFAQ.AnswerDesc.Contains(filter.AnswerDesc));

            if (filter.FAQCategoryMasterCenterID != null)
                query = query.Where(o => o.FAQCategory.ID == filter.FAQCategoryMasterCenterID);

            if (!string.IsNullOrEmpty(filter.CreatedBy))
                query = query.Where(o => o.QuestionFAQ.CreatedBy.DisplayName.Contains(filter.CreatedBy));

            if (filter.CreatedFrom != null)
                query = query.Where(o => o.QuestionFAQ.Created >= filter.CreatedFrom);
            if (filter.CreatedTo != null)
                query = query.Where(o => o.QuestionFAQ.Created <= filter.CreatedTo);
            if (filter.UpsertPineconeDateFrom != null)
                query = query.Where(o => o.QuestionFAQ.UpsertPineconeDate >= filter.UpsertPineconeDateFrom);
            if (filter.UpsertPineconeDateTo != null)
                query = query.Where(o => o.QuestionFAQ.UpsertPineconeDate <= filter.UpsertPineconeDateTo);


            #endregion 
            QuestionFAQDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<QuestionFAQQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);
            var result = queryResults.Select(o => QuestionFAQDTO.CreateFromQueryResult(o, DB)).ToList();
            return new FrequentQuestionsPaging()
            {
                PageOutput = pageOutput,
                FrequentQuestions = result
            };
        }


        public async Task<QuestionFAQDTO> AddQuestionAsync(QuestionFAQDTO input)
        {

            QuestionFAQ question = new QuestionFAQ();
            question.QuestionDesc = input.QuestionDesc;
            question.IsDeleted = false;
            question.IsSubmit = false;
            question.FAQCategoryMasterCenterID = input.FAQCategory?.Id;
            DB.QuestionFAQ.Add(question);

            AnswerFAQ answer = new AnswerFAQ();
            answer.AnswerDesc = input.AnswerFAQ.AnswerDesc;
            answer.QuestionID = question.ID;
            answer.IsDeleted = false;
            answer.IsSubmit = false;
            DB.AnswerFAQ.Add(answer);

            await DB.SaveChangesAsync();



            var newData = await DB.AnswerFAQ
                          .Include(o => o.QuestionFAQ)
                          .Include(o => o.QuestionFAQ).ThenInclude(o => o.FAQCategoryMasterCenter)
                          .Where(o => o.QuestionFAQ.ID == question.ID).FirstOrDefaultAsync();
            var result = QuestionFAQDTO.CreateFromModel(newData, DB);
            return result;
        }

        public async Task<QuestionFAQDTO> EditQuestionAsync(QuestionFAQDTO input)
        {

            var answer = await DB.AnswerFAQ.Where(o => o.ID == input.AnswerFAQ.ID).FirstOrDefaultAsync();
            answer.AnswerDesc = input.AnswerFAQ.AnswerDesc;
            DB.AnswerFAQ.Update(answer);

            var question = await DB.QuestionFAQ.Where(o => o.ID == answer.QuestionID).FirstOrDefaultAsync();
            question.QuestionDesc = input.QuestionDesc;
            question.FAQCategoryMasterCenterID = input.FAQCategory?.Id;
            DB.QuestionFAQ.Update(question);

            await DB.SaveChangesAsync();



            var newData = await DB.AnswerFAQ
                          .Include(o => o.QuestionFAQ)
                          .Include(o => o.QuestionFAQ).ThenInclude(o => o.FAQCategoryMasterCenter)
                          .Where(o => o.QuestionFAQ.ID == input.ID).FirstOrDefaultAsync();
            var result = QuestionFAQDTO.CreateFromModel(newData, DB);
            return result;
        }

        public async Task<FileDTO> GenTextFileQuestionAsync(List<Guid> questionID, CancellationToken cancellationToken = default)
        {
            var FileName = string.Empty;
            var resultFileName = string.Empty;

            var answers = new List<AnswerFAQ>();

            if (questionID.Count > 0)
            {
                answers = await DB.AnswerFAQ.Include(o => o.QuestionFAQ).Where(o => questionID.Contains(o.QuestionID.Value)).ToListAsync();
            }
            else
            {
                answers = await DB.AnswerFAQ.Include(o => o.QuestionFAQ).ToListAsync();
            }

            var questions = new List<QuestionFAQ>();
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            using (StreamWriter output = new StreamWriter(stream, new UTF8Encoding(false)))
            {
                foreach (var item1 in answers)
                {
                    output.WriteLine("Q: " + item1.QuestionFAQ.QuestionDesc + "\nA: " + item1.AnswerDesc + "\n");
                }
                output.Flush();
                Stream fileStream = new MemoryStream(stream.ToArray());

                var dateTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileName = "CRM_FAQ_" + dateTimeString + ".txt";
                string filePath = $"data/";
                string contentType = "text/*";
                var uploadResult = await this.FileHelper.UploadFileFromStreamWithOutGuid(fileStream, Environment.GetEnvironmentVariable("minio_TempBucket"), filePath, fileName, contentType);
                return new FileDTO()
                {
                    Name = fileName,
                    Url = uploadResult.Url
                };
            }
        }


        public async Task DeleteQuestionAsync(QuestionFAQDTO input)
        {

            var answer = await DB.AnswerFAQ.FirstOrDefaultAsync(o => o.ID == input.AnswerFAQ.ID);
            if (answer is not null)
            {
                answer.IsDeleted = true;
                DB.AnswerFAQ.Update(answer);

                var question = await DB.QuestionFAQ.FirstOrDefaultAsync(o => o.ID == answer.QuestionID);
                question.IsDeleted = false;
                DB.QuestionFAQ.Update(question);
            }

            await DB.SaveChangesAsync();
        }
    }
}
