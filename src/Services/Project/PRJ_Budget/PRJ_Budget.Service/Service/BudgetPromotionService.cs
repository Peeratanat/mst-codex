using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Dapper;
using Database.Models;
using Database.Models.DbQueries;
using Database.Models.DbQueries.PRJ;
using Database.Models.LOG;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using ExcelExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Budget.Params.Filters;
using PRJ_Budget.Params.Outputs;
using PRJ_Budget.Services.Excels;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using FileStorage;
namespace PRJ_Budget.Services
{
    public class BudgetPromotionService : IBudgetPromotionService
    {
        private readonly DatabaseContext DB;
        //private readonly IConfiguration Configuration;
        private FileHelper FileHelper;
        private FileHelper FileHelperSap;
        public LogModel logModel { get; set; }
        int Timeout = 120;

        public BudgetPromotionService(DatabaseContext db)
        {
            logModel = new LogModel("BudgetPromotionService", null);
            DB = db;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Environment.GetEnvironmentVariable("DBConnectionString"));
            Timeout = builder.ConnectTimeout;
            DB.Database.SetCommandTimeout(Timeout);

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            var minioSapEndpoint = Environment.GetEnvironmentVariable("minioSAP_Endpoint");
            var minioSapAccessKey = Environment.GetEnvironmentVariable("minioSAP_AccessKey");
            var minioSapSecretKey = Environment.GetEnvironmentVariable("minioSAP_SecretKey");
            var minioSapWithSSL = Environment.GetEnvironmentVariable("minioSAP_WithSSL");

            FileHelperSap = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, "bgt", "", publicURL, minioSapWithSSL == "true");

            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }

        public async Task<BudgetPromotionPaging> GetBudgetPromotionListAsync(Guid projectID, BudgetPromotionFilter filter, PageParam pageParam, BudgetPromotionSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var unitsModel = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync(cancellationToken);

            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("@ProjectID", projectID);
            ParamList.Add("@UnitNo", filter.UnitNo);
            ParamList.Add("@HouseNo", filter.HouseNo);
            ParamList.Add("@SaleAreaFrom", filter.SaleAreaFrom ?? 0);
            ParamList.Add("@SaleAreaTo", filter.SaleAreaTo ?? 0);
            ParamList.Add("@TotalPriceFrom", filter.TotalPriceFrom ?? 0);
            ParamList.Add("@TotalPriceTo", filter.TotalPriceTo ?? 0);
            ParamList.Add("@PromotionPriceFrom", filter.PromotionPriceFrom ?? 0);
            ParamList.Add("@PromotionPriceTo", filter.PromotionPriceTo ?? 0);
            ParamList.Add("@PromotionTransferPriceFrom", filter.PromotionTransferPriceFrom ?? 0);
            ParamList.Add("@PromotionTransferPriceTo", filter.PromotionTransferPriceTo ?? 0);
            ParamList.Add("@WBSCRM_P", filter.WBSCRM_P);
            ParamList.Add("@WBSSAP_P", filter.WBSSAP_P);
            ParamList.Add("@UpdatedBy", filter.UpdatedBy);
            ParamList.Add("@UpdatedFrom", filter.UpdatedFrom);
            ParamList.Add("@UpdatedTo", filter.UpdatedTo);
            ParamList.Add("@SyncJob_StatusKey", filter.SyncJob_StatusKey);

            var sortby = string.Empty;
            bool sort = true;
            sortby = nameof(BudgetPromotionSortBy.Unit_UnitNo);
            if (sortByParam.SortBy != null)
            {
                sort = sortByParam.Ascending;
                switch (sortByParam.SortBy.Value)
                {
                    case BudgetPromotionSortBy.Unit_HouseNo:
                        sortby = nameof(BudgetPromotionSortBy.Unit_HouseNo);
                        break;
                    case BudgetPromotionSortBy.Unit_SaleArea:
                        sortby = nameof(BudgetPromotionSortBy.Unit_SaleArea);
                        break;
                    case BudgetPromotionSortBy.Unit_SapwbsObject:
                        sortby = nameof(BudgetPromotionSortBy.Unit_SapwbsObject);
                        break;
                    case BudgetPromotionSortBy.Unit_SapwbsObject_P:
                        sortby = nameof(BudgetPromotionSortBy.Unit_SapwbsObject_P);
                        break;
                    case BudgetPromotionSortBy.Unit_SapwbsNo:
                        sortby = nameof(BudgetPromotionSortBy.Unit_SapwbsNo);
                        break;
                    case BudgetPromotionSortBy.Unit_SapwbsNo_P:
                        sortby = nameof(BudgetPromotionSortBy.Unit_SapwbsNo_P);
                        break;
                    case BudgetPromotionSortBy.PromotionPrice:
                        sortby = nameof(BudgetPromotionSortBy.PromotionPrice);
                        break;
                    case BudgetPromotionSortBy.PromotionTransferPrice:
                        sortby = nameof(BudgetPromotionSortBy.PromotionTransferPrice);
                        break;
                    case BudgetPromotionSortBy.TotalPrice:
                        sortby = nameof(BudgetPromotionSortBy.TotalPrice);
                        break;
                    case BudgetPromotionSortBy.Updated:
                        sortby = nameof(BudgetPromotionSortBy.Updated);
                        break;
                    case BudgetPromotionSortBy.UpdatedBy:
                        sortby = nameof(BudgetPromotionSortBy.UpdatedBy);
                        break;
                    case BudgetPromotionSortBy.Unit_UnitNo:
                        sortby = nameof(BudgetPromotionSortBy.Unit_UnitNo);
                        break;
                    default:
                        sortby = nameof(BudgetPromotionSortBy.Unit_UnitNo);
                        break;
                }
            }

            ParamList.Add("@Sys_SortBy", sortby);
            ParamList.Add("@Sys_SortType", sort ? "asc" : "desc");
            ParamList.Add("@Page", pageParam?.Page ?? 1);
            ParamList.Add("@PageSize", pageParam?.PageSize ?? 10);


            CommandDefinition commandDefinition = new(
                commandText: DBStoredNames.sp_BudgetPromotionList,
                parameters: ParamList,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken,
                transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.StoredProcedure
            );
            var queryResult = await cmd.Connection.QueryAsync<dbqBudgetPromotionList>(commandDefinition) ?? new List<dbqBudgetPromotionList>();
            var result = queryResult.Select(x => BudgetPromotionDTO.CreateFromQuery(x, DB, unitsModel)).ToList();
            var firstResult = queryResult.FirstOrDefault();
            return new BudgetPromotionPaging()
            {
                PageOutput = firstResult != null ? firstResult.CreateBaseDTOFromQuery() : new PageOutput(),
                BudgetPromotions = result ?? new List<BudgetPromotionDTO>()
            };
        }

        public async Task<BudgetPromotionDTO> GetBudgetPromotionAsync(Guid projectID, Guid unitID, CancellationToken cancellationToken = default)
        {
            var query = await DB.BudgetPromotions
                                .Include(o => o.UpdatedBy)
                                .Where(o => o.ProjectID == projectID
                                            && o.UnitID == unitID)
                                .Select(o => new
                                {
                                    BudgetPromotion = o,
                                    Unit = o.Unit
                                }).ToListAsync(cancellationToken);

            var temp = query.GroupBy(o => o.Unit).Select(o => new TempBudgetPromotionQueryResult
            {
                Unit = o.Key,
                BudgetPromotions = o.Select(p => p.BudgetPromotion).ToList()
            }).ToList();

            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync(cancellationToken);
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync(cancellationToken);
            var data = temp.Select(o => new BudgetPromotionQueryResult
            {
                Unit = o.Unit,
                BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
            }).FirstOrDefault();

            var result = await BudgetPromotionDTO.CreateFromQueryResultAsync(data, DB);
            return result;
        }

        public async Task<BudgetPromotionDTO> CreateBudgetPromotionAsync(Guid projectID, BudgetPromotionDTO input)
        {
            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
            BudgetPromotion modelSale = new BudgetPromotion();
            input.ToModelSale(ref modelSale);
            modelSale.ProjectID = projectID;
            modelSale.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeSaleID;

            BudgetPromotion modelTransfer = new BudgetPromotion();
            input.ToModelTransfer(ref modelTransfer);
            modelTransfer.ProjectID = projectID;
            modelTransfer.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeTransferID;

            await DB.BudgetPromotions.AddAsync(modelSale);
            await DB.BudgetPromotions.AddAsync(modelTransfer);
            await DB.SaveChangesAsync();

            var listBudgetPromotion = new List<BudgetPromotion>();

            listBudgetPromotion.Add(modelTransfer);
            listBudgetPromotion.Add(modelSale);
            await CreateNewSyncJobAsync(listBudgetPromotion, projectID);


            var result = await GetBudgetPromotionAsync(projectID, input.Unit.Id.Value);

            return result;
        }

        public async Task<BudgetPromotionDTO> UpdateBudgetPromotionAsync(Guid projectID, Guid unitID, BudgetPromotionDTO input)
        {
            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();

            BudgetPromotion modelSale = new BudgetPromotion();
            input.ToModelSale(ref modelSale);
            modelSale.ProjectID = projectID;
            modelSale.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeSaleID;

            BudgetPromotion modelTransfer = new BudgetPromotion();
            input.ToModelTransfer(ref modelTransfer);
            modelTransfer.ProjectID = projectID;
            modelTransfer.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeTransferID;

            await DB.BudgetPromotions.AddAsync(modelSale);
            await DB.BudgetPromotions.AddAsync(modelTransfer);
            await DB.SaveChangesAsync();

            //var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            //var budgetProDataStatusMasterCenterID = await BudgetPromotionDataStatus(projectID);
            //project.BudgetProDataStatusMasterCenterID = budgetProDataStatusMasterCenterID;
            //await DB.SaveChangesAsync();

            var listBudgetPromotion = new List<BudgetPromotion>();
            listBudgetPromotion.Add(modelTransfer);
            listBudgetPromotion.Add(modelSale);
            await CreateNewSyncJobAsync(listBudgetPromotion, projectID);

            var result = await GetBudgetPromotionAsync(projectID, unitID);
            return result;
        }

        public async Task DeleteBudgetPromotionAsync(Guid projectID, Guid unitID)
        {
            var models = await DB.BudgetPromotions.Where(o => o.ProjectID == projectID && o.ID == unitID).ToListAsync();
            foreach (var item in models)
            {
                item.IsDeleted = true;
                DB.Entry(item).State = EntityState.Modified;
            }
            await DB.SaveChangesAsync();

            //var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            //var budgetProDataStatusMasterCenterID = await BudgetPromotionDataStatus(projectID);
            //project.BudgetProDataStatusMasterCenterID = budgetProDataStatusMasterCenterID;

            //await DB.SaveChangesAsync();
        }

        public async Task<BudgetPromotionExcelDTO> ImportBudgetPromotionAsync(Guid projectID, FileDTO input, Guid? UserID = null)
        {
            var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
            var result = new BudgetPromotionExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>(), Messages = new List<string>() };


            if (input.IsTemp)
            {
                string Name = "Budget.xlsx";
                string minpriceName = $"import-project/{projectNo}/budgetprom/{Name}";
                await FileHelper.MoveTempFileAsync(input.Name, minpriceName);
                result.Messages.Add("อัพโหลดไฟล์สำเร็จ กรุณารอผลทางอีเมล");
                result.Success = 1;
            }
            else
            {
                result.Messages.Add("ไม่สามารถอัพโหลดไฟล์ได้ กรุณาติดต่อ Admin");
                result.Success = 0;
            }

            ImptMstProjTran imp = new ImptMstProjTran();
            imp.CreatedByUserID = UserID;
            imp.IsDeleted = false;
            imp.ProjectID = projectID;
            imp.Import_Type = "budgetprom";
            imp.Import_Status = "I";

            await DB.ImptMstProjTrans.AddAsync(imp);
            await DB.SaveChangesAsync();

            return result;
        }

        public async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention == "xls")
                {
                    excelByte = XLSToXLSXConverter.Convert(stream);
                }
                else
                {
                    excelByte = XLSToXLSXConverter.ReadFully(stream);
                }
                using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
                using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
                {
                    var ws = pck.Workbook.Worksheets.First();
                    DataTable tbl = new DataTable();
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }

                    return tbl;
                }
            }
        }

        public async Task<FileDTO> ExportExcelBudgetPromotionAsync(Guid projectID, CancellationToken cancellationToken = default)
        {
            ExportExcel result = new ExportExcel();
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("@ProjectID", projectID);

            var sortby = string.Empty;
            sortby = nameof(BudgetPromotionSortBy.Unit_UnitNo);

            ParamList.Add("@Sys_SortBy", sortby);
            ParamList.Add("@Sys_SortType", "asc");
            ParamList.Add("@Page", 1);
            ParamList.Add("@PageSize", 999999);

            CommandDefinition commandDefinition = new(
                commandText: DBStoredNames.sp_BudgetPromotionList,
                parameters: ParamList,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken,
                transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.StoredProcedure
            );
            var queryResult = await cmd.Connection.QueryAsync<dbqBudgetPromotionList>(commandDefinition) ?? new List<dbqBudgetPromotionList>();
            queryResult = queryResult.Where(x => x.ActualTransferDate == null && !x.IsAccountApproved.Value).ToList();
            var results = queryResult.Select(x => BudgetPromotionDTO.CreateExportFromQuery(x, DB)).OrderBy(x => x.Unit?.SapwbsNo).ToList();


            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_Budget.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = BudgetPromotionExcelModel._projectNoIndex + 1;
                int _unitNoIndex = BudgetPromotionExcelModel._unitNoIndex + 1;
                int _houseNoIndex = BudgetPromotionExcelModel._houseNoIndex + 1;
                int _houseTypeIndex = BudgetPromotionExcelModel._houseTypeIndex + 1;
                int _wbsNoIndex = BudgetPromotionExcelModel._wbsNoIndex + 1;
                int _promotionPriceIndex = BudgetPromotionExcelModel._promotionPriceIndex + 1;
                int _promotionTransferPriceIndex = BudgetPromotionExcelModel._promotionTransferPriceIndex + 1;
                int _totalPriceIndex = BudgetPromotionExcelModel._totalPriceIndex + 1;
                int _UnitStatus = BudgetPromotionExcelModel._UnitStatus + 1;


                var Project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync(cancellationToken);
                for (int c = 2; c < results.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = Project.ProjectNo;
                    worksheet.Cells[c, _unitNoIndex].Value = results[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _houseNoIndex].Value = results[c - 2].Unit?.HouseNo;
                    worksheet.Cells[c, _houseTypeIndex].Value = "";
                    worksheet.Cells[c, _wbsNoIndex].Value = results[c - 2].Unit?.SapwbsNo;
                    worksheet.Cells[c, _promotionPriceIndex].Value = results[c - 2].PromotionPrice;
                    worksheet.Cells[c, _promotionTransferPriceIndex].Value = results[c - 2].PromotionTransferPrice;
                    worksheet.Cells[c, _totalPriceIndex].Value = results[c - 2].PromotionPrice + results[c - 2].PromotionTransferPrice;
                    worksheet.Cells[c, _UnitStatus].Value = results[c - 2].Unit?.UnitStatus?.Name;

                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = Project.ProjectNo + "_Budget.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = result.FileName; //$"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"project/{projectID}/export-excels/";
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var uploadResult = await FileHelper.UploadFileFromStreamWithOutGuid(fileStream, minioTempBucketName, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = result.FileName,
                Url = uploadResult.Url
            };
        }

        /// <summary>
        /// สร้าง Job ใหม่ หลังจากมีการ Update ข้อมูล Budget
        /// </summary>
        /// <returns></returns>
        public async Task CreateNewSyncJobAsync(List<BudgetPromotion> input, Guid projectID)
        {
            var model = new BudgetPromotionSyncJob();

            model.Status = BackgroundJobStatus.Waiting;
            var units = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
            var listSynItem = new List<BudgetPromotionSyncItem>();
            var temp = input.GroupBy(o => o.UnitID).Select(o => new TempBudgetPromotionQueryResult
            {
                Unit = units.Where(p => p.ID == o.Key).FirstOrDefault(),
                BudgetPromotions = o.Select(p => p).ToList()
            }).ToList();

            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
            var data = temp.Select(o => new BudgetPromotionQueryResult
            {
                Unit = o.Unit,
                BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
            }).ToList();


            var budgetPromotionSyncStatusSyncingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "BudgetPromotionSyncStatus" && o.Key == BudgetPromotionSyncStatusKeys.Syncing).Select(o => o.ID).FirstAsync();

            foreach (var item in data)
            {
                var unit = units.Where(o => o.ID == item.Unit.ID).FirstOrDefault();
                var synItem = new BudgetPromotionSyncItem();
                synItem.BudgetPromotionSyncJobID = model.ID;
                synItem.SAPWBSObject_P = unit.SAPWBSObject_P;
                synItem.SaleBudgetPromotionID = item.BudgetPromotionSale.ID;
                synItem.TransferBudgetPromotionID = item.BudgetPromotionTransfer.ID;
                synItem.Amount = Convert.ToDecimal(item.BudgetPromotionSale.Budget + item.BudgetPromotionTransfer.Budget);
                synItem.BudgetPromotionSyncStatusMasterCenterID = budgetPromotionSyncStatusSyncingMasterCenterID;
                synItem.Retry = 0;
                synItem.Currency = "THB";
                listSynItem.Add(synItem);
            }

            await DB.BudgetPromotionSyncJobs.AddAsync(model);

            await DB.BudgetPromotionSyncItems.AddRangeAsync(listSynItem);

            await DB.SaveChangesAsync();

        }


    }

}
