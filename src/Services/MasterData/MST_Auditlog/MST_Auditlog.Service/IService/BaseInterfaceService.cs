using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using MST_Auditlog.Params.Filters;
using MST_Auditlog.Params.Outputs;
using Common.Helper.Logging;

namespace MST_Auditlog.Services
{
    public interface BaseInterfaceService
    {
        LogModel logModel { get; set; }
    }
}
