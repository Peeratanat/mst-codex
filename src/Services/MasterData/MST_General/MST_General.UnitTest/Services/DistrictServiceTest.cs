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
    public class DistrictServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task FindDistrictAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var service = new DistrictService(db);
                var province = await db.Provinces.FirstAsync();
                var district = await db.Districts.FirstAsync(o => o.ProvinceID == province.ID);
                var result = await service.FindDistrictAsync(province.ID, district.NameTH);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetDistrictListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here

                var service = new DistrictService(db);
                DistrictFilter filter = FixtureFactory.Get().Build<DistrictFilter>().Create();
                PageParam pageParam = new PageParam();
                DistrictSortByParam sortByParam = new DistrictSortByParam();
                var results = await service.GetDistrictListAsync(filter, pageParam, sortByParam);

                filter = new DistrictFilter();
                pageParam = new PageParam() { Page = 1, PageSize = 10 };

                var sortByParams = Enum.GetValues(typeof(DistrictSortBy)).Cast<DistrictSortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new DistrictSortByParam() { SortBy = item };
                    results = await service.GetDistrictListAsync(filter, pageParam, sortByParam);
                    Assert.NotNull(results.Districts);
                }

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task CreateDistrictAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var province = await db.Provinces.FirstAsync(o => !o.IsDeleted);

                var service = new DistrictService(db);
                var input = new DistrictDTO
                {
                    NameTH = "เมือง",
                    Province = ProvinceListDTO.CreateFromModel(province)
                };
                var result = await service.CreateDistrictAsync(input);
                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetDistrictAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var province = await db.Provinces.FirstAsync(o => !o.IsDeleted);

                var service = new DistrictService(db);
                var input = new DistrictDTO
                {
                    NameTH = "เมือง",
                    Province = ProvinceListDTO.CreateFromModel(province)
                };

                var resultCreate = await service.CreateDistrictAsync(input);
                var result = await service.GetDistrictAsync(resultCreate.Id.Value);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task UpdateDistrictAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var province = await db.Provinces.FirstAsync(o => !o.IsDeleted);

                var service = new DistrictService(db);
                var input = new DistrictDTO
                {
                    NameTH = "เมือง",
                    Province = ProvinceListDTO.CreateFromModel(province)
                };

                var resultCreate = await service.CreateDistrictAsync(input);
                resultCreate.NameTH = "เทส";
                resultCreate.NameEN = "Test";
                var result = await service.UpdateDistrictAsync(resultCreate.Id.Value, resultCreate);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteDistrictAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var province = await db.Provinces.FirstAsync(o => !o.IsDeleted);

                var service = new DistrictService(db);
                var input = new DistrictDTO
                {
                    NameTH = "เมือง",
                    Province = ProvinceListDTO.CreateFromModel(province)
                };

                var resultCreate = await service.CreateDistrictAsync(input);


                bool beforeDelete = db.Districts.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteDistrictAsync(resultCreate.Id.Value);
                bool afterDelete = db.Districts.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.False(afterDelete);
                await tran.RollbackAsync();
            });
        }
    }
}
