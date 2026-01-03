using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using MST_General.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class SubBGsController : BaseController
    {
        private readonly DatabaseContext DB;
        private ISubBGService SubBGService;
        private readonly ILogger<SubBGsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public SubBGsController(DatabaseContext db, ISubBGService subBGService, ILogger<SubBGsController> logger, IHttpResultHelper httpResultHelper)
        {
            this.DB = db;
            this.SubBGService = subBGService;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ลิสข้อมูล SUBG Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bGID"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<SubBGDropdownDTO>>))]
        public async Task<IActionResult> GetSubBGDropdownListAsync([FromQuery] string name, [FromQuery] Guid? bGID = null, CancellationToken cancellationToken = default)
        {
            var result = await SubBGService.GetSubBGDropdownListAsync(name, bGID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, SubBGService.logModel);
        }
        /// <summary>
        /// ลิสข้อมูล Subbg
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<SubBGDTO>>))]
        public async Task<IActionResult> GetSubBGListAsync([FromQuery] SubBGFilter filter, [FromQuery] PageParam pageParam, [FromQuery] SubBGSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await SubBGService.GetSubBGListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.SubBGs, SubBGService.logModel);
        }
        /// <summary>
        /// ข้อมูล Subbg
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SubBGDTO>))]
        public async Task<IActionResult> GetSubBGAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await SubBGService.GetSubBGAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, SubBGService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูล Subbg
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<SubBGDTO>))]
        public async Task<IActionResult> CreateSubBGAsync([FromBody] SubBGDTO input)
        {
            try
            {
                var result = await SubBGService.CreateSubBGAsync(input);
                return await _httpResultHelper.SuccessCustomResult(result, SubBGService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateSubBGAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไขข้อมูล Subbg
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SubBGDTO>))]
        public async Task<IActionResult> UpdateSubBGAsync([FromRoute] Guid id, [FromBody] SubBGDTO input)
        {
            try
            {
                var result = await SubBGService.UpdateSubBGAsync(id, input);
                return await _httpResultHelper.SuccessCustomResult(result, SubBGService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateSubBGAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// ลบข้อมูล Subbg
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubBGAsync([FromRoute] Guid id)
        {
            try
            {
                await SubBGService.DeleteSubBGAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteSubBGAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}