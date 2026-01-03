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
using Base.DTOs;

namespace PRJ_Unit.UnitTests
{
    public class WaiveCustomerSignServiceTest
    {

        [Fact]
        public async Task GetWaiveCustomerSignListAsync()
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

                        var service = new WaiveCustomerSignService(db);
                        var project = await db.Projects.AsNoTracking().FirstAsync(f => f.ProjectNo == "60019");

                        PageParam pageParam = new PageParam()
                        {
                            Page = 1,
                            PageSize = 1
                        };
                        WaiveCustomerSignSortByParam sortByParam = new WaiveCustomerSignSortByParam
                        {
                            SortBy = WaiveCustomerSignSortBy.Unit,
                        };
                        var results = await service.GetWaiveCustomerSignListAsync(project.ID, new WaiveCustomerSignFilter(), pageParam, sortByParam);
                        Assert.NotEmpty(results.WaiveCustomerSigns);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetWaiveCustomerSignAsync()
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

                        var service = new WaiveCustomerSignService(db);

                        var model = await db.WaiveQCs.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var result = await service.GetWaiveCustomerSignAsync(model.ProjectID, model.ID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateWaiveCustomerSignAsync()
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

                        var service = new WaiveCustomerSignService(db);

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new WaiveCustomerSignDTO
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(unit),
                            WaiveSignDate = DateTime.Now
                        };

                        var result = await service.CreateWaiveCustomerSignAsync(unit.ProjectID.Value, input);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task ImportWaiveCustomerSignAsync()
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

                        var service = new WaiveCustomerSignService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstAsync();

                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_WaiveCustomerSign.xlsx",
                            Name = "ProjectID_WaiveCustomerSign.xlsx"
                        };
                        var results = await service.ImportWaiveCustomerSignAsync(project.ID, fileInput);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }



        [Fact]
        public async Task UpdateWaiveCustomerSignAsync()
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

                        var service = new WaiveCustomerSignService(db);

                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "60019").FirstAsync();
                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();

                        var input = new WaiveCustomerSignDTO
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(unit),
                            WaiveSignDate = DateTime.Now
                        };

                        var resultCreate = await service.CreateWaiveCustomerSignAsync(unit.ProjectID.Value, input);

                        resultCreate.WaiveSignDate = new DateTime(2019, 5, 22);

                        var result = await service.UpdateWaiveCustomerSignAsync(unit.ProjectID.Value, resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);
                        Assert.Equal(result.WaiveSignDate, new DateTime(2019, 5, 22));

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteWaiveCustomerSignAsync()
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

                        var service = new WaiveCustomerSignService(db);

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new WaiveCustomerSignDTO
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(unit),
                            WaiveSignDate = DateTime.Now
                        };

                        var resultCreate = await service.CreateWaiveCustomerSignAsync(unit.ProjectID.Value, input);

                        await service.DeleteWaiveCustomerSignAsync(unit.ProjectID.Value, resultCreate.Id.Value);
                        bool afterDelete = db.WaiveQCs.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
