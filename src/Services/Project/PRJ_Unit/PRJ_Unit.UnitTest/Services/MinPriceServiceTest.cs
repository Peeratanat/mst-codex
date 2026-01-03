using Base.DTOs.PRJ;
using Database.Models.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.UnitTests
{
    public class MinPriceServiceTest
    {
        IConfiguration Configuration;
        public MinPriceServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async Task GetMinPriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Projects.AsNoTracking().FirstAsync(f => f.ProjectNo == "60022");
                        var service = new MinPriceService(db);


                        PageParam pageParam = new PageParam()
                        {
                            Page = 1,
                            PageSize = 1
                        };
                        MinPriceSortByParam sortByParam = new MinPriceSortByParam
                        {
                            SortBy = MinPriceSortBy.Unit_UnitNo,
                        };
                        var result = await service.GetMinPriceListAsync(model.ID, new MinPriceFilter(), pageParam, sortByParam);
                        Assert.NotEmpty(result.MinPrices);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetMinPriceAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.MinPrices.AsNoTracking().FirstAsync();
                        var service = new MinPriceService(db);

                        var result = await service.GetMinPriceAsync((Guid)model.ProjectID, model.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task CreateMinPriceAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MinPriceService(db);


                        var minPrice = await db.MinPrices.FirstAsync();
                        var titledeedDetail = await db.TitledeedDetails.FirstAsync();

                        var input = MinPriceDTO.CreateFromModel(minPrice, titledeedDetail);

                        var result = await service.CreateMinPriceAsync((Guid)minPrice.ProjectID, input);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task UpdateMinPriceAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var dateNow = DateTime.Now;
                        var service = new MinPriceService(db);
                        var minPrice = await db.MinPrices.FirstAsync();
                        var titledeedDetail = await db.TitledeedDetails.FirstAsync();

                        var input = MinPriceDTO.CreateFromModel(minPrice, titledeedDetail);
                        input.Cost = 99; 
                        input.ROIMinprice = 99;
                        input.SalePrice = 99;
                        var result = await service.UpdateMinPriceAsync((Guid)minPrice.ProjectID, (Guid)input.Id, input);
                        Assert.NotNull(result);
                        Assert.Equal(result.Cost, 99);
                        Assert.Equal(result.ROIMinprice, 99);
                        Assert.Equal(result.SalePrice, 99);


                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteLowRiseFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.MinPrices.AsNoTracking().FirstAsync();
                        var service = new MinPriceService(db);


                        await service.DeleteMinPriceAsync((Guid)model.ProjectID, model.ID);
                        bool afterDelete = db.MinPrices.Any(o => o.ID == model.ID && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
