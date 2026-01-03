using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using MST_General.Service;
using MST_General.Params.Filters;
using Common.Helper.HttpResultHelper;
using Microsoft.AspNetCore.Http.HttpResults;
using Base.DTOs.Common;

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
    //[AllowAnonymous]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class CompaniesController : BaseController
    {
        private ICompanyService CompanyService;
        private readonly DatabaseContext DB;
        private readonly ILogger<CompaniesController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;
        public CompaniesController(ILogger<CompaniesController> logger, ICompanyService companyService, DatabaseContext db, IHttpResultHelper httpResultHelper)
        {
            _logger = logger;
            this.CompanyService = companyService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// Get CompaniesDDL List
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<CompanyDropdownDTO>>))]
        public async Task<IActionResult> GetCompanyDropdownListAsync([FromQuery] CompanyDropdownFilter filter, [FromQuery] CompanyDropdownSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var results = await CompanyService.GetCompanyDropdownListAsync(filter, sortByParam, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, CompanyService.logModel);
        }
        /// <summary>
        /// Get Companies List
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<CompanyDTO>>))]
        public async Task<IActionResult> GetCompanyListAsync([FromQuery] CompanyFilter filter, [FromQuery] PageParam pageParam, [FromQuery] CompanySortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await CompanyService.GetCompanyListAsync(filter, pageParam, sortByParam, cancellationToken);

            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Companies, CompanyService.logModel);
        }
        /// <summary>
        /// Get Company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CompanyDTO>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetCompanyAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await CompanyService.GetCompanyAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, CompanyService.logModel);

        }
        /// <summary>
        /// Create Company
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<CompanyDTO>))]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CompanyDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await CompanyService.CreateCompanyAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, CompanyService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateCompanyAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        /// <summary>
        /// Edit Company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CompanyDTO>))]
        public async Task<IActionResult> UpdateCompanyAsync([FromRoute] Guid id, [FromBody] CompanyDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await CompanyService.UpdateCompanyAsync(id, input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, CompanyService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateCompanyAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        /// <summary>
        /// Delete Company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyAsync([FromRoute] Guid id)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                await CompanyService.DeleteCompanyAsync(id);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(null, CompanyService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteCompanyAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
    }
}