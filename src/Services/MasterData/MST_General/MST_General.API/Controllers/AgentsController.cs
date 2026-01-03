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

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class AgentsController : BaseController
    {

        private IAgentsService AgentsService;
        private readonly DatabaseContext DB;

        private readonly ILogger<AgentsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public AgentsController(DatabaseContext db, IAgentsService agentsService, ILogger<AgentsController> logger, IHttpResultHelper httpResultHelper)
        {
            AgentsService = agentsService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ข้อมูล Agent Drodown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AgentDropdownDTO>>))]
        public async Task<IActionResult> GetAgentDropdownListAsync([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var result = await AgentsService.GetAgentDropdownListAsync(name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsService.logModel);
        }


        /// <summary>
        /// ลิสข้องข้อมูล Agent
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AgentDTO>>))]
        public async Task<IActionResult> GetAgentListAsync([FromQuery] AgentFilter filter
        , [FromQuery] PageParam pageParam
        , [FromQuery] AgentSortByParam sortByParam
        , CancellationToken cancellationToken = default)
        {
            var result = await AgentsService.GetAgentListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Agents, AgentsService.logModel);
        }

        /// <summary>
        /// ข้อมูล Agent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<AgentDTO>))]
        public async Task<IActionResult> GetAgent([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await AgentsService.GetAgentAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentsService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูล Agent
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<AgentDTO>))]
        public async Task<IActionResult> CreateAgentAsync([FromBody] AgentDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await AgentsService.CreateAgentAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, AgentsService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateAgentAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw;
            }
        }
        /// <summary>
        /// แก้ไขข้อมูล Agent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<AgentDTO>))]
        public async Task<IActionResult> UpdateAgentAsync([FromRoute] Guid id, [FromBody] AgentDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await AgentsService.UpdateAgentAsync(id, input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, AgentsService.logModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateAgentAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw;
            }
        }
        /// <summary>
        /// ลบข้อมุล Agent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgentAsync([FromRoute] Guid id)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                await AgentsService.DeleteAgentAsync(id);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(null, AgentsService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteAgentAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw;
            }
        }
    }
}