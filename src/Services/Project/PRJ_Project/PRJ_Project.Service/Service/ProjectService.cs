using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using ExcelExtensions;
using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NPOI.HPSF;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Outputs;
using PRJ_Project.Services.Excels;
using Report.Integration;
using Report.Integration.PrintForms.MD;
using Report.Integration.Reports.MD;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage;
using Database.Models.MST;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PRJ_Project.Services
{
	public class ProjectService : IProjectService
	{
		private readonly IConfiguration Configuration;
		private readonly DatabaseContext DB;
		public LogModel logModel { get; set; }
		private FileHelper FileHelper;

		public ProjectService(IConfiguration configuration, DatabaseContext db)
		{
			logModel = new LogModel("ProjectService", null);
			this.DB = db;

			var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
			var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
			var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
			var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
			var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
			var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
			var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

			FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, Convert.ToBoolean(minioWithSSL));
			Configuration = configuration;
		}


		public async Task<ProjectDataStatusDTO> GetProjectDataStatusAsync(Guid id, CancellationToken cancellationToken = default)
		{
			var model = await DB.Projects.AsNoTracking()
										 .Include(o => o.GeneralDataStatus)
										 .Include(o => o.AgreementDataStatus)
										 .Include(o => o.ModelDataStatus)
										 .Include(o => o.TowerDataStatus)
										 .Include(o => o.UnitDataStatus)
										 .Include(o => o.TitleDeedDataStatus)
										 .Include(o => o.PictureDataStatus)
										 .Include(o => o.MinPriceDataStatus)
										 .Include(o => o.PriceListDataStatus)
										 .Include(o => o.TransferFeeDataStatus)
										 .Include(o => o.BudgetProDataStatus)
										 .Include(o => o.WaiveDataStatus)
										 .Include(o => o.ProjectStatus)
										 .FirstAsync(o => o.ID == id, cancellationToken);
			var result = ProjectDataStatusDTO.CreateFromModel(model);
			return result;
		}

		public async Task<ProjectPaging> GetProjectListAsync(ProjectsFilter filter, PageParam pageParam, ProjectSortByParam sortByParam, CancellationToken cancellationToken = default)
		{
			var userProjects = new List<UserAuthorizeProject>();

			if (filter.UserID != null)
			{
				userProjects = await DB.UserAuthorizeProjects.AsNoTracking()
									.Where(o => o.UserID == filter.UserID).ToListAsync();
			}

			IQueryable<ProjectQueryResult> query = DB.Projects.AsNoTracking()
													 //.Include(o => o.TitledeedConfig)
													 //.Where(o => o.TitledeedConfig != null)

													 .Select(o => new ProjectQueryResult
													 {
														 Project = o,
														 Brand = o.Brand,
														 Company = o.Company,
														 ProductType = o.ProductType,
														 ProjectStatus = o.ProjectStatus,
														 UpdatedBy = o.UpdatedBy,
														 TitledeedConfig = o.TitledeedConfig,
														 titledeedConfigUpdatedBy = o.TitledeedConfig.UpdatedBy
													 });


			#region Filter
			if (!string.IsNullOrEmpty(filter.ProjectNo))
			{
				query = query.Where(x => x.Project.ProjectNo.Contains(filter.ProjectNo));
			}
			if (!string.IsNullOrEmpty(filter.SapCode))
			{
				query = query.Where(x => x.Project.SapCode.Contains(filter.SapCode));
			}
			if (!string.IsNullOrEmpty(filter.ProjectNameTH))
			{
				query = query.Where(x => x.Project.ProjectNameTH.Contains(filter.ProjectNameTH));
			}
			if (!string.IsNullOrEmpty(filter.ProjectNameEN))
			{
				query = query.Where(x => x.Project.ProjectNameEN.Contains(filter.ProjectNameEN));
			}
			if (filter.BrandID != null && filter.BrandID != Guid.Empty)
			{
				query = query.Where(x => x.Project.BrandID == filter.BrandID);
			}
			if (filter.CompanyID != null && filter.CompanyID != Guid.Empty)
			{
				query = query.Where(x => x.Project.CompanyID == filter.CompanyID);
			}
			if (!string.IsNullOrEmpty(filter.ProductTypeKey))
			{
				var productTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ProductTypeKey
																	   && x.MasterCenterGroupKey == "ProductType")
																	  .Select(x => x.ID).FirstAsync();
				query = query.Where(x => x.Project.ProductTypeMasterCenterID == productTypeMasterCenterID);
			}
			if (!string.IsNullOrEmpty(filter.ProjectStatusKeys))
			{
				var projectStatusKeys = filter.ProjectStatusKeys.Split(',');
				var projectStatusMasterCenterIDs = await DB.MasterCenters.Where(x => projectStatusKeys.Contains(x.Key)
																		  && x.MasterCenterGroupKey == "ProjectStatus")
																		 .Select(x => x.ID).ToListAsync();
				query = query.Where(x => projectStatusMasterCenterIDs.Contains(x.Project.ProjectStatusMasterCenterID.Value));
			}
			if (filter.IsActive != null)
			{
				query = query.Where(x => x.Project.IsActive == filter.IsActive);

			}
			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(x => x.Project.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(x => x.Project.Updated <= filter.UpdatedTo);
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				query = query.Where(x => x.Project.Updated >= filter.UpdatedFrom && x.Project.Updated <= filter.UpdatedTo);
			}
			if (userProjects.Count > 0)
			{
				query = query.Where(x => userProjects.Select(s => s.ProjectID).Contains(x.Project.ID));
			}
			#endregion

			ProjectDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<ProjectQueryResult>(pageParam, ref query);

			var results = await query.Select(o => ProjectDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

			//นับจำนวน Unit
			var availableStatusIDs = await DB.MasterCenters
				.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && (o.Key == UnitStatusKeys.WaitingForConfirmBooking || o.Key == UnitStatusKeys.Available))
				.Select(o => o.ID).ToListAsync(cancellationToken);
			var bookingStatusIDs = await DB.MasterCenters
				.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && (o.Key == UnitStatusKeys.WaitingForAgreement || o.Key == UnitStatusKeys.WaitingForTransfer))
				.Select(o => o.ID).ToListAsync(cancellationToken);
			var transferStatusIDs = await DB.MasterCenters
				.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && (o.Key == UnitStatusKeys.Transfer || o.Key == UnitStatusKeys.PreTransfer))
				.Select(o => o.ID).ToListAsync(cancellationToken);


			var assetTypeId = await DB.MasterCenters
				.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.AssetType && (o.Key == AssetTypeKeys.Unit || o.Key == AssetTypeKeys.SampleModelHome)).Select(o => o.ID).ToListAsync(cancellationToken);
			foreach (var item in results)
			{
				item.UnitCount = new ProjectUnitCountDTO
				{
					Available = await DB.Units.CountAsync(o => o.ProjectID == item.Id && availableStatusIDs.Contains(o.UnitStatusMasterCenterID ?? Guid.Empty) && assetTypeId.Contains(o.AssetTypeMasterCenterID ?? Guid.Empty)),
					Booking = await DB.Units.CountAsync(o => o.ProjectID == item.Id && bookingStatusIDs.Contains(o.UnitStatusMasterCenterID ?? Guid.Empty) && assetTypeId.Contains(o.AssetTypeMasterCenterID ?? Guid.Empty)),
					Transfer = await DB.Units.CountAsync(o => o.ProjectID == item.Id && transferStatusIDs.Contains(o.UnitStatusMasterCenterID ?? Guid.Empty) && assetTypeId.Contains(o.AssetTypeMasterCenterID ?? Guid.Empty))
				};
				if (!string.IsNullOrEmpty(item.URLMasterPlan))
				{
					string masterPlan = await FileHelper.GetFileUrlAsync("projects", item.URLMasterPlan);
					item.URLMasterPlan = masterPlan;
				}
				var cmsConfig = await DB.GeneralConfigCMS.Where(o => o.ProjectID == item.Id).FirstOrDefaultAsync();
				if (cmsConfig != null)
				{
					if (!string.IsNullOrEmpty(cmsConfig.MasterPlanFilePath))
					{
						item.CMSMasterPlan = cmsConfig.MasterPlanFilePath;
					}
				}
				item.IsEventUse = await DB.ProjectInEvents.Include(o => o.Event).Where(o => o.ProjectID == item.Id && o.Event.EventDateTo < DateTime.Today).AnyAsync();


            }

			return new ProjectPaging()
			{
				PageOutput = pageOutput,
				Projects = results
			};
		}

		public async Task<ProjectDTO> GetProjectAsync(Guid id, CancellationToken cancellationToken = default)
		{
			var model = await DB.Projects
				.Include(o => o.Brand)
				.Include(o => o.Company)
				.Include(o => o.ProductType)
				.Include(o => o.ProjectStatus)
				.Include(o => o.UpdatedBy)
				.Include(o => o.BG)
				.FirstAsync(o => o.ID == id, cancellationToken);
			var result = ProjectDTO.CreateFromModel(model);

			return result;
		}

		public async Task<List<ProjectDropdownDTO>> GetProjectDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, Guid? UserID = null, CancellationToken cancellationToken = default)
		{
			IQueryable<Database.Models.PRJ.Project> query = null;

			if (UserID.HasValue)
			{
				query = DB.UserAuthorizeProjects.AsNoTracking()
						.Include(o => o.Project)
						.Where(o => o.Project.IsActive == isActive && o.UserID == UserID).Select(p => p.Project);
			}
			else
			{
				query = DB.Projects.AsNoTracking();
			}

			query = query.Include(o => o.ProjectStatus)
							.Include(o => o.ProductType)
							.Include(o => o.BG)
							.Include(o => o.SubBG)
							.Where(o => o.IsActive == isActive && o.SubBG.SubBGNo.Substring(2, 1) != "0");

			if (!string.IsNullOrEmpty(name))
			{
				name = name.Replace("-", "");
				name = name.ToLower();
				query = query.Where(o => ((o.ProjectNo ?? "") + (o.ProjectNameTH ?? "")).ToLower().Contains(name));
			}

			if (companyID != null && companyID != Guid.Empty)
				query = query.Where(o => o.CompanyID == companyID);

			if (!string.IsNullOrEmpty(projectStatusKey))
			{
				var projectStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == projectStatusKey
																	  && o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus)
																	 .Select(o => o.ID).FirstAsync();
				query = query.Where(o => o.ProjectStatusMasterCenterID == projectStatusMasterCenterID);
			}

			return await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH).Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
		}

		public async Task<ProjectCountDTO> GetProjectCountAsync(CancellationToken cancellationToken = default)
		{

			var projects = await DB.Projects
				.Where(o => !o.IsDeleted)
				.Select(o => new { o.IsActive })
				.ToListAsync(cancellationToken);

			var result = new ProjectCountDTO
			{
				All = projects.Count,
				Inactive = projects.Count(o => !o.IsActive),
				Active = projects.Count(o => o.IsActive)
			};
			return result;
		}

		public async Task<ProjectDTO> CreateProjectAsync(ProjectDTO input)
		{
			await input.ValidateAsync(DB);
			Project model = new Project();
			input.ToModel(ref model);

			var projectStatusPrepareID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectStatus" && o.Key == "0")).ID;
			var projectDataStatusPrepareID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == "0")).ID;
			model.ProjectStatusMasterCenterID = projectStatusPrepareID;
			model.IsForeignProject = false;

			#region Tab
			model.GeneralDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.AgreementDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.ModelDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.TowerDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.UnitDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.TitleDeedDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.PictureDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.MinPriceDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.PriceListDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.TransferFeeDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.BudgetProDataStatusMasterCenterID = projectDataStatusPrepareID;
			model.WaiveDataStatusMasterCenterID = projectDataStatusPrepareID;
			#endregion

			AgreementConfig agreement = new AgreementConfig
			{
				ProjectID = model.ID,
				IsPrintAgreementForBuyer = true,
				IsPrintAgreementForSeller = input.ProductType?.Key == ProductTypeKeys.HighRise ? true : false,
				IsPrintAgreementForRevenue = true,
				IsPrintAgreementEmpty = true

			};

			await DB.Projects.AddAsync(model);
			await DB.AgreementConfigs.AddAsync(agreement);
			await DB.SaveChangesAsync();

			var data = await DB.Projects
									  .Include(o => o.Brand)
									  .Include(o => o.Company)
									  .Include(o => o.ProductType)
									  .Include(o => o.ProjectStatus)
									  .FirstAsync(o => o.ID == model.ID);

			var result = ProjectDTO.CreateFromModel(data);
			return result;
		}

		public async Task DeleteProjectAsync(Guid id, string reason)
		{
			var model = await DB.Projects.FindAsync(id);
			model.DeleteReason = reason;
			model.IsDeleted = true;
			await DB.SaveChangesAsync();
		}


		public async Task<ReportResult> GetExportBookingTemplateUrlAsync(Guid projectID, CancellationToken cancellationToken = default)
		{
			var project = await DB.Projects.Include(o => o.ProductType).FirstOrDefaultAsync(o => o.ID == projectID);
			var agreement = await DB.AgreementConfigs.FirstOrDefaultAsync(o => o.ProjectID == projectID);
			var reportName = string.Empty;

			var lcm = await DB.UserAuthorizeProject_LCMs.Include(o => o.User).Where(o => o.ProjectID == projectID).OrderBy(o => o.User.EmployeeNo).FirstOrDefaultAsync();

			FileDTO signImageFile = null;

            if (!string.IsNullOrEmpty(lcm?.User.EmployeeNo))
            {
                signImageFile = await FileDTO.CreateFromBucketandFileNameAsync("sales", "SignImage/" + lcm.User.EmployeeNo + ".png", FileHelper);
            }


            var SignatureURL = (signImageFile != null) ? signImageFile.Url : "";

            if (project.ProductType?.Key == ProductTypeKeys.LowRise) //แนวราบ
			{
				if (agreement.IsNotLicenseLand) //ไม่จัดสรร
				{
					reportName = "PF_AG_003_NotAllocate_TMP";
				}
				else
				{
					reportName = "PF_AG_003_TMP";
				}
			}
			else
			{
				if (project.IsForeignProject == null)
				{
					project.IsForeignProject = false;
				}

				if (project.IsForeignProject.Value) //FQTH
				{
					reportName = "PF_AG_002ThEngV2_TMP";
				}
				else
				{
					reportName = "PF_AG_002ThEngV2_NormalTMP";
				}
			}

			ReportFactory reportFactory = new ReportFactory(Configuration, ReportFolder.AG, reportName, ShowAs.PDF);
			reportFactory.AddParameter("@ProjectID", projectID);
			if (project.ProductType?.Key != ProductTypeKeys.LowRise)
			{
                //reportFactory.AddParameter("@LCMName", lcm?.User.DisplayName);
                //reportFactory.AddParameter("@LCMSignatureURL", SignatureURL);
            }

            return reportFactory.CreateUrl();

		}

		public async Task<ReportResult> GetExportAgreementTemplateUrlAsync(Guid projectID, CancellationToken cancellationToken = default)
		{
			var project = await DB.Projects.Include(o => o.ProductType).FirstOrDefaultAsync(o => o.ID == projectID);
			var agreement = await DB.AgreementConfigs.FirstOrDefaultAsync(o => o.ProjectID == projectID);
			var reportName = string.Empty;
			if (project.ProductType?.Key == ProductTypeKeys.LowRise) //แนวราบ
			{
				if (agreement.IsNotLicenseLand) //ไม่จัดสรร
				{
					reportName = "PF_AG_005_NotAllocate_TMP";
				}
				else
				{
					reportName = "PF_AG_005_TMP";
				}
			}
			else
			{
				if (project.IsForeignProject.GetValueOrDefault())  //FQTH
				{
					reportName = "PF_AG_004ThEngV2_TMP";
				}
				else
				{
					reportName = "PF_AG_004ThEngV2_NormalTMP";
				}
			}

			ReportFactory reportFactory = new ReportFactory(Configuration, ReportFolder.AG, reportName, ShowAs.PDF);
			reportFactory.AddParameter("@ProjectID", projectID);
			return reportFactory.CreateUrl();

		}

		public async Task UpdateProjectStatus(Guid projectID, MasterCenterDropdownDTO projectStatus)
		{
			var project = await DB.Projects.Include(o => o.ProjectStatus).Include(o => o.ProductType)
										   .FirstAsync(o => o.ID == projectID);
			ValidateException ex = new ValidateException();

			if (project.ProjectStatus.Key == ProjectStatusKeys.Active && projectStatus.Key == ProjectStatusKeys.InActive) //อยู่ระหว่างขาย และต้องการ ปิดการขาย
			{
				project.ProjectStatusMasterCenterID = projectStatus?.Id;
				project.IsActive = false;

				DB.Update(project);
				await DB.SaveChangesAsync();
			}
            else if (project.ProjectStatus.Key == ProjectStatusKeys.Preparing && projectStatus.Key == ProjectStatusKeys.Active) // เพิ่มปรับโครงการจาก พร้อมขาย เป็น ขาย
            {
                project.ProjectStatusMasterCenterID = projectStatus?.Id;
                // freedown
                if (project.PercentFreeDown.GetValueOrDefault() <= 0)
                    project.PercentFreeDown = 25;

                if (project.MaxFreeDownAmount.GetValueOrDefault() <= 0)
                    project.MaxFreeDownAmount = 999999999;
                if (project.ProductType.Key == ProductTypeKeys.HighRise && project.IsNewProject == false)
                {
                    project.IsNewProject = true;
                }
                DB.Update(project);
                await DB.SaveChangesAsync();
            }
            else
			{
				var errMsg = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR9999");
				string desc = "สามารถแก้ไขสถานะโครงการได้เฉพาะ อยู่ระหว่างขาย ไปเป็นปิดการขาย เท่านั้น";
				var msg = errMsg.Message.Replace("[message]", desc);
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}
			if (ex.HasError)
			{
				throw ex;
			}
		}

		public async Task<ProjectDTO> UpdateProjectStatusM(string projectNo, string projectStatusKey)
		{
			var project = await DB.Projects.FirstOrDefaultAsync(o => o.ProjectNo == projectNo);

			var projectStatus = await DB.MasterCenters.FirstOrDefaultAsync(o => o.Key == projectStatusKey && o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus);

			ValidateException ex = new ValidateException();
			if (project != null && projectStatus != null)
			{
				project.ProjectStatusMasterCenterID = projectStatus.ID;
				//project.IsActive = true;

				DB.Update(project);
				await DB.SaveChangeAsync();
			}
			else
			{
				string msg = "ระบุรหัสโครงการ Ex.10001 หรือระบุสถานะ จัดเตรียม=0, เปิดขาย=1, ปิดขาย=2";
				ex.AddError("", msg, 0);
			}

			if (ex.HasError)
			{
				throw ex;
			}

			var projectR = await DB.Projects
				  .Include(o => o.Brand)
				  .Include(o => o.Company)
				  .Include(o => o.ProductType)
				  .Include(o => o.ProjectStatus)
			 .FirstOrDefaultAsync(o => o.ProjectNo == projectNo);

			var result = ProjectDTO.CreateFromModel(projectR);

			return result;

		}

		public async Task SaveUserDefaultProject(ProfileUserDTO input, Guid? UserID = null)
		{
			ValidateException ex = new ValidateException();
			if (input != null)
			{
				var userDefaultProjectExist = await DB.UserDefaultProjects.FirstOrDefaultAsync(o => o.UserID == UserID.Value);
				if (userDefaultProjectExist != null)
				{
					userDefaultProjectExist.UserID = UserID.Value;
					if (input.Project == null || input.Project.Id == null)
					{
						userDefaultProjectExist.IsDeleted = true;
					}
					else
					{
						userDefaultProjectExist.ProjectID = input.Project.Id.Value;
						userDefaultProjectExist.IsDeleted = false;
					}

					DB.Update(userDefaultProjectExist);
					await DB.SaveChangesAsync();
				}
				else
				{
					UserDefaultProject userDefaultAdd = new UserDefaultProject
					{
						UserID = UserID.Value,
						ProjectID = input.Project.Id.Value,
						IsDeleted = false
					};
					await DB.UserDefaultProjects.AddAsync(userDefaultAdd);
					await DB.SaveChangesAsync();
				}

				var user = await DB.Users.FirstOrDefaultAsync(o => o.ID == UserID.Value);
				if (user != null && input.User != null)
				{
					user.LineId = input.User?.LineId;
					user.PhoneNo = input.User?.PhoneNo;
					DB.Update(user);
					await DB.SaveChangesAsync();
				}
			}
			else
			{
				var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR9999");
				string desc = "ไม่สามารถบันทึกข้อมูลโปรไฟล์ได้";
				var msg = errMsg.Message.Replace("[message]", desc);
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}
			if (ex.HasError)
			{
				throw ex;
			}
		}

		public async Task<ProfileUserDTO> GetDefaultProjectAsync(Guid? UserID = null, CancellationToken cancellationToken = default)
		{
			var userDefaultProject = await DB.UserDefaultProjects.FirstOrDefaultAsync(o => o.UserID == UserID, cancellationToken);
			var userProfile = await DB.Users.FirstOrDefaultAsync(o => o.ID == UserID, cancellationToken);

			ProfileUserDTO profileUser = new ProfileUserDTO();
			profileUser.Project = new ProjectDropdownDTO();

			if (userDefaultProject != null)
			{
				profileUser.Project.Id = userDefaultProject.ProjectID;

			}
			if (userProfile != null)
			{
				profileUser.User = UserDTO.CreateFromModel(userProfile);
			}
			return profileUser;
		}


		public async Task<bool> UpdateProjectEvent(Guid projectID, bool input)
		{
			var result = false;

			var project = await DB.Projects.Where(o => o.ID == projectID).FirstOrDefaultAsync();
			var projectinevents = await DB.ProjectInEvents.Where(o => o.ProjectID == projectID).FirstOrDefaultAsync();
			if (projectinevents != null)
			{
				var events = await DB.Event.Where(o => o.ID == projectinevents.EventID).IgnoreQueryFilters().FirstOrDefaultAsync();
				if (events != null)
				{
					events.IsDeleted = !input;
					events.EventDateFrom = DateTime.Now;
					events.EventDateTo = DateTime.Now.AddYears(1);
					DB.Event.Update(events);
				}

			}
			else
			{
				Guid eventID = Guid.NewGuid();
				ProjectInEvent projectInEvent = new ProjectInEvent();
				projectInEvent.ProjectID = projectID;
				projectInEvent.EventID = eventID;

				await DB.ProjectInEvents.AddAsync(projectInEvent);

				Event _event = new Event();
				_event.ID = eventID;


				_event.NameTH = project.ProjectNameTH;
				_event.NameEN = project.ProjectNameEN;

				_event.EventDateFrom = DateTime.Now;
				_event.EventDateTo = DateTime.Now.AddYears(1);
                _event.Isactive = true;
				await DB.Event.AddAsync(_event);
			}
			await DB.SaveChangesAsync();
			return result;

		}

	}

}