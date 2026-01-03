using System;
using System.Linq;
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
using Xunit;
using models = Database.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace PRJ_Unit.UnitTests
{
    public class PriceServiceTest
    {
        IConfiguration Configuration;
        public PriceServiceTest()
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
        public async Task GetPriceListsAsync()
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

                        var service = new PriceListService(Configuration, db);

                        var project = await db.Units.FirstAsync();
                        PageParam pageParam = new PageParam();
                        PriceListSortByParam sortByParam = new PriceListSortByParam()
                        {
                            SortBy = PriceListSortBy.UnitNo
                        };
                        var results = await service.GetPriceListsAsync((Guid)project.ProjectID, new PriceListFilter(), pageParam, sortByParam);
                        Assert.NotEmpty(results.PriceLists);

                        await tran.RollbackAsync();

                    }
                });
            }
        }
        [Fact]
        public async Task GetPriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new PriceListService(Configuration, db);

                        // var result = await service.GetPriceListAsync(Guid.Parse("{23c5645b-a737-4dfc-91f7-f1479a3c22c0}"), Guid.Parse("{4d119453-e91c-4e0b-9f6f-0585780e5b5b}"));
                        // Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task CreatePriceListAsync()
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

                        var service = new PriceListService(Configuration, db);

                        var project = await db.Projects.Where(o => o.ProjectNo == "40039").FirstOrDefaultAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID).FirstOrDefaultAsync();

                        var input = new PriceListDTO()
                        {
                            BookingAmount = 60000,
                            ContractAmount = 60000,
                            TotalSalePrice = 1200000,
                            UnitNo = unit.UnitNo,
                            DownAmount = 200000,
                            DownPaymentPeriod = 20,
                            DownPaymentPerPeriod = 10000,
                            SpecialDown = "2,10",
                            SpecialDownPrice = "20000,20000",
                            PercentDownPayment = 20,
                        };

                        var result = await service.CreatePriceListAsync(project.ID, input);
                        Assert.NotNull(result);


                        await tran.RollbackAsync();

                    }
                });
            }
        }

        [Fact]
        public async Task UpdatePriceListAsync()
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

                        var service = new PriceListService(Configuration, db);

                      var project = await db.Projects.Where(o => o.ProjectNo == "40039").FirstOrDefaultAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID).FirstOrDefaultAsync();

                        var input = new PriceListDTO()
                        {
                            BookingAmount = 60000,
                            ContractAmount = 60000,
                            TotalSalePrice = 1200000,
                            UnitNo = unit.UnitNo,
                            DownAmount = 200000,
                            DownPaymentPeriod = 20,
                            DownPaymentPerPeriod = 10000,
                            SpecialDown = "2,10",
                            SpecialDownPrice = "20000,20000",
                            PercentDownPayment = 20,
                        };

                        var resultCreate = await service.CreatePriceListAsync(project.ID, input);
                        resultCreate.SpecialDown = string.Empty;
                        resultCreate.SpecialDownPrice = string.Empty;

                        var resultupdate = await service.UpdatePriceListAsync(project.ID, resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(resultupdate);
                        Assert.Null(resultupdate.SpecialDown);
                        Assert.Null(resultupdate.SpecialDownPrice);


                        await tran.RollbackAsync();

                    }
                });
            }
        }

        [Fact]
        public async Task ExportExcelPriceListAsync()
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
                        PriceListService service = new PriceListService(Configuration, db);
                        //PriceListFilter filter = new PriceListFilter();
                        //PriceListSortByParam sortByParam = new PriceListSortByParam();
                        var project = await db.Projects.Where(o => o.ProjectNo == "40039").FirstOrDefaultAsync();
                        var result = await service.ExportExcelPriceListAsync(project.ID);
                        Assert.NotNull(result.Url);
                        await tran.RollbackAsync();
                    }
                });
            }
        }


    }
}
