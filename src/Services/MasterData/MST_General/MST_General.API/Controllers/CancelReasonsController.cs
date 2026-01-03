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
    public class CancelReasonsController : BaseController
    {
        private ICancelReasonService CancelReasonService;
        private readonly DatabaseContext DB;
        private readonly ILogger<CancelReasonsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public CancelReasonsController(ICancelReasonService cancelReasonService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<CancelReasonsController> logger)
        {
            this.CancelReasonService = cancelReasonService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }

        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<CancelReasonDropdownDTO>>))]
        public async Task<IActionResult> GetCancelReasonDropdownListAsync([FromQuery] string name, CancellationToken cancellationToken = default)
        {

            var results = await CancelReasonService.GetCancelReasonDropdownListAsync(name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, CancelReasonService.logModel);
        }

        /// <summary>
        /// ลิสของเหตุผลการยกเลิก
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<CancelReasonDTO>>))]
        public async Task<IActionResult> GetCancelReasonListAsync([FromQuery] CancelReasonFilter filter, [FromQuery] PageParam pageParam, [FromQuery] CancelReasonSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await CancelReasonService.GetCancelReasonListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result, CancelReasonService.logModel);
        }

        /// <summary>
        /// ข้อมูลเหตุผลการยกเลิก
        /// </summary>
        /// <param name="id"></param>s
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CancelReasonDTO>))]
        public async Task<IActionResult> GetCancelReasonAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await CancelReasonService.GetCancelReasonAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, CancelReasonService.logModel);
        }

        /// <summary>
        /// สร้างเหตุผลการยกเลิก
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<CancelReasonDTO>))]
        public async Task<IActionResult> CreateCancelReason([FromBody] CancelReasonDTO input)
        {
            try
            {
                var result = await CancelReasonService.CreateCancelReasonAsync(input);
                return await _httpResultHelper.SuccessCustomResult(result, CancelReasonService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateCancelReasonAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// แก้ไขเหตุผลการยกเลิก
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CancelReasonDTO>))]
        public async Task<IActionResult> UpdateCancelReasonAsync([FromRoute] Guid id, [FromBody] CancelReasonDTO input)
        {
            try
            {
                var result = await CancelReasonService.UpdateCancelReasonAsync(id, input);
                return await _httpResultHelper.SuccessCustomResult(result, CancelReasonService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateCancelReasonAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// ลบเหตุผลการยกเลิก
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCancelReasonAsync([FromRoute] Guid id)
        {
            try
            {

                await CancelReasonService.DeleteCancelReasonAsync(id);
                return await _httpResultHelper.SuccessCustomResult(null, CancelReasonService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteCancelReasonAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}