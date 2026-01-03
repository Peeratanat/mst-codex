using AutoFixture;
using Base.DTOs.SAL;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using PRJ_UnitInfos.Params.Filters;
using PRJ_UnitInfos.Services;
using PRJ_UnitInfos.Services;
using System;
using System.Linq;
using Xunit;

namespace PRJ_UnitInfos.UnitTests
{
    public class UnitInfoServiceTest : BaseServiceTest
    {

        [Fact]
        public async Task GetUnitInfoListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                var dbQuery = factory.CreateDbQueryContext();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        // Act
                        var service = new UnitInfoService(db);
                        UnitInfoListFilter filter = new UnitInfoListFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        UnitInfoListSortByParam sortByParam = new UnitInfoListSortByParam();

                        filter.ProjectID = (await db.Projects.FirstOrDefaultAsync()).ID;
                        //filter.UnitNo = "A05A15";

                        var results = await service.GetUnitInfoListAsync(filter, pageParam, sortByParam);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetUnitInfoAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                var dbQuery = factory.CreateDbQueryContext();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitInfoService(db);

                        var UnitID = (await db.Units.FirstOrDefaultAsync()).ID;

                        var result = await service.GetUnitInfoAsync(UnitID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetUnitInfoForPaymentAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                var dbQuery = factory.CreateDbQueryContext();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitInfoService(db);

                        var UnitID = (await db.Units.FirstOrDefaultAsync()).ID;

                        var result = await service.GetUnitInfoForPaymentAsync(UnitID);
                        Assert.NotNull(result);

                        var xxx = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        Assert.NotNull(xxx);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetUnitInfoSalePromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                var dbQuery = factory.CreateDbQueryContext();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitInfoService(db);

                        var booking = await db.Bookings.Where(o => !string.IsNullOrEmpty(o.BookingNo)).FirstAsync();
                        var result = await service.GetUnitInfoSalePromotionAsync(booking.UnitID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetUnitInfoPromotionExpensesAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                var dbQuery = factory.CreateDbQueryContext();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitInfoService(db);

                        var booking = await db.Bookings.Where(o => !string.IsNullOrEmpty(o.BookingNo)).FirstAsync();
                        var result = await service.GetUnitInfoPromotionExpensesAsync(booking.UnitID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetUnitInfoPreSalePromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                var dbQuery = factory.CreateDbQueryContext();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitInfoService(db);

                        var PreSalePromotions = await db.PreSalePromotions.FirstOrDefaultAsync();
                        var booking = await db.Bookings.FirstOrDefaultAsync(o => PreSalePromotions.BookingID == o.ID);
                        var result = await service.GetUnitInfoPreSalePromotionAsync(booking.UnitID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetPriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                var dbQuery = factory.CreateDbQueryContext();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitInfoService(db);

                        var UnitID = (await db.Units.FirstOrDefaultAsync()).ID;

                        var result = await service.GetPriceListAsync(UnitID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetUnitInfoCountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                var dbQuery = factory.CreateDbQueryContext();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitInfoService(db);

                        var ProjectID = Guid.NewGuid();

                        var result = await service.GetUnitInfoCountAsync(ProjectID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
