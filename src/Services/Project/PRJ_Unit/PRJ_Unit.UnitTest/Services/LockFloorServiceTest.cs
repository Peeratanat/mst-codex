using Base.DTOs.PRJ;
using Database.Models.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.UnitTests
{
    public class LockFloorServiceTest
    {
        [Fact]
        public async Task GetLockFloorAsync()
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
                        var service = new LockFloorService(db);

                        PageParam pageParam = new PageParam()
                        {
                            Page = 1,
                            PageSize = 1
                        };
                        UnitControlLockByParam sortByParam = new UnitControlLockByParam
                        {
                            SortBy = UnitControlLockSortBy.FloorNameTH
                        };
                        var result = await service.GetLockFloorAsync(model.ID, new UnitControlLockFilter(), pageParam, sortByParam);
                        Assert.NotEmpty(result.unitControlLocks);
                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task UpdateLockFloorAsync()
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
                        var service = new LockFloorService(db);
                        var modelEdit = await (from floor in db.Floors.Where(x => x.ProjectID == model.ID)
                                                               .Include(x => x.Tower)

                                               join unitControlLock in db.UnitControlLocks on floor.ID equals unitControlLock.FloorID
                                                into unitControlLocks
                                               from unitControlLockModel in unitControlLocks.DefaultIfEmpty()

                                               select new UnitControlLockQueryResult
                                               {
                                                   Tower = floor.Tower,
                                                   Floor = floor,
                                                   ProjectID = floor.ProjectID,
                                                   FloorID = unitControlLockModel != null ? unitControlLockModel.FloorID : null,
                                                   ExpiredDate = unitControlLockModel != null ? unitControlLockModel.ExpiredDate : null,
                                                   EffectiveDate = unitControlLockModel != null ? unitControlLockModel.EffectiveDate : null,
                                                   Remark = unitControlLockModel != null ? unitControlLockModel.Remark : null,
                                                   UnitControlLockID = unitControlLockModel != null ? unitControlLockModel.ID : Guid.Empty,
                                                   Unit = new Unit(),
                                                   StatusLock = "Lock Floor"
                                               }).FirstOrDefaultAsync();


                        var dateNow = DateTime.Now;
                        modelEdit.EffectiveDate = dateNow;
                        modelEdit.ExpiredDate = dateNow;
                        modelEdit.Remark = "UnitTest";
                        var editModel = UnitControlLockDTO.CreateFromQueryResult(modelEdit);

                        var result = await service.UpdateLockFloorAsync(editModel);

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
        public async Task DeleteLockFloorAsync()
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
                        var service = new LockFloorService(db);

                        await service.DeleteLockFloorAsync(model.ID);
                        bool afterDelete = db.UnitControlLocks.Any(o => o.ID == model.ID && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
