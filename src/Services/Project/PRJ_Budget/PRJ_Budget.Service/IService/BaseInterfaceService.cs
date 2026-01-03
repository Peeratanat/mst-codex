using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using PRJ_Budget.Params.Filters;
using PRJ_Budget.Params.Outputs;
using Common.Helper.Logging;

namespace PRJ_Budget.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
