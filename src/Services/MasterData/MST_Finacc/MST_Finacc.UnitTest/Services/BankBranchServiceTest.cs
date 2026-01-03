using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using Database.UnitTestExtensions;
using MST_Finacc.Params.Filters;
using MST_Finacc.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagingExtensions;
using Xunit;

namespace MST_Finacc.UnitTests
{
    public class BankBranchServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetBankBranchListAsync()
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

                        var service = new BankBranchService(db);
                        BankBranchFilter filter = FixtureFactory.Get().Build<BankBranchFilter>().Create();
                        PageParam pageParam = new PageParam();
                        BankBranchSortByParam sortByParam = new BankBranchSortByParam();

                        var results = await service.GetBankBranchListAsync(filter, pageParam, sortByParam);

                        filter = new BankBranchFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(BankBranchSortBy)).Cast<BankBranchSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new BankBranchSortByParam() { SortBy = item };
                            results = await service.GetBankBranchListAsync(filter, pageParam, sortByParam);
                            Assert.NotNull(results.BankBranches);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateBankBranchAsync()
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
                        var bank = await db.Banks.FirstAsync();
                        var province = await db.Provinces.Where(o => !o.IsDeleted).FirstAsync();
                        var district = await db.Districts.Where(o => !o.IsDeleted && o.ProvinceID == province.ID).FirstAsync();
                        var subdistrict = await db.SubDistricts.Where(o => !o.IsDeleted && o.DistrictID == district.ID).FirstAsync();

                        var service = new BankBranchService(db);
                        var input = new BankBranchDTO
                        {
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            Province = ProvinceListDTO.CreateFromModel(province),
                            District = DistrictListDTO.CreateFromModel(district),
                            SubDistrict = SubDistrictListDTO.CreateFromModel(subdistrict),
                            AreaCode = "111",
                            Name = "เทส",
                            PostalCode = "5555",
                            IsActive = true
                        };

                        var result = await service.CreateBankBranchAsync(input);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetBankBranchAsync()
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
                        var bank = await db.Banks.FirstAsync();
                        var province = await db.Provinces.Where(o => !o.IsDeleted).FirstAsync();
                        var district = await db.Districts.Where(o => !o.IsDeleted && o.ProvinceID == province.ID).FirstAsync();
                        var subdistrict = await db.SubDistricts.Where(o => !o.IsDeleted && o.DistrictID == district.ID).FirstAsync();

                        var service = new BankBranchService(db);
                        var input = new BankBranchDTO
                        {
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            Province = ProvinceListDTO.CreateFromModel(province),
                            District = DistrictListDTO.CreateFromModel(district),
                            SubDistrict = SubDistrictListDTO.CreateFromModel(subdistrict),
                            AreaCode = "111",
                            Name = "เทส",
                            PostalCode = "5555",
                        };

                        var resultCreate = await service.CreateBankBranchAsync(input);

                        var result = await service.GetBankBranchAsync(resultCreate.Id.Value);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateBankBranchAsync()
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
                        var bank = await db.Banks.FirstAsync();
                        var province = await db.Provinces.Where(o => !o.IsDeleted).FirstAsync();
                        var district = await db.Districts.Where(o => !o.IsDeleted && o.ProvinceID == province.ID).FirstAsync();
                        var subdistrict = await db.SubDistricts.Where(o => !o.IsDeleted && o.DistrictID == district.ID).FirstAsync();

                        var service = new BankBranchService(db);
                        var input = new BankBranchDTO
                        {
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            Province = ProvinceListDTO.CreateFromModel(province),
                            District = DistrictListDTO.CreateFromModel(district),
                            SubDistrict = SubDistrictListDTO.CreateFromModel(subdistrict),
                            AreaCode = "111",
                            Name = "เทส",
                            PostalCode = "5555",
                        };

                        var resultCreate = await service.CreateBankBranchAsync(input);
                        resultCreate.Name = "ภาษาไทย";
                        var resultUpdate = await service.UpdateBankBranchAsync(resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(resultUpdate);
                        Assert.Equal("ภาษาไทย", resultUpdate.Name);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteBankBranchAsync()
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
                        var bank = await db.Banks.FirstAsync();
                        var province = await db.Provinces.Where(o => !o.IsDeleted).FirstAsync();
                        var district = await db.Districts.Where(o => !o.IsDeleted && o.ProvinceID == province.ID).FirstAsync();
                        var subdistrict = await db.SubDistricts.Where(o => !o.IsDeleted && o.DistrictID == district.ID).FirstAsync();

                        var service = new BankBranchService(db);
                        var input = new BankBranchDTO
                        {
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            Province = ProvinceListDTO.CreateFromModel(province),
                            District = DistrictListDTO.CreateFromModel(district),
                            SubDistrict = SubDistrictListDTO.CreateFromModel(subdistrict),
                            AreaCode = "111",
                            Name = "เทส",
                            PostalCode = "5555",
                        };

                        var resultCreate = await service.CreateBankBranchAsync(input);
                        await service.DeleteBankBranchAsync(resultCreate.Id.Value);
                        var afterDelete = db.BankBranches.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
