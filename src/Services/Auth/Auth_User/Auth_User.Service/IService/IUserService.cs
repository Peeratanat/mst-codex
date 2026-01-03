using Base.DTOs.USR;
using Auth_User.Params.Filters;
using Auth_User.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Helper.Logging;
using Auth;

namespace Auth_User.Services
{
    public interface IUserService : BaseInterfaceService
    {
        Task<UserPaging> GetUserListAsync(UserFilter filter, PageParam pageParam, UserListSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<UserPaging> GetUserForProjectListAsync(UserFilter filter, PageParam pageParam, UserListSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<List<UserListDTO>> GetUserDropdownListAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> SyncUserRoleByEmpCodeAsync(string Empcode, CancellationToken cancellationToken = default);
        Task<List<UserListDTO>> GetUserLCDropdownListAsync(string name, CancellationToken cancellationToken = default);
        Task<List<UserListDTO>> GetKCashCardUserByProjectAsync(Guid? projectID, string textFilter, CancellationToken cancellationToken = default);
        Task SyncUserFromAuthAsync(CancellationToken cancellationToken = default);
        Task<UserListDTO> GetUserAsync(Guid id, CancellationToken cancellationToken = default);
        Task<JsonWebToken> GetUserAppPermissionAsync(Guid userid, CancellationToken cancellationToken = default);
        Task<List<UserListDTO>> GetSaleUserProjectInZoneAsync(Guid ProjectId, bool ignoreQueryFilters, string SaleUserFullName, CancellationToken cancellationToken = default);
        Task<List<UserListDTO>> GetPCardUserByProjectAsync(Guid? transferId);

    }
}
