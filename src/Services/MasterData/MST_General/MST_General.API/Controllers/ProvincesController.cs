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
//#if !DEBUG
    [Authorize]
//#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class ProvincesController : BaseController
    {
        private IProvinceService ProvinceService;
        private readonly DatabaseContext DB;
        private readonly ILogger<ProvincesController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public ProvincesController(IProvinceService provinceService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<ProvincesController> logger)
        {
            ProvinceService = provinceService;
            DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }

        /// <summary>
        /// หาจังหวัดจากชื่อ
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("Find")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProvinceListDTO))]
        public async Task<IActionResult> FindProvinceAsync([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var result = await ProvinceService.FindProvinceAsync(name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProvinceService.logModel);
        }

        /// <summary>
        /// ลิส ข้อมูลจังหวัด Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProvinceListDTO>))]
        public async Task<IActionResult> GetProvinceDropdownList([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var result = await ProvinceService.GetProvinceDropdownListAsync(name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProvinceService.logModel);
        }
        /// <summary>
        /// ลิส ข้อมูลจังหวัด
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ProvinceDTO>>))]
        public async Task<IActionResult> GetProvinceListAsync([FromQuery] ProvinceFilter filter
            , [FromQuery] PageParam pageParam
            , [FromQuery] ProvinceSortByParam sortByParam, CancellationToken cancellationToken = default)
        {

            var result = await ProvinceService.GetProvinceListAsync(filter, pageParam, sortByParam,cancellationToken);

            AddPagingResponse(result.PageOutput);

            return await _httpResultHelper.SuccessCustomResult(result.Provinces, ProvinceService.logModel);
        }
        /// <summary>
        /// ข้อมูลจังหวัด
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProvinceDTO>))]
        public async Task<IActionResult> GetProvinceAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await ProvinceService.GetProvinceAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProvinceService.logModel);
        }

        /// <summary>
        /// ข้อมูลจังหวัด
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        [HttpGet("PostalCode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProvinceDTO>))]
        public async Task<IActionResult> GetProvincePostalCodeAsync([FromQuery] string postalCode, CancellationToken cancellationToken = default)
        {
            var result = await ProvinceService.GetProvincePostalCodeAsync(postalCode, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProvinceService.logModel);
        }

        /// <summary>
        /// สร้างข้อมูลจังหวัด
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<ProvinceDTO>))]
        public async Task<IActionResult> CreateProvinceAsync([FromBody] ProvinceDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await ProvinceService.CreateProvinceAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, ProvinceService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError(message: string.Join(" : ", "CreateProvinceAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลจังหวัด
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProvinceDTO>))]
        public async Task<IActionResult> UpdateProvinceAsync([FromRoute] Guid id, [FromBody] ProvinceDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await ProvinceService.UpdateProvinceAsync(id, input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, ProvinceService.logModel);
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError(message: string.Join(" : ", "UpdateProvinceAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// ลบข้อมูลจังหวัด
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvinceAsync([FromRoute] Guid id)
        {
            using var tran = await DB.Database.BeginTransactionAsync();

            try
            {
                await ProvinceService.DeleteProvinceAsync(id);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(null, ProvinceService.logModel);
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError(message: string.Join(" : ", "DeleteProvinceAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}