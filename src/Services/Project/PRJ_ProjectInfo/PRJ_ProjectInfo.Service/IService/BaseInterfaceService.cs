using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using PRJ_ProjectInfo.Params.Filters;
using PRJ_ProjectInfo.Params.Outputs;
using Common.Helper.Logging;

namespace PRJ_ProjectInfo.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
