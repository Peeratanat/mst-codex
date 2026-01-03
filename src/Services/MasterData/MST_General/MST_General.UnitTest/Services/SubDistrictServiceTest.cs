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
    public class SubDistrictServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task FindSubDistrictAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var service = new SubDistrictService(db);
                var province = await db.Provinces.FirstAsync();
                var district = await db.Districts.FirstAsync(o => o.ProvinceID == province.ID);
                var subDistrict = await db.SubDistricts.FirstAsync(o => o.DistrictID == district.ID);

                var result = await service.FindSubDistrictAsync(district.ID, district.NameTH);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetSubDistrictListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                //Put unit test here

                var service = new SubDistrictService(db);
                SubDistrictFilter filter = FixtureFactory.Get().Build<SubDistrictFilter>().Create();
                PageParam pageParam = new PageParam();
                SubDistrictSortByParam sortByParam = new SubDistrictSortByParam();

                var results = await service.GetSubDistrictListAsync(filter, pageParam, sortByParam);

                filter = new SubDistrictFilter();
                pageParam = new PageParam() { Page = 1, PageSize = 10 };

                var sortByParams = Enum.GetValues(typeof(SubDistrictSortBy)).Cast<SubDistrictSortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new SubDistrictSortByParam() { SortBy = item };
                    results = await service.GetSubDistrictListAsync(filter, pageParam, sortByParam);
                    Assert.NotNull(results.SubDistricts);
                }
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task CreateSubDistrictAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var district = await db.Districts.FirstOrDefaultAsync(o => !o.IsDeleted);
                //Put unit test here
                var input = new SubDistrictDTO
                {
                    District = DistrictListDTO.CreateFromModel(district),
                    NameTH = "เทส",
                    PostalCode = "55555",
                    LandOffice = new LandOfficeListDTO
                    {
                        NameTH = "เทสสสสสสสสสสส"
                    }
                };
                var service = new SubDistrictService(db);

                var result = await service.CreateSubDistrictAsync(input);
                var testCreateLandOffice = await db.LandOffices.FirstOrDefaultAsync(o => o.NameTH == "เทสสสสสสสสสสส");
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetSubDistrictAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                //Put unit test here
                var district = await db.Districts.FirstOrDefaultAsync(o => !o.IsDeleted);

                var input = new SubDistrictDTO
                {
                    District = DistrictListDTO.CreateFromModel(district),
                    NameTH = "เทส",
                    PostalCode = "55555"
                };

                var service = new SubDistrictService(db);

                var resultCreate = await service.CreateSubDistrictAsync(input);

                var result = await service.GetSubDistrictAsync(resultCreate.Id.Value);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task UpdateSubDistrictAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                //Put unit test here
                var district = await db.Districts.FirstOrDefaultAsync(o => !o.IsDeleted);

                var input = new SubDistrictDTO
                {
                    District = DistrictListDTO.CreateFromModel(district),
                    NameTH = "เทส",
                    PostalCode = "81150"
                };
                var service = new SubDistrictService(db);

                var resultCreate = await service.CreateSubDistrictAsync(input);

                resultCreate.LandOffice = new LandOfficeListDTO
                {
                    NameTH = "เทสสสสสสสสสสส"
                };

                var result = await service.UpdateSubDistrictAsync(resultCreate.Id.Value, resultCreate);
                var testCreateLandOffice = await db.LandOffices.FirstOrDefaultAsync(o => o.NameTH == "เทสสสสสสสสสสส");
                Assert.NotNull(testCreateLandOffice);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteSubDistrictAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var district = await db.Districts.Where(o => !o.IsDeleted).FirstAsync();

                var input = new SubDistrictDTO
                {
                    District = DistrictListDTO.CreateFromModel(district),
                    NameTH = "เทส",
                    PostalCode = "55555"
                };
                var service = new SubDistrictService(db);

                var resultCreate = await service.CreateSubDistrictAsync(input);

                bool beforeDelete = db.SubDistricts.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteSubDistrictAsync(resultCreate.Id.Value);
                bool afterDelete = db.SubDistricts.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
    }
}
