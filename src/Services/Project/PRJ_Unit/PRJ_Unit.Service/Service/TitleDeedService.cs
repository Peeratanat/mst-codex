using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using ExcelExtensions;
using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Inputs;
using PRJ_Unit.Params.Outputs;
using PRJ_Unit.Services.Excels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorHandling;
using System.Reflection;
using System.ComponentModel;
using NPOI.SS.Formula.Functions;
using Database.Models.LOG;
using static Database.Models.DbQueries.DBQueryParam;
using System.Data.SqlClient;
using Database.Models.DbQueries;
using Database.Models.DbQueries.PRJ;
using Database.Models.DbQueries.FIN;
using Database.Models.MST;
using Database.Models.DbQueries.ACC;
using System.Data.Common;
using Dapper;
using Common.Helper.Logging;
using FileStorage;
using Newtonsoft.Json;

namespace PRJ_Unit.Services
{
    public class TitleDeedService : ITitleDeedService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        private FileHelper FileHelper;

        public TitleDeedService(DatabaseContext db)
        {
            logModel = new LogModel("TitleDeedService", null);
            this.DB = db;

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }


        /// <summary>
        /// UI https://projects.invisionapp.com/d/main#/console/17482171/362360642/preview
        /// </summary>
        /// <returns>The title deed list async.</returns>
        /// <param name="projectID">Project identifier.</param>
        /// <param name="request">Request.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<TitleDeedPaging> GetTitleDeedListAsync(Guid? projectID, TitleDeedFilter filter, PageParam pageParam, TitleDeedListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {

            IQueryable<TitleDeedQueryResult> query = from u in DB.Units.AsNoTracking().Where(o => o.ProjectID == projectID)
                                                     join ttd in DB.TitledeedDetails.AsNoTracking().Where(o => o.ProjectID == projectID)
                                                      on u.ID equals ttd.UnitID into ttdData
                                                     from ttdModel in ttdData.DefaultIfEmpty()
                                                     join assetT in DB.MasterCenters.AsNoTracking()
                                                       on u.AssetTypeMasterCenterID equals assetT.ID

                                                     where u.ProjectID == projectID

                                                     select new TitleDeedQueryResult
                                                     {
                                                         Unit = u,
                                                         Titledeed = ttdModel,
                                                         Project = ttdModel.Project,
                                                         Model = ttdModel.Unit.Model,
                                                         LandOffice = ttdModel.Unit.LandOffice,
                                                         LandStatus = ttdModel.LandStatus,
                                                         PreferStatus = ttdModel.PreferStatus,
                                                         UpdatedBy = ttdModel.UpdatedBy,
                                                         AssetType = assetT,
                                                     };

            IQueryable<TitleDeedQueryResult> queryChkTrf = (from u in DB.Units.Where(o => o.ProjectID == projectID)

                                                            join arg in DB.Agreements.Where(o => o.IsDeleted == false && o.IsCancel == false)
                                                            on u.ID equals arg.UnitID

                                                            join trf in DB.Transfers.Where(o => o.IsDeleted == false)
                                                            on arg.ID equals trf.AgreementID

                                                            join ttd in DB.TitledeedDetails.Where(o => o.ProjectID == projectID)
                                                            on u.ID equals ttd.UnitID into ttdData
                                                            from ttdModel in ttdData.DefaultIfEmpty()

                                                            select new TitleDeedQueryResult
                                                            {
                                                                Unit = u,
                                                                Titledeed = ttdModel,
                                                                Project = ttdModel.Project,
                                                                Model = ttdModel.Unit.Model,
                                                                LandOffice = ttdModel.Unit.LandOffice,
                                                                LandStatus = ttdModel.LandStatus,
                                                                PreferStatus = ttdModel.PreferStatus,
                                                                UpdatedBy = ttdModel.UpdatedBy,
                                                                AssetType = DB.MasterCenters.Where(o => o.ID == u.AssetTypeMasterCenterID).FirstOrDefault()
                                                            });

            query = query.Where(o => o.AssetType.Key != "6" && o.AssetType.Key != "7");
            queryChkTrf = queryChkTrf.Where(o => o.AssetType.Key != "6" && o.AssetType.Key != "7");


            #region Filter 
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (!string.IsNullOrEmpty(filter.TitledeedNo))
            {
                query = query.Where(x => x.Titledeed.TitledeedNo != null && x.Titledeed.TitledeedNo.Contains(filter.TitledeedNo));
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo));
            }
            if (filter.LandOfficeID != null && filter.LandOfficeID != Guid.Empty)
            {
                query = query.Where(x => x.LandOffice.ID == filter.LandOfficeID);
            }
            if (!string.IsNullOrEmpty(filter.HouseName))
            {
                query = query.Where(x => x.Model.ID == Guid.Parse(filter.HouseName));
            }

            if (filter.TitledeedAreaFrom != null)
            {
                query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom);
            }
            if (filter.TitledeedAreaTo != null)
            {
                query = query.Where(x => x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo);
            }
            if (filter.TitledeedAreaFrom != null && filter.TitledeedAreaTo != null)
            {
                query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom
                                        && x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo);
            }

            if (filter.UsedAreaFrom != null)
            {
                query = query.Where(x => x.Titledeed.Unit.UsedArea >= filter.UsedAreaFrom);
            }
            if (filter.UsedAreaTo != null)
            {
                query = query.Where(x => x.Titledeed.Unit.UsedArea <= filter.UsedAreaTo);
            }
            if (filter.UsedAreaFrom != null && filter.UsedAreaTo != null)
            {
                query = query.Where(x => x.Titledeed.Unit.UsedArea >= filter.UsedAreaFrom
                                        && x.Titledeed.Unit.UsedArea <= filter.UsedAreaTo);
            }

            if (!string.IsNullOrEmpty(filter.LandNo))
            {
                query = query.Where(x => x.Titledeed.LandNo.Contains(filter.LandNo));
            }
            if (!string.IsNullOrEmpty(filter.LandSurveyArea))
            {
                query = query.Where(x => x.Titledeed.LandSurveyArea.Contains(filter.LandSurveyArea));
            }
            if (!string.IsNullOrEmpty(filter.LandPortionNo))
            {
                query = query.Where(x => x.Titledeed.LandPortionNo.Contains(filter.LandPortionNo));
            }
            if (!string.IsNullOrEmpty(filter.LandStatusKey))
            {
                var landStatusMasterCenterID = await DB.MasterCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Key == filter.LandStatusKey
                                                                       && x.MasterCenterGroupKey == "LandStatus");
                if (landStatusMasterCenterID is not null)
                    query = query.Where(x => x.LandStatus.ID == landStatusMasterCenterID.ID);
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Key == filter.UnitStatusKey
                                                                       && x.MasterCenterGroupKey == "UnitStatus");
                if (unitStatusMasterCenterID is not null)
                    query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID.ID);
            }
            if (!string.IsNullOrEmpty(filter.PreferStatusKey))
            {
                var preferStatusMasterCenterID = await DB.MasterCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Key == filter.PreferStatusKey
                                                                       && x.MasterCenterGroupKey == "PreferStatus");
                if (preferStatusMasterCenterID is not null)
                    query = query.Where(x => x.PreferStatus.ID == preferStatusMasterCenterID.ID);
            }

            if (filter.LandStatusDateFrom != null)
            {
                query = query.Where(x => x.Titledeed.LandStatusDate >= filter.LandStatusDateFrom);
            }
            if (filter.LandStatusDateTo != null)
            {
                query = query.Where(x => x.Titledeed.LandStatusDate <= filter.LandStatusDateTo);
            }
            if (filter.LandStatusDateFrom != null && filter.LandStatusDateTo != null)
            {
                query = query.Where(x => x.Titledeed.LandStatusDate >= filter.LandStatusDateFrom
                                        && x.Titledeed.LandStatusDate <= filter.LandStatusDateTo);
            }


            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Titledeed.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Titledeed.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Titledeed.Updated >= filter.UpdatedFrom && x.Titledeed.Updated <= filter.UpdatedTo);
            }
            //}
            #endregion

            var ProJect = await DB.Projects.Where(o => o.ID == projectID
                                            && (
                                                (o.MortgageBankID == null && o.RedeemLoanDate == null)
                                                || (o.MortgageBankID == null && o.RedeemLoanDate != null && o.RedeemLoanDate <= DateTime.Now)
                                                || (o.MortgageBankID != null && o.RedeemLoanDate != null && o.RedeemLoanDate <= DateTime.Now)
                                            )).AnyAsync();

            TitleDeedListDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<TitleDeedQueryResult>(pageParam, ref query);

            var queryResults = await query.OrderBy(c => c.Unit.UnitNo).ToListAsync(cancellationToken);
            var queryChkTrfResults = await queryChkTrf.Select(o => o.Unit.UnitNo).ToListAsync(cancellationToken);



            var results = queryResults.Select(o => TitleDeedListDTO.CreateFromQueryTitleUnitResult(o, queryChkTrfResults, ProJect)).ToList();

            return new TitleDeedPaging()
            {
                PageOutput = pageOutput,
                TitleDeeds = results
            };
        }

        /// <summary>
        /// UI https://projects.invisionapp.com/d/main#/console/17482171/362360642/preview
        /// </summary>
        /// <returns>The title deed list async.</returns>
        /// <param name="projectID">Project identifier.</param>
        /// <param name="request">Request.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>

        public async Task<TitleDeedDTO> GetTitleDeedAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.TitledeedDetails.AsNoTracking()
                                                 .Include(o => o.Project)
                                                 .Include(o => o.Unit)
                                                 .ThenInclude(o => o.LandOffice)
                                                 .Include(o => o.Unit)
                                                 .ThenInclude(o => o.HouseProvince)
                                                 .Include(o => o.Unit)
                                                 .ThenInclude(o => o.HouseDistrict)
                                                 .Include(o => o.Unit)
                                                 .ThenInclude(o => o.HouseSubDistrict)
                                                 .Include(o => o.Address)
                                                 .Include(o => o.Address.Province)
                                                 .Include(o => o.Address.District)
                                                 .Include(o => o.Address.District.SubDistricts)
                                                 .Include(o => o.LandStatus)
                                                 .Include(o => o.PreferStatus)
                                                 .Include(o => o.UpdatedBy)
                                                 .FirstOrDefaultAsync(o => o.ID == id, cancellationToken);

            var result = TitleDeedDTO.CreateFromModel(model);
            return result;
        }

        public async Task<TitleDeedDTO> CreateTitleDeedAsync(Guid projectID, TitleDeedDTO input)
        {
            await input.ValidateAsync(projectID, false, DB);
            var unit = await DB.Units.FirstAsync(o => o.ID == input.Unit.Id);
            TitledeedDetail model = new TitledeedDetail();
            model.Unit = unit;
            input.ToModel(ref model);
            model.ProjectID = projectID;
            await DB.TitledeedDetails.AddAsync(model);
            DB.Update(model.Unit);
            await DB.SaveChangesAsync();
            var result = await this.GetTitleDeedAsync(model.ID);
            return result;
        }

        public async Task<TitleDeedDTO> UpdateTitleDeedAsync(Guid projectID, Guid id, TitleDeedDTO input)
        {
            await input.ValidateAsync(projectID, true, DB);
            var model = await DB.TitledeedDetails.Include(o => o.Unit).FirstAsync(x => x.ProjectID == projectID && x.ID == id);

            input.ToModel(ref model);
            model.ProjectID = projectID;

            var project = await DB.Projects.FirstAsync(o => o.ID == projectID);
            var titleDeedDataStatusMasterCenterID = await this.TitleDeedDataStatus(projectID);
            project.TitleDeedDataStatusMasterCenterID = titleDeedDataStatusMasterCenterID;

            DB.Entry(model).State = EntityState.Modified;
            DB.Update(model.Unit);
            await DB.SaveChangesAsync();
            var result = await this.GetTitleDeedAsync(model.ID);
            return result;
        }

        public async Task<TitledeedDetail> DeleteTitleDeedAsync(Guid projectID, Guid id)
        {
            var model = await DB.TitledeedDetails.FirstAsync(o => o.ProjectID == projectID && o.ID == id);
            model.IsDeleted = true;

            var project = await DB.Projects.FirstAsync(o => o.ID == projectID);
            var titleDeedDataStatusMasterCenterID = await this.TitleDeedDataStatus(projectID);
            project.TitleDeedDataStatusMasterCenterID = titleDeedDataStatusMasterCenterID;

            await DB.SaveChangesAsync();
            return model;
        }
        public async Task<TitledeedExcelDTO> ImportTitleDeedAsync(Guid projectID, FileDTO input, Guid? UserID = null)
        {

            #region Validate Excel template
            var dt = await this.validateFileTemplate(input);
            #endregion




            var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
            var result = new TitledeedExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>(), Messages = new List<string>() };
            //var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            //if (dt.Columns.Count != 24)
            //{
            //	result.Messages.Add("Invalid File Format");
            //	result.Success = 0;
            //	return result;
            //}

            //ValidateException ex = new ValidateException();
            //if (dt.Columns.Count != 24)
            //{
            //	var msg = "Invalid File Format";
            //	ex.AddError("", msg, 1);
            //}
            //if (ex.HasError)
            //{
            //	throw ex;
            //}


            if (input.IsTemp)
            {
                string Name = "TitleDeed.xlsx";
                string titledeedName = $"import-project/{projectNo}/titledeed/{Name}";
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
            imp.Import_Type = "titledeed";
            imp.Import_Status = "I";

            await DB.ImptMstProjTrans.AddAsync(imp);
            await DB.SaveChangesAsync();

            return result;

        }

        public async Task<FileDTO> ExportExcelTitleDeedAsync(Guid projectID, CancellationToken cancellationToken = default)
        {
            ExportExcel result = new ExportExcel();

            IQueryable<TitleDeedQueryResult> query = from u in DB.Units.Where(o => o.ProjectID == projectID
                                                                         //   && string.IsNullOrEmpty(o.HouseNo)
                                                                         //   && (o.HouseNoReceivedYear == 0 || o.HouseNoReceivedYear == null)
                                                                         )
                                                                        .Include(o => o.UnitStatus)
                                                                        .Include(o => o.Model)
                                                                        .Include(o => o.Floor)
                                                     join ttd in DB.TitledeedDetails.OrderByDescending(o => o.Created).Where(o => o.ProjectID == projectID
                                                                                      //   && string.IsNullOrEmpty(o.TitledeedNo)
                                                                                      //   && string.IsNullOrEmpty(o.LandNo)
                                                                                      //   && string.IsNullOrEmpty(o.LandSurveyArea)
                                                                                      //   && string.IsNullOrEmpty(o.LandPortionNo)
                                                                                      //   && string.IsNullOrEmpty(o.PageNo)
                                                                                      //   && string.IsNullOrEmpty(o.BookNo)
                                                                                      //   && (o.TitledeedArea == 0 || o.TitledeedArea == null)
                                                                                      //   && (o.EstimatePrice == 0 || o.EstimatePrice == null)
                                                                                      //   && string.IsNullOrEmpty(o.Remark)
                                                                                      )


                                                     on u.ID equals ttd.UnitID into ttdData
                                                     from ttdModel in ttdData.DefaultIfEmpty()


                                                     select new TitleDeedQueryResult
                                                     {
                                                         Unit = u,
                                                         Titledeed = ttdModel ?? new TitledeedDetail(),
                                                         Project = ttdModel.Project,
                                                         Model = u.Model,
                                                         LandOffice = ttdModel.Unit.LandOffice,
                                                         LandStatus = ttdModel.LandStatus,
                                                         PreferStatus = ttdModel.PreferStatus,
                                                         UpdatedBy = ttdModel.UpdatedBy,
                                                         Floor = u.Floor
                                                     };

            var data = await query.Where(o => o.Unit.AssetType.Key != "6" && o.Unit.AssetType.Key != "7").OrderBy(c => c.Unit.SAPWBSNo).ToListAsync(cancellationToken);

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_TitleDeed.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = TitledeedDetailExcelModel._projectNoIndex + 1;
                int _wbsNoIndex = TitledeedDetailExcelModel._wbsNoIndex + 1;
                int _unitNoIndex = TitledeedDetailExcelModel._unitNoIndex + 1;
                int _modelNameIndex = TitledeedDetailExcelModel._modelNameIndex + 1;

                int _floorNoIndex = TitledeedDetailExcelModel._floorNoIndex + 1;

                int _houseNoIndex = TitledeedDetailExcelModel._houseNoIndex + 1;
                int _yearGotHouseNo = TitledeedDetailExcelModel._houseNoReceivedYear + 1;
                int _titledeedNoIndex = TitledeedDetailExcelModel._titledeedNoIndex + 1;
                int _landNoIndex = TitledeedDetailExcelModel._landNoIndex + 1;
                int _landSurveyAreaIndex = TitledeedDetailExcelModel._landSurveyAreaIndex + 1;
                int _landPortionNoIndex = TitledeedDetailExcelModel._landPortionNoIndex + 1;
                int _pageNoIndex = TitledeedDetailExcelModel._pageNoIndex + 1;
                int _bookNoIndex = TitledeedDetailExcelModel._bookNoIndex + 1;
                int _titledeedAreaIndex = TitledeedDetailExcelModel._titledeedAreaIndex + 1;

                int _usedAreaIndex = TitledeedDetailExcelModel._usedAreaIndex + 1;
                int _fenceAreaIndex = TitledeedDetailExcelModel._fenceAreaIndex + 1;
                int _fenceIronAreaIndex = TitledeedDetailExcelModel._fenceIronAreaIndex + 1;
                int _balconyAreaIndex = TitledeedDetailExcelModel._balconyAreaIndex + 1;
                int _airAreaIndex = TitledeedDetailExcelModel._airAreaIndex + 1;
                int _poolAreaIndex = TitledeedDetailExcelModel._poolAreaIndex + 1;
                int _parkingAreaIndex = TitledeedDetailExcelModel._parkingAreaIndex + 1;
                int _totalAreaIndex = TitledeedDetailExcelModel._totalAreaIndex + 1;

                int _estimatePriceIndex = TitledeedDetailExcelModel._estimatePriceIndex + 1;
                int _remarkIndex = TitledeedDetailExcelModel._remarkIndex + 1;

                var Project = await DB.Projects.FirstOrDefaultAsync(x => x.ID == projectID, cancellationToken);
                var productTypeMasterCenterKey = await DB.MasterCenters.Where(o => o.ID == Project.ProductTypeMasterCenterID).Select(o => o.Key).FirstOrDefaultAsync(cancellationToken);
                int rowCount = 0;
                for (int c = 2; c < data.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = Project?.ProjectNo;
                    worksheet.Cells[c, _wbsNoIndex].Value = data[c - 2].Unit?.SAPWBSNo;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _modelNameIndex].Value = data[c - 2].Model?.NameTH;
                    worksheet.Cells[c, _floorNoIndex].Value = data[c - 2].Unit?.Floor?.NameEN;
                    //if (isData)
                    //{
                    worksheet.Cells[c, _houseNoIndex].Value = data[c - 2].Unit?.HouseNo;
                    if (data[c - 2].Unit?.HouseNoReceivedYear > 0)
                    {
                        worksheet.Cells[c, _yearGotHouseNo].Value = data[c - 2].Unit?.HouseNoReceivedYear;
                    }

                    worksheet.Cells[c, _titledeedNoIndex].Value = data[c - 2].Titledeed?.TitledeedNo;
                    worksheet.Cells[c, _landNoIndex].Value = data[c - 2].Titledeed?.LandNo;
                    worksheet.Cells[c, _landSurveyAreaIndex].Value = data[c - 2].Titledeed?.LandSurveyArea;
                    worksheet.Cells[c, _landPortionNoIndex].Value = data[c - 2].Titledeed?.LandPortionNo;
                    worksheet.Cells[c, _pageNoIndex].Value = data[c - 2].Titledeed?.PageNo;
                    worksheet.Cells[c, _bookNoIndex].Value = data[c - 2].Titledeed?.BookNo;
                    worksheet.Cells[c, _titledeedAreaIndex].Value = data[c - 2].Titledeed?.TitledeedArea;
                    if (productTypeMasterCenterKey == "1") //แนบราบ
                    {
                        worksheet.Cells[c, _usedAreaIndex].Value = data[c - 2].Unit?.UsedArea;
                        worksheet.Cells[c, _fenceAreaIndex].Value = data[c - 2].Unit?.FenceArea;
                        worksheet.Cells[c, _fenceIronAreaIndex].Value = data[c - 2].Unit?.FenceIronArea;
                    }

                    if (productTypeMasterCenterKey == "2") //แนบสูง
                    {
                        worksheet.Cells[c, _usedAreaIndex].Value = data[c - 2].Unit?.UsedArea;
                        worksheet.Cells[c, _parkingAreaIndex].Value = data[c - 2].Unit?.ParkingArea;
                        worksheet.Cells[c, _balconyAreaIndex].Value = data[c - 2].Unit?.BalconyArea;
                        worksheet.Cells[c, _airAreaIndex].Value = data[c - 2].Unit?.AirArea;
                        worksheet.Cells[c, _poolAreaIndex].Value = data[c - 2].Unit?.PoolArea;

                        double? titledeedArea = data[c - 2].Titledeed?.TitledeedArea != null ? data[c - 2].Titledeed?.TitledeedArea : 0;
                        double? balconyArea = data[c - 2].Unit?.BalconyArea != null ? data[c - 2].Unit?.BalconyArea : 0;
                        double? airArea = data[c - 2].Unit?.AirArea != null ? data[c - 2].Unit?.AirArea : 0;
                        double? poolArea = data[c - 2].Unit?.PoolArea != null ? data[c - 2].Unit?.PoolArea : 0;
                        double? totalArea = titledeedArea + balconyArea + airArea + poolArea;
                        worksheet.Cells[c, _totalAreaIndex].Value = totalArea;
                    }
                    if (data[c - 2].Titledeed?.EstimatePrice > 0)
                    {
                        worksheet.Cells[c, _estimatePriceIndex].Value = data[c - 2].Titledeed?.EstimatePrice;
                    }
                    worksheet.Cells[c, _remarkIndex].Value = data[c - 2].Titledeed?.Remark;
                    //}
                    rowCount += 1;
                }

                if (productTypeMasterCenterKey == "1") //แนบราบ
                {
                    rowCount = rowCount + 2;
                    var titleDeedPublicUtilityMasterList = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PublicUtility").OrderBy(o => o.Order).ToListAsync(cancellationToken);
                    //if (isData)
                    //{
                    var tdpuList = await DB.TitleDeedPublicUtilitys.Where(o => o.ProjectID == projectID).OrderBy(o => o.UnitNo).ToListAsync(cancellationToken);

                    for (int c = 0; c < titleDeedPublicUtilityMasterList.Count; c++)
                    {
                        worksheet.Cells[rowCount + c, _projectNoIndex].Value = Project.ProjectNo;

                        worksheet.Cells[rowCount + c, _wbsNoIndex].Value = "WBSPublicUtility" + (c + 1);
                        worksheet.Cells[rowCount + c, _unitNoIndex].Value = "UnitPublicUtility" + (c + 1);
                        worksheet.Cells[rowCount + c, _houseNoIndex].Value = titleDeedPublicUtilityMasterList[c]?.Name;
                        if (c >= tdpuList.Count)
                        {
                            continue;
                        }
                        if (tdpuList.Any())
                        {
                            worksheet.Cells[rowCount + c, _titledeedNoIndex].Value = tdpuList[c]?.TitledeedNo;
                            worksheet.Cells[rowCount + c, _landNoIndex].Value = tdpuList[c]?.LandNo;
                            worksheet.Cells[rowCount + c, _landSurveyAreaIndex].Value = tdpuList[c]?.LandSurveyArea;
                            worksheet.Cells[rowCount + c, _landPortionNoIndex].Value = tdpuList[c]?.LandPortionNo;
                            worksheet.Cells[rowCount + c, _pageNoIndex].Value = tdpuList[c]?.PageNo;
                            worksheet.Cells[rowCount + c, _bookNoIndex].Value = tdpuList[c]?.BookNo;
                            worksheet.Cells[rowCount + c, _titledeedAreaIndex].Value = tdpuList[c]?.TitledeedArea;
                        }

                    }
                }

                result.FileContent = package.GetAsByteArray();
                result.FileName = Project.ProjectNo + "_TitleDeed.xlsx";
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

        private async Task<Guid> TitleDeedDataStatus(Guid projectID)
        {
            //string[] SAPWBSNoTemp =
            //	{
            //					"WBSPublicUtility1",
            //					"WBSPublicUtility2",
            //					"WBSPublicUtility3",
            //					"WBSPublicUtility4",
            //					"WBSPublicUtility5",
            //					"WBSPublicUtility6"
            //				};

            //var allTitleDeedDetail = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID).ToListAsync();
            var allTitleDeedDetail = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID)
                                     .GroupBy(o => o.UnitID).Select(o => o.OrderByDescending(o => o.Created).First())
                                     .ToListAsync();

            var titleDeedDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync(); //อยู่ระหว่างจัดเตรียม
            var titleDeedDataStatusTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer).Select(o => o.ID).FirstAsync(); //พร้อมโอน
            var titleDeedDataStatusMasterCenterID = titleDeedDataStatusPrepareMasterCenterID;
            if (allTitleDeedDetail.TrueForAll(o => o.TitledeedNo != null
                                         && o.UnitID != null))
            // && o.AddressID != null))
            {
                titleDeedDataStatusMasterCenterID = titleDeedDataStatusTransferMasterCenterID;
            }

            return titleDeedDataStatusMasterCenterID;
        }

        public async Task UpdateMultipleHouseNosAsync(Guid projectID, UpdateMultipleHouseNoParam input)
        {
            ValidateException ex = new ValidateException();
            //validate house no
            if (!input.FromHouseNo.IsOnlyNumberWithSpecialCharacter(true))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
                string desc = input.GetType().GetProperty(nameof(UpdateMultipleHouseNoParam.FromHouseNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            var units = await DB.Units
                .Where(c => c.ProjectID == projectID && (String.Compare(c.UnitNo, input.FromUnit.UnitNo) >= 0) && (String.Compare(c.UnitNo, input.ToUnit.UnitNo) <= 0))
                .OrderBy(o => o.UnitNo)
                .ToListAsync();
            string houseNo = input.FromHouseNo;
            int runningHouseNo = 1;
            if (input.FromHouseNo.Contains('/'))
            {
                var lastSlashIndex = input.FromHouseNo.LastIndexOf('/');
                var beforeSlash = input.FromHouseNo.Substring(0, lastSlashIndex);
                var afterSlash = input.FromHouseNo.Substring(lastSlashIndex + 1, input.FromHouseNo.Length - (lastSlashIndex + 1));
                houseNo = beforeSlash;
                if (!int.TryParse(afterSlash, out runningHouseNo))
                {
                    runningHouseNo = 1;
                }
            }
            List<string> houseNos = new List<string>();
            foreach (var item in units)
            {
                houseNos.Add($"{houseNo}/{runningHouseNo++}");
            }

            //validate unique
            var hasHouseNo = await DB.Units.Where(o => o.ProjectID == projectID && houseNos.Contains(o.HouseNo)).AnyAsync();
            if (hasHouseNo)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                string desc = input.GetType().GetProperty(nameof(UpdateMultipleHouseNoParam.FromHouseNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", string.Join(',', houseNos));
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            foreach (var item in units)
            {
                item.HouseNo = item.HouseNo;
                item.HouseNoReceivedYear = item.HouseNoReceivedYear;
            }
            DB.UpdateRange(units);
            await DB.SaveChangesAsync();

        }

        public async Task UpdateMultipleLandOfficesAsync(Guid projectID, UpdateMultipleLandOfficeParam input)
        {
            var units = await DB.Units
                .Where(c => c.ProjectID == projectID && (String.Compare(c.UnitNo, input.FromUnit.UnitNo) >= 0) && (String.Compare(c.UnitNo, input.ToUnit.UnitNo) <= 0))
                .ToListAsync();
            foreach (var unit in units)
            {
                unit.LandOfficeID = input.LandOffice.Id;
            }
            DB.UpdateRange(units);
            await DB.SaveChangesAsync();
        }

        public async Task UpdateMultipleHouseNoReceivedYearAsync(Guid projectID, UpdateMultipleHouseNoReceivedYearParam input)
        {

            IQueryable<TitleDeedQueryResult> queryChkTrf = DB.Units
                .Where(u => u.ProjectID == projectID)
                .Join(DB.Agreements.Where(arg => !arg.IsDeleted && !arg.IsCancel),
                      u => u.ID,
                      arg => arg.UnitID,
                      (u, arg) => new { u, arg })
                .Join(DB.Transfers.Where(trf => !trf.IsDeleted),
                      combined => combined.arg.ID,
                      trf => trf.AgreementID,
                      (combined, trf) => new { combined.u, combined.arg, trf })
                .GroupJoin(DB.TitledeedDetails
                           .Where(ttd => ttd.ProjectID == projectID)
                           .GroupBy(ttd => ttd.UnitID)
                           .Select(group => group.First())
                           .OrderByDescending(ttd => ttd.Created),
                           combined => combined.u.ID,
                           ttd => ttd.UnitID,
                           (combined, ttdData) => new { combined.u, ttdData = ttdData.DefaultIfEmpty() })
                .Select(result => new TitleDeedQueryResult
                {
                    Unit = result.u
                });

            var unitsNo = await queryChkTrf.Select(o => o.Unit.UnitNo).ToListAsync();

            var units = await DB.Units
                .Where(c => c.ProjectID == projectID && (String.Compare(c.UnitNo, input.FromUnit.UnitNo) >= 0) && (String.Compare(c.UnitNo, input.ToUnit.UnitNo) <= 0)
                && !unitsNo.Contains(c.UnitNo) && c.HouseNo != null).ToListAsync();

            foreach (var unit in units)
            {
                //if (unitsNo.Contains(unit?.UnitNo) || unit?.HouseNo == null) {
                //	continue;
                //}
                unit.HouseNoReceivedYear = input.HouseNoReceivedYear;
            }
            DB.UpdateRange(units);
            await DB.SaveChangesAsync();
        }

        public async Task<SyncTitledeedFromLandResponse> SyncTitledeedFromLandAsync(string projectNo, CancellationToken cancellationToken = default)
        {
            SyncTitledeedFromLandResponse result = new SyncTitledeedFromLandResponse();

            string landApiUrl = Environment.GetEnvironmentVariable("landApiUrl");
            string landApiKey = Environment.GetEnvironmentVariable("landApiKey");
            string landApiToken = Environment.GetEnvironmentVariable("landApiToken");

            var requestUrl = $"{landApiUrl}External/GetUnitWithTitleDeed?project_no=" + projectNo;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("apiKey", landApiKey);
                client.DefaultRequestHeaders.Add("apiToken", landApiToken);


                //using (var stringContent = new StringContent(null, System.Text.Encoding.UTF8, "application/json"))
                using (var Response = await client.PostAsync(requestUrl, null))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string resultObj = await Response.Content.ReadAsStringAsync();
                        SyncTitledeedFromLandDTO Result = JsonConvert.DeserializeObject<SyncTitledeedFromLandDTO>(resultObj);
                        var UnitUpdateList = new List<Unit>();
                        var TitledeedUpdateList = new List<TitledeedDetail>();
                        var TitledeedNewList = new List<TitledeedDetail>();
                        foreach (var project in Result.Data)
                        {
                            foreach (var units in project.Units)
                            {
                                var unit = await DB.Units.Include(o => o.Project).Where(o => o.Project.ProjectNo == project.ProjectNo && o.UnitNo == units.UnitNo).FirstOrDefaultAsync();

                                if (unit != null)
                                {
                                    if (unit.ModelID == null)
                                    {
                                        var model = await DB.Models.Where(o => o.ProjectID == unit.ProjectID && o.NameTH == units.ModelName).Select(o => o.ID).FirstOrDefaultAsync();
                                        unit.ModelID = Guid.Parse(model.ToString());
                                    }
                                    //if (unit.FloorID)
                                    //{

                                    //}
                                    if (unit.HouseNo == null)
                                    {
                                        unit.HouseNo = units.HouseNo;
                                    }
                                    if (unit.HouseNoReceivedYear == null)
                                    {
                                        unit.HouseNo = units.HouseNoReceivedYear;
                                    }
                                    if (unit.UsedArea == null)
                                    {
                                        unit.UsedArea = units.UsedArea;
                                    }
                                    if (unit.FenceArea == null)
                                    {
                                        unit.FenceArea = units.FenceArea;
                                    }
                                    if (unit.FenceIronArea == null)
                                    {
                                        unit.FenceIronArea = units.FenceIronArea;
                                    }
                                    if (unit.BalconyArea == null)
                                    {
                                        unit.BalconyArea = units.BalconyArea;
                                    }
                                    if (unit.AirArea == null)
                                    {
                                        unit.AirArea = units.AirArea;
                                    }
                                    if (unit.PoolArea == null)
                                    {
                                        unit.PoolArea = units.PoolArea;
                                    }
                                    if (unit.ParkingArea == null)
                                    {
                                        unit.ParkingArea = units.ParkingArea;
                                    }
                                    if (unit.UsedArea == null)
                                    {
                                        unit.UsedArea = units.UsedArea;
                                    }
                                    //unit.UpdatedByUserID = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                    unit.RefMigrateID2 = "Sync By Admin From LAM. " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                                    UnitUpdateList.Add(unit);
                                    var titledeed = await DB.TitledeedDetails.Where(o => o.UnitID == unit.ID).FirstOrDefaultAsync();
                                    if (titledeed != null)
                                    {
                                        titledeed.TitledeedNo = units.DeedNumber;
                                        titledeed.LandNo = units.LandNumber;
                                        titledeed.LandSurveyArea = units.PlaceSurvey;
                                        titledeed.LandPortionNo = units.Portion;
                                        titledeed.BookNo = units.BookNumber;
                                        titledeed.PageNo = units.PageNumber;
                                        titledeed.TitledeedArea = units.SquareWa;

                                        if (titledeed.EstimatePrice == null)
                                        {
                                            titledeed.EstimatePrice = units.EstimatePrice;
                                        }
                                        titledeed.Remark = units.Remark;
                                        titledeed.UpdatedByUserID = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                        titledeed.RefMigrateID2 = "Sync By Admin From LAM. " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                                        TitledeedUpdateList.Add(titledeed);
                                    }
                                    else
                                    {
                                        var newTitledeed = new TitledeedDetail()
                                        {
                                            UnitID = unit.ID,
                                            TitledeedNo = units.DeedNumber,
                                            LandNo = units.LandNumber,
                                            LandSurveyArea = units.PlaceSurvey,
                                            LandPortionNo = units.Portion,
                                            BookNo = units.BookNumber,
                                            PageNo = units.PageNumber,
                                            TitledeedArea = units.SquareWa,
                                            EstimatePrice = units.EstimatePrice,
                                            Remark = units.Remark,
                                            //UpdatedByUserID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                                            RefMigrateID2 = "Sync By Admin From LAM. " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                                        };
                                        TitledeedNewList.Add(newTitledeed);
                                    }
                                }
                            }


                        }
                        DB.Units.UpdateRange(UnitUpdateList);
                        DB.TitledeedDetails.UpdateRange(TitledeedUpdateList);
                        await DB.TitledeedDetails.AddRangeAsync(TitledeedNewList);
                        await DB.SaveChangesAsync();

                    }
                    else
                    {
                        string resultObj = await Response.Content.ReadAsStringAsync();
                        SyncTitledeedFromLandDTO Result = JsonConvert.DeserializeObject<SyncTitledeedFromLandDTO>(resultObj);
                        string message = Result.Message;
                        throw new UnauthorizedException(message);
                    }
                }
                result.Message = "Sync Titledeed From Land Success";
                result.IsSuccess = true;
                return result;
            }
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
                    if (tbl.Columns.Count != 24)
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
