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
    public class AgentEmployeesController : BaseController
    {
        private IAgentEmployeeService AgentEmployeeService;
        private readonly ILogger<AgentEmployeesController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly DatabaseContext DB;
        public AgentEmployeesController(DatabaseContext db, IAgentEmployeeService agentEmployeeService, ILogger<AgentEmployeesController> logger, IHttpResultHelper httpResultHelper)
        {
            this.AgentEmployeeService = agentEmployeeService;
            this.DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ข้อมูล AgentEmployee Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AgentEmployeeDropdownDTO>>))]
        public async Task<IActionResult> GetAgentEmployeeDropdownListAsync([FromQuery] string name, [FromQuery] Guid? agentID, CancellationToken cancellationToken = default)
        {
            var result = await AgentEmployeeService.GetAgentEmployeeDropdownListAsync(name, agentID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentEmployeeService.logModel);
        }
        /// <summary>
        /// ลิสข้องข้อมูล AgentEmployee
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AgentEmployeeDTO>>))]
        public async Task<IActionResult> GetAgentEmployeeListAsync([FromQuery] AgentEmployeeFilter filter, [FromQuery] PageParam pageParam, [FromQuery] AgentEmployeeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await AgentEmployeeService.GetAgentEmployeeListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.AgentEmployees, AgentEmployeeService.logModel);
        }
        /// <summary>
        /// ข้อมูล AgentEmployee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<AgentEmployeeDTO>))]
        public async Task<IActionResult> GetAgentEmployeeAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await AgentEmployeeService.GetAgentEmployeeAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgentEmployeeService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูล AgentEmployee
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<AgentEmployeeDTO>))]
        public async Task<IActionResult> CreateAgentEmployeeAsync([FromBody] AgentEmployeeDTO input)
        {
            try
            {
                var result = await AgentEmployeeService.CreateAgentEmployeeAsync(input);
                return await _httpResultHelper.SuccessCustomResult(result, AgentEmployeeService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateAgentEmployeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไขข้อมูล AgentEmployee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<AgentEmployeeDTO>))]
        public async Task<IActionResult> UpdateAgentEmployeeAsync([FromRoute] Guid id, [FromBody] AgentEmployeeDTO input)
        {
            try
            {
                var result = await AgentEmployeeService.UpdateAgentEmployeeAsync(id, input);
                return await _httpResultHelper.SuccessCustomResult(result, AgentEmployeeService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateAgentEmployeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// ลบข้อมุล AgentEmployee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgentEmployeeAsync([FromRoute] Guid id)
        {
            try
            {
                await AgentEmployeeService.DeleteAgentEmployeeAsync(id);
                return await _httpResultHelper.SuccessCustomResult(null, AgentEmployeeService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteAgentEmployeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}