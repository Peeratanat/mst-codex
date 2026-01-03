using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using models = Database.Models;
using Database.UnitTestExtensions;
using Microsoft.Extensions.Configuration;
using PRJ_Budget.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;
using PRJ_Budget.Params.Filters;
using PagingExtensions;
using Base.DTOs.PRJ;
using System.Linq;
using System.IO;
using Common.Helper;
using Database.Models.PRJ;

namespace PRJ_Budget.UnitTests
{
    public class BudgetPromotionServiceTest
    {
        public BudgetPromotionServiceTest()
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
        public async Task GetBudgetPromotionListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync();
                        var budgetPromotionSyncStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "BudgetPromotionSyncStatus").FirstAsync();
                        var service = new BudgetPromotionService(db);
                        BudgetPromotionFilter filter = FixtureFactory.Get().Build<BudgetPromotionFilter>().Create();
                        PageParam pageParam = new PageParam();
                        BudgetPromotionSortByParam sortByParam = new BudgetPromotionSortByParam();

                        var results = await service.GetBudgetPromotionListAsync(project.ID,filter, pageParam, sortByParam);

                        filter = new BudgetPromotionFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(BudgetPromotionSortBy)).Cast<BudgetPromotionSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new BudgetPromotionSortByParam() { SortBy = item };
                            results = await service.GetBudgetPromotionListAsync(project.ID,filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.BudgetPromotions);
                        } 
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateBudgetPromotionAsync()
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
                        var service = new BudgetPromotionService(db);
                        var project = await db.Projects.FirstOrDefaultAsync();
                        var unit = await db.Units.FirstOrDefaultAsync(f=>f.ProjectID ==project.ID);

                        var input = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.PromotionPrice, 50)
                                           .With(o => o.PromotionTransferPrice, 100)
                                           .Create();

                        var result = await service.CreateBudgetPromotionAsync(project.ID, input);
                        Assert.NotNull(result);
                        //await service.RunWaitingSyncJobAsync();
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetBudgetPromotionAsync()
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
                        var service = new BudgetPromotionService(db);
                       
                        var project = await db.Projects.FirstOrDefaultAsync();
                        var unit = await db.Units.FirstOrDefaultAsync(f=>f.ProjectID ==project.ID);

                        var result = await service.GetBudgetPromotionAsync(project.ID, unit.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateBudgetPromotionAsync()
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
                        var service = new BudgetPromotionService(db);
                      
                        var project = await db.Projects.FirstOrDefaultAsync();
                        var unit = await db.Units.FirstOrDefaultAsync(f=>f.ProjectID ==project.ID);

                        var input = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.PromotionPrice, 50)
                                           .With(o => o.PromotionTransferPrice, 100)
                                           .Create();

                        var resultCreate = await service.CreateBudgetPromotionAsync(project.ID, input);

                        resultCreate.PromotionPrice = 25;
                        resultCreate.PromotionTransferPrice = 50;
                        var result = await service.UpdateBudgetPromotionAsync(project.ID, resultCreate.Unit.Id.Value, resultCreate);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteBudgetPromotionAsync()
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
                        var service = new BudgetPromotionService(db);
                        
                        var project = await db.Projects.FirstOrDefaultAsync();
                        var unit = await db.Units.FirstOrDefaultAsync(f=>f.ProjectID ==project.ID);

                        var input = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.PromotionPrice, 50)
                                           .With(o => o.PromotionTransferPrice, 100)
                                           .Create();

                        var resultCreate = await service.CreateBudgetPromotionAsync(project.ID, input);

                        await service.DeleteBudgetPromotionAsync(project.ID, resultCreate.Unit.Id.Value);
                        bool afterDelete = db.SpecMaterialItems.Any(o => o.ID == resultCreate.Unit.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }



        [Fact]
        public async Task ExportExcelBudgetPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BudgetPromotionService(db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "10191");
                        var result = await service.ExportExcelBudgetPromotionAsync(project.ID);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
