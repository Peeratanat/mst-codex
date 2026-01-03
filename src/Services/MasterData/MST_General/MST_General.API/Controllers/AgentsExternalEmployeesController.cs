using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using MST_General.Params.Filters;
using MST_General.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;
using MST_General.Params.Outputs;

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class AgentsExternalEmployeesController : BaseController
    {
        private IAgentsEmployeeExternalService AgentsExternalEmployeesService;
        private readonly ILogger<AgentsExternalEmployeesController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly DatabaseContext DB;
        public AgentsExternalEmployeesController(DatabaseContext db, IAgentsEmployeeExternalService agentsExternalEmployeesService, ILogger<AgentsExternalEmployeesController> logger, IHttpResultHelper httpResultHelper)
        {
            this.AgentsExternalEmployeesService = agentsExternalEmployeesService;
            this.DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// ลิสข้องข้อมูล AgentsExternalEmployee
        /// </summary>
        /// <param name="agentID"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("AgentsExternalEmployeeList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AgentEmployeeExternalDTO>>))]
        public async Task<IActionResult> AgentsExternalEmployeeList([FromQuery] PageParam pageParam, [FromQuery] AgentExternalSortByParam sortByParam, [FromQuery] AgentEmployeeExternalFilter filter, CancellationToken cancellationToken = default)
        {
            var result = await AgentsExternalEmployeesService.AgentsExternalEmployeeListAsync(pageParam, sortByParam, filter, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.AgentEmployeeExternal, AgentsExternalEmployeesService.logModel);
        }

        /// <summary>
        /// ข้อมูล AgentsExternalEmployeeDetail
        /// </summary>
        /// <param name="agentSaleID"></param>
        /// <returns></returns>
        [HttpGet("AgentsExternalEmployeeDetail/{agentSaleID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<AgentEmployeeExternalDTO>))]
        public async Task<IActionResult> AgentsExternalEmployeeDetail([FromRoute] Guid agentSaleID, CancellationToken cancellationToken = default)
        {
            var result = await AgentsExternalEmployeesService.AgentsExternalEmployeeDetailAsync(agentSaleID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalEmployeesService.logModel);
        }

        /// <summary>
        /// แก้ไขข้อมูล UpdateAgentsExternalEmployee
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateAgentsExternalEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<AgentExternalEmployeesResp>))]
        public async Task<IActionResult> UpdateAgentsExternalEmployee([FromBody] AgentEmployeeExternalDTO input)
        {
            var result = await AgentsExternalEmployeesService.UpdateAgentsExternalEmployeeAsync(input);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalEmployeesService.logModel);
        }

        /// <summary>
        /// สร้างข้อมูล UpdateAgentsExternalEmployee
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CreateAgentsExternalEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<AgentExternalEmployeesResp>))]
        public async Task<IActionResult> CreateAgentsExternalEmployee([FromBody] AgentEmployeeExternalDTO input)
        {
            var result = await AgentsExternalEmployeesService.CreateAgentsExternalEmployeeAsync(input);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalEmployeesService.logModel, HttpStatusCode.Created);
        }
    }
}