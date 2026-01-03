using MST_Gpsim.Params.Filters;
using Base.DTOs.MST;
using Microsoft.AspNetCore.Mvc;
using MST_Gpsim.Services;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using System.Net;
using Report.Integration;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;
using Microsoft.AspNetCore.Authorization;

namespace MST_Gpsim.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class GPSimulateController : BaseController
    {
        private IGPSimulateService GPSimulateService;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<GPSimulateController> _logger;
        private readonly DatabaseContext DB;

        public GPSimulateController(IGPSimulateService gpSimulateService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<GPSimulateController> logger)
        {
            this.GPSimulateService = gpSimulateService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }
        [HttpGet("GPSimulateList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<GPSimulateDTO>>))]
        public async Task<IActionResult> GetGPSimulateListAsync([FromQuery] GPSimulateFilter filter, [FromQuery] PageParam pageParam, [FromQuery] GPSimulateSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                userID = parsedUserID;
            var result = await GPSimulateService.GetGPSimulateListAsync(filter, pageParam, sortByParam, userID, cancellationToken);

            AddPagingResponse(result.PageOutput);

            return await _httpResultHelper.SuccessCustomResult(result.GPSumulateDTOs, GPSimulateService.logModel);
        }
        [HttpGet("GetNewGPFromProjectID/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<GPSimulateDTO>))]
        public async Task<IActionResult> GetGPOriginalAsync([FromRoute] Guid projectID, CancellationToken cancellationToken = default)
        {
            var result = await GPSimulateService.GetGPOriginalAsync(projectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }

        [HttpPost("SaveDraftGPSimulate")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<GPSimulateDTO>))]
        public async Task<IActionResult> SaveDraftGPSimulateAsync([FromBody] GPSimulateDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await GPSimulateService.SaveDraftGPSimulateAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "SaveDraftGPSimulateAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        [HttpPost("SaveCalculateGPSimulate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<GPSimulateDTO>))]
        public async Task<IActionResult> SaveCalculateGPSimulateAsync([FromBody] GPSimulateDTO input)
        {
            GPSimulateDTO result = null;
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Console.WriteLine("SaveCalculateGPSimulateAsync 1 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    result = await GPSimulateService.SaveDraftGPSimulateAsync(input);
                    Console.WriteLine("SaveCalculateGPSimulateAsync 2 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    await tran.CommitAsync();
                    Console.WriteLine("SaveCalculateGPSimulateAsync 3 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "SaveCalculateGPSimulateAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                } 
            }
            Console.WriteLine("SaveCalculateGPSimulateAsync 4 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            await GPSimulateService.CalGPSimulateAsync(result.Id);
            Console.WriteLine("SaveCalculateGPSimulateAsync 5 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }
        [HttpGet("GetGPVersion/{versionID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<GPSimulateDTO>))]
        public async Task<IActionResult> GetGPVersionAsync([FromRoute] Guid versionID, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await GPSimulateService.GetGPVersionAsync(versionID, cancellationToken);
                return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "GetGPVersionAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        [HttpDelete("DeleteGPSimulate/{versionID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> DeleteGPSimulateAsync([FromRoute] Guid? versionID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await GPSimulateService.DeleteGPSimulateAsync(versionID); 
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteGPSimulateAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        [HttpGet("ExportTemplatePrice/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTemplatePriceAsync([FromRoute] Guid projectID, CancellationToken cancellationToken = default)
        {
            var result = await GPSimulateService.ExportTemplatePriceAsync(projectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }
        [HttpPost("ImportPrice/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<GPUnitImportDTO>))]
        public async Task<IActionResult> ImportPrice([FromRoute] Guid projectID, [FromBody] FileDTO fileDTO)
        {
            //Get user ID
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await GPSimulateService.ImportPriceAsync(fileDTO, projectID, userID);
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }
        [HttpPost("ImportUnitCO01/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<GPUnitImportDTO>))]
        public async Task<IActionResult> ImportUnitCO01([FromRoute] Guid projectID, [FromBody] FileDTO fileDTO)
        {
            //Get user ID
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await GPSimulateService.ImportUnitAsync(fileDTO, projectID, userID);
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }
        [HttpPost("ImportBlockCO01/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<GPBlockImportDTO>))]
        public async Task<IActionResult> ImportBlockCO01([FromRoute] Guid projectID, [FromBody] FileDTO fileDTO)
        {
            //Get user ID
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await GPSimulateService.ImportBlockAsync(fileDTO, projectID, userID);
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }
        [HttpGet("PrintChangeBudget/{versionID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ReportResult>))]
        public async Task<IActionResult> PrintChangeBudget([FromRoute] Guid? versionID)
        {
            try
            {
                var result = await GPSimulateService.PrintChangeBudget(versionID);
                return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "PrintChangeBudget", ex?.InnerException?.Message ?? ex?.Message));
                throw;
            }
        }
        [HttpGet("PrintChangePrice/{versionID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ReportResult>))]
        public async Task<IActionResult> PrintChangePrice([FromRoute] Guid? versionID)
        {
            try
            {
                var result = await GPSimulateService.PrintChangePrice(versionID);
                return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "PrintChangePrice", ex?.InnerException?.Message ?? ex?.Message));
                throw;
            }
        }

        [HttpGet("ExportTemplateCO01Block")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTemplateCO01BlockAsync()
        {
            var result = await GPSimulateService.ExportTemplateCO01BlockAsync(); 
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }
        [HttpGet("ExportTemplateCO01Unit")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTemplateCO01UnitAsync()
        {
            var result = await GPSimulateService.ExportTemplateCO01UnitAsync();
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }
        [HttpGet("ExportTemplateCO01UnitFromGP/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTemplateCO01UnitFromGP([FromRoute] Guid projectID)
        {
            var result = await GPSimulateService.ExportTemplateCO01UnitFromGPAsync(projectID); 
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel,(result is null) ? HttpStatusCode.NoContent : HttpStatusCode.OK);
        }
        [HttpGet("ExportTemplateCO01BlockFromGP/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTemplateCO01BlockFromGP([FromRoute] Guid projectID)
        {
            var result = await GPSimulateService.ExportTemplateCO01BlockFromGPAsync(projectID);
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel,(result is null) ? HttpStatusCode.NoContent : HttpStatusCode.OK);
        }
        [HttpGet("ExportTemplatePriceAndCO01/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTemplatePriceAndCO01([FromRoute] Guid projectID)
        {
            var result = await GPSimulateService.ExportTemplatePriceAndCO01Async(projectID);
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel,(result is null) ? HttpStatusCode.NoContent : HttpStatusCode.OK);
        }
        [HttpPost("ImportCO01Unit/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GPCO01UnitImportDTO))]
        public async Task<IActionResult> ImportCO01Unit([FromRoute] Guid projectID, [FromBody] FileDTO fileDTO)
        {
            //Get user ID
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await GPSimulateService.ImportCO01UnitAsync(fileDTO, projectID, userID);
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }
        [HttpPost("ImportPriceAndCO01Unit/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GPCO01UnitImportDTO))]
        public async Task<IActionResult> ImportPriceAndCO01Unit([FromRoute] Guid projectID, [FromBody] FileDTO fileDTO)
        {
            //Get user ID
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await GPSimulateService.ImportPriceAndCO01UnitAsync(fileDTO, projectID, userID);
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }
        [HttpPost("ImportCO01Block/{projectID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GPCO01BlockImportDTO))]
        public async Task<IActionResult> ImportCO01Block([FromRoute] Guid projectID, [FromBody] FileDTO fileDTO)
        {
            //Get user ID
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await GPSimulateService.ImportCO01BlockAsync(fileDTO, projectID, userID);
            return await _httpResultHelper.SuccessCustomResult(result, GPSimulateService.logModel);
        }


    }
}