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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using PRJ_Unit.Services.Excels;
using PRJ_Unit.Services.Sap;
using System.Data;
using System.Data.Common;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Globalization;
using FileStorage; 
namespace PRJ_Unit.Services
{
    public class UnitService : IUnitService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        private readonly DbQueryContext DBQuery;
        private FileHelper FileHelper;
        int Timeout = 300;
        public UnitService(DatabaseContext db, DbQueryContext dbQuery)
        {
            logModel = new LogModel("UnitService", null);
            DB = db;
            DBQuery = dbQuery;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Environment.GetEnvironmentVariable("DBConnectionString"));
            Timeout = builder.ConnectTimeout;
            DB.Database.SetCommandTimeout(Timeout);

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }
        public UnitService(DatabaseContext db)
        {
            logModel = new LogModel("UnitService", null);
            DB = db;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Environment.GetEnvironmentVariable("DBConnectionString"));
            Timeout = builder.ConnectTimeout;
            DB.Database.SetCommandTimeout(Timeout);

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }


        public async Task<List<UnitDropdownDTO>> GetUnitDropdownListAsync(Guid projectID, Guid? towerID = null, Guid? floorID = null, string name = null, string unitStatusKey = null, bool? allUnit = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Unit> query = DB.Units.AsNoTracking().Where(x => x.ProjectID == projectID);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.UnitNo.Contains(name));
            }
            if (towerID != null)
            {
                query = query.Where(o => o.TowerID == towerID);
            }
            if (floorID != null)
            {
                query = query.Where(o => o.FloorID == floorID);
            }
            if (!string.IsNullOrEmpty(unitStatusKey))
            {
                query = query.Where(o => o.UnitStatus.Key == unitStatusKey);
            }

            if (!(allUnit ?? false))
            {
                query = query.Where(o => o.AssetType.Key == AssetTypeKeys.Unit);
            }
            else
            {
                List<string> AssetTypeKeyList = [AssetTypeKeys.Unit, AssetTypeKeys.SampleModelHome];
                query = query.Where(o => AssetTypeKeyList.Contains(o.AssetType.Key));
            }

            var queryResults = await query.OrderBy(o => o.UnitNo).ToListAsync(cancellationToken);
            var results = queryResults.Select(o => UnitDropdownDTO.CreateFromModel(o)).ToList();

            return results;

        }

        public async Task<List<UnitDropdownDTO>> GetUnitQuotationDropdownListAsync(Guid projectID, Guid? towerID = null, Guid? floorID = null, string name = null, string unitStatusKey = null, bool? allUnit = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Unit> query = DB.Units.AsNoTracking().Where(x => x.ProjectID == projectID);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.UnitNo.Contains(name));
            }
            if (towerID != null)
            {
                query = query.Where(o => o.TowerID == towerID);
            }
            if (floorID != null)
            {
                query = query.Where(o => o.FloorID == floorID);
            }
            if (!string.IsNullOrEmpty(unitStatusKey))
            {
                query = query.Where(o => o.UnitStatus.Key == unitStatusKey);
            }

            if (!(allUnit ?? false))
            {
                query = query.Where(o => o.AssetType.Key == AssetTypeKeys.Unit);
            }
            else
            {
                List<string> AssetTypeKeyList = [AssetTypeKeys.Unit, AssetTypeKeys.SampleModelHome];
                query = query.Where(o => AssetTypeKeyList.Contains(o.AssetType.Key));
            }

            var queryResults = await query.OrderBy(o => o.UnitNo).ToListAsync(cancellationToken);
            var results = queryResults.Select(o => UnitDropdownDTO.CreateFromModel(o)).ToList();


            //ไม่เอาที่จ่ายเงินโดย Prebook แล้ว
            var qrs = await DB.PaymentPrebooks.AsNoTracking()
                        .Include(o => o.Quotation)
                            .ThenInclude(o => o.Unit)
                        .ToListAsync(cancellationToken);

            var unitPaidPrebookIDs = qrs.Where(o => o.Quotation.IsPrebook).Select(o => o.Quotation.Unit.ID).ToList();
            var resultsUnitUse = results.Where(o => !unitPaidPrebookIDs.Contains(o.Id)).ToList();

            return resultsUnitUse;

        }

        public async Task<List<UnitDropdownSellPriceDTO>> GetUnitDropdownWithSellPriceListAsync(Guid projectID, string name, string unitStatusKey = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Unit> query = DB.Units.AsNoTracking().Where(x => x.ProjectID == projectID);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.UnitNo.Contains(name));
            }
            if (!string.IsNullOrEmpty(unitStatusKey))
            {
                query = query.Where(o => o.UnitStatus.Key == unitStatusKey);
            }

            var queryResults = await query.OrderBy(o => o.UnitNo).Take(50).ToListAsync(cancellationToken);
            var results = queryResults.Select(async o => await UnitDropdownSellPriceDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();

            return results;
        }

        public async Task<UnitPaging> GetUnitListAsync(Guid projectID, UnitFilter filter, PageParam pageParam, UnitListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var query = from o in DB.Units
                                        .Include(o => o.Model)
                                        .Include(o => o.UnitStatus)
                                        .Include(o => o.Model.TypeOfRealEstate)
                                        .Include(o => o.Tower)
                                        .OrderBy(o => o.SAPWBSNo)
                        where o.ProjectID == projectID
                        select new UnitQueryResult
                        {
                            Model = o.Model,
                            Floor = o.Floor,
                            UnitDirection = o.UnitDirection,
                            Tower = o.Tower,
                            Unit = o,
                            TitledeedDetail = DB.TitledeedDetails
                                    .Where(f => f.ProjectID == projectID && f.UnitID == o.ID)
                                    .GroupBy(f => f.UnitID)
                                    .Select(g => g.OrderByDescending(f => f.Created).FirstOrDefault())
                                    .FirstOrDefault(),
                            UnitStatus = o.UnitStatus,
                            UnitType = o.UnitType,
                            AssetType = o.AssetType,
                            UpdatedBy = o.UpdatedBy
                        };
            #region Filter
            if (filter.IsAddressProject ?? false) //AddressProject
            {
                if (filter.AddressID != null) //upDate
                {
                    query = query.Where(x => x.Unit.AddressID == filter.AddressID || x.Unit.AddressID == null);
                }
                else //add
                {
                    query = query.Where(x => x.Unit.AddressID == null);
                }
            }
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo));
            }
            if (!string.IsNullOrEmpty(filter.ModelCode))
            {
                query = query.Where(x => x.Model.Code.Contains(filter.ModelCode));
            }
            if (filter.TypeOfRealEstateID != null && filter.TypeOfRealEstateID != Guid.Empty)
            {
                query = query.Where(o => o.Model.TypeOfRealEstateID == filter.TypeOfRealEstateID);
            }
            if (!string.IsNullOrEmpty(filter.ModelName))
            {
                query = query.Where(o => o.Model.NameTH.Contains(filter.ModelName));
            }
            if (filter.TowerID != null && filter.TowerID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.TowerID == filter.TowerID);
            }
            if (filter.FloorID != null && filter.FloorID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.FloorID == filter.FloorID);
            }
            if (!string.IsNullOrEmpty(filter.UnitDirectionKey))
            {
                var unitDirectionMasterCenterID = (await DB.MasterCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Key == filter.UnitDirectionKey
                                                                       && x.MasterCenterGroupKey == "UnitDirection")
                                                                     )?.ID;
                if (unitDirectionMasterCenterID is not null)
                    query = query.Where(x => x.Unit.UnitDirectionMasterCenterID == unitDirectionMasterCenterID);
            }

            if (!string.IsNullOrEmpty(filter.AssetTypeKey))
            {
                var assetTypeMasterCenterID = (await DB.MasterCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Key == filter.AssetTypeKey
                                                                       && x.MasterCenterGroupKey == "AssetType")
                                                                      )?.ID;
                if (assetTypeMasterCenterID is not null)
                    query = query.Where(x => x.Unit.AssetTypeMasterCenterID == assetTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = (await DB.MasterCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Key == filter.UnitStatusKey
                                                                       && x.MasterCenterGroupKey == "UnitStatus")
                                                                      )?.ID;
                if (unitStatusMasterCenterID is not null)
                    query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
            }

            if (filter.SaleAreaFrom != null)
            {
                query = query.Where(x => x.Unit.SaleArea >= filter.SaleAreaFrom);
            }
            if (filter.SaleAreaTo != null)
            {
                query = query.Where(x => x.Unit.SaleArea <= filter.SaleAreaTo);
            }
            if (filter.SaleAreaFrom != null && filter.SaleAreaTo != null)
            {
                query = query.Where(x => x.Unit.SaleArea >= filter.SaleAreaFrom
                                    && x.Unit.SaleArea <= filter.SaleAreaTo);
            }
            if (filter.TitleDeedAreaFrom != null)
            {
                query = query.Where(o => (o.Unit.TitledeedDetails.FirstOrDefault() != null ? o.Unit.TitledeedDetails.FirstOrDefault().TitledeedArea : (double?)0) >= filter.TitleDeedAreaFrom);
            }
            if (filter.TitleDeedAreaTo != null)
            {
                query = query.Where(o => (o.Unit.TitledeedDetails.FirstOrDefault() != null ? o.Unit.TitledeedDetails.FirstOrDefault().TitledeedArea : (double?)0) <= filter.TitleDeedAreaTo);
            }
            if (filter.isForeignUnit != null)
            {
                query = query.Where(o => o.Unit.IsForeignUnit == filter.isForeignUnit.Equals("1") ? false : true);
            }

            #endregion

            UnitDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<UnitQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => UnitDTO.CreateFromQueryResult(o)).ToList();

            return new UnitPaging()
            {
                PageOutput = pageOutput,
                Units = results
            };

        }

        public async Task<UnitDTO> GetUnitAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Units.Where(o => o.ProjectID == projectID && o.ID == id)
                .Include(o => o.Model).ThenInclude(o => o.TypeOfRealEstate)
                                      .Include(o => o.Model)
                                      .Include(o => o.Floor)
                                      .Include(o => o.UnitDirection)
                                      .Include(o => o.Tower)
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.UnitType)
                                      .Include(o => o.AssetType)
                                      .Include(o => o.UpdatedBy)
                                      .Include(o => o.TitledeedDetails)
                                      .FirstOrDefaultAsync(cancellationToken);
            if (model is null)
            {
                return null;
            }


            #region ImageName
            var floorPlan = await DB.FloorPlanImages.Where(o => o.Name == model.FloorPlanFileName)
                                                .Select(o => FloorPlanImageDTO.CreateFromModelAsync(o, FileHelper))
                                                .Select(o => o.Result)
                                                .FirstOrDefaultAsync(cancellationToken);
            var roomPlan = await DB.RoomPlanImages.Where(o => o.Name == model.RoomPlanFileName)
                                                .Select(o => RoomPlanImageDTO.CreateFromModelAsync(o, FileHelper))
                                                .Select(o => o.Result)
                                                .FirstOrDefaultAsync(cancellationToken);
            #endregion


            var result = UnitDTO.CreateFromModel(model, floorPlan, roomPlan);
            return result;
        }

        public async Task<UnitDTO> CreateUnitAsync(Guid projectID, UnitDTO input)
        {
            await input.ValidateAsync(DB);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            Unit model = new Unit();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            await DB.Units.AddAsync(model);
            await DB.SaveChangesAsync();

            var unitDataStatusMasterCenterID = await UnitDataStatus(projectID);
            project.UnitDataStatusMasterCenterID = unitDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await GetUnitAsync(projectID, model.ID);
            return result;
        }

        public async Task<UnitDTO> UpdateUnitAsync(Guid projectID, Guid id, UnitDTO input, Guid? userID = null)
        {
            await input.ValidateAsync(DB);
            var project = await DB.Projects.FindAsync(projectID);
            var model = await DB.Units.FirstAsync(o => o.ProjectID == projectID && o.ID == id);
            var OldNotSale = model.IsNotSale;
            input.ToModel(ref model);
            model.ProjectID = projectID;

            if (input.IsNotSale != OldNotSale)
            {
                model.NotSaleByUserID = userID;
            }


            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var titleDeed = await DB.TitledeedDetails.FirstOrDefaultAsync(o => o.UnitID == model.ID);
            if (input.TitleDeed?.TitledeedArea != null && titleDeed.TitledeedArea != input.TitleDeed?.TitledeedArea)
            {
                titleDeed.TitledeedArea = input.TitleDeed.TitledeedArea;
                DB.Entry(titleDeed).State = EntityState.Modified;
            }

            var unitDataStatusMasterCenterID = await UnitDataStatus(projectID);
            project.UnitDataStatusMasterCenterID = unitDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await GetUnitAsync(projectID, id);
            return result;
        }

        public async Task<Unit> DeleteUnitAsync(Guid projectID, Guid id)
        {
            ValidateException ex = new ValidateException();

            var project = await DB.Projects.FindAsync(projectID);
            var model = await DB.Units.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();
            var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == "0" && x.MasterCenterGroupKey == "UnitStatus").Select(x => x.ID).FirstAsync();
            if (model.UnitStatusMasterCenterID != unitStatusMasterCenterID)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0142").FirstAsync();
                string desc = model.UnitNo;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var unitDataStatusMasterCenterID = await UnitDataStatus(projectID);
            project.UnitDataStatusMasterCenterID = unitDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<Base.DTOs.PRJ.UnitInfoDTO> GetUnitInfoAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync(cancellationToken);
            var model = await DB.Units.Include(o => o.Model)
                                       .ThenInclude(o => o.TypeOfRealEstate)
                                      .Include(o => o.Floor)
                                      .Include(o => o.UnitDirection)
                                      .Include(o => o.Tower)
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.UnitType)
                                      .Include(o => o.AssetType)
                                      .Include(o => o.TitledeedDetails)
                                      .Include(o => o.UpdatedBy)
                .Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync(cancellationToken);
            var result = Base.DTOs.PRJ.UnitInfoDTO.CreateFromModel(model);
            return result;
        }

        public async Task<UnitInitialExcelDTO> ImportUnitInitialAsyncTemp(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // String Format (Eng And Number)
            var err0064 = await DB.ErrorMessages.Where(o => o.Key == "ERR0064").FirstAsync();
            // Decimal Format 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();

            var rowErrors = new List<string>();

            var checkNullWbsCodes = new List<string>();
            var checkNullObjectCodes = new List<string>();
            var checkFormatObjectCodes = new List<string>();
            var checkFormatSaleArea = new List<string>();

            var result = new UnitInitialExcelDTO();
            var dt = await ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 9)
            {
                throw new Exception("Invalid File Format");
            }
            //Read Excel Model
            var checkFormatUnitArea = new List<string>();
            var unitExcels = new List<UnitInitialExcelModel>();
            var row = 1;
            var error = 0;
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;

                var excelModel = UnitInitialExcelModel.CreateFromDataRow(r);

                #region Validate
                if (!string.IsNullOrEmpty(r[UnitInitialExcelModel._saleAreaIndex].ToString()))
                {
                    if (!r[UnitInitialExcelModel._saleAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatSaleArea.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(excelModel.SAPWBSNo))
                {
                    checkNullWbsCodes.Add((row + 1).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.SAPWBSObject))
                {
                    checkNullObjectCodes.Add((row + 1).ToString());
                    isError = true;
                }
                else
                {
                    if (!excelModel.SAPWBSObject.CheckLang(false, true, false, false))
                    {
                        checkFormatObjectCodes.Add((row + 1).ToString());
                        isError = true;
                    }
                }

                if (isError)
                {
                    error++;
                }
                #endregion

                unitExcels.Add(excelModel);

                row++;
            }

            List<Unit> addingUnits = new List<Unit>();
            List<Unit> updatingUnits = new List<Unit>();
            List<Unit> deletingUnits = new List<Unit>();

            List<Floor> addingFloors = new List<Floor>();
            List<Tower> addingTowers = new List<Tower>();

            var units = await DB.Units.Where(o => o.ProjectID == projectID).Include(o => o.UnitStatus).ToListAsync();
            var project = await (from p in DB.Projects.Include(o => o.ProductType)
                                 where p.ID == projectID
                                 select p).FirstAsync();
            #region Validate ProjectCode
            ValidateException ex = new ValidateException();
            if (unitExcels.Any(o => o.ProjectSapCode != project.SapCode))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "PROJECTCODE");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            #endregion

            #region Add Result Validate
            if (checkNullWbsCodes.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBSCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBSCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullObjectCodes.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "OBJECTCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullObjectCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "OBJECTCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullObjectCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatObjectCodes.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0064.Message.Replace("[column]", "OBJECTCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatObjectCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0064.Message.Replace("[column]", "OBJECTCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatObjectCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatSaleArea.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "AREA UNIT");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatSaleArea));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "AREA UNIT");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatSaleArea));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region RowError
            rowErrors.AddRange(checkNullWbsCodes);
            rowErrors.AddRange(checkNullObjectCodes);
            rowErrors.AddRange(checkFormatObjectCodes);
            rowErrors.AddRange(checkFormatSaleArea);
            #endregion

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();

            var towers = await (from t in DB.Towers where t.ProjectID == projectID select t).ToListAsync();
            var floors = await (from f in DB.Floors where f.ProjectID == projectID select f).ToListAsync();
            var unitStatuses = await (from us in DB.MasterCenters where us.MasterCenterGroupKey == "UnitStatus" select us).ToListAsync();
            row = 2;
            foreach (var item in unitExcels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    //invalid project
                    if (item.ProjectSapCode != project.SapCode)
                    {
                        continue;
                    }

                    bool isAddNew = false;
                    if (!string.IsNullOrEmpty(item.SAPWBSNo))
                    {
                        var unit = units.Find(o => o.ProjectID == projectID && o.SAPWBSNo == item.SAPWBSNo);
                        if (unit == null)
                        {
                            unit = new Unit()
                            {
                                ProjectID = projectID
                            };
                            unit.UnitStatusMasterCenterID = unitStatuses.Where(o => o.Key == UnitStatusKeys.Available).Select(o => o.ID).First();
                            isAddNew = true;
                        }
                        else
                        {
                            //if not not available then do nothing
                            if (UnitStatusKeys.IsSold(unit.UnitStatus?.Key))
                            {
                                continue;
                            }
                        }
                        item.ToModel(ref unit);

                        if (project.ProductType.Key == ProductTypeKeys.HighRise && unit.SAPWBSNo.Length > 15)
                        {
                            var floorNames = new List<string>();
                            var towerCode = item.SAPWBSNo.Substring(12, 1);
                            var tower = towers.Find(o => o.TowerCode == towerCode && o.ProjectID == projectID);
                            if (tower == null)
                            {
                                tower = addingTowers.Find(o => o.TowerCode == towerCode && o.ProjectID == projectID);
                                if (tower == null)
                                {
                                    tower = new Tower() { TowerCode = towerCode, ProjectID = projectID };
                                    addingTowers.Add(tower);
                                }
                            }
                            unit.TowerID = tower.ID;
                            var floorName = item.SAPWBSNo.Substring(13, 2);
                            int floorInt;
                            if (int.TryParse(floorName, out floorInt))
                            {
                                //floorName = floorInt.ToString();
                                for (int i = 1; i <= floorInt; i++)
                                {
                                    floorNames.Add(i.ToString("00"));
                                }
                            }
                            foreach (var name in floorNames)
                            {
                                var floor = floors.Find(o => o.NameEN == name && o.TowerID == tower.ID);
                                if (floor == null)
                                {
                                    floor = addingFloors.Find(o => o.NameEN == name && o.TowerID == tower.ID);
                                    if (floor == null)
                                    {
                                        floor = new Floor() { NameEN = name, NameTH = name, TowerID = tower.ID, ProjectID = projectID };
                                        addingFloors.Add(floor);
                                    }
                                }
                            }
                            var floorID = addingFloors.Find(o => o.NameEN == floorName && o.TowerID == tower.ID)?.ID == Guid.Empty ?
                            floors.Find(o => o.NameEN == floorName && o.TowerID == tower.ID)?.ID
                            : addingFloors.Find(o => o.NameEN == floorName && o.TowerID == tower.ID)?.ID;
                            unit.FloorID = floorID;
                        }

                        if (isAddNew)
                        {
                            addingUnits.Add(unit);
                        }
                        else
                        {
                            updatingUnits.Add(unit);
                        }
                    }
                }
                row++;
            }

            foreach (var dbItem in units)
            {
                var existingInput = unitExcels.Find(o => o.SAPWBSNo == dbItem.SAPWBSNo);
                if (existingInput == null)
                {
                    deletingUnits.Add(dbItem);
                    dbItem.IsDeleted = true;
                }
            }

            DB.AddRange(addingTowers);
            DB.AddRange(addingFloors);
            DB.AddRange(addingUnits);
            DB.UpdateRange(updatingUnits);
            DB.UpdateRange(deletingUnits);
            await DB.SaveChangesAsync();

            var unitDataStatusMasterCenterID = await UnitDataStatus(projectID);
            project.UnitDataStatusMasterCenterID = unitDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            result.Success = addingUnits.Count() + updatingUnits.Count();
            result.Error = error;
            result.Delete = deletingUnits.Count();
            //result.CreateTowerCount = addingTowers.Count;
            //result.CreateFloorCount = addingFloors.Count;
            //result.CreateUnitCount = addingUnits.Count;
            //result.UpdateUnitCount = updatingUnits.Count;
            //result.DeleteUnitCount = deletingUnits.Count;
            //result.CreateUnitSapWbsNos = addingUnits.Select(o => o.SAPWBSNo).ToList();
            //result.UpdateUnitSapWbsNos = updatingUnits.Select(o => o.SAPWBSNo).ToList();
            //result.DeleteUnitSapWbsNos = deletingUnits.Select(o => o.SAPWBSNo).ToList();
            //result.CreateTowerCodes = addingTowers.Select(o => o.TowerCode).ToList();
            //result.CreateFloorNames = addingFloors.Select(o => o.NameEN).ToList();
            return result;
        }
        public async Task<UnitInitialExcelDTO> ImportUnitInitialAsync(Guid projectID, FileDTO input, Guid? UserID = null)
        {

            #region Validate File Template
            var dt = await this.validateFileTemplate(input);
            #endregion


            var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
            var result = new UnitInitialExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>(), Messages = new List<string>() };


            if (input.IsTemp)
            {

                string Name = "InitialUnits.xlsx";
                string generalunitsName = $"import-project/{projectNo}/initialunits/{Name}";
                await FileHelper.MoveTempFileAsync(input.Name, generalunitsName);
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
            imp.Import_Type = "initialunits";
            imp.Import_Status = "I";

            await DB.ImptMstProjTrans.AddAsync(imp);
            await DB.SaveChangesAsync();

            return result;
        }

        public async Task<DataTable> ConvertExcelInitialToDataTable(FileDTO input)
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

        public async Task<UnitGeneralExcelDTO> ImportUnitGeneralAsyncTemp(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();
            // Decimal 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();
            // 1 2 3 4 5
            var err0067 = await DB.ErrorMessages.Where(o => o.Key == "ERR0067").FirstAsync();
            // Direction 
            var err0066 = await DB.ErrorMessages.Where(o => o.Key == "ERR0066").FirstAsync();

            var dt = await ConvertExcelGeneralToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 19)
            {
                throw new Exception("Invalid File Format");
            }
            //Read Excel Model
            var unitExcelModels = new List<UnitExcelModel>();

            var models = await DB.Models.Where(o => o.ProjectID == projectID).ToListAsync();
            var assetTypes = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "AssetType").ToListAsync();
            var unitDirections = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitDirection").ToListAsync();
            var towers = await DB.Towers.Where(o => o.ProjectID == projectID).ToListAsync();
            var floors = await DB.Floors.Where(o => o.ProjectID == projectID).ToListAsync();
            var units = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
            var project = await DB.Projects.Where(o => o.ID == projectID).Include(o => o.ProductType)
                                                                         .Include(o => o.Brand)
                                                                         .ThenInclude(o => o.UnitNumberFormat)
                                                                         .FirstAsync();
            #region Valdiate
            var row = 4;
            var error = 0;
            var formatUnitdirections = new List<string> { "n", "e", "w", "s", "ne", "nw", "se", "sw" };
            var formatAssetTypes = new List<string> { "1", "2", "3", "4", "5" };
            var checkNullWBSCodes = new List<string>();
            var checkNullWBSObjects = new List<string>();
            var checkNullModelNames = new List<string>();
            var checkNullAssetTypes = new List<string>();
            var checkNullSaleAreas = new List<string>();

            var modelNotFounds = new List<string>();

            //var checkFormatHighLocations = new List<string>();
            var checkFormatUnitDirections = new List<string>();
            var checkFormatAssertTypes = new List<string>();
            var checkFormatTitleDeedAreas = new List<string>();
            var checkFormatNumberOfPrivileges = new List<string>();
            var checkFormatNumberOfParkingFixs = new List<string>();
            var checkFormatNumberOfParkingUnFixs = new List<string>();


            var rowErrors = new List<string>();
            #endregion

            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = UnitExcelModel.CreateFromDataRow(r);
                unitExcelModels.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.WBSNo))
                {
                    checkNullWBSCodes.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.WBSObjectCode))
                {
                    checkNullWBSObjects.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.ModelName))
                {
                    checkNullModelNames.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    var model = models.Find(o => o.NameTH == excelModel.ModelName);
                    if (model == null)
                    {
                        modelNotFounds.Add((row).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(excelModel.AssetType))
                {
                    checkNullAssetTypes.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(r[UnitExcelModel._saleAreaIndex].ToString()))
                {
                    checkNullSaleAreas.Add((row).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(excelModel.UnitDirection))
                {
                    if (!formatUnitdirections.Contains(excelModel.UnitDirection.ToLower()))
                    {
                        checkFormatUnitDirections.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.AssetType))
                {
                    if (!formatAssetTypes.Contains(excelModel.AssetType))
                    {
                        checkFormatAssertTypes.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[UnitExcelModel._titledeedArea].ToString()))
                {
                    if (!r[UnitExcelModel._titledeedArea].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatTitleDeedAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                if (project.ProductType.Key == ProductTypeKeys.LowRise)
                {
                    if (!string.IsNullOrEmpty(r[UnitExcelModel._numberOfPrivilegeIndex].ToString()))
                    {
                        if (!r[UnitExcelModel._numberOfPrivilegeIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                        {
                            checkFormatNumberOfPrivileges.Add((row).ToString());
                            isError = true;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(r[UnitExcelModel._numberOfParkingFixIndex].ToString()))
                {
                    if (!r[UnitExcelModel._numberOfParkingFixIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatNumberOfParkingFixs.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[UnitExcelModel._numberOfParkingUnFixIndex].ToString()))
                {
                    if (!r[UnitExcelModel._numberOfParkingUnFixIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatNumberOfParkingUnFixs.Add((row).ToString());
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
            UnitGeneralExcelDTO result = new UnitGeneralExcelDTO() { Error = 0, Success = 0, ErrorMessages = new List<string>() };

            ValidateException ex = new ValidateException();
            if (project.Brand?.UnitNumberFormatMasterCenterID == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0054").FirstAsync();
                string value = project.Brand?.Name;
                var msg = errMsg.Message.Replace("[value]", value);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            ex = new ValidateException();
            if (unitExcelModels.Any(o => o.ProjectNo != project.ProjectNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ CRM");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            ex = new ValidateException();
            if (unitExcelModels.Any(o => o.ProjectCode != project.SapCode))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "ProjectCode");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }




            #region Add Result Validation

            #region Required

            if (checkNullWBSCodes.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBSCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBSCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullWBSObjects.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ObjectCode");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSObjects));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ObjectCode");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSObjects));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullModelNames.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ชื่อแบบบ้าน");
                    msg = msg.Replace("[row]", String.Join(",", checkNullModelNames));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ชื่อแบบบ้าน");
                    msg = msg.Replace("[row]", String.Join(",", checkNullModelNames));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullAssetTypes.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "AssertType (1,2,3,4,5)");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAssetTypes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "AssertType (1,2,3,4,5)");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAssetTypes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullSaleAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "พื้นที่ขาย");
                    msg = msg.Replace("[row]", String.Join(",", checkNullSaleAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "พื้นที่ขาย");
                    msg = msg.Replace("[row]", String.Join(",", checkNullSaleAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region Direction
            if (checkFormatUnitDirections.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0066.Message.Replace("[column]", "ทิศ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatUnitDirections));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0066.Message.Replace("[column]", "ทิศ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatUnitDirections));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region Format 1,2,3,4,5
            if (checkFormatAssertTypes.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0067.Message.Replace("[column]", "AssertType (1,2,3,4,5)");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatAssertTypes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0067.Message.Replace("[column]", "AssertType (1,2,3,4,5)");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatAssertTypes));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region Decimal 2 Digit
            if (checkFormatTitleDeedAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "พื้นที่โฉนด");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatTitleDeedAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "พื้นที่โฉนด");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatTitleDeedAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatNumberOfPrivileges.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ราคาบุริมสิทธิ์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfPrivileges));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ราคาบุริมสิทธิ์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfPrivileges));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatNumberOfParkingFixs.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "จำนวนที่จอดรถ Fix");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfParkingFixs));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "จำนวนที่จอดรถ Fix");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfParkingFixs));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatNumberOfParkingUnFixs.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "จำนวนที่จอดรถไม่ Fix");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfParkingUnFixs));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "จำนวนที่จอดรถไม่ Fix");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfParkingUnFixs));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region NotFound
            if (modelNotFounds.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "ชื่อแบบบ้าน");
                    msg = msg.Replace("[row]", String.Join(",", modelNotFounds));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "ชื่อแบบบ้าน");
                    msg = msg.Replace("[row]", String.Join(",", modelNotFounds));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region RowError
            rowErrors.AddRange(checkNullWBSCodes);
            rowErrors.AddRange(checkNullWBSObjects);
            rowErrors.AddRange(checkNullModelNames);
            rowErrors.AddRange(checkNullAssetTypes);
            rowErrors.AddRange(checkNullSaleAreas);
            rowErrors.AddRange(modelNotFounds);
            rowErrors.AddRange(checkFormatUnitDirections);
            rowErrors.AddRange(checkFormatAssertTypes);
            rowErrors.AddRange(checkFormatTitleDeedAreas);
            rowErrors.AddRange(checkFormatNumberOfPrivileges);
            rowErrors.AddRange(checkFormatNumberOfParkingFixs);
            rowErrors.AddRange(checkFormatNumberOfParkingUnFixs);

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();

            #endregion

            #endregion
            row = 4;
            List<Unit> unitsUpdate = new List<Unit>();
            foreach (var item in unitExcelModels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var existingUnit = units.Find(o => o.SAPWBSNo == item.WBSNo);
                    if (existingUnit != null)
                    {
                        item.ToModel(ref existingUnit, project.ProductType.Key == ProductTypeKeys.LowRise ? true : false);
                        existingUnit.ProjectID = projectID;

                        //calculate UnitNo
                        var productTypeKey = project.ProductType?.Key;
                        var unitNumberFormat = project.Brand?.UnitNumberFormat?.Key;
                        var splitWbsNo = item.WBSNo.Split("-").ToList();


                        if (productTypeKey == ProductTypeKeys.LowRise && unitNumberFormat == "1")
                        {
                            existingUnit.UnitNo = splitWbsNo[3];
                        }
                        if (productTypeKey == ProductTypeKeys.LowRise && unitNumberFormat == "2")
                        {
                            existingUnit.UnitNo = splitWbsNo[3] + "-" + Convert.ToInt32(splitWbsNo[5]);
                        }

                        if (productTypeKey == ProductTypeKeys.HighRise && item.ModelName.ToLower() != "shop")
                        {
                            existingUnit.UnitNo = item.TowerCode + item.FloorName + item.ModelName + item.HighRiseLocation;
                        }
                        if (productTypeKey == ProductTypeKeys.HighRise && item.ModelName.ToLower() == "shop")
                        {
                            existingUnit.UnitNo = item.ModelName + item.HighRiseLocation + item.TowerCode;
                        }
                        //}

                        var assetTypeMasterCenter = assetTypes.Find(o => o.MasterCenterGroupKey == "AssetType" && o.Key == item.AssetType);
                        existingUnit.AssetTypeMasterCenterID = assetTypeMasterCenter?.ID;

                        var unitDirectionMasterCenter = unitDirections.Find(o => o.MasterCenterGroupKey == "UnitDirection" && o.Key == item.UnitDirection);
                        existingUnit.UnitDirectionMasterCenterID = unitDirectionMasterCenter?.ID;

                        existingUnit.ModelID = models.Find(o => o.NameTH == item.ModelName) == null ? (Guid?)null : models.Find(o => o.NameTH == item.ModelName)?.ID;
                        if (!string.IsNullOrEmpty(item.TowerCode))
                        {
                            var existingTower = towers.Find(o => o.ProjectID == projectID && o.TowerCode == item.TowerCode);
                            if (existingTower != null)
                            {
                                existingUnit.TowerID = existingTower.ID;
                                var existingFloor = floors.Find(o => o.TowerID == existingTower.ID && o.NameTH == item.FloorName);
                                if (existingFloor != null)
                                {
                                    existingUnit.FloorID = existingFloor.ID;
                                }
                            }
                        }
                        unitsUpdate.Add(existingUnit);
                    }
                }
                row++;
            }
            DB.UpdateRange(unitsUpdate);
            await DB.SaveChangesAsync();

            var unitDataStatusMasterCenterID = await UnitDataStatus(projectID);
            project.UnitDataStatusMasterCenterID = unitDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
            result.Success = unitsUpdate.Count();
            result.Error = error;
            return result;
        }
        public async Task<UnitGeneralExcelDTO> ImportUnitGeneralAsync(Guid projectID, FileDTO input, Guid? UserID = null)
        {
            var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
            var result = new UnitGeneralExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>(), Messages = new List<string>() };

            if (input.IsTemp)
            {

                string Name = "GeneralUnits.xlsx";
                string generalunitsName = $"import-project/{projectNo}/generalunits/{Name}";
                await FileHelper.MoveTempFileAsync(input.Name, generalunitsName);
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
            imp.Import_Type = "generalunits";
            imp.Import_Status = "I";

            await DB.ImptMstProjTrans.AddAsync(imp);
            await DB.SaveChangesAsync();

            return result;
        }

        public async Task<DataTable> ConvertExcelGeneralToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

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
                    foreach (var firstRowCell in ws.Cells[3, 1, 3, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 4 : 3;
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

        public async Task<UnitFenceAreaExcelDTO> ImportUnitFenceAreaAsync(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // Decimal Format 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();
            // Not Found 
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();

            ValidateException ex = new ValidateException();

            #region Validate ProductType
            var project = await DB.Projects.Where(o => o.ID == projectID).Include(o => o.ProductType).FirstOrDefaultAsync();
            if (project.ProductType.Key == ProductTypeKeys.HighRise)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0050").FirstAsync();
                var msg = errMsg.Message;
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            #endregion

            var result = new UnitFenceAreaExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>() };
            var dt = await ConvertExcelUnitFenceAreaToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 5)
            {
                throw new Exception("Invalid File Format");
            }

            var checkNullWBSCodes = new List<string>();
            var checkNullUnitNos = new List<string>();
            var checkNullFenceAreas = new List<string>();
            var checkNullFenceIronAreas = new List<string>();
            var checkFormatFenceAreas = new List<string>();
            var checkFormatFenceIronAreas = new List<string>();
            var unitNotFounds = new List<string>();
            var row = 2;
            var error = 0;

            var units = await DB.Units.Where(o => o.ProjectID == project.ID).ToListAsync();
            //Read Excel Model
            var unitFenceAreaExcelModels = new List<UnitFenceAreaExcelModel>();
            foreach (DataRow r in dt.Rows)
            {

                var excelModel = UnitFenceAreaExcelModel.CreateFromDataRow(r);
                unitFenceAreaExcelModels.Add(excelModel);

                var isError = false;

                #region Validate
                var unit = units.Find(o => o.UnitNo == excelModel.UnitNo && o.SAPWBSNo == excelModel.WBSNo);
                if (unit == null)
                {
                    unitNotFounds.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.WBSNo))
                {
                    checkNullWBSCodes.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(r[UnitFenceAreaExcelModel._fenceAreaIndex].ToString()))
                {
                    if (!r[UnitFenceAreaExcelModel._fenceAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatFenceAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                else
                {
                    checkNullFenceAreas.Add((row).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(r[UnitFenceAreaExcelModel._fenceIronAreaIndex].ToString()))
                {
                    if (!r[UnitFenceAreaExcelModel._fenceIronAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatFenceIronAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                else
                {
                    checkNullFenceIronAreas.Add((row).ToString());
                    isError = true;
                }
                if (isError)
                {
                    error++;
                }
                row++;
                #endregion

            }



            #region Validate ProjectNo In File
            ex = new ValidateException();
            if (unitFenceAreaExcelModels.Any(o => o.ProjectNo != project.ProjectNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            #endregion


            #region ResultValidate
            if (checkNullWBSCodes.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBS CODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBS CODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
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
            if (checkNullFenceAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "พื้นที่รั้วคอนกรีต");
                    msg = msg.Replace("[row]", String.Join(",", checkNullFenceAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "พื้นที่รั้วคอนกรีต");
                    msg = msg.Replace("[row]", String.Join(",", checkNullFenceAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullFenceIronAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "พื้นที่รั้วเหล็กดัด");
                    msg = msg.Replace("[row]", String.Join(",", checkNullFenceIronAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "พื้นที่รั้วเหล็กดัด");
                    msg = msg.Replace("[row]", String.Join(",", checkNullFenceIronAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatFenceAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "พื้นที่รั้วคอนกรีต");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatFenceAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "พื้นที่รั้วคอนกรีต");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatFenceAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatFenceIronAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "พื้นที่รั้วเหล็กดัด");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatFenceIronAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "พื้นที่รั้วเหล็กดัด");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatFenceIronAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (unitNotFounds.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", unitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "พื้นที่รั้วเหล็กดัด");
                    msg = msg.Replace("[row]", String.Join(",", unitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region RowError
            var rowErrors = new List<string>();

            rowErrors.AddRange(checkNullWBSCodes);
            rowErrors.AddRange(checkNullUnitNos);
            rowErrors.AddRange(checkNullFenceAreas);
            rowErrors.AddRange(checkNullFenceIronAreas);
            rowErrors.AddRange(checkFormatFenceAreas);
            rowErrors.AddRange(checkFormatFenceIronAreas);
            rowErrors.AddRange(unitNotFounds);

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            #endregion
            row = 2;
            List<Unit> updateList = new List<Unit>();
            //Update Data
            foreach (var item in unitFenceAreaExcelModels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var unit = units.Find(o => o.SAPWBSNo == item.WBSNo && o.UnitNo == item.UnitNo);
                    if (unit != null)
                    {
                        item.ToModel(ref unit);
                        updateList.Add(unit);
                    }
                }
                row++;
            }
            DB.UpdateRange(updateList);
            await DB.SaveChangesAsync();
            result.Success = updateList.Count();
            result.Error = error;
            return result;
        }

        private async Task<DataTable> ConvertExcelUnitFenceAreaToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            ////Stream ddd = new MemoryStream(test);

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

        public async Task<Unit> DeleteUnitMeterAsync(Guid id)
        {
            try
            {
                var model = await DB.Units.FindAsync(id);
                model.ElectricMeter = null;
                model.WaterMeter = null;
                model.ElectricMeterPrice = null;
                model.WaterMeterPrice = null;
                model.IsTransferElectricMeter = null;
                model.IsTransferWaterMeter = null;
                model.ElectricMeterTransferDate = null;
                model.WaterMeterTransferDate = null;
                model.ElectricMeterTopic = null;
                model.WaterMeterTopic = null;
                model.ElectricMeterRemark = null;
                model.WaterMeterRemark = null;

                DB.Entry(model).State = EntityState.Modified;
                await DB.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UnitMeterDTO> GetUnitMeterAsync(Guid unitID, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = await DB.Units.AsNoTracking()
                                          .Include(o => o.UnitStatus)
                                          .Include(o => o.Model)
                                          .Include(o => o.ElectrictMeterStatus)
                                          .Include(o => o.WaterMeterStatus)
                                          .Include(o => o.ElectricMeterTopic)
                                          .Include(o => o.WaterMeterTopic)
                                          .Include(o => o.ElectricMeterPrice)
                                          .Include(o => o.WaterMeterPrice)
                                          .Include(o => o.UpdatedBy)
                                          .FirstAsync(o => o.ID == unitID, cancellationToken);

                var result = UnitMeterDTO.CreateFromModel(model);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UnitMeterDTO> UpdateUnitMeterAsync(Guid unitID, UnitMeterDTO input)
        {
            var model = await DB.Units.FirstAsync(x => x.ID == unitID);
            var meterStatuses = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterStatus).ToListAsync();
            input.ToModel(ref model);

            if (model.IsTransferElectricMeter == true)
            {
                model.ElectricMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "2")?.ID;
            }
            else if (model.IsTransferElectricMeter != true && !string.IsNullOrEmpty(model.ElectricMeter))
            {
                model.ElectricMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "1")?.ID;
            }
            else if (string.IsNullOrEmpty(model.ElectricMeter))
            {
                model.ElectricMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "0")?.ID;
            }

            if (model.IsTransferWaterMeter == true)
            {
                model.WaterMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "2")?.ID;
            }
            else if (model.IsTransferWaterMeter != true && !string.IsNullOrEmpty(model.WaterMeter))
            {
                model.WaterMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "1")?.ID;
            }
            else if (string.IsNullOrEmpty(model.WaterMeter))
            {
                model.WaterMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "0")?.ID;
            }

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await GetUnitMeterAsync(unitID);
            return result;
        }

        public async Task<UnitMeterPaging> GetUnitMeterListAsync(UnitMeterFilter filter, PageParam pageParam, UnitMeterListSortByParam sortByParam)
        {
            IQueryable<UnitMeterListQueryResult> query = from project in DB.Projects.AsNoTracking()
                                                         join unit in DB.Units.AsNoTracking().DefaultIfEmpty()
                                                         .Include(o => o.UnitStatus)
                                                         .Include(o => o.Model)
                                                         .Include(o => o.ElectrictMeterStatus)
                                                         .Include(o => o.WaterMeterStatus)
                                                         .Include(o => o.WaterMeterTopic)
                                                         .Include(o => o.ElectricMeterTopic)
                                                         on project.ID equals unit.ProjectID
                                                         select new UnitMeterListQueryResult
                                                         {
                                                             Project = project,
                                                             Unit = unit,
                                                             UpdatedBy = unit.UpdatedBy
                                                         };
            #region Filter
            if (!string.IsNullOrEmpty(filter.ProjectIDs))
            {
                var projectIds = filter.ProjectIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                query = query.Where(x => projectIds.Contains(x.Project.ID));
            }
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo));
            }
            if (filter.ModelID != null && filter.ModelID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.ModelID == filter.ModelID);
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                      && x.MasterCenterGroupKey == "UnitStatus")
                                                                     .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
            }
            if (filter.TransferOwnerShipDateFrom != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom);
            }
            if (filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo);
            }
            if (filter.TransferOwnerShipDateFrom != null && filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom
                                    && x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo);
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeter))
            {
                query = query.Where(x => x.Unit.ElectricMeter.Contains(filter.ElectricMeter));
            }
            if (!string.IsNullOrEmpty(filter.WaterMeter))
            {
                query = query.Where(x => x.Unit.WaterMeter.Contains(filter.WaterMeter));
            }
            if (filter.CompletedDocumentDateFrom != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom);
            }
            if (filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo);
            }
            if (filter.CompletedDocumentDateFrom != null && filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom
                                    && x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo);
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeterStatusKey))
            {
                var electricMeterStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ElectricMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == electricMeterStatusMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.WaterMeterStatusKey))
            {
                var waterMeterStatusKeyMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.WaterMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == waterMeterStatusKeyMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Unit.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Unit.Updated <= filter.UpdatedTo);
            }

            #endregion


            ProjectUnitMeterListDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<UnitMeterListQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => new ProjectUnitMeterListDTO
            {
                Project = ProjectDTO.CreateFromModel(o.Project),
                UnitMeter = UnitMeterListDTO.CreateFromModel(o.Unit)
            }).ToList();

            return new UnitMeterPaging()
            {
                PageOutput = pageOutput,
                ProjectUnitMeterLists = results
            };

        }

        public async Task<FileDTO> ExportExcelUnitInitialAsync(Guid projectID)
        {
            ExportExcel result = new ExportExcel();

            var project = await (from p in DB.Projects.Include(o => o.Company)
                                 where p.ID == projectID
                                 select p).FirstOrDefaultAsync();

            var unitsChk = await (from u in DB.Units
                               .Include(o => o.UnitStatus)
                               .Include(o => o.AssetType)
                               .Include(o => o.Model.TypeOfRealEstate)
                                  where u.ProjectID == projectID
                                  select u).OrderBy(c => c.SAPWBSNo).ToListAsync();

            var units = new List<Unit>();

            if (unitsChk.Where(o => o.AssetTypeMasterCenterID != null).ToList().Count == unitsChk.Count)
            {
                units = unitsChk.Where(o => o.UnitStatus.Key.Equals("0") && o.UnitStatus.Order == 1 && o.UnitStatus.MasterCenterGroupKey.Equals("UnitStatus") && o.AssetType.Key != "6" && o.AssetType.Key != "7").ToList();
            }
            else
            {
                units = unitsChk.Where(o => o.UnitStatus.Key.Equals("0") && o.UnitStatus.Order == 1 && o.UnitStatus.MasterCenterGroupKey.Equals("UnitStatus")).ToList();
            }


            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_InitialFromSAP.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int projectSapCodeIndex = UnitInitialExcelModel._projectSapCodeIndex + 1;
                int wbsNoIndex = UnitInitialExcelModel._wbsNoIndex + 1;
                int wbsObjectCodeIndex = UnitInitialExcelModel._wbsObjectCodeIndex + 1;
                int companyIndex = UnitInitialExcelModel._companyIndex + 1;
                int boqStyleIndex = UnitInitialExcelModel._boqStyleIndex + 1;
                int typeOfRealEstateIndex = UnitInitialExcelModel._typeOfRealEstateIndex + 1;
                int wbsStatusIndex = UnitInitialExcelModel._wbsStatusIndex + 1;
                int saleAreaIndex = UnitInitialExcelModel._saleAreaIndex + 1;

                for (int c = 2; c < units.Count + 2; c++)
                {
                    var unit = units[c - 2];
                    worksheet.Cells[c, projectSapCodeIndex].Value = project.SapCode;
                    worksheet.Cells[c, wbsNoIndex].Value = unit.SAPWBSNo;
                    worksheet.Cells[c, wbsObjectCodeIndex].Value = unit.SAPWBSObject;
                    worksheet.Cells[c, companyIndex].Value = project.Company?.SAPCompanyID;
                    worksheet.Cells[c, boqStyleIndex].Value = string.Empty;
                    worksheet.Cells[c, typeOfRealEstateIndex].Value = unit.Model?.TypeOfRealEstate?.Name;
                    worksheet.Cells[c, wbsStatusIndex].Value = unit.SAPWBSStatus;
                    worksheet.Cells[c, saleAreaIndex].Value = unit.SaleArea;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ProjectNo + "_InitialFromSAP.xlsx";
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

        public async Task<FileDTO> ExportExcelUnitGeneralAsync(Guid projectID)
        {
            ExportExcel result = new ExportExcel();
            IQueryable<UnitQueryResult> query = DB.Units.Where(x => x.ProjectID == projectID).OrderBy(o => o.UnitNo)
                                                             .Select(x => new UnitQueryResult
                                                             {
                                                                 Model = x.Model,
                                                                 Floor = x.Floor,
                                                                 UnitDirection = x.UnitDirection,
                                                                 Tower = x.Tower,
                                                                 Unit = x,
                                                                 UnitStatus = x.UnitStatus,
                                                                 UnitType = x.UnitType,
                                                                 AssetType = x.AssetType,
                                                                 TitledeedDetail = DB.TitledeedDetails
                                                                    .Where(c => c.UnitID == x.ID && c.ProjectID == projectID)
                                                                    .GroupBy(f => f.UnitID)
                                                                    .Select(g => g.OrderByDescending(f => f.Created).FirstOrDefault())
                                                                    .FirstOrDefault()
                                                             });
            //var data = query.ToList();
            var data = await query.Where(o => o.UnitStatus.Key.Equals("0")
             && o.UnitStatus.Order == 1
             && o.UnitStatus.MasterCenterGroupKey.Equals("UnitStatus")
             && o.AssetType.Key != "6"
              && o.AssetType.Key != "7")
              .OrderBy(x => x.Unit.SAPWBSNo)
              .ToListAsync();


            var project = await DB.Projects.FirstOrDefaultAsync(x => x.ID == projectID);
            if (project is null) return null;

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_Units.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = UnitExcelModel._projectNoIndex + 1;
                int _projectCodeIndex = UnitExcelModel._projectCodeIndex + 1;
                int _wbsCodeIndex = UnitExcelModel._wbsCodeIndex + 1;
                int _wbsobjectCodeIndex = UnitExcelModel._wbsobjectCodeIndex + 1;
                int _unitNoIndex = UnitExcelModel._unitNoIndex + 1;
                int _modelNameIndex = UnitExcelModel._modelNameIndex + 1;
                int _unitHighRiseLocationIndex = UnitExcelModel._unitHighRiseLocationIndex + 1;
                int _towerIndex = UnitExcelModel._towerIndex + 1;
                int _floorIndex = UnitExcelModel._floorIndex + 1;
                int _unitDirectionIndex = UnitExcelModel._unitDirectionIndex + 1;
                int _floorplanNameIndex = UnitExcelModel._floorplanNameIndex + 1;
                int _roomplanNameIndex = UnitExcelModel._roomplanNameIndex + 1;
                int _assetTypeIndex = UnitExcelModel._assetTypeIndex + 1;
                int _saleAreaIndex = UnitExcelModel._saleAreaIndex + 1;
                int _titledeedArea = UnitExcelModel._titledeedArea + 1;
                int _numberOfPrivilegeIndex = UnitExcelModel._numberOfPrivilegeIndex + 1;
                int _numberOfParkingFixIndex = UnitExcelModel._numberOfParkingFixIndex + 1;
                int _numberOfParkingUnFixIndex = UnitExcelModel._numberOfParkingUnFixIndex + 1;
                int _quotaForeignUnit = UnitExcelModel._quotaForeignUnit + 1;


                for (int c = 4; c < data.Count + 4; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = project.ProjectNo;
                    worksheet.Cells[c, _projectCodeIndex].Value = project.SapCode;
                    worksheet.Cells[c, _wbsCodeIndex].Value = data[c - 4].Unit?.SAPWBSNo;
                    worksheet.Cells[c, _wbsobjectCodeIndex].Value = data[c - 4].Unit?.SAPWBSObject;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 4].Unit.UnitNo;
                    worksheet.Cells[c, _modelNameIndex].Value = data[c - 4].Model?.NameTH;
                    worksheet.Cells[c, _unitHighRiseLocationIndex].Value = data[c - 4].Unit.Position;
                    worksheet.Cells[c, _towerIndex].Value = data[c - 4].Tower?.TowerCode;
                    worksheet.Cells[c, _floorIndex].Value = data[c - 4].Floor?.NameEN;
                    worksheet.Cells[c, _unitDirectionIndex].Value = data[c - 4].UnitDirection?.Key;
                    worksheet.Cells[c, _floorplanNameIndex].Value = data[c - 4].Unit.FloorPlanFileName;
                    worksheet.Cells[c, _roomplanNameIndex].Value = data[c - 4].Unit.RoomPlanFileName;
                    worksheet.Cells[c, _assetTypeIndex].Value = data[c - 4].AssetType?.Key;
                    worksheet.Cells[c, _saleAreaIndex].Value = data[c - 4].Unit.SaleArea;
                    worksheet.Cells[c, _titledeedArea].Value = data[c - 4].TitledeedDetail?.TitledeedArea;
                    worksheet.Cells[c, _numberOfPrivilegeIndex].Value = data[c - 4].Unit.UnitLoanAmount;
                    worksheet.Cells[c, _numberOfParkingFixIndex].Value = data[c - 4].Unit.NumberOfParkingFix;
                    worksheet.Cells[c, _numberOfParkingUnFixIndex].Value = data[c - 4].Unit.NumberOfParkingUnFix;
                    worksheet.Cells[c, _quotaForeignUnit].Value = data[c - 4].Unit.IsForeignUnit ? "1" : "0";
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ProjectNo + "_Units.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            //string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            //string contentType = result.FileType;
            //string filePath = $"{projectID}/export-excels/";
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

        public async Task<FileDTO> ExportExcelUnitFenceAreaAsync(Guid projectID)
        {
            ExportExcel result = new ExportExcel();
            IQueryable<UnitQueryResult> query = from unit in DB.Units
                                                orderby unit.UnitNo
                                                join titledeed in DB.TitledeedDetails
                                                on unit.ID equals titledeed.UnitID into d
                                                where unit.ProjectID == projectID
                                                from e in d.DefaultIfEmpty()
                                                select new UnitQueryResult
                                                {
                                                    TitledeedDetail = e,
                                                    Unit = unit,
                                                    AssetType = DB.MasterCenters.Where(o => o.ID == unit.AssetTypeMasterCenterID).FirstOrDefault()

                                                };
            var data = query.Where(o => o.AssetType.Key != "6" && o.AssetType.Key != "7").ToList();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_Address.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = UnitFenceAreaExcelModel._projectNoIndex + 1;
                int _wbsNoIndex = UnitFenceAreaExcelModel._wbsNoIndex + 1;
                int _unitNoIndex = UnitFenceAreaExcelModel._unitNoIndex + 1;
                int _fenceAreaIndex = UnitFenceAreaExcelModel._fenceAreaIndex + 1;
                int _fenceIronAreaIndex = UnitFenceAreaExcelModel._fenceIronAreaIndex + 1;


                var project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
                for (int c = 2; c < data.Count() + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = project.ProjectNo;
                    worksheet.Cells[c, _wbsNoIndex].Value = data[c - 2].Unit?.SAPWBSNo;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _fenceAreaIndex].Value = data[c - 2].Unit?.FenceArea;
                    worksheet.Cells[c, _fenceIronAreaIndex].Value = data[c - 2].Unit?.FenceIronArea;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ProjectNo + "_Address.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            //string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            //string contentType = result.FileType;
            //string filePath = $"{projectID}/export-excels/";
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
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137937/preview
        /// Sample File: http://192.168.2.29:9001/xunit-tests/UnitMeter.xlsx
        /// </summary>
        /// <returns>The unit meter excel async.</returns>
        /// <param name="input">Input.</param>
        public async Task<UnitMeterExcelDTO> ImportUnitMeterExcelAsync(FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();

            // String with 10 character
            var err0074 = await DB.ErrorMessages.Where(o => o.Key == "ERR0074").FirstAsync();

            UnitMeterExcelDTO result = new UnitMeterExcelDTO();
            var dt = await ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 5)
            {
                throw new Exception("Invalid File Format");
            }

            var row = 1;
            var error = 0;

            var checkNullUnitNos = new List<string>();
            var checkNullAddressNumbers = new List<string>();
            var checkFormatElectricMeterNumbers = new List<string>();
            var checkFormatWaterMeterNumbers = new List<string>();

            //Read Excel Model
            var unitMeterExcelModels = new List<UnitMeterExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = UnitMeterExcelModel.CreateFromDataRow(r);
                unitMeterExcelModels.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row + 1).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.HouseNo))
                {
                    checkNullAddressNumbers.Add((row + 1).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(excelModel.WaterMeter))
                {
                    if (!excelModel.WaterMeter.IsOnlyNumberWithMaxLength(10))
                    {
                        checkFormatElectricMeterNumbers.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.ElectricMeter))
                {
                    if (!excelModel.ElectricMeter.IsOnlyNumberWithMaxLength(10))
                    {
                        checkFormatWaterMeterNumbers.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                #endregion

                if (isError)
                {
                    error++;
                }
                row++;
            }

            #region Add Result Validate


            if (checkNullUnitNos.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "UnitNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "UnitNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkNullAddressNumbers.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "AddressNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAddressNumbers));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "AddressNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAddressNumbers));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormatElectricMeterNumbers.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0074.Message.Replace("[column]", "ElectricMeterNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatElectricMeterNumbers));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0074.Message.Replace("[column]", "ElectricMeterNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatElectricMeterNumbers));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormatWaterMeterNumbers.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WaterMeterNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatWaterMeterNumbers));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WaterMeterNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatWaterMeterNumbers));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion


            var projectNos = unitMeterExcelModels.Select(o => o.ProjectNo).Distinct().ToList();
            var projects = await (from p in DB.Projects
                                  where projectNos.Contains(p.ProjectNo)
                                  select p).ToListAsync();
            var projectIDs = projects.Select(o => o.ID).Distinct().ToList();
            var units = await (from u in DB.Units
                               where projectIDs.Contains(u.ProjectID ?? Guid.Empty)
                               select u).ToListAsync();
            var meterStatuses = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterStatus).ToListAsync();

            List<Unit> updateList = new List<Unit>();
            //Update Data
            foreach (var item in unitMeterExcelModels)
            {
                var project = projects.Find(o => o.ProjectNo == item.ProjectNo);
                if (project != null)
                {
                    var unit = units.Find(x => x.ProjectID == project.ID && x.UnitNo == item.UnitNo);
                    if (unit != null)
                    {
                        item.ToModel(ref unit);
                        if (string.IsNullOrEmpty(unit.WaterMeter))
                        {
                            unit.WaterMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "0")?.ID;
                        }
                        else
                        {
                            unit.WaterMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "1")?.ID;
                        }
                        if (string.IsNullOrEmpty(unit.ElectricMeter))
                        {
                            unit.ElectricMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "0")?.ID;
                        }
                        else
                        {
                            unit.ElectricMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "1")?.ID;
                        }
                        updateList.Add(unit);
                    }
                }
            }
            DB.Units.UpdateRange(updateList);
            await DB.SaveChangesAsync();
            result.Success = updateList.Count();
            result.Error = error;
            return result;
        }

        private async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

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
                        tbl.Columns.Add(hasHeader ? $"Column {firstRowCell.Start.Column} {firstRowCell.Text}" : string.Format("Column {0}", firstRowCell.Start.Column));
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

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137937/preview
        /// Sample File: http://192.168.2.29:9001/xunit-tests/UnitMeter.xls
        /// </summary>
        /// <returns>The unit meter excel async.</returns>
        /// <param name="input">Input.</param>
        public async Task<FileDTO> ExportUnitMeterExcelAsync(UnitMeterFilter filter, UnitMeterListSortByParam sortByParam)
        {
            ExportExcel result = new ExportExcel();
            List<Database.Models.PRJ.Project> projects = new List<Database.Models.PRJ.Project>();
            if (!string.IsNullOrEmpty(filter.ProjectIDs))
            {
                var projectIds = filter.ProjectIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                projects = await DB.Projects.Where(o => projectIds.Contains(o.ID)).ToListAsync();
            }
            else
            {
                projects = await DB.Projects.ToListAsync();
            }
            var projectsId = projects.Select(o => o.ID).ToList();
            var units = await DB.Units.Where(o => projectsId.Contains(o.ProjectID.Value))
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.Model)
                                      .Include(o => o.ElectrictMeterStatus)
                                      .Include(o => o.WaterMeterStatus)
                                      .Include(o => o.WaterMeterTopic)
                                      .Include(o => o.ElectricMeterTopic)
                                      .ToListAsync();

            var query = (from project in projects
                         join unit in units
                         on project.ID equals unit.ProjectID
                         into temp
                         from tempdata in temp.DefaultIfEmpty()
                         select new UnitMeterListQueryResult
                         {
                             Project = project,
                             Unit = tempdata,
                         }).ToList();

            #region Filter
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo)).ToList();
            }
            if (filter.ModelID != null && filter.ModelID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.ModelID == filter.ModelID).ToList();
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                      && x.MasterCenterGroupKey == "UnitStatus")
                                                                     .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID).ToList();
            }
            if (filter.TransferOwnerShipDateFrom != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom).ToList();
            }
            if (filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo).ToList();
            }
            if (filter.TransferOwnerShipDateFrom != null && filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom
                                    && x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo).ToList();
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeter))
            {
                query = query.Where(x => x.Unit.ElectricMeter.Contains(filter.ElectricMeter)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.WaterMeter))
            {
                query = query.Where(x => x.Unit.WaterMeter.Contains(filter.WaterMeter)).ToList();
            }
            if (filter.CompletedDocumentDateFrom != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom).ToList();
            }
            if (filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo).ToList();
            }
            if (filter.CompletedDocumentDateFrom != null && filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom
                                    && x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo).ToList();
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeterStatusKey))
            {
                var electricMeterStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ElectricMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == electricMeterStatusMasterCenterID).ToList();
            }
            if (!string.IsNullOrEmpty(filter.WaterMeterStatusKey))
            {
                var waterMeterStatusKeyMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.WaterMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == waterMeterStatusKeyMasterCenterID).ToList();
            }

            #endregion

            ProjectUnitMeterListDTO.SortBy(sortByParam, ref query);

            var data = query;

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "UnitMeter.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = UnitMeterExcelModel._projectNoIndex + 1;
                int _unitNoIndex = UnitMeterExcelModel._unitNoIndex + 1;
                int _addressNoIndex = UnitMeterExcelModel._addressNoIndex + 1;
                int _electricMeterNoIndex = UnitMeterExcelModel._electricMeterNoIndex + 1;
                int _waterMeterNoIndex = UnitMeterExcelModel._waterMeterNoIndex + 1;


                for (int c = 2; c < data.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = data[c - 2].Project?.ProjectNo;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _addressNoIndex].Value = data[c - 2].Unit?.HouseNo;
                    worksheet.Cells[c, _electricMeterNoIndex].Value = data[c - 2].Unit?.ElectricMeter;
                    worksheet.Cells[c, _waterMeterNoIndex].Value = data[c - 2].Unit?.WaterMeter;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = "UnitMeter_" + (DateTime.Today).ToString("yyyy-MM-dd") + ".xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"unit-meter/export-excels/";

            var uploadResult = await FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = result.FileName,
                Url = uploadResult.Url
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137937/preview
        /// Sample File: http://192.168.2.29:9001/xunit-tests/UnitMeterStatus.xls
        /// </summary>
        /// <returns>The unit meter status excel async.</returns>
        public async Task<UnitMeterExcelDTO> ImportUnitMeterStatusExcelAsync(FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();

            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();

            // 1 2 null
            var err0075 = await DB.ErrorMessages.Where(o => o.Key == "ERR0075").FirstAsync();

            //Format Date
            var err0071 = await DB.ErrorMessages.Where(o => o.Key == "ERR0071").FirstAsync();

            // 1,2,3,4
            var err0070 = await DB.ErrorMessages.Where(o => o.Key == "ERR0070").FirstAsync();


            var result = new UnitMeterExcelDTO();
            var dt = await ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 11)
            {
                throw new Exception("Invalid File Format");
            }

            var row = 1;
            var error = 0;

            var topics = new List<string> { "1", "2", "3", "4" };

            var checkNullUnitNos = new List<string>();
            var checkNullAddressNumbers = new List<string>();
            var checkIsTransferElectricMeters = new List<string>();
            var checkIsTransferWaterMeters = new List<string>();
            var checkElectricMeterTopics = new List<string>();
            var checkElectricMeterDates = new List<string>();
            var checkWaterMeterTopics = new List<string>();
            var checkWaterMeterDates = new List<string>();

            //Read Excel Model
            var unitMeterStatusExcelModels = new List<UnitMeterStatusExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = UnitMeterStatusExcelModel.CreateFromDataRow(r);
                unitMeterStatusExcelModels.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row + 1).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.HouseNo))
                {
                    checkNullAddressNumbers.Add((row + 1).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(excelModel.IsTransferElectricMeter))
                {
                    if (excelModel.IsTransferElectricMeter != "0" || excelModel.IsTransferElectricMeter != "1")
                    {
                        checkIsTransferElectricMeters.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.IsTransferWaterMeter))
                {
                    if (excelModel.IsTransferWaterMeter != "0" || excelModel.IsTransferWaterMeter != "1")
                    {
                        checkIsTransferWaterMeters.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.ElectricMeterTopic))
                {
                    if (!topics.Contains(excelModel.ElectricMeterTopic))
                    {
                        checkElectricMeterTopics.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.WaterMeterTopic))
                {
                    if (!topics.Contains(excelModel.WaterMeterTopic))
                    {
                        checkWaterMeterTopics.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[UnitMeterStatusExcelModel._electricMeterTransferDateIndex].ToString()))
                {
                    if (!r[UnitMeterStatusExcelModel._electricMeterTransferDateIndex].ToString().isFormatDateNew())
                    {
                        checkElectricMeterDates.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[UnitMeterStatusExcelModel._waterMeterTransferDate].ToString()))
                {
                    if (!r[UnitMeterStatusExcelModel._waterMeterTransferDate].ToString().isFormatDateNew())
                    {
                        checkWaterMeterDates.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                #endregion

                if (isError)
                {
                    error++;
                }
                row++;
            }

            if (checkNullUnitNos.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "UnitNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "UnitNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkNullAddressNumbers.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "AddressNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAddressNumbers));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "AddressNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAddressNumbers));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkIsTransferElectricMeters.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0075.Message.Replace("[column]", "สถานะโอนมิเตอร์ไฟฟ้า");
                    msg = msg.Replace("[row]", String.Join(",", checkIsTransferElectricMeters));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0075.Message.Replace("[column]", "สถานะโอนมิเตอร์ไฟฟ้า");
                    msg = msg.Replace("[row]", String.Join(",", checkIsTransferElectricMeters));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkIsTransferWaterMeters.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0075.Message.Replace("[column]", "สถานะโอนมิเตอร์น้ำประปา");
                    msg = msg.Replace("[row]", String.Join(",", checkIsTransferWaterMeters));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0075.Message.Replace("[column]", "สถานะโอนมิเตอร์น้ำประปา");
                    msg = msg.Replace("[row]", String.Join(",", checkIsTransferWaterMeters));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkElectricMeterTopics.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0070.Message.Replace("[column]", "หัวข้อ 1,2,3,4");
                    msg = msg.Replace("[row]", String.Join(",", checkElectricMeterTopics));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0070.Message.Replace("[column]", "หัวข้อ 1,2,3,4");
                    msg = msg.Replace("[row]", String.Join(",", checkElectricMeterTopics));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkWaterMeterTopics.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0070.Message.Replace("[column]", "หัวข้อ 1,2,3,4");
                    msg = msg.Replace("[row]", String.Join(",", checkWaterMeterTopics));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0070.Message.Replace("[column]", "หัวข้อ 1,2,3,4");
                    msg = msg.Replace("[row]", String.Join(",", checkWaterMeterTopics));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkElectricMeterDates.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0071.Message.Replace("[column]", "วันที่โอนมิเตอร์ไฟฟ้า Format=DD/MM/YYYY");
                    msg = msg.Replace("[row]", String.Join(",", checkElectricMeterDates));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0071.Message.Replace("[column]", "วันที่โอนมิเตอร์ไฟฟ้า Format=DD/MM/YYYY");
                    msg = msg.Replace("[row]", String.Join(",", checkElectricMeterDates));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkWaterMeterDates.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0071.Message.Replace("[column]", "วันที่โอนมิเตอร์น้ำประปาFormat=DD/MM/YYYY");
                    msg = msg.Replace("[row]", String.Join(",", checkWaterMeterDates));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0071.Message.Replace("[column]", "วันที่โอนมิเตอร์น้ำประปาFormat=DD/MM/YYYY");
                    msg = msg.Replace("[row]", String.Join(",", checkWaterMeterDates));
                    result.ErrorMessages.Add(msg);
                }
            }

            var projectNos = unitMeterStatusExcelModels.Select(o => o.ProjectNo).Distinct().ToList();
            var projects = await (from p in DB.Projects
                                  where projectNos.Contains(p.ProjectNo)
                                  select p).ToListAsync();
            var projectIDs = projects.Select(o => o.ID).Distinct().ToList();
            var units = await (from u in DB.Units
                               where projectIDs.Contains(u.ProjectID ?? Guid.Empty)
                               select u).ToListAsync();
            var meterTopics = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterTopic).ToListAsync();
            var meterStatuses = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterStatus).ToListAsync();
            List<Unit> updateList = new List<Unit>();

            //Update Data
            foreach (var item in unitMeterStatusExcelModels)
            {
                var project = projects.Find(o => o.ProjectNo == item.ProjectNo);
                if (project != null)
                {
                    var unit = units.Find(x => x.ProjectID == project.ID && x.UnitNo == item.UnitNo);
                    if (unit != null)
                    {
                        // ไฟฟ้า
                        unit.IsTransferElectricMeter = item.IsTransferElectricMeter == "0" ? false : item.IsTransferElectricMeter == "1" ? true : (bool?)null;
                        DateTime electricMeterTransferDate;
                        if (DateTime.TryParseExact(item.ElectricMeterTransferDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out electricMeterTransferDate))
                        {
                            unit.ElectricMeterTransferDate = electricMeterTransferDate;
                        }
                        var electricMeterTopicMasterCenter = meterTopics.Find(x => x.Key == item.ElectricMeterTopic);
                        unit.ElectricMeterTopicMasterCenterID = electricMeterTopicMasterCenter?.ID;
                        unit.ElectricMeterRemark = item.ElectricMeterRemark;
                        if (unit.IsTransferElectricMeter == true)
                        {
                            unit.ElectricMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "2")?.ID;
                        }
                        else if (unit.IsTransferElectricMeter != true && !string.IsNullOrEmpty(unit.ElectricMeter))
                        {
                            unit.ElectricMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "1")?.ID;
                        }
                        else if (string.IsNullOrEmpty(unit.ElectricMeter))
                        {
                            unit.ElectricMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "0")?.ID;
                        }
                        //น้ำ
                        unit.IsTransferWaterMeter = item.IsTransferWaterMeter == "0" ? false : item.IsTransferWaterMeter == "1" ? true : (bool?)null;
                        DateTime waterMeterTransferDate;
                        if (DateTime.TryParseExact(item.ElectricMeterTransferDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out waterMeterTransferDate))
                        {
                            unit.WaterMeterTransferDate = waterMeterTransferDate;
                        }
                        var waterMeterTopicMasterCenter = meterTopics.Find(x => x.Key == item.WaterMeterTopic);
                        unit.WaterMeterTopicMasterCenterID = waterMeterTopicMasterCenter?.ID;
                        unit.WaterMeterRemark = item.WaterMeterRemark;
                        if (unit.IsTransferWaterMeter == true)
                        {
                            unit.WaterMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "2")?.ID;
                        }
                        else if (unit.IsTransferWaterMeter != true && !string.IsNullOrEmpty(unit.WaterMeter))
                        {
                            unit.WaterMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "1")?.ID;
                        }
                        else if (string.IsNullOrEmpty(unit.WaterMeter))
                        {
                            unit.WaterMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "0")?.ID;
                        }
                        updateList.Add(unit);
                    }
                }
            }
            DB.Units.UpdateRange(updateList);
            await DB.SaveChangesAsync();
            result.Success = updateList.Count();
            result.Error = error;
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137937/preview
        /// Sample File: http://192.168.2.29:9001/xunit-tests/UnitMeterStatus.xls
        /// </summary>
        /// <returns>The unit meter excel async.</returns>
        public async Task<FileDTO> ExportUnitMeterStatusExcelAsync(UnitMeterFilter filter, UnitMeterListSortByParam sortByParam)
        {
            ExportExcel result = new ExportExcel();
            List<Database.Models.PRJ.Project> projects = new List<Database.Models.PRJ.Project>();
            if (!string.IsNullOrEmpty(filter.ProjectIDs))
            {
                var projectIds = filter.ProjectIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                projects = await DB.Projects.Where(o => projectIds.Contains(o.ID)).ToListAsync();
            }
            else
            {
                projects = await DB.Projects.ToListAsync();
            }
            var projectsId = projects.Select(o => o.ID).ToList();
            var units = await DB.Units.Where(o => projectsId.Contains(o.ProjectID.Value))
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.Model)
                                      .Include(o => o.ElectrictMeterStatus)
                                      .Include(o => o.WaterMeterStatus)
                                      .Include(o => o.WaterMeterTopic)
                                      .Include(o => o.ElectricMeterTopic)
                                      .ToListAsync();

            var query = (from project in projects
                         join unit in units
                         on project.ID equals unit.ProjectID
                         into temp
                         from tempdata in temp.DefaultIfEmpty()
                         select new UnitMeterListQueryResult
                         {
                             Project = project,
                             Unit = tempdata,
                         }).ToList();

            #region Filter
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo)).ToList();
            }
            if (filter.ModelID != null && filter.ModelID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.ModelID == filter.ModelID).ToList();
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                      && x.MasterCenterGroupKey == "UnitStatus")
                                                                     .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID).ToList();
            }
            if (filter.TransferOwnerShipDateFrom != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom).ToList();
            }
            if (filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo).ToList();
            }
            if (filter.TransferOwnerShipDateFrom != null && filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom
                                    && x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo).ToList();
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeter))
            {
                query = query.Where(x => x.Unit.ElectricMeter.Contains(filter.ElectricMeter)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.WaterMeter))
            {
                query = query.Where(x => x.Unit.WaterMeter.Contains(filter.WaterMeter)).ToList();
            }
            if (filter.CompletedDocumentDateFrom != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom).ToList();
            }
            if (filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo).ToList();
            }
            if (filter.CompletedDocumentDateFrom != null && filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom
                                    && x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo).ToList();
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeterStatusKey))
            {
                var electricMeterStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ElectricMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == electricMeterStatusMasterCenterID).ToList();
            }
            if (!string.IsNullOrEmpty(filter.WaterMeterStatusKey))
            {
                var waterMeterStatusKeyMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.WaterMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == waterMeterStatusKeyMasterCenterID).ToList();
            }

            #endregion

            ProjectUnitMeterListDTO.SortBy(sortByParam, ref query);

            var data = query;

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "UnitMeterStatus.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = UnitMeterStatusExcelModel._projectNoIndex + 1;
                int _unitNoIndex = UnitMeterStatusExcelModel._unitNoIndex + 1;
                int _addressNoIndex = UnitMeterStatusExcelModel._addressNoIndex + 1;
                int _isTransferElectricMeter = UnitMeterStatusExcelModel._isTransferElectricMeter + 1;
                int _electricMeterTransferDateIndex = UnitMeterStatusExcelModel._electricMeterTransferDateIndex + 1;
                int _electricMeterTopic = UnitMeterStatusExcelModel._electricMeterTopic + 1;
                int _electricMeterRemark = UnitMeterStatusExcelModel._electricMeterRemark + 1;
                int _isTransferWaterMeter = UnitMeterStatusExcelModel._isTransferWaterMeter + 1;
                int _waterMeterTransferDate = UnitMeterStatusExcelModel._waterMeterTransferDate + 1;
                int _waterMeterTopic = UnitMeterStatusExcelModel._waterMeterTopic + 1;
                int _waterMeterRemark = UnitMeterStatusExcelModel._waterMeterRemark + 1;



                for (int c = 2; c < data.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = data[c - 2].Project?.ProjectNo;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _addressNoIndex].Value = data[c - 2].Unit?.HouseNo;

                    worksheet.Cells[c, _isTransferElectricMeter].Value = data[c - 2].Unit?.IsTransferElectricMeter == true ? 1.ToString() : data[c - 2].Unit?.IsTransferElectricMeter == false ? 0.ToString() : null;

                    worksheet.Cells[c, _electricMeterTransferDateIndex].Style.Numberformat.Format = "dd/mm/yyyy";
                    worksheet.Cells[c, _electricMeterTransferDateIndex].Value = data[c - 2].Unit?.ElectricMeterTransferDate;

                    worksheet.Cells[c, _electricMeterTopic].Value = data[c - 2].Unit?.ElectricMeterTopic?.Key;
                    worksheet.Cells[c, _electricMeterRemark].Value = data[c - 2].Unit?.ElectricMeterRemark;


                    worksheet.Cells[c, _isTransferWaterMeter].Value = data[c - 2].Unit?.IsTransferWaterMeter == true ? 1.ToString() : data[c - 2].Unit?.IsTransferWaterMeter == false ? 0.ToString() : null; ;
                    worksheet.Cells[c, _waterMeterTransferDate].Style.Numberformat.Format = "dd/mm/yyyy";
                    worksheet.Cells[c, _waterMeterTransferDate].Value = data[c - 2].Unit?.WaterMeterTransferDate;
                    worksheet.Cells[c, _waterMeterTopic].Value = data[c - 2].Unit?.WaterMeterTopic?.Key;
                    worksheet.Cells[c, _waterMeterRemark].Value = data[c - 2].Unit?.WaterMeterRemark;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = "UnitMeterStatus_" + (DateTime.Today).ToString("yyyy-MM-dd") + ".xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"unit-meter/export-excels/";

            var uploadResult = await FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = result.FileName,
                Url = uploadResult.Url
            };
        }


        private async Task<Guid> UnitDataStatus(Guid projectID)
        {
            var allUnit = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
            var project = await DB.Projects.Where(o => o.ID == projectID).Include(o => o.ProductType).FirstAsync();
            var unitDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstAsync(); //พร้อมขาย
            var unitDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync(); //อยู่ระหว่างจัดเตรียม

            var unitDataStatusMasterCenterID = unitDataStatusPrepareMasterCenterID;
            if (allUnit.Count() == 0)
            {
                return unitDataStatusPrepareMasterCenterID;
            }
            if (project.ProductType.Key == ProductTypeKeys.HighRise)
            {
                if (allUnit.Any() && allUnit.TrueForAll(o =>
                          !string.IsNullOrEmpty(o.SAPWBSNo)
                          && !string.IsNullOrEmpty(o.SAPWBSObject)
                          && o.ModelID != null
                          && o.SaleArea != null
                          && o.UnitStatusMasterCenterID != null
                //&& !string.IsNullOrEmpty(o.FloorPlanFileName)
                //&& !string.IsNullOrEmpty(o.RoomPlanFileName)
                ))
                {
                    unitDataStatusMasterCenterID = unitDataStatusSaleMasterCenterID;
                }
            }
            else
            {
                if (allUnit.Any() && allUnit.TrueForAll(o =>
                        !string.IsNullOrEmpty(o.SAPWBSNo)
                        && !string.IsNullOrEmpty(o.SAPWBSObject)
                        && o.ModelID != null
                        && o.SaleArea != null
                        && o.UnitStatusMasterCenterID != null
                ))
                {
                    unitDataStatusMasterCenterID = unitDataStatusSaleMasterCenterID;
                }
            }
            return unitDataStatusMasterCenterID;
        }

        /// <summary>
        /// อ่าน Text File เพื่อ Update WBS กิ่ง P
        /// </summary>
        /// <returns></returns>
        public async Task<UnitSyncResponse> ReadSAPWBSPromotionTextFileAsync(string[] text)
        {
            var response = new UnitSyncResponse { SAPWBSNoNotFound = new List<string>(), Update = 0 };
            var listModelSap = new List<UnitSap>();
            foreach (var item in text)
            {
                var data = item.Split(';').ToList();
                var model = new UnitSap
                {
                    PSPNR = data[0],
                    POSID = data[1],
                    PSPHI = data[2],
                    ERDAT = data[3],
                    AEDAT = data[4],
                    VERNR = data[5],
                    VERNA = data[6],
                    ASTNR = data[7],
                    ASTNA = data[8],
                    PBUKR = data[9],
                    PRCTR = data[10],
                    PRART = data[11],
                    STUFE = data[12],
                    POST1 = data[13],
                    OBJNR = data[14],
                    DOWN = data[15],
                    WERKS = data[16],
                    HMTYP = data[17],
                    CRMID = data[18],
                    WBS = data[19],
                    WBSR = data[20],
                    ROBJ = data[21],
                };
                listModelSap.Add(model);
            }

            var updateUnit = new List<Unit>();
            foreach (var item in listModelSap)
            {
                var unit = await DB.Units.Where(o => o.SAPWBSNo == item.WBSR).FirstOrDefaultAsync();
                if (unit != null)
                {
                    unit.SAPWBSNo_P = item.WBS;
                    unit.SAPWBSObject_P = item.OBJNR;
                    updateUnit.Add(unit);
                }
                else
                {
                    response.SAPWBSNoNotFound.Add(item.WBSR);
                }
            }

            DB.Units.UpdateRange(updateUnit);
            await DB.SaveChangesAsync();
            response.Update = updateUnit.Count();
            return response;
        }


        public async Task<List<UnitDropdownDTO>> GetUnitPosition(string ProjectNo, CancellationToken cancellationToken = default)
        {
            IQueryable<Unit> query = DB.Units.AsNoTracking().Where(x => x.Project.ProjectNo == ProjectNo);

            var queryResults = await query.OrderBy(o => o.UnitNo).ToListAsync(cancellationToken);
            var results = queryResults.Select(o => UnitDropdownDTO.CreateFromModel(o)).ToList();

            return results;

        }

        public async Task<UnitMasterPlanDTO> GetUnitMasterPlanDetail(Guid UnitID, CancellationToken cancellationToken = default)
        {

            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("UnitID", UnitID);


            CommandDefinition commandDefinition = new(
                commandText: DBStoredNames.sp_GetMasterPlanUnitDetail,
                parameters: ParamList,
                commandTimeout: Timeout,
                transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken
            );
            var queryResult = await cmd.Connection.QueryAsync<dbqGetMasterPlanUnitDetail>(commandDefinition) ?? new List<dbqGetMasterPlanUnitDetail>();
            return queryResult.Select(o => UnitMasterPlanDTO.CreateFromModel(o))?.FirstOrDefault();
        }

        public async Task<bool> ClearPointUnitAsync(Guid projectID)
        {
            var query = await DB.Units.Where(x => x.ProjectID == projectID).ToListAsync();

            foreach (var item in query)
            {
                item.PositionX = null;
                item.PositionY = null;
            }
            DB.UpdateRange(query);
            await DB.SaveChangesAsync(); 
            return true;
        }

        public async Task<List<UnitDropdownDefectDTO>> GetUnitDropdownDefectListAsync(Guid projectID, string KeySearch, CancellationToken cancellationToken)
        {
            string sqlQuery = sqlUnitDropdownDefectList.QueryString;
            List<SqlParameter> ParamList = sqlUnitDropdownDefectList.QueryFilter(ref sqlQuery, projectID, KeySearch);

            var queryResult = await DBQuery.dbqUnitDropdownDefects.FromSqlRaw(sqlQuery, ParamList.ToArray()).ToListAsync(cancellationToken) ?? new List<dbqUnitDropdownDefect>();


            var result = queryResult.Select(x => UnitDropdownDefectDTO.CreateFromSQLQueryResult(x )).ToList();


            var tmpRst = result.Select(o => o.Id).ToList();
            var booking = await DB.Bookings.Where(o => tmpRst.Contains(o.UnitID) && o.IsCancelled == false).ToListAsync(cancellationToken);

            var results = new List<UnitDropdownDefectDTO>();
            foreach (var item in booking)
            {
                var sumAmount = await DB.Payments.Include(o => o.PaymentState).Where(o => o.BookingID == item.ID && (o.PaymentState.Key == "Booking" || o.PaymentState.Key == "Agreement") && o.IsCancel == false)
                    .SumAsync(o => o.TotalAmount, cancellationToken); 

                var pricelist = await DB.PriceListItems.Include(o => o.PriceList).Where(o => o.PriceList.ActiveDate <= DateTime.Now && o.PriceList.UnitID == item.UnitID && o.Order == 3).OrderByDescending(o => o.PriceList.ActiveDate).Select(o => o.Amount).FirstOrDefaultAsync(cancellationToken);
                 
                if (item.ApproveDate != null && sumAmount >= pricelist)
                {
                    var resultTmp = result.Where(o => o.Id == item.UnitID).FirstOrDefault();
                    if(resultTmp != null)
                    results.Add(resultTmp); 
                }
            }
            results = results.OrderBy(o => o.UnitNo).ToList();

            return results;
        }

        public async Task<bool> validateFileTemplate(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

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
                    if (tbl.Columns.Count != 9)
                    {
                        ValidateException ex = new ValidateException();
                        ex.AddError("1", "Import ข้อมูลไม่สำเร็จ เนื่องจากรูปแบบไฟล์ไม่ถูกต้อง\r\nกรุณาตรวจสอบและ Import ใหม่อีกครั้ง", 1);
                        throw ex;
                    }
                    return true;
                }
            }
        }

    }

}
