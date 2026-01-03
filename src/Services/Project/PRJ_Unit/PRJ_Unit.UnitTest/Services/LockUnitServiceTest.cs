using AutoFixture;
using Base.DTOs.PRJ;
using CustomAutoFixture;
using Database.Models.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.UnitTests
{
    public class LockUnitServiceTest
    {
        [Fact]
        public async Task GetLockUnitAsync()
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
                        var service = new LockUnitService(db);

                        UnitControlLockFilter filter = FixtureFactory.Get().Build<UnitControlLockFilter>().Create();
                        PageParam pageParam = new PageParam();
                        UnitControlLockByParam sortByParam = new UnitControlLockByParam();

                        var results = await service.GetLockUnitAsync(model.ID,filter, pageParam, sortByParam);

                        filter = new UnitControlLockFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(UnitControlLockSortBy)).Cast<UnitControlLockSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new UnitControlLockByParam() { SortBy = item };
                            results = await service.GetLockUnitAsync(model.ID,filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.unitControlLocks);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task UpdateLockUnitAsync()
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
                        var dateNow = DateTime.Now;
                        var service = new LockUnitService(db);
                        var modelEdit = await (from unit in db.Units.Where(x => x.ProjectID == model.ID)
                                                               .Include(x => x.Tower)
                                                               .Include(x => x.Floor)
                                                               .Include(x => x.UnitStatus)

                                               join unitControlLock in db.UnitControlLocks on unit.ID equals unitControlLock.UnitID
                                                into unitControlLocks
                                               from unitControlLockModel in unitControlLocks.DefaultIfEmpty()

                                               let floorLock = db.UnitControlLocks.Where(x => x.FloorID == unit.FloorID && x.EffectiveDate <= dateNow && (x.ExpiredDate == null || x.ExpiredDate >= dateNow)).FirstOrDefault()

                                               select new UnitControlLockQueryResult
                                               {
                                                   Tower = unit.Tower,
                                                   Floor = unit.Floor,
                                                   ProjectID = unit.ProjectID,
                                                   FloorID = unitControlLockModel != null ? unitControlLockModel.FloorID : null,
                                                   ExpiredDate = unitControlLockModel != null ? unitControlLockModel.ExpiredDate : null,
                                                   EffectiveDate = unitControlLockModel != null ? unitControlLockModel.EffectiveDate : null,
                                                   Remark = unitControlLockModel != null ? unitControlLockModel.Remark : null,
                                                   UnitControlLockID = unitControlLockModel != null ? unitControlLockModel.ID : Guid.Empty,
                                                   Unit = unit,
                                                   StatusLock = (unitControlLockModel != null && unitControlLockModel.EffectiveDate <= dateNow && (unitControlLockModel.ExpiredDate == null || unitControlLockModel.ExpiredDate >= dateNow) ? ("Lock Unit" + (floorLock != null ? "," : "")) : "") +
                                                   ((unit.Floor != null && floorLock != null) ? "Lock Floor" : ""),
                                               }).FirstOrDefaultAsync();


                        modelEdit.EffectiveDate = dateNow;
                        modelEdit.ExpiredDate = dateNow;
                        modelEdit.Remark = "UnitTest";
                        var editModel = UnitControlLockDTO.CreateFromQueryResult(modelEdit);

                        var result = await service.UpdateLockUnitAsync(editModel);

                        Assert.NotNull(result);
                        Assert.Equal(result.EffectiveDate, dateNow);
                        Assert.Equal(result.ExpiredDate, dateNow);
                        Assert.Equal(result.Remark, "UnitTest");


                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteLockUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.UnitControlLocks.AsNoTracking().FirstAsync();
                        var service = new LockUnitService(db);

                        await service.DeleteLockUnitAsync(model.ID);
                        bool afterDelete = db.UnitControlLocks.Any(o => o.ID == model.ID && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
