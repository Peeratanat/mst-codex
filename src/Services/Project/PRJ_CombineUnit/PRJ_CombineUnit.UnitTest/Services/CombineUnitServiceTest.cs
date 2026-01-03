using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using PRJ_CombineUnit.Params.Filters;
using PRJ_CombineUnit.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Base.DTOs.PRJ;
using Database.Models.MasterKeys;

namespace PRJ_CombineUnit.UnitTests
{
    public class CombineUnitServiceTest
    {
        public CombineUnitServiceTest()
        {
            Environment.SetEnvironmentVariable("minio_AccessKey", "XNTYE7HIMF6KK4BVEIXA");
            Environment.SetEnvironmentVariable("minio_DefaultBucket", "master-data");
            Environment.SetEnvironmentVariable("minio_PublicURL", "192.168.2.29:30050");
            Environment.SetEnvironmentVariable("minio_Endpoint", "192.168.2.29:9001");
            Environment.SetEnvironmentVariable("minio_SecretKey", "naD+esQ+uV7+xwfF3bPfAn5iC7C1XUyXeM8HkBlO");
            Environment.SetEnvironmentVariable("minio_TempBucket", "temp");
            Environment.SetEnvironmentVariable("minio_WithSSL", "false");


            Environment.SetEnvironmentVariable("email_isTest", "true");
            Environment.SetEnvironmentVariable("email_Test_mailTo", "kanokporn_t@apthai.com");
            Environment.SetEnvironmentVariable("email_HostEmail", "http://mailapi-mailapi.apps.dev.apthai.com/api/mail/SendMail");
            Environment.SetEnvironmentVariable("email_mailFromName", "CRM Consult");
            Environment.SetEnvironmentVariable("email_mailFrom", "crmsale@apthai.com");
            Environment.SetEnvironmentVariable("apmailapi_masterdataurl", "http://master-data-app-crmrevo-dev.apps.dev.apthai.com");
            Environment.SetEnvironmentVariable("email_mailType", "CRM_Email");
            Environment.SetEnvironmentVariable("email_cc", "kanokporn_t@apthai.com");
        }
        [Fact]
        public async Task GetCombineUnitList()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new CombineUnitService(db);
                        CombineUnitFilter filter = FixtureFactory.Get().Build<CombineUnitFilter>().Create();
                        PageParam pageParam = new PageParam();
                        CombineUnitSortByParam sortByParam = new CombineUnitSortByParam();

                        var results = await service.GetCombineUnitList(filter, pageParam, sortByParam);

                        filter = new CombineUnitFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CombineUnitSortBy)).Cast<CombineUnitSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CombineUnitSortByParam() { SortBy = item };
                            results = await service.GetCombineUnitList(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.CombineUnit);
                        }


                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task CreateCombineUnitAsync()
        {
            // using var factory = new UnitTestDbContextFactory();
            // var db = factory.CreateContext();
            // 
            // var strategy = db.Database.CreateExecutionStrategy();
            // await strategy.ExecuteAsync(async () =>
            // {
            //     using var tran = await db.Database.BeginTransactionAsync();

            //     var project = await db.Projects.FirstOrDefaultAsync();
            //     DateTime now = DateTime.Now;
            //     CombineUnitDTO input = FixtureFactory.Get().Build<CombineUnitDTO>().Create();
            //     var service = new CombineUnitService(db);
            //     var result = await service.CreateCombineUnitAsync(new List<CombineUnitDTO> { input });
            //     Assert.NotNull(result);

            //     await tran.RollbackAsync();
            // });
        }
        [Fact]
        public async Task GetUnitDropdownCanCombineAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.CombineUnits.AsNoTracking().FirstAsync();
                        var service = new CombineUnitService(db);

                        var result = await service.GetUnitDropdownCanCombineAsync(new CombineUnitDDLDTO
                        {
                            ProjectID = model.ProjectID,
                            txt = null
                        });
                        Assert.NotEmpty(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateAndApproveCombineUnitAsync()
        {
            // using var factory = new UnitTestDbContextFactory();
            // var db = factory.CreateContext();
            // 
            // var strategy = db.Database.CreateExecutionStrategy();
            // await strategy.ExecuteAsync(async () =>
            // {
            //     using var tran = await db.Database.BeginTransactionAsync();

            //     DateTime now = DateTime.Now;
            //     CombineUnitDTO input = FixtureFactory.Get().Build<CombineUnitDTO>().Create();
            //     var service = new CombineUnitService(db);
            //     var CombineStatus = await db.MasterCenters.FirstOrDefaultAsync(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.CombineStatus) &&
            //     x.Key != MasterCombineStatusKeys.WaitApprove);
            //     var UnitCombines = await db.CombineUnits.Include(x => x.CombineStatus).Include(x => x.Unit).Include(x => x.Project).Include(x => x.UnitCombine).Include(x => x.CombineDocType)
            //     .FirstOrDefaultAsync(f => f.CombineStatusMasterCenterID == CombineStatus.ID);

            //     input.Project = ProjectDropdownDTO.CreateFromModel(UnitCombines.Project);
            //     input.Unit = UnitDropdownDTO.CreateFromModel(UnitCombines.Unit);
            //     input.UnitCombine = UnitDropdownDTO.CreateFromModel(UnitCombines.Unit);
            //     input.CombineDocType = MasterCenterDropdownDTO.CreateFromModel(UnitCombines.CombineDocType);
            //     input.CombineStatus = MasterCenterDropdownDTO.CreateFromModel(UnitCombines.CombineStatus);


            //     var result = await service.CreateAndApproveCombineUnitAsync(new List<CombineUnitDTO> { input });
            //     Assert.NotNull(result);

            //     await tran.RollbackAsync();
            // });
        }
        [Fact]
        public async Task DeleteCombineAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();

            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var service = new CombineUnitService(db);
                var CombineStatus = await db.MasterCenters.FirstOrDefaultAsync(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.CombineStatus) &&
                x.Key != MasterCombineStatusKeys.WaitApprove);
                var CombineUnit = db.CombineUnits.Include(x => x.CombineStatus).Include(x => x.Unit).Include(x => x.UnitCombine).Include(x => x.CombineDocType)
                .FirstOrDefault(f => f.CombineStatusMasterCenterID == CombineStatus.ID);

                bool beforeDelete = db.CombineUnits.Any(o => o.ID == CombineUnit.ID && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteCombineAsync(new CombineUnitDTO
                {
                    Id = CombineUnit.ID,
                });
                var afterDelete = await db.CombineUnits.FirstOrDefaultAsync(o => o.ID == CombineUnit.ID);
                Assert.Equal(afterDelete.ApprovedDate, null);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetCombineHistoryList()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var CombineUnit = await db.CombineUnits.FirstAsync();
                        var service = new CombineUnitService(db);

                        PageParam pageParam = new PageParam() { Page = 1, PageSize = 10 };
                        var result = await service.GetCombineHistoryList(CombineUnit.ID, pageParam);
                        Assert.NotEmpty(result.CombineUnit);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetProjectDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var CombineUnit = await db.CombineUnits.FirstAsync();
                        var service = new CombineUnitService(db);

                        PageParam pageParam = new PageParam() { Page = 1, PageSize = 10 };
                        var result = await service.GetProjectDropdownListAsync(null, null, true, null, null);
                        Assert.NotEmpty(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
