using Base.DTOs.CTM;
using MST_Auditlog.Params.Filters;
using MST_Auditlog.Params.Outputs;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Common.Helper.Logging;

namespace MST_Auditlog.Services.ContactServices
{
    public class AudtiLogService : IAudtiLogService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }

        public AudtiLogService(DatabaseContext db)
        {
            logModel = new LogModel("AudtiLogService", null);
            DB = db;
        }

        public async Task<ContactAuditPaging> GetContactAuditAsync(ContactAuditFilter filter, PageParam pageParam, ContactAuditSortByParam sortByParam, Guid? userID, CancellationToken cancellationToken = default)
        {
            IQueryable<ContactAuditQueryResult> query = Enumerable.Empty<ContactAuditQueryResult>().AsQueryable();
            query = from contactAudit in DB.Contact_Audits.Include(o => o.CreatedBy).IgnoreQueryFilters().Include(o => o.UpdatedBy).IgnoreQueryFilters()
                    join contact in DB.Contacts on contactAudit.ContactID equals contact.ID into g
                    from t in g.DefaultIfEmpty()
                    select new ContactAuditQueryResult
                    {
                        ContactAudit = contactAudit,
                        Contact = t
                    };

            #region Filter 
            if (!string.IsNullOrEmpty(filter.Actions))
                query = query.Where(q => q.ContactAudit.Actions.Contains(filter.Actions));
            if (!string.IsNullOrEmpty(filter.ContactNo))
                query = query.Where(q => q.ContactAudit.ContactNo.Contains(filter.ContactNo));

            if (!string.IsNullOrEmpty(filter.FirstNameTH))
                query = query.Where(q => q.ContactAudit.FirstNameTH.Contains(filter.FirstNameTH));

            if (!string.IsNullOrEmpty(filter.LastNameTH))
                query = query.Where(q => q.ContactAudit.LastNameTH.Contains(filter.LastNameTH));

            if (filter.UpdatedDateFrom != null && filter.UpdatedDateTo != null)
                query = query.Where(q => q.ContactAudit.Updated >= filter.UpdatedDateFrom && q.ContactAudit.Updated <= filter.UpdatedDateTo);

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
                query = query.Where(q => q.ContactAudit.UpdatedBy.DisplayName.Contains(filter.UpdatedBy)); 
            #endregion

            ContactAuditDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var result = queryResults.Select(o => ContactAuditDTO.CreateFromQueryResult(o))?.ToList();

            return new ContactAuditPaging()
            {
                PageOutput = pageOutput,
                ContactAudit = result
            };
        }


        public async Task<ContactChangeLogPaging> GetContactChangeLogAsync(Guid ContactID, PageParam pageParam, ContactChangeLogSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<ContactChangeLogQueryResult> query = Enumerable.Empty<ContactChangeLogQueryResult>().AsQueryable();

            query = from contactChangeLog in DB.Contact_Change_Logs.Include(o => o.CreatedBy).Include(o => o.UpdatedBy).Where(o => o.ContactID == ContactID)
                    join contact in DB.Contacts on contactChangeLog.ContactID equals contact.ID into g
                    from t in g.DefaultIfEmpty()
                    select new ContactChangeLogQueryResult
                    {
                        ContactChangeLog = contactChangeLog,
                        Contact = t
                    };

            ContactChangeLogDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging(pageParam, ref query);
            var queryResults = await query.ToListAsync(cancellationToken);

            var result = queryResults.Select(o => ContactChangeLogDTO.CreateFromQueryResule(o))?.ToList();

            return new ContactChangeLogPaging()
            {
                PageOutput = pageOutput,
                ContactChangeLog = result
            };
        }

       
    }
}
