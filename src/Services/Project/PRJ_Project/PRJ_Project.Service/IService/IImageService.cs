
using Base.DTOs;
using Base.DTOs.PRJ;
using PagingExtensions;
using PRJ_Project.Params.Outputs;
namespace PRJ_Project.Services
{
    public interface IImageService : BaseInterfaceService
    {
        Task<FileDTO> GetProjectLogoAsync(Guid projectID, CancellationToken cancellationToken = default);

        Task<FileDTO> UpdateProjectLogoAsync(Guid projectID, FileDTO input);

        Task  DeleteProjectLogoAsync(Guid projectID);

        Task<List<FloorPlanImageDTO>> GetFloorPlanImagesAsync(Guid projectID, string name = null, CancellationToken cancellationToken = default);

        Task<List<FloorPlanDetailDTO>> GetFloorPlanDetailAsync(Guid projectID, Guid? unitID = null, Guid? floorID = null, Guid? towerID = null, CancellationToken cancellationToken = default);

        Task<List<RoomPlanDetailDTO>> GetRoomPlanDetailAsync(Guid projectID, Guid? unitID = null, Guid? floorID = null, Guid? towerID = null, CancellationToken cancellationToken = default);

        Task<List<RoomPlanImageDTO>> GetRoomPlanImagesAsync(Guid projectID, string name = null, CancellationToken cancellationToken = default);

        Task<List<FloorPlanImageDTO>> SaveFloorPlanImagesAsync(Guid projectID, List<FloorPlanImageDTO> inputs);

        Task<List<RoomPlanImageDTO>> SaveRoomPlanImagesAsync(Guid projectID, List<RoomPlanImageDTO> inputs);
    }
}
