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
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class LowRiseFeesController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<LowRiseFeesController> _logger;
        private readonly ILowRiseFeeService LowRiseFeeService;
        private readonly IHttpResultHelper _httpResultHelper;

        public LowRiseFeesController(DatabaseContext dB, ILogger<LowRiseFeesController> logger, IHttpResultHelper httpResultHelper, ILowRiseFeeService lowRiseFeeService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            LowRiseFeeService = lowRiseFeeService;
        }
        /// <summary>
        /// ลิสข้อมูล ค่าธรรมเนียม ราคาประเมินที่ดิน (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseFees")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<LowRiseFeeDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLowRiseFeeListAsync([FromRoute] Guid projectID, [FromQuery] LowRiseFeeFilter filter, [FromQuery] PageParam pageParam, [FromQuery] LowRiseFeeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await LowRiseFeeService.GetLowRiseFeeListAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.LowRiseFees, LowRiseFeeService.logModel);
        }
        /// <summary>
        /// ข้อมูล ราคาประเมินที่ดินแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseFees/{id}")]
        [ProducesResponseType(typeof(ResponseModel<LowRiseFeeDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLowRiseFeeAsync([FromRoute] Guid projectID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await LowRiseFeeService.GetLowRiseFeeAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, LowRiseFeeService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูล ราคาประเมินที่ดินแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/LowRiseFees")]
        [ProducesResponseType(typeof(ResponseModel<LowRiseFeeDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateLowRiseFeeAsync([FromRoute] Guid projectID, [FromBody] LowRiseFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LowRiseFeeService.CreateLowRiseFeeAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LowRiseFeeService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateLowRiseFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูล ราคาประเมินที่ดินแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/LowRiseFees/{id}")]
        [ProducesResponseType(typeof(ResponseModel<LowRiseFeeDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateLowRiseFeeAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] LowRiseFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LowRiseFeeService.UpdateLowRiseFeeAsync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LowRiseFeeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateLowRiseFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูล ราคาประเมินที่ดินแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/LowRiseFees/{id}")]
        public async Task<IActionResult> DeleteLowRiseFeeAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LowRiseFeeService.DeleteLowRiseFeeAsync(projectID, id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LowRiseFeeService.logModel);
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
