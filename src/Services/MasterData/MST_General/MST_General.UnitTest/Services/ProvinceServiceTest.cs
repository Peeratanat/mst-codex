using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
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
    public class ProvinceServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task FindProvinceAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var service = new ProvinceService(db);

                var model = await db.Provinces.FirstAsync();
                var result = await service.FindProvinceAsync(model.NameTH);
                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetProvinceListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here

                var service = new ProvinceService(db);
                ProvinceFilter filter = FixtureFactory.Get().Build<ProvinceFilter>().Create();
                PageParam pageParam = new PageParam();
                ProvinceSortByParam sortByParam = new ProvinceSortByParam();

                var results = await service.GetProvinceListAsync(filter, pageParam, sortByParam);

                filter = new ProvinceFilter();
                pageParam = new PageParam() { Page = 1, PageSize = 10 };

                var sortByParams = Enum.GetValues(typeof(ProvinceSortBy)).Cast<ProvinceSortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new ProvinceSortByParam() { SortBy = item };
                    results = await service.GetProvinceListAsync(filter, pageParam, sortByParam);
                    Assert.NotNull(results.Provinces);
                }

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task CreateProvinceAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var input = new ProvinceDTO();
                input.NameTH = "เทส";
                input.NameEN = "Test";

                var service = new ProvinceService(db);
                var result = await service.CreateProvinceAsync(input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetProvinceAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var input = new ProvinceDTO();
                input.NameTH = "เทส";
                input.NameEN = "Test";

                var service = new ProvinceService(db);
                var resultCreate = await service.CreateProvinceAsync(input);

                var result = await service.GetProvinceAsync(resultCreate.Id.Value);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetProvincePostalCodeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var service = new ProvinceService(db);

                var result = await service.GetProvincePostalCodeAsync("39140");

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task UpdateProvinceAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var input = new ProvinceDTO();
                input.NameTH = "เทส";
                input.NameEN = "Test";

                var service = new ProvinceService(db);
                var resultCreate = await service.CreateProvinceAsync(input);
                resultCreate.NameTH = "เทสหนึ่ง";
                var result = await service.UpdateProvinceAsync(resultCreate.Id.Value, resultCreate);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteProvinceAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var input = new ProvinceDTO
                {
                    NameTH = "เทส",
                    NameEN = "Test"
                };

                var service = new ProvinceService(db);
                var resultCreate = await service.CreateProvinceAsync(input);

                bool beforeDelete = db.Provinces.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteProvinceAsync(resultCreate.Id.Value);
                bool afterDelete = db.Provinces.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
    }
}
