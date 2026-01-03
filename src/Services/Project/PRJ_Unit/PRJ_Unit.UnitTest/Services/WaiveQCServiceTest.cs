using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
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
using models = Database.Models;
using OfficeOpenXml;

namespace PRJ_Unit.UnitTests
{
    public class WaiveQCServiceTest
    {
        IConfiguration Configuration;
        public WaiveQCServiceTest()
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



        [Fact]
        public async Task GetWaiveQCListAsync()
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
                          var service = new WaiveQCService(db);
                        var project = await db.Projects.AsNoTracking().FirstAsync(f => f.ProjectNo == "60019");

                        PageParam pageParam = new PageParam()
                        {
                            Page = 1,
                            PageSize = 10
                        };
                        WaiveQCSortByParam sortByParam = new WaiveQCSortByParam
                        {
                            SortBy = WaiveQCSortBy.Unit,
                        };
                        var results = await service.GetWaiveQCListAsync(project.ID, new WaiveQCFilter(), pageParam, sortByParam);
                        Assert.NotEmpty(results.WaiveQC);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetWaiveQCAsync()
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

                        var service = new WaiveQCService(Configuration, db);

                        var model = await db.WaiveQCs.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var result = await service.GetWaiveQCAsync(model.ProjectID, model.ID);

                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateWaiveQCAsyncs()
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

                        var service = new WaiveQCService(Configuration, db);

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new WaiveQCDTO
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(unit),
                            WaiveQCDate = DateTime.Now
                        };

                        var result = await service.CreateWaiveQCAsync(unit.ProjectID.Value, input);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateWaiveQCAsync()
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

                        var service = new WaiveQCService(Configuration, db);

                        var unit = await db.Units.FirstAsync(o => !o.IsDeleted && o.ProjectID != null
                            && db.MasterCenters.Any(x => x.ID == o.UnitStatusMasterCenterID && x.Key == "1" && x.IsActive == true && x.MasterCenterGroupKey == "UnitStatus"));

                        var input = new WaiveQCDTO
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(unit),
                            WaiveQCDate = DateTime.Now
                        };

                        var resultCreate = await service.CreateWaiveQCAsync(unit.ProjectID.Value, input);
                        resultCreate.WaiveQCDate = new DateTime(2019, 5, 20);

                        var result = await service.UpdateWaiveQCAsync(unit.ProjectID.Value, resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);
                        Assert.Equal(result.WaiveQCDate, new DateTime(2019, 5, 20));

                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task DeleteWaiveQCAsync()
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

                        var service = new WaiveQCService(Configuration, db);

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new WaiveQCDTO
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(unit),
                            WaiveQCDate = DateTime.Now
                        };

                        var resultCreate = await service.CreateWaiveQCAsync(unit.ProjectID.Value, input);

                        await service.DeleteWaiveQCAsync(unit.ProjectID.Value, resultCreate.Id.Value);
                        bool afterDelete = db.WaiveQCs.Any(o => o.ID == unit.ProjectID.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);


                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task ExportExcelWaiveQCAsync()
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
                    var service = new WaiveQCService(Configuration, db);
                    var filter = new WaiveQCFilter();
                    WaiveQCSortByParam sortByParam = new WaiveQCSortByParam();

                    var result = await service.ExportExcelWaiveQCAsync(model.ID, filter, sortByParam);
                    Assert.NotNull(result.Url);
                    await tran.RollbackAsync();
                });
            }
        }


    }

}
