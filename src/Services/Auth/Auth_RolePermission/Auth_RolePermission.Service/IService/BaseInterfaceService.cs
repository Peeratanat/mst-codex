using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using Auth_RolePermission.Params.Filters;
using Auth_RolePermission.Params.Outputs;
using Common.Helper.Logging;

namespace Auth_RolePermission.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
