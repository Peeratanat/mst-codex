using Base.DTOs.PRJ;
using Base.DTOs.SAL.Sortings;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.USR;
using FileStorage;
using static Base.DTOs.MST.LetterOfGuaranteeDTO;

namespace Base.DTOs.MST
{
    public class QuestionFAQDTO : BaseDTO
    {
        public Guid ID { get; set; }
        public string QuestionDesc { get; set; }
        public string Remark { get; set; }
        public bool? IsSubmit { get; set; }
        public AnswerFAQDTO AnswerFAQ { get; set; }
        public MasterCenterDTO FAQCategory { get; set; }
        public DateTime? UpsertPineconeDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string PineconeListID { get; set; }


        public static QuestionFAQDTO CreateFromModel(AnswerFAQ model, DatabaseContext db)
        {
            if (model != null)
            {
                
                var result = new QuestionFAQDTO()
                {
                    ID = model.QuestionFAQ.ID,
                    QuestionDesc = model.QuestionFAQ.QuestionDesc,
                    Remark = model.QuestionFAQ.Remark,
                    IsSubmit = model.QuestionFAQ.IsSubmit,
                    Updated = model.QuestionFAQ.Updated,
                    UpdatedBy = model.QuestionFAQ.UpdatedBy?.DisplayName,
                    AnswerFAQ = AnswerFAQDTO.CreateFromModel(model),
                    FAQCategory = MasterCenterDTO.CreateFromModel(model.QuestionFAQ.FAQCategoryMasterCenter),
                    UpsertPineconeDate = model.QuestionFAQ.UpsertPineconeDate,
                    CreatedBy = model.QuestionFAQ.CreatedBy?.DisplayName,
                    Created = model.QuestionFAQ.Created,
                    PineconeListID = model.QuestionFAQ.PineconeListID 
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static QuestionFAQDTO CreateFromQueryResult(QuestionFAQQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
               

                QuestionFAQDTO result = new QuestionFAQDTO()
                {
                    ID = model.QuestionFAQ.ID,
                    QuestionDesc = model.QuestionFAQ.QuestionDesc,
                    Remark = model.QuestionFAQ.Remark,
                    IsSubmit = model.QuestionFAQ.IsSubmit,
                    Updated = model.QuestionFAQ.Updated,
                    UpdatedBy = model.QuestionFAQ.UpdatedBy?.DisplayName,
                    AnswerFAQ = AnswerFAQDTO.CreateFromModel(model.AnswerFAQ),
                    FAQCategory = MasterCenterDTO.CreateFromModel(model.FAQCategory),
                    UpsertPineconeDate = model.QuestionFAQ.UpsertPineconeDate,
                    CreatedBy = model.QuestionFAQ.CreatedBy?.DisplayName,
                    Created = model.QuestionFAQ.Created,
                    PineconeListID = model.QuestionFAQ.PineconeListID
                };
                return result;
            }
            else
            {
                return null;
            }
        }



        public static void SortBy(QuestionFAQSortByParam sortByParam, ref IQueryable<QuestionFAQQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case QuestionFAQSortBy.QuestionDesc:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.QuestionFAQ.QuestionDesc);
                        else query = query.OrderByDescending(o => o.QuestionFAQ.QuestionDesc);
                        break;
                    case QuestionFAQSortBy.AnswerDesc:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.AnswerFAQ.AnswerDesc);
                        else query = query.OrderByDescending(o => o.AnswerFAQ.AnswerDesc);
                        break;
                    case QuestionFAQSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.QuestionFAQ.Updated);
                        else query = query.OrderByDescending(o => o.QuestionFAQ.Updated);
                        break;
                    case QuestionFAQSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.QuestionFAQ.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.QuestionFAQ.UpdatedBy.DisplayName);
                        break;
                    case QuestionFAQSortBy.Created:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.QuestionFAQ.Created);
                        else query = query.OrderByDescending(o => o.QuestionFAQ.Created);
                        break;
                    case QuestionFAQSortBy.CreatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.QuestionFAQ.CreatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.QuestionFAQ.CreatedBy.DisplayName);
                        break;
                    case QuestionFAQSortBy.UpsertPineconeDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.QuestionFAQ.UpsertPineconeDate);
                        else query = query.OrderByDescending(o => o.QuestionFAQ.UpsertPineconeDate);
                        break;
                    //case QuestionFAQSortBy.FAQCategory:
                    //    if (sortByParam.Ascending) query = query.OrderBy(o => o.FAQCategory.Name);
                    //    else query = query.OrderByDescending(o => o.FAQCategory.Name);
                    //    break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.QuestionFAQ.Updated).ThenBy(o => o.QuestionFAQ.Updated);
            }
        }

        public class QuestionFAQQueryResult
        {
            public models.MST.QuestionFAQ QuestionFAQ { get; set; }
            public models.MST.AnswerFAQ AnswerFAQ { get; set; }
            public models.USR.User CreatedBy { get; set; }
            public models.USR.User UpdatedBy { get; set; }
            public MasterCenter FAQCategory { get; set; }
        }
    }
}
