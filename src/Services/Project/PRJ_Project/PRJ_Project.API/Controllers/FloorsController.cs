using System.Net;
using Base.DTOs;
using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Inputs;
using PRJ_Project.Services;
using Microsoft.AspNetCore.Authorization;
namespace PRJ_Project.API.Controllers
{
#if !DEBUG
     [Authorize(Policy = "MultiSchemePolicy")]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class FloorsController : BaseController
    {
        private IFloorService FloorService;
        private readonly DatabaseContext DB;
        private readonly ILogger<FloorsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public FloorsController(
            DatabaseContext db,
            ILogger<FloorsController> logger,
            IHttpResultHelper httpResultHelper,
            IFloorService floorService)
        {
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            FloorService = floorService;
        }


        /// <summary>
        /// ลิสของชั้น Dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/Floors/DropdownList")]
        [ProducesResponseType(typeof(ResponseModel<List<FloorDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFloorDropdown([FromRoute] Guid projectID, [FromQuery] Guid? towerID = null, [FromQuery] string name = null, CancellationToken cancellationToken = default)
        {
            var result = await FloorService.GetFloorDropdownListAsync(projectID, towerID, name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, FloorService.logModel);
        }

        /// <summary>
        /// ลิสของชั้น Dropdown forEventbooking
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/FloorsEventBooking/DropdownList")]
        [ProducesResponseType(typeof(ResponseModel<List<FloorDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFloorEventBookingDropdown([FromRoute] Guid projectID, [FromQuery] Guid? towerID = null, [FromQuery] string name = null, CancellationToken cancellationToken = default)
        {
            var result = await FloorService.GetFloorEventBookingDropdownListAsync(projectID, towerID, name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, FloorService.logModel);
        }
        /// <summary>
        /// ลิสของชั้น
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/{towerID}/Floors")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<FloorDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFloorList([FromRoute] Guid projectID, [FromRoute] Guid towerID, [FromQuery] FloorsFilter filter, [FromQuery] PageParam pageParam, [FromQuery] FloorSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await FloorService.GetFloorListAsync(projectID, towerID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Floors, FloorService.logModel);
        }
        /// <summary>
        /// ข้อมูลชั้น
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/{towerID}/Floors/{id}")]
        [ProducesResponseType(typeof(ResponseModel<FloorDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFloor([FromRoute] Guid projectID, [FromRoute] Guid towerID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await FloorService.GetFloorAsync(projectID, towerID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, FloorService.logModel);
        }
        /// <summary>
        /// เพิ่มชั้น
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Towers/{towerID}/Floors")]
        [ProducesResponseType(typeof(ResponseModel<FloorDTO>), StatusCodes.Status201Created)]

        public async Task<IActionResult> CreateFloorAsync([FromRoute] Guid projectID, [FromRoute] Guid towerID, [FromBody] FloorDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await FloorService.CreateFloorAsync(projectID, towerID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, FloorService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateFloorAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// เพิ่มชั้นทีละหลายชั้น
        /// </summary>
        /// <returns>The multiple floors.</returns>
        /// <param name="projectID">Project identifier.</param>
        /// <param name="towerID">Tower identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPost("{projectID}/Towers/{towerID}/Floors/Multiple")]
        [ProducesResponseType(typeof(ResponseModel<List<FloorDTO>>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMultipleFloorAsync([FromRoute] Guid projectID, [FromRoute] Guid towerID, [FromBody] CreateMultipleFloorInput input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await FloorService.CreateMultipleFloorAsync(projectID, towerID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, FloorService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMultipleFloorAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขชั้น
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Towers/{towerID}/Floors/{id}")]
        [ProducesResponseType(typeof(ResponseModel<FloorDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateFloorAsync([FromRoute] Guid projectID, [FromRoute] Guid towerID, [FromRoute] Guid id, [FromBody] FloorDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await FloorService.UpdateFloorAsync(projectID, towerID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, FloorService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateFloorAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบชั้น
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/Towers/{towerID}/Floors/{id}")]
        public async Task<IActionResult> DeleteFloorAsync([FromRoute] Guid projectID, [FromRoute] Guid towerID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await FloorService.DeleteFloorAsync(projectID, towerID, id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, FloorService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteFloorAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Import Floors Excel
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseModel<FloorExcelDTO>), StatusCodes.Status200OK)]
        [HttpPost("{projectID}/Towers/{towerID}/Floors/Import")]
        public async Task<IActionResult> ImportFloorAsync([FromRoute] Guid projectID, [FromRoute] Guid towerID, [FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await FloorService.ImportFloorAsync(projectID, towerID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, FloorService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportFloorAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Export Floor
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/{towerID}/Floors/Export")]
        [ProducesResponseType(typeof(ResponseModel<FileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportExcelFloorAsync([FromRoute] Guid projectID, [FromRoute] Guid towerID, [FromQuery] FloorsFilter filter, [FromQuery] FloorSortByParam sortByParam)
        {
            try
            {
                var result = await FloorService.ExportExcelFloorAsync(projectID, towerID, filter, sortByParam);
                return await _httpResultHelper.SuccessCustomResult(result, FloorService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportExcelFloorAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}