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
    public class LegalEntitiesController : BaseController
    {
        private ILegalEntityService LegalEntityService;
        private readonly DatabaseContext DB;
        private readonly ILogger<DistrictsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public LegalEntitiesController(ILegalEntityService legalEntityService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<DistrictsController> logger)
        {
            this.LegalEntityService = legalEntityService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }
        /// <summary>
        /// ลิสข้อมูลนิติบุคคล Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<LegalEntityDropdownDTO>>))]
        public async Task<IActionResult> GetLegalEntityDropdownListAsync([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var result = await LegalEntityService.GetLegalEntityDropdownListAsync(name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, LegalEntityService.logModel);
        }
        /// <summary>
        /// ลิสของข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<LegalEntityDTO>>))]
        public async Task<IActionResult> GetLegalEntityListAsync([FromQuery] LegalFilter filter, [FromQuery] PageParam pageParam, [FromQuery] LegalEntitySortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await LegalEntityService.GetLegalEntityListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.LegalEntities, LegalEntityService.logModel);
        }
        /// <summary>
        /// ข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{id}/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LegalEntityDTO>))]
        public async Task<IActionResult> GetLegalEntityAsync([FromRoute] Guid id, [FromRoute] Guid? projectID, CancellationToken cancellationToken = default)
        {
            var result = await LegalEntityService.GetLegalEntityAsync(id, projectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, LegalEntityService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<LegalEntityDTO>))]
        public async Task<IActionResult> CreateLegalEntityAsync([FromBody] LegalEntityDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await LegalEntityService.CreateLegalEntityAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, LegalEntityService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateLegalEntityAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }

        }
        /// <summary>
        /// แก้ไขข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LegalEntityDTO>))]
        public async Task<IActionResult> UpdateLegalEntityAsync([FromRoute] Guid id, [FromBody] LegalEntityDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await LegalEntityService.UpdateLegalEntityAsync(id, input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, LegalEntityService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateLegalEntityAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        /// <summary>
        /// ลบข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLegalEntityAsync([FromRoute] Guid id)
        {
            try
            {
                await LegalEntityService.DeleteLegalEntityAsync(id);
                return await _httpResultHelper.SuccessCustomResult(null, LegalEntityService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteLegalEntityAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}