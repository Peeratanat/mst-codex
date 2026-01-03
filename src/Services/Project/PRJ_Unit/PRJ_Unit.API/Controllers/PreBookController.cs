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
    public class PreBookController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<PreBookController> _logger;
        private readonly IPreBookService PreBookService;
        private readonly IHttpResultHelper _httpResultHelper;

        public PreBookController(DatabaseContext dB, ILogger<PreBookController> logger, IWaiveQCService waiveQCService, IHttpResultHelper httpResultHelper, IPreBookService preBookService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            PreBookService = preBookService;
        }


        /// <summary>
        /// ลิส ข้อมูลแปลง Dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="unitNo"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<UnitDropdownDTO>>))]
        public async Task<IActionResult> GetUnitPreBookDropdownListAsync([FromRoute] Guid projectID, [FromQuery] string unitNo)
        {
            var result = await PreBookService.GetUnitPreBookDropdownListAsync(projectID, unitNo);
            return await _httpResultHelper.SuccessCustomResult(result, PreBookService.logModel);
        }


    }
}
