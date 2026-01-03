using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_ProjectInfo.Params.Outputs;


namespace PRJ_ProjectInfo.Services
{
    public interface IProjectService : BaseInterfaceService
    { 

        Task<ProjectInformationPaging.APIResult> GetProjectInformationListAsync(ProjectInformationPaging.Filter filter, ProjectInformationPaging.SortByParam sortByParam, PageParam pageParam, CancellationToken cancellationToken = default);

        Task<ProjectInformationModel.ResultProjectInformation> GetProjectInformationDetailAsync(Guid ProjectID, CancellationToken cancellationToken = default);

        Task<List<DropdownListModel>> GetProjectBrandDDLAsync(Guid? ID, string TextSearch, CancellationToken cancellationToken = default);

        List<DropdownListModel> GetProjectTypeDDL(string TextSearch);

        Task<List<DropdownListModel>> GetProjectStatusDDLAsync(string TextSearch, CancellationToken cancellationToken = default);

        Task<List<DropdownListModel>> GetProjectZoneDDLAsync(string TextSearch, CancellationToken cancellationToken = default);

        Task<bool> UpdateProjectAdminDescriptionAsync(UpdateProjectInfoModel Input);

    }
}