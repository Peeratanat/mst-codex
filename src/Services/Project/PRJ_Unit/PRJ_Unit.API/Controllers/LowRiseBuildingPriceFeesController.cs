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
    public class LowRiseBuildingPriceFeesController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<LowRiseBuildingPriceFeesController> _logger;
        private readonly ILowRiseBuildingPriceFeeService LowRiseBuildingPriceFeeService;
        private readonly IHttpResultHelper _httpResultHelper;

        public LowRiseBuildingPriceFeesController(DatabaseContext dB, ILogger<LowRiseBuildingPriceFeesController> logger, IHttpResultHelper httpResultHelper, ILowRiseBuildingPriceFeeService lowRiseBuildingPriceFeeService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            LowRiseBuildingPriceFeeService = lowRiseBuildingPriceFeeService;
        }
        /// <summary>
        /// ลิส ข้อมูลค่าธรรมเนียม ค่าพื้นที่สิ่งปลูกสร้างแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseBuildingPriceFees")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<LowRiseBuildingPriceFeeDTO>>))]
        public async Task<IActionResult> GetProjectLowRiseBuildingPriceFeeList([FromRoute] Guid projectID, [FromQuery] LowRiseBuildingPriceFeeFilter filter, [FromQuery] PageParam pageParam, [FromQuery] LowRiseBuildingPriceFeeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await LowRiseBuildingPriceFeeService.GetLowRiseBuildingPriceFeeListAsync(projectID, filter, pageParam, sortByParam,cancellationToken);
            AddPagingResponse(result.PageOutput);

            return await _httpResultHelper.SuccessCustomResult(result.LowRiseBuildingPriceFees, LowRiseBuildingPriceFeeService.logModel);
        }
        /// <summary>
        /// ข้อมูลค่าธรรมเนียม ค่าพื้นที่สิ่งปลูกสร้างแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseBuildingPriceFees/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LowRiseBuildingPriceFeeDTO>))]
        public async Task<IActionResult> GetProjectLowRiseBuildingPriceFee([FromRoute] Guid projectID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await LowRiseBuildingPriceFeeService.GetLowRiseBuildingPriceFeeAsync(projectID, id,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, LowRiseBuildingPriceFeeService.logModel);
        }
        /// <summary>
        /// สร้าง ข้อมูลค่าธรรมเนียม ค่าพื้นที่สิ่งปลูกสร้างแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/LowRiseBuildingPriceFees")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LowRiseBuildingPriceFeeDTO>))]
        public async Task<IActionResult> CreateLowRiseBuildingPriceFeeAsync([FromRoute] Guid projectID, [FromBody] LowRiseBuildingPriceFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LowRiseBuildingPriceFeeService.CreateLowRiseBuildingPriceFeeAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LowRiseBuildingPriceFeeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateLowRiseBuildingPriceFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลค่าธรรมเนียม ค่าพื้นที่สิ่งปลูกสร้างแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/LowRiseBuildingPriceFees/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LowRiseBuildingPriceFeeDTO>))]
        public async Task<IActionResult> UpdateLowRiseBuildingPriceFeesync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] LowRiseBuildingPriceFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LowRiseBuildingPriceFeeService.UpdateLowRiseBuildingPriceFeesync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LowRiseBuildingPriceFeeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateLowRiseBuildingPriceFeesync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูลค่าธรรมเนียม ค่าพื้นที่สิ่งปลูกสร้างแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/LowRiseBuildingPriceFees/{id}")]
        public async Task<IActionResult> DeleteLowRiseBuildingPriceFeeAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LowRiseBuildingPriceFeeService.DeleteLowRiseBuildingPriceFeeAsync(projectID, id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, LowRiseBuildingPriceFeeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteLowRiseBuildingPriceFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
