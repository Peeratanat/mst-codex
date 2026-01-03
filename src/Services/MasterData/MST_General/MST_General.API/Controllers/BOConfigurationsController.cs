using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Base.DTOs.MST;
using MST_General.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class BOConfigurationsController : ControllerBase
    {
        private DatabaseContext DB;
        private readonly IBOConfigurationService BOConfigurationService;
        private readonly ILogger<BOConfigurationsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public BOConfigurationsController(IBOConfigurationService bOConfigurationService, DatabaseContext db, ILogger<BOConfigurationsController> logger, IHttpResultHelper httpResultHelper)
        {
            this.BOConfigurationService = bOConfigurationService;
            this.DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ดึงข้อมูล BOConfig
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BOConfigurationDTO>))]
        public async Task<IActionResult> GetBOConfigurationAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await BOConfigurationService.GetBOConfigurationAsync(cancellationToken);
                return await _httpResultHelper.SuccessCustomResult(result, BOConfigurationService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteBG", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// แก้ไขข้อมูล BOConfig
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BOConfigurationDTO>))]
        public async Task<IActionResult> UpdateBOConfigurationAsync([FromRoute] Guid id, [FromBody] BOConfigurationDTO input)
        {
            try
            {
                var result = await BOConfigurationService.UpdateBOConfigurationAsync(id, input);
                return await _httpResultHelper.SuccessCustomResult(result, BOConfigurationService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateBOConfigurationAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

    }
}