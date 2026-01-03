
using Base.DTOs;
using Base.DTOs.PRJ;
using PagingExtensions;
using PRJ_Project.Params.Outputs;
namespace PRJ_Project.Services
{
    public interface IAddressService : BaseInterfaceService
    {
        Task<List<ProjectAddressListDTO>> GetProjectAddressDropdownListAsync(Guid projectID, string name, string projectAddressTypeKey, CancellationToken cancellationToken = default);
        Task<AddressPaging> GetProjectAddressListAsync(Guid projectID, PageParam pageParam, SortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<ProjectAddressDTO> GetProjectAddressAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ProjectAddressDTO> CreateProjectAddressAsync(Guid projectID, ProjectAddressDTO input);
        Task<ProjectAddressDTO> UpdateProjectAddressAsync(Guid projectID, Guid id, ProjectAddressDTO input);
        Task DeleteProjectAddressAsync(Guid id);

         
    }
}
