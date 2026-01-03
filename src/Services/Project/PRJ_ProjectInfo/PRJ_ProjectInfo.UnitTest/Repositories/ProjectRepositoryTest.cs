using Moq;
using Xunit;
using System.Threading.Tasks;
using PRJ_ProjectInfo.Services;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_ProjectInfo.Params.Outputs;
using static PRJ_ProjectInfo.Params.Outputs.ProjectInformationPaging;
using PRJ_ProjectInfo.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Database.Models.PRJ;
using CustomAutoFixture;
using AutoFixture;
namespace PRJ_ProjectInfo.Repository.UnitTests
{
    public class ProjectRepositoryTest
    {
        IConfiguration Configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProjectRepositoryTest()
        {

            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }



        [Fact]
        public async Task GetProjectInformationListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {

                        var service = new ProjectRepository(_hostingEnvironment, Configuration, db);
                         

                        ProjectInformationPaging.Filter filter = FixtureFactory.Get().Build<ProjectInformationPaging.Filter>().Create();
                        PageParam pageParam = new PageParam();
                        ProjectInformationPaging.SortByParam sortByParam = new ProjectInformationPaging.SortByParam();

                        var results = await service.GetProjectInformationListAsync( filter, sortByParam,pageParam);

                        filter = new ProjectInformationPaging.Filter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(SortBy)).Cast<SortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ProjectInformationPaging.SortByParam() { SortBy = item };
                            results = await service.GetProjectInformationListAsync( filter, sortByParam,pageParam);
                            Assert.NotEmpty(results.DataResult);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetProjectInfoAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var project = await db.ProjectInfos.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetProjectInfoAsync(project.ID, project.ProjectID);
                    Assert.NotEmpty(results);
                });
            }
        }


        [Fact]
        public async Task GetProjectInfoDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var project = await db.ProjectInfos.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetProjectInfoDetailAsync(project.ProjectID);
                    Assert.NotNull(results);
                });
            }
        }


        [Fact]
        public async Task GetProjectInfoLocationDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var project = await db.ProjectInfos.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetProjectInfoLocationDetailAsync(project.ProjectID);
                    Assert.NotNull(results);
                });
            }
        }

        [Fact]
        public async Task GetProjectInfoPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var project = await db.ProjectInfos.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetProjectInfoPromotionAsync(project.ProjectID);
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetProjectInfoCampaignAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var project = await db.ProjectInfos.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetProjectInfoCampaignAsync("special");
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetActiveMSTBrandAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var brand = await db.Brands.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetActiveMSTBrandAsync(brand.ID, "MIGRATE");
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetMasterCenterAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var master = await db.MasterCenters.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetMasterCenterAsync(master.ID, "เงินสด", "PaymentMethod", false);
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetProjectAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var master = await db.MasterCenters.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetProjectAsync();
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetProjectZoneAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var projectInfo = await db.ProjectInfoLocations.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetProjectZoneAsync(projectInfo.ID, "รถไฟฟ้า");
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetvwPMUserAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var projectInfo = await db.Projects.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetvwPMUserAsync(projectInfo.ID);
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetvwLCMUserDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var projectInfo = await db.Projects.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetvwLCMUserDetailAsync(projectInfo.ID);
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetvwLCUserDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var projectInfo = await db.Projects.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetvwLCUserDetailAsync(projectInfo.ID);
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetProjectInfoDetailDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var projectInfo = await db.Projects.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetProjectInfoDetailDataAsync(projectInfo.ID);
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetProjectInfoDestAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var projectInfo = await db.ProjectInfos.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetProjectInfoDestAsync(projectInfo.ID, projectInfo.ProjectID);
                    Assert.NotNull(results);
                });
            }
        }
        [Fact]
        public async Task GetProjectInfoBrandDDLAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var projectInfo = await db.ProjectInfos.FirstAsync();

                    var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                    var results = await service.GetProjectInfoBrandDDLAsync();
                    Assert.NotNull(results);
                });
            }
        }

        [Fact]
        public async Task UpdateProjecInfoAsync()
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

                        var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                        var projectInfos = await db.ProjectInfos.Where(w => w.ProjectType == "ทาวน์โฮม").Take(10).ToListAsync();
                        int count = 1;
                        projectInfos.ForEach(s =>
                        {
                            s.BrandName = string.Join("_", s.BrandName, count);
                            count++;
                        });
                        var result = await service.UpdateProjecInfoAsync(projectInfos);
                        Assert.True(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task UpdateProjecInfoDetailAsync()
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

                        var service = new ProjectRepository(_hostingEnvironment, Configuration, db);

                        var projectInfos = await db.ProjectInfoDetails.Take(10).ToListAsync();
                        int count = 1;
                        projectInfos.ForEach(s =>
                        {
                            s.AdminDescription = string.Join("_", s.AdminDescription, count);
                            count++;
                        });
                        var result = await service.UpdateProjecInfoDetailAsync(projectInfos);
                        Assert.True(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task InsertProjecInfoDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new ProjectRepository(_hostingEnvironment, Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync();
                        var projectInfos = new List<ProjectInfoDetail>
                                {
                                    new ProjectInfoDetail { ID = Guid.NewGuid(),ProjectID = project.ID, AdminDescription = "Mock Description 1" },
                                    new ProjectInfoDetail { ID = Guid.NewGuid(),ProjectID = project.ID, AdminDescription = "Mock Description 2" }
                                };
                        var result = await service.InsertProjecInfoDetailAsync(projectInfos);
                        Assert.True(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
