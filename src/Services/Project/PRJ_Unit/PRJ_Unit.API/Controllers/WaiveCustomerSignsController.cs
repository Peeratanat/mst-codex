using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using PRJ_Unit.API;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class WaiveCustomerSignsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<WaiveCustomerSignsController> _logger;
        private readonly IWaiveCustomerSignService WaiveCustomerSignsService;
        private readonly IWaiveQCService WaiveQCService;
        private readonly IHttpResultHelper _httpResultHelper;

        public WaiveCustomerSignsController(IHttpResultHelper httpResultHelper, DatabaseContext dB, ILogger<WaiveCustomerSignsController> logger, IWaiveCustomerSignService WaiveCustomerSignService, IWaiveQCService waiveQCService)
        {
            _httpResultHelper = httpResultHelper;
            DB = dB;
            _logger = logger;
            WaiveCustomerSignsService = WaiveCustomerSignService;
            WaiveQCService = waiveQCService;
        }

        /// <summary>
        /// ลิสข้อมูล Waive รับบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveCustomerSigns")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<WaiveCustomerSignDTO>>))]
        public async Task<IActionResult> GetWaiveCustomerSignListAsync([FromRoute] Guid projectID, [FromQuery] WaiveCustomerSignFilter filter,
         [FromQuery] PageParam pageParam,
         [FromQuery] WaiveCustomerSignSortByParam sortByParam,
         CancellationToken cancellationToken = default)
        {
            var result = await WaiveCustomerSignsService.GetWaiveCustomerSignListAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.WaiveCustomerSigns, WaiveCustomerSignsService.logModel);

        }
        /// <summary>
        /// ข้อมูล Waive รับบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveCustomerSigns/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<WaiveCustomerSignDTO>))]
        public async Task<IActionResult> GetWaiveCustomerSignAsync([FromRoute] Guid projectID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await WaiveCustomerSignsService.GetWaiveCustomerSignAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, WaiveCustomerSignsService.logModel);
        }
        /// <summary>
        /// สร้าง ข้อมูล Waive รับบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/WaiveCustomerSigns")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<WaiveCustomerSignDTO>))]
        public async Task<IActionResult> CreateWaiveCustomerSignAsync([FromRoute] Guid projectID, [FromBody] WaiveCustomerSignDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await WaiveCustomerSignsService.CreateWaiveCustomerSignAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, WaiveCustomerSignsService.logModel,HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateWaiveCustomerSignAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูล Waive รับบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/WaiveCustomerSigns/{id?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<WaiveCustomerSignDTO>))]
        public async Task<IActionResult> UpdateWaiveCustomerSignAsync([FromRoute] Guid projectID, [FromRoute] Guid? id, [FromBody] WaiveCustomerSignDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await WaiveCustomerSignsService.UpdateWaiveCustomerSignAsync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, WaiveCustomerSignsService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateWaiveCustomerSignAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูล Waive รับบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/WaiveCustomerSigns/{id}")]
        public async Task<IActionResult> DeleteProjectWaiveCustomerSign([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await WaiveQCService.DeleteWaiveQCAsync(projectID, id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteProjectWaiveCustomerSign", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import WaiveCustomerSign
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<WaiveCustomerSignExcelDTO>))]
        [HttpPost("{projectID}/WaiveCustomerSigns/Import")]
        public async Task<IActionResult> ImportWaiveCustomerSignAsync([FromRoute] Guid projectID, [FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await WaiveCustomerSignsService.ImportWaiveCustomerSignAsync(projectID, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, WaiveCustomerSignsService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportWaiveCustomerSignAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export WaiveCustomerSign
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveCustomerSigns/Export")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportProjectWaiveCustomerSign([FromRoute] Guid projectID, [FromQuery] WaiveCustomerSignFilter filter, [FromQuery] WaiveCustomerSignSortByParam sortByParam)
        {
            var result = await WaiveCustomerSignsService.ExportExcelWaiveCustomerSignAsync(projectID, filter, sortByParam);
            return await _httpResultHelper.SuccessCustomResult(result, WaiveCustomerSignsService.logModel);
        }
    }
}
