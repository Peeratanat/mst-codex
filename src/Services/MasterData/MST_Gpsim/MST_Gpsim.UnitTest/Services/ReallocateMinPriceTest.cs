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
    public class ReallocateMinPriceTest
    {
        public ReallocateMinPriceTest()
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
        public async Task GetReallocateMinPriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new ReallocateMinPriceService(db);
                        ReallocateMinPriceFilter filter = FixtureFactory.Get().Build<ReallocateMinPriceFilter>().Create();
                        PageParam pageParam = new PageParam();
                        ReallocateMinPriceSortByParam sortByParam = new ReallocateMinPriceSortByParam();

                        var results = await service.GetReallocateMinPriceListAsync(filter, pageParam, sortByParam, null);

                        filter = new ReallocateMinPriceFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(ReallocateMinPriceSortBy)).Cast<ReallocateMinPriceSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ReallocateMinPriceSortByParam() { SortBy = item };
                            results = await service.GetReallocateMinPriceListAsync(filter, pageParam, sortByParam, null);
                            Assert.NotNull(results.ReallocateMinPriceDTOs);
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
                        var service = new ReallocateMinPriceService(db);
                        var model = await db.Projects.FirstOrDefaultAsync();
                        var results = await service.GetGPOriginalAsync(model.ID);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }



        [Fact]
        public async Task SaveDraftReallocateMinPriceAsync()
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
                ReallocateMinPriceDTO input = new ReallocateMinPriceDTO()
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
                var service = new ReallocateMinPriceService(db);
                var result = await service.SaveDraftReallocateMinPriceAsync(input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task GetReallocateMinPriceAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new ReallocateMinPriceService(db);
                        var LastGPVersion = await db.GPVersions.Where(o => o.GPVersionType.Equals("Reallocate")).OrderByDescending(o => o.Updated).FirstOrDefaultAsync();
                        var results = await service.GetReallocateMinPriceAsync(LastGPVersion.ID);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task DeleteReallocateMinPriceAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var model = await db.GPVersions.FirstOrDefaultAsync();

                var service = new ReallocateMinPriceService(db);
                await service.DeleteReallocateMinPriceAsync(model.ID);
                bool afterDelete = db.GPVersions.Any(o => o.ID == model.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task ExportTemplateReallocateMinPrice()
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
                        var service = new ReallocateMinPriceService(db);

                        var user = await db.Users.FirstOrDefaultAsync();
                        var project = await db.GPVersions.FirstOrDefaultAsync();

                        var result = await service.ExportTemplateReallocateMinPrice((Guid)project.ProjectID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task PrintReallocateMinPrice()
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
                        var service = new ReallocateMinPriceService(db);

                        var model = await db.GPVersions.FirstOrDefaultAsync();
                        var result = service.PrintReallocateMinPrice(model.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
