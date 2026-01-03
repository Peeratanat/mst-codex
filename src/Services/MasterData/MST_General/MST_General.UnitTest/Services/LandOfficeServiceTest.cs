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
    public class LandOfficeServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetLandOfficeDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here

                var service = new LandOfficeService(db);
                var province = await db.Provinces.FirstAsync(o => o.NameTH.Contains("กรุงเทพ"));
                var results = await service.GetLandOfficeDropdownListAsync("ก", province.ID);
                Assert.NotNull(results);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetLandOfficeListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here

                var service = new LandOfficeService(db);
                LandOfficeFilter filter = FixtureFactory.Get().Build<LandOfficeFilter>().Create();
                PageParam pageParam = new PageParam();
                LandOfficeSortByParam sortByParam = new LandOfficeSortByParam();
                var results = await service.GetLandOfficeListAsync(filter, pageParam, sortByParam);

                filter = new LandOfficeFilter();
                pageParam = new PageParam() { Page = 1, PageSize = 10 };

                var sortByParams = Enum.GetValues(typeof(LandOfficeSortBy)).Cast<LandOfficeSortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new LandOfficeSortByParam() { SortBy = item };
                    results = await service.GetLandOfficeListAsync(filter, pageParam, sortByParam);
                    Assert.NotNull(results.LandOffices);
                }

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task CreateLandOfficeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new LandOfficeService(db);
                var subDistrict = await db.SubDistricts.FirstOrDefaultAsync(o => o.NameTH == "สวด");
                var district = await db.Districts.FirstOrDefaultAsync(o => o.ID == subDistrict.DistrictID);
                var province = await db.Provinces.FirstOrDefaultAsync(o => o.ID == district.ProvinceID);
                var input = new LandOfficeDTO
                {
                    NameTH = "ยูนิตเทส",
                    NameEN = "UnitTest",
                    SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict),
                    Province = ProvinceListDTO.CreateFromModel(province),
                    District = DistrictListDTO.CreateFromModel(district)
                };
                var result = await service.CreateLandOfficeAsync(input);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetLandOfficeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new LandOfficeService(db);
                var subDistrict = await db.SubDistricts.FirstOrDefaultAsync(o => o.NameTH == "สวด");
                var district = await db.Districts.FirstOrDefaultAsync(o => o.ID == subDistrict.DistrictID);
                var province = await db.Provinces.FirstOrDefaultAsync(o => o.ID == district.ProvinceID);
                var input = new LandOfficeDTO
                {
                    NameTH = "ยูนิตเทส",
                    NameEN = "UnitTest",
                    SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict),
                    Province = ProvinceListDTO.CreateFromModel(province),
                    District = DistrictListDTO.CreateFromModel(district)
                };
                var resultCreate = await service.CreateLandOfficeAsync(input);
                var result = await service.GetLandOfficeAsync(resultCreate.Id.Value);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task UpdateLandOfficeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new LandOfficeService(db);
                var subDistrict = await db.SubDistricts.FirstOrDefaultAsync(o => o.NameTH == "สวด");
                var district = await db.Districts.FirstOrDefaultAsync(o => o.ID == subDistrict.DistrictID);
                var province = await db.Provinces.FirstOrDefaultAsync(o => o.ID == district.ProvinceID);
                var input = new LandOfficeDTO
                {
                    NameTH = "ยูนิตเทส",
                    NameEN = "UnitTest",
                    SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict),
                    Province = ProvinceListDTO.CreateFromModel(province),
                    District = DistrictListDTO.CreateFromModel(district)
                };
                var resultCreate = await service.CreateLandOfficeAsync(input);

                resultCreate.NameTH = "ยูนิตเทสสส";
                resultCreate.NameEN = "UnitTesttt";


                var result = await service.UpdateLandOfficeAsync(resultCreate.Id.Value, resultCreate);
                Assert.NotNull(result);
                Assert.Equal("ยูนิตเทสสส", result.NameTH);
                Assert.Equal("UnitTesttt", result.NameEN);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteLandOfficeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new LandOfficeService(db);
                var subDistrict = await db.SubDistricts.FirstOrDefaultAsync(o => o.NameTH == "สวด");
                var district = await db.Districts.FirstOrDefaultAsync(o => o.ID == subDistrict.DistrictID);
                var province = await db.Provinces.FirstOrDefaultAsync(o => o.ID == district.ProvinceID);
                var input = new LandOfficeDTO
                {
                    NameTH = "ยูนิตเทส",
                    NameEN = "UnitTest",
                    SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict),
                    Province = ProvinceListDTO.CreateFromModel(province),
                    District = DistrictListDTO.CreateFromModel(district)
                };

                var resultCreate = await service.CreateLandOfficeAsync(input);

                bool beforeDelete = db.LandOffices.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.True(beforeDelete);

                await service.DeleteLandOfficeAsync(resultCreate.Id.Value);

                bool afterDelete = db.LandOffices.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.False(afterDelete);
                bool afterSubDelete = db.SubDistricts.Any(o => o.LandOfficeID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.False(afterSubDelete);


                await tran.RollbackAsync();
            });
        }
    }
}
