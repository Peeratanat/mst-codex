using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using MST_Auditlog.Params.Filters;
using MST_Auditlog.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using MST_Auditlog.Services.ContactServices;
using Base.DTOs.CTM;

namespace MST_Auditlog.UnitTests
{
    public class AudtiLogServiceTest
    {
        [Fact]
        public async Task GetContactAuditAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new AudtiLogService(db);
                        var user = await db.Users.FirstOrDefaultAsync(f => f.EmployeeNo == "AP006316");

                        ContactAuditFilter filter = FixtureFactory.Get().Build<ContactAuditFilter>().Create();
                        PageParam pageParam = new PageParam();
                        ContactAuditSortByParam sortByParam = new ContactAuditSortByParam();

                        var results = await service.GetContactAuditAsync(filter, pageParam, sortByParam, user.ID);

                        filter = new ContactAuditFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(ContactAuditSortBy)).Cast<ContactAuditSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ContactAuditSortByParam() { SortBy = item };
                            results = await service.GetContactAuditAsync(filter, pageParam, sortByParam, user.ID);
                            Assert.NotEmpty(results.ContactAudit);
                        }
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetContactChangeLogAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new AudtiLogService(db);

                        var model = await (from contactChangeLog in db.Contact_Change_Logs.Include(o => o.CreatedBy).Include(o => o.UpdatedBy) 
                                    join contact in db.Contacts on contactChangeLog.ContactID equals contact.ID into g
                                    from t in g.DefaultIfEmpty()
                                    select contactChangeLog).FirstOrDefaultAsync();

                        PageParam pageParam = new PageParam();
                        ContactChangeLogSortByParam sortByParam = new ContactChangeLogSortByParam();

                        var results = await service.GetContactChangeLogAsync((Guid)model.ContactID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(ContactChangeLogSortBy)).Cast<ContactChangeLogSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ContactChangeLogSortByParam() { SortBy = item };
                            results = await service.GetContactChangeLogAsync((Guid)model.ContactID, pageParam, sortByParam);
                            Assert.NotEmpty(results.ContactChangeLog);
                        }
                        await tran.RollbackAsync();
                    }
                });
            }
        }


    }
}
