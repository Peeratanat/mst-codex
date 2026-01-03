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
    //[AllowAnonymous]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class TypeOfRealEstatesController : BaseController
    {
        private ITypeOfRealEstateService TypeOfRealEstateService;
        private readonly DatabaseContext DB;

        private readonly ILogger<TypeOfRealEstatesController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public TypeOfRealEstatesController(ITypeOfRealEstateService typeOfRealEstateService, DatabaseContext db, ILogger<TypeOfRealEstatesController> logger, IHttpResultHelper httpResultHelper)
        {
            TypeOfRealEstateService = typeOfRealEstateService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ลิสข้อมูลประเภทบ้าน Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<TypeOfRealEstateDropdownDTO>>))]
        public async Task<IActionResult> GetTypeOfRealEstateDropdownList([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var result = await TypeOfRealEstateService.GetTypeOfRealEstateDropdownListAsync(name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, TypeOfRealEstateService.logModel);
        }
        /// <summary>
        /// ลิสข้อมูลประเภทบ้าน
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<TypeOfRealEstateDTO>>))]
        public async Task<IActionResult> GetTypeOfRealEstateList([FromQuery] TypeOfRealEstateFilter filter
            , [FromQuery] PageParam pageParam
            , [FromQuery] TypeOfRealEstateSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await TypeOfRealEstateService.GetTypeOfRealEstateListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.TypeOfRealEstates, TypeOfRealEstateService.logModel);
        }
        /// <summary>
        /// ข้อมูลประเภทบ้าน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<TypeOfRealEstateDTO>))]
        public async Task<IActionResult> GetTypeOfRealEstateAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await TypeOfRealEstateService.GetTypeOfRealEstateAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, TypeOfRealEstateService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูลประเภทบ้าน
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<TypeOfRealEstateDTO>))]
        public async Task<IActionResult> CreateTypeOfRealEstateAsync([FromBody] TypeOfRealEstateDTO input)
        {
            var result = await TypeOfRealEstateService.CreateTypeOfRealEstateAsync(input);
            return await _httpResultHelper.SuccessCustomResult(result, TypeOfRealEstateService.logModel, HttpStatusCode.Created);
        }
        /// <summary>
        /// แก้ไขข้อมูลประเภทบ้าน
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<TypeOfRealEstateDTO>))]
        public async Task<IActionResult> UpdateTypeOfRealEstateAsync([FromRoute] Guid id, [FromBody] TypeOfRealEstateDTO input)
        {
            var result = await TypeOfRealEstateService.UpdateTypeOfRealEstateAsync(id, input);
            return await _httpResultHelper.SuccessCustomResult(result, TypeOfRealEstateService.logModel);
        }
        /// <summary>
        /// ลบข้อมูลประเภทบ้าน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeOfRealEstateAsync([FromRoute] Guid id)
        {
            await TypeOfRealEstateService.DeleteTypeOfRealEstateAsync(id);
            return await _httpResultHelper.SuccessCustomResult(null, TypeOfRealEstateService.logModel);
        }
    }
}