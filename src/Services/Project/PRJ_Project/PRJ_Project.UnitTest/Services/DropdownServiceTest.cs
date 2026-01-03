using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PRJ_Project.Services;

namespace PRJ_Project.UnitTests
{
    public class DropdownServiceTest
    {
        [Fact]
        public async Task GetProjectWalkReferDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var service = new DropdownService(db);
                var result = await service.GetProjectWalkReferDropdownListAsync(null, null, true, null);
                Assert.NotEmpty(result);
            });
        }
        [Fact]
        public async Task GetProjectTitledeedRequestDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var service = new DropdownService(db);
                var result = await service.GetProjectTitledeedRequestDropdownListAsync(null, null, true, null, null, null);
                Assert.NotEmpty(result);
            });
        }

        [Fact]
        public async Task GetProjectByProductTypeDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var service = new DropdownService(db);
                var result = await service.GetProjectByProductTypeDropdownListAsync(null, null, true, null, null);
                Assert.NotEmpty(result);
            });
        }

        [Fact]
        public async Task GetProjectAllStatusDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var service = new DropdownService(db);
                var result = await service.GetProjectAllStatusDropdownListAsync(null, null);
                Assert.NotEmpty(result);
            });
        }

        [Fact]
        public async Task GetProjectAllIsActiveDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var service = new DropdownService(db);
                var result = await service.GetProjectAllIsActiveDropdownListAsync(null, null);
                Assert.NotEmpty(result);
            });
        }
        [Fact]
        public async Task GetProjectDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var service = new DropdownService(db);
                var result = await service.GetProjectDropdownListAsync(null, null, true, false, null, null);
                Assert.NotEmpty(result);
            });
        }
    }
}
