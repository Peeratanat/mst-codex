using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.UnitTests
{
    public class ModelServiceTest
    {
        [Fact]
        public async Task GetModelDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Projects.AsNoTracking().FirstAsync();
                        var service = new ModelService(db);

                        var result = await service.GetModelDropdownListAsync(model.ID, null);
                        Assert.NotEmpty(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
 
    }
}
