using Base.DTOs;
using Base.DTOs.MST;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using FileStorage;
using MST_Lg.Params.Filters;
using MST_Lg.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using static Base.DTOs.MST.LetterOfGuaranteeDTO;

namespace MST_Lg.Services
{
    public class LetterOfGuaranteeService : ILetterOfGuaranteeService
    {
        private readonly DatabaseContext DB;
        private readonly KafkaService kafkaService;
        private FileHelper FileHelper;
        public LogModel logModel { get; set; }
        public LetterOfGuaranteeService(DatabaseContext db, KafkaService kafkaService)
        {
            logModel = new LogModel("LetterOfGuaranteeService", null);
            DB = db;

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");
            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
            this.kafkaService = kafkaService;
        }

        // for unit test
        public LetterOfGuaranteeService(DatabaseContext db)
        {
            logModel = new LogModel("LetterOfGuaranteeService", null);
            DB = db;

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");
            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }


        public async Task<LetterOfGuaranteePaging> GetLetterOfGuaranteeAsync(LetterOfGuaranteeFilter filter, PageParam pageParam, LetterOfGuaranteeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var query = from l in DB.LetterGuarantees
                        join p in DB.Projects on l.ProjectID equals p.ID into g

                        select new LetterOfGuaranteeQueryResult
                        {
                            LetterOfGuarantee = l,
                            Bank = l.Banks,
                            Company = l.Company,
                            Project = l.Project,
                            LGGuarantor = l.LGGuarantor,
                            LGType = l.LGType,
                            LGGuaranteeCondetions = l.LGGuaranteeConditions,
                            CreatedBy = l.CreatedBy,
                            UpdatedBy = l.UpdatedBy,
                            LGSubType = l.LGSubType,
                        };

            #region Filter
            if (filter.ProjectID != null)
                query = query.Where(o => o.LetterOfGuarantee.ProjectID == filter.ProjectID);
            if (filter.CompanyID != null)
                query = query.Where(o => o.LetterOfGuarantee.CompanyID == filter.CompanyID);
            if (filter.LGTypeMasterCenterID != null)
                query = query.Where(o => o.LetterOfGuarantee.LGTypeMasterCenterID == filter.LGTypeMasterCenterID);
            if (!string.IsNullOrEmpty(filter.LetterOfGuaranteeNo))
                query = query.Where(o => o.LetterOfGuarantee.LetterOfGuaranteeNo.Contains(filter.LetterOfGuaranteeNo.Trim().ToString()));
            if (filter.ExpireDateFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.ExpiredDate >= filter.ExpireDateFrom);
            if (filter.ExpireDateTo != null)
                query = query.Where(o => o.LetterOfGuarantee.ExpiredDate <= filter.ExpireDateTo);
            if (!string.IsNullOrEmpty(filter.MeterNumber))
                query = query.Where(o => o.LetterOfGuarantee.MeterNumber.Contains(filter.MeterNumber.Trim().ToString()));
            if (!string.IsNullOrEmpty(filter.CreatedBy))
                query = query.Where(o => o.LetterOfGuarantee.CreatedBy.DisplayName.Contains(filter.CreatedBy.Trim().ToString()));
            if (!string.IsNullOrEmpty(filter.JuristicSetupBy))
                query = query.Where(o => o.LetterOfGuarantee.JuristicSetupBy.Contains(filter.JuristicSetupBy.Trim().ToString()));
            if (!string.IsNullOrEmpty(filter.JuristicSetupRemarks))
                query = query.Where(o => o.LetterOfGuarantee.JuristicSetupRemarks.Contains(filter.JuristicSetupRemarks.Trim().ToString()));
            if (!string.IsNullOrEmpty(filter.CostCenter))
                query = query.Where(o => o.LetterOfGuarantee.CostCenter.Contains(filter.CostCenter.Trim().ToString()));
            if (!string.IsNullOrEmpty(filter.Remark))
                query = query.Where(o => o.LetterOfGuarantee.Remark.Contains(filter.Remark.Trim().ToString()));
            if (filter.IssueDateFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.IssueDate >= filter.IssueDateFrom);
            if (filter.IssueDateTo != null)
                query = query.Where(o => o.LetterOfGuarantee.IssueDate <= filter.IssueDateTo);
            if (filter.JuristicSetupDateFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.JuristicSetupDate >= filter.JuristicSetupDateFrom);
            if (filter.JuristicSetupDateTo != null)
                query = query.Where(o => o.LetterOfGuarantee.JuristicSetupDate <= filter.JuristicSetupDateTo);
            if (filter.EffectiveDateFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.EffectiveDate >= filter.EffectiveDateFrom);
            if (filter.EffectiveDateTo != null)
                query = query.Where(o => o.LetterOfGuarantee.EffectiveDate <= filter.EffectiveDateTo);
            if (filter.CreatedFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.Created >= filter.CreatedFrom);
            if (filter.CreatedTo != null)
                query = query.Where(o => o.LetterOfGuarantee.Created <= filter.CreatedTo);
            if (filter.UpdatedFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.Updated >= filter.UpdatedFrom);
            if (filter.UpdatedTo != null)
                query = query.Where(o => o.LetterOfGuarantee.Updated >= filter.UpdatedTo);
            if (filter.IsJuristicSetup != null)
                query = query.Where(o => o.LetterOfGuarantee.IsJuristicSetup == filter.IsJuristicSetup);
            if (filter.BankID != null)
                query = query.Where(o => o.LetterOfGuarantee.BankID == filter.BankID);
            if (filter.LGGuarantorMasterCenterID != null)
                query = query.Where(o => o.LetterOfGuarantee.LGGuarantorMasterCenterID == filter.LGGuarantorMasterCenterID);
            if (filter.LGGuaranteeConditionsMasterCenterID != null)
                query = query.Where(o => o.LetterOfGuarantee.LGGuaranteeConditionsMasterCenterID == filter.LGGuaranteeConditionsMasterCenterID);
            if (filter.ProjectAreaFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.ProjectArea >= filter.ProjectAreaFrom);
            if (filter.ProjectAreaTo != null)
                query = query.Where(o => o.LetterOfGuarantee.ProjectArea <= filter.ProjectAreaTo);
            if (filter.FeeRateFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.FeeRate >= filter.FeeRateFrom);
            if (filter.FeeRateTo != null)
                query = query.Where(o => o.LetterOfGuarantee.FeeRate <= filter.FeeRateTo);
            if (filter.IssueAmountFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.IssueAmount >= filter.IssueAmountFrom);
            if (filter.IssueAmountTo != null)
                query = query.Where(o => o.LetterOfGuarantee.IssueAmount <= filter.IssueAmountTo);
            if (filter.RefundAmountFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.RefundAmount >= filter.RefundAmountFrom);
            if (filter.RefundAmountTo != null)
                query = query.Where(o => o.LetterOfGuarantee.RefundAmount <= filter.RefundAmountTo);
            if (filter.RemainAmountFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.RemainAmount >= filter.RemainAmountFrom);
            if (filter.RemainAmountTo != null)
                query = query.Where(o => o.LetterOfGuarantee.RemainAmount <= filter.RemainAmountTo);
            if (filter.ConditionCalFeeFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.ConditionCalFee >= filter.ConditionCalFeeFrom);
            if (filter.ConditionCalFeeTo != null)
                query = query.Where(o => o.LetterOfGuarantee.ConditionCalFee <= filter.ConditionCalFeeTo);
            if (filter.FeeRateAmountByPeriodFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.FeeRateAmountByPeriod >= filter.FeeRateAmountByPeriodFrom);
            if (filter.FeeRateAmountByPeriodTo != null)
                query = query.Where(o => o.LetterOfGuarantee.FeeRateAmountByPeriod <= filter.FeeRateAmountByPeriodTo);
            if (filter.IsCancel != null)
                query = query.Where(o => o.LetterOfGuarantee.IsCanceled == filter.IsCancel);
            if (filter.ExpiredPeriodDateFrom != null)
                query = query.Where(o => o.LetterOfGuarantee.ExpiredPeriodDate >= filter.ExpiredPeriodDateFrom);
            if (filter.ExpiredPeriodDateTo != null)
                query = query.Where(o => o.LetterOfGuarantee.ExpiredPeriodDate <= filter.ExpiredPeriodDateTo);

            #endregion 
            LetterOfGuaranteeDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<LetterOfGuaranteeQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);
            var result = queryResults.Select(o => LetterOfGuaranteeDTO.CreateFromQueryResult(o, DB)).ToList();
            return new LetterOfGuaranteePaging()
            {
                PageOutput = pageOutput,
                LetterOfGuarantee = result
            };
        }

        public async Task<LetterOfGuaranteeDTO> AddLetterOfGuaranteeAsync(LetterOfGuaranteeDTO input)
        {
            //var project = await DB.Projects.Where(o => o.ID == input.Project.Id).FirstOrDefaultAsync();
            LetterGuarantee model = new LetterGuarantee
            {
                ID = input.Id.Value,
                IssueDate = input?.IssueDate,
                ExpiredDate = input?.ExpireDate,
                MeterNumber = input?.MeterNumber,
                IsJuristicSetup = input?.IsJuristicSetup,
                JuristicSetupDate = input?.JuristicSetupDate,
                JuristicSetupBy = input?.JuristicSetupBy,
                JuristicSetupRemarks = input?.JuristicSetupRemarks,
                BankID = input?.Bank?.Id,
                Bank = input?.Bank?.Alias,
                CompanyID = input?.Company?.Id,
                CompanyCode = input?.Company?.Code,
                CostCenter = input?.CostCenter,
                ProjectID = input?.Project?.Id,
                ProjectArea = input?.ProjectArea,
                LetterOfGuaranteeNo = input?.LetterOfGuaranteeNo,
                LGGuarantorMasterCenterID = input?.LGGuarantor?.Id,
                LGTypeMasterCenterID = input?.LGType?.Id,
                IssueAmount = input?.IssueAmount,
                RefundAmount = input?.RefundAmount,
                RemainAmount = input?.RemainAmount,
                LGGuaranteeConditionsMasterCenterID = input?.LGGuaranteeConditions?.Id,
                Remark = input?.Remark,
                IsCanceled = false,
                EffectiveDate = input?.EffectiveDate,
                ExpiredPeriodDate = input?.ExpiredPeriodDate,
                ConditionCalFee = input?.ConditionCalFee,
                FeeRate = input?.FeeRate,
                FeeRateAmountByPeriod = input?.FeeRateAmountByPeriod,
                LGSubTypeMasterCenterID = input?.LGSubType?.Id
            };

            await DB.LetterGuarantees.AddAsync(model);
#if !DEBUG
            LetterGuaranteeFile files = new LetterGuaranteeFile();
            var tf = new FileDTO();
            var f = input.AttachFile;
            if (f != null)
            {
                var filename = f.Name;
                if (f.IsTemp)
                {
                    string pathName = $"letter-guarantee/{input.Project.ProjectNo}/{filename}";
                    await FileHelper.MoveTempFileAsync(f.Name, pathName);

                    string url = await FileHelper.GetFileUrlAsync(pathName);
                    string type = Path.GetExtension(f.Name);
                    tf.Name = System.IO.Path.GetFileName(filename);
                    tf.Url = url;

                    files.LetterGuaranteeID = model.ID;
                    files.FileName = f.Name;
                    files.FilePath = pathName;
                    files.FileType = type;

                    await DB.LetterGuaranteeFiles.AddAsync(files);
                }
            }
#endif

            await DB.SaveChangesAsync();
            var NewLG = await DB.LetterGuarantees
                                .Include(o => o.Banks)
                                .Include(o => o.Company)
                                .Include(o => o.Project)
                                .Include(o => o.LGGuarantor)
                                .Include(o => o.LGType)
                                .Include(o => o.LGGuaranteeConditions)
                                .Include(o => o.CreatedBy)
                                .Include(o => o.UpdatedBy)
                                .Include(o => o.LGSubType)
                                .FirstOrDefaultAsync(o => o.ID == model.ID);
            var result = LetterOfGuaranteeDTO.CreateFromModel(NewLG, DB);
            //var NewLG = new LetterGuarantee();

#if !DEBUG
            await kafkaService.ProduceNewLG(NewLG);
#endif
            return result;
        }

        public async Task<LetterOfGuaranteeDTO> EditLetterOfGuaranteeAsync(LetterOfGuaranteeDTO input, Guid? userID)
        {
            await DB.LetterGuarantees
            .Where(o => o.ID == input.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(o => o.IssueDate, o => input.IssueDate)
                .SetProperty(o => o.ExpiredDate, o => input.ExpireDate)
                .SetProperty(o => o.MeterNumber, o => input.MeterNumber)
                .SetProperty(o => o.IsJuristicSetup, o => input.IsJuristicSetup)
                .SetProperty(o => o.JuristicSetupDate, o => input.JuristicSetupDate)
                .SetProperty(o => o.JuristicSetupBy, o => input.JuristicSetupBy)
                .SetProperty(o => o.JuristicSetupRemarks, o => input.JuristicSetupRemarks)
                .SetProperty(o => o.BankID, o => input.Bank != null ? input.Bank.Id : null)
                .SetProperty(o => o.Bank, o => input.Bank != null ? input.Bank.Alias : null)
                .SetProperty(o => o.CompanyID, o => input.Company != null ? input.Company.Id : null)
                .SetProperty(o => o.CompanyCode, o => input.Company != null ? input.Company.Code : null)
                .SetProperty(o => o.CostCenter, o => input.CostCenter)
                .SetProperty(o => o.ProjectID, o => input.Project != null ? input.Project.Id : null)
                .SetProperty(o => o.ProjectArea, o => input.ProjectArea)
                .SetProperty(o => o.LetterOfGuaranteeNo, o => input.LetterOfGuaranteeNo)
                .SetProperty(o => o.LGGuarantorMasterCenterID, o => input.LGGuarantor != null ? input.LGGuarantor.Id : null)
                .SetProperty(o => o.LGTypeMasterCenterID, o => input.LGType != null ? input.LGType.Id : null)
                .SetProperty(o => o.IssueAmount, o => input.IssueAmount)
                .SetProperty(o => o.RefundAmount, o => input.RefundAmount)
                .SetProperty(o => o.RemainAmount, o => input.RemainAmount)
                .SetProperty(o => o.LGGuaranteeConditionsMasterCenterID, o => input.LGGuaranteeConditions != null ? input.LGGuaranteeConditions.Id : null)
                .SetProperty(o => o.Remark, o => input.Remark)
                .SetProperty(o => o.IsCanceled, o => false)
                .SetProperty(o => o.EffectiveDate, o => input.EffectiveDate)
                .SetProperty(o => o.ExpiredPeriodDate, o => input.ExpiredPeriodDate)
                .SetProperty(o => o.ConditionCalFee, o => input.ConditionCalFee)
                .SetProperty(o => o.FeeRate, o => input.FeeRate)
                .SetProperty(o => o.FeeRateAmountByPeriod, o => input.FeeRateAmountByPeriod)
                .SetProperty(o => o.LGSubTypeMasterCenterID, o => input.LGSubType != null ? input.LGSubType.Id : null)
            );



            if (input.RemainAmount == 0)
            {
                await CancelLetterOfGuaranteeAsync(input, userID);
            }

            if (input.AttachFile != null)
            {
                LetterGuaranteeFile files = new LetterGuaranteeFile();
                var tf = new FileDTO();
                var f = input.AttachFile;
                if (f != null)
                {
                    var filename = f.Name;
                    if (f.IsTemp)
                    {
                        string pathName = $"letter-guarantee/{input.Project.ProjectNo}/{filename}";
                        await FileHelper.MoveTempFileAsync(f.Name, pathName);

                        string url = await FileHelper.GetFileUrlAsync(pathName);
                        string type = Path.GetExtension(f.Name);
                        tf.Name = System.IO.Path.GetFileName(filename);
                        tf.Url = url;

                        files.LetterGuaranteeID = (Guid)input.Id;
                        files.FileName = f.Name;
                        files.FilePath = pathName;
                        files.FileType = type;

                        await DB.LetterGuaranteeFiles.AddAsync(files);
                    }
                }
            }
            await DB.SaveChangesAsync();

            var NewLG = await DB.LetterGuarantees.Where(o => o.ID == input.Id)
                                .Include(o => o.Banks)
                                .Include(o => o.Company)
                                .Include(o => o.Project)
                                .Include(o => o.LGGuarantor)
                                .Include(o => o.LGType)
                                .Include(o => o.LGGuaranteeConditions)
                                .Include(o => o.CreatedBy)
                                .Include(o => o.UpdatedBy)
                                .Include(o => o.LGSubType)
                                .FirstOrDefaultAsync();


            var result = LetterOfGuaranteeDTO.CreateFromModel(NewLG, DB);
#if !DEBUG
            await kafkaService.ProduceEditLG(NewLG);
#endif
            return result;

        }

        public async Task<bool> DeleteLetterOfGuaranteeAsync(LetterOfGuaranteeDTO input)
        {

            var model = await DB.LetterGuarantees
                .Include(o => o.LGType)
                .Include(o => o.CreatedBy)
                .Include(o => o.UpdatedBy).Where(o => o.ID == input.Id).Include(o => o.Project).FirstOrDefaultAsync();
            model.IsDeleted = true;
            DB.LetterGuarantees.Update(model);


            var file = await DB.LetterGuaranteeFiles.Where(o => o.LetterGuaranteeID == input.Id).ToListAsync();
            foreach (var i in file)
            {
                i.IsDeleted = true;
                DB.LetterGuaranteeFiles.Update(i);
            }
            await DB.SaveChangesAsync();

            var result = true;
#if !DEBUG
            await kafkaService.ProduceDeleteLG(model);
#endif
            return result;
        }

        public async Task<LetterOfGuaranteeDTO> CancelLetterOfGuaranteeAsync(LetterOfGuaranteeDTO input, Guid? userID)
        {

            var model = await DB.LetterGuarantees.Where(o => o.ID == input.Id)
                   .Include(o => o.Project)
                   .Include(o => o.LGType)
                   .Include(o => o.CreatedBy)
                   .Include(o => o.UpdatedBy).FirstOrDefaultAsync();
            model.IsCanceled = true;
            model.CancelDate = DateTime.Now;
            model.CancelByUserID = userID;
            model.CancelRemark = input?.CancelRemark;

            model.RefundAmount = input?.IssueAmount;
            model.RemainAmount = input?.IssueAmount - input?.RefundAmount;

            DB.LetterGuarantees.Update(model);
            await DB.SaveChangesAsync();

            var NewLG = new LetterGuarantee();
            var result = LetterOfGuaranteeDTO.CreateFromModels(NewLG, DB);
#if !DEBUG
            await kafkaService.ProduceInActiveLG(model);
#endif

            return result;
        }

        public async Task<LetterOfGuaranteeDTO> CancelCancelLetterOfGuaranteeAsync(LetterOfGuaranteeDTO input, Guid? userID)
        {

            var model = await DB.LetterGuarantees
                .Include(o => o.LGType)
                .Include(o => o.CreatedBy)
                .Include(o => o.UpdatedBy)
                .Where(o => o.ID == input.Id).Include(o => o.Project).FirstOrDefaultAsync();
            model.IsCanceled = false;
            model.CancelDate = null;
            model.CancelByUserID = null;
            model.CancelRemark = null;

            DB.LetterGuarantees.Update(model);
            await DB.SaveChangesAsync();

            var NewLG = new LetterGuarantee();
            var result = LetterOfGuaranteeDTO.CreateFromModels(NewLG, DB);
#if !DEBUG
            await kafkaService.ProduceActiveLG(model);
#endif
            return result;
        }

        public async Task<List<LetterGuaranteeFileDTO>> GetLetterGuaranteeFileListAsync(Guid LetterGuaranteeID, CancellationToken cancellationToken = default)
        {
            var fileList = await DB.LetterGuaranteeFiles
                .Include(o => o.CreatedBy)
                .Include(o => o.UpdatedBy)
                .Where(o => o.LetterGuaranteeID == LetterGuaranteeID)
                .OrderByDescending(w => w.Created).ToListAsync(cancellationToken);

            var result = new List<LetterGuaranteeFileDTO>();

            if (fileList.Any())
            {
                foreach (var f in fileList)
                {
                    var item = await LetterGuaranteeFileDTO.CreateFromModel(f, FileHelper);
                    result.Add(item);
                }
            }

            return result;
        }


        public async Task<string> DeleteLetterGuaranteeFileAsync(Guid id)
        {
            var file = await DB.LetterGuaranteeFiles.FindAsync(id);
            file.IsDeleted = true;
            DB.LetterGuaranteeFiles.Update(file);
            await DB.SaveChangesAsync();

            // await DB.LetterGuaranteeFiles.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //     c.SetProperty(col => col.IsDeleted, true)
            //     .SetProperty(col => col.Updated, DateTime.Now)
            // );
            var result = "Success";
            return result;
        }


        public async Task<string> MoveFile()
        {
            var file = await DB.LetterGuaranteeFiles.Where(o => o.FileName != null).ToListAsync();

            foreach (var i in file)
            {
                if (!string.IsNullOrEmpty(i.FileName))
                {
                    var projectNo = i.FileName.Substring(0, 5);
                    //string pathName = $"{i.FilePath}";
                    //await FileHelper.MoveTempFileAsync(i.FileName, pathName);

                    string pathName = $"letter-guarantee/{projectNo}/{i.FileName}";

                    try
                    {
                        await FileHelper.MoveTempFileAsync(i.FileName, pathName);
                    }
                    catch
                    {

                    }
                }

            }
            var result = "Success";
            return result;
        }


        public async Task<bool> AddLGGuarantor(MasterCenterDTO input)
        {
            var result = false;

            var masterCenter = await DB.MasterCenters.Where(o => o.Name == input.Name).FirstOrDefaultAsync();
            if (masterCenter == null)
            {
                var getOrder = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "LGGuarantor").OrderByDescending(o => o.Order).FirstOrDefaultAsync();
                var newMasterCenter = new MasterCenter
                {
                    Name = input.Name,
                    Order = getOrder.Order + 1
                };
                newMasterCenter.Key = newMasterCenter.Order.ToString();
                newMasterCenter.IsActive = true;
                newMasterCenter.IsDeleted = false;

                newMasterCenter.MasterCenterGroupKey = "LGGuarantor";

                await DB.MasterCenters.AddAsync(newMasterCenter);
                await DB.SaveChangesAsync();

                result = true;
            }
            else
            {
                ValidateException ex = new ValidateException();
                var msg = "ชื่อผู้รับค้ำประกันนี้มีอยู่ในระบบแล้ว...!";
                ex.AddError("1", msg, 1);
                throw ex;
            }
            return result;
        }
    }
}
