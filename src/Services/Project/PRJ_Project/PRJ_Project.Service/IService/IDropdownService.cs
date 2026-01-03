
using Base.DTOs;
using Base.DTOs.PRJ;
using PagingExtensions;
using PRJ_Project.Params.Outputs;
namespace PRJ_Project.Services
{
    public interface IDropdownService : BaseInterfaceService
    {
        Task<List<ProjectDropdownDTO>> GetProjectDropdownListAsync(string name, Guid? companyID, bool isActive, bool ignoreRepurchase, string projectStatusKey, Guid? UserID = null, CancellationToken cancellationToken = default);
        Task<List<ProjectDropdownDTO>> GetProjectWalkReferDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, CancellationToken cancellationToken = default);
        Task<List<ProjectDropdownDTO>> GetProjectTitledeedRequestDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, Guid? productType, Guid? landOffice, CancellationToken cancellationToken = default);
        Task<List<ProjectDropdownDTO>> GetProjectByProductTypeDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, Guid? productType, CancellationToken cancellationToken = default);
        Task<List<ProjectDropdownDTO>> GetProjectAllStatusDropdownListAsync(string name, Guid? companyID, Guid? UserID = null, CancellationToken cancellationToken = default);
        Task<List<ProjectDropdownDTO>> GetProjectAllIsActiveDropdownListAsync(string name, Guid? companyID, Guid? UserID = null, CancellationToken cancellationToken = default);
        Task<List<ProjectDropdownDTO>> GetNonAuthProjectDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, Guid? UserID = null, CancellationToken cancellationToken = default);
    }
}
