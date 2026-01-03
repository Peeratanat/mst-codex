using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models.FIN;
using Database.Models.MST;
using Database.UnitTestExtensions;
using MST_Finacc.Params.Filters;
using MST_Finacc.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Xunit;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;

namespace MST_Finacc.UnitTests
{
    public class EDCsServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        public EDCsServiceTest()
        {
            Environment.SetEnvironmentVariable("minio_AccessKey", "XNTYE7HIMF6KK4BVEIXA");
            Environment.SetEnvironmentVariable("minio_DefaultBucket", "master-data");
            Environment.SetEnvironmentVariable("minio_PublicURL", "192.168.2.29:30050");
            Environment.SetEnvironmentVariable("minio_Endpoint", "192.168.2.29:9001");
            Environment.SetEnvironmentVariable("minio_SecretKey", "naD+esQ+uV7+xwfF3bPfAn5iC7C1XUyXeM8HkBlO");
            Environment.SetEnvironmentVariable("minio_TempBucket", "temp");
            Environment.SetEnvironmentVariable("minio_WithSSL", "false");
            Environment.SetEnvironmentVariable("report_SecretKey", "nIcHeoYiMNZiJMYz");
            Environment.SetEnvironmentVariable("report_Url", "http://192.168.4.160/rptcrmrevo/printform.aspx");


            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async Task GetEDCDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EDCService(db, Configuration);

                        var projectID = await db.Projects.Select(o => o.ID).FirstAsync();
                        var results = await service.GetEDCDropdownListUrlAsync(projectID, "ก");
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task GetEDCListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EDCService(db, Configuration);

                        EDCFilter filter = FixtureFactory.Get().Build<EDCFilter>().Create();
                        PageParam pageParam = new PageParam();
                        EDCSortByParam sortByParam = new EDCSortByParam();
                        filter.ProjectStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "ProjectStatus")
                                                                         .Select(x => x.Key).FirstAsync();
                        filter.CardMachineStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "CardMachineStatus")
                                                                         .Select(x => x.Key).FirstAsync();
                        filter.CardMachineTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "CardMachineType")
                                                                         .Select(x => x.Key).FirstAsync();
                        var results = await service.GetEDCListAsync(filter, pageParam, sortByParam);
                        filter = new EDCFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(EDCSortBy)).Cast<EDCSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new EDCSortByParam() { SortBy = item };
                            results = await service.GetEDCListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.EDCs);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetEDCDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EDCService(db, Configuration);

                        var bank = await db.Banks.FirstAsync();
                        var cardMachineType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineType").FirstAsync();
                        var cardMachineStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineStatus").FirstAsync();
                        var project = await db.Projects.FirstAsync();

                        //Put unit test here
                        var data = new EDCDTO()
                        {
                            Code = "00002",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CardMachineType = MasterCenterDropdownDTO.CreateFromModel(cardMachineType),
                            CardMachineStatus = MasterCenterDropdownDTO.CreateFromModel(cardMachineStatus),
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            TelNo = "2222",
                            Remark = "Test",
                            ReceiveBy = "Test",
                            ReceiveDate = new DateTime(),
                        };

                        var resultCreate = await service.CreateEDCAsync(data);

                        var result = await service.GetEDCDetailAsync(resultCreate.Id.Value);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateEDCAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var bank = await db.Banks.FirstAsync();
                        var cardMachineType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineType").FirstAsync();
                        var cardMachineStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineStatus").FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var service = new EDCService(db, Configuration);

                        //Put unit test here
                        var data = new EDCDTO()
                        {
                            Code = "00002",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CardMachineType = MasterCenterDropdownDTO.CreateFromModel(cardMachineType),
                            CardMachineStatus = MasterCenterDropdownDTO.CreateFromModel(cardMachineStatus),
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            TelNo = "2222",
                            Remark = "Test",
                            ReceiveBy = "Test",
                            ReceiveDate = new DateTime(),
                        };

                        var result = await service.CreateEDCAsync(data);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateEDCAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var bank = await db.Banks.FirstAsync();
                        var cardMachineType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineType").FirstAsync();
                        var cardMachineStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineStatus").FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var service = new EDCService(db, Configuration);

                        //Put unit test here
                        var data = new EDCDTO()
                        {
                            Code = "00002",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CardMachineType = MasterCenterDropdownDTO.CreateFromModel(cardMachineType),
                            CardMachineStatus = MasterCenterDropdownDTO.CreateFromModel(cardMachineStatus),
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            TelNo = "2222",
                            Remark = "Test",
                            ReceiveBy = "Test",
                            ReceiveDate = new DateTime(),
                        };

                        var result = await service.CreateEDCAsync(data);
                        result.Remark = "TestUpdate";
                        var resultupdate = await service.UpdateEDCAsync(result.Id.Value, result);
                        Assert.NotNull(resultupdate);
                        Assert.Equal("TestUpdate", resultupdate.Remark);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteEDCAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var bank = await db.Banks.FirstAsync();
                        var cardMachineType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineType").FirstAsync();
                        var cardMachineStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineStatus").FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var service = new EDCService(db, Configuration);

                        //Put unit test here
                        var data = new EDCDTO()
                        {
                            Code = "00002",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CardMachineType = MasterCenterDropdownDTO.CreateFromModel(cardMachineType),
                            CardMachineStatus = MasterCenterDropdownDTO.CreateFromModel(cardMachineStatus),
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            TelNo = "2222",
                            Remark = "Test",
                            ReceiveBy = "Test",
                            ReceiveDate = new DateTime(),
                        };

                        var resultCreate = await service.CreateEDCAsync(data);
                        await service.DeleteEDCAsync(resultCreate.Id.Value);
                        var afterDelete = db.EDCs.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetEDCBankListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EDCService(db, Configuration);

                        EDCBankFilter filter = FixtureFactory.Get().Build<EDCBankFilter>().Create();
                        PageParam pageParam = new PageParam();
                        EDCBankSortByParam sortByParam = new EDCBankSortByParam();

                        var results = await service.GetEDCBankListAsync(filter, pageParam, sortByParam);

                        filter = new EDCBankFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(EDCBankSortBy)).Cast<EDCBankSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new EDCBankSortByParam() { SortBy = item };
                            results = await service.GetEDCBankListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.EDCBanks);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetEDCFeeListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EDCService(db, Configuration);

                        EDCFeeFilter filter = FixtureFactory.Get().Build<EDCFeeFilter>().Create();
                        PageParam pageParam = new PageParam();
                        EDCFeeSortByParam sortByParam = new EDCFeeSortByParam();

                        var results = await service.GetEDCFeeListAsync(filter, pageParam, sortByParam);

                        filter = new EDCFeeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(EDCFeeSortBy)).Cast<EDCFeeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new EDCFeeSortByParam() { SortBy = item };
                            results = await service.GetEDCFeeListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.EDCFees);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateEDCFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EDCService(db, Configuration);
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").FirstAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").FirstAsync();
                        var bank = await db.Banks.FirstAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").FirstAsync();

                        //Put unit test here
                        var input = new EDCFeeDTO
                        {
                            Fee = 50,
                            PaymentCardType = MasterCenterDropdownDTO.CreateFromModel(paymentCardType),
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CreditCardType = MasterCenterDropdownDTO.CreateFromModel(creditCardType),
                            CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType)
                        };

                        var result = await service.CreateEDCFeeAsync(input);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateEDCFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EDCService(db, Configuration);
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").FirstAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").FirstAsync();
                        var bank = await db.Banks.FirstAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").FirstAsync();

                        //Put unit test here
                        var input = new EDCFeeDTO
                        {
                            Fee = 50,
                            PaymentCardType = MasterCenterDropdownDTO.CreateFromModel(paymentCardType),
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CreditCardType = MasterCenterDropdownDTO.CreateFromModel(creditCardType),
                            CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType)
                        };

                        var resultCreate = await service.CreateEDCFeeAsync(input);

                        resultCreate.Fee = 50;
                        var result = await service.UpdateEDCFeeAsync(resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteEDCFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EDCService(db, Configuration);
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").FirstAsync();
                        var bank = await db.Banks.FirstAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").FirstAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").FirstAsync();

                        //Put unit test here
                        var input = new EDCFeeDTO
                        {
                            Fee = 50,
                            PaymentCardType = MasterCenterDropdownDTO.CreateFromModel(paymentCardType),
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CreditCardType = MasterCenterDropdownDTO.CreateFromModel(creditCardType),
                            CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType)
                        };

                        var resultCreate = await service.CreateEDCFeeAsync(input);
                        //Put unit test here
                        await service.DeleteEDCFeeAsync(resultCreate.Id.Value);
                        var afterDelete = db.EDCFees.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task ExportEDCListUrlAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EDCService(db, Configuration);

                        EDCFilter filter = FixtureFactory.Get().Build<EDCFilter>().Create();
                        filter.ProjectStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "ProjectStatus")
                                                                         .Select(x => x.Key).FirstAsync();
                        filter.CardMachineStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "CardMachineStatus")
                                                                         .Select(x => x.Key).FirstAsync();
                        filter.CardMachineTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "CardMachineType")
                                                                         .Select(x => x.Key).FirstAsync();
                        var result = await service.ExportEDCListUrlAsync(filter, Report.Integration.ShowAs.PDF);
                        filter = new EDCFilter();
                        result = await service.ExportEDCListUrlAsync(filter, Report.Integration.ShowAs.Excel);
                        Assert.NotNull(result.URL);

                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task GetFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var edc = await db.EDCFees.FirstAsync();
                        var edcId = await db.EDCs.FirstAsync();

                        decimal paidAmount = 500000;
                        var service = new EDCService(db, Configuration);

                        var result = await service.GetFeeAsync(edcId.ID
                        , (Guid)edc.BankID
                        , (Guid)edc.CreditCardTypeMasterCenterID
                        , (Guid)edc.CreditCardPaymentTypeMasterCenterID
                        , (Guid)edc.PaymentCardTypeMasterCenterID
                        , paidAmount);

                        Assert.True(result > 0);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetFeePercentAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new EDCService(db, Configuration);

                        //Put unit test here
                        var edc = await db.EDCFees.FirstAsync();
                        var edcId = await db.EDCs.FirstAsync();

                        decimal paidAmount = 500000;

                        var result = await service.GetFeePercentAsync(edcId.ID
                        , (Guid)edc.BankID
                        , (Guid)edc.CreditCardTypeMasterCenterID
                        , (Guid)edc.CreditCardPaymentTypeMasterCenterID
                        , (Guid)edc.PaymentCardTypeMasterCenterID);
                        Assert.True(result > 0);



                        await tran.RollbackAsync();
                    }
                });
            }
        }


    }
}
