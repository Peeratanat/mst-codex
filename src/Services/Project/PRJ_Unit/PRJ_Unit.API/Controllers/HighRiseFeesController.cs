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
using PRJ_Unit.API.Controllers;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.API.Controllers
{
    #if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class HighRiseFeesController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<HighRiseFeesController> _logger;
        private readonly IHighRiseFeeService HighRiseFeeService;
        private readonly IHttpResultHelper _httpResultHelper;

        public HighRiseFeesController(DatabaseContext dB, ILogger<HighRiseFeesController> logger, IHttpResultHelper httpResultHelper, IHighRiseFeeService highRiseFeeService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            HighRiseFeeService = highRiseFeeService;
        }
        /// <summary>
        /// ลิสข้อมูลค่าทำเนียมโอน-ราคาประเมินที่ดิน (แนวสูง)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/HighRiseFees")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<HighRiseFeeDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectHighRiseFeeList([FromRoute] Guid projectID, [FromQuery] HighRiseFeeFilter filter, [FromQuery] PageParam pageParam, [FromQuery] HighRiseFeeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await HighRiseFeeService.GetHighRiseFeeListAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.HighRiseFees, HighRiseFeeService.logModel);
        }
        /// <summary>
        /// ข้อมูลค่าทำเนียมโอน-ราคาประเมินที่ดินแนวสูง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/HighRiseFees/{id}")]
        [ProducesResponseType(typeof(ResponseModel<HighRiseFeeDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectHighRiseFee([FromRoute] Guid projectID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await HighRiseFeeService.GetHighRiseFeeAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, HighRiseFeeService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูลค่าทำเนียมโอน-ราคาประเมินที่ดินแนวสูง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/HighRiseFees")]
        [ProducesResponseType(typeof(ResponseModel<HighRiseFeeDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateHighRiseFeeAsync([FromRoute] Guid projectID, [FromBody] HighRiseFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await HighRiseFeeService.CreateHighRiseFeeAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, HighRiseFeeService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateHighRiseFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลค่าทำเนียมโอน-ราคาประเมินที่ดินแนวสูง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/HighRiseFees/{id}")]
        [ProducesResponseType(typeof(ResponseModel<HighRiseFeeDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateHighRiseFeeAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] HighRiseFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await HighRiseFeeService.UpdateHighRiseFeeAsync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, HighRiseFeeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateHighRiseFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบข้อมูลค่าทำเนียมโอน-ราคาประเมินที่ดินแนวสูง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/HighRiseFees/{id}")]
        public async Task<IActionResult> DeleteHighRiseFeeAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await HighRiseFeeService.DeleteHighRiseFeeAsync(projectID, id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteHighRiseFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import ราคาประเมิณที่ดิน (แนวสูง)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/HighRiseFees/Import")]
        [ProducesResponseType(typeof(ResponseModel<HighRiseFeeExcelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportHighRiseFeeAsync([FromRoute] Guid projectID, [FromBody] FileDTO input)
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

                    var result = await HighRiseFeeService.ImportHighRiseFeeAsync(projectID, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, HighRiseFeeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportHighRiseFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export ราคาประเมินที่ดิน (แนวสูง)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/HighRiseFees/Export")]
        [ProducesResponseType(typeof(ResponseModel<FileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportHighRiseFeeAsync([FromRoute] Guid projectID, [FromQuery] HighRiseFeeFilter filter, [FromQuery] HighRiseFeeSortByParam sortByParam)
        {
            try
            {
                var result = await HighRiseFeeService.ExportHighRiseFeeAsync(projectID, filter, sortByParam);
                return await _httpResultHelper.SuccessCustomResult(result, HighRiseFeeService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportHighRiseFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}
