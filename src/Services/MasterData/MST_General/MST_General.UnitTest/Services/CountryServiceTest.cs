using System;
using System.Diagnostics;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using MST_General.Params.Filters;
using MST_General.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagingExtensions;
using Xunit;
using Xunit.Abstractions;

namespace MST_General.UnitTests
{
    public class CountryServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetCountryDropdownListAsync()
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

                        var service = new CountryService(db);
                        CountryFilter filter = FixtureFactory.Get().Build<CountryFilter>().Create();
                        var results = await service.GetCountryDropdownListAsync(filter);

                        filter = new CountryFilter();
                        results = await service.GetCountryDropdownListAsync(filter);
                        Assert.NotEmpty(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetCountryListAsync()
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
                        var service = new CountryService(db);
                        CountryFilter filter = FixtureFactory.Get().Build<CountryFilter>().Create();
                        PageParam pageParam = new PageParam();
                        CountrySortByParam sortByParam = new CountrySortByParam();

                        var results = await service.GetCountryListAsync(filter, pageParam, sortByParam);

                        filter = new CountryFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CountrySortBy)).Cast<CountrySortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CountrySortByParam() { SortBy = item };
                            results = await service.GetCountryListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.Countries);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateCountryAsync()
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

                        var input = new CountryDTO
                        {
                            Code = "AB",
                            NameTH = "เทสประเทศ",
                            NameEN = "TestCountry"
                        };

                        var service = new CountryService(db);
                        var result = await service.CreateCountryAsync(input);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetCountryAsync()
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
                        var input = new CountryDTO
                        {
                            Code = "EB",
                            NameTH = "เทสประเทศ",
                            NameEN = "TestCountry"
                        };

                        var service = new CountryService(db);
                        var resultCreate = await service.CreateCountryAsync(input);

                        var result = await service.GetCountryAsync(resultCreate.Id.Value);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task FindCountryAsync()
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
                        var input = new CountryDTO();
                        input.Code = "EB";
                        input.NameTH = "เทสประเทศ";
                        input.NameEN = "TestCountry";

                        var service = new CountryService(db);
                        var resultCreate = await service.CreateCountryAsync(input);

                        var result = await service.FindCountryAsync(resultCreate.Code);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateCountryAsync()
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
                        var input = new CountryDTO();
                        input.Code = "EB";
                        input.NameTH = "เทสประเทศ";
                        input.NameEN = "TestCountry";

                        var service = new CountryService(db);
                        var resultCreate = await service.CreateCountryAsync(input);

                        resultCreate.NameTH = "เทสแก้ไขประเทศ";
                        var result = await service.UpdateCountryAsync(resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteCountryAsync()
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
                        var input = new CountryDTO();
                        input.Code = "EB";
                        input.NameTH = "เทสประเทศ";
                        input.NameEN = "TestCountry";

                        var service = new CountryService(db);
                        var resultCreate = await service.CreateCountryAsync(input);

                        bool beforeDelete = db.Countries.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.True(beforeDelete);
                        await service.DeleteCountryAsync(resultCreate.Id.Value);
                        bool afterDelete = db.Countries.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
