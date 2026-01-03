using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using MST_Titledeeds.Params.Filters;
using MST_Titledeeds.Services;
using Common.Helper.HttpResultHelper;
using Report.Integration;
using Base.DTOs.Common;

namespace MST_Titledeeds.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class TitleDeedsController : BaseController
    {
        private readonly ITitleDeedService TitleDeedService;
        private readonly DatabaseContext DB;

        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<TitleDeedsController> _logger;
        public TitleDeedsController(ITitleDeedService titleDeedService, DatabaseContext db, ILogger<TitleDeedsController> logger, IHttpResultHelper httpResultHelper)
        {
            this.TitleDeedService = titleDeedService;
            this.DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// ลิส ข้อมูลโฉนด
        /// </summary>
        /// <returns>The title deed list.</returns>
        /// <param name="request">Request.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        /// <param name="projectID">Project identifier.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<TitleDeedListDTO>>))]
        public async Task<IActionResult> GetTitleDeedListAsync([FromQuery] TitleDeedFilter request, [FromQuery] PageParam pageParam, [FromQuery] TitleDeedListSortByParam sortByParam, [FromQuery] Guid? projectID = null, CancellationToken cancellationToken = default)
        {
            var result = await TitleDeedService.GetTitleDeedListAsync(projectID, request, pageParam, sortByParam,cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.TitleDeeds, TitleDeedService.logModel);
        }
        /// <summary>
        /// ข้อมูลสถานะโฉนด
        /// </summary>
        /// <returns>The title deed list.</returns>
        /// <param name="request">Request.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        /// <param name="projectID">Project identifier.</param>
        [HttpGet("GetTitleDeedStatus")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<TitleDeedListDTO>>))]
        public async Task<IActionResult> GetTitleDeedStatusListAsync([FromQuery] TitleDeedFilter request, [FromQuery] PageParam pageParam, [FromQuery] TitleDeedListSortByParam sortByParam, [FromQuery] Guid? projectID = null, CancellationToken cancellationToken = default)
        {
            var result = await TitleDeedService.GetTitleDeedStatusListAsync(projectID, request, pageParam, sortByParam,cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.TitleDeeds, TitleDeedService.logModel);
        }
        /// <summary>
        /// ข้อมูลสถานะโฉนดทั้งหมด by project
        /// </summary>
        /// <returns>The title deed list.</returns>
        /// <param name="request">Request.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        /// <param name="projectID">Project identifier.</param>
        [HttpGet("GetTitleDeedStatusSelectAllList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<TitleDeedListDTO>>))]
        public async Task<IActionResult> GetTitleDeedStatusSelectAllListAsync([FromQuery] TitleDeedFilter request, [FromQuery] PageParam pageParam, [FromQuery] TitleDeedListSortByParam sortByParam, [FromQuery] Guid? projectID = null, CancellationToken cancellationToken = default)
        {
            var result = await TitleDeedService.GetTitleDeedStatusSelectAllListAsync(projectID, request, pageParam, sortByParam,cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.TitleDeeds, TitleDeedService.logModel);
        }

        /// <summary>
        /// ข้อมูลโฉนด
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<TitleDeedDTO>))]
        public async Task<IActionResult> GetTitleDeedAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await TitleDeedService.GetTitleDeedAsync(id,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);
        }

        /// <summary>
        /// แก้ไขสถานะโฉนด
        /// </summary>
        /// <returns>The titledeed status.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/Status")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<TitleDeedDTO>))]
        public async Task<IActionResult> UpdateTitleDeedStatusAsync([FromRoute] Guid id, [FromBody] TitleDeedDTO input)
        {
            try
            {
                var result = await TitleDeedService.UpdateTitleDeedStatusAsync(id, input);
                return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateTitleDeedStatusAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไขสถานะโฉนด List
        /// </summary>
        /// <returns>The titledeed status.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/ListStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<TitleDeedDTO>))]
        public async Task<IActionResult> UpdateTitleDeedListStatusAsync([FromRoute] Guid id, [FromBody] TitleDeedFilterInput input)
        {
            try
            {
                var result = await TitleDeedService.UpdateTitleDeedListStatusAsync(id, input.Items);

                return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateTitleDeedListStatusAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// ดึงรายการประวัติสถานะโฉนด
        /// </summary>
        /// <returns>The titledeed history items.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}/History")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<TitleDeedDTO>>))]
        public async Task<IActionResult> GetTitleDeedHistoryItemsAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var results = await TitleDeedService.GetTitleDeedHistoryItemsAsync(id,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, TitleDeedService.logModel);
        }

        [HttpPost("ExportDebtFreePrintFormUrl")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ReportResult>))]
        public async Task<IActionResult> ExportDebtFreePrintFormUrlAsync([FromBody] TitleDeedReportDTO input)
        {

            try
            {
                Guid? userID = null;
                //Guid? userID = Guid.Parse("DBBC7F68-4EA8-4C14-9F5E-72D390A5F287");
                Guid parsedUserID;
                if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                {
                    userID = parsedUserID;
                }

                var result = await TitleDeedService.ExportDebtFreePrintFormUrlAsync(input, userID);
                return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);

            }
            catch (Exception ex)
            {

                _logger.LogError(message: string.Join(" : ", "ExportDebtFreePrintFormUrlAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// Import ประวัติสถานะโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/TitleDeedStatusImport")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<TitleDeedHistoryExcelDTO>))]
        public async Task<IActionResult> ImportTitleDeedHistoryAsync([FromRoute] Guid projectID, [FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await TitleDeedService.ImportTitleDeedHistoryAsync(projectID, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportTitleDeedHistoryAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Export ประวัติสถานะโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/ExportTitleDeedStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTitleDeedStatus([FromRoute] Guid projectID, [FromBody] TitleDeedDTO input, CancellationToken cancellationToken = default)
        {
            var result = await TitleDeedService.ExportTitleDeedStatusAsync(projectID, input,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);
        }



    }
}
