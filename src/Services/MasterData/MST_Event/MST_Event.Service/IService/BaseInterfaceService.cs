using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using MST_Event.Params.Filters;
using MST_Event.Params.Outputs;
using Common.Helper.Logging;

namespace MST_Event.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
