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
    public class WaterElectricMeterPricesController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<WaterElectricMeterPricesController> _logger;
        private readonly IWaterElectricMeterPriceService WaterElectricMeterPriceService;
        private readonly IHttpResultHelper _httpResultHelper;

        public WaterElectricMeterPricesController(DatabaseContext dB, ILogger<WaterElectricMeterPricesController> logger, IWaiveQCService waiveQCService, IHttpResultHelper httpResultHelper, IWaterElectricMeterPriceService waterElectricMeterPriceService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            WaterElectricMeterPriceService = waterElectricMeterPriceService;
        }

        /// <summary>
        /// ลิส ข้อมูลมิเตอร์ไฟฟ้า-น้ำประปา ของแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="modelID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Model/{modelID}/WaterElectricMeterPrice")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<WaterElectricMeterPriceDTO>>))]
        public async Task<IActionResult> GetWaterElectricMeterPriceListAsync([FromRoute] Guid modelID,
         [FromQuery] WaterElectricMeterPriceFilter filter,
         [FromQuery] PageParam pageParam,
         [FromQuery] SortByParam sortByParam,
         CancellationToken cancellationToken = default)
        {
            var result = await WaterElectricMeterPriceService.GetWaterElectricMeterPriceListAsync(modelID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.WaterElectricMeterPrices, WaterElectricMeterPriceService.logModel);
        }
        /// <summary>
        /// ข้อมูลมิเตอร์ไฟฟ้า-น้ำประปา ของแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="modelID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Model/{modelID}/WaterElectricMeterPrice/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<WaterElectricMeterPriceDTO>))]
        public async Task<IActionResult> GetWaterElectricMeterPriceAsync([FromRoute] Guid modelID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await WaterElectricMeterPriceService.GetWaterElectricMeterPriceAsync(modelID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, WaterElectricMeterPriceService.logModel);
        }
    }
}
