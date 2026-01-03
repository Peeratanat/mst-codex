using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using MST_Gpsim.Params.Filters;
using MST_Gpsim.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using OfficeOpenXml;
using Base.DTOs.PRJ;

namespace MST_Gpsim.UnitTests
{
    public class GPSimulateTest
    {
        public GPSimulateTest()
        {

            Environment.SetEnvironmentVariable("minio_AccessKey", "XNTYE7HIMF6KK4BVEIXA");
            Environment.SetEnvironmentVariable("minio_DefaultBucket", "master-data");
            Environment.SetEnvironmentVariable("minio_PublicURL", "192.168.2.29:30050");
            Environment.SetEnvironmentVariable("minio_Endpoint", "192.168.2.29:9001");
            Environment.SetEnvironmentVariable("minio_SecretKey", "naD+esQ+uV7+xwfF3bPfAn5iC7C1XUyXeM8HkBlO");
            Environment.SetEnvironmentVariable("minio_TempBucket", "temp");
            Environment.SetEnvironmentVariable("minio_WithSSL", "false");

            Environment.SetEnvironmentVariable("report_SecretKey", "nIcHeoYiMNZiJMYz");
            Environment.SetEnvironmentVariable("report_Url", "http://192.168.4.160/rptcrmrevo/printform.aspx");
        }
        [Fact]
        public async Task GetGPSimulateListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new GPSimulateService(db);
                        GPSimulateFilter filter = FixtureFactory.Get().Build<GPSimulateFilter>().Create();
                        PageParam pageParam = new PageParam();
                        GPSimulateSortByParam sortByParam = new GPSimulateSortByParam();

                        var results = await service.GetGPSimulateListAsync(filter, pageParam, sortByParam, null);

                        filter = new GPSimulateFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(GPSimulateSortBy)).Cast<GPSimulateSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new GPSimulateSortByParam() { SortBy = item };
                            results = await service.GetGPSimulateListAsync(filter, pageParam, sortByParam, null);
                            Assert.NotNull(results.GPSumulateDTOs);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetGPOriginalAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new GPSimulateService(db);
                        var model = await db.Projects.FirstOrDefaultAsync();
                        var results = await service.GetGPOriginalAsync(model.ID);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }



        [Fact]
        public async Task SaveDraftGPSimulateAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var project = await db.Projects.FirstOrDefaultAsync();
                DateTime now = DateTime.Now;

                GPProjectDTO GPProjectDTO = FixtureFactory.Get().Build<GPProjectDTO>().Create();
                GPSimulateDTO input = new GPSimulateDTO()
                {
                    Id = null,
                    VersionCode = "000",
                    GPProjectDTO = GPProjectDTO,
                    Remark = "unittest001",
                    Project = ProjectDTO.CreateFromModel(project),
                    Updated = now,
                    UpdatedBy = "unittest001",
                    Created = now,
                    Status = 2,
                };
                var service = new GPSimulateService(db);
                var result = await service.SaveDraftGPSimulateAsync(input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task GetGPVersionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new GPSimulateService(db);
                        var LastGPVersion = await db.GPVersions.Where(o => o.GPVersionType.Equals("GPSimulate")).OrderByDescending(o => o.Updated).FirstOrDefaultAsync();
                        var results = await service.GetGPVersionAsync(LastGPVersion.ID);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task DeleteGPSimulateAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var model = await db.GPVersions.FirstOrDefaultAsync();

                var service = new GPSimulateService(db);
                await service.DeleteGPSimulateAsync(model.ID);
                bool afterDelete = db.GPVersions.Any(o => o.ID == model.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task ExportTemplatePriceAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new GPSimulateService(db);

                        var user = await db.Users.FirstOrDefaultAsync();
                        var project = await db.GPVersions.FirstOrDefaultAsync();

                        var result = await service.ExportTemplatePriceAsync((Guid)project.ProjectID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task PrintChangeBudget()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new GPSimulateService(db);

                        var model = await db.GPVersions.FirstOrDefaultAsync();
                        var result = await service.PrintChangeBudget(model.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task PrintChangePrice()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new GPSimulateService(db);

                        var model = await db.GPVersions.FirstOrDefaultAsync();
                        var result = await service.PrintChangePrice(model.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
