using Base.DTOs.PRJ;
using Database.Models.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.UnitTests
{
    public class LowRiseFeeServiceTest
    {
        [Fact]
        public async Task GetLowRiseFeeListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.LowRiseFees.AsNoTracking().FirstAsync();
                        var service = new LowRiseFeeService(db);

                        PageParam pageParam = new PageParam()
                        {
                            Page = 1,
                            PageSize = 1
                        };
                        LowRiseFeeSortByParam sortByParam = new LowRiseFeeSortByParam
                        {
                            SortBy = LowRiseFeeSortBy.Unit,
                        };
                        var result = await service.GetLowRiseFeeListAsync(model.ProjectID, new LowRiseFeeFilter(), pageParam, sortByParam);
                        Assert.NotEmpty(result.LowRiseFees);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetLowRiseFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.LowRiseFees.AsNoTracking().FirstAsync();
                        var service = new LowRiseFeeService(db);
                        var result = await service.GetLowRiseFeeAsync(model.ProjectID, model.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task CreateLowRiseFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new LowRiseFeeService(db);

                        var model = await db.LowRiseFees.FirstAsync();

                        var input = LowRiseFeeDTO.CreateFromModel(model);

                        var result = await service.CreateLowRiseFeeAsync(model.ProjectID, input);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task UpdateLowRiseFeeAsync()
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
                        var service = new LowRiseFeeService(db);
                        var model = await db.LowRiseFees.FirstAsync();
                        var input = LowRiseFeeDTO.CreateFromModel(model);
                        input.EstimatePriceArea = 99;

                        var result = await service.UpdateLowRiseFeeAsync(model.ProjectID, (Guid)input.Id, input);
                        Assert.NotNull(result);
                        Assert.Equal(result.EstimatePriceArea, 99);


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
                        var model = await db.LowRiseFees.AsNoTracking().FirstAsync();
                        var service = new LowRiseFeeService(db);

                        await service.DeleteLowRiseFeeAsync((Guid)model.ProjectID, model.ID);
                        bool afterDelete = db.UnitControlLocks.Any(o => o.ID == model.ID && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
