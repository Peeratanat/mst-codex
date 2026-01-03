using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using MST_General.Params.Filters;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using MST_General.Services;

namespace MST_General.UnitTests
{
    public class BOConfigurationServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetBOConfigurationAsync()
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

                        var service = new BOConfigurationService(db);
                        var result = await service.GetBOConfigurationAsync();
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateBOConfigurationAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new BOConfigurationService(db);
                        var result = await service.GetBOConfigurationAsync();
                        result.Vat = 1234;
                        result.BOIAmount = 1234;
                        result.IncomeTaxPercent = 1234;
                        result.BusinessTaxPercent = 1234;
                        result.LocalTaxPercent = 1234;
                        result.UnitTransferFee = 1234;
                        result.AdjustAccount = 1234;
                        result.TaxAccount = 1234;
                        result.DepreciationYear = 1234;
                        result.VoidRefund = 1234;
                        result.TransferFeeRate = 1234;

                        var resultUpdate = await service.UpdateBOConfigurationAsync(result.Id.Value, result);
                        Assert.Equal(resultUpdate.Vat, 1234);
                        Assert.Equal(resultUpdate.BOIAmount, 1234);
                        Assert.Equal(resultUpdate.IncomeTaxPercent, 1234);
                        Assert.Equal(resultUpdate.BusinessTaxPercent, 1234);
                        Assert.Equal(resultUpdate.LocalTaxPercent, 1234);
                        Assert.Equal(resultUpdate.UnitTransferFee, 1234);
                        Assert.Equal(resultUpdate.AdjustAccount, 1234);
                        Assert.Equal(resultUpdate.TaxAccount, 1234);
                        Assert.Equal(resultUpdate.DepreciationYear, 1234);
                        Assert.Equal(resultUpdate.VoidRefund, 1234);
                        Assert.Equal(resultUpdate.TransferFeeRate, 1234);


                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
