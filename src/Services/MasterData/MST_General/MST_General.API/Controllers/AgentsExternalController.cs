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
using Database.Models.USR;

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class AgentsExternalController : BaseController
    {

        private IAgentsExternalService AgentsExternalService;
        private readonly DatabaseContext DB;

        private readonly ILogger<AgentsExternalController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public AgentsExternalController(DatabaseContext db, IAgentsExternalService agentsExternalService, ILogger<AgentsExternalController> logger, IHttpResultHelper httpResultHelper)
        {
            AgentsExternalService = agentsExternalService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// ลิสข้องข้อมูล AgentExternal
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("AgentExternalList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AgentExternalDTO>>))]
        public async Task<IActionResult> AgentExternalList([FromQuery] AgentExternalFilter filter
        , [FromQuery] PageParam pageParam, [FromQuery] AgentExternalSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await AgentsExternalService.AgentExternalListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.AgentExternals, AgentsExternalService.logModel);
        }

        /// <summary>
        /// ข้อมูล AgentExternal
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        [HttpGet("AgentExternaDetail/{agentID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<AgentExternalDTO>))]
        public async Task<IActionResult> AgentExternaDetail([FromRoute] Guid agentID, CancellationToken cancellationToken = default)
        {
            var result = await AgentsExternalService.AgentExternaDetailAsync(agentID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalService.logModel);
        }

        /// <summary>
        /// แก้ไขข้อมูล AgentExternal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateAgentExternal")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<AgentExternalResp>))]
        public async Task<IActionResult> UpdateAgentExternal([FromBody] AgentExternalDTO input)
        {
            var result = await AgentsExternalService.UpdateAgentExternalAsync(input);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalService.logModel);
        }

        /// <summary>
        /// สร้างข้อมูล AgentExternal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CreateAgentExternal")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<AgentExternalResp>))]
        public async Task<IActionResult> CreateAgentExternal([FromBody] AgentExternalDTO input)
        {
            var result = await AgentsExternalService.CreateAgentExternalAsync(input);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalService.logModel, HttpStatusCode.Created);
        }


        /// <summary>
        /// ข้อมูล PrefixList
        /// </summary>
        /// <param name="isCorporate"></param>
        /// <returns></returns>
        [HttpGet("GetPrefixList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AgentExternalPrefixDTO>>))]
        public async Task<IActionResult> GetPrefixList([FromQuery] int isCorporate, CancellationToken cancellationToken = default)
        {
            var result = await AgentsExternalService.GetPrefixListAsync(isCorporate, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalService.logModel);
        }

        /// <summary>
        /// ข้อมูล BusinessTypeList
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBusinessTypeList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AgentBusinessTypeDropdownDTO>>))]
        public async Task<IActionResult> GetBusinessTypeList(CancellationToken cancellationToken = default)
        {
            var result = await AgentsExternalService.GetBusinessTypeListAsync(cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalService.logModel);
        }

        /// <summary>
        /// check duplicate AgentExternal
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("CheckDuplicateAgentExternal")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<AgentExternalResp>))]
        public async Task<IActionResult> CheckDuplicateAgentExternal([FromQuery] AgentExternalFilter filter, CancellationToken cancellationToken = default)
        {
            var result = await AgentsExternalService.CheckDuplicateAgentExternalAsync(filter, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalService.logModel);
        }


        /// <summary>
        /// ข้อมูล AgentExternalAPOwnerList สำหรับ checkduplicate
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("AgentExternalAPOwnerList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AgentExternalDTO>>))]
        public async Task<IActionResult> AgentExternalAPOwnerList([FromQuery] AgentExternalFilter filter, CancellationToken cancellationToken = default)
        {
            var result = await AgentsExternalService.AgentExternalAPOwnerListAsync(filter, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalService.logModel);
        }


        /// <summary>
        /// ข้อมูล CheckAuthUserBG
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckAuthUserBG")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<UserAuthBGDTO>))]
        public async Task<IActionResult> CheckAuthUserBG(CancellationToken cancellationToken = default)
        {
            Guid? userID = GetUserId();
            //Guid? userID = new Guid("0BC1A20C-8497-4566-8A5F-463B2DFB5D14");
            //Guid? userID = new Guid("C24BD13B-9C71-4FC1-8A54-14D988CC06F9");
            var result = await AgentsExternalService.CheckAuthUserBGAsync(userID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsExternalService.logModel);
        }

    }
}