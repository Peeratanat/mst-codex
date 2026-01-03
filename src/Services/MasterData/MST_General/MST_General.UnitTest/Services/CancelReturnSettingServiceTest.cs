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
    public class CancelReturnSettingServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetCancelReturnSettingAsync()
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

                        var service = new CancelReturnSettingService(db);
                        var result = await service.GetCancelReturnSettingAsync();
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateCancelReturnSettingAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new CancelReturnSettingService(db);
                        var result = await service.GetCancelReturnSettingAsync();
                        result.HandlingFee = 66;
                        var resultUpdate = await service.UpdateCancelReturnSettingAsync(result.Id.Value, result);
                        Assert.NotNull(resultUpdate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
