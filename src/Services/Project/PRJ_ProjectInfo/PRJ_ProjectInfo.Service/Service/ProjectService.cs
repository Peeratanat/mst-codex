

using Common.Helper.Logging;
using Database.Models.DbQueries.PRJ;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PagingExtensions;
using PRJ_ProjectInfo.Params.Outputs;
using PRJ_ProjectInfo.Repositories;
using static Database.Models.PRJ.ProjectInformationModel;

namespace PRJ_ProjectInfo.Services
{
    public class ProjectService : IProjectService
    {
        public const string TAG = "ProjectService";
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IProjectRepository _ProjectRepo;

        public LogModel logModel { get; set; }
        private readonly Guid? CurrentUserID;

        public ProjectService(IHttpContextAccessor HttpContextAccessor, IProjectRepository projectRepo)
        {
            logModel = new LogModel("BudgetMinPriceService", null);
            _HttpContextAccessor = HttpContextAccessor;
            _ProjectRepo = projectRepo;

            Guid parsedUserID;
            if (Guid.TryParse(HttpContextAccessor?.HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                CurrentUserID = parsedUserID;
        }

        public async Task<ProjectInformationPaging.APIResult> GetProjectInformationListAsync(ProjectInformationPaging.Filter filter, ProjectInformationPaging.SortByParam sortByParam, PageParam pageParam, CancellationToken cancellationToken = default)
        {
            var Result = new ProjectInformationPaging.APIResult { DataResult = new List<ProjectInformationPaging.Result>(), PageResult = new PageOutput() };
            var Data = await _ProjectRepo.GetProjectInformationListAsync(filter, sortByParam, pageParam, cancellationToken);

            if (Data != null)
            {
                Result.DataResult = Data.DataResult.Select(o => ProjectInformationPaging.Result.ToModel(o)).ToList();
                Result.PageResult = new PageOutput { Page = Data.Page, PageSize = Data.PageSize, PageCount = Data.PageCount, RecordCount = Data.PageCount };
            }

            return Result;
        }

        public async Task<ProjectInformationModel.ResultProjectInformation> GetProjectInformationDetailAsync(Guid ProjectID, CancellationToken cancellationToken = default)
        {
            var Result = new ProjectInformationModel.ResultProjectInformation();

            var ProjectInfo = await _ProjectRepo.GetProjectInfoDetailAsync(ProjectID, cancellationToken) ?? new sql_ProjectInformation.Result();

            var ProjectInfoDetail = await _ProjectRepo.GetProjectInfoDetailDataAsync(ProjectID, cancellationToken) ?? new sql_ProjectInfoDetail.Result();

            var PMUser = await _ProjectRepo.GetvwPMUserAsync(ProjectID, cancellationToken) ?? new List<USR_vwPMUser>();
            var LCMUser = await _ProjectRepo.GetvwLCMUserDetailAsync(ProjectID, cancellationToken) ?? new List<USR_vwLCMUserDetail>();
            var LCUser = await _ProjectRepo.GetvwLCUserDetailAsync(ProjectID, cancellationToken) ?? new List<USR_vwLCUserDetail>();

            var LocationList = await _ProjectRepo.GetProjectInfoLocationDetailAsync(ProjectID, cancellationToken) ?? new List<sql_ProjectInforLocationDetail.Result>();

            var PromotionList = await _ProjectRepo.GetProjectInfoPromotionAsync(ProjectID, cancellationToken) ?? new List<PRJ_ProjectInfoPromotion>();
            PromotionList = PromotionList.Where(o => o.StartedDate <= DateTime.Now && o.EndDate >= DateTime.Now.Date).ToList();

            var BUType = "";
            if (ProjectInfo.BGNo == "1") BUType = "house";
            if (ProjectInfo.BGNo == "2") BUType = "townhome";
            if (ProjectInfo.BGNo == "3" || ProjectInfo.BGNo == "4") BUType = "condo";

            var CampaignList = await _ProjectRepo.GetProjectInfoCampaignAsync(BUType) ?? new List<PRJ_ProjectInfoCampaign>();
            CampaignList = CampaignList.Where(o => o.StartDate <= DateTime.Now && o.EndDate >= DateTime.Now.Date).ToList();

            Result = ProjectInformationModel.ToModel(ProjectInfo, ProjectInfoDetail, PMUser, LCMUser, LCUser, LocationList, PromotionList, CampaignList);

            return Result;
        }

        public async Task<List<DropdownListModel>> GetProjectBrandDDLAsync(Guid? ID, string TextSearch, CancellationToken cancellationToken = default)
        {
            var Result = new List<DropdownListModel>();

            var BrandList = await _ProjectRepo.GetProjectInfoBrandDDLAsync(cancellationToken) ?? new List<sql_ProjectInfoBrand.Result>();

            if (BrandList.Any())
                Result = BrandList.Select(p => new { p.BrandName })
                  .Select(o => new DropdownListModel
                  {
                      ID = o.BrandName,
                      Name = o.BrandName,
                      NameEN = o.BrandName,
                      FullName = o.BrandName,
                      Key = null,
                      Order = 0
                  }).OrderBy(o => o.Name).ToList();

            return Result;
        }

        public List<DropdownListModel> GetProjectTypeDDL(string TextSearch)
        {
            List<DropdownListModel> Result = [
                new() { ID = "1", Name = "บ้านเดี่ยว", Order = 1 },
                new() { ID = "2", Name = "ทาวน์เฮ้าส์", Order = 2 },
                new() { ID = "3", Name = "คอนโด", Order = 3 }
            ];

            if (!string.IsNullOrEmpty(TextSearch))
            {
                Result = Result.Where(o => o.Name.Contains(TextSearch)).ToList() ?? [];
            }

            return [.. Result.OrderBy(o => o.Order)];
        }

        public async Task<List<DropdownListModel>> GetProjectStatusDDLAsync(string TextSearch, CancellationToken cancellationToken = default)
        {
            var Result = new List<DropdownListModel>();

            var StatusList = await _ProjectRepo.GetMasterCenterAsync(null, TextSearch, MasterCenterGroupKeys.ProjectStatus, false, cancellationToken);

            if (StatusList.Count != 0)
                Result = StatusList.Select(o => new DropdownListModel
                {
                    ID = o.Key.ToString(),
                    Name = o.Name,
                    NameEN = o.NameEN,
                    FullName = o.Name,
                    Key = o.Key,
                    Order = o.Order
                }).OrderBy(o => o.Order).ToList();

            return Result;
        }

        public async Task<List<DropdownListModel>> GetProjectZoneDDLAsync(string TextSearch, CancellationToken cancellationToken = default)
        {
            var Result = new List<DropdownListModel>();

            var ZoneList = await _ProjectRepo.GetProjectZoneAsync(null, TextSearch, cancellationToken);

            if (ZoneList.Count != 0)
                Result = ZoneList.GroupBy(g => new { g.Description })
                    .Select(o => new DropdownListModel
                    {
                        ID = o.Key.Description,
                        Name = o.Key.Description,
                        NameEN = o.Key.Description,
                        FullName = o.Key.Description,
                        Key = null,
                        Order = 0
                    }).OrderBy(o => o.Name).ToList();

            return Result;
        }



        public async Task<bool> UpdateProjectAdminDescriptionAsync(UpdateProjectInfoModel Input)
        {

            var projectInfoDetail = (await _ProjectRepo.GetProjectInfoDestAsync(null, Input.ProjectID)).FirstOrDefault();
            var isInsert = false;

            var projectInfoDetailAdd = new ProjectInfoDetail();

            if (projectInfoDetail == null)
            {
                projectInfoDetailAdd.ID = Guid.NewGuid();
                projectInfoDetailAdd.ProjectID = Input.ProjectID;
                projectInfoDetailAdd.AdminDescription = Input.AdminDescription;
                projectInfoDetailAdd.IsDeleted = false;
                projectInfoDetailAdd.CreatedByUserID = CurrentUserID;
                projectInfoDetailAdd.Created = DateTime.Now;

                isInsert = true;
            }
            else
            {
                projectInfoDetail.AdminDescription = Input.AdminDescription;
                projectInfoDetail.UpdatedByUserID = CurrentUserID;
                projectInfoDetail.Updated = DateTime.Now;
            }

            var result = false;

            if (isInsert)
            {
                var resultInt = await _ProjectRepo.InsertProjecInfoDetailAsync([projectInfoDetailAdd]);
                result = resultInt;
            }
            else
            {
                result = await _ProjectRepo.UpdateProjecInfoDetailAsync([projectInfoDetail]);
            }
            return result;
        }


    }
}