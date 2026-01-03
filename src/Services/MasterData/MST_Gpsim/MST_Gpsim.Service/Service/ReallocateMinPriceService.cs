using Base.DTOs;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Common;
using Common.Helper.Logging;
using Dapper;
using Database.Models;
using Database.Models.DbQueries;
using Database.Models.DbQueries.Finance;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.ROI;
using Database.Models.SAL;
using Database.Models.USR;
using ErrorHandling;
using ExcelExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Minio.DataModel;
using MST_Gpsim.Params.Filters;
using MST_Gpsim.Params.Outputs;
using NPOI.SS.Util;
using NPOI.Util;
using OfficeOpenXml;
using PagingExtensions;
using Report.Integration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Base.DTOs.MST.QuestionFAQDTO;
using static Database.Models.DbQueries.DBQueryParam;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MST_Gpsim.Services
{
    public class ReallocateMinPriceService : IReallocateMinPriceService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }
        private FileHelper FileHelper;

        int Timeout = 300;
        public ReallocateMinPriceService(DatabaseContext db)
        {
            logModel = new LogModel("ReallocateMinPriceService", null);
            DB = db;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Environment.GetEnvironmentVariable("DBConnectionString"));
            int Timeout = builder.ConnectTimeout;
            DB.Database.SetCommandTimeout(Timeout);

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");
            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }

        public async Task<ReallocateMinPricePaging> GetReallocateMinPriceListAsync(ReallocateMinPriceFilter filter, PageParam pageParam, ReallocateMinPriceSortByParam sortByParam, Guid? userID, CancellationToken cancellationToken = default)
        {
            var GPSysnc = await DB.GPSyncOriginals.OrderByDescending(o => o.LogSyncDate).FirstOrDefaultAsync();
            var query = from v in DB.GPVersions.Where(o => o.GPVersionType.Equals("Reallocate") && o.GPSyncOriginalID == GPSysnc.ID)
                        join ps in DB.Projects on v.ProjectID equals ps.ID into p
                        from pm in p.DefaultIfEmpty()
                        join u in DB.Users on v.CreatedByUserID equals u.ID into u
                        from um in u.DefaultIfEmpty()
                        join u2 in DB.Users on v.UpdatedByUserID equals u2.ID into u2
                        from um2 in u2.DefaultIfEmpty()
                        select new GPReallocateMinPriceQueryResult
                        {
                            GPVersion = v,
                            Projetc = pm,
                            CreatedBy = um,
                            UpdatedBy = um2
                        };

            #region Filter
            if (!string.IsNullOrEmpty(filter.Version))
                query = query.Where(o => o.GPVersion.VersionCode.Contains(filter.Version));

            if (!string.IsNullOrEmpty(filter.Remark))
                query = query.Where(o => o.GPVersion.VersionRemark.Contains(filter.Remark));

            if (filter.ProjectID != null)
                query = query.Where(o => o.GPVersion.ProjectID == filter.ProjectID);

            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
                query = query.Where(o => o.GPVersion.Updated >= filter.UpdatedFrom && o.GPVersion.Updated <= filter.UpdatedTo);

            if (filter.CreatedFrom != null && filter.CreatedTo != null)
                query = query.Where(o => o.GPVersion.Created >= filter.CreatedFrom && o.GPVersion.Created <= filter.CreatedTo);

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
                query = query.Where(o => ((o.UpdatedBy.DisplayName != null) ? o.UpdatedBy.DisplayName : o.CreatedBy.DisplayName).Contains(filter.UpdatedBy));
            if (userID != null)
            {
                var userProject = await DB.UserAuthorizeProjects.Where(o => o.UserID == userID).Select(o => o.ProjectID).ToListAsync(cancellationToken);
                query = query.Where(q => userProject.Contains(q.GPVersion.ProjectID));
            }
            #endregion 
            ReallocateMinPriceDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<GPReallocateMinPriceQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();
            var result = queryResults.Select(o => ReallocateMinPriceDTO.CreateFromQueryResult(o)).ToList();
            return new ReallocateMinPricePaging()
            {
                PageOutput = pageOutput,
                ReallocateMinPriceDTOs = result
            };
        }
        public async Task<ReallocateMinPriceDTO> GetGPOriginalAsync(Guid ProjectID, CancellationToken cancellationToken = default)
        {
            var project = await DB.Projects.Include(x => x.BG).Include(x => x.ProductType).Where(o => o.ID == ProjectID).FirstOrDefaultAsync(cancellationToken);
            // sap ล่าสุดของ project
            var lastSapSync = await DB.GPSyncOriginals.OrderByDescending(o => o.LogSyncDate).FirstOrDefaultAsync(cancellationToken);
            var gpOriginalQuery = await (from op in DB.GPOriginalProjects.Where(e => e.ProjectID == ProjectID)
                                         join ous in DB.GPOriginalUnits on op.ProjectID equals ous.ProjectID into ou
                                         from oum in ou.DefaultIfEmpty()
                                         join us in DB.Units.Include(x => x.UnitStatus) on oum.UnitID equals us.ID into u
                                         from um in u.DefaultIfEmpty()
                                         join us in DB.VWUnitStatus on oum.UnitID equals us.UnitID into u2
                                         from um2 in u2.DefaultIfEmpty()
                                         select new GPReallocateMinPriceQuery { GPOriginalProject = op, GPOriginalUnit = oum, Unit = um, UnitStatus = um2 }).ToListAsync(cancellationToken);
            // GP Version ล่าสุด ของ project
            // Status = 1 draft , 2 Calculate
            var LastGPVersion = await DB.GPVersions.Where(o => o.ProjectID == ProjectID && o.GPVersionType.Equals("Reallocate")).OrderByDescending(o => o.VersionCode).FirstOrDefaultAsync(cancellationToken);
            GPProject gpProject = null;
            List<GPUnit> gpUnit = null;
            Guid? RefGPVersion = null;
            if (LastGPVersion != null) // มีข้อมูล GP Version
            {
                RefGPVersion = LastGPVersion.ID;
                // ถ้า draft อยู่ ให้ดึงข้อมูล Original ล่าสุดมาเทียบ
                var lastSapSyncDate = lastSapSync.LogSyncDate.GetValueOrDefault();
                lastSapSyncDate = new DateTime(lastSapSyncDate.Year, lastSapSyncDate.Month, lastSapSyncDate.Day, lastSapSyncDate.Hour, lastSapSyncDate.Minute, lastSapSyncDate.Second);
                var LastGPVersionDate = LastGPVersion.SyncDate.GetValueOrDefault();
                LastGPVersionDate = new DateTime(LastGPVersionDate.Year, LastGPVersionDate.Month, LastGPVersionDate.Day, LastGPVersionDate.Hour, LastGPVersionDate.Minute, LastGPVersionDate.Second);
                if (LastGPVersion.Status == 2
                    && lastSapSyncDate > LastGPVersionDate)
                {
                    LastGPVersion = null;
                }
                if (LastGPVersion != null)
                {
                    gpProject = await DB.GPProjects.Where(o => o.GPVersionID == LastGPVersion.ID).FirstOrDefaultAsync(cancellationToken);
                    gpUnit = await DB.GPUnits.Where(o => o.GPVersionID == LastGPVersion.ID).ToListAsync();
                }
            }

            // Roi ล่าสุด ของ project

            var result = ReallocateMinPriceDTO.CreateFromQuery(LastGPVersion, gpProject, gpUnit, gpOriginalQuery, project, lastSapSync);
            result.RefGPVersion = RefGPVersion;
            return result;
        }
        public async Task<ReallocateMinPriceDTO> SaveDraftReallocateMinPriceAsync(ReallocateMinPriceDTO input)
        {
            if (input.Id == null)
            {
                input = await NewGPversion(input);
            }
            else
            {
                var gpVersion = await DB.GPVersions.Where(o => o.ID == input.Id).AsNoTracking().FirstOrDefaultAsync();
                if (gpVersion.Status == 2)
                {
                    input = await NewGPversion(input);
                }
                else
                {
                    input = await UpdateGPVersion(input);
                }
                input.Id = gpVersion.ID;
            }
            return input;
        }
        public async Task CalReallocateMinPriceAsync(Guid? VersionID)
        {
            var connection = DB.Database.GetDbConnection();
            var wasOpen = connection.State == ConnectionState.Open;

            // Ensure connection is open
            if (!wasOpen)
            {
                await connection.OpenAsync();
            }

            try
            {
                DynamicParameters ParamList = new();
                ParamList.Add("Version", VersionID);

                var commandDefinition1 = new CommandDefinition(
                    commandText: DBStoredNames.spROIGPCal1,
                    parameters: ParamList,
                    commandTimeout: Timeout,
                    transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                    commandType: CommandType.StoredProcedure
                );

                Console.WriteLine("CalReallocateMinPriceAsync 1: " + DateTime.Now);
                var res = await connection.QueryFirstOrDefaultAsync<int?>(commandDefinition1);
                Console.WriteLine("CalReallocateMinPriceAsync 2: " + DateTime.Now);

                var commandDefinition2 = new CommandDefinition(
                    commandText: DBStoredNames.spROIGPCal2,
                    parameters: ParamList,
                    commandTimeout: Timeout,
                    transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                    commandType: CommandType.StoredProcedure
                );

                var res2 = await connection.QueryFirstOrDefaultAsync<int?>(commandDefinition2);
                Console.WriteLine("CalReallocateMinPriceAsync 3: " + DateTime.Now);
            }
            finally
            {
                // Close connection if it wasn't open before
                if (!wasOpen && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var gpVersion = await DB.GPVersions.FirstOrDefaultAsync(x => x.ID == VersionID);
                    if (gpVersion != null)
                    {
                   
                    gpVersion.Status = 2;

                    DB.GPVersions.Update(gpVersion);
                    await DB.SaveChangesAsync();
                    await tran.CommitAsync();
                    }
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        public async Task<ReallocateMinPriceDTO> GetReallocateMinPriceAsync(Guid versionID, CancellationToken cancellationToken = default)
        {
            var LastGPVersion = await DB.GPVersions.Where(o => o.ID == versionID && o.GPVersionType.Equals("Reallocate")).OrderByDescending(o => o.Updated).FirstOrDefaultAsync(cancellationToken);
            var project = await DB.Projects.Include(x => x.BG).Include(x => x.ProductType).Where(o => o.ID == LastGPVersion.ProjectID).FirstOrDefaultAsync(cancellationToken);

            var gpOriginalQuery = await (from op in DB.GPOriginalProjects.Where(e => e.ProjectID == LastGPVersion.ProjectID)
                                         join ous in DB.GPOriginalUnits on op.ProjectID equals ous.ProjectID into ou
                                         from oum in ou.DefaultIfEmpty()
                                         join us in DB.Units.Include(x => x.UnitStatus) on oum.UnitID equals us.ID into u
                                         from um in u.DefaultIfEmpty()
                                         join us in DB.VWUnitStatus on oum.UnitID equals us.UnitID into u2
                                         from um2 in u2.DefaultIfEmpty()
                                         select new GPReallocateMinPriceQuery { GPOriginalProject = op, GPOriginalUnit = oum, Unit = um, UnitStatus = um2 }).ToListAsync(cancellationToken);

            GPProject gpProject = null;
            List<GPUnit> gpUnit = null;
            Guid? RefGPVersion = null;
            RefGPVersion = LastGPVersion.RefGPVersion;
            gpProject = await DB.GPProjects.Where(o => o.GPVersionID == LastGPVersion.ID).FirstOrDefaultAsync(cancellationToken);
            gpUnit = await DB.GPUnits.Where(o => o.GPVersionID == LastGPVersion.ID).ToListAsync(cancellationToken);

            var result = ReallocateMinPriceDTO.CreateFromQuery(LastGPVersion, gpProject, gpUnit, gpOriginalQuery, project);
            //check last cal version can print
            var lastCalVersion = await DB.GPVersions.Where(x => x.ProjectID == LastGPVersion.ProjectID && x.GPVersionType.Equals("Reallocate") && x.Status == 2).OrderByDescending(x => x.VersionCode).FirstOrDefaultAsync(cancellationToken);
            result.IsCanPrint = false;
            if (lastCalVersion != null && lastCalVersion.ID == LastGPVersion.ID)
            {
                result.IsCanPrint = true;
            }
            result.RefGPVersion = RefGPVersion;
            return result;
        }
        public async Task<bool> DeleteReallocateMinPriceAsync(Guid? id)
        {
            var model = await DB.GPVersions.FindAsync(id);
            if (model != null)
            {
                model.IsDeleted = true;
                DB.GPVersions.Update(model);
                await DB.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<GPUnitImportDTO> ImportReallocateMinPriceAsync(FileDTO fileDTO, Guid? projectID, Guid? userID)
        {
            GPUnitImportDTO result = new GPUnitImportDTO();
            List<GPUnitDTO> resultList = new List<GPUnitDTO>();
            var dt = await ConvertExcelToDataTable(fileDTO);
            int i = 1;
            var projectModel = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
            if (projectModel == null)
            {
                throw new Exception("ไม่พบข้อมูลโครงการ");
            }
            foreach (DataRow r in dt.Rows)
            {
                var excelModel = await CreateGPUnitFromDataRow(r);
                i++;
                resultList.Add(excelModel);
            }
            var ReallocateMinPrice = await GetGPOriginalAsync(projectID.GetValueOrDefault());
            var gpOriginalQuery = ReallocateMinPrice.GPUnitDTOs;
            resultList = resultList.Where(x => x.MinPrice.HasValue || x.IsError == true).ToList();
            foreach (var item in resultList)
            {
                if (item.IsError == true && string.IsNullOrEmpty(item.Unit.UnitNo))
                {
                    continue;
                }
                var unit = gpOriginalQuery.Find(x => item.Unit.UnitNo.Equals(x.Unit.UnitNo));
                if (unit != null)
                {
                    item.GPVersionID = unit.GPVersionID;
                    item.UnitID = unit.UnitID;
                    item.WBSBlock = unit.WBSBlock;
                    item.BlockNumber = unit.BlockNumber;
                    item.BudgetLI = unit.BudgetLI;
                    item.BudgetCO01 = unit.BudgetCO01;
                    item.Budget_CO01_Block = unit.Budget_CO01_Block;
                    item.BudgetCOC1 = unit.BudgetCOC1;
                    item.Budget_CO_A1 = unit.Budget_CO_A1;
                    item.Budget_UT = unit.Budget_UT;
                    item.Budget_AC = unit.Budget_AC;
                    item.NetPrice = unit.NetPrice;
                    item.PercentGPNew = unit.PercentGPNew;
                    item.PriceCommit = unit.PriceCommit;
                    item.Ori_COGS_LD = unit.Ori_COGS_LD;
                    item.Ori_COGS_LI = unit.Ori_COGS_LI;
                    item.Ori_COGS_CO = unit.Ori_COGS_CO;
                    item.Ori_Budget_CO01 = unit.Ori_Budget_CO01;
                    item.Ori_WIP_COC1 = unit.Ori_WIP_COC1;
                    item.Ori_COGS_UT = unit.Ori_COGS_UT;
                    item.Ori_COGS_AC = unit.Ori_COGS_AC;
                    item.Ori_NetPrice = unit.Ori_NetPrice;
                    item.Ori_PercentGPCommit = unit.Ori_PercentGPCommit;
                    item.Ori_MinPrice = unit.Ori_MinPrice;
                    item.Unit = unit.Unit;
                    item.UnitStatus = unit.UnitStatus;
                    if (item.UnitStatus.Equals("TF") && item.MinPrice.GetValueOrDefault() > 0)
                    {
                        item.IsError = true;
                        item.ErrorMessage = "ห้องโอนแล้ว ไม่สามารถแก้ไขได้";
                    }
                }
                else
                {
                    item.IsError = true;
                    item.ErrorMessage = "ไม่พบข้อมูล Unit No";
                }
            }

            result.ImportUnitList = resultList;
            result.Total = resultList.Count();
            result.Valid = resultList.Where(x => x.IsError == false).Count();
            result.Invalid = resultList.Where(x => x.IsError == true).Count();
            return result;
        }
        public async Task<FileDTO> ExportTemplateReallocateMinPrice(Guid ProjectID)
        {
            ExportExcel result = new ExportExcel();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "TemplateReallocate.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);


            using (MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                var project = await DB.Projects.Include(x => x.BG).Include(x => x.SubBG).Where(o => o.ID == ProjectID).FirstOrDefaultAsync();
                worksheet.Cells[2, 2].Value = project.ProjectNo;
                worksheet.Cells[2, 3].Value = project.ProjectNameTH;
                worksheet.Cells[3, 2].Value = project.SubBG?.Name;
                int nRow = 7;

                var ReallocateMinPrice = await GetGPOriginalAsync(ProjectID);
                var gpOriginalQuery = ReallocateMinPrice.GPUnitDTOs;

                gpOriginalQuery = gpOriginalQuery.Where(x => !x.UnitStatus.Equals("TF")).ToList();
                foreach (var modal in gpOriginalQuery)
                {
                    worksheet.Cells[nRow, 1].Value = modal.Unit?.SapwbsNo;
                    worksheet.Cells[nRow, 2].Value = modal.Unit?.UnitNo;
                    if (modal.UnitStatus != null)
                    {
                        var unitStatus = "";
                        if (modal.UnitStatus.Equals("TF"))
                        {
                            unitStatus = "โอน";
                        }
                        else if (modal.UnitStatus.Equals("AG"))
                        {
                            unitStatus = "สัญญา";
                        }
                        else if (modal.UnitStatus.Equals("BK"))
                        {
                            unitStatus = "จอง";
                        }
                        else
                        {
                            unitStatus = "ว่าง";
                        }

                        worksheet.Cells[nRow, 3].Value = unitStatus;
                    }
                    worksheet.Cells[nRow, 4].Value = modal.Ori_MinPrice.GetValueOrDefault().ToString("0.00");
                    nRow++;
                }

                result.FileContent = package.GetAsByteArray();
                result.FileName = "TemplateReallocate_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = result.FileName;
            string contentType = result.FileType;
            string filePath = $"RoiAndGp/";
            var minioBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var uploadResult = await FileHelper.UploadFileFromStreamWithOutGuid(fileStream, minioBucketName, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = fileName,
                Url = uploadResult.Url
            };
        }
        public ReportResult PrintReallocateMinPrice(Guid? VersionID)
        {
            ReportFactory reportFactory = new ReportFactory(ReportFolder.GP, "GP_Reallocate");
            reportFactory.AddParameter("@VersionID", VersionID);
            ReportResult report = reportFactory.CreateUrl();
            return report;
        }
        private async Task<ReallocateMinPriceDTO> NewGPversion(ReallocateMinPriceDTO input)
        {
            var now = DateTime.Now;
            var gpVersion = new GPVersion();
            var GpVersionCode = await GetGPVersionCode();
            gpVersion.VersionCode = GpVersionCode;
            gpVersion.VersionRemark = input.Remark;
            gpVersion.GPVersionType = "Reallocate";
            gpVersion.ProjectID = input.Project.Id;
            gpVersion.Y = now.Year;
            gpVersion.Q = (now.Month - 1) / 3 + 1;
            gpVersion.M = now.Month;
            gpVersion.Status = 1;
            gpVersion.SyncDate = input.SyncDate;
            gpVersion.GPSyncOriginalID = input.GPSyncOriginalID;
            gpVersion.RefGPVersion = input.RefGPVersion;
            DB.GPVersions.Add(gpVersion);
            await DB.SaveChangesAsync();
            input.Id = gpVersion.ID;
            // version project 
            //Console.WriteLine("1" + DateTime.Now);
            var gpProject = new GPProject()
            {
                GPVersionID = gpVersion.ID,
                BudgetLI = input.GPProjectDTO.BudgetLI,
                BudgetCO01 = input.GPProjectDTO.BudgetCO01,
                BudgetCOA1 = input.GPProjectDTO.BudgetCOA1,
                BudgetCOC1 = input.GPProjectDTO.BudgetCOC1,
                BudgetUT = input.GPProjectDTO.BudgetUT,
                BudgetAC = input.GPProjectDTO.BudgetAC,
                PercentGPCommit = input.GPProjectDTO.PercentGPCommit,
                NetPrice = input.GPProjectDTO.NetPrice,
                PriceCommit = input.GPProjectDTO.PriceCommit,
                OriCOGSLD = input.GPProjectDTO.OriCOGSLD,
                OriCOGSLI = input.GPProjectDTO.OriCOGSLI,
                OriCOGSCO = input.GPProjectDTO.OriCOGSCO,
                OriWIPCOC1 = input.GPProjectDTO.OriWIPCOC1,
                OriBudgetCO01 = input.GPProjectDTO.OriBudgetCO01,
                OriBudgetCOP1 = input.GPProjectDTO.OriBudgetCOP1,
                OriCOGSUT = input.GPProjectDTO.OriCOGSUT,
                OriCOGSAC = input.GPProjectDTO.OriCOGSAC,
                OriNetPrice = input.GPProjectDTO.OriNetPrice,
            };
            DB.GPProjects.Add(gpProject);
            await DB.SaveChangesAsync();
            //Console.WriteLine("2" + DateTime.Now);
            // version unit 
            List<GPUnit> gpUnitList = new List<GPUnit>();
            if (input.GPUnitDTOs is not null)
            {

                foreach (var item in input.GPUnitDTOs)
                {
                    var gpUnit = new GPUnit()
                    {
                        GPVersionID = gpVersion.ID,
                        UnitID = item.UnitID,
                        WBSBlock = item.WBSBlock,
                        BlockNumber = item.BlockNumber,
                        Budget_LI = item.BudgetLI,
                        Budget_CO01 = item.BudgetCO01,
                        Budget_CO01_Block = item.Budget_CO01_Block,
                        Budget_COC1 = item.BudgetCOC1,
                        Budget_CO_A1 = item.Budget_CO_A1,
                        Budget_UT = item.Budget_UT,
                        Budget_AC = item.Budget_AC,
                        NetPrice = item.NetPrice,
                        PercentGPNew = item.PercentGPNew,
                        PriceCommit = item.PriceCommit,
                        MinPrice = item.MinPrice,
                        Ori_COGS_LD = item.Ori_COGS_LD,
                        Ori_COGS_LI = item.Ori_COGS_LI,
                        Ori_COGS_CO = item.Ori_COGS_CO,
                        Ori_Budget_CO01 = item.Ori_Budget_CO01,
                        Ori_WIP_COC1 = item.Ori_WIP_COC1,
                        Ori_COGS_UT = item.Ori_COGS_UT,
                        Ori_COGS_AC = item.Ori_COGS_AC,
                        Ori_NetPrice = item.Ori_NetPrice,
                        Ori_PercentGPCommit = item.Ori_PercentGPCommit,
                        Ori_MinPrice = item.Ori_MinPrice,
                    };
                    gpUnitList.Add(gpUnit);
                }
            }
            //Console.WriteLine("3" + DateTime.Now);
            DB.GPUnits.AddRange(gpUnitList);
            await DB.SaveChangesAsync();

            return input;
        }
        private async Task<ReallocateMinPriceDTO> UpdateGPVersion(ReallocateMinPriceDTO input)
        {
            var gpVersion = await DB.GPVersions.Where(x => x.ID == input.Id).FirstOrDefaultAsync();
            gpVersion.VersionRemark = input.Remark;
            gpVersion.SyncDate = input.SyncDate;
            gpVersion.GPSyncOriginalID = input.GPSyncOriginalID;
            DB.GPVersions.Update(gpVersion);
            // version project
            //Console.WriteLine("1" + DateTime.Now);
            var gpProject = await DB.GPProjects.Where(x => x.GPVersionID == gpVersion.ID).FirstOrDefaultAsync();
            gpProject.BudgetLI = input.GPProjectDTO.BudgetLI;
            gpProject.BudgetCO01 = input.GPProjectDTO.BudgetCO01;
            gpProject.BudgetCOA1 = input.GPProjectDTO.BudgetCOA1;
            gpProject.BudgetCOC1 = input.GPProjectDTO.BudgetCOC1;
            gpProject.BudgetUT = input.GPProjectDTO.BudgetUT;
            gpProject.BudgetAC = input.GPProjectDTO.BudgetAC;
            gpProject.PercentGPCommit = input.GPProjectDTO.PercentGPCommit;
            gpProject.NetPrice = input.GPProjectDTO.NetPrice;
            gpProject.PriceCommit = input.GPProjectDTO.PriceCommit;

            DB.GPProjects.Update(gpProject);
            // version unit
            //Console.WriteLine("2" + DateTime.Now);
            var gpunitListID = input.GPUnitDTOs.Select(x => x.Id).ToList();
            var gpUnitList = await DB.GPUnits.Where(x => x.GPVersionID == gpVersion.ID && gpunitListID.Contains(x.ID)).ToListAsync();
            var gpUnitListID2 = new List<GPUnit>();
            //Console.WriteLine("3" + DateTime.Now);
            foreach (var item in input.GPUnitDTOs)
            {
                var gpUnit = gpUnitList.Find(x => x.ID == item.Id);
                gpUnit.WBSBlock = item.WBSBlock;
                gpUnit.BlockNumber = item.BlockNumber;
                gpUnit.Budget_LI = item.BudgetLI;
                gpUnit.Budget_CO01 = item.BudgetCO01;
                gpUnit.Budget_CO01_Block = item.Budget_CO01_Block;
                gpUnit.Budget_COC1 = item.BudgetCOC1;
                gpUnit.Budget_CO_A1 = item.Budget_CO_A1;
                gpUnit.Budget_UT = item.Budget_UT;
                gpUnit.Budget_AC = item.Budget_AC;
                gpUnit.NetPrice = item.NetPrice;
                gpUnit.PercentGPNew = item.PercentGPNew;
                gpUnit.PriceCommit = item.PriceCommit;
                gpUnit.MinPrice = item.MinPrice;
                gpUnitListID2.Add(gpUnit);
            }
            if (gpUnitListID2.Any()) DB.GPUnits.UpdateRange(gpUnitListID2);

            await DB.SaveChangesAsync();
            return input;
        }
        private async Task<string> GetGPVersionCode()
        {
            var RunningNumber = "";
            //Generate ContactNo 
            string year = DateTime.Today.Year.ToString();
            string month = DateTime.Today.Month.ToString("00");
            var runningKey = "Ver" + year + month;

            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("RunningKey", runningKey);
            ParamList.Add("RunningType", "GPSimulate");
            ParamList.Add("ZoroPad", 4);

            CommandDefinition commandDefinition = new(
                             commandText: DBStoredNames.spGenRunningNumber,
                             parameters: ParamList,
                             commandTimeout: 30,
                             transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(), // Read-only: no transaction
                             commandType: CommandType.StoredProcedure,
                             flags: CommandFlags.None); // No cache, read-only
            var query = await cmd.Connection.QueryAsync<string>(commandDefinition);
            var queryResult = query.FirstOrDefault();
            RunningNumber = queryResult;

            //var runningNumber = await DB.RunningNumberCounters.Where(o => o.Key == runningKey && o.Type == "GPSimulate").FirstOrDefaultAsync();
            //if (runningNumber != null)
            //{
            //    runningNumber.Count = runningNumber.Count + 1;
            //    RunningNumber = runningKey + runningNumber.Count.ToString("0000");
            //    DB.Entry(runningNumber).State = EntityState.Modified;
            //    await DB.SaveChangesAsync();
            //}
            //else
            //{
            //    var runningModel = new Database.Models.MST.RunningNumberCounter()
            //    {
            //        Key = runningKey,
            //        Type = "GPSimulate",
            //        Count = 1
            //    };

            //    await DB.RunningNumberCounters.AddAsync(runningModel);
            //    await DB.SaveChangesAsync();
            //    RunningNumber = runningKey + runningModel.Count.ToString("0000");
            //}
            return RunningNumber;
        }
        private async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            bool hasHeader = false;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                try
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
                    using (MemoryStream xlsxStream = new MemoryStream(excelByte))
                    using (var pck = new ExcelPackage(xlsxStream))
                    {
                        var ws = pck.Workbook.Worksheets.First();
                        DataTable tbl = new DataTable();
                        foreach (var firstRowCell in ws.Cells[6, 1, 6, ws.Dimension.End.Column])
                        {
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                        }
                        var startRow = 7;

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
                catch (Exception exp)
                {
                    stream.Close();
                    ValidateException ex = new ValidateException();
                    var errMsgFile = await DB.ErrorMessages.Where(o => o.Key == "ERR0147").FirstAsync();
                    ex.AddError(errMsgFile.Key, errMsgFile.Message, (int)errMsgFile.Type);
                    throw ex;
                }
            }
        }
        private async Task<GPUnitDTO> CreateGPUnitFromDataRow(DataRow dr)
        {
            try
            {
                var result = new GPUnitDTO();
                List<string> errorStrList = new List<string>();
                result.Unit = new UnitDTO();
                result.Unit.UnitNo = dr[1]?.ToString();
                string minPriceStr = dr[4]?.ToString();


                if (string.IsNullOrEmpty(result.Unit?.UnitNo))
                {
                    result.IsError = true;
                    errorStrList.Add("ต้องระบุข้อมูล Unit No");
                }
                if (!string.IsNullOrEmpty(minPriceStr))
                {
                    try
                    {
                        result.MinPrice = await TryParseDecimal(minPriceStr);

                    }
                    catch
                    {
                        result.IsError = true;
                        errorStrList.Add("ข้อมูล Min Price ต้องเป็นตัวเลข");
                    }
                }


                if (result.IsError)
                {
                    result.ErrorMessage = string.Join(',', errorStrList);
                }
                else
                {
                    result.ErrorMessage = "ข้อมูลถูกต้อง";
                }
                return result;
            }
            catch (Exception exp)
            {
                ValidateException ex = new ValidateException();
                var errMsgFile = await DB.ErrorMessages.Where(o => o.Key == "ERR0147").FirstAsync();
                ex.AddError(errMsgFile.Key, errMsgFile.Message, (int)errMsgFile.Type);
                throw ex;
            }
        }
        private async Task<decimal> TryParseDecimal(string input)
        {

            decimal result = 0;
            if (decimal.TryParse(input, out result))
            {
            }
            else
            {
                ValidateException ex = new ValidateException();
                var errMsgFile = await DB.ErrorMessages.Where(o => o.Key == "ERR0147").FirstAsync();
                ex.AddError(errMsgFile.Key, errMsgFile.Message, (int)errMsgFile.Type);
                throw ex;
            }
            return result;
        }
    }
}
