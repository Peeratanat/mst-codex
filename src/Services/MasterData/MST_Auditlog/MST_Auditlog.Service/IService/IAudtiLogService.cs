using Base.DTOs.CTM;
using MST_Auditlog.Params.Filters;
using MST_Auditlog.Params.Outputs;
using PagingExtensions;

namespace MST_Auditlog.Services.ContactServices
{
    public interface IAudtiLogService : BaseInterfaceService
    {
        Task<ContactAuditPaging> GetContactAuditAsync(ContactAuditFilter input, PageParam pageParam, ContactAuditSortByParam sortByParam, Guid? userID, CancellationToken cancellationToken = default);
        Task<ContactChangeLogPaging> GetContactChangeLogAsync(Guid ContactID, PageParam pageParam, ContactChangeLogSortByParam sortByParam, CancellationToken cancellationToken = default);

    }
}
