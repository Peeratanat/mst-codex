using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.USR;
using Database.Models;
using Auth_User.Params.Filters;
using Auth_User.Services;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Microsoft.AspNetCore.Authorization;
using Common.Helper.HttpResultHelper;
using Database.Models.USR;
using System.Net;
using System.Collections;
using System.Reflection.Metadata;
using Auth;
using ErrorHandling;
using Base.DTOs.MST;
using Base.DTOs.Common;

namespace Auth_User.API.Controllers
{

    //#if !DEBUG
    [Authorize]
    //#endif

    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class AgentsBCController : BaseController
    {
        private readonly IAgentsBCService AgentsBCService;
        private readonly IHttpResultHelper _httpResultHelper;

        public AgentsBCController(IHttpResultHelper httpResultHelper, IAgentsBCService agentsBCService)
        {
            _httpResultHelper = httpResultHelper;
            AgentsBCService = agentsBCService;
        }

        [HttpGet("AgentBCList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AgentDropdownDTO>>))]
        public async Task<IActionResult> AgentBCListAsync([FromQuery] string agentOwner, [FromQuery] string name)
        {
            var result = await AgentsBCService.AgentBCListAsync(agentOwner, name);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsBCService.logModel);
        }

        [HttpGet("AgentBCSaleList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AgentEmployeeDropdownDTO>))]
        public async Task<IActionResult> AgentBCSaleListAsync([FromQuery] Guid? agentID, [FromQuery] string name)
        {
            var result = await AgentsBCService.AgentBCSaleListAsync(agentID, name);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsBCService.logModel);
        }


    }
}
