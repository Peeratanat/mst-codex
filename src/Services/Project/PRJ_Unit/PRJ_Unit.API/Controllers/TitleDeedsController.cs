using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using PRJ_Unit.API;
using PRJ_Unit.API.Controllers;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Inputs;
using PRJ_Unit.Services;

namespace PRJ_Unit.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class TitleDeedsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<TitleDeedsController> _logger;
        private readonly ITitleDeedService TitleDeedService;
        private readonly IHttpResultHelper _httpResultHelper;

        public TitleDeedsController(DatabaseContext dB, ILogger<TitleDeedsController> logger, IHttpResultHelper httpResultHelper, ITitleDeedService titleDeedService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            TitleDeedService = titleDeedService;
        }
        /// <summary>
        /// ลิส ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="request"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/TitleDeeds")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<TitleDeedListDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTitleDeedListAsync([FromRoute] Guid projectID, [FromQuery] TitleDeedFilter request, [FromQuery] PageParam pageParam, [FromQuery] TitleDeedListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await TitleDeedService.GetTitleDeedListAsync(projectID, request, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.TitleDeeds, TitleDeedService.logModel);

        }
        /// <summary>
        /// ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/TitleDeeds/{id}")]
        [ProducesResponseType(typeof(ResponseModel<TitleDeedDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTitleDeedAsync([Required][FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await TitleDeedService.GetTitleDeedAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);
        }
        /// <summary>
        /// สร้าง ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/TitleDeeds")]
        [ProducesResponseType(typeof(ResponseModel<TitleDeedDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTitleDeedAsync([FromRoute] Guid projectID, [FromBody] TitleDeedDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await TitleDeedService.CreateTitleDeedAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateTitleDeedAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/TitleDeeds/{id}")]
        [ProducesResponseType(typeof(ResponseModel<TitleDeedDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTitleDeedAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] TitleDeedDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await TitleDeedService.UpdateTitleDeedAsync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateTitleDeedAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/TitleDeeds/{id}")]
        public async Task<IActionResult> DeleteTitleDeedAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await TitleDeedService.DeleteTitleDeedAsync(projectID, id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, TitleDeedService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteTitleDeedAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import Excel ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/TitleDeeds/Import")]
        [ProducesResponseType(typeof(ResponseModel<TitledeedExcelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportTitleDeedAsync([FromRoute] Guid projectID, [FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                        userID = parsedUserID;

                    var result = await TitleDeedService.ImportTitleDeedAsync(projectID, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportTitleDeedAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export Excel ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/TitleDeeds/Export")]
        [ProducesResponseType(typeof(ResponseModel<FileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportExcelTitleDeedAsync([FromRoute] Guid projectID, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await TitleDeedService.ExportExcelTitleDeedAsync(projectID, cancellationToken);
                return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportExcelTitleDeedAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// กำหนดบ้านเลขที่
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/TitleDeeds/UpdateMultipleHouseNos")]
        public async Task<IActionResult> UpdateMultipleHouseNosAsync([FromRoute] Guid projectID, [FromBody] UpdateMultipleHouseNoParam input)
        {
            try
            {
                await TitleDeedService.UpdateMultipleHouseNosAsync(projectID, input);
                return await _httpResultHelper.SuccessCustomResult(null, TitleDeedService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateMultipleHouseNosAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// กำหนดสำนักงานที่ดิน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/TitleDeeds/UpdateMultipleLandOffices")]
        public async Task<IActionResult> UpdateMultipleLandOfficesAsync([FromRoute] Guid projectID, [FromBody] UpdateMultipleLandOfficeParam input)
        {
            try
            {
                await TitleDeedService.UpdateMultipleLandOfficesAsync(projectID, input);
                return await _httpResultHelper.SuccessCustomResult(null, TitleDeedService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateMultipleLandOfficesAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// ระบุปีที่ได้บ้านเลขที่
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/TitleDeeds/UpdateMultipleHouseNoReceivedYear")]
        public async Task<IActionResult> UpdateMultipleHouseNoReceivedYearAsync([FromRoute] Guid projectID, [FromBody] UpdateMultipleHouseNoReceivedYearParam input)
        {
            try
            {
                await TitleDeedService.UpdateMultipleHouseNoReceivedYearAsync(projectID, input);
                return await _httpResultHelper.SuccessCustomResult(null, TitleDeedService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateMultipleHouseNoReceivedYearAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// Sync TitleDeed from Land
        /// </summary>
        /// <param name="projectNo"></param>
        /// <returns></returns>
        [HttpPost("SyncTitledeedFromLand/{projectNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SyncTitledeedFromLandResponse>))]
        public async Task<IActionResult> SyncTitledeedFromLand([FromRoute] string projectNo, CancellationToken cancellationToken = default)
        {
            var result = await TitleDeedService.SyncTitledeedFromLandAsync(projectNo, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, TitleDeedService.logModel);
        }
    }
}
