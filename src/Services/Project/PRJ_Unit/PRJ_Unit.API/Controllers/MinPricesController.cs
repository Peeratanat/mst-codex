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
//#if !DEBUG
     [Authorize]
//#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class MinPricesController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<MinPricesController> _logger;
        private readonly IMinPriceService MinPriceService;
        private readonly IHttpResultHelper _httpResultHelper;

        public MinPricesController(DatabaseContext dB, ILogger<MinPricesController> logger, IHttpResultHelper httpResultHelper, IMinPriceService minPriceService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            MinPriceService = minPriceService;
        }
        /// <summary>
        /// ลิส ข้อมูล Minprice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/MinPrices")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<MinPriceDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMinPriceListAsync([FromRoute] Guid projectID, [FromQuery] MinPriceFilter filter, [FromQuery] PageParam pageParam, [FromQuery] MinPriceSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MinPriceService.GetMinPriceListAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MinPrices, MinPriceService.logModel);
        }
        /// <summary>
        /// ข้อมูล MinPrice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/MinPrices/{id}")]
        [ProducesResponseType(typeof(ResponseModel<MinPriceDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMinPriceAsync([FromRoute] Guid projectID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await MinPriceService.GetMinPriceAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MinPriceService.logModel);
        }
        /// <summary>
        /// สร้าง ข้อมูล Minprice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/MinPrices")]
        [ProducesResponseType(typeof(ResponseModel<MinPriceDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMinPriceAsync([FromRoute] Guid projectID, [FromBody] MinPriceDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MinPriceService.CreateMinPriceAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MinPriceService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMinPriceAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูล Minprice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/MinPrices/{id}")]
        [ProducesResponseType(typeof(ResponseModel<MinPriceDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMinPriceAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] MinPriceDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MinPriceService.UpdateMinPriceAsync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MinPriceService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMinPriceAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบข้อมูล Minprice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/MinPrices/{id}")]
        public async Task<IActionResult> DeleteMinPriceAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MinPriceService.DeleteMinPriceAsync(projectID, id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MinPriceService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMinPriceAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import MinPrice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/MinPrices/Import")]
        [ProducesResponseType(typeof(ResponseModel<MinPriceExcelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportMinPriceAsync([FromRoute] Guid projectID, [FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                        userID = parsedUserID;

                    var result = await MinPriceService.ImportMinPriceAsync(projectID, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MinPriceService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportMinPriceAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export MinPrice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/MinPrices/Export")]
        [ProducesResponseType(typeof(ResponseModel<FileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportProjectMinPrice([FromRoute] Guid projectID, [FromQuery] MinPriceFilter filter, [FromQuery] MinPriceSortByParam sortByParam)
        {
            var result = await MinPriceService.ExportExcelMinPriceAsync(projectID, filter, sortByParam);
            return await _httpResultHelper.SuccessCustomResult(result, MinPriceService.logModel);
        }

        /// <summary>
        /// Export MinPricesToSAP
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/ExportMinpriceToSAP")]
        [ProducesResponseType(typeof(ResponseModel<FileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportProjectMinPriceToSAPAsync([FromRoute] Guid projectID)
        {
            var result = await MinPriceService.ExportProjectMinPriceToSAPAsync(projectID);
            return await _httpResultHelper.SuccessCustomResult(result, MinPriceService.logModel);
        }
    }
}
