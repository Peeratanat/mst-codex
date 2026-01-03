using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRJ_Unit.API;
using PRJ_Unit.Services;

namespace PRJ_Unit.API.Controllers
{
#if !DEBUG
      [Authorize(Policy = "MultiSchemePolicy")]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class ModelsController : Controller
    {
        private readonly IModelService ModelService;
        private readonly IHttpResultHelper _httpResultHelper;

        public ModelsController(IModelService modelService, IHttpResultHelper httpResultHelper)
        {
            ModelService = modelService;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// ลิสข้อมูลแบบบ้านทั้งหมด
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(typeof(ResponseModel<List<ModelDropdownDTO>>), StatusCodes.Status201Created)]
        public async Task<IActionResult> GetModelDropdownListAsync([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var results = await ModelService.GetModelDropdownListAsync(null, name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, ModelService.logModel,HttpStatusCode.Created);
        }
    }
}
