using AutoFixture;
using Base.DTOs.PRJ;
using CustomAutoFixture;
using Database.Models;
using Database.Models.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.UnitTests
{
    public class UnitServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        public UnitServiceTest()
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

        public async Task GetUnitListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbq = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        //Put unit test here

                        var service = new UnitService(db, dbq);
                        UnitFilter filter = FixtureFactory.Get().Build<UnitFilter>().Create();
                        var unit = await db.Units
                                        .Include(o => o.Model)
                                        .Include(o => o.UnitStatus)
                                        .Include(o => o.Model.TypeOfRealEstate)
                                        .Include(o => o.Tower).FirstAsync();
                        PageParam pageParam = new PageParam();
                        UnitListSortByParam sortByParam = new UnitListSortByParam();
                        var results = await service.GetUnitListAsync((Guid)unit.ProjectID, filter, pageParam, sortByParam);

                        filter = new UnitFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(UnitListSortBy)).Cast<UnitListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new UnitListSortByParam() { SortBy = item };
                            results = await service.GetUnitListAsync((Guid)unit.ProjectID, filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.Units);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task GetUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbq = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Units.AsNoTracking().FirstAsync();
                        var service = new UnitService(db, dbq);

                        var result = await service.GetUnitAsync((Guid)model.ProjectID, model.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetUnitInfoAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbq = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Units.AsNoTracking().FirstAsync();
                        var service = new UnitService(db,dbq);

                        var result = await service.GetUnitInfoAsync((Guid)model.ProjectID, model.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbq = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitService(db, dbq);

                        var model = await db.Projects.FirstAsync();
                        UnitDTO input = new UnitDTO
                        {
                            UnitNo = "UnitTest",
                            HouseNo = "123",
                        };
                        var result = await service.CreateUnitAsync(model.ID, input);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task UpdateUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbq = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var dateNow = DateTime.Now;
                        var service = new UnitService(db, dbq);
                        var model = await db.Projects.FirstAsync();
                        UnitDTO input = new UnitDTO
                        {
                            UnitNo = "UnitTest",
                            HouseNo = "123",
                        };
                        var result = await service.CreateUnitAsync(model.ID, input);
                        result.UnitNo = "UnitTest";
                        var resultU = await service.UpdateUnitAsync(model.ID, (Guid)result.Id, result);
                        Assert.NotNull(resultU);
                        Assert.Equal("UnitTest", resultU.UnitNo);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbq = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitService(db, dbq);

                        var unit = await db.Units.FirstAsync(o => !o.IsDeleted && o.ProjectID != null
                          && db.MasterCenters.Any(x => x.ID == o.UnitStatusMasterCenterID && x.Key == "0" && x.IsActive == true && x.MasterCenterGroupKey == "UnitStatus"));


                        await service.DeleteUnitAsync((Guid)unit.ProjectID, unit.ID);
                        bool afterDelete = db.UnitControlInterests.Any(o => o.ID == unit.ID && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task ExportExcelUnitInitialAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbq = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitService(db, dbq);
                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstOrDefaultAsync();
                        var result = await service.ExportExcelUnitInitialAsync(project.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task ExportExcelUnitGeneralAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbq = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitService(db, dbq);
                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstOrDefaultAsync();
                        var result = await service.ExportExcelUnitGeneralAsync(project.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task ExportExcelUnitFenceAreaAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbq = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitService(db, dbq);
                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstOrDefaultAsync();
                        var result = await service.ExportExcelUnitFenceAreaAsync(project.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
