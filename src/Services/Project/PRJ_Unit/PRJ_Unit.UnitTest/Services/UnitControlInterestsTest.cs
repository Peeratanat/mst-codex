using AutoFixture;
using Base.DTOs.PRJ;
using CustomAutoFixture;
using Database.Models.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.UnitTests
{
    public class UnitControlInterestsTest
    {
        private static readonly Fixture Fixture = new Fixture();

        public UnitControlInterestsTest()
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

        }
        [Fact]

        public async Task GetUnitControlInterestAsync()
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

                        var service = new UnitControlService(db);
                        UnitControlInterestFilter filter = FixtureFactory.Get().Build<UnitControlInterestFilter>().Create();
                        var unit = await db.Units
                                        .Include(o => o.Model)
                                        .Include(o => o.UnitStatus)
                                        .Include(o => o.Model.TypeOfRealEstate)
                                        .Include(o => o.Tower).FirstAsync();
                        PageParam pageParam = new PageParam();
                        UnitControlInterestSortByParam sortByParam = new UnitControlInterestSortByParam();
                        var results = await service.GetUnitControlInterestAsync((Guid)unit.ProjectID, filter, pageParam, sortByParam);

                        filter = new UnitControlInterestFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(UnitControlInterestSortByParamSortBy)).Cast<UnitControlInterestSortByParamSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new UnitControlInterestSortByParam() { SortBy = item };
                            results = await service.GetUnitControlInterestAsync((Guid)unit.ProjectID, filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.unitInterests);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }



        [Fact]
        public async Task UpsertUnitControlInterestAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var dateNow = DateTime.Now;
                        var service = new UnitControlService(db);

                        var unit = await db.Units.FirstAsync();
                        var input = new UnitControlInterestDTO()
                        {
                            ProjectID = unit.ProjectID,
                            UnitID = unit.ID,
                            EffectiveDate = DateTime.Now,
                            ExpiredDate = DateTime.Now,
                            Id = Guid.NewGuid(),
                            Remark = "Test",
                        };
                        var resultAdd = await service.AddUnitControlInterestAsync(input);
                        Assert.NotNull(resultAdd);

                        var model = await db.UnitControlInterests.FirstAsync();
                        var result = new UnitControlInterestDTO()
                        {
                            ProjectID = model.ProjectID,
                            UnitID = model.UnitID,
                            EffectiveDate = dateNow,
                            ExpiredDate = model.ExpiredDate,
                            Id = model.ID,
                            Remark = model.Remark,
                            InterestCounter = model.InterestCounter
                        };
                        var resultU = await service.UpdateUnitControlInterestAsync(result);
                        Assert.NotNull(resultU);
                        Assert.Equal(dateNow, resultU.EffectiveDate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteUnitControlInterestAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UnitControlService(db);
                        var unit = await db.Units.FirstAsync();
                        var input = new UnitControlInterestDTO()
                        {
                            ProjectID = unit.ProjectID,
                            UnitID = unit.ID,
                            EffectiveDate = DateTime.Now,
                            ExpiredDate = DateTime.Now,
                            Id = Guid.NewGuid(),
                            Remark = "Test",
                        };
                        var resultAdd = await service.AddUnitControlInterestAsync(input);
                        Assert.NotNull(resultAdd);
                        var unitContorller = await db.UnitControlInterests.FirstAsync();


                        await service.DeleteUnitControlInterestAsync(unitContorller.ID);
                        bool afterDelete = db.UnitControlInterests.Any(o => o.ID == unitContorller.ID && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
