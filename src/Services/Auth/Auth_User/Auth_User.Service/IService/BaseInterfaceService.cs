using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using Auth_User.Params.Filters;
using Auth_User.Params.Outputs;
using Common.Helper.Logging;

namespace Auth_User.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
