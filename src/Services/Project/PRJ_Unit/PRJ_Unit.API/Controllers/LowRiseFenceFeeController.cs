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
    public class LowRiseFenceFeeController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<LowRiseFenceFeeController> _logger;
        private readonly ILowRiseFenceFeeService LowRiseFenceFeeService;
        private readonly IHttpResultHelper _httpResultHelper;

        public LowRiseFenceFeeController(DatabaseContext dB, ILogger<LowRiseFenceFeeController> logger, IHttpResultHelper httpResultHelper, ILowRiseFenceFeeService lowRiseFenceFeeService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            LowRiseFenceFeeService = lowRiseFenceFeeService;
        }

        /// <summary>
        /// ลิสข้อมูล ค่าธรรมเนียม สำนักงานที่ดินค่ารั้ว (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseFenceFees")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<LowRiseFenceFeeDTO>>))]
        public async Task<IActionResult> GetProjectLowRiseFenceFeeList([FromRoute] Guid projectID, [FromQuery] LowRiseFenceFeeFilter filter, [FromQuery] PageParam pageParam, [FromQuery] LowRiseFenceFeeSortByParam sortByParam)
        {
            var result = await LowRiseFenceFeeService.GetLowRiseFenceFeeListAsync(projectID, filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.LowRiseFenceFees, LowRiseFenceFeeService.logModel);
        }
        /// <summary>
        /// ข้อมูล ค่าธรรมเนียม สำนักงานที่ดินค่ารั้ว (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseFenceFees/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LowRiseFenceFeeDTO>))]
        public async Task<IActionResult> GetProjectLowRiseFenceFee([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            var result = await LowRiseFenceFeeService.GetLowRiseFenceFeeAsync(projectID, id);
            return await _httpResultHelper.SuccessCustomResult(result, LowRiseFenceFeeService.logModel);

        }
        /// <summary>
        /// สร้าง ข้อมูล ค่าธรรมเนียม สำนักงานที่ดินค่ารั้ว (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/LowRiseFenceFees")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LowRiseFenceFeeDTO>))]
        public async Task<IActionResult> CreateLowRiseFenceFeeAsync([FromRoute] Guid projectID, [FromBody] LowRiseFenceFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LowRiseFenceFeeService.CreateLowRiseFenceFeeAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LowRiseFenceFeeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateLowRiseFenceFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูล ค่าธรรมเนียม สำนักงานที่ดินค่ารั้ว (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/LowRiseFenceFees/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LowRiseFenceFeeDTO>))]
        public async Task<IActionResult> UpdateLowRiseFenceFeeAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] LowRiseFenceFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LowRiseFenceFeeService.UpdateLowRiseFenceFeeAsync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LowRiseFenceFeeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateLowRiseFenceFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูล ค่าธรรมเนียม สำนักงานที่ดินค่ารั้ว (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/LowRiseFenceFees/{id}")]
        public async Task<IActionResult> DeleteLowRiseFenceFeeAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LowRiseFenceFeeService.DeleteLowRiseFenceFeeAsync(projectID, id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, LowRiseFenceFeeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteLowRiseFenceFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

    }
}
