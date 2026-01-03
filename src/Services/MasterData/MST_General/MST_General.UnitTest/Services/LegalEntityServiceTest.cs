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
    public class LegalEntityServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetLegalEntityListAsync()
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

                        var service = new LegalEntityService(db);
                        LegalFilter filter = FixtureFactory.Get().Build<LegalFilter>().Create();
                        PageParam pageParam = new PageParam();
                        LegalEntitySortByParam sortByParam = new LegalEntitySortByParam();
                        var results = await service.GetLegalEntityListAsync(filter, pageParam, sortByParam);

                        filter = new LegalFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(LegalEntitySortBy)).Cast<LegalEntitySortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new LegalEntitySortByParam() { SortBy = item };
                            results = await service.GetLegalEntityListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.LegalEntities);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateLegalEntityAsync()
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
                        var bankAccType = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "BankAccountType");
                        var service = new LegalEntityService(db);
                        var input = new LegalEntityDTO
                        {
                            NameTH = "เทส",
                            NameEN = "Test",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccType),
                            BankAccountNo = "222222"
                        };

                        var result = await service.CreateLegalEntityAsync(input);

                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetLegalEntityAsync()
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
                        var service = new LegalEntityService(db);
                        var bankAccType = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "BankAccountType");
                        var input = new LegalEntityDTO
                        {
                            NameTH = "เทส",
                            NameEN = "Test",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            BankAccountNo = "222222",
                            BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccType)
                        };

                        var resultCreate = await service.CreateLegalEntityAsync(input);
                        Assert.NotNull(resultCreate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateLegalEntityAsync()
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
                        var service = new LegalEntityService(db);
                        var bankAccType = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "BankAccountType");
                        var input = new LegalEntityDTO
                        {
                            NameTH = "เทส",
                            NameEN = "Test",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            BankAccountNo = "222222",
                            BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccType)
                        };

                        var resultCreate = await service.CreateLegalEntityAsync(input);
                        resultCreate.NameTH = "เทส";
                        resultCreate.NameEN = "TestTest";
                        var result = await service.UpdateLegalEntityAsync(resultCreate.Id.Value, resultCreate);

                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteLegalEntityAsync()
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
                        var bankAccType = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "BankAccountType");
                        var service = new LegalEntityService(db);
                        var input = new LegalEntityDTO
                        {
                            NameTH = "เทส",
                            NameEN = "Test",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            BankAccountNo = "222222",
                            BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccType)
                        };

                        var resultCreate = await service.CreateLegalEntityAsync(input);

                        bool beforeDelete = db.LegalEntities.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.True(beforeDelete);
                        await service.DeleteLegalEntityAsync(resultCreate.Id.Value);
                        bool afterDelete = db.LegalEntities.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
