using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MST_General.Services;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class DistrictsController : BaseController
    {
        private IDistrictService DistrictService;
        private readonly ILogger<DistrictsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly DatabaseContext DB;

        public DistrictsController(IDistrictService counterService, DatabaseContext db, ILogger<DistrictsController> logger, IHttpResultHelper httpResultHelper)
        {
            this.DistrictService = counterService;
            this.DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// หาอำเภอจากชื่อ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="provinceID"></param>
        /// <returns></returns>
        [HttpGet("Find")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<DistrictListDTO>))]
        public async Task<IActionResult> FindDistrictAsync([FromQuery] string name, [FromQuery] Guid provinceID, CancellationToken cancellationToken = default)
        {
            var result = await DistrictService.FindDistrictAsync(provinceID, name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DistrictService.logModel);
        }

        /// <summary>
        /// ลิสของอำเภอ dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <param name="provinceID"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<DistrictListDTO>>))]
        public async Task<IActionResult> GetDistrictDropdownListAsync([FromQuery] string name, [FromQuery] Guid? provinceID = null, CancellationToken cancellationToken = default)
        {
            var result = await DistrictService.GetDistrictDropdownListAsync(name, provinceID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DistrictService.logModel);
        }
        /// <summary>
        /// ลิสของอำเภอ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<DistrictDTO>>))]
        public async Task<IActionResult> GetDistrictListAsync([FromQuery] DistrictFilter filter, [FromQuery] PageParam pageParam, [FromQuery] DistrictSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await DistrictService.GetDistrictListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result, DistrictService.logModel);
        }
        /// <summary>
        /// ข้อมูลอำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<DistrictDTO>))]
        public async Task<IActionResult> GetDistrictAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await DistrictService.GetDistrictAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DistrictService.logModel);
        }
        /// <summary>
        /// สร้างอำเภอ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<DistrictDTO>))]
        public async Task<IActionResult> CreateDistrictAsync([FromBody] DistrictDTO input)
        {
            try
            {
                var result = await DistrictService.CreateDistrictAsync(input);
                return await _httpResultHelper.SuccessCustomResult(result, DistrictService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateDistrictAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไขข้อมูล อำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<DistrictDTO>))]
        public async Task<IActionResult> UpdateDistrictAsync([FromRoute] Guid id, [FromBody] DistrictDTO input)
        {
            try
            {
                var result = await DistrictService.UpdateDistrictAsync(id, input);
                return await _httpResultHelper.SuccessCustomResult(result, DistrictService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateDistrictAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// ลบ ข้อมูลอำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrictAsync([FromRoute] Guid id)
        {
            try
            {
                await DistrictService.DeleteDistrictAsync(id);
                return await _httpResultHelper.SuccessCustomResult(null, DistrictService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteDistrictAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}