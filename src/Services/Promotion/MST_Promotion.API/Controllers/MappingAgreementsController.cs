using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRM;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Services;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace MST_Promotion.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class MappingAgreementsController : BaseController
    {
        private readonly IMappingAgreementService MappingAgreementService;
        private readonly DatabaseContext DB;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<MappingAgreementsController> _logger;

        public MappingAgreementsController(IMappingAgreementService mappingAgreementService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<MappingAgreementsController> logger)
        {
            this.MappingAgreementService = mappingAgreementService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }

        [HttpPost("Import")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ImportMappingAgreementDTO>))]
        public async Task<IActionResult> ImportToGetMappingAgreementsDataFromExcel([FromBody] FileDTO input)
        {
            try
            {
                var results = await MappingAgreementService.GetMappingAgreementsDataFromExcelAsync(input);
                return await _httpResultHelper.SuccessCustomResult(results, MappingAgreementService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ImportToGetMappingAgreementsDataFromExcel", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        [HttpPost("Confirm")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MappingAgreementDTO>>))]
        public async Task<IActionResult> ConfirmImportMappingAgreementsAsync([FromBody] ImportMappingAgreementDTO inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MappingAgreementService.ConfirmImportMappingAgreementsAsync(inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MappingAgreementService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ConfirmImportMappingAgreementsAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        [HttpGet("Export")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportMappingAgreementsAsync([FromQuery] MappingAgreementFilter filter, [FromQuery] MappingAgreementSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await MappingAgreementService.ExportMappingAgreementsAsync(filter, sortByParam, cancellationToken);
                return await _httpResultHelper.SuccessCustomResult(result, MappingAgreementService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportMappingAgreementsAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MappingAgreementDTO>>))]
        public async Task<IActionResult> GetMappingAgreementsList([FromQuery] MappingAgreementFilter filter, [FromQuery] PageParam pageParam, [FromQuery] MappingAgreementSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MappingAgreementService.GetMappingAgreementsList(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MappingAgreements, MappingAgreementService.logModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMappingAgreement([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MappingAgreementService.DeleteMappingAgreementAsync(id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMappingAgreementAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        [HttpGet("ExportTemplates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTemplatesMappingAgreementsAsync()
        {
            try
            {
                var result = await MappingAgreementService.ExportTemplatesMappingAgreementsAsync();
                return await _httpResultHelper.SuccessCustomResult(result, MappingAgreementService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportTemplatesMappingAgreementsAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MappingAgreementDTO>))]
        public async Task<IActionResult> AddMappingAgreementsAsync([FromBody] MappingAgreementDTO inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MappingAgreementService.AddMappingAgreementsAsync(inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MappingAgreementService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "AddMappingAgreementsAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
