using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using MST_Unitmeter.Params.Filters;
using MST_Unitmeter.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helper.Logging;
using OfficeOpenXml;
using MST_Unitmeter.Services.Excels;
using Common.Helper;
using Microsoft.Extensions.Configuration;
using Database.Models.MasterKeys;
using System.Data;
using ExcelExtensions;
using System.Globalization;
using ErrorHandling;
using FileStorage;
namespace MST_Unitmeter.Services
{
    public class UnitMeterService : IUnitMeterService
    {
        private FileHelper FileHelper;
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        public LogModel logModel { get; set; }
        public UnitMeterService(DatabaseContext db)
        {
            logModel = new LogModel("UnitMeterService", null);
            this.DB = db;
            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }
        public UnitMeterService(IConfiguration configuration, DatabaseContext db)
        {
            this.Configuration = configuration;
            logModel = new LogModel("UnitMeterService", null);
            this.DB = db;
            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }

        public async Task<List<WaterMeterPriceDropdownDTO>> GetWaterMeterPriceDropdownListAsync(Guid unitID, CancellationToken cancellationToken = default)
        {
            var modelID = (await DB.Units.FirstOrDefaultAsync(x => x.ID == unitID))?.ModelID;
            IQueryable<WaterElectricMeterPrice> query = DB.WaterElectricMeterPrices.Include(o => o.UpdatedBy).Where(x => x.ModelID == modelID).OrderByDescending(o => o.Version);

            var queryResults = await query.OrderBy(o => o.Version).ToListAsync(cancellationToken);
            var results = queryResults.Select(o => WaterMeterPriceDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }
        public async Task<List<ElectricMeterPriceDropdownDTO>> GetElectricMeterPriceDropdownListAsync(Guid unitID, CancellationToken cancellationToken = default)
        {
            var modelID = (await DB.Units.FirstOrDefaultAsync(x => x.ID == unitID))?.ModelID;
            IQueryable<WaterElectricMeterPrice> query = DB.WaterElectricMeterPrices.Include(o => o.UpdatedBy).Where(x => x.ModelID == modelID).OrderByDescending(o => o.Version);

            var queryResults = await query.ToListAsync(cancellationToken);
            var results = queryResults.Select(o => ElectricMeterPriceDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<UnitMeterPaging> GetUnitMeterListAsync(UnitMeterFilter filter, PageParam pageParam, UnitMeterListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<UnitMeterListQueryResult> query = from project in DB.Projects.AsNoTracking()
                                                         .Include(o => o.UpdatedBy)
                                                         join unit in DB.Units.AsNoTracking().DefaultIfEmpty()
                                                         .Include(o => o.UpdatedBy)
                                                         .Include(o => o.UnitStatus)
                                                         .Include(o => o.Model)
                                                         .Include(o => o.ElectrictMeterStatus)
                                                         .Include(o => o.WaterMeterStatus)
                                                         .Include(o => o.WaterMeterTopic)
                                                         .Include(o => o.ElectricMeterTopic)
                                                         on project.ID equals unit.ProjectID
                                                         where project.IsActive == true && !project.ProjectNameTH.Contains("ระงับ") && !project.ProjectNameTH.Contains("ไม่ใช้")
                                                         select new UnitMeterListQueryResult
                                                         {
                                                             Project = project,
                                                             Unit = unit,
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
                var unitStatusMasterCenterID = (await DB.MasterCenters.FirstAsync(x => x.Key == filter.UnitStatusKey
                                                                      && x.MasterCenterGroupKey == "UnitStatus")).ID;
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

            var queryResults = await query.ToListAsync(cancellationToken);

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

        public async Task<UnitMeterDTO> GetUnitMeterAsync(Guid unitID, CancellationToken cancellationToken = default)
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

        public async Task DeleteUnitMeterAsync(Guid id)
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

        }


        public async Task<UnitMeterDTO> UpdateUnitMeterAsync(Guid unitID, UnitMeterDTO input, Guid? UserID = null)
        {
            var model = await DB.Units.Where(x => x.ID == unitID).FirstAsync();
            var meterStatuses = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterStatus).ToListAsync();
            input.ToModel(ref model);

            if (model.IsTransferElectricMeter == true)
            {
                model.ElectricMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "2")?.ID;
                model.ElectricMeterTransferDateByUserID = UserID;
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
                model.WaterMeterTransferDateByUserID = UserID;
            }
            else if (model.IsTransferWaterMeter != true && !string.IsNullOrEmpty(model.WaterMeter))
            {
                model.WaterMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "1")?.ID;
            }
            else if (string.IsNullOrEmpty(model.WaterMeter))
            {
                model.WaterMeterStatusMasterCenterID = meterStatuses.Find(o => o.Key == "0")?.ID;
            }


            //สถานะได้รับเอกสาร, วันที่ได้รับเอกสาร  ไฟ
            model.IsElectricMeterDocReceiptDate = input.IsElectricMeterDocReceiptDate;
            model.ElectricMeterDocReceiptDate = input.ElectricMeterDocReceiptDate;
            if(input.IsElectricMeterDocReceiptDate.HasValue && model.ElectricMeterDocReceiptDate != null)
                model.ElectricMeterDocReceiptByUserID = UserID;


            //สถานะได้รับเอกสาร, วันที่ได้รับเอกสาร  น้ำ
            model.IsWaterMeterDocReceiptDate = input.IsWaterMeterDocReceiptDate;
            model.WaterMeterDocReceiptDate = input.WaterMeterDocReceiptDate;
            if (input.IsWaterMeterDocReceiptDate.HasValue && model.WaterMeterDocReceiptDate != null)
                model.WaterMeterDocReceiptByUserID = UserID;


            //วันที่ติดตั้งมิเตอร์ไฟฟ้า
            model.ElectricMeterInstallDate = input.ElectricMeterInstallDate;
            if (input.ElectricMeterInstallDate != null)
                model.ElectricMeterInstallByUserID = UserID;


            //วันที่ติดตั้งมิเตอร์น้ำ
            model.WaterMeterInstallDate = input.WaterMeterInstallDate;
            if (input.WaterMeterInstallDate != null)
                model.WaterMeterInstallByUserID = UserID;


            //รอบการอ่าน Meter ไฟ
            model.ElectricMeterBillingCycle = input.ElectricMeterBillingCycle;
            if(input.ElectricMeterBillingCycle != null || input.ElectricMeterBillingCycle > 0)
                model.ElectricMeterBillingCycleByUserID = UserID;


            //รอบการอ่าน Meter น้ำ
            model.WaterMeterBillingCycleDate = input.WaterMeterBillingCycleDate;
            if (input.WaterMeterBillingCycleDate != null || input.WaterMeterBillingCycleDate > 0)
                model.WaterMeterBillingCycleByUserID = UserID;


            //จำหน่ายออกจากกองจัดเก็บ ไฟ
            model.IsElectricMeterTerminate = input.IsElectricMeterTerminate;
            model.ElectricMeterTerminateDate = input.ElectricMeterTerminateDate;
            if(input.IsElectricMeterTerminate.HasValue && model.ElectricMeterTerminateDate != null)
                model.ElectricMeterTerminateByUserID = UserID;


            //จำหน่ายออกจากกองจัดเก็บ น้ำ
            model.IsWaterMeterTerminate = input.IsWaterMeterTerminate;
            model.WaterMeterTerminateDate = input.WaterMeterTerminateDate;
            if (input.IsWaterMeterTerminate.HasValue && model.WaterMeterTerminateDate != null)
                model.WaterMeterTerminateByUserID = UserID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetUnitMeterAsync(unitID);
            return result;
        }

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
            var dt = await this.ConvertExcelToDataTable(input);
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
                var project = projects.FirstOrDefault(o => o.ProjectNo == item.ProjectNo);
                if (project != null)
                {
                    var unit = units.FirstOrDefault(x => x.ProjectID == project.ID && x.UnitNo == item.UnitNo);
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

        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137937/preview
        /// Sample File: http://192.168.2.29:9001/xunit-tests/UnitMeter.xls
        /// </summary>
        /// <returns>The unit meter excel async.</returns>
        /// <param name="input">Input.</param>
        public async Task<FileDTO> ExportUnitMeterExcelAsync(UnitMeterFilter filter, UnitMeterListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            ExportExcel result = new ExportExcel();
            List<Database.Models.PRJ.Project> projects = new List<Database.Models.PRJ.Project>();
            if (!string.IsNullOrEmpty(filter.ProjectIDs))
            {
                var projectIds = filter.ProjectIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                projects = await DB.Projects.Where(o => projectIds.Contains(o.ID)).ToListAsync(cancellationToken);
            }
            else
            {
                projects = await DB.Projects.ToListAsync(cancellationToken);
            }
            var projectsId = projects.Select(o => o.ID).ToList();
            var units = await DB.Units.Where(o => projectsId.Contains(o.ProjectID.Value))
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.Model)
                                      .Include(o => o.ElectrictMeterStatus)
                                      .Include(o => o.WaterMeterStatus)
                                      .Include(o => o.WaterMeterTopic)
                                      .Include(o => o.ElectricMeterTopic)
                                      .ToListAsync(cancellationToken);

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
            var dt = await this.ConvertExcelToDataTable(input);
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
        public async Task<FileDTO> ExportUnitMeterStatusExcelAsync(UnitMeterFilter filter, UnitMeterListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            ExportExcel result = new ExportExcel();
            List<Database.Models.PRJ.Project> projects = new List<Database.Models.PRJ.Project>();
            if (!string.IsNullOrEmpty(filter.ProjectIDs))
            {
                var projectIds = filter.ProjectIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                projects = await DB.Projects.Where(o => projectIds.Contains(o.ID)).ToListAsync(cancellationToken);
            }
            else
            {
                projects = await DB.Projects.ToListAsync(cancellationToken);
            }
            var projectsId = projects.Select(o => o.ID).ToList();
            var units = await DB.Units.Where(o => projectsId.Contains(o.ProjectID.Value))
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.Model)
                                      .Include(o => o.ElectrictMeterStatus)
                                      .Include(o => o.WaterMeterStatus)
                                      .Include(o => o.WaterMeterTopic)
                                      .Include(o => o.ElectricMeterTopic)
                                      .ToListAsync(cancellationToken);

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
    }
}
