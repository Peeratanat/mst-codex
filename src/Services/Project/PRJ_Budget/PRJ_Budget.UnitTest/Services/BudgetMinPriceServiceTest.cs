using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PRJ_Budget.Params.Filters;
using PRJ_Budget.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using PagingExtensions;
using OfficeOpenXml;

namespace PRJ_Budget.UnitTests
{
    public class BudgetMinPriceServiceTest
    {
        public BudgetMinPriceServiceTest()
        {
            Environment.SetEnvironmentVariable("minio_AccessKey", "XNTYE7HIMF6KK4BVEIXA");
            Environment.SetEnvironmentVariable("minio_DefaultBucket", "master-data");
            Environment.SetEnvironmentVariable("minio_PublicURL", "192.168.2.29:30050");
            Environment.SetEnvironmentVariable("minio_Endpoint", "192.168.2.29:9001");
            Environment.SetEnvironmentVariable("minio_SecretKey", "naD+esQ+uV7+xwfF3bPfAn5iC7C1XUyXeM8HkBlO");
            Environment.SetEnvironmentVariable("minio_TempBucket", "temp");
            Environment.SetEnvironmentVariable("minio_WithSSL", "false");
        }

        [Fact]
        public async Task GetBudgetMinPriceListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var unit = await db.Units.FirstAsync();
                var service = new BudgetMinPriceService(db);
                PageParam pageParam = new()
                {
                    Page = 1,
                    PageSize = 1
                };
                BudgetMinPriceSortByParam param = new()
                {
                    SortBy = BudgetMinPriceSortBy.UnitNo,
                    Ascending = false
                };

                var filter = new BudgetMinPriceFilter
                {
                    ProjectID = unit.ProjectID
                };

                var result = await service.GetBudgetMinPriceListAsync(filter, pageParam, param);
                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task SaveBudgetMinPriceAsync()
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
                        var project = await db.Projects.FirstAsync(o => o.ProjectNo == "10179");
                        var service = new BudgetMinPriceService(db);
                        var input = new BudgetMinPriceDTO
                        {
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            Quarter = 2,
                            Year = 2018,
                            QuarterlyTotalAmount = 6,
                            TransferTotalAmount = 7,
                            TransferTotalUnit = 7
                        };
                        var filter = new BudgetMinPriceFilter
                        {
                            ProjectID = project.ID,
                            Quarter = 2,
                            Year = 2018
                        };
                        var result = await service.SaveBudgetMinPriceAsync(filter, input);

                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task SaveBudgetMinPriceUnitListAsync()
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
                        var project = await db.Projects.Where(o => o.ProjectNo == "10179").FirstAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID && o.UnitNo == "C02-3").FirstAsync();
                        var service = new BudgetMinPriceService(db);

                        var inputs = new BudgetMinPriceListDTO
                        {
                            BudgetMinPriceDTO = new BudgetMinPriceDTO(),
                            BudgetMinPriceUnitDTO = new List<BudgetMinPriceUnitDTO>()
                        };
                        var userID = Guid.Parse("49AFD472-07FD-4856-BFB8-2FB0756A328B");
                        //var result = service.SaveBudgetMinPriceUnitListAsync(inputs, userID);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
 

        [Fact]
        public async Task ExportTransferBudgetAsync()
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
                        var service = new BudgetMinPriceService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10139").FirstAsync();
                        var filter = new BudgetMinPriceFilter
                        {
                            ProjectID = project.ID,
                            Quarter = 1,
                            Year = 2019
                        };
                        var result = await service.ExportTransferBudgetAsync(filter);
                        Assert.NotNull(result.Url);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task ExportQuarterlyBudgetAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BudgetMinPriceService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10059").FirstAsync();
                        var filter = new BudgetMinPriceFilter
                        {
                            ProjectID = project.ID,
                            Quarter = 2,
                            Year = 2018
                        };
                        var result = await service.ExportQuarterlyBudgetAsync(filter);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
