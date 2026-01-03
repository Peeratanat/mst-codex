using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.IDT;
using Database.Models.DbQueries.IDT;
using Auth_RolePermission.Params.Filters;
using Auth_RolePermission.Params.Outputs;
using PagingExtensions;

namespace Auth_RolePermission.Services
{
    public interface IPermissionService : BaseInterfaceService
    {
        Task<Guid?> ChangeUserRoleAsync(Guid UserID, Guid RoleID, CancellationToken cancellationToken = default);

        Task<List<UserRoleDTO>> GetUserRolesAsync(Guid UserID, string EmployeeNo, CancellationToken cancellationToken = default);

        Task<List<UserMenuDTO>> GetUserMenuAsync(Guid UserID, Guid? RoleID, CancellationToken cancellationToken = default);

        Task<List<UserMenuActionsDTO>> GetUserMenuActionsAsync(Guid UserID, Guid? RoleID, CancellationToken cancellationToken = default);

        #region Get Permission List

        Task<List<RoleDropdownDTO>> GetRoleDropdownListAsync(string DisplayName, CancellationToken cancellationToken = default);
        Task<List<ModuleDropdownDTO>> GetModuleDropdownListAsync(string DisplayName, CancellationToken cancellationToken = default);
        Task<List<MenuDropdownDTO>> GetMenuDropdownListAsync(List<Guid?> ModuleIDs, string DisplayName, CancellationToken cancellationToken = default);
        Task<List<MenuActionDropdownDTO>> GetMenuActionDropdownListAsync(List<Guid?> ModuleIDs, List<Guid?> MenuIDs, string DisplayName, CancellationToken cancellationToken = default);
        Task<PermissionByRolePaging> GetPermissionByRoleAsync(PermissionByRoleFilter filter, PageParam pageParam, sqlMenuAction.PermissionByRoleSortByParam sortByParam, CancellationToken cancellationToken = default);

        #endregion

        #region Update
        Task UpdatePermissionByRoleAsync(UpdatePermissionByRoleDTO model, Guid? userID);
        #endregion

        Task<List<UserDashboardMenuDTO>> GetUserDashboardAsync(Guid UserID, string MasterApp, CancellationToken cancellationToken = default);
    }
}
