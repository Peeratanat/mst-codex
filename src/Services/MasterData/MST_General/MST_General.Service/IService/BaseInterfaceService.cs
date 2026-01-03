using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
