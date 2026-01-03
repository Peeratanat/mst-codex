using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Database.Models;
using MST_General.Params.Filters;
using MST_General.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    public class CountriesController : BaseController
    {
        private readonly ICountryService CountryService;
        private readonly ILogger<CountriesController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly DatabaseContext DB;
        public CountriesController(ICountryService countryService, DatabaseContext db, ILogger<CountriesController> logger, IHttpResultHelper httpResultHelper)
        {
            CountryService = countryService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ลิสของประเทศ Dropdown
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<CountryDTO>>))]
        public async Task<IActionResult> GetCountryDropdownListAsync([FromQuery] CountryFilter filter, CancellationToken cancellationToken = default)
        {
            var results = await CountryService.GetCountryDropdownListAsync(filter, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, CountryService.logModel);
        }

        /// <summary>
        /// ลิสของ ข้อมูลประเทศ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<CountryDTO>>))]
        public async Task<IActionResult> GetCountryListAsync([FromQuery] CountryFilter filter, [FromQuery] PageParam pageParam, [FromQuery] CountrySortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await CountryService.GetCountryListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Countries, CountryService.logModel);
        }

        /// <summary>
        /// ข้อมูลประเทศ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CountryDTO>))]
        public async Task<IActionResult> GetCountryAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await CountryService.GetCountryAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, CountryService.logModel);
        }

        /// <summary>
        /// หาประเทศจาก code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("Find")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CountryDTO>))]
        public async Task<IActionResult> FindCountryAsync([FromQuery] string code, CancellationToken cancellationToken = default)
        {
            var result = await CountryService.FindCountryAsync(code, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, CountryService.logModel);
        }

        /// <summary>
        /// สร้าง ข้อมูลประเทศ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<CountryDTO>))]
        public async Task<IActionResult> CreateCountryAsync([FromBody] CountryDTO input)
        {
            var result = await CountryService.CreateCountryAsync(input);
            return await _httpResultHelper.SuccessCustomResult(result, CountryService.logModel, HttpStatusCode.Created);
        }

        /// <summary>
        /// แก้ไข ข้อมูลประเทศ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CountryDTO>))]
        public async Task<IActionResult> UpdateBGAsync([FromRoute] Guid id, [FromBody] CountryDTO input)
        {
            var result = await CountryService.UpdateCountryAsync(id, input);
            return await _httpResultHelper.SuccessCustomResult(result, CountryService.logModel);
        }

        /// <summary>
        /// ลบ ข้อมูลประเทศ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryAsync([FromRoute] Guid id)
        {
            await CountryService.DeleteCountryAsync(id);
            return Ok();
        }
    }
}
