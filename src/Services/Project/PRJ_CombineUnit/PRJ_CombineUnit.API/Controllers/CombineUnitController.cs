using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using PRJ_CombineUnit.Params.Filters;
using PRJ_CombineUnit.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace PRJ_CombineUnit.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class CombineUnitController : BaseController
    {
        private ICombineUnitService CombineUnitService;
        private readonly DatabaseContext DB;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<CombineUnitController> _logger;

        public CombineUnitController(ICombineUnitService combineUnitService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<CombineUnitController> logger)
        {
            this.CombineUnitService = combineUnitService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }

        /// <summary>
        /// ลิสข้อมูล CombineUNit
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>

        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<CombineUnitDTO>>))]
        public async Task<IActionResult> GetCombineUnitList([FromQuery] CombineUnitFilter filter, [FromQuery] PageParam pageParam, [FromQuery] CombineUnitSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                userID = parsedUserID;
            filter.UserID = userID;
            var result = await CombineUnitService.GetCombineUnitList(filter, pageParam, sortByParam, cancellationToken);

            AddPagingResponse(result.PageOutput);

            return await _httpResultHelper.SuccessCustomResult(result.CombineUnit, CombineUnitService.logModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<List<CombineUnitDTO>>))]
        public async Task<IActionResult> CreateCombineUnitAsync([FromBody] List<CombineUnitDTO> input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await CombineUnitService.CreateCombineUnitAsync(input);
                    await tran.CommitAsync();

                    return await _httpResultHelper.SuccessCustomResult(result, CombineUnitService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateCombineUnitAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        [HttpPost("GetUnitDropdownCanCombine")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<UnitDropdownDTO>>))]
        public async Task<IActionResult> GetUnitDropdownCanCombineAsync([FromBody] CombineUnitDDLDTO input, CancellationToken cancellationToken = default)
        {
            var result = await CombineUnitService.GetUnitDropdownCanCombineAsync(input, cancellationToken);

            return await _httpResultHelper.SuccessCustomResult(result, CombineUnitService.logModel);
        }

        [HttpPost("SendApprove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<CombineUnitDTO>>))]
        public async Task<IActionResult> SendApproveAsync([FromBody] List<CombineUnitDTO> input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await CombineUnitService.SendApproveAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, CombineUnitService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "SendApproveAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        [HttpPost("CreateAndApprove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<CombineUnitDTO>>))]
        public async Task<IActionResult> CreateAndApproveCombineUnitAsync([FromBody] List<CombineUnitDTO> input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await CombineUnitService.CreateAndApproveCombineUnitAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, CombineUnitService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateAndApproveCombineUnitAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }


        [HttpPost("DeleteCombine")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CombineUnitDTO>))]
        public async Task<IActionResult> DeleteCombineAsync([FromBody] CombineUnitDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await CombineUnitService.DeleteCombineAsync(input);
                    await tran.CommitAsync();
                    //return await _httpResultHelper.SuccessCustomResult(result, CombineUnitService.logModel);
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteCombineAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        [AllowAnonymous]
        [HttpPost("Approve")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CombineUnitDTO>))]
        public async Task<IActionResult> ApproveAsync([FromBody] CombineUnitDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await CombineUnitService.ApproveAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, CombineUnitService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ApproveAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        [HttpGet("GetCombineHistory")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<CombineUnitDTO>>))]
        public async Task<IActionResult> GetCombineHistoryList([FromQuery] Guid? CombineID, [FromQuery] PageParam pageParam, CancellationToken cancellationToken = default)
        {
            var result = await CombineUnitService.GetCombineHistoryList(CombineID, pageParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.CombineUnit, CombineUnitService.logModel);

        }

        [HttpGet("DropdownProjectList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ProjectDropdownDTO>>))]
        public async Task<IActionResult> GetProjectDropdownListAsync([FromQuery] string name, [FromQuery] Guid? companyID, [FromQuery] bool isActive = true, [FromQuery] string projectStatusKey = null, CancellationToken cancellationToken = default)
        {

            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                userID = parsedUserID;

            var result = await CombineUnitService.GetProjectDropdownListAsync(name, companyID, isActive, projectStatusKey, userID, cancellationToken);

            return await _httpResultHelper.SuccessCustomResult(result, CombineUnitService.logModel);
        }
    }
}