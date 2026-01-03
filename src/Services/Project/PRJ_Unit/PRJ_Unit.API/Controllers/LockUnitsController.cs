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
using PRJ_Unit.Services;

namespace PRJ_Unit.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class LockUnitsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<LockUnitsController> _logger;
        private readonly ILockUnitService LockUnitService;
        private readonly IHttpResultHelper _httpResultHelper;

        public LockUnitsController(DatabaseContext dB, ILogger<LockUnitsController> logger, IHttpResultHelper httpResultHelper, ILockUnitService lockUnitService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            LockUnitService = lockUnitService;
        }
        [HttpGet("{projectID}/LockUnit")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<UnitControlLockDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLockUnitAsync([FromRoute] Guid projectID, [FromQuery] UnitControlLockFilter filter, [FromQuery] PageParam pageParam, [FromQuery] UnitControlLockByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await LockUnitService.GetLockUnitAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.unitControlLocks, LockUnitService.logModel);
        }
        [HttpPut("{projectID}/LockUnit")]
        [ProducesResponseType(typeof(ResponseModel<UnitControlLockDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateLockUnitAsync([FromRoute] Guid projectID, UnitControlLockDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    input.ProjectID = projectID;
                    var result = await LockUnitService.UpdateLockUnitAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LockUnitService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateLockUnitAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLockUnitAsync([Required][FromRoute] Guid? id)
        {
            try
            {
                await LockUnitService.DeleteLockUnitAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteLockUnitAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}
