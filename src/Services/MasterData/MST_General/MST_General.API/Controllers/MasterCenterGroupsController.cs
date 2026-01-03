using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MST_General.Services;
using Database.Models;
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
    public class MasterCenterGroupsController : BaseController
    {
        private IMasterCenterGroupService MasterCenterGroupService;
        private readonly DatabaseContext DB;
        private readonly ILogger<MasterCenterGroupsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;


        public MasterCenterGroupsController(IMasterCenterGroupService masterCenterGroupService, DatabaseContext db, ILogger<MasterCenterGroupsController> logger, IHttpResultHelper httpResultHelper)
        {
            this.MasterCenterGroupService = masterCenterGroupService;
            this.DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ลิสข้อมูล กลุ่มข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterCenterGroupDTO>>))]
        public async Task<IActionResult> GetMasterCenterGroupListAsync([FromQuery] MasterCenterGroupFilter filter, [FromQuery] PageParam pageParam, [FromQuery] MasterCenterGroupSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MasterCenterGroupService.GetMasterCenterGroupListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterCenterGroups, MasterCenterGroupService.logModel);
        }
        /// <summary>
        /// ข้อมูล กลุ่มข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterCenterGroupDTO>))]
        public async Task<IActionResult> GetMasterCenterGroupAsync([FromRoute] string key, CancellationToken cancellationToken = default)
        {
            var result = await MasterCenterGroupService.GetMasterCenterGroupAsync(key, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterCenterGroupService.logModel);
        }
        /// <summary>
        /// เพิ่ม กลุ่มข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<MasterCenterGroupDTO>))]
        public async Task<IActionResult> CreateMasterCenterGroupAsync([FromBody] MasterCenterGroupDTO input)
        {
            try
            {
                var result = await MasterCenterGroupService.CreateMasterCenterGroupAsync(input);
                return await _httpResultHelper.SuccessCustomResult(result, MasterCenterGroupService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateMasterCenterGroupAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไขกลุ่มข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="key"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterCenterGroupDTO>))]
        public async Task<IActionResult> UpdateMasterCenterGroupAsync([FromRoute] string key, [FromBody] MasterCenterGroupDTO input)
        {
            try
            {
                var result = await MasterCenterGroupService.UpdateMasterCenterGroupAsync(key, input);
                return await _httpResultHelper.SuccessCustomResult(result, MasterCenterGroupService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateMasterCenterGroupAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// ลบ กลุ่มข้อมูลกลุ่มพื้นฐานทั่วไป
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteMasterCenterGroupAsync([FromRoute] string key)
        {
            try
            {
                await MasterCenterGroupService.DeleteMasterCenterGroupAsync(key);
                return await _httpResultHelper.SuccessCustomResult(null, MasterCenterGroupService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteMasterCenterGroupAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}