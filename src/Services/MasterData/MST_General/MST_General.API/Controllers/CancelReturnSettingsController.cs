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
    public class CancelReturnSettingsController : BaseController
    {
        private ICancelReturnSettingService CancelReturnSettingService;
        private readonly DatabaseContext DB;
        private readonly ILogger<CancelReturnSettingsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public CancelReturnSettingsController(ICancelReturnSettingService cancelReturnSettingService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<CancelReturnSettingsController> logger)
        {
            this.CancelReturnSettingService = cancelReturnSettingService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }

        /// <summary>
        /// ดึงข้อมูล ตั้งค่าการยกเลิกคืนเงิน
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CancelReturnSettingDTO>))]
        public async Task<IActionResult> GetCancelReturnSettingAsync(CancellationToken cancellationToken = default)
        {
            var result = await CancelReturnSettingService.GetCancelReturnSettingAsync(cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, CancelReturnSettingService.logModel);
        }

        /// <summary>
        /// แก้ไขข้อมูล ตั้งค่าการยกเลิกคืนเงิน
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CancelReturnSettingDTO>))]
        public async Task<IActionResult> UpdateCancelReturnSettingAsync([FromRoute] Guid id, [FromBody] CancelReturnSettingDTO input)
        {
            try
            {
                var result = await CancelReturnSettingService.UpdateCancelReturnSettingAsync(id, input);
                return await _httpResultHelper.SuccessCustomResult(result, CancelReturnSettingService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateCancelReturnSettingAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

    }
}