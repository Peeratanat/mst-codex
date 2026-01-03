using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Outputs;
using Report.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_Project.Services
{
    public interface IProjectService : BaseInterfaceService
    {
        Task<ProjectDataStatusDTO> GetProjectDataStatusAsync(Guid projectID, CancellationToken cancellationToken = default); 
        Task<ProjectPaging> GetProjectListAsync(ProjectsFilter filter, PageParam pageParam, ProjectSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<ProjectDTO> GetProjectAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<ProjectDropdownDTO>> GetProjectDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, Guid? userID, CancellationToken cancellationToken = default);
        Task<ProjectCountDTO> GetProjectCountAsync(CancellationToken cancellationToken= default);
        Task<ProjectDTO> CreateProjectAsync(ProjectDTO input);
        Task DeleteProjectAsync(Guid projectID, string reason);
        Task<ReportResult> GetExportBookingTemplateUrlAsync(Guid projectID, CancellationToken cancellationToken = default);
        Task<ReportResult> GetExportAgreementTemplateUrlAsync(Guid projectID, CancellationToken cancellationToken = default);
        Task UpdateProjectStatus(Guid id, MasterCenterDropdownDTO projectStatus);
        Task<ProjectDTO> UpdateProjectStatusM(string projectNo, string projectStatusKey);
        Task SaveUserDefaultProject(ProfileUserDTO input, Guid? userID);
        Task<ProfileUserDTO> GetDefaultProjectAsync(Guid? userID, CancellationToken cancellationToken = default);
        Task<bool> UpdateProjectEvent(Guid projectID, bool input);
    }
}
