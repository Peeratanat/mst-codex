using AutoFixture;
using Base.DTOs.PRJ;
using CustomAutoFixture;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Services;

namespace PRJ_Project.UnitTests
{
    public class TowerServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        [Fact]
        public async Task GetTowerDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var project = await db.Projects.FirstAsync();
                    var service = new TowerService(db);

                    var result = await service.GetTowerDropdownListAsync(project.ID, null);
                    Assert.NotNull(result);
                });
            }
        }

        [Fact]
        public async Task GetTowerListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        //Put unit test here

                        var service = new TowerService(db);
                        TowerFilter filter = FixtureFactory.Get().Build<TowerFilter>().Create();
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        PageParam pageParam = new PageParam();
                        TowerSortByParam sortByParam = new TowerSortByParam();
                        var results = await service.GetTowerListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new TowerFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(TowerSortBy)).Cast<TowerSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new TowerSortByParam() { SortBy = item };
                            results = await service.GetTowerListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        Assert.NotNull(results.Towers);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetTowerAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var tower = await db.Towers.FirstAsync();
                    var service = new TowerService(db);

                    var result = await service.GetTowerAsync((Guid)tower.ProjectID, tower.ID);
                    Assert.NotNull(result);
                });
            }
        }
        [Fact]
        public async Task CreateTowerAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var project = await db.Projects.FirstOrDefaultAsync();
                TowerDTO input = new()
                {
                    Code = "UnitTest",
                    NoTH = "UnitTest",
                    NoEN = "UnitTest",
                    CondominiumNo = "123456",
                };

                var service = new TowerService(db);
                var result = await service.CreateTowerAsync(project.ID, input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task UpdateTowerAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var model = await db.Towers.FirstOrDefaultAsync();
                var service = new TowerService(db);
                model.TowerCode = "123";
                var tower = await TowerDTO.CreateFromModelAsync(model, db);
                var resultEdit = await service.UpdateTowerAsync((Guid)model.ProjectID, model.ID, tower);
                Assert.NotNull(resultEdit);
                Assert.Equal("123", resultEdit.Code);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task DeleteTowerAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var tower = await db.Towers.FirstOrDefaultAsync(t => !db.Units.Any(u => u.TowerID == t.ID));
                var service = new TowerService(db);
                await service.DeleteTowerAsync((Guid)tower.ProjectID, tower.ID);
                bool afterDelete = db.SpecMaterialItems.Any(o => o.ID == tower.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
    }
}
