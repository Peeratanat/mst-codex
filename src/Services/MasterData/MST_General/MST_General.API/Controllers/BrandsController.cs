using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using Microsoft.AspNetCore.Mvc;
using MST_General.Services;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using MST_General.API;
using Common.Helper.HttpResultHelper;
using MST_General.Params.Filters;
using Base.DTOs.Common;

namespace MST_General.API.Controllers
{

#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class BrandsController : BaseController
    {
        private readonly DatabaseContext DB;
        private IBrandService BrandService;
        private readonly ILogger<BrandsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public BrandsController(IBrandService brandService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<BrandsController> logger)
        {
            BrandService = brandService;
            DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }
        /// <summary>
        /// ลิสของแบรนด์ Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BrandDropdownDTO>>))]
        public async Task<IActionResult> GetBrandDropdownListAsync([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var result = await BrandService.GetBrandDropdownListAsync(name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BrandService.logModel);
        }
        /// <summary>
        /// ลิสของแบรนด์
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BrandDTO>>))]
        public async Task<IActionResult> GetBrandListAsync([FromQuery] BrandFilter filter, [FromQuery] PageParam pageParam, [FromQuery] BrandSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await BrandService.GetBrandListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Brands, BrandService.logModel);
        }
        /// <summary>
        /// ข้อมูลแบรนด์
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BrandDTO>))]
        public async Task<IActionResult> GetBrandAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await BrandService.GetBrandAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BrandService.logModel);
        }
        /// <summary>
        /// สร้างแบรนด์
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BrandDTO>))]
        public async Task<IActionResult> CreateBrandAsync([FromBody] BrandDTO input)
        {
            var result = await BrandService.CreateBrandAsync(input);
            return await _httpResultHelper.SuccessCustomResult(result, BrandService.logModel, HttpStatusCode.Created);
        }
        /// <summary>
        /// แก้ไขแบรนด์
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BrandDTO>))]
        public async Task<IActionResult> UpdateBrandAsync([FromRoute] Guid id, [FromBody] BrandDTO input)
        {
            var result = await BrandService.UpdateBrandAsync(id, input);
            return await _httpResultHelper.SuccessCustomResult(result, BrandService.logModel);
        }
        /// <summary>
        /// ลบแบรนด์
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrandAsync([FromRoute] Guid id)
        {
            await BrandService.DeleteBrandAsync(id);
            return await _httpResultHelper.SuccessCustomResult(null, BrandService.logModel);
        }
    }
}
