using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using PRJ_UnitInfos.Params.Filters;
using PRJ_UnitInfos.Params.Outputs;
using Common.Helper.Logging;

namespace PRJ_UnitInfos.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
