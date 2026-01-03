using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using MST_General.Params.Filters;
using MST_General.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MST_General.UnitTests
{
    public class TypeOfRealEstateServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetTypeOfRealEstateListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here

                var service = new TypeOfRealEstateService(db);
                TypeOfRealEstateFilter filter = FixtureFactory.Get().Build<TypeOfRealEstateFilter>().Create();
                PageParam pageParam = new PageParam();
                TypeOfRealEstateSortByParam sortByParam = new TypeOfRealEstateSortByParam();
                filter.RealEstateCategoryKey = (await db.MasterCenters.FirstOrDefaultAsync(x => x.MasterCenterGroupKey == "RealEstateCategory"))?.Key ?? string.Empty;
                var results = await service.GetTypeOfRealEstateListAsync(filter, pageParam, sortByParam);

                filter = new TypeOfRealEstateFilter();
                pageParam = new PageParam() { Page = 1, PageSize = 10 };

                var sortByParams = Enum.GetValues(typeof(TypeOfRealEstateSortBy)).Cast<TypeOfRealEstateSortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new TypeOfRealEstateSortByParam() { SortBy = item };
                    results = await service.GetTypeOfRealEstateListAsync(filter, pageParam, sortByParam);
                    Assert.NotEmpty(results.TypeOfRealEstates);
                }
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task CreateTypeOfRealEstateAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var realEstateCategory = await db.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == "RealEstateCategory");
                var input = new TypeOfRealEstateDTO
                {
                    Name = "Test",
                    Code = "55",
                    RealEstateCategory = MasterCenterDropdownDTO.CreateFromModel(realEstateCategory)
                };
                var service = new TypeOfRealEstateService(db);

                var result = await service.CreateTypeOfRealEstateAsync(input);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }


        [Fact]
        public async Task GetTypeOfRealEstateAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var realEstateCategory = await db.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == "RealEstateCategory");
                var input = new TypeOfRealEstateDTO
                {
                    Name = "Test",
                    Code = "55",
                    RealEstateCategory = MasterCenterDropdownDTO.CreateFromModel(realEstateCategory)
                };
                var service = new TypeOfRealEstateService(db);

                var resultCreate = await service.CreateTypeOfRealEstateAsync(input);

                var result = await service.GetTypeOfRealEstateAsync(resultCreate.Id.Value);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task UpdateTypeOfRealEstateAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var realEstateCategory = await db.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == "RealEstateCategory");
                var input = new TypeOfRealEstateDTO
                {
                    Name = "Test",
                    Code = "55",
                    RealEstateCategory = MasterCenterDropdownDTO.CreateFromModel(realEstateCategory)
                };
                var service = new TypeOfRealEstateService(db);

                var resultCreate = await service.CreateTypeOfRealEstateAsync(input);
                resultCreate.Name = "Test1234";

                var result = await service.UpdateTypeOfRealEstateAsync(resultCreate.Id.Value, resultCreate);
                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteTypeOfRealEstateAsync()
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
                        var realEstateCategory = await db.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == "RealEstateCategory");
                        var input = new TypeOfRealEstateDTO
                        {
                            Name = "Test",
                            Code = "55",
                            RealEstateCategory = MasterCenterDropdownDTO.CreateFromModel(realEstateCategory)
                        };
                        var service = new TypeOfRealEstateService(db);

                        var resultCreate = await service.CreateTypeOfRealEstateAsync(input);

                        bool beforeDelete = db.TypeOfRealEstates.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.True(beforeDelete);
                        await service.DeleteTypeOfRealEstateAsync(resultCreate.Id.Value);
                        bool afterDelete = db.TypeOfRealEstates.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
