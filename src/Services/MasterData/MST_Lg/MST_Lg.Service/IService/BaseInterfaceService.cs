using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using MST_Lg.Params.Filters;
using MST_Lg.Params.Outputs;
using Common.Helper.Logging;

namespace MST_Lg.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
