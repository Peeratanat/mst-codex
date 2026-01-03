using Base.DTOs.PRJ;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Outputs;

namespace PRJ_Project.Services
{
    public interface ITowerService : BaseInterfaceService
    {

        Task<List<TowerDropdownDTO>> GetTowerDropdownListAsync(Guid projectID, string code, CancellationToken cancellationToken = default);
        Task<TowerPaging> GetTowerListAsync(Guid projectID, TowerFilter filter, PageParam pageParam, TowerSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<TowerDTO> GetTowerAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<TowerDTO> CreateTowerAsync(Guid projectID, TowerDTO input);
        Task<TowerDTO> UpdateTowerAsync(Guid projectID, Guid id, TowerDTO input);
        Task DeleteTowerAsync(Guid projectID, Guid id);

    }
}
