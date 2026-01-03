using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using CTM_CustomerConsent.Params.Filters;
using CTM_CustomerConsent.Params.Outputs;
using Common.Helper.Logging;

namespace CTM_CustomerConsent.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
