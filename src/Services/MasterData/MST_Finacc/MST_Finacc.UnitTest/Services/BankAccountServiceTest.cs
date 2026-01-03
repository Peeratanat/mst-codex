using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using Database.UnitTestExtensions;
using MST_Finacc.Params.Filters;
using MST_Finacc.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ErrorHandling;
using Database.Models;
using MST_Finacc.Params.Filters;

namespace MST_Finacc.UnitTests
{
    public class BankAccountServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetBankAccountDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BankAccountService(db);
                        var sortByParam = new BankAccountDropdownSortByParam();
                        var results = await service.GetBankAccountDropdownListAsync(string.Empty,
                          string.Empty,
                        null,
                        null,
                        string.Empty,
                        null,
                        sortByParam);
                        Assert.NotEmpty(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetBankAccountListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BankAccountService(db);

                        //Put unit test here

                        BankAccountFilter filter = FixtureFactory.Get().Build<BankAccountFilter>().Create();
                        PageParam pageParam = new PageParam();
                        BankAccountSortByParam sortByParam = new BankAccountSortByParam();
                        filter.BankAccountTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "BankAccountType")
                                                                          .Select(x => x.Key).FirstAsync();
                        filter.GLAccountTypeKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "GLAccountType").Select(o => o.Key).FirstAsync();
                        var results = await service.GetBankAccountListAsync(filter, pageParam, sortByParam);

                        filter = new BankAccountFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(BankAccountSortBy)).Cast<BankAccountSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new BankAccountSortByParam() { SortBy = item };
                            results = await service.GetBankAccountListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.BankAccounts);
                        }


                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetBankAccountDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BankAccountService(db);

                        //Put unit test here
                        var data = FixtureFactory.Get().Build<BankAccount>()
                                   .With(o => o.IsDeleted, false)
                                   .Create();
                        await db.BankAccounts.AddAsync(data);
                        await db.SaveChangesAsync();

                        var result = await service.GetBankAccountDetailAsync(data.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateBankAccountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BankAccountService(db);

                        var bankbranch = await db.BankBranches.FirstAsync();
                        var bank = await db.Banks.Where(o => o.ID == bankbranch.BankID).FirstAsync();
                        var province = await db.Provinces.FirstAsync();
                        var bankAccountType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "BankAccountType").FirstAsync();



                        var company = await (from t1 in db.Companies
                                             join t2 in db.BankAccounts on t1.ID equals t2.CompanyID into t2Group
                                             from t2 in t2Group.DefaultIfEmpty()
                                             where t2 == null
                                             select t1).FirstOrDefaultAsync();



                        BankAccountDTO input = new BankAccountDTO
                        {
                            GLAccountNo = "222222",
                            DisplayName = "111111",
                            PCardGLAccountNo = "111111",
                            BankAccountNo = "111111",
                            Province = ProvinceListDTO.CreateFromModel(province),
                            Company = CompanyDropdownDTO.CreateFromModel(company),
                            BankBranch = BankBranchDropdownDTO.CreateFromModel(bankbranch),
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccountType),
                            MerchantID = "11111",
                            ServiceCode = "22222",
                            IsPCard = true
                        };

                        var result = await service.CreateBankAccountAsync(input);
                        Assert.NotNull(result);


                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateChartOfAccountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BankAccountService(db);

                        var GLAccountType = await (from t1 in db.GLAccountTypes
                                                   join t2 in db.BankAccounts on t1.ID equals t2.GLAccountTypeID into t2Group
                                                   from t2 in t2Group.DefaultIfEmpty()
                                                   where t2 == null
                                                   select t1).FirstOrDefaultAsync();

                        BankAccountDTO input = new BankAccountDTO
                        {
                            GLAccountNo = "222222",
                            BankAccountNo = "111111",
                            GLAccountType = GLAccountTypeDTO.CreateFromModel(GLAccountType),
                            Name = "Testtt"
                        };

                        var result = await service.CreateChartOfAccountAsync(input);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateBankAccountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BankAccountService(db);

                        //Put unit test here
                        var glAccountType = await db.GLAccountTypes.FirstAsync();
                        var company = await db.Companies.FirstAsync();
                        var bankbranch = await db.BankBranches.FirstAsync();
                        var bank = await db.Banks.Where(o => o.ID == bankbranch.BankID).FirstAsync();
                        var province = await db.Provinces.FirstAsync();
                        var bankAccountType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "BankAccountType").FirstAsync();

                        BankAccountDTO input = new BankAccountDTO();
                        input.GLAccountNo = "0123456789";
                        input.BankAccountNo = "1234567890";
                        input.Province = ProvinceListDTO.CreateFromModel(province);
                        input.Company = CompanyDropdownDTO.CreateFromModel(company);
                        input.BankBranch = BankBranchDropdownDTO.CreateFromModel(bankbranch);
                        input.Bank = BankDropdownDTO.CreateFromModel(bank);
                        input.BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccountType);
                        input.MerchantID = "100000";
                        input.ServiceCode = "10000";
                        input.IsPCard = false;
                        input.Name = "Testtt";
                        input.DisplayName = "PreFix";
                        input.GLAccountType = GLAccountTypeDTO.CreateFromModel(glAccountType);

                        var resultCreate = await service.CreateBankAccountAsync(input);
                        resultCreate.DisplayName = "Test";

                        var resultupdate = await service.UpdateBankAccountAsync(resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(resultupdate);
                        Assert.Equal("Test", resultupdate.DisplayName);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteBankAccountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BankAccountService(db);

                        //Put unit test here
                        var data = FixtureFactory.Get().Build<BankAccount>()
                                  .With(o => o.BankAccountNo, "B-0001")
                                  .With(o => o.IsDeleted, false)
                                  .Create();
                        await db.BankAccounts.AddAsync(data);
                        await db.SaveChangesAsync();
                        await service.DeleteBankAccountAsync(data.ID);
                        var afterDelete = db.BankBranches.Any(o => o.ID == data.ID && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteBankAccountListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BankAccountService(db);

                        //Put unit test here
                        List<BankAccount> bankAccounts = new List<BankAccount>()
                        {
                            FixtureFactory.Get().Build<BankAccount>()
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<BankAccount>()
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        await db.BankAccounts.AddRangeAsync(bankAccounts);
                        await db.SaveChangesAsync();

                        BankAccountFilter bankAccountFilter = new BankAccountFilter();
                        PageParam pagparam = new PageParam()
                        {
                            Page = 1,
                            PageSize = 1
                        };
                        BankAccountSortByParam sortByParam = new BankAccountSortByParam();

                        var results = await service.GetBankAccountListAsync(bankAccountFilter, pagparam, sortByParam);
                        await service.DeleteBankAccountListAsync(results.BankAccounts);
                        var afterDelete = db.BankBranches.Any(o => o.ID == results.BankAccounts.First().Id && o.IsDeleted == false);
                        Assert.False(afterDelete);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
