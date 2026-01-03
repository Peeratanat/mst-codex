using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using MST_Gpsim.Params.Filters;
using MST_Gpsim.Params.Outputs;
using Common.Helper.Logging;

namespace MST_Gpsim.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
