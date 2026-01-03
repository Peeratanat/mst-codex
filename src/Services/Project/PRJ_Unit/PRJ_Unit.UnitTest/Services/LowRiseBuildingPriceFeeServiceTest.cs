using AutoFixture;
using CustomAutoFixture;
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

namespace PRJ_Unit.UnitTests
{
    public class LowRiseBuildingPriceFeeServiceTest
    {

        [Fact]
        public async Task GetLowRiseBuildingPriceFeeListAsync()
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

                        var service = new LowRiseBuildingPriceFeeService(db);

                        LowRiseBuildingPriceFeeFilter filter = FixtureFactory.Get().Build<LowRiseBuildingPriceFeeFilter>().Create();

                        var project = await db.Projects.FirstAsync();
                        PageParam pageParam = new PageParam();
                        LowRiseBuildingPriceFeeSortByParam sortByParam = new LowRiseBuildingPriceFeeSortByParam();
                        var results = await service.GetLowRiseBuildingPriceFeeListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new LowRiseBuildingPriceFeeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(LowRiseBuildingPriceFeeSortBy)).Cast<LowRiseBuildingPriceFeeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new LowRiseBuildingPriceFeeSortByParam() { SortBy = item };
                            results = await service.GetLowRiseBuildingPriceFeeListAsync(project.ID, filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.LowRiseBuildingPriceFees);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetLowRiseBuildingPriceFeeAsync()
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

                        var service = new LowRiseBuildingPriceFeeService(db);

                        var model = await db.LowRiseBuildingPriceFees.Where(o => !o.IsDeleted).FirstAsync();

                        var result = await service.GetLowRiseBuildingPriceFeeAsync(model.ProjectID, model.ID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateLowRiseBuildingPriceFeeAsync()
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

                        var service = new LowRiseBuildingPriceFeeService(db);
                        var project = await (from t1 in db.Projects
                                             join t2 in db.LowRiseBuildingPriceFees on t1.ID equals t2.ProjectID into t2Group
                                             from t2 in t2Group.DefaultIfEmpty()
                                             where t2 == null
                                             select t1).FirstOrDefaultAsync();

                        var model = await db.Models.FirstOrDefaultAsync(o => o.ProjectID == project.ID);

                        var input = new LowRiseBuildingPriceFeeDTO
                        {
                            Model = ModelDropdownDTO.CreateFromModel(model),
                            Price = 600
                        };

                        var result = await service.CreateLowRiseBuildingPriceFeeAsync(model.ProjectID, input);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateLowRiseBuildingPriceFeesync()
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

                        var service = new LowRiseBuildingPriceFeeService(db);
                        var project = await (from t1 in db.Projects
                                             join t2 in db.LowRiseBuildingPriceFees on t1.ID equals t2.ProjectID into t2Group
                                             from t2 in t2Group.DefaultIfEmpty()
                                             where t2 == null
                                             select t1).FirstOrDefaultAsync();

                        var model = await db.Models.FirstOrDefaultAsync(o => o.ProjectID == project.ID);

                        var input = new LowRiseBuildingPriceFeeDTO
                        {
                            Model = ModelDropdownDTO.CreateFromModel(model),
                            Price = 600
                        };

                        var resultCreate = await service.CreateLowRiseBuildingPriceFeeAsync(model.ProjectID, input);
                        resultCreate.Price = 1000;
                        var result = await service.UpdateLowRiseBuildingPriceFeesync(model.ProjectID, resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteLowRiseBuildingPriceFeeAsync()
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

                        var service = new LowRiseBuildingPriceFeeService(db);
                        var project = await (from t1 in db.Projects
                                             join t2 in db.LowRiseBuildingPriceFees on t1.ID equals t2.ProjectID into t2Group
                                             from t2 in t2Group.DefaultIfEmpty()
                                             where t2 == null
                                             select t1).FirstOrDefaultAsync();

                        var model = await db.Models.FirstOrDefaultAsync(o => o.ProjectID == project.ID);

                        var input = new LowRiseBuildingPriceFeeDTO
                        {
                            Model = ModelDropdownDTO.CreateFromModel(model),
                            Price = 600
                        };

                        var resultCreate = await service.CreateLowRiseBuildingPriceFeeAsync(model.ProjectID, input);

                        await service.DeleteLowRiseBuildingPriceFeeAsync(model.ProjectID, resultCreate.Id.Value);
                        var afterDelete = await db.LowRiseBuildingPriceFees.AnyAsync(o => o.ProjectID == model.ProjectID && o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
