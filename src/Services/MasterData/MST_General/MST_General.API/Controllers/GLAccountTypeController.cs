using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using MST_General.Services;
using Base.DTOs.MST;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class GLAccountTypeController : BaseController
    {
        private readonly IGLAccountTypeService GLAccountTypeService;
        private readonly DatabaseContext DB;
        private readonly ILogger<MasterCenterGroupsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public GLAccountTypeController(IGLAccountTypeService glAccountTypeService, DatabaseContext db , ILogger<MasterCenterGroupsController> logger, IHttpResultHelper httpResultHelper)
        {
            this.GLAccountTypeService = glAccountTypeService;
            this.DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }


        /// <summary>
        /// ลิสข้อมูล GL Account Type Dropdown
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<GLAccountTypeDropdownDTO>>))]
        public async Task<IActionResult> GetGLAccountTypeDropdownList([FromQuery] string key, [FromQuery] string name)
        {
            try
            {
                var results = await GLAccountTypeService.GetGLAccountTypeDropdownListAsync(key, name);
                return await _httpResultHelper.SuccessCustomResult(results, GLAccountTypeService.logModel, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "GetGLAccountTypeDropdownList", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}
