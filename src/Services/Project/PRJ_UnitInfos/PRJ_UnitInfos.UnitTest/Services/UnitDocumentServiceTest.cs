using Base.DTOs.SAL;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_UnitInfos.Params.Filters;
using PRJ_UnitInfos.Services;

namespace PRJ_UnitInfos.UnitTests
{
    public class UnitDocumentServiceTest : BaseServiceTest
    {

        [Fact]
        public async Task GetUnitDocumentListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        // Act
                        var service = new UnitDocumentService(db);
                        UnitDocumentFilter filter = new UnitDocumentFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        UnitDocumentSortByParam sortByParam = new UnitDocumentSortByParam();

                        var results = await service.GetUnitDocumentDropdownListAsync(filter, pageParam, sortByParam);
                        Assert.NotNull(results.UnitDocuments);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetDocumentOwnerAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        // Act
                        var service = new UnitDocumentService(db);
                        UnitDocumentFilter filter = new UnitDocumentFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        UnitDocumentSortByParam sortByParam = new UnitDocumentSortByParam();

                        var BookingID = await db.Bookings.FirstOrDefaultAsync();

                        var results = await service.GetDocumentOwnerAsync(BookingID.ID);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
