using AutoFixture;
using Base.DTOs.MST;
using CustomAutoFixture;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using MST_General.Params.Filters;
using MST_General.Service;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MST_General.UnitTests
{
    public class CompaniesServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        [Fact]
        public async Task GetCompanyDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here

                var service = new CompanyService(db);
                CompanyDropdownFilter filter = FixtureFactory.Get().Build<CompanyDropdownFilter>().Create();
                PageParam pageParam = new PageParam();
                CompanyDropdownSortByParam sortByParam = new CompanyDropdownSortByParam();
                var results = await service.GetCompanyDropdownListAsync(filter, sortByParam);

                filter = new CompanyDropdownFilter();
                pageParam = new PageParam() { Page = 1, PageSize = 10 };

                var sortByParams = Enum.GetValues(typeof(CompanyDropdownSortBy)).Cast<CompanyDropdownSortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new CompanyDropdownSortByParam() { SortBy = item };
                    results = await service.GetCompanyDropdownListAsync(filter, sortByParam);
                    Assert.NotEmpty(results);
                }
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetCompanyListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here

                var service = new CompanyService(db);
                CompanyFilter filter = FixtureFactory.Get().Build<CompanyFilter>().Create();
                PageParam pageParam = new PageParam();
                CompanySortByParam sortByParam = new CompanySortByParam();
                var results = await service.GetCompanyListAsync(filter, pageParam, sortByParam);

                filter = new CompanyFilter();
                pageParam = new PageParam() { Page = 1, PageSize = 10 };

                var sortByParams = Enum.GetValues(typeof(CompanySortBy)).Cast<CompanySortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new CompanySortByParam() { SortBy = item };
                    results = await service.GetCompanyListAsync(filter, pageParam, sortByParam);
                    Assert.NotEmpty(results.Companies);
                }
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetCompanyAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var province = await db.Provinces.FirstOrDefaultAsync(o => !o.IsDeleted);
                var district = await db.Districts.FirstOrDefaultAsync(o => !o.IsDeleted && o.ProvinceID == province.ID);
                var subdistrict = await db.SubDistricts.FirstOrDefaultAsync(o => !o.IsDeleted && o.DistrictID == district.ID);

                var service = new CompanyService(db);
                var input = FixtureFactory.Get().Build<CompanyDTO>()
                                   .With(o => o.Id, (Guid?)null)
                                   .With(o => o.Province, ProvinceListDTO.CreateFromModel(province))
                                   .With(o => o.District, DistrictListDTO.CreateFromModel(district))
                                   .With(o => o.SubDistrict, SubDistrictListDTO.CreateFromModel(subdistrict))
                                   .Create();

                var resultCreate = await service.CreateCompanyAsync(input);

                var result = await service.GetCompanyAsync(resultCreate.Id.Value);
                Assert.True(result.Id == resultCreate.Id);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task UpdateCompanyAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var province = await db.Provinces.FirstOrDefaultAsync(o => !o.IsDeleted);
                var district = await db.Districts.FirstOrDefaultAsync(o => !o.IsDeleted && o.ProvinceID == province.ID);
                var subdistrict = await db.SubDistricts.FirstOrDefaultAsync(o => !o.IsDeleted && o.DistrictID == district.ID);

                var service = new CompanyService(db);
                var input = FixtureFactory.Get().Build<CompanyDTO>()
                                   .With(o => o.Id, (Guid?)null)
                                   .With(o => o.Province, ProvinceListDTO.CreateFromModel(province))
                                   .With(o => o.District, DistrictListDTO.CreateFromModel(district))
                                   .With(o => o.SubDistrict, SubDistrictListDTO.CreateFromModel(subdistrict))
                                   .Create();

                var resultCreate = await service.CreateCompanyAsync(input);
                Assert.NotNull(resultCreate);
                resultCreate.NameTH = "Test";
                var resultUpdate = await service.UpdateCompanyAsync(resultCreate.Id.Value, resultCreate);
                Assert.True(resultUpdate.NameTH == resultCreate.NameTH);
                await tran.RollbackAsync();
            });
        }

    }
}
