using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Base.DTOs;
using OfficeOpenXml;

namespace PRJ_Unit.UnitTests
{
    public class HighRiseFeeServiceTest
    {
        IConfiguration Configuration;
        public HighRiseFeeServiceTest()
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



            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();


        [Fact]
        public async Task GetHighRiseFeeListAsync()
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

                        var service = new HighRiseFeeService(Configuration, db);

                        HighRiseFeeFilter filter = FixtureFactory.Get().Build<HighRiseFeeFilter>().Create();
                        filter.CalculateParkAreaKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CalculateParkArea").Select(o => o.Key).FirstAsync();

                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        PageParam pageParam = new PageParam()
                        {
                            Page = 1,
                            PageSize = 10
                        };
                        HighRiseFeeSortByParam sortByParam = new HighRiseFeeSortByParam()
                        {
                            SortBy = HighRiseFeeSortBy.Tower
                        };
                        var results = await service.GetHighRiseFeeListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new HighRiseFeeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(HighRiseFeeSortBy)).Cast<HighRiseFeeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new HighRiseFeeSortByParam() { SortBy = item };
                            results = await service.GetHighRiseFeeListAsync(project.ID, filter, pageParam, sortByParam);
                        }
                        Assert.NotEmpty(results.HighRiseFees);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetHighRiseFeeAsync()
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

                        var service = new HighRiseFeeService(Configuration, db);
                        var model = await db.HighRiseFees.Where(o => !o.IsDeleted).FirstAsync();

                        var result = await service.GetHighRiseFeeAsync(model.ProjectID, model.ID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateHighRiseFeeAsync()
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

                        var service = new HighRiseFeeService(Configuration, db);

                        var project = await db.Projects.Where(o => o.ProjectNo == "10103").FirstAsync();
                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();
                        var tower = await db.Towers.Where(o => o.ProjectID == project.ID).FirstAsync();
                        var floor = await db.Floors.Where(o => o.TowerID == tower.ID).FirstAsync();
                        var input = new HighRiseFeeDTO
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(unit),
                            Tower = TowerDropdownDTO.CreateFromModel(tower),
                            Floor = FloorDropdownDTO.CreateFromModel(floor),
                            EstimatePriceAirArea = 50
                        };

                        var result = await service.CreateHighRiseFeeAsync(project.ID, input);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateHighRiseFeeAsync()
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

                        var service = new HighRiseFeeService(Configuration, db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10057").FirstAsync();
                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID == project.ID && o.UnitNo == "A04-B01").FirstAsync();

                        var input = new HighRiseFeeDTO
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(unit),
                            EstimatePriceAirArea = 50
                        };

                        var resultCreate = await service.CreateHighRiseFeeAsync(project.ID, input);

                        resultCreate.EstimatePriceAirArea = 60;

                        var result = await service.UpdateHighRiseFeeAsync(project.ID, resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);
                        Assert.Equal(result.EstimatePriceAirArea, 60);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteHighRiseFeeAsync()
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
                        var service = new HighRiseFeeService(Configuration, db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10057").FirstAsync();
                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID == project.ID && o.UnitNo == "A04-B01").FirstAsync();

                        var input = new HighRiseFeeDTO
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(unit),
                            EstimatePriceAirArea = 50
                        };

                        var resultCreate = await service.CreateHighRiseFeeAsync(project.ID, input);

                        await service.DeleteHighRiseFeeAsync(project.ID, resultCreate.Id.Value);
                        bool afterDelete = db.HighRiseFees.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task ExportHighRiseFeeAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using var tran = await db.Database.BeginTransactionAsync();
                    var model = await db.Projects.FirstAsync();
                    var service = new HighRiseFeeService(Configuration, db);
                    var filter = new HighRiseFeeFilter();
                    HighRiseFeeSortByParam sortByParam = new HighRiseFeeSortByParam();

                    var result = await service.ExportHighRiseFeeAsync(model.ID, filter, sortByParam);
                    Assert.NotNull(result.Url);
                    await tran.RollbackAsync();
                });
            }
        }


    }
}
