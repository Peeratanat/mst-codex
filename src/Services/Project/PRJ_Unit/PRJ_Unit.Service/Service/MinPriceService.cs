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
using ErrorHandling;
using ExcelExtensions;
using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using PRJ_Unit.Services.Excels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Database.Models.DbQueries.DBQueryParam;
using FileStorage;
namespace PRJ_Unit.Services
{
    public class MinPriceService : IMinPriceService
    {
        private readonly DatabaseContext DB;
        //private readonly IConfiguration Configuration;
        public LogModel logModel { get; set; }
        private FileHelper FileHelper;

        public MinPriceService(DatabaseContext db)
        {
            logModel = new LogModel("MinPriceService", null);
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

        public async Task<MinPriceDTO> GetMinPriceAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.MinPrices.Where(x => x.ProjectID == projectID && x.ID == id).GroupJoin(DB.TitledeedDetails, minprice => minprice.UnitID, titledeed => titledeed.UnitID,
                                                (minprice, titledeed) => new { Minprice = minprice, TitleDeed = titledeed })

                                                .Select(x => new MinPriceQueryResult
                                                {
                                                    MinPrice = x.Minprice,
                                                    MinPriceType = x.Minprice.MinPriceType,
                                                    DocType = x.Minprice.DocType,
                                                    Unit = x.Minprice.Unit,
                                                    UpdatedBy = x.Minprice.UpdatedBy,
                                                    Titledeed = x.TitleDeed.FirstOrDefault()
                                                }).FirstOrDefaultAsync();

            var result = MinPriceDTO.CreateFromQueryResult(model);
            return result;
        }

        public async Task<MinPriceDTO> CreateMinPriceAsync(Guid projectID, MinPriceDTO input)
        {
            MinPrice model = new MinPrice();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            model.ActiveDate = DateTime.Now;

            await DB.MinPrices.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await GetMinPriceAsync(projectID, model.ID);
            return result;
        }

        public async Task<MinPriceDTO> UpdateMinPriceAsync(Guid projectID, Guid id, MinPriceDTO input)
        {
            MinPrice model = new MinPrice();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            model.ActiveDate = DateTime.Now;
            model.ApprovedMinPrice = await GetApproveMinPrice(model.UnitID);
            await DB.MinPrices.AddAsync(model);
            await DB.SaveChangesAsync();

            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var minPriceDataStatusMasterCenterID = await MinPriceDataStatus(projectID);
            project.MinPriceDataStatusMasterCenterID = minPriceDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await GetMinPriceAsync(projectID, model.ID);
            return result;
        }

        public async Task<MinPrice> DeleteMinPriceAsync(Guid projectID, Guid id)
        {
            var model = await DB.MinPrices.Where(x => x.ProjectID == projectID && x.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var minPriceDataStatusMasterCenterID = await MinPriceDataStatus(projectID);
            project.MinPriceDataStatusMasterCenterID = minPriceDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<MinPriceExcelDTO> ImportMinPriceAsyncTemp(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // Decimal 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();
            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();
            // 1,2,3,4
            var err0070 = await DB.ErrorMessages.Where(o => o.Key == "ERR0070").FirstAsync();

            var result = new MinPriceExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>() };
            var dt = await ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 5)
            {
                throw new Exception("Invalid File Format");
            }

            var row = 2;
            var error = 0;

            var units = await DB.Units.Where(o => o.ProjectID == projectID).Select(x => new { x.ID, x.ProjectID, x.UnitNo, x.SAPWBSNo }).ToListAsync();

            var minPriceTypes = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceType).ToListAsync();
            var formatPriceTypes = new List<string> { "1", "2", "3", "4" };
            var checkNullWbsCodes = new List<string>();
            var checkNullUnitNos = new List<string>();
            var checkNullMinimumPriceTypes = new List<string>();
            var checkNullMinimumPrices = new List<string>();
            var checkUnitNotFounds = new List<string>();
            var checkFormatMinimumPrices = new List<string>();
            var checkFormatMinimumPriceTypes = new List<string>();
            //Read Excel Model
            var minPriceExcelModels = new List<MinPriceExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = MinPriceExcelModel.CreateFromDataRow(r);
                if (string.IsNullOrEmpty(r[0]?.ToString()))
                {
                    continue;
                }

                minPriceExcelModels.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.WBSCode))
                {
                    checkNullWbsCodes.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    var unit = units.Find(o => o.UnitNo == excelModel.UnitNo && o.SAPWBSNo == excelModel.WBSCode);
                    if (unit == null)
                    {

                        checkUnitNotFounds.Add((row).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(r[MinPriceExcelModel._minimumPrice].ToString()))
                {
                    checkNullMinimumPrices.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[MinPriceExcelModel._minimumPrice].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatMinimumPrices.Add((row).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(excelModel.MinimumPriceType))
                {
                    checkNullMinimumPriceTypes.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!formatPriceTypes.Contains(excelModel.MinimumPriceType))
                    {
                        checkFormatMinimumPriceTypes.Add((row).ToString());
                        isError = true;
                    }
                }
                if (isError)
                {
                    error++;
                }
                #endregion

                row++;
            }

            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            ValidateException ex = new ValidateException();
            if (minPriceExcelModels.Where(o => !string.IsNullOrEmpty(o.ProjectNo)).Any(o => o.ProjectNo != project.ProjectNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            List<MinPrice> minPrices = new List<MinPrice>();
            //Update Data
            #region Add Result Validate
            if (checkNullWbsCodes.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBS Code");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBS Code");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullUnitNos.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkNullMinimumPrices.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "Minimum Price");
                    msg = msg.Replace("[row]", String.Join(",", checkNullMinimumPrices));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "Minimum Price");
                    msg = msg.Replace("[row]", String.Join(",", checkNullMinimumPrices));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullMinimumPriceTypes.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "Minimum Price Type");
                    msg = msg.Replace("[row]", String.Join(",", checkNullMinimumPriceTypes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "Minimum Price Type");
                    msg = msg.Replace("[row]", String.Join(",", checkNullMinimumPriceTypes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkUnitNotFounds.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkUnitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkUnitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatMinimumPrices.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "Minimum Price");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatMinimumPrices));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "Minimum Price");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatMinimumPrices));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatMinimumPriceTypes.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0070.Message.Replace("[column]", "Minimum Price Type");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatMinimumPriceTypes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0070.Message.Replace("[column]", "Minimum Price Type");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatMinimumPriceTypes));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region RowErrors
            var rowErrors = new List<string>();
            rowErrors.AddRange(checkNullWbsCodes);
            rowErrors.AddRange(checkNullUnitNos);
            rowErrors.AddRange(checkNullUnitNos);
            rowErrors.AddRange(checkNullMinimumPriceTypes);
            rowErrors.AddRange(checkNullMinimumPrices);
            rowErrors.AddRange(checkUnitNotFounds);
            rowErrors.AddRange(checkFormatMinimumPrices);
            rowErrors.AddRange(checkFormatMinimumPriceTypes);
            #endregion

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();

            row = 2;
            foreach (var item in minPriceExcelModels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var unit = units.Find(o => o.ProjectID == projectID && o.UnitNo == item.UnitNo && o.SAPWBSNo == item.WBSCode);
                    if (unit != null)
                    {
                        MinPrice min = new MinPrice();
                        Guid? minpricetypemastercenterID = minPriceTypes.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceType && o.Key == item.MinimumPriceType)?.ID;
                        min.MinPriceTypeMasterCenterID = minpricetypemastercenterID;
                        min.Cost = Convert.ToDecimal(item.MinimumPrice);

                        var minPrice = await GetMinPrice(unit.ID);

                        if (item.MinimumPriceType == "2")
                        {
                            min.ROIMinprice = Convert.ToDecimal(item.MinimumPrice);
                        }
                        else
                        {
                            min.ROIMinprice = minPrice?.ROIMinprice;
                        }

                        min.SalePrice = minPrice?.SalePrice;
                        min.ApprovedMinPrice = minPrice?.ApprovedMinPrice;

                        min.ActiveDate = DateTime.Now;
                        min.ProjectID = projectID;
                        min.UnitID = unit.ID;
                        minPrices.Add(min);
                    }
                }
                row++;
            }
            await DB.MinPrices.AddRangeAsync(minPrices);
            await DB.SaveChangesAsync();
            result.Success = minPrices.Count();
            result.Error = error;
            return result;
        }

        public async Task<MinPriceExcelDTO> ImportMinPriceAsync(Guid projectID, FileDTO input, Guid? UserID = null)
        {
            var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
            var result = new MinPriceExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>(), Messages = new List<string>() };
            //var dt = await ConvertExcelToDataTable(input);
            /// Valudate Header
            //if (dt.Columns.Count != 5)
            //{
            //	result.Messages.Add("Invalid File Format");
            //	result.Success = 0;
            //	return result;
            //}

            //ValidateException ex = new ValidateException();
            //if (dt.Columns.Count != 5)
            //{
            //    var msg = "Invalid File Format";
            //    ex.AddError("", msg, 1);
            //}
            //if (ex.HasError)
            //{
            //    throw ex;
            //}


            if (input.IsTemp)
            {
                string Name = "MinPrice.xlsx";
                string minpriceName = $"import-project/{projectNo}/minprice/{Name}";
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
            imp.Import_Type = "minprice";
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

            ////Stream ddd = new MemoryStream(test);

            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention.ToLower() == "xls")
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

        private async Task<Guid> MinPriceDataStatus(Guid projectID)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new();
            ParamList.Add("@ProjectID", projectID);

            var sortby = string.Empty;
            bool sort = true;
            sortby = nameof(MinPriceSortBy.Unit_UnitNo);
            ParamList.Add("@Sys_SortBy", sortby);
            ParamList.Add("@Sys_SortType", sort ? "asc" : "desc");
            ParamList.Add("@Page", 1);
            ParamList.Add("@PageSize", 999999999);

            CommandDefinition commandDefinition = new(
                                 commandText: DBStoredNames.sp_MinPriceList,
                                 parameters: ParamList,
                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                 commandType: CommandType.StoredProcedure);
            var queryResult = await cmd.Connection.QueryAsync<dbqMinPriceList>(commandDefinition) ?? new List<dbqMinPriceList>();
            var results = queryResult.Select(async x => await MinPriceDTO.CreateFromQueryAsync(x, DB)).Select(o => o.Result).ToList();

            var minPriceDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectDataStatus && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync(); //อยู่ระหว่างจัดเตรียม
            var minPriceDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectDataStatus && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstAsync(); //อยู่ระหว่างขาย
            var minPriceDataStatusMasterCenterID = minPriceDataStatusPrepareMasterCenterID;


            // var results = query.Select(o => MinPriceDTO.CreateFromQueryResult(o)).ToList();

            if (results.TrueForAll(o => o.Cost != null
                                  && o.MinPriceType != null))
            {
                minPriceDataStatusMasterCenterID = minPriceDataStatusSaleMasterCenterID;
            }

            return minPriceDataStatusMasterCenterID;

        }

        private async Task<decimal?> GetApproveMinPrice(Guid? unitID)
        {
            var query = await DB.MinPrices
                                  .Where(o => o.UnitID == unitID)
                                  .Select(o => new MinPriceQueryResult
                                  {
                                      MinPrice = o,
                                      MinPriceType = o.MinPriceType,
                                      DocType = o.DocType,
                                      Unit = o.Unit,
                                  }).ToListAsync();

            var queryResult = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
            {
                Unit = o.Key,
                MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
                MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault()
            }).FirstOrDefault();


            return queryResult?.MinPrice?.ApprovedMinPrice;
        }

        private async Task<decimal?> GetROIMinprice(Guid? unitID)
        {
            var query = await DB.MinPrices
                                  .Where(o => o.UnitID == unitID)
                                  .Select(o => new MinPriceQueryResult
                                  {
                                      MinPrice = o,
                                      MinPriceType = o.MinPriceType,
                                      DocType = o.DocType,
                                      Unit = o.Unit,
                                  }).ToListAsync();

            var queryResult = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
            {
                Unit = o.Key,
                MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
                MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault()
            }).FirstOrDefault();


            return queryResult?.MinPrice?.ROIMinprice;
        }

        //private async Task<decimal?> GetSalePrice(Guid? unitID)
        //{
        //	var query = await DB.MinPrices
        //						  .Where(o => o.UnitID == unitID)
        //						  .Select(o => new MinPriceQueryResult
        //						  {
        //							  MinPrice = o,
        //							  MinPriceType = o.MinPriceType,
        //							  DocType = o.DocType,
        //							  Unit = o.Unit,
        //						  }).ToListAsync();

        //	var queryResult = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
        //	{
        //		Unit = o.Key,
        //		MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
        //		DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
        //		MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault()
        //	}).FirstOrDefault();


        //	return queryResult?.MinPrice?.SalePrice;
        //}

        private async Task<MinPrice> GetMinPrice(Guid? unitID)
        {
            var query = await DB.MinPrices
                                  .Where(o => o.UnitID == unitID)
                                  .Select(o => new MinPriceQueryResult
                                  {
                                      MinPrice = o,
                                      //MinPriceType = o.MinPriceType,
                                      //DocType = o.DocType,
                                      //Unit = o.Unit,
                                  }).ToListAsync();

            var queryResult = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
            {
                //Unit = o.Key,
                MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                //DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
                //MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault()
            }).FirstOrDefault();


            return queryResult?.MinPrice;
        }

        public async Task<MinPricePaging> GetMinPriceListAsync(Guid projectID, MinPriceFilter filter, PageParam pageParam, MinPriceSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new();
            ParamList.Add("@ProjectID", projectID);
            ParamList.Add("@UnitNo", filter.UnitNo);
            ParamList.Add("@HouseNo", filter.HouseNo);
            ParamList.Add("@SaleAreaFrom", filter.SaleAreaFrom ?? 0);
            ParamList.Add("@SaleAreaTo", filter.SaleAreaTo ?? 0);
            ParamList.Add("@SalePriceFrom", filter.SalePriceFrom ?? 0);
            ParamList.Add("@SalePriceTo", filter.SalePriceTo ?? 0);
            ParamList.Add("@CostFrom", filter.CostFrom ?? 0);
            ParamList.Add("@CostTo", filter.CostTo ?? 0);
            ParamList.Add("@ROIMinpriceFrom", filter.ROIMinpriceFrom ?? 0);
            ParamList.Add("@ROIMinpriceTo", filter.ROIMinpriceTo ?? 0);
            ParamList.Add("@ApprovedMinPriceFrom", filter.ApprovedMinPriceFrom ?? 0);
            ParamList.Add("@ApprovedMinPriceTo", filter.ApprovedMinPriceTo ?? 0);
            ParamList.Add("@TitledeedAreaFrom", filter.TitledeedAreaFrom ?? 0);
            ParamList.Add("@TitledeedAreaTo", filter.TitledeedAreaTo ?? 0);
            ParamList.Add("@MinPriceTypeKey", filter.MinPriceTypeKey);
            ParamList.Add("@DocTypeKey", filter.DocTypeKey);
            ParamList.Add("@UpdatedBy", filter.UpdatedBy);
            ParamList.Add("@UpdatedFrom", filter.UpdatedFrom);
            ParamList.Add("@UpdatedTo", filter.UpdatedTo);

            var sortby = string.Empty;
            bool sort = true;
            sortby = nameof(MinPriceSortBy.Unit_UnitNo);
            if (sortByParam.SortBy != null)
            {
                sort = sortByParam.Ascending;
                switch (sortByParam.SortBy.Value)
                {
                    case MinPriceSortBy.Unit_HouseNo:
                        sortby = nameof(MinPriceSortBy.Unit_HouseNo);
                        break;
                    case MinPriceSortBy.Unit_SaleArea:
                        sortby = nameof(MinPriceSortBy.Unit_SaleArea);
                        break;
                    case MinPriceSortBy.Titledeed_TitledeedArea:
                        sortby = nameof(MinPriceSortBy.Titledeed_TitledeedArea);
                        break;
                    case MinPriceSortBy.Cost:
                        sortby = nameof(MinPriceSortBy.Cost);
                        break;
                    case MinPriceSortBy.MinPriceType:
                        sortby = nameof(MinPriceSortBy.MinPriceType);
                        break;
                    case MinPriceSortBy.ROIMinprice:
                        sortby = nameof(MinPriceSortBy.ROIMinprice);
                        break;
                    case MinPriceSortBy.SalePrice:
                        sortby = nameof(MinPriceSortBy.SalePrice);
                        break;
                    case MinPriceSortBy.ApprovedMinPrice:
                        sortby = nameof(MinPriceSortBy.ApprovedMinPrice);
                        break;
                    case MinPriceSortBy.DocType:
                        sortby = nameof(MinPriceSortBy.DocType);
                        break;
                    case MinPriceSortBy.Updated:
                        sortby = nameof(MinPriceSortBy.Updated);
                        break;
                    case MinPriceSortBy.UpdatedBy:
                        sortby = nameof(MinPriceSortBy.UpdatedBy);
                        break;
                    case MinPriceSortBy.Unit_UnitNo:
                        sortby = nameof(MinPriceSortBy.Unit_UnitNo);
                        break;
                    default:
                        sortby = nameof(MinPriceSortBy.Unit_UnitNo);
                        break;
                }
            }

            ParamList.Add("@Sys_SortBy", sortby);
            ParamList.Add("@Sys_SortType", sort ? "asc" : "desc");
            ParamList.Add("@Page", pageParam?.Page ?? 1);
            ParamList.Add("@PageSize", pageParam?.PageSize ?? 10);
            CommandDefinition commandDefinition = new(
                         commandText: DBStoredNames.sp_MinPriceList,
                         parameters: ParamList,
                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                         commandType: CommandType.StoredProcedure,
                         cancellationToken: cancellationToken
                     );
            var queryResult = await cmd.Connection.QueryAsync<dbqMinPriceList>(commandDefinition) ?? new List<dbqMinPriceList>();
            var result = queryResult.Select(async x => await MinPriceDTO.CreateFromQueryAsync(x, DB)).Select(o => o.Result).ToList();

            return new MinPricePaging()
            {
                PageOutput = queryResult.FirstOrDefault() != null ? queryResult.FirstOrDefault().CreateBaseDTOFromQuery() : new PageOutput(),
                MinPrices = result ?? new List<MinPriceDTO>()
            };
        }

        public async Task<FileDTO> ExportExcelMinPriceAsync(Guid projectID, MinPriceFilter filter, MinPriceSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("@ProjectID", projectID);
            ParamList.Add("@UnitNo", filter.UnitNo);
            ParamList.Add("@HouseNo", filter.HouseNo);
            ParamList.Add("@SaleAreaFrom", filter.SaleAreaFrom ?? 0);
            ParamList.Add("@SaleAreaTo", filter.SaleAreaTo ?? 0);
            ParamList.Add("@SalePriceFrom", filter.SalePriceFrom ?? 0);
            ParamList.Add("@SalePriceTo", filter.SalePriceTo ?? 0);
            ParamList.Add("@CostFrom", filter.CostFrom ?? 0);
            ParamList.Add("@CostTo", filter.CostTo ?? 0);
            ParamList.Add("@ROIMinpriceFrom", filter.ROIMinpriceFrom ?? 0);
            ParamList.Add("@ROIMinpriceTo", filter.ROIMinpriceTo ?? 0);
            ParamList.Add("@ApprovedMinPriceFrom", filter.ApprovedMinPriceFrom ?? 0);
            ParamList.Add("@ApprovedMinPriceTo", filter.ApprovedMinPriceTo ?? 0);
            ParamList.Add("@TitledeedAreaFrom", filter.TitledeedAreaFrom ?? 0);
            ParamList.Add("@TitledeedAreaTo", filter.TitledeedAreaTo ?? 0);
            ParamList.Add("@MinPriceTypeKey", filter.MinPriceTypeKey);
            ParamList.Add("@DocTypeKey", filter.DocTypeKey);
            ParamList.Add("@UpdatedBy", filter.UpdatedBy);
            ParamList.Add("@UpdatedFrom", filter.UpdatedFrom);
            ParamList.Add("@UpdatedTo", filter.UpdatedTo);


            var sortby = string.Empty;
            bool sort = true;
            sortby = nameof(MinPriceSortBy.Unit_UnitNo);
            if (sortByParam.SortBy != null)
            {
                sort = sortByParam.Ascending;
                switch (sortByParam.SortBy.Value)
                {
                    case MinPriceSortBy.Unit_HouseNo:
                        sortby = nameof(MinPriceSortBy.Unit_HouseNo);
                        break;
                    case MinPriceSortBy.Unit_SaleArea:
                        sortby = nameof(MinPriceSortBy.Unit_SaleArea);
                        break;
                    case MinPriceSortBy.Titledeed_TitledeedArea:
                        sortby = nameof(MinPriceSortBy.Titledeed_TitledeedArea);
                        break;
                    case MinPriceSortBy.Cost:
                        sortby = nameof(MinPriceSortBy.Cost);
                        break;
                    case MinPriceSortBy.MinPriceType:
                        sortby = nameof(MinPriceSortBy.MinPriceType);
                        break;
                    case MinPriceSortBy.ROIMinprice:
                        sortby = nameof(MinPriceSortBy.ROIMinprice);
                        break;
                    case MinPriceSortBy.SalePrice:
                        sortby = nameof(MinPriceSortBy.SalePrice);
                        break;
                    case MinPriceSortBy.ApprovedMinPrice:
                        sortby = nameof(MinPriceSortBy.ApprovedMinPrice);
                        break;
                    case MinPriceSortBy.DocType:
                        sortby = nameof(MinPriceSortBy.DocType);
                        break;
                    case MinPriceSortBy.Updated:
                        sortby = nameof(MinPriceSortBy.Updated);
                        break;
                    case MinPriceSortBy.UpdatedBy:
                        sortby = nameof(MinPriceSortBy.UpdatedBy);
                        break;
                    case MinPriceSortBy.Unit_UnitNo:
                        sortby = nameof(MinPriceSortBy.Unit_UnitNo);
                        break;
                    default:
                        sortby = nameof(MinPriceSortBy.Unit_UnitNo);
                        break;
                }
            }
            ParamList.Add("@Sys_SortBy", sortby);
            ParamList.Add("@Sys_SortType", sort ? "asc" : "desc");
            ParamList.Add("@Page", 1);
            ParamList.Add("@PageSize", 999999);


            CommandDefinition commandDefinition = new(
                                 commandText: DBStoredNames.sp_MinPriceList,
                                 parameters: ParamList,
                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                 commandType: CommandType.StoredProcedure,
                                 cancellationToken: cancellationToken
                             );
            var queryResult = await cmd.Connection.QueryAsync<dbqMinPriceList>(commandDefinition) ?? new List<dbqMinPriceList>();
            var results = queryResult.Select(async x => await MinPriceDTO.CreateFromQueryAsync(x, DB)).Select(o => o.Result)
            .OrderBy(x => x.Unit?.SapwbsNo).ToList();

            ExportExcel result = new ExportExcel();
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_MinPrice.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = MinPriceExcelModel._projectNoIndex + 1;
                int _wbsNoIndex = MinPriceExcelModel._wbsCodeIndex + 1;
                int _unitNoIndex = MinPriceExcelModel._unitNoIndex + 1;
                int _minimumPrice = MinPriceExcelModel._minimumPrice + 1;
                int _minimumPriceType = MinPriceExcelModel._minimumPriceType + 1;
                int _unitStatus = MinPriceExcelModel._unitStatus + 1;

                var project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
                for (int c = 2; c < results.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = project.ProjectNo;
                    worksheet.Cells[c, _wbsNoIndex].Value = results[c - 2].Unit?.SapwbsNo;
                    worksheet.Cells[c, _unitNoIndex].Value = results[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _minimumPrice].Value = results[c - 2].Cost;
                    worksheet.Cells[c, _minimumPriceType].Value = results[c - 2].MinPriceType?.Key;
                    worksheet.Cells[c, _unitStatus].Value = results[c - 2].Unit?.UnitStatus?.Name;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ProjectNo + "_MinPrice.xlsx";
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

        public async Task<FileDTO> ExportProjectMinPriceToSAPAsync(Guid projectID)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("@ProjectID", projectID);

            CommandDefinition commandDefinition = new(
                commandText: DBStoredNames.sp_MinPriceSAPList,
                parameters: ParamList,
                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.StoredProcedure
            );
            var queryResult = await cmd.Connection.QueryAsync<dbqMinPriceSAPList>(commandDefinition) ?? new List<dbqMinPriceSAPList>();
            var results = queryResult.Select(x => MinPriceSapDTO.CreateFromQuery(x)).ToList();
            var project = await DB.Projects.FirstOrDefaultAsync(x => x.ID == projectID);

            ExportExcel result = new ExportExcel();
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_MinpriceSAP.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectCodeIndex = MinPriceSAPExcelModel._projectCodeIndex + 1;
                int _wbsCodeIndex = MinPriceSAPExcelModel._wbsCodeIndex + 1;
                int _objectCodeIndex = MinPriceSAPExcelModel._objectCodeIndex + 1;
                int _companyCodeIndex = MinPriceSAPExcelModel._companyCodeIndex + 1;
                int _boqStyleIndexType = MinPriceSAPExcelModel._boqStyleIndex + 1;
                int _homeTyleIndex = MinPriceSAPExcelModel._homeTyleIndex + 1;
                int _wbsStatusIndex = MinPriceSAPExcelModel._wbsStatusIndex + 1;
                int _minimumPriceIndex = MinPriceSAPExcelModel._minimumPriceIndex + 1;
                int _saleAreaIndex = MinPriceSAPExcelModel._saleAreaIndex + 1;

                for (int c = 2; c < results.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectCodeIndex].Value = results[c - 2].SapCode;
                    worksheet.Cells[c, _wbsCodeIndex].Value = results[c - 2].SAPWBSNo;
                    worksheet.Cells[c, _objectCodeIndex].Value = results[c - 2].SAPWBSObject;
                    worksheet.Cells[c, _companyCodeIndex].Value = results[c - 2].CompanyCode;
                    worksheet.Cells[c, _boqStyleIndexType].Value = results[c - 2].BOQStyle;
                    worksheet.Cells[c, _homeTyleIndex].Value = results[c - 2].HomeStyle;
                    worksheet.Cells[c, _wbsStatusIndex].Value = results[c - 2].SAPWBSStatus;
                    worksheet.Cells[c, _minimumPriceIndex].Value = results[c - 2].Minprice;
                    worksheet.Cells[c, _saleAreaIndex].Value = results[c - 2].SaleArea;
                }

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ProjectNo + "_MinPriceSAP.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = result.FileName;
            string contentType = result.FileType;
            string filePath = $"project/{projectID}/export-excels/";
            // var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            // var uploadResult = await FileHelper.UploadFileFromStreamWithOutGuid(fileStream, minioTempBucketName, filePath, fileName, contentType);
            var uploadResult = await FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = result.FileName,
                Url = uploadResult.Url
            };
        }
    }
}
