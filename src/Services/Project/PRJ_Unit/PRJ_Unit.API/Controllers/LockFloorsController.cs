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
    public class LockFloorsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<LockFloorsController> _logger;
        private readonly ILockFloorService LockFloorService;
        private readonly IHttpResultHelper _httpResultHelper;

        public LockFloorsController(DatabaseContext dB, ILogger<LockFloorsController> logger, IHttpResultHelper httpResultHelper, ILockFloorService lockFloorService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            LockFloorService = lockFloorService;
        }

        [HttpGet("{projectID}/LockFloor")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<UnitControlLockDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLockFloorAsync([FromRoute] Guid projectID, [FromQuery] UnitControlLockFilter filter, [FromQuery] PageParam pageParam, [FromQuery] UnitControlLockByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await LockFloorService.GetLockFloorAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.unitControlLocks, LockFloorService.logModel);
        }
        [HttpPut("{projectID}/LockFloor")]
        [ProducesResponseType(typeof(ResponseModel<UnitControlLockDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateLockFloorAsync([FromRoute] Guid projectID, UnitControlLockDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    input.ProjectID = projectID;
                    var result = await LockFloorService.UpdateLockFloorAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LockFloorService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateLockFloorAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLockFloorAsync([Required][FromRoute] Guid id)
        {
            try
            {
                await LockFloorService.DeleteLockFloorAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteLockFloorAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}
