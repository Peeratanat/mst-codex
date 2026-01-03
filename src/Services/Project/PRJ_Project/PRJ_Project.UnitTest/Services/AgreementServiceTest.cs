using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PRJ_Project.Services;


namespace PRJ_Project.UnitTests
{
    public class AgreementServiceTest
    {
        [Fact]
        public async Task GetAgreementAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Projects.FirstAsync();
                        var service = new AgreementService(db);

                        var result = await service.GetAgreementAsync(model.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task UpdateProjectAddressAsync()
        {

            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var serviceAgreement = new AgreementService(db);
                var service = new AgreementService(db);
                var model = await db.Projects.FirstAsync();

                var getAgreement = await serviceAgreement.GetAgreementAsync(model.ID);

                getAgreement.AttorneyNameTH1 = "เทส";
                getAgreement.AttorneyNameEN1 = "UnitTest001";
                getAgreement.PreferApproveName = "UnitTest001";

                var resultUpdateAgreement = await serviceAgreement.UpdateAgreementAsync(model.ID, getAgreement.Id.Value, getAgreement);
                Assert.NotNull(resultUpdateAgreement);
                Assert.Equal("เทส", resultUpdateAgreement.AttorneyNameTH1);
                Assert.Equal("UnitTest001", resultUpdateAgreement.AttorneyNameEN1);
                Assert.Equal("UnitTest001", resultUpdateAgreement.PreferApproveName);

                await tran.RollbackAsync();
            });
        }


    }
}
