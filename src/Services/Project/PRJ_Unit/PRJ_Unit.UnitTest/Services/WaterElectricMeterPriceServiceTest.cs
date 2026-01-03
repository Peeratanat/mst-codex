using Base.DTOs;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.UnitTests
{
    public class WaterElectricMeterPriceServiceTest
    {
        [Fact]
        public async Task GetWaterElectricMeterPriceListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var model = await db.WaterElectricMeterPrices.AsNoTracking().FirstAsync();
                var service = new WaterElectricMeterPriceService(db);
                PageParam pageParam = new() { Page = 1, PageSize = 10 };
                SortByParam sortBy = new()
                {
                    SortBy = "",
                    Ascending = false
                };

                var result = await service.GetWaterElectricMeterPriceListAsync((Guid)model.ModelID, new WaterElectricMeterPriceFilter(), pageParam, sortBy);
                Assert.NotEmpty(result.WaterElectricMeterPrices);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetWaterElectricMeterPriceAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var model = await db.WaterElectricMeterPrices.AsNoTracking().FirstAsync();
                var service = new WaterElectricMeterPriceService(db);

                var result = await service.GetWaterElectricMeterPriceAsync((Guid)model.ModelID, model.ID);
                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

    }
}
