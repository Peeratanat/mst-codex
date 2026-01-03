using Base.DTOs.IDT;
using Database.Models;
using Auth_RolePermission.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PagingExtensions;
using System.Linq;
using Common.Helper.HttpResultHelper;
namespace Auth_RolePermission.API.Controllers
{
    //#if !DEBUG
    [Authorize]
    //#endif

    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class MenuController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IPermissionService PermissionService;
        private readonly ILogger<MenuController> Logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public MenuController(IPermissionService permissionService, DatabaseContext db, ILogger<MenuController> logger, IHttpResultHelper httpResultHelper)
        {
            PermissionService = permissionService;
            DB = db;
            Logger = logger;
            _httpResultHelper = httpResultHelper;
        }



        /// <summary>  
        /// Get RoleDropdownList
        /// </summary>
        [HttpGet("GetRoleDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleDropdownDTO>))]
        public async Task<IActionResult> GetRoleDropdownListAsync([FromQuery] string DisplayName, CancellationToken cancellationToken = default)
        {
            var result = await PermissionService.GetRoleDropdownListAsync(DisplayName, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, PermissionService.logModel);
        }

        /// <summary>  
        /// Get ModuleDropdown
        /// </summary>
        [HttpGet("GetModuleDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ModuleDropdownDTO>))]
        public async Task<IActionResult> GetModuleDropdownListAsync([FromQuery] string DisplayName, CancellationToken cancellationToken = default)
        {
            var result = await PermissionService.GetModuleDropdownListAsync(DisplayName, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, PermissionService.logModel);
        }

        /// <summary>  
        /// Get MenuDropdownList
        /// </summary>
        [HttpGet("GetMenuDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MenuDropdownDTO>))]
        public async Task<IActionResult> GetMenuDropdownListAsync([FromQuery] List<Guid?> ModuleIDs, [FromQuery] string DisplayName, CancellationToken cancellationToken = default)
        {
            var result = await PermissionService.GetMenuDropdownListAsync(ModuleIDs, DisplayName, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, PermissionService.logModel);
        }

        /// <summary>  
        /// Get MenuActionDropdownList
        /// </summary>
        [HttpGet("GetMenuActionDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MenuActionDropdownDTO>))]
        public async Task<IActionResult> GetMenuActionDropdownListAsync([FromQuery] List<Guid?> ModuleIDs, [FromQuery] List<Guid?> MenuIDs, [FromQuery] string DisplayName, CancellationToken cancellationToken = default)
        {
            var result = await PermissionService.GetMenuActionDropdownListAsync(ModuleIDs, MenuIDs, DisplayName, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, PermissionService.logModel);
        }


    }
}
