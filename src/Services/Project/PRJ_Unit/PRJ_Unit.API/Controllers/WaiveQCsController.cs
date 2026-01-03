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
    public class WaiveQCsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<WaiveQCsController> _logger;
        private readonly IWaiveQCService WaiveQCService;
        private readonly IHttpResultHelper _httpResultHelper;

        public WaiveQCsController(IHttpResultHelper httpResultHelper, IWaiveQCService waiveQCService, DatabaseContext dB, ILogger<WaiveQCsController> logger)
        {
            _httpResultHelper = httpResultHelper;
            WaiveQCService = waiveQCService;
            DB = dB;
            _logger = logger;
        }

        /// <summary>
        /// ลิสข้อมูล WaiveQCs
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveQCs")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<WaiveQCDTO>>))]
        public async Task<IActionResult> GetProjectWaiveQCList([FromRoute] Guid projectID, [FromQuery] WaiveQCFilter filter, [FromQuery] PageParam pageParam, [FromQuery] WaiveQCSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await WaiveQCService.GetWaiveQCListAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.WaiveQC, WaiveQCService.logModel);
        }
        /// <summary>
        /// ข้อมูล WaiveQCs
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveQCs/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<WaiveQCDTO>))]
        public async Task<IActionResult> GetProjectWaiveQC([FromRoute] Guid projectID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await WaiveQCService.GetWaiveQCAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, WaiveQCService.logModel);
        }
        /// <summary>
        /// สร้าง ข้อมูล WaiveQCs
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/WaiveQCs")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<WaiveQCDTO>))]
        public async Task<IActionResult> CreateWaiveQCAsync([FromRoute] Guid projectID, [FromBody] WaiveQCDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await WaiveQCService.CreateWaiveQCAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, WaiveQCService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateWaiveQCAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูล WaiveQCs
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/WaiveQCs/{id?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<WaiveQCDTO>))]
        public async Task<IActionResult> UpdateWaiveQCAsync([FromRoute] Guid projectID, [FromRoute] Guid? id, [FromBody] WaiveQCDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await WaiveQCService.UpdateWaiveQCAsync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, WaiveQCService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateWaiveQCAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูล WaiveQCs
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/WaiveQCs/{id}")]
        public async Task<IActionResult> DeleteWaiveQCAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
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
                    _logger.LogError(message: string.Join(" : ", "DeleteWaiveQCAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import WavieQC Excel
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<WaiveQCExcelDTO>))]
        [HttpPost("{projectID}/WaiveQCs/Import")]
        public async Task<IActionResult> ImportWaiveQCAsync([FromRoute] Guid projectID, [FromBody] FileDTO input)
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
                    var result = await WaiveQCService.ImportWaiveQCAsync(projectID, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, WaiveQCService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportWaiveQCAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export WaiveQC
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveQCs/Export")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportExcelWaiveQCAsync([FromRoute] Guid projectID, [FromQuery] WaiveQCFilter filter, [FromQuery] WaiveQCSortByParam sortByParam)
        {
            try
            {
                var result = await WaiveQCService.ExportExcelWaiveQCAsync(projectID, filter, sortByParam);

                return await _httpResultHelper.SuccessCustomResult(result, WaiveQCService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportExcelWaiveQCAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}
