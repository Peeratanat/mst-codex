using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Services;

namespace PRJ_Project.UnitTests
{
    public class RoundFeeServiceTest
    {

        [Fact]
        public async Task GetRoundFeeListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var project = await db.Projects.FirstAsync();
                    var service = new RoundFeeService(db);
                    RoundFeeFilter filter = new();
                    PageParam pageParam = new()
                    {
                        Page = 1,
                        PageSize = 1
                    };
                    RoundFeeSortByParam param = new()
                    {
                        SortBy = RoundFeeSortBy.UpdatedBy,
                        Ascending = false
                    };

                    var result = await service.GetRoundFeeListAsync(project.ID, filter, pageParam, param);
                    Assert.NotNull(result);
                });
            }
        }



        [Fact]
        public async Task GetRoundFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var roundFees = await db.RoundFees.FirstAsync();
                    var service = new RoundFeeService(db);

                    var result = await service.GetRoundFeeAsync(roundFees.ProjectID, roundFees.ID);
                    Assert.NotNull(result);
                });
            }
        }
        [Fact]
        public async Task CreateRoundFeeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var project = await db.Projects.FirstOrDefaultAsync();
                var landOffice = await db.LandOffices.FirstOrDefaultAsync();
                RoundFeeDTO input = new()
                {
                    LandOffice = LandOfficeListDTO.CreateFromModel(landOffice),
                };

                var service = new RoundFeeService(db);
                var result = await service.CreateRoundFeeAsync(project.ID, input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task UpdateRoundFeeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new RoundFeeService(db);
                var model = await db.RoundFees.FirstOrDefaultAsync();
                var landOffice = await db.LandOffices.FirstOrDefaultAsync();
                var modelEdit = await service.GetRoundFeeAsync(model.ProjectID, model.ID);

                modelEdit.LandOffice.Id = landOffice.ID;

                var resultEdit = await service.UpdateRoundFeeAsync(model.ProjectID, model.ID, modelEdit);
                Assert.NotNull(resultEdit);
                Assert.Equal(landOffice.ID, resultEdit.LandOffice.Id);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task DeleteRoundFeeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var model = await db.RoundFees.FirstOrDefaultAsync();

                var service = new RoundFeeService(db);
                await service.DeleteRoundFeeAsync(model.ProjectID, model.ID);
                bool afterDelete = db.SpecMaterialItems.Any(o => o.ID == model.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }

    }
}

