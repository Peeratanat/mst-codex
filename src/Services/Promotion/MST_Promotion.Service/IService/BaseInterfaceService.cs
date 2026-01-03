using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Params.Outputs;
using Common.Helper.Logging;

namespace MST_Promotion.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
