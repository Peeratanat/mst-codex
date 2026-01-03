using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Dapper;
using Database.Models;
using Database.Models.CTM;
using Database.Models.DbQueries;
using Database.Models.DbQueries.SAL;
using Database.Models.PRJ;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Database.Models.DbQueries.DBQueryParam;

namespace PRJ_ProjectInfo.Services
{
	public class ProjectInfoService : IProjectInfoService
	{

		private readonly DatabaseContext DB;
		private FileHelper FileHelperReport;

		public LogModel logModel { get; set; }

		public ProjectInfoService(DatabaseContext dB)
		{

			logModel = new LogModel("ProjectInfoService", null);
			DB = dB;
			var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
			var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
			var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
			var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
			var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
			var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
			var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

			FileHelperReport = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
		}

		public async Task<dbqUnitAreaNationality> GetUnitAreaEngNatinality(Guid projectID, CancellationToken cancellationToken = default)
		{
			var sqlQuery = @"SELECT * FROM vw_SaleAreaNationality WHERE ProjectID = @ProjectID";

			using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
			DynamicParameters ParamList = new DynamicParameters();
			ParamList.Add("ProjectID", projectID);

			CommandDefinition commandDefinition = new(
										 commandText: sqlQuery,
											 cancellationToken: cancellationToken,
										 parameters: ParamList,
										 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
										 commandType: CommandType.Text);
			return await cmd.Connection.QueryFirstOrDefaultAsync<dbqUnitAreaNationality>(commandDefinition) ?? new();
		}

		public async Task<ProjectInfoDTO> GetProjectInfoAsync(Guid id, CancellationToken cancellationToken = default)
		{
			var model = await DB.Projects.AsNoTracking().Where(o => o.ID == id)
										.Include(o => o.ProjectStatus)
									  .Include(o => o.Brand)
									  .Include(o => o.Company)
									  .Include(o => o.ProductType)
									  .Include(o => o.ProjectType)
									  .Include(o => o.BG)
									  .Include(o => o.SubBG)
									  .Include(o => o.MortgageBank)
									  .Include(o => o.UpdatedBy)
                                      .FirstOrDefaultAsync(cancellationToken);

            var agreementConfig = await DB.AgreementConfigs.AsNoTracking().Where(o => o.ProjectID == id).FirstOrDefaultAsync(cancellationToken);


            // var dbqUnitAreaNationalityInfo = await GetUnitAreaEngNatinality(id, cancellationToken);

            var queryResult = new dbqCDGetQuotaFQTHByProject();

			if (model.ProductType.Key == "2")
			{
				using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
				DynamicParameters ParamList = new DynamicParameters();
				ParamList.Add("ProjectID", id);

				CommandDefinition commandDefinition = new(
											 commandText: DBStoredNames.sp_CD_GetQuotaFQTHByProject,
											 parameters: ParamList,
											 cancellationToken: cancellationToken,
											 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
											 commandType: CommandType.StoredProcedure);
				queryResult = await cmd.Connection.QueryFirstOrDefaultAsync<dbqCDGetQuotaFQTHByProject>(commandDefinition) ?? new();
			}



			var result = await ProjectInfoDTO.CreateInfoFromModelAsync(model, DB, queryResult);
			if (!string.IsNullOrEmpty(model.MasterPlanFilePath))
			{
				string masterPlan = await FileHelperReport.GetFileUrlAsync("projects", model.MasterPlanFilePath);
				result.URLMasterPlan = masterPlan;
			}
	
			result.ReasonCancelFQTQ = agreementConfig?.ReasonCancelFQTQ;
            result.ReasonCancelFQTQDateTime = agreementConfig?.ReasonCancelFQTQDateTime;
            result.ReasonCancelFQTQBy = agreementConfig?.ReasonCancelFQTQBy;
            return result;
		}
		public async Task<ProjectInfoDTO> UpdateProjectInfoAsync(Guid id, ProjectInfoDTO input, Guid? userID)
		{

			await input.ValidateAsync(DB);


			var model = await DB.Projects.FindAsync(id);
			if (input.IsForeignProject != model.IsForeignProject && input.IsForeignProject == false)
			{
				model.ForeignProjectEnd = DateTime.Now;
			}
			if (input.IsForeignProject != model.IsForeignProject && input.IsForeignProject == true)
			{
				model.ForeignProjectStart = DateTime.Now;
			}
			input.ToModel(ref model);


			var generalDataStatus = await GeneralDataStatus(model.ID);
			model.GeneralDataStatusMasterCenterID = generalDataStatus;
			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();

			var projectStatus = await ProjectStatus(model.ID);

			model.ProjectStatusMasterCenterID = projectStatus;
			DB.Entry(model).State = EntityState.Modified;
			var rotateConfig = await DB.LeadRotateProjectConfigs.Where(o => o.ProjectID == id && o.IsActived == true).FirstOrDefaultAsync();
			if (rotateConfig != null)
			{
				if (input.NumberRotateDay == true)
				{
					rotateConfig.NumberRotateDay = 18;
				}
				else
				{
					rotateConfig.NumberRotateDay = 30;
				}
				DB.Update(rotateConfig);
			}
			else
			{
				var newRotateConfig = new LeadRotateProjectConfig
				{
					ProjectID = id,
					IsActived = true
				};
				if (input.NumberRotateDay == true)
				{
					newRotateConfig.NumberRotateDay = 18;
				}
				else
				{
					newRotateConfig.NumberRotateDay = 30;
				}
				DB.Add(newRotateConfig);
			}

            var modelagreementConfig = await DB.AgreementConfigs.Where(o => o.ProjectID == id).FirstOrDefaultAsync();
			if (modelagreementConfig != null)
			{
                modelagreementConfig.ReasonCancelFQTQ = input?.ReasonCancelFQTQ;
                modelagreementConfig.ReasonCancelFQTQDateTime = input?.ReasonCancelFQTQDateTime;
                modelagreementConfig.ReasonCancelFQTQBy = userID;

                DB.Entry(modelagreementConfig).State = EntityState.Modified;
            }
			
			var unitFirst = await DB.Units.Where(o => o.ProjectID == id).FirstOrDefaultAsync();
			if(unitFirst != null)
			{
                unitFirst.Updated = DateTime.Now;
                DB.Entry(unitFirst).State = EntityState.Modified;
            }

            await DB.SaveChangesAsync();

			var result = new ProjectInfoDTO();
			return result;
		}

		private async Task<Guid> GeneralDataStatus(Guid projectID)
		{

			var model = await DB.Projects.Where(o => o.ID == projectID).FirstOrDefaultAsync();
			var projectAddressTypeKeySale = new List<string> { "1", "2" };
			var projectAddressTypeKeyTransfer = "3";
			var allProjectAdressSale = await DB.Addresses.Include(o => o.ProjectAddressType).Where(o => o.ProjectID == projectID && projectAddressTypeKeySale.Contains(o.ProjectAddressType.Key)).ToListAsync();
			var allProjectAdrresTransfer = await DB.Addresses.Include(o => o.ProjectAddressType).Where(o => o.ProjectID == projectID && o.ProjectAddressType.Key == projectAddressTypeKeyTransfer).ToListAsync();
			var generalDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstOrDefaultAsync(); //พร้อมขาย
			var generalDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstOrDefaultAsync(); //อยู่ระหว่างจัดเตรียม
			var generalDataStatusTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer).Select(o => o.ID).FirstOrDefaultAsync(); //พร้อมโอน
			var checkGeneralDataStatus = generalDataStatusPrepareMasterCenterID;

			if (allProjectAdressSale.Count() + allProjectAdrresTransfer.Count() == 0)
			{
				return generalDataStatusPrepareMasterCenterID; //อยู่ระหว่างจัดเตรียม
			}

			if (!string.IsNullOrEmpty(model.ProjectNo) //รหัสโครงการ
				&& !string.IsNullOrEmpty(model.SapCode) //รหัสโครงการ SAP
				&& !string.IsNullOrEmpty(model.ProjectNameTH) //ชื่อโครงการ (TH)
				&& !string.IsNullOrEmpty(model.ProjectNameEN) //ชื่อโครงการ (EN)
				&& model.ProductTypeMasterCenterID != null //ประเภทของโครงการ
				&& model.ProjectStatusMasterCenterID != null // สถานะการขายของโครงการ
				&& model.BGID != null //BG
				&& model.SubBGID != null //Sub BG
				&& model.BrandID != null //แบรนด์
										 //&& model.CompanyID != null //บริษัท
				&& !string.IsNullOrEmpty(model.CostCenterCode) //รหัส Cost Center
				&& !string.IsNullOrEmpty(model.ProfitCenterCode) //รหัส Profit Center
				&& allProjectAdressSale.Any()
				&& allProjectAdressSale.TrueForAll(o => o.ProjectAddressTypeMasterCenterID != null //ที่ตั้งโครงการ หรือ ที่ตั้งโฉนด
																								   //&& !string.IsNullOrEmpty(o.TitleDeedNo)
																								   //&& !string.IsNullOrEmpty(o.LandNo)
																								   //&& !string.IsNullOrEmpty(o.InspectionNo)
					&& !string.IsNullOrEmpty(o.PostalCode) //รหัสไปรษณีย์
					&& o.ProvinceID != null //จังหวัด
					&& o.DistrictID != null //อำเภอ/เขต
					&& o.SubDistrictID != null) //ตำบล/แขวง
				)
			{
				checkGeneralDataStatus = generalDataStatusSaleMasterCenterID; //พร้อมขาย
			}

			if (!string.IsNullOrEmpty(model.ProjectNo)
				&& !string.IsNullOrEmpty(model.SapCode)
				&& !string.IsNullOrEmpty(model.ProjectNameTH)
				&& !string.IsNullOrEmpty(model.ProjectNameEN)
				&& model.ProductTypeMasterCenterID != null
				&& model.ProjectStatusMasterCenterID != null
				&& model.BGID != null
				&& model.SubBGID != null
				&& model.BrandID != null
				//&& model.CompanyID != null
				&& !string.IsNullOrEmpty(model.CostCenterCode)
				&& !string.IsNullOrEmpty(model.ProfitCenterCode)
				&& allProjectAdrresTransfer.Any()
				&& allProjectAdressSale.TrueForAll(o => o.ProjectAddressTypeMasterCenterID != null
					//&& !string.IsNullOrEmpty(o.TitleDeedNo)
					//&& !string.IsNullOrEmpty(o.LandNo)
					//&& !string.IsNullOrEmpty(o.InspectionNo)
					&& !string.IsNullOrEmpty(o.PostalCode)
					&& o.ProvinceID != null
					&& o.DistrictID != null
					&& o.SubDistrictID != null)
				&& allProjectAdrresTransfer.TrueForAll(o => o.ProjectAddressType != null  //ที่ตั้งโฉนด(โอน)
																						  //&& !string.IsNullOrEmpty(o.TitleDeedNo)
																						  //&& !string.IsNullOrEmpty(o.LandNo)
																						  //&& !string.IsNullOrEmpty(o.InspectionNo)
						&& !string.IsNullOrEmpty(o.PostalCode)
						&& o.ProvinceID != null
						&& o.DistrictID != null
						&& o.HouseSubDistrictID != null
						&& o.TitledeedSubDistrictID != null
				))
			{
				checkGeneralDataStatus = generalDataStatusTransferMasterCenterID; //พร้อมโอน
			}

			return checkGeneralDataStatus;
		}

		private async Task<Guid> ProjectStatus(Guid projectID)
		{

			var model = await DB.Projects.Where(o => o.ID == projectID && o.LastMigrateDate == null).Include(o => o.ProductType).FirstOrDefaultAsync();
			var projectStatus = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectStatusMasterCenterID).FirstOrDefaultAsync();

			//สถานะข้อมูล
			var generalDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus"
														   && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstOrDefaultAsync(); //พร้อมขาย
			var generalDataStatusTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus"
											   && o.Key == ProjectDataStatusKeys.Transfer).Select(o => o.ID).FirstOrDefaultAsync(); //พร้อมโอน

			//สถานะโครงการ
			var ProjectStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectStatus"
														   && o.Key == ProjectStatusKeys.Preparing).Select(o => o.ID).FirstOrDefaultAsync(); //อยู่ระหว่างจัดเตรียม
			var ProjectStatusActiveMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectStatus"
											   && o.Key == ProjectStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync(); //อยู่ระหว่างขาย
																															  //var ProjectStatusInActiveMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectStatus"
																															  //								   && o.Key == ProjectStatusKeys.InActive).Select(o => o.ID).FirstOrDefaultAsync(); //ปิดการขาย

			var checkProjectStatus = projectStatus.Value;

			if (model != null)
			{
				if (model.ProductType.Key == ProductTypeKeys.HighRise)
				{
					if ((model?.GeneralDataStatusMasterCenterID == generalDataStatusSaleMasterCenterID || model?.GeneralDataStatusMasterCenterID == generalDataStatusTransferMasterCenterID)  //ข้อมูลทั่วไป
						&& model?.ModelDataStatusMasterCenterID == generalDataStatusSaleMasterCenterID //แบบบ้าน
						&& model?.TowerDataStatusMasterCenterID == generalDataStatusSaleMasterCenterID //ตึก
						&& model?.PictureDataStatusMasterCenterID == generalDataStatusSaleMasterCenterID //จัดการรูป
						&& model?.MinPriceDataStatusMasterCenterID == generalDataStatusSaleMasterCenterID //Minprice
						&& model?.BudgetProDataStatusMasterCenterID == generalDataStatusSaleMasterCenterID //BudgetProm
						)
					{
						checkProjectStatus = ProjectStatusActiveMasterCenterID;
					}
				}
				else
				{
					if ((model?.GeneralDataStatusMasterCenterID == generalDataStatusSaleMasterCenterID || model?.GeneralDataStatusMasterCenterID == generalDataStatusTransferMasterCenterID)  //ข้อมูลทั่วไป
						&& model?.ModelDataStatusMasterCenterID == generalDataStatusSaleMasterCenterID //แบบบ้าน
						&& model?.MinPriceDataStatusMasterCenterID == generalDataStatusSaleMasterCenterID //Minprice
						&& model?.BudgetProDataStatusMasterCenterID == generalDataStatusSaleMasterCenterID //BudgetProm
						)
					{
						checkProjectStatus = ProjectStatusActiveMasterCenterID;
					}
				}
			}
			return checkProjectStatus;
		}
	}
}
