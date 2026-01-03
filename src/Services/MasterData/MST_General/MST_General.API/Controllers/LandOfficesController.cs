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
    public class LandOfficesController : BaseController
    {
        private ILandOfficeService LandOfficeService;
        private readonly DatabaseContext DB;

        private readonly ILogger<AgentsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public LandOfficesController(ILandOfficeService landOfficeService, DatabaseContext db, ILogger<AgentsController> logger, IHttpResultHelper httpResultHelper)
        {
            LandOfficeService = landOfficeService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ลิสของข้อมูล สำนักงานที่ดิน dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<LandOfficeListDTO>>))]
        public async Task<IActionResult> GetLandOfficeDropdownListAsync([FromQuery] string name = null, [FromQuery] Guid? provinceID = null, CancellationToken cancellationToken = default)
        {
            var result = await LandOfficeService.GetLandOfficeDropdownListAsync(name, provinceID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, LandOfficeService.logModel);
        }
        /// <summary>
        /// ลิสของข้อมูล สำนักงานที่ดิน
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<LandOfficeDTO>>))]
        public async Task<IActionResult> GetLandOfficeListAsync([FromQuery] LandOfficeFilter filter, [FromQuery] PageParam pageParam, [FromQuery] LandOfficeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await LandOfficeService.GetLandOfficeListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.LandOffices, LandOfficeService.logModel);
        }
        /// <summary>
        /// ข้อมูลสำนักงานที่ดิน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LandOfficeDTO>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> GetLandOfficeAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await LandOfficeService.GetLandOfficeAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, LandOfficeService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูลสำนักงานที่ดิน
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<LandOfficeDTO>))]
        public async Task<IActionResult> CreateLandOfficeAsync([FromBody] LandOfficeDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await LandOfficeService.CreateLandOfficeAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, LandOfficeService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateLandOfficeAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลสำนักงานที่ดิน
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LandOfficeDTO>))]
        public async Task<IActionResult> UpdateLandOfficeAsync([FromRoute] Guid id, [FromBody] LandOfficeDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await LandOfficeService.UpdateLandOfficeAsync(id, input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, LandOfficeService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateLandOfficeAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        /// <summary>
        /// ลบข้อมูลสำนักงานที่ดิน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLandOfficeAsync([FromRoute] Guid id)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                await LandOfficeService.DeleteLandOfficeAsync(id);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(null, LandOfficeService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteLandOfficeAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
    }
}