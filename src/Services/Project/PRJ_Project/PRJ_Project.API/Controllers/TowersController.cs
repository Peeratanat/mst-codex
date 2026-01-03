using System.Net;
using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Services;
using Microsoft.AspNetCore.Authorization;
namespace PRJ_Project.API.Controllers
{
#if !DEBUG
     [Authorize(Policy = "MultiSchemePolicy")]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class TowersController : BaseController
    {
        private ITowerService TowerService;
        private readonly DatabaseContext DB;
        private readonly ILogger<TowersController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public TowersController(
            DatabaseContext db,
            ILogger<TowersController> logger,
            IHttpResultHelper httpResultHelper,
            ITowerService towerService)
        {
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            TowerService = towerService;
        }
        /// <summary>
        /// ลิสข้อมูลตึก Dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/DropdownList")]
        [ProducesResponseType(typeof(ResponseModel<List<TowerDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTowerDropdownListAsync([FromRoute] Guid projectID, [FromQuery] string code, CancellationToken cancellationToken = default)
        {
            var result = await TowerService.GetTowerDropdownListAsync(projectID, code, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, TowerService.logModel);
        }
        /// <summary>
        /// ลิสข้อมูลตึก
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<TowerDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTowerListAsync([FromRoute] Guid projectID, [FromQuery] TowerFilter filter, [FromQuery] PageParam pageParam, [FromQuery] TowerSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await TowerService.GetTowerListAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Towers, TowerService.logModel);
        }
        /// <summary>
        /// ข้อมูลตึก
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/{id}")]
        [ProducesResponseType(typeof(ResponseModel<TowerDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTowerAsync([FromRoute] Guid projectID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await TowerService.GetTowerAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, TowerService.logModel);

        }
        /// <summary>
        /// สร้างข้อมูลตึก
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Towers")]
        [ProducesResponseType(typeof(ResponseModel<TowerDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTowerAsync([FromRoute] Guid projectID, [FromBody] TowerDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await TowerService.CreateTowerAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, TowerService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateTowerAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลตึก
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Towers/{id}")]
        [ProducesResponseType(typeof(ResponseModel<TowerDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTowerAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] TowerDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await TowerService.UpdateTowerAsync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, TowerService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateTowerAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบข้อมูลตึก
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/Towers/{id}")]
        public async Task<IActionResult> DeleteTowerAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await TowerService.DeleteTowerAsync(projectID, id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteTowerAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
