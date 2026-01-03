using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using MST_Event.Params.Filters;
using MST_Event.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;

namespace MST_Event.UnitTests
{
    public class EventServiceTest
    {
        [Fact]
        public async Task GetEventDropownList()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EventService(db);
                        var results = await service.GetEventDropownList();
                        Assert.NotEmpty(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetPaymentOnlineConfig()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EventService(db);
                        var model = await db.Projects.FirstOrDefaultAsync();
                        var results = await service.GetPaymentOnlineConfig(model.ID);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
