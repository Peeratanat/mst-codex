using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using MST_Titledeeds.Params.Filters;
using MST_Titledeeds.Params.Outputs;
using Common.Helper.Logging;

namespace MST_Titledeeds.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
