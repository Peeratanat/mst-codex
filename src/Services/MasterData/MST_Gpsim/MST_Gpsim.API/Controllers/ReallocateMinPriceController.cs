using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MST_Gpsim.Params.Filters;
using Base.DTOs.MST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MST_Gpsim.Services;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Base.DTOs.FIN;
using Report.Integration;
using Database.Models.FIN;
using Microsoft.CodeAnalysis;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace MST_Gpsim.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class ReallocateMinPriceController : BaseController
    {
        private IReallocateMinPriceService ReallocateMinPriceService;
        private readonly DatabaseContext DB;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<ReallocateMinPriceController> _logger;

        public ReallocateMinPriceController(IReallocateMinPriceService reallocateMinPriceService, DatabaseContext db, ILogger<ReallocateMinPriceController> logger, IHttpResultHelper httpResultHelper)
        {
            ReallocateMinPriceService = reallocateMinPriceService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        [HttpGet("GetReallocateMinPriceList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ReallocateMinPriceDTO>>))]
        public async Task<IActionResult> GetReallocateMinPriceListAsync([FromQuery] ReallocateMinPriceFilter filter, [FromQuery] PageParam pageParam, [FromQuery] ReallocateMinPriceSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                userID = parsedUserID;
            var result = await ReallocateMinPriceService.GetReallocateMinPriceListAsync(filter, pageParam, sortByParam, userID, cancellationToken);

            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.ReallocateMinPriceDTOs, ReallocateMinPriceService.logModel);
        }
        [HttpGet("GetNewReallocateMinPriceFromProjectID/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<GPSimulateDTO>))]
        public async Task<IActionResult> GetGPOriginalAsync([FromRoute] Guid projectID, CancellationToken cancellationToken = default)
        {
            var result = await ReallocateMinPriceService.GetGPOriginalAsync(projectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ReallocateMinPriceService.logModel);
        }
        [HttpPost("SaveDraftReallocateMinPrice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ReallocateMinPriceDTO>))]
        public async Task<IActionResult> SaveDraftReallocateMinPriceAsync([FromBody] ReallocateMinPriceDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ReallocateMinPriceService.SaveDraftReallocateMinPriceAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ReallocateMinPriceService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "SaveDraftReallocateMinPriceAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        [HttpPost("SaveCalculateReallocateMinPrice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ReallocateMinPriceDTO>))]
        public async Task<IActionResult> SaveCalculateReallocateMinPriceAsync([FromBody] ReallocateMinPriceDTO input)
        {
            ReallocateMinPriceDTO result = null;
            using (var tran = await DB.Database.BeginTransactionAsync()) 
            {
                try
                {
                    result = await ReallocateMinPriceService.SaveDraftReallocateMinPriceAsync(input);

                    await tran.CommitAsync();
                   
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "SaveCalculateReallocateMinPriceAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            } 
            // Cal
            await ReallocateMinPriceService.CalReallocateMinPriceAsync(result.Id);
            return await _httpResultHelper.SuccessCustomResult(result, ReallocateMinPriceService.logModel);
        }
        [HttpGet("GetReallocateMinPrice/{versionID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ReallocateMinPriceDTO>))]
        public async Task<IActionResult> GetReallocateMinPriceAsync([FromRoute] Guid versionID, CancellationToken cancellationToken = default)
        {

            var result = await ReallocateMinPriceService.GetReallocateMinPriceAsync(versionID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ReallocateMinPriceService.logModel);

        }
        [HttpDelete("DeleteReallocateMinPrice/{versionID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> DeleteReallocateMinPriceAsync([FromRoute] Guid? versionID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ReallocateMinPriceService.DeleteReallocateMinPriceAsync(versionID);

                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ReallocateMinPriceService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteReallocateMinPriceAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        [HttpPost("ImportReallocateMinPrice/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<GPUnitImportDTO>))]
        public async Task<IActionResult> ImportReallocateMinPrice([FromRoute] Guid projectID, [FromBody] FileDTO fileDTO)
        {
            //Get user ID
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await ReallocateMinPriceService.ImportReallocateMinPriceAsync(fileDTO, projectID, userID);
            return await _httpResultHelper.SuccessCustomResult(result, ReallocateMinPriceService.logModel);
        }
        [HttpGet("ExportTemplateReallocateMinPrice/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTemplateReallocateMinPrice([FromRoute] Guid projectID)
        {
            var result = await ReallocateMinPriceService.ExportTemplateReallocateMinPrice(projectID);
            return await _httpResultHelper.SuccessCustomResult(result, ReallocateMinPriceService.logModel);
        }
        [HttpGet("PrintReallocateMinPrice/{versionID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ReportResult>))]
        public async Task<IActionResult> PrintReallocateMinPrice([FromRoute] Guid? versionID)
        {
            try
            {
                var result = ReallocateMinPriceService.PrintReallocateMinPrice(versionID);
                return await _httpResultHelper.SuccessCustomResult(result, ReallocateMinPriceService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "PrintReallocateMinPrice", ex?.InnerException?.Message ?? ex?.Message));
                throw;
            }
        }
    }
}