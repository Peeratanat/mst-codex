using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.LOG;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using ExcelExtensions;
using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using PRJ_Unit.Services.Excels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FileStorage;
namespace PRJ_Unit.Services
{
    public class HighRiseFeeService : IHighRiseFeeService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public HighRiseFeeService(DatabaseContext db)
        {
            logModel = new LogModel("HighRiseFeeService", null);
            this.DB = db;

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }
        public HighRiseFeeService(IConfiguration configuration, DatabaseContext db)
        {
            logModel = new LogModel("HighRiseFeeService", null);
            this.DB = db;
            this.Configuration = configuration;

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }

        public async Task<HighRiseFeePaging> GetHighRiseFeeListAsync(Guid projectID, HighRiseFeeFilter filter, PageParam pageParam, HighRiseFeeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {

            IQueryable<HighRiseFeeQueryResult> query = from t in DB.Towers.Where(o => o.ProjectID == projectID)
                                                       orderby t.TowerCode ascending

                                                       join fl in DB.Floors.Where(o => o.ProjectID == projectID) on t.ID equals fl.TowerID into flData
                                                       from flModel in flData.DefaultIfEmpty()
                                                       orderby flModel.NameTH

                                                       join hrff in DB.HighRiseFees on flModel.ID equals hrff.FloorID into hffData
                                                       from hffModel in hffData.DefaultIfEmpty()

                                                       join mst in DB.MasterCenters on hffModel.CalculateParkAreaMasterCenterID equals mst.ID into mstData
                                                       from mstModel in mstData.DefaultIfEmpty()

                                                       join unit in DB.Units on hffModel.UnitID equals unit.ID into unitData
                                                       from unitModel in unitData.DefaultIfEmpty()

                                                       join user in DB.Users on hffModel.UpdatedByUserID equals user.ID into userData
                                                       from userModel in userData.DefaultIfEmpty()

                                                       select new HighRiseFeeQueryResult
                                                       {
                                                           HighRiseFee = hffModel,
                                                           CalculateParkArea = mstModel,
                                                           Floor = flModel,
                                                           Tower = t,
                                                           Unit = unitModel,
                                                           UpdatedBy = userModel
                                                       };

            #region Filter
            #region estimatePriceAirArea
            if (filter.EstimatePriceAirAreaFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceAirArea >= filter.EstimatePriceAirAreaFrom);
            }
            if (filter.EstimatePriceAirAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceAirArea <= filter.EstimatePriceAirAreaTo);
            }
            if (filter.EstimatePriceAirAreaFrom != null && filter.EstimatePriceAirAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceAirArea >= filter.EstimatePriceAirAreaFrom
                                    && x.HighRiseFee.EstimatePriceAirArea <= filter.EstimatePriceAirAreaTo);
            }
            #endregion

            #region estimatePriceArea
            if (filter.EstimatePriceAreaFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceArea >= filter.EstimatePriceAreaFrom);
            }
            if (filter.EstimatePriceAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceArea <= filter.EstimatePriceAreaTo);
            }
            if (filter.EstimatePriceAreaFrom != null && filter.EstimatePriceAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceArea >= filter.EstimatePriceAreaFrom
                                    && x.HighRiseFee.EstimatePriceArea <= filter.EstimatePriceAreaTo);
            }
            #endregion

            #region estimatePriceBalconyArea
            if (filter.EstimatePriceBalconyAreaFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceBalconyArea >= filter.EstimatePriceBalconyAreaFrom);
            }
            if (filter.EstimatePriceBalconyAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceBalconyArea <= filter.EstimatePriceBalconyAreaTo);
            }
            if (filter.EstimatePriceBalconyAreaFrom != null && filter.EstimatePriceBalconyAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceBalconyArea >= filter.EstimatePriceBalconyAreaFrom
                                    && x.HighRiseFee.EstimatePriceBalconyArea <= filter.EstimatePriceBalconyAreaTo);
            }
            #endregion

            #region estimatePricePoolArea
            if (filter.EstimatePricePoolAreaFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePricePoolArea >= filter.EstimatePricePoolAreaFrom);
            }
            if (filter.EstimatePricePoolAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePricePoolArea <= filter.EstimatePricePoolAreaTo);
            }
            if (filter.EstimatePricePoolAreaFrom != null && filter.EstimatePricePoolAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePricePoolArea >= filter.EstimatePricePoolAreaFrom
                                    && x.HighRiseFee.EstimatePricePoolArea <= filter.EstimatePricePoolAreaTo);
            }
            #endregion

            #region estimatePriceUsageArea
            if (filter.EstimatePriceUsageAreaFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceUsageArea >= filter.EstimatePriceUsageAreaFrom);
            }
            if (filter.EstimatePriceUsageAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceUsageArea <= filter.EstimatePriceUsageAreaTo);
            }
            if (filter.EstimatePriceUsageAreaFrom != null && filter.EstimatePriceUsageAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceUsageArea >= filter.EstimatePriceUsageAreaFrom
                                    && x.HighRiseFee.EstimatePriceUsageArea <= filter.EstimatePriceUsageAreaTo);
            }
            #endregion

            if (!string.IsNullOrEmpty(filter.CalculateParkAreaKey))
            {
                var calculateParkAreaID = await DB.MasterCenters.Where(x => x.Key == filter.CalculateParkAreaKey
                                                                 && x.MasterCenterGroupKey == "CalculateParkArea")
                                                                .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.HighRiseFee.CalculateParkAreaMasterCenterID == calculateParkAreaID);
            }
            if (filter.FloorID != null && filter.FloorID != Guid.Empty)
            {
                query = query.Where(x => x.HighRiseFee.Floor.ID == filter.FloorID);
            }
            if (filter.UnitID != null && filter.UnitID != Guid.Empty)
            {
                query = query.Where(x => x.HighRiseFee.UnitID == filter.UnitID);
            }
            if (filter.TowerID != null && filter.TowerID != Guid.Empty)
            {
                query = query.Where(x => x.HighRiseFee.Tower.ID == filter.TowerID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.HighRiseFee.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.HighRiseFee.Updated >= filter.UpdatedFrom && x.HighRiseFee.Updated <= filter.UpdatedTo);
            }
            if (filter.UnitID == Guid.Empty)
            {
                query = query.Where(o => o.HighRiseFee.UnitID == null);
            }
            #endregion

            HighRiseFeeDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<HighRiseFeeQueryResult>(pageParam, ref query);

            var results = await query.Select(o => HighRiseFeeDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new HighRiseFeePaging()
            {
                PageOutput = pageOutput,
                HighRiseFees = results
            };
        }

        public async Task<HighRiseFeeDTO> GetHighRiseFeeAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.HighRiseFees.Where(o => o.ProjectID == projectID && o.ID == id)
                                                   .Select(o => new HighRiseFeeQueryResult
                                                   {
                                                       HighRiseFee = o,
                                                       CalculateParkArea = o.CalculateParkArea,
                                                       Floor = o.Floor,
                                                       Tower = o.Tower,
                                                       Unit = o.Unit,
                                                       UpdatedBy = o.UpdatedBy
                                                   }).FirstAsync(cancellationToken);
            var result = HighRiseFeeDTO.CreateFromQueryResult(model);
            return result;
        }

        public async Task<HighRiseFeeDTO> CreateHighRiseFeeAsync(Guid projectID, HighRiseFeeDTO input)
        {
            await this.ValidateHighRiseFee(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            HighRiseFee model = new HighRiseFee();
            input.ToModel(ref model);
            if (model.UnitID != null)
            {
                var unit = await DB.Units.Where(o => o.ID == model.UnitID).FirstOrDefaultAsync();
                model.TowerID = unit.TowerID;
                model.FloorID = unit.FloorID;
            }
            model.ProjectID = projectID;
            await DB.HighRiseFees.AddAsync(model);
            await DB.SaveChangesAsync();


            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetHighRiseFeeAsync(projectID, model.ID);
            return result;
        }

        public async Task<HighRiseFeeDTO> UpdateHighRiseFeeAsync(Guid projectID, Guid id, HighRiseFeeDTO input)
        {
            await this.ValidateHighRiseFee(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.HighRiseFees.Where(o => o.ProjectID == projectID && o.ID == id)
                                             .FirstAsync();

            input.ToModel(ref model);
            model.ProjectID = projectID;
            if (model.UnitID != null)
            {
                var unit = await DB.Units.Where(o => o.ID == model.UnitID).FirstOrDefaultAsync();
                model.TowerID = unit.TowerID;
                model.FloorID = unit.FloorID;
            }

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();


            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetHighRiseFeeAsync(projectID, id);
            return result;
        }

        public async Task<HighRiseFee> DeleteHighRiseFeeAsync(Guid projectID, Guid id)
        {
            var project = await DB.Projects.FirstAsync(o => o.ID == projectID);
            var model = await DB.HighRiseFees.FirstAsync(o => o.ProjectID == projectID && o.ID == id);

            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<HighRiseFeeExcelDTO> ImportHighRiseFeeAsyncTemp(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0061");
            // Decimal 2 Digit
            var err0065 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0065");
            // Not Found
            var err0062 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0062");
            // Unique
            var err0084 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0084");
            // 0 1 
            var err0086 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0086");

            var result = new HighRiseFeeExcelDTO
            {
                Error = 0,
                Success = 0,
            };
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 10)
            {
                throw new Exception("Invalid File Format");
            }
            //Read Excel Model
            var row = 2;
            var error = 0;
            var highRiseFeeExcelModels = new List<HighRiseFeeExcelModel>();

            var project = await DB.Projects.FirstAsync(o => o.ID == projectID);
            var towers = await DB.Towers.Where(o => o.ProjectID == projectID).ToListAsync();
            var floors = await DB.Floors.Where(o => o.ProjectID == projectID).ToListAsync();
            var units = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
            var highRiseFees = await DB.HighRiseFees.Where(o => o.ProjectID == projectID)
                                                    .Include(o => o.Unit)
                                                    .Include(o => o.Tower)
                                                    .Include(o => o.Floor)
                                                    .ToListAsync();
            var calculateCarParkAreas = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.CalculateParkArea).ToListAsync();


            var formatCalulateCarParks = new List<string> { "0", "1" };

            var checkNullTowers = new List<string>();
            var checkNullFloors = new List<string>();
            var checkNullUnits = new List<string>();
            var checkTowerNotFounds = new List<string>();
            var checkFloorNotFounds = new List<string>();
            var checkUnitNotFounds = new List<string>();
            var checkUniqueUnitNo = new List<string>();
            var chcekFormatCalculateCarParks = new List<string>();
            var checkFormatEstimatePriceAreas = new List<string>();
            var checkFormatEstimatePriceUsageAreas = new List<string>();
            var checkFormatEstimatePriceBalconyAreas = new List<string>();
            var checkFormatEstimatePriceAirAreas = new List<string>();
            var checkFormatEstimatePricePoolAreas = new List<string>();

            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = HighRiseFeeExcelModel.CreateFromDataRow(r);
                highRiseFeeExcelModels.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.TowerCode))
                {
                    checkNullTowers.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    var tower = towers.Find(o => o.TowerCode == excelModel.TowerCode);
                    if (tower == null)
                    {
                        checkTowerNotFounds.Add(row.ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(excelModel.FloorName))
                {
                    checkNullFloors.Add(row.ToString());
                    isError = true;
                }
                else
                {
                    var floor = floors.Find(o => o.NameTH == excelModel.FloorName || o.NameEN == excelModel.FloorName);
                    if (floor == null)
                    {
                        checkFloorNotFounds.Add((row).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnits.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (excelModel.UnitNo != "ทุกแปลง")
                    {
                        var unit = units.Find(o => o.UnitNo == excelModel.UnitNo);
                        if (unit == null)
                        {
                            checkUnitNotFounds.Add((row).ToString());
                            isError = true;
                        }
                        var unique = highRiseFees.Find(o => o.Unit?.UnitNo == excelModel.UnitNo);
                        if (unique != null)
                        {
                            checkUniqueUnitNo.Add((row).ToString());
                            isError = true;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.CalculateParkArea))
                {
                    if (!formatCalulateCarParks.Contains(excelModel.CalculateParkArea))
                    {
                        chcekFormatCalculateCarParks.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[HighRiseFeeExcelModel._estimatePriceAreaIndex].ToString()))
                {
                    if (!r[HighRiseFeeExcelModel._estimatePriceAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatEstimatePriceAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[HighRiseFeeExcelModel._estimatePriceUsageAreaIndex].ToString()))
                {
                    if (!r[HighRiseFeeExcelModel._estimatePriceUsageAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatEstimatePriceUsageAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[HighRiseFeeExcelModel._estimatePriceBalconyArea].ToString()))
                {
                    if (!r[HighRiseFeeExcelModel._estimatePriceBalconyArea].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatEstimatePriceBalconyAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[HighRiseFeeExcelModel._estimatePriceAirAreaIndex].ToString()))
                {
                    if (!r[HighRiseFeeExcelModel._estimatePriceAirAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatEstimatePriceAirAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[HighRiseFeeExcelModel._estimatePricePoolAreaIndex].ToString()))
                {
                    if (!r[HighRiseFeeExcelModel._estimatePricePoolAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatEstimatePricePoolAreas.Add((row).ToString());
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

            ValidateException ex = new ValidateException();
            if (highRiseFeeExcelModels.Any(o => o.ProjectNo != project.ProjectNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            #region AddResult Validate

            #region Required
            if (checkNullTowers.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ตึก");
                    msg = msg.Replace("[row]", String.Join(",", checkNullTowers));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ตึก");
                    msg = msg.Replace("[row]", String.Join(",", checkNullTowers));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullFloors.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ชั้น");
                    msg = msg.Replace("[row]", String.Join(",", checkNullFloors));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ชั้น");
                    msg = msg.Replace("[row]", String.Join(",", checkNullFloors));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullUnits.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnits));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnits));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region Format
            if (chcekFormatCalculateCarParks.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0086.Message.Replace("[column]", "คำนวณที่จอดรถตาม พท.");
                    msg = msg.Replace("[row]", String.Join(",", chcekFormatCalculateCarParks));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0086.Message.Replace("[column]", "คำนวณที่จอดรถตาม พท.");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePriceAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatEstimatePriceAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน พท.จอดรถ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePriceAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน พท.จอดรถ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePriceAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatEstimatePriceUsageAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน พท.ใช้สอย");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePriceUsageAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน พท.ใช้สอย");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePriceUsageAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatEstimatePriceBalconyAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน พท.ใช้สอยระเบียง");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePriceBalconyAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน พท.ใช้สอยระเบียง");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePriceBalconyAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatEstimatePriceAirAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน พท.วางแอร์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePriceAirAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน พท.วางแอร์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePriceAirAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatEstimatePricePoolAreas.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน พท.สระว่ายน้ำ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePricePoolAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน พท.สระว่ายน้ำ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePricePoolAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region NotFound
            if (checkTowerNotFounds.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "ตึก");
                    msg = msg.Replace("[row]", String.Join(",", checkTowerNotFounds));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "ตึก");
                    msg = msg.Replace("[row]", String.Join(",", checkTowerNotFounds));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFloorNotFounds.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "ชั้น");
                    msg = msg.Replace("[row]", String.Join(",", checkFloorNotFounds));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "ชั้น");
                    msg = msg.Replace("[row]", String.Join(",", checkFloorNotFounds));
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
            #endregion

            #region Unique
            if (checkUniqueUnitNo.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0084.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkUniqueUnitNo));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0084.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkUniqueUnitNo));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #endregion

            #region RowErrors
            var rowErrors = new List<string>();
            rowErrors.AddRange(checkNullTowers);
            rowErrors.AddRange(checkNullFloors);
            rowErrors.AddRange(checkNullUnits);
            rowErrors.AddRange(checkTowerNotFounds);
            rowErrors.AddRange(checkFloorNotFounds);
            rowErrors.AddRange(checkUnitNotFounds);
            rowErrors.AddRange(checkUniqueUnitNo);
            rowErrors.AddRange(chcekFormatCalculateCarParks);
            rowErrors.AddRange(checkFormatEstimatePriceAreas);
            rowErrors.AddRange(checkFormatEstimatePriceUsageAreas);
            rowErrors.AddRange(checkFormatEstimatePriceBalconyAreas);
            rowErrors.AddRange(checkFormatEstimatePriceAirAreas);
            rowErrors.AddRange(checkFormatEstimatePricePoolAreas);
            #endregion

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            row = 2;
            List<HighRiseFee> highRiseFeesUpdate = new List<HighRiseFee>();
            List<HighRiseFee> highRiseFeesCreate = new List<HighRiseFee>();
            foreach (var item in highRiseFeeExcelModels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var existeingModel = highRiseFees.Find(o => o.Tower.TowerCode == item.TowerCode
                                                            && (o.Floor.NameTH == item.FloorName)
                                                            && (item.UnitNo == "ทุกแปลง" ? o.UnitID == null : o.Unit?.UnitNo == item.UnitNo)
                                                            );
                    if (existeingModel == null) //Update
                    {
                        var model = new HighRiseFee();
                        var tower = towers.Find(o => o.TowerCode == item.TowerCode);
                        if (tower != null)
                        {
                            model.TowerID = tower.ID;
                        }
                        var floor = floors.Find(o => o.NameTH == item.FloorName && o.TowerID == model.TowerID);
                        if (floor != null)
                        {
                            model.FloorID = floor.ID;
                        }
                        if (item.UnitNo == "ทุกแปลง")
                        {
                            model.UnitID = (Guid?)null;
                        }
                        else
                        {
                            var unit = units.Find(o => o.UnitNo == item.UnitNo);
                            if (unit != null)
                            {
                                model.UnitID = unit.ID;
                            }
                        }
                        if (model.FloorID != (Guid?)null && model.TowerID != (Guid?)null && ((item.UnitNo != "ทุกแปลง" && model.UnitID != (Guid?)null) || item.UnitNo == "ทุกแปลง"))
                        {
                            item.ToModel(ref model);
                            model.CalculateParkAreaMasterCenterID = calculateCarParkAreas.Find(o => o.Key == item.CalculateParkArea)?.ID;
                            model.ProjectID = project.ID;
                            highRiseFeesCreate.Add(model);
                        }
                    }
                    else
                    {
                        item.ToModel(ref existeingModel);
                        existeingModel.CalculateParkAreaMasterCenterID = calculateCarParkAreas.Find(o => o.Key == item.CalculateParkArea)?.ID;
                        highRiseFeesUpdate.Add(existeingModel);
                    }
                }
                row++;
            }
            DB.HighRiseFees.UpdateRange(highRiseFeesUpdate);
            await DB.HighRiseFees.AddRangeAsync(highRiseFeesCreate);
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            result.Success = highRiseFeesUpdate.Count() + highRiseFeesCreate.Count();
            result.Error = error;
            return result;
        }

        public async Task<HighRiseFeeExcelDTO> ImportHighRiseFeeAsync(Guid projectID, FileDTO input, Guid? UserID = null)
        {
            var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
            var result = new HighRiseFeeExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>(), Messages = new List<string>() };

            if (input.IsTemp)
            {
                string Name = "LandAppraisalPrice.xlsx";
                string titledeedName = $"import-project/{projectNo}/landappraisal/{Name}";
                await FileHelper.MoveTempFileAsync(input.Name, titledeedName);
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
            imp.Import_Type = "landappraisal";
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
        public async Task<FileDTO> ExportHighRiseFeeAsync(Guid projectID, HighRiseFeeFilter filter, HighRiseFeeSortByParam sortByParam)
        {
            ExportExcel result = new ExportExcel();

            IQueryable<HighRiseFeeQueryResult> query = from t in DB.Towers.Where(o => o.ProjectID == projectID)
                                                       orderby t.TowerCode ascending

                                                       join fl in DB.Floors.Where(o => o.ProjectID == projectID) on t.ID equals fl.TowerID into flData
                                                       from flModel in flData.DefaultIfEmpty()
                                                       orderby flModel.NameTH

                                                       join hrff in DB.HighRiseFees on flModel.ID equals hrff.FloorID into hffData
                                                       from hffModel in hffData.DefaultIfEmpty()

                                                       join mst in DB.MasterCenters on hffModel.CalculateParkAreaMasterCenterID equals mst.ID into mstData
                                                       from mstModel in mstData.DefaultIfEmpty()

                                                       join unit in DB.Units on hffModel.UnitID equals unit.ID into unitData
                                                       from unitModel in unitData.DefaultIfEmpty()

                                                       join user in DB.Users on hffModel.UpdatedByUserID equals user.ID into userData
                                                       from userModel in userData.DefaultIfEmpty()

                                                       select new HighRiseFeeQueryResult
                                                       {
                                                           HighRiseFee = hffModel,
                                                           CalculateParkArea = mstModel,
                                                           Floor = flModel,
                                                           Tower = t,
                                                           Unit = unitModel,
                                                           UpdatedBy = userModel
                                                       };

            #region Filter
            #region estimatePriceAirArea
            if (filter.EstimatePriceAirAreaFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceAirArea >= filter.EstimatePriceAirAreaFrom);
            }
            if (filter.EstimatePriceAirAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceAirArea <= filter.EstimatePriceAirAreaTo);
            }
            if (filter.EstimatePriceAirAreaFrom != null && filter.EstimatePriceAirAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceAirArea >= filter.EstimatePriceAirAreaFrom
                                    && x.HighRiseFee.EstimatePriceAirArea <= filter.EstimatePriceAirAreaTo);
            }
            #endregion

            #region estimatePriceArea
            if (filter.EstimatePriceAreaFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceArea >= filter.EstimatePriceAreaFrom);
            }
            if (filter.EstimatePriceAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceArea <= filter.EstimatePriceAreaTo);
            }
            if (filter.EstimatePriceAreaFrom != null && filter.EstimatePriceAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceArea >= filter.EstimatePriceAreaFrom
                                    && x.HighRiseFee.EstimatePriceArea <= filter.EstimatePriceAreaTo);
            }
            #endregion

            #region estimatePriceBalconyArea
            if (filter.EstimatePriceBalconyAreaFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceBalconyArea >= filter.EstimatePriceBalconyAreaFrom);
            }
            if (filter.EstimatePriceBalconyAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceBalconyArea <= filter.EstimatePriceBalconyAreaTo);
            }
            if (filter.EstimatePriceBalconyAreaFrom != null && filter.EstimatePriceBalconyAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceBalconyArea >= filter.EstimatePriceBalconyAreaFrom
                                    && x.HighRiseFee.EstimatePriceBalconyArea <= filter.EstimatePriceBalconyAreaTo);
            }
            #endregion

            #region estimatePricePoolArea
            if (filter.EstimatePricePoolAreaFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePricePoolArea >= filter.EstimatePricePoolAreaFrom);
            }
            if (filter.EstimatePricePoolAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePricePoolArea <= filter.EstimatePricePoolAreaTo);
            }
            if (filter.EstimatePricePoolAreaFrom != null && filter.EstimatePricePoolAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePricePoolArea >= filter.EstimatePricePoolAreaFrom
                                    && x.HighRiseFee.EstimatePricePoolArea <= filter.EstimatePricePoolAreaTo);
            }
            #endregion

            #region estimatePriceUsageArea
            if (filter.EstimatePriceUsageAreaFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceUsageArea >= filter.EstimatePriceUsageAreaFrom);
            }
            if (filter.EstimatePriceUsageAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceUsageArea <= filter.EstimatePriceUsageAreaTo);
            }
            if (filter.EstimatePriceUsageAreaFrom != null && filter.EstimatePriceUsageAreaTo != null)
            {
                query = query.Where(x => x.HighRiseFee.EstimatePriceUsageArea >= filter.EstimatePriceUsageAreaFrom
                                    && x.HighRiseFee.EstimatePriceUsageArea <= filter.EstimatePriceUsageAreaTo);
            }
            #endregion

            if (!string.IsNullOrEmpty(filter.CalculateParkAreaKey))
            {
                var calculateParkAreaID = await DB.MasterCenters.Where(x => x.Key == filter.CalculateParkAreaKey
                                                                 && x.MasterCenterGroupKey == "CalculateParkArea")
                                                                .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.HighRiseFee.CalculateParkAreaMasterCenterID == calculateParkAreaID);
            }
            if (filter.FloorID != null && filter.FloorID != Guid.Empty)
            {
                query = query.Where(x => x.HighRiseFee.Unit.FloorID == filter.FloorID);
            }
            if (filter.UnitID != null && filter.UnitID != Guid.Empty)
            {
                query = query.Where(x => x.HighRiseFee.UnitID == filter.UnitID);
            }
            if (filter.TowerID != null && filter.TowerID != Guid.Empty)
            {
                query = query.Where(x => x.HighRiseFee.Unit.TowerID == filter.TowerID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.HighRiseFee.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.HighRiseFee.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.HighRiseFee.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.HighRiseFee.Updated >= filter.UpdatedFrom && x.HighRiseFee.Updated <= filter.UpdatedTo);
            }
            #endregion

            HighRiseFeeDTO.SortBy(sortByParam, ref query);
            if (sortByParam?.SortBy is null)
            {
                query = query.OrderBy(o => o.Tower.TowerCode).ThenBy(o => o.Floor.NameEN) ;
            }
            var results = await query.ToListAsync();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_LandAppraisalPrice.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                int _projectNoIndex = HighRiseFeeExcelModel._projectNoIndex + 1;
                int _towerCodeIndex = HighRiseFeeExcelModel._towerCodeIndex + 1;
                int _floorNameIndex = HighRiseFeeExcelModel._floorNameIndex + 1;
                int _unitNoIndex = HighRiseFeeExcelModel._unitNoIndex + 1;
                int _calculateParkAreaIndex = HighRiseFeeExcelModel._calculateParkAreaIndex + 1;
                int _estimatePriceAreaIndex = HighRiseFeeExcelModel._estimatePriceAreaIndex + 1;
                int _estimatePriceUsageAreaIndex = HighRiseFeeExcelModel._estimatePriceUsageAreaIndex + 1;
                int _estimatePriceBalconyArea = HighRiseFeeExcelModel._estimatePriceBalconyArea + 1;
                int _estimatePriceAirAreaIndex = HighRiseFeeExcelModel._estimatePriceAirAreaIndex + 1;
                int _estimatePricePoolAreaIndex = HighRiseFeeExcelModel._estimatePricePoolAreaIndex + 1;

                var project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
                for (int c = 2; c < results.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = project.ProjectNo;
                    worksheet.Cells[c, _towerCodeIndex].Value = results[c - 2].Tower?.TowerCode;
                    worksheet.Cells[c, _floorNameIndex].Value = results[c - 2].Floor?.NameEN;
                    worksheet.Cells[c, _unitNoIndex].Value = results[c - 2].Unit == null ? "ทุกแปลง" : results[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _calculateParkAreaIndex].Value = results[c - 2].CalculateParkArea?.Key;
                    worksheet.Cells[c, _estimatePriceAreaIndex].Value = results[c - 2].HighRiseFee?.EstimatePriceArea;
                    worksheet.Cells[c, _estimatePriceUsageAreaIndex].Value = results[c - 2].HighRiseFee?.EstimatePriceUsageArea;
                    worksheet.Cells[c, _estimatePriceBalconyArea].Value = results[c - 2].HighRiseFee?.EstimatePriceBalconyArea;
                    worksheet.Cells[c, _estimatePriceAirAreaIndex].Value = results[c - 2].HighRiseFee?.EstimatePriceAirArea;
                    worksheet.Cells[c, _estimatePricePoolAreaIndex].Value = results[c - 2].HighRiseFee?.EstimatePricePoolArea;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ProjectNo + "_LandAppraisalPrice.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = result.FileName; //$"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"project/{projectID}/export-excels/";
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var uploadResult = await this.FileHelper.UploadFileFromStreamWithOutGuid(fileStream, minioTempBucketName, filePath, fileName, contentType);
            return new FileDTO()
            {
                Name = result.FileName,
                Url = uploadResult.Url
            };
        }

        private async Task<Guid> TransferFeeDataStatus(Guid projectID)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).Include(o => o.ProductType).FirstAsync();
            var allHiseRiseFee = await DB.HighRiseFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseFees = await DB.LowRiseFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseFenceFees = await DB.LowRiseFenceFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseBuildingPriceFees = await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allRoundFees = await DB.RoundFees.Where(o => o.ProjectID == projectID).ToListAsync();


            var transferFeeDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
            var transferFeeDataStatusTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer).Select(o => o.ID).FirstAsync();
            var transferFeeDataStatusMasterCenterID = transferFeeDataStatusPrepareMasterCenterID;

            if (project.ProductType.Key == ProductTypeKeys.LowRise)
            {
                if (allLowRiseFees.Count() == 0 || allRoundFees.Count() == 0 || allLowRiseBuildingPriceFees.Count() == 0)
                {
                    return transferFeeDataStatusMasterCenterID;
                }
                if (allLowRiseFees.TrueForAll(o => o.EstimatePriceArea != null)
                    && allRoundFees.TrueForAll(o => o.LandOfficeID != null && o.OtherFee != null && o.LocalTaxRoundFormulaMasterCenterID != null && o.TransferFeeRoundFormulaMasterCenterID != null && o.IncomeTaxRoundFormulaMasterCenterID != null && o.BusinessTaxRoundFormulaMasterCenterID != null)
                    && allLowRiseBuildingPriceFees.TrueForAll(o => o.ModelID != null && o.UnitID != null && o.Price != null)
                    )
                {
                    transferFeeDataStatusMasterCenterID = transferFeeDataStatusTransferMasterCenterID;
                }
            }
            else
            {
                if (allRoundFees.Count() == 0)
                {
                    return transferFeeDataStatusMasterCenterID;
                }
                if (allRoundFees.TrueForAll(o => o.LandOfficeID != null && o.OtherFee != null && o.LocalTaxRoundFormulaMasterCenterID != null && o.TransferFeeRoundFormulaMasterCenterID != null && o.IncomeTaxRoundFormulaMasterCenterID != null && o.BusinessTaxRoundFormulaMasterCenterID != null))
                {
                    transferFeeDataStatusMasterCenterID = transferFeeDataStatusTransferMasterCenterID;
                }
            }
            return transferFeeDataStatusMasterCenterID;
        }

        private async Task ValidateHighRiseFee(Guid projectID, HighRiseFeeDTO input)
        {
            ValidateException ex = new ValidateException();

            if (input.Unit != null)
            {
                var checkUniqueUnit = input.Id != (Guid?)null
               ? await DB.HighRiseFees.Where(o => o.ProjectID == projectID && o.ID != input.Id && o.UnitID == input.Unit.Id).CountAsync() > 0
               : await DB.HighRiseFees.Where(o => o.ProjectID == projectID && o.UnitID == input.Unit.Id).CountAsync() > 0;
                if (checkUniqueUnit)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(HighRiseFeeDTO.Unit)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.Unit.UnitNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

    }
}
