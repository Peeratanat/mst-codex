using Base.DTOs;
using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using PRJ_Project.Services;
using System.Net;
using Microsoft.AspNetCore.Authorization;
namespace PRJ_Project.API.Controllers
{
#if !DEBUG
     [Authorize(Policy = "MultiSchemePolicy")]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class ImagesController : BaseController
    {
        private IImageService ImageService;
        private readonly DatabaseContext DB;
        private readonly ILogger<ImagesController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public ImagesController(
            DatabaseContext db,
            ILogger<ImagesController> logger,
            IHttpResultHelper httpResultHelper,
            IImageService imageService)
        {
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            ImageService = imageService;
        }
        [HttpGet("{projectID}/Logo")]
        [ProducesResponseType(typeof(ResponseModel<FileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectLogoAsync([FromRoute] Guid projectID,
         CancellationToken cancellationToken = default)
        {
            var result = await ImageService.GetProjectLogoAsync(projectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ImageService.logModel);
        }

        [HttpPut("{projectID}/Logo")]
        [ProducesResponseType(typeof(ResponseModel<FileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProjectLogoAsync([FromRoute] Guid projectID, FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ImageService.UpdateProjectLogoAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ImageService.logModel);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        [HttpDelete("{projectID}/Logo")]
        public async Task<IActionResult> DeleteProjectLogo([FromRoute] Guid projectID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await ImageService.DeleteProjectLogoAsync(projectID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, ImageService.logModel);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        [HttpGet("{projectID}/FloorPlanImages")]
        [ProducesResponseType(typeof(ResponseModel<List<FloorPlanImageDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFloorPlanImages([FromRoute] Guid projectID, [FromQuery] string name = null,
         CancellationToken cancellationToken = default)
        {
            var results = await ImageService.GetFloorPlanImagesAsync(projectID, name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, ImageService.logModel);
        }

        [HttpGet("{projectID}/FloorPlanDetail")]
        [ProducesResponseType(typeof(ResponseModel<List<FloorPlanDetailDTO>>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetFloorPlanDetail([FromRoute] Guid projectID,
        [FromQuery] Guid? unitID = null,
        [FromQuery] Guid? floorID = null,
        [FromQuery] Guid? towerID = null,
         CancellationToken cancellationToken = default)
        {
            var results = await ImageService.GetFloorPlanDetailAsync(projectID, unitID, floorID, towerID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, ImageService.logModel);
        }

        [HttpGet("{projectID}/RoomPlanDetail")]
        [ProducesResponseType(typeof(ResponseModel<List<RoomPlanDetailDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomPlanDetail([FromRoute] Guid projectID,
        [FromQuery] Guid? unitID = null,
        [FromQuery] Guid? floorID = null,
        [FromQuery] Guid? towerID = null,
         CancellationToken cancellationToken = default)
        {
            var results = await ImageService.GetRoomPlanDetailAsync(projectID, unitID, floorID, towerID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, ImageService.logModel);
        }

        [HttpGet("{projectID}/RoomPlanImages")]
        [ProducesResponseType(typeof(ResponseModel<List<RoomPlanImageDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomPlanImage([FromRoute] Guid projectID, [FromQuery] string name = null,
         CancellationToken cancellationToken = default)
        {
            var results = await ImageService.GetRoomPlanImagesAsync(projectID, name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, ImageService.logModel);
        }

        [HttpPost("{projectID}/FloorPlanImages")]
        [ProducesResponseType(typeof(ResponseModel<List<FloorPlanImageDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveFloorPlanImages([FromRoute] Guid projectID, List<FloorPlanImageDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ImageService.SaveFloorPlanImagesAsync(projectID, inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ImageService.logModel);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        [HttpPost("{projectID}/RoomPlanImages")]
        [ProducesResponseType(typeof(ResponseModel<List<RoomPlanImageDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveRoomPlanImage([FromRoute] Guid projectID, List<RoomPlanImageDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ImageService.SaveRoomPlanImagesAsync(projectID, inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ImageService.logModel);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}