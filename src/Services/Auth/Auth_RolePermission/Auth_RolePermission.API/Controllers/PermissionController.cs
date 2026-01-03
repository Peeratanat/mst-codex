using Base.DTOs.IDT;
using Database.Models;
using Auth_RolePermission.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PagingExtensions;
using Auth_RolePermission.Params.Filters;
using Database.Models.DbQueries.IDT;
using System.Linq;
using Common.Helper.HttpResultHelper;

namespace Auth_RolePermission.API.Controllers
{
#if !DEBUG
        [Authorize]
#endif

    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class PermissionController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IPermissionService PermissionService;
        private readonly ILogger<PermissionController> Logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public PermissionController(IPermissionService permissionService, DatabaseContext db, ILogger<PermissionController> logger, IHttpResultHelper httpResultHelper)
        {
            PermissionService = permissionService;
            DB = db;
            Logger = logger;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// Change User Role
        /// </summary>
        [HttpGet("ChangeUserRole")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid?))]
        public async Task<IActionResult> ChangeUserRoleAsync([FromQuery] Guid UserID, [FromQuery] Guid RoleID, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await PermissionService.ChangeUserRoleAsync(UserID, RoleID, cancellationToken);
                return await _httpResultHelper.SuccessCustomResult(result, PermissionService.logModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// User Role List
        /// </summary>
        [HttpGet("UserRoles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserRoleDTO>))]
        public async Task<IActionResult> GetUserRolesAsync([FromQuery] Guid UserID, [FromQuery] string EmployeeNo, CancellationToken cancellationToken = default)
        {
            var result = await PermissionService.GetUserRolesAsync(UserID, EmployeeNo, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, PermissionService.logModel);
        }

        /// <summary>
        /// Menu Permission List
        /// </summary>
        [HttpGet("UserMenu")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserMenuDTO>))]
        public async Task<IActionResult> GetUserMenuAsync([FromQuery] Guid UserID, [FromQuery] Guid? RoleID, CancellationToken cancellationToken = default)
        {
            var result = await PermissionService.GetUserMenuAsync(UserID, RoleID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, PermissionService.logModel);
        }

        /// <summary>
        /// User Menu Actions และ Permission
        /// </summary>
        [HttpGet("UserMenuActions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserMenuActionsDTO>))]
        public async Task<IActionResult> GetUserMenuActionsAsync([FromQuery] Guid UserID, [FromQuery] Guid? RoleID, CancellationToken cancellationToken = default)
        {
            var result = await PermissionService.GetUserMenuActionsAsync(UserID, RoleID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, PermissionService.logModel);
        }


        #region Get Permission List

        /// <summary>  
        /// GetPermissionByRole
        /// </summary>
        [HttpGet("GetPermissionByRole")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionByRoleDTO))]
        public async Task<IActionResult> GetPermissionByRoleAsync([FromQuery] PermissionByRoleFilter filter, [FromQuery] PageParam pageParam, [FromQuery] sqlMenuAction.PermissionByRoleSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await PermissionService.GetPermissionByRoleAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.PermissionByRole, PermissionService.logModel);
        }

        /// <summary>
        /// Update PermissionByRole
        /// </summary>
        [HttpPut("UpdatePermissionByRole")]
        public async Task<IActionResult> UpdatePermissionByRoleAsync([FromBody] UpdatePermissionByRoleDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out Guid parsedUserID))
                        userID = parsedUserID;

                    await PermissionService.UpdatePermissionByRoleAsync(input, userID);

                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult("Success", PermissionService.logModel);
                }
                catch (Exception)
                {
                    await tran.RollbackAsync();
                    throw;
                }
            }
        }

        #endregion

        #region Dash Board

        /// <summary>
        /// Dashboard URL List
        /// </summary>
        [HttpGet("GetUserDashboard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDashboardMenuDTO>))]
        public async Task<IActionResult> GetUserDashboardAsync([FromQuery] Guid UserID, [FromQuery] string MasterApp, CancellationToken cancellationToken = default)
        {
            var result = await PermissionService.GetUserDashboardAsync(UserID, MasterApp, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, PermissionService.logModel);
        }

        #endregion

    }
}
