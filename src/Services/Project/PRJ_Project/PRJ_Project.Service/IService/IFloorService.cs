using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Inputs;
using PRJ_Project.Params.Outputs;

namespace PRJ_Project.Services
{
    public interface IFloorService : BaseInterfaceService
    {
        Task<List<FloorDropdownDTO>> GetFloorDropdownListAsync(Guid projectID, Guid? towerID, string name, CancellationToken cancellationToken = default);
        Task<List<FloorDropdownDTO>> GetFloorEventBookingDropdownListAsync(Guid projectID, Guid? towerID, string name, CancellationToken cancellationToken = default);
        Task<FloorPaging> GetFloorListAsync(Guid projectID, Guid towerID, FloorsFilter filter, PageParam pageParam, FloorSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<FloorDTO> GetFloorAsync(Guid projectID, Guid towerID, Guid id, CancellationToken cancellationToken = default);
        Task<FloorDTO> CreateFloorAsync(Guid projectID, Guid towerID, FloorDTO input);
        Task<List<FloorDTO>> CreateMultipleFloorAsync(Guid projectID, Guid towerID, CreateMultipleFloorInput input);
        Task<FloorDTO> UpdateFloorAsync(Guid projectID, Guid towerID, Guid id, FloorDTO input);
        Task<Floor> DeleteFloorAsync(Guid projectID, Guid towerID, Guid id);
        Task<FileDTO> ExportExcelFloorAsync(Guid projectID, Guid towerID, FloorsFilter filter, FloorSortByParam sortByParam);
        Task<FloorExcelDTO> ImportFloorAsync(Guid projectID, Guid towerID, FileDTO input);
    }
}
