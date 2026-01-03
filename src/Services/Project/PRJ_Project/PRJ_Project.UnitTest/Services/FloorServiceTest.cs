using AutoFixture;
using Base.DTOs;
using Base.DTOs.PRJ;
using CustomAutoFixture;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Inputs;
using PRJ_Project.Services;

namespace PRJ_Project.UnitTests
{
    public class FloorServiceTest
    {
        IConfiguration Configuration;
        public FloorServiceTest()
        {
            
            Environment.SetEnvironmentVariable("minio_AccessKey", "XNTYE7HIMF6KK4BVEIXA");
            Environment.SetEnvironmentVariable("minio_DefaultBucket", "master-data");
            Environment.SetEnvironmentVariable("minio_PublicURL", "192.168.2.29:30050");
            Environment.SetEnvironmentVariable("minio_Endpoint", "192.168.2.29:9001");
            Environment.SetEnvironmentVariable("minio_SecretKey", "naD+esQ+uV7+xwfF3bPfAn5iC7C1XUyXeM8HkBlO");
            Environment.SetEnvironmentVariable("minio_TempBucket", "temp");
            Environment.SetEnvironmentVariable("minio_WithSSL", "false");
            
            Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async Task GetFloorDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var service = new FloorService(db);
                var model = await db.Floors.FirstAsync();
                var result = await service.GetFloorDropdownListAsync(model.ProjectID, null, null);
                Assert.NotEmpty(result);
            });
        }
        [Fact]
        public async Task GetFloorEventBookingDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var service = new FloorService(db);
                var model = await db.Floors.FirstAsync();
                var result = await service.GetFloorEventBookingDropdownListAsync(model.ProjectID, null, null);
                Assert.NotEmpty(result);
            });
        }


        [Fact]
        public async Task GetFloorListAsync()
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

                        var service = new FloorService(Configuration, db);
                        FloorsFilter filter = FixtureFactory.Get().Build<FloorsFilter>().Create();
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10016").FirstAsync();
                        var tower = await db.Towers.Where(o => !o.IsDeleted && o.TowerCode == "01").FirstAsync();
                        PageParam pageParam = new PageParam();
                        FloorSortByParam sortByParam = new FloorSortByParam();
                        var results = await service.GetFloorListAsync(project.ID, tower.ID, filter, pageParam, sortByParam);

                        filter = new FloorsFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(FloorSortBy)).Cast<FloorSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new FloorSortByParam() { SortBy = item };
                            results = await service.GetFloorListAsync(project.ID, tower.ID, filter, pageParam, sortByParam);
                        }

                        Assert.NotEmpty(results.Floors);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetFloorAsync()
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

                        var service = new FloorService(Configuration, db);
                        var model = await db.Floors.Where(o => !o.IsDeleted && o.ProjectID != null && o.TowerID != null).FirstAsync();

                        var result = await service.GetFloorAsync(model.ProjectID, model.TowerID, model.ID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateFloorAsync()
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

                        var service = new FloorService(Configuration, db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10016").FirstAsync();
                        var tower = await db.Towers.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();
                        var input = FixtureFactory.Get().Build<FloorDTO>().Create();
                        input.NameTH = "70";
                        input.NameEN = "70";

                        var result = await service.CreateFloorAsync(project.ID, tower.ID, input);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMultipleFloorAsync()
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

                        var service = new FloorService(Configuration, db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10016").FirstAsync();
                        var tower = await db.Towers.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();
                        var input = FixtureFactory.Get().Build<CreateMultipleFloorInput>().Create();
                        input.From = 5;
                        input.To = 10;

                        var result = await service.CreateMultipleFloorAsync(project.ID, tower.ID, input);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateFloorAsync()
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

                        var service = new FloorService(Configuration, db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10016").FirstAsync();
                        var tower = await db.Towers.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();
                        var input = FixtureFactory.Get().Build<FloorDTO>().Create();
                        input.NameTH = "70";
                        input.NameEN = "70";
                        var resultCreate = await service.CreateFloorAsync(project.ID, tower.ID, input);
                        resultCreate.NameTH = "เทส";
                        resultCreate.NameEN = "Test";
                        var result = await service.UpdateFloorAsync(project.ID, tower.ID, resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);
                        Assert.Equal("เทส", resultCreate.NameTH);
                        Assert.Equal("Test", resultCreate.NameEN);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteFloorAsync()
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

                        var service = new FloorService(Configuration, db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10016").FirstAsync();
                        var tower = await db.Towers.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();
                        var input = FixtureFactory.Get().Build<FloorDTO>().Create();
                        input.NameTH = "70";
                        input.NameEN = "70";

                        var resultCreate = await service.CreateFloorAsync(project.ID, tower.ID, input);

                        bool beforeDelete = db.Floors.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.True(beforeDelete);
                        var result = await service.DeleteFloorAsync(project.ID, tower.ID, resultCreate.Id.Value);
                        bool afterDelete = db.Floors.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);



                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task ExportExcelFloorAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var model = await db.Floors.FirstAsync();
                    var service = new FloorService(Configuration, db);
                    FileDTO result = await service.ExportExcelFloorAsync(model.ProjectID, model.TowerID, new(), new());
                    Assert.NotNull(result.Url);
                });
            }

        }
    }
}
