using AutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using MST_Titledeeds.Params.Filters;
using MST_Titledeeds.Params.Inputs;
using MST_Titledeeds.Services;
using OfficeOpenXml;
using CustomAutoFixture;

namespace MST_Titledeeds.UnitTests
{
    public class TitleDeedServiceTest
    {
        IConfiguration Configuration;
        private static readonly Fixture Fixture = new Fixture();

        public TitleDeedServiceTest()
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
        public async Task GetTitleDeedListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new TitleDeedService(Configuration, db);

                        var titleDeed = await db.Projects.FirstOrDefaultAsync(f => f.ProjectNo == "10019");

                        TitleDeedFilter filter = FixtureFactory.Get().Build<TitleDeedFilter>().Create();
                        PageParam pageParam = new PageParam();
                        TitleDeedListSortByParam sortByParam = new TitleDeedListSortByParam();

                        var results = await service.GetTitleDeedListAsync(titleDeed.ID, filter, pageParam, sortByParam);

                        filter = new TitleDeedFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(TitleDeedListSortBy)).Cast<TitleDeedListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new TitleDeedListSortByParam() { SortBy = item };
                            results = await service.GetTitleDeedListAsync(titleDeed.ID, filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.TitleDeeds);
                        }
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetTitleDeedStatusSelectAllListAsync()
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

                        var service = new TitleDeedService(Configuration, db);
                        var titleDeed = await db.TitledeedDetails.FirstOrDefaultAsync();

                        TitleDeedFilter filter = FixtureFactory.Get().Build<TitleDeedFilter>().Create();
                        PageParam pageParam = new PageParam();
                        TitleDeedListSortByParam sortByParam = new TitleDeedListSortByParam();

                        var results = await service.GetTitleDeedStatusSelectAllListAsync(titleDeed.ID, filter, pageParam, sortByParam);
                        Assert.NotNull(results.TitleDeeds);

                        filter = new TitleDeedFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(TitleDeedListSortBy)).Cast<TitleDeedListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new TitleDeedListSortByParam() { SortBy = item };
                            results = await service.GetTitleDeedStatusSelectAllListAsync(titleDeed.ID, filter, pageParam, sortByParam);
                            Assert.NotNull(results.TitleDeeds);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetTitleDeedAsync()
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

                        var service = new TitleDeedService(Configuration, db);

                        var titleDeed = await db.TitledeedDetails.FirstOrDefaultAsync();

                        var results = await service.GetTitleDeedAsync(titleDeed.ID);
                        Assert.NotNull(results);


                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task UpdateTitleDeedStatusAsync()
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

                        var service = new TitleDeedService(Configuration, db);
                        var TitledeedDetail = await db.TitledeedDetails.FirstOrDefaultAsync();
                        var model = TitleDeedDTO.CreateFromModel(TitledeedDetail);
                        DateTime now = DateTime.Now;
                        model.LandStatusDate = now;
                        model.LandStatusNote = "Unit";
                        var resultUpdate = await service.UpdateTitleDeedStatusAsync(TitledeedDetail.ID, model);

                        Assert.NotNull(resultUpdate);
                        Assert.Equal(resultUpdate.LandStatusDate, now);
                        Assert.Equal("Unit", resultUpdate.LandStatusNote);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task UpdateTitleDeedListStatusAsync()
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

                        var service = new TitleDeedService(Configuration, db);
                        var TitledeedDetail = await db.TitledeedDetails.FirstOrDefaultAsync();
                        var model = TitleDeedDTO.CreateFromModel(TitledeedDetail);
                        DateTime now = DateTime.Now;
                        model.LandStatusDate = now;
                        model.LandStatusNote = "Unit";
                        try
                        {
                            var resultUpdate = await service.UpdateTitleDeedListStatusAsync(TitledeedDetail.ID, new List<TitleDeedDTO> { model });
                            Assert.True(true);
                        }
                        catch
                        {
                            Assert.True(false);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetTitleDeedHistoryItemsAsync()
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

                        var service = new TitleDeedService(Configuration, db);

                        var TitledeedDetail = await db.TitledeedDetails.FirstOrDefaultAsync();
                        var results = await service.GetTitleDeedHistoryItemsAsync(TitledeedDetail.ID);
                        Assert.NotNull(results);


                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task ExportDebtFreePrintFormUrlAsync()
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
                        var service = new TitleDeedService(Configuration, db);


                        var user = await db.Users.FirstOrDefaultAsync();
                        var project = await db.Projects.FirstOrDefaultAsync();
                        var model = await db.Units.FirstOrDefaultAsync(f => f.ProjectID == project.ID);


                        var result = await service.ExportDebtFreePrintFormUrlAsync(new TitleDeedReportDTO
                        {
                            ProjectID = [model.ProjectID.ToString()],
                            ProjectNo = project.ProjectNo,
                            UnitNo = [model.UnitNo],
                            DateStart = DateTime.Now
                        }, user.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
