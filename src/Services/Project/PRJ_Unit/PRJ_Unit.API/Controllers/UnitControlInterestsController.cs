using System;
using System.Collections.Generic;
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
using PRJ_Unit.Services;

namespace PRJ_Unit.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class UnitControlInterestsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<UnitControlInterestsController> _logger;
        private readonly IUnitControlService UnitControlService;
        private readonly IHttpResultHelper _httpResultHelper;

        public UnitControlInterestsController(DatabaseContext dB, ILogger<UnitControlInterestsController> logger, IHttpResultHelper httpResultHelper, IUnitControlService unitControlService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            UnitControlService = unitControlService;
        }
        [HttpGet("{projectID}/UnitControlInterest")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<UnitInterestDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitControlInterestAsync([FromRoute] Guid projectID, [FromQuery] UnitControlInterestFilter filter, [FromQuery] PageParam pageParam, [FromQuery] UnitControlInterestSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await UnitControlService.GetUnitControlInterestAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.unitInterests, UnitControlService.logModel);
        }
        [HttpPost("{projectID}/UnitControlInterest/")]
        [ProducesResponseType(typeof(ResponseModel<UnitControlInterestDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddUnitControlInterestAsync([FromRoute] Guid projectID, UnitControlInterestDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                input.ProjectID = projectID;
                var result = await UnitControlService.AddUnitControlInterestAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, UnitControlService.logModel,HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "AddUnitControlInterestAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        [HttpPut("{projectID}/UnitControlInterest/")]
        [ProducesResponseType(typeof(ResponseModel<UnitControlInterestDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUnitControlInterestAsync([FromRoute] Guid projectID, UnitControlInterestDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                input.ProjectID = projectID;
                var result = await UnitControlService.UpdateUnitControlInterestAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, UnitControlService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateUnitControlInterestAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        [HttpDelete("{projectID}/UnitControlInterest/{id}")]
        public async Task<IActionResult> DeleteUnitControlInterestAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                await UnitControlService.DeleteUnitControlInterestAsync(id);
                await tran.CommitAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteUnitControlInterestAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
    }
}
