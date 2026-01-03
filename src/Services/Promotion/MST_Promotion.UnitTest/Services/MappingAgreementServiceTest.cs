using System;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MST_Promotion.Services;
using AutoFixture;
using Xunit;
using Base.DTOs;
using Database.Models.PRM;
using CustomAutoFixture;

namespace MST_Promotion.UnitTests
{
    public class MappingAgreementServiceTest : BaseServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;

        [Fact]
        public async Task GetMappingAgreementsDataFromExcelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                using (var db = factory.CreateContext())
                {

                    var dbQuery = factory.CreateDbQueryContext();
                    MappingAgreementService service = new MappingAgreementService(db);
                    FileDTO fileInput = new FileDTO()
                    {
                        Url = "http://192.168.2.29:9001/xunit-tests/Export_MappingAgreement.xlsx"
                    };
                    var results = await service.GetMappingAgreementsDataFromExcelAsync(fileInput);
                    Assert.NotNull(results);
                }
            }
        }

        [Fact]
        public async Task ExportMappingAgreementsAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                using (var db = factory.CreateContext())
                {
                    var dbQuery = factory.CreateDbQueryContext();
                    MappingAgreementService service = new MappingAgreementService(db);
                    var results = await service.ExportMappingAgreementsAsync(null, null);
                    Assert.NotNull(results);
                }
            }
        }


        [Fact]
        public async Task ConfirmImportMappingAgreementsAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var data = FixtureFactory.Get().Build<MappingAgreement>()
                                          .With(o => o.IsDeleted, false)
                                          .Create();
                        await db.MappingAgreements.AddAsync(data);
                        await db.SaveChangesAsync();
                        var dbQuery = factory.CreateDbQueryContext();
                        var service = new MappingAgreementService(db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/Export_MappingAgreement.xlsx"
                        };
                        var results = await service.GetMappingAgreementsDataFromExcelAsync(fileInput);
                        var resultsComfirm = await service.ConfirmImportMappingAgreementsAsync(results);
                        Assert.NotNull(results);
                        Assert.NotNull(resultsComfirm);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
