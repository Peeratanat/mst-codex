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

namespace PRJ_Unit.UnitTests
{
    public class LowRiseFenceFeeServiceTest
    {
        IConfiguration Configuration;
        public LowRiseFenceFeeServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetLowRiseFenceFeeListAsync()
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

                        var service = new LowRiseFenceFeeService(db);

                        LowRiseFenceFeeFilter filter = FixtureFactory.Get().Build<LowRiseFenceFeeFilter>().Create();

                        var project = await db.Projects.FirstAsync();
                        PageParam pageParam = new PageParam();
                        LowRiseFenceFeeSortByParam sortByParam = new LowRiseFenceFeeSortByParam();
                        var results = await service.GetLowRiseFenceFeeListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new LowRiseFenceFeeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(LowRiseFenceFeeSortBy)).Cast<LowRiseFenceFeeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new LowRiseFenceFeeSortByParam() { SortBy = item };
                            results = await service.GetLowRiseFenceFeeListAsync(project.ID, filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.LowRiseFenceFees);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetLowRiseFenceFeeAsync()
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

                        var service = new LowRiseFenceFeeService(db);
                        var model = await db.LowRiseFenceFees.Where(o => !o.IsDeleted).FirstAsync();

                        var result = await service.GetLowRiseFenceFeeAsync(model.ProjectID, model.ID);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateLowRiseFenceFeeAsync()
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

                        var service = new LowRiseFenceFeeService(db);
                        var landOffice = await db.LandOffices.Where(o => !o.IsDeleted).FirstAsync();
                        var type = await db.TypeOfRealEstates.Where(o => !o.IsDeleted).FirstAsync();
                        var project = await db.Projects.Where(o => !o.IsDeleted).FirstAsync();

                        //var project = await db.Projects.Where(o => o.ProjectNo == "20005").FirstAsync();
                        //var landOffice = await db.LandOffices.Where(o => o.ID == new Guid("0504F6F7-EC51-45E4-89CA-7EC1C1E89B76")).FirstAsync();
                        ////var type = await db.TypeOfRealEstates.Where(o => o.ID == new Guid("5BD74C37-D843-4369-A838-F035CBD2D81E")).FirstAsync();
                        var input = new LowRiseFenceFeeDTO();
                        input.LandOffice = LandOfficeListDTO.CreateFromModel(landOffice);
                        input.TypeOfRealEstate = TypeOfRealEstateDropdownDTO.CreateFromModel(type);

                        var result = await service.CreateLowRiseFenceFeeAsync(project.ID, input);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateLowRiseFenceFeeAsync()
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

                        var service = new LowRiseFenceFeeService(db);
                        var landOffice = await db.LandOffices.Where(o => !o.IsDeleted).FirstAsync();
                        var type = await db.TypeOfRealEstates.Where(o => !o.IsDeleted).FirstAsync();
                        var project = await db.Projects.Where(o => !o.IsDeleted).FirstAsync();
                        var input = new LowRiseFenceFeeDTO();
                        input.LandOffice = LandOfficeListDTO.CreateFromModel(landOffice);
                        input.TypeOfRealEstate = TypeOfRealEstateDropdownDTO.CreateFromModel(type);

                        var resultCreate = await service.CreateLowRiseFenceFeeAsync(project.ID, input);

                        resultCreate.IronRate = 6;

                        var result = await service.UpdateLowRiseFenceFeeAsync(project.ID, resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);
                        Assert.Equal(result.IronRate, 6);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteLowRiseFenceFeeAsync()
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

                        var service = new LowRiseFenceFeeService(db);
                        var landOffice = await db.LandOffices.Where(o => !o.IsDeleted).FirstAsync();
                        var type = await db.TypeOfRealEstates.Where(o => !o.IsDeleted).FirstAsync();
                        var project = await db.Projects.Where(o => !o.IsDeleted).FirstAsync();
                        var input = new LowRiseFenceFeeDTO();
                        input.LandOffice = LandOfficeListDTO.CreateFromModel(landOffice);
                        input.TypeOfRealEstate = TypeOfRealEstateDropdownDTO.CreateFromModel(type);

                        var resultCreate = await service.CreateLowRiseFenceFeeAsync(project.ID, input);

                        await service.DeleteLowRiseFenceFeeAsync(project.ID, resultCreate.Id.Value);
                        bool afterDelete = db.LowRiseFenceFees.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
