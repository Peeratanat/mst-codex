using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.PRJ;
using ErrorHandling;
using ExcelExtensions;
using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Inputs;
using PRJ_Project.Params.Outputs;
using PRJ_Project.Services.Excels;
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
namespace PRJ_Project.Services
{
    public class FloorService : IFloorService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }
        private FileHelper FileHelper;

        private readonly IConfiguration Configuration;
        public FloorService(IConfiguration configuration, DatabaseContext db)
        {
            logModel = new LogModel("FloorService", null);
            this.DB = db;

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
            Configuration = configuration;
        }
        public FloorService(DatabaseContext db)
        {
            logModel = new LogModel("FloorService", null);
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

        public async Task<List<FloorDropdownDTO>> GetFloorDropdownListAsync(Guid projectID, Guid? towerID, string name, CancellationToken cancellationToken = default)
        {
            IQueryable<Floor> query = DB.Floors.AsNoTracking().Where(x => x.ProjectID == projectID);
            if (towerID != null)
            {
                query = query.Where(o => o.TowerID == towerID);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }
            var results = await query.OrderBy(o => o.NameTH).Take(100)
             .Select(o => FloorDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }
        public async Task<List<FloorDropdownDTO>> GetFloorEventBookingDropdownListAsync(Guid projectID, Guid? towerID, string name, CancellationToken cancellationToken = default)
        {
            IQueryable<Floor> query = DB.Floors.AsNoTracking().Where(x => x.ProjectID == projectID);
            if (towerID != null)
            {
                query = query.Where(o => o.TowerID == towerID);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }

            var queryResults = await query.ToListAsync(cancellationToken);
            var results = queryResults.Select(o => FloorDropdownDTO.CreateFromModel(o)).OrderByDescending(o => o.NameTH).ToList();
            return results;
        }
        public async Task<FloorPaging> GetFloorListAsync(Guid projectID, Guid towerID, FloorsFilter filter, PageParam pageParam, FloorSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<FloorQueryResult> query = DB.Floors.AsNoTracking().Where(o => o.TowerID == towerID).Select(o => new FloorQueryResult
            {
                Floor = o,
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrWhiteSpace(filter.NameTH))
            {
                query = query.Where(x => x.Floor.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrWhiteSpace(filter.NameEN))
            {
                query = query.Where(x => x.Floor.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                query = query.Where(x => x.Floor.Description.Contains(filter.Description));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Floor.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Floor.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Floor.Updated >= filter.UpdatedFrom && x.Floor.Updated <= filter.UpdatedTo);
            }
            if (filter.DueTransferDateFrom != null && filter.DueTransferDateTo != null)
            {
                query = query.Where(x => x.Floor.DueTransferDate >= filter.DueTransferDateFrom && x.Floor.DueTransferDate <= filter.DueTransferDateTo);
            }
            #endregion

            FloorDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<FloorQueryResult>(pageParam, ref query);

            var results = await query.Select(o => FloorDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new FloorPaging()
            {
                PageOutput = pageOutput,
                Floors = results
            };
        }

        public async Task<FloorDTO> GetFloorAsync(Guid projectID, Guid towerID, Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Floors.Include(o => o.UpdatedBy).FirstAsync(o => o.ProjectID == projectID && o.TowerID == towerID && o.ID == id, cancellationToken);
            var result = FloorDTO.CreateFromModel(model);
            return result;
        }

        public async Task<FloorDTO> CreateFloorAsync(Guid projectID, Guid towerID, FloorDTO input)
        {
            await this.ValidateFloor(projectID, towerID, input);

            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            Floor model = new Floor();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            model.TowerID = towerID;

            await DB.Floors.AddAsync(model);
            await DB.SaveChangesAsync();

            var towerDataStatusMasterCenterID = await this.TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetFloorAsync(projectID, towerID, model.ID);
            return result;
        }

        public async Task<List<FloorDTO>> CreateMultipleFloorAsync(Guid projectID, Guid towerID, CreateMultipleFloorInput input)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            List<FloorDTO> result = new List<FloorDTO>();
            for (var i = input.From; i <= input.To; i++)
            {
                Floor model = new Floor();
                model.ProjectID = projectID;
                model.TowerID = towerID;
                model.NameEN = i.ToString("00");
                model.NameTH = i.ToString("00");
                await DB.Floors.AddAsync(model);
                await DB.SaveChangesAsync();
                result.Add(await this.GetFloorAsync(projectID, towerID, model.ID));
            }
            var towerDataStatusMasterCenterID = await this.TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
            return result;
        }

        public async Task<FloorDTO> UpdateFloorAsync(Guid projectID, Guid towerID, Guid id, FloorDTO input)
        {
            await this.ValidateFloor(projectID, towerID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.ID == id).FirstAsync();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            model.TowerID = towerID;

            DB.Entry(model).State = EntityState.Modified;

            await DB.SaveChangesAsync();

            var towerDataStatusMasterCenterID = await this.TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
            var result = FloorDTO.CreateFromModel(model);
            return result;
        }

        public async Task<Floor> DeleteFloorAsync(Guid projectID, Guid towerID, Guid id)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            var towerDataStatusMasterCenterID = await this.TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            return model;
        }

        private async Task<Guid> TowerDataStatus(Guid projectID)
        {
            var towers = await DB.Towers.Where(o => o.ProjectID == projectID).ToListAsync();
            var floors = await DB.Floors.Where(o => o.ProjectID == projectID).ToListAsync();
            var towerDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
            var towerDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstAsync();

            var towerDataStatusMasterCenterID = towerDataStatusPrepareMasterCenterID;

            if (towers.Count() == 0 || floors.Count() == 0)
            {
                return towerDataStatusMasterCenterID;
            }

            if (towers.TrueForAll(o =>
                     !string.IsNullOrEmpty(o.TowerCode)
                     && !string.IsNullOrEmpty(o.TowerNoTH)
                     && !string.IsNullOrEmpty(o.TowerNoEN)
                     && !string.IsNullOrEmpty(o.CondominiumName)
                     && !string.IsNullOrEmpty(o.CondominiumNo)
                    )
                    && floors.TrueForAll(o =>
                        !string.IsNullOrEmpty(o.NameTH)
                      && !string.IsNullOrEmpty(o.NameEN)
                       )
              )
            {

                towerDataStatusMasterCenterID = towerDataStatusSaleMasterCenterID;
            }
            return towerDataStatusMasterCenterID;
        }

        private async Task ValidateFloor(Guid projectID, Guid towerID, FloorDTO input)
        {
            ValidateException ex = new ValidateException();
            //validate unique
            if (!string.IsNullOrEmpty(input.NameTH))
            {
                if (!input.NameTH.CheckLang(true, true, false, false))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0031").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(FloorDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueNameTH = input.Id != (Guid?)null
               ? await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.ID != input.Id && o.NameTH == input.NameTH).CountAsync() > 0
               : await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.NameTH == input.NameTH).CountAsync() > 0;
                if (checkUniqueNameTH)
                {
                    var errMsg = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0042");
                    string desc = input.GetType().GetProperty(nameof(FloorDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(input.NameEN))
            {
                if (!input.NameEN.CheckLang(false, true, false, false))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(FloorDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueNameEN = input.Id != (Guid?)null
               ? await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.ID != input.Id && o.NameEN == input.NameEN).CountAsync() > 0
               : await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.NameEN == input.NameEN).CountAsync() > 0;
                if (checkUniqueNameEN)
                {
                    var errMsg = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0042");
                    string desc = input.GetType().GetProperty(nameof(FloorDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.NameEN);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task<FileDTO> ExportExcelFloorAsync(Guid projectID, Guid towerID, FloorsFilter filter, FloorSortByParam sortByParam)
        {
            ExportExcel result = new ExportExcel();

            IQueryable<FloorQueryResult> query = DB.Floors.AsNoTracking().Where(o => o.ProjectID == projectID)
                                                                 .Include(o => o.Tower)
                                                         .Select(o => new FloorQueryResult
                                                         {
                                                             Tower = o.Tower,
                                                             Floor = o
                                                         }).Where(x => x.Floor.TowerID == towerID);


            #region Filter
            if (!string.IsNullOrWhiteSpace(filter.NameTH))
            {
                query = query.Where(x => x.Floor.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrWhiteSpace(filter.NameEN))
            {
                query = query.Where(x => x.Floor.NameEN.Contains(filter.NameEN));
            }
            if (filter.DueTransferDateFrom != null && filter.DueTransferDateTo != null)
            {
                query = query.Where(x => x.Floor.DueTransferDate >= filter.DueTransferDateFrom && x.Floor.DueTransferDate <= filter.DueTransferDateTo);
            }
            #endregion

            FloorDTO.SortBy(sortByParam, ref query);

            var data = await query.ToListAsync();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_Floors.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _towerCodeIndex = FloorExcelModel._towerCodeIndex + 1;
                int _floorNameTHIndex = FloorExcelModel._floorNameTHIndex + 1;
                int _floorNameENIndex = FloorExcelModel._floorNameENIndex + 1;
                int _dueTransferDateIndex = FloorExcelModel._dueTransferDateIndex + 1;

                for (int c = 2; c < data.Count + 2; c++)
                {
                    worksheet.Cells[c, _towerCodeIndex].Value = data[c - 2].Tower?.TowerCode;
                    worksheet.Cells[c, _floorNameTHIndex].Value = data[c - 2].Floor?.NameTH;
                    worksheet.Cells[c, _floorNameENIndex].Value = data[c - 2].Floor?.NameEN;

                    //worksheet.Cells[c, _dueTransferDateIndex].Style.Numberformat.Format = "dd/mm/yyyy";
                    if (data[c - 2].Floor?.DueTransferDate != null)
                    {
                        //worksheet.Cells[c, _dueTransferDateIndex].Style.Numberformat.Format = "dd/mm/yyyy";
                        worksheet.Cells[c, _dueTransferDateIndex].Value = (data[c - 2].Floor?.DueTransferDate ?? new DateTime()).ToString("dd/MM/yyyy");
                    }
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                var Project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
                result.FileName = Project.ProjectNo + "_Floors.xlsx";
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

        public async Task<FloorExcelDTO> ImportFloorAsync(Guid projectID, Guid towerID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0061");

            // FormatDate
            var err0071 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0071");

            // Not Found
            var err0062 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0062");


            var result = new FloorExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>() };
            var towers = await DB.Towers.Where(o => o.ProjectID == projectID && o.ID == towerID && o.IsDeleted == false).ToListAsync();
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 4)
            {
                throw new Exception("Invalid File Format");
            }
            var row = 2;
            var error = 0;
            var checkTowerNotFounds = new List<string>();

            var checkNullTowerCode = new List<string>();
            var checkNullNameTH = new List<string>();
            var checkNullNameEH = new List<string>();
            var checkFormateDueTransferDates = new List<string>();

            //Read Excel Model
            var floorExcelModel = new List<FloorExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = FloorExcelModel.CreateFromDataRow(r);
                floorExcelModel.Add(excelModel);

                #region Validate
                var tower = towers.Find(o => o.ProjectID == projectID && o.TowerCode == excelModel.TowerCode);
                if (tower == null)
                {
                    checkTowerNotFounds.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.TowerCode))
                {
                    checkNullTowerCode.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.FloorNameTH))
                {
                    checkNullNameTH.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.FloorNameEN))
                {
                    checkNullNameEH.Add((row).ToString());
                    isError = true;
                }
                //if (!string.IsNullOrEmpty(r[FloorExcelModel._dueTransferDateIndex].ToString()))
                //{
                //    if (!r[FloorExcelModel._dueTransferDateIndex].ToString().isFormatDate())
                //    {
                //        checkFormateDueTransferDates.Add((row).ToString());
                //        isError = true;
                //    }
                //}
                #endregion

                if (isError)
                {
                    error++;
                }
                row++;

            }

            #region Validate Tower
            var towerV = await DB.Towers.Where(o => o.ID == towerID).FirstOrDefaultAsync();
            ValidateException ex = new ValidateException();
            if (floorExcelModel.Any(o => o.TowerCode != towerV.TowerCode && towerV.TowerCode != null))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0171").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "รหัสตึก");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            #endregion

            #region Add Result ErrorMassage
            if (checkNullTowerCode.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "รหัสตึก");
                    msg = msg.Replace("[row]", String.Join(",", checkNullTowerCode));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "รหัสตึก");
                    msg = msg.Replace("[row]", String.Join(",", checkNullTowerCode));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkNullNameTH.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ชื่อชั้นTH");
                    msg = msg.Replace("[row]", String.Join(",", checkNullNameTH));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ชื่อชั้นTH");
                    msg = msg.Replace("[row]", String.Join(",", checkNullNameTH));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkNullNameEH.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ชื่อชั้นEH");
                    msg = msg.Replace("[row]", String.Join(",", checkNullNameEH));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ชื่อชั้นEH");
                    msg = msg.Replace("[row]", String.Join(",", checkNullNameEH));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormateDueTransferDates.Any())
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0071.Message.Replace("[column]", "วันที่โอนกรรมสิทธิ์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormateDueTransferDates));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0071.Message.Replace("[column]", "วันที่โอนกรรมสิทธิ์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormateDueTransferDates));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region RowErrors
            var rowErrors = new List<string>();
            rowErrors.AddRange(checkNullTowerCode);
            rowErrors.AddRange(checkNullNameTH);
            rowErrors.AddRange(checkNullNameEH);
            rowErrors.AddRange(checkFormateDueTransferDates);
            #endregion


            var floors = await DB.Floors.Where(o => o.ProjectID == projectID).ToListAsync();

            List<Floor> floorsCreate = new List<Floor>();
            List<Floor> floorsUpdate = new List<Floor>();
            //Update Data
            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            row = 2;

            foreach (var item in floorExcelModel)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var tower = towers.Where(o => o.ProjectID == projectID && o.TowerCode == item.TowerCode).FirstOrDefault();
                    if (tower != null)
                    {
                        var existedfloor = floors.Where(x => x.ProjectID == projectID
                                           && x.TowerID == tower.ID && x.NameTH == item.FloorNameTH && x.NameEN == item.FloorNameEN).FirstOrDefault();
                        if (existedfloor == null)
                        {
                            Floor floor = new Floor();
                            item.ToModel(ref floor);
                            floor.ProjectID = projectID;
                            floor.TowerID = tower.ID;
                            floor.NameTH = item.FloorNameTH;
                            floor.NameEN = item.FloorNameEN;
                            result.Success++;
                            floorsCreate.Add(floor);
                        }
                        else
                        {
                            item.ToModel(ref existedfloor);
                            existedfloor.ProjectID = projectID;
                            existedfloor.TowerID = tower.ID;
                            existedfloor.NameTH = item.FloorNameTH;
                            existedfloor.NameEN = item.FloorNameEN;
                            result.Success++;
                            floorsUpdate.Add(existedfloor);
                        }
                    }
                }
                row++;
            }
            await DB.Floors.AddRangeAsync(floorsCreate);
            DB.UpdateRange(floorsUpdate);
            await DB.SaveChangesAsync();
            result.Error = error;
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
    }
}
