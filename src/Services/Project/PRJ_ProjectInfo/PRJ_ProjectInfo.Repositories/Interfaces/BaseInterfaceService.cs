using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using PRJ_ProjectInfo.Params.Filters;
using PRJ_ProjectInfo.Params.Outputs;
using Common.Helper.Logging;

namespace PRJ_ProjectInfo.Repositories
{
    public interface BaseInterfaceRepositories
    {
        LogModel logModel { get; set; }
    }
}
