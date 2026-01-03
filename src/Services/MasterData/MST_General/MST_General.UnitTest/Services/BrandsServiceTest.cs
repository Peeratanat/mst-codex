using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using Database.UnitTestExtensions;
using MST_General.Params.Filters;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using MST_General.Services;

namespace MST_General.UnitTests
{
    public class BrandsServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        [Fact]
        public async Task GetBrandListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new BrandService(db);
                BrandFilter filter = FixtureFactory.Get().Build<BrandFilter>().Create();
                PageParam pageParam = new PageParam();
                BrandSortByParam sortByParam = new BrandSortByParam();
                filter.UnitNumberFormatKey = "1";
                var results = await service.GetBrandListAsync(filter, pageParam, sortByParam);

                filter = new BrandFilter();
                pageParam = new PageParam() { Page = 1, PageSize = 10 };

                var sortByParams = Enum.GetValues(typeof(BrandSortBy)).Cast<BrandSortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new BrandSortByParam() { SortBy = item };
                    results = await service.GetBrandListAsync(filter, pageParam, sortByParam);
                    Assert.NotEmpty(results.Brands);
                }

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task CreateBrandAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var unitForMatNumber = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitNumberFormat").FirstAsync();

                var input = new BrandDTO
                {
                    BrandNo = "0001",
                    Name = "HP"
                };
                //input.UnitNumberFormat = MasterCenterDropdownDTO.CreateFromModel(unitForMatNumber);

                var service = new BrandService(db);
                var result = await service.CreateBrandAsync(input);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetBrandAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var unitForMatNumber = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitNumberFormat").FirstAsync();

                var input = new BrandDTO
                {
                    BrandNo = "0001",
                    Name = "HP",
                    UnitNumberFormat = MasterCenterDropdownDTO.CreateFromModel(unitForMatNumber)
                };

                var service = new BrandService(db);
                var resultCreate = await service.CreateBrandAsync(input);

                var result = await service.GetBrandAsync(resultCreate.Id.Value);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task UpdateBrandAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var unitForMatNumber = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitNumberFormat").FirstAsync();

                var input = new BrandDTO
                {
                    BrandNo = "0001",
                    Name = "HP",
                    UnitNumberFormat = MasterCenterDropdownDTO.CreateFromModel(unitForMatNumber)
                };

                var service = new BrandService(db);
                var resultCreate = await service.CreateBrandAsync(input);
                resultCreate.Name = "HP01";
                var result = await service.UpdateBrandAsync(resultCreate.Id.Value, resultCreate);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteBrandAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var unitForMatNumber = await db.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == "UnitNumberFormat");

                var input = new BrandDTO
                {
                    BrandNo = "0001",
                    Name = "HP",
                    UnitNumberFormat = MasterCenterDropdownDTO.CreateFromModel(unitForMatNumber)
                };

                var service = new BrandService(db);
                var resultCreate = await service.CreateBrandAsync(input);
                bool beforeDelete = db.Brands.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteBrandAsync(resultCreate.Id.Value);
                bool afterDelete = db.Brands.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
    }
}
