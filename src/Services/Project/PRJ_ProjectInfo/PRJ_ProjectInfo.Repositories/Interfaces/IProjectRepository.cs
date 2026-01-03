using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Database.Models.DbQueries.PRJ;
using Database.Models.MST;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_ProjectInfo.Params.Outputs;
using static Database.Models.PRJ.ProjectInformationModel;

namespace PRJ_ProjectInfo.Repositories
{
    public interface IProjectRepository : BaseInterfaceRepositories
    {
        Task<List<ProjectInfo>> GetProjectInfoAsync(Guid? ID, Guid? ProjectID, CancellationToken cancellationToken = default);

        Task<sql_ProjectInformation.PageResult> GetProjectInformationListAsync(ProjectInformationPaging.Filter filter, ProjectInformationPaging.SortByParam sortByParam, PageParam page, CancellationToken cancellationToken = default);

        Task<sql_ProjectInformation.Result> GetProjectInfoDetailAsync(Guid ProjectID, CancellationToken cancellationToken = default);

        Task<List<sql_ProjectInforLocationDetail.Result>> GetProjectInfoLocationDetailAsync(Guid ProjectID, CancellationToken cancellationToken = default);

        Task<List<PRJ_ProjectInfoPromotion>> GetProjectInfoPromotionAsync(Guid ProjectID, CancellationToken cancellationToken = default);

        Task<List<PRJ_ProjectInfoCampaign>> GetProjectInfoCampaignAsync(string BUType, CancellationToken cancellationToken = default);

        Task<List<Brand>> GetActiveMSTBrandAsync(Guid? ID, string TextSearch, CancellationToken cancellationToken = default);

        Task<List<MasterCenter>> GetMasterCenterAsync(Guid? ID, string Name, string MasterCenterGroupKey, bool? IsDeleted, CancellationToken cancellationToken = default);

        Task<List<Project>> GetProjectAsync(CancellationToken cancellationToken = default);

        Task<List<ProjectInfoLocation>> GetProjectZoneAsync(Guid? ID, string TextSearch, CancellationToken cancellationToken = default);

        Task<List<USR_vwPMUser>> GetvwPMUserAsync(Guid? ProjectID, CancellationToken cancellationToken = default);

        Task<List<USR_vwLCMUserDetail>> GetvwLCMUserDetailAsync(Guid? ProjectID, CancellationToken cancellationToken = default);

        Task<List<USR_vwLCUserDetail>> GetvwLCUserDetailAsync(Guid? ProjectID, CancellationToken cancellationToken = default);

        Task<List<ProjectInfoDetail>> GetProjectInfoDestAsync(Guid? ID, Guid? ProjectID, CancellationToken cancellationToken = default);

        Task<sql_ProjectInfoDetail.Result> GetProjectInfoDetailDataAsync(Guid ProjectID, CancellationToken cancellationToken = default);

        Task<bool> UpdateProjecInfoAsync(List<ProjectInfo> Input);

        Task<bool> UpdateProjecInfoDetailAsync(List<ProjectInfoDetail> Input);

        Task<bool> InsertProjecInfoDetailAsync(List<ProjectInfoDetail> Input);

        Task<List<sql_ProjectInfoBrand.Result>> GetProjectInfoBrandDDLAsync(CancellationToken cancellationToken = default);

    }
}
