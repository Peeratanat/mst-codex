using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Services;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace PRJ_Project.API.Controllers
{
#if !DEBUG
     [Authorize(Policy = "MultiSchemePolicy")]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class RoundFeesController : BaseController
    {
        private IRoundFeeService RoundFeeService;
        private readonly DatabaseContext DB;
        private readonly ILogger<RoundFeesController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public RoundFeesController(
            DatabaseContext db,
            ILogger<RoundFeesController> logger,
            IHttpResultHelper httpResultHelper,
            IRoundFeeService roundFeeService)
        {
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            RoundFeeService = roundFeeService;
        }
        /// <summary>
        /// ลิสข้อมูล ค่าทำเนียมโอน-สูตรปัดเศษ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/RoundFees")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<RoundFeeDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoundFeeListAsync([FromRoute] Guid projectID, [FromQuery] RoundFeeFilter filter, [FromQuery] PageParam pageParam, [FromQuery] RoundFeeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await RoundFeeService.GetRoundFeeListAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.RoundFees, RoundFeeService.logModel);

        }
        /// <summary>
        /// ข้อมูล ค่าทำเนียมโอน-สูตรปัดเศษ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/RoundFees/{id}")]
        [ProducesResponseType(typeof(ResponseModel<RoundFeeDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectRoundFee([FromRoute] Guid projectID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await RoundFeeService.GetRoundFeeAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, RoundFeeService.logModel);
        }
        /// <summary>
        /// สร้าง ข้อมูล ค่าทำเนียมโอน-สูตรปัดเศษ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/RoundFees")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoundFeeDTO))]
        public async Task<IActionResult> CreateRoundFeeAsync([FromRoute] Guid projectID, [FromBody] RoundFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await RoundFeeService.CreateRoundFeeAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, RoundFeeService.logModel,HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateRoundFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูล ค่าทำเนียมโอน-สูตรปัดเศษ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/RoundFees/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoundFeeDTO))]
        public async Task<IActionResult> UpdateRoundFeeAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] RoundFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await RoundFeeService.UpdateRoundFeeAsync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, RoundFeeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateRoundFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูล ค่าทำเนียมโอน-สูตรปัดเศษ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/RoundFees/{id}")]
        public async Task<IActionResult> DeleteRoundFeeAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await RoundFeeService.DeleteRoundFeeAsync(projectID, id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteRoundFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}