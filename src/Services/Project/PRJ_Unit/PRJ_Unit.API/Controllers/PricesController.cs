using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
      [Authorize(Policy = "MultiSchemePolicy")]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class PricesController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<PricesController> _logger;
        private readonly IPriceListService PriceListService;
        private readonly IHttpResultHelper _httpResultHelper;

        public PricesController(DatabaseContext dB, ILogger<PricesController> logger, IHttpResultHelper httpResultHelper, IPriceListService priceListService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            PriceListService = priceListService;
        }
        /// <summary>
        /// ดึงข้อมูล PriceList
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/PriceLists")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<PriceListDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPriceListsAsync([FromRoute] Guid projectID, [FromQuery] PriceListFilter filter, [FromQuery] PageParam pageParam, [FromQuery] PriceListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await PriceListService.GetPriceListsAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.PriceLists, PriceListService.logModel);
        }

        [HttpGet("{projectID}/PriceLists/{id}")]
        [ProducesResponseType(typeof(ResponseModel<PriceListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPriceListAsync([FromRoute] Guid projectID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await PriceListService.GetPriceListAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, PriceListService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูล PriceList
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/PriceLists")]
        [ProducesResponseType(typeof(ResponseModel<PriceListDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePriceListAsync([FromRoute] Guid projectID, [FromBody] PriceListDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await PriceListService.CreatePriceListAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, PriceListService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreatePriceListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขข้อมูล PriceList
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/PriceLists/{id}")]
        [ProducesResponseType(200, Type = typeof(PriceListDTO))]
        public async Task<IActionResult> UpdatePriceListAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] PriceListDTO input)
        {
            try
            {
                var result = await PriceListService.UpdatePriceListAsync(projectID, id, input);
                return await _httpResultHelper.SuccessCustomResult(result, PriceListService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdatePriceListAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// ลบข้อมูล PriceList
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceListAsync([Required][FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await PriceListService.DeletePriceListAsync(id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, PriceListService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeletePriceListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import Excel PriceList
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/PriceLists/Import")]
        [ProducesResponseType(200, Type = typeof(PriceListExcelDTO))]
        public async Task<IActionResult> ImportProjectPriceListAsync([FromRoute] Guid projectID, [FromBody] FileDTO file)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                        userID = parsedUserID;

                    var result = await PriceListService.ImportProjectPriceListAsync(projectID, file, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, PriceListService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportProjectPriceListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export PriceList Excel
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/PriceLists/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportExcelPriceListAsync([FromRoute] Guid projectID)
        {
            try
            {
                var result = await PriceListService.ExportExcelPriceListAsync(projectID);
                return await _httpResultHelper.SuccessCustomResult(result, PriceListService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportExcelPriceListAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}
