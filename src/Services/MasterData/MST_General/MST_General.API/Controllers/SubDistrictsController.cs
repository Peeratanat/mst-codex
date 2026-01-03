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
    public class SubDistrictsController : BaseController
    {
        private ISubDistrictService SubDistrictService;
        private readonly DatabaseContext DB;
        private readonly ILogger<SubDistrictsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;
        public SubDistrictsController(ISubDistrictService subDistrictService, DatabaseContext db, ILogger<SubDistrictsController> logger, IHttpResultHelper httpResultHelper)
        {
            SubDistrictService = subDistrictService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// หาตำบลจากชื่อ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="districtID"></param>
        /// <returns></returns>
        [HttpGet("Find")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SubDistrictListDTO>))]
        public async Task<IActionResult> FindSubDistrictAsync([FromQuery] string name, [FromQuery] Guid districtID, CancellationToken cancellationToken = default)
        {
            var result = await SubDistrictService.FindSubDistrictAsync(districtID, name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, SubDistrictService.logModel);

        }

        /// <summary>
        /// ลิส ข้อมูลอำเภอ Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <param name="districtID"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<SubDistrictListDTO>>))]
        public async Task<IActionResult> GetSubDistrictDropdownListAsync([FromQuery] string name, [FromQuery] Guid? districtID = null, CancellationToken cancellationToken = default)
        {
            var result = await SubDistrictService.GetSubDistrictDropdownListAsync(name, districtID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, SubDistrictService.logModel);
        }
        /// <summary>
        /// ลิสข้อมูลอำเภอ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<SubDistrictDTO>>))]
        public async Task<IActionResult> GetSubDistrictListAsync([FromQuery] SubDistrictFilter filter, [FromQuery] PageParam pageParam, [FromQuery] SubDistrictSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await SubDistrictService.GetSubDistrictListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.SubDistricts, SubDistrictService.logModel);
        }
        /// <summary>
        /// ข้อมูลอำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SubDistrictDTO>))]
        public async Task<IActionResult> GetSubDistrictAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await SubDistrictService.GetSubDistrictAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, SubDistrictService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูล อำเภอ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<SubDistrictDTO>))]
        public async Task<IActionResult> CreateSubDistrictAsync([FromBody] SubDistrictDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await SubDistrictService.CreateSubDistrictAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, SubDistrictService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateSubDistrictAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลอำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SubDistrictDTO>))]
        public async Task<IActionResult> UpdateSubDistrictAsync([FromRoute] Guid id, [FromBody] SubDistrictDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await SubDistrictService.UpdateSubDistrictAsync(id, input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, SubDistrictService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateSubDistrictAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        /// <summary>
        /// ลบข้อมูลอำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubDistrictAsync([FromRoute] Guid id)
        {
            try
            {
                await SubDistrictService.DeleteSubDistrictAsync(id);
                return await _httpResultHelper.SuccessCustomResult(null, SubDistrictService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteSubDistrictAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}