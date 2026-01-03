using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using PRJ_CombineUnit.Params.Filters;
using PRJ_CombineUnit.Params.Outputs;
using Common.Helper.Logging;

namespace PRJ_CombineUnit.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
