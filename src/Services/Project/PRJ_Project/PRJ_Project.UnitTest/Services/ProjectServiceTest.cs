using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Services;
using Report.Integration;

namespace PRJ_Project.UnitTests
{
    public class ProjectServiceTest
    {
        IConfiguration Configuration;
        public ProjectServiceTest()
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

            Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async Task GetProjectDataStatusAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var project = await db.Projects.FirstAsync();
                    var service = new ProjectService(Configuration, db);
                    var result = await service.GetProjectDataStatusAsync(project.ID);
                    Assert.NotNull(result);
                });
            }
        }


        [Fact]
        public async Task GetProjectListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var service = new ProjectService(Configuration, db);
                    ProjectsFilter filter = new();
                    PageParam pageParam = new()
                    {
                        Page = 1,
                        PageSize = 1
                    };
                    ProjectSortByParam param = new()
                    {
                        SortBy = ProjectSortBy.ProjectNo,
                        Ascending = false
                    };

                    var result = await service.GetProjectListAsync(filter, pageParam, param);
                    Assert.NotEmpty(result.Projects);
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
                    var project = await db.Projects.FirstAsync();
                    var service = new ProjectService(Configuration, db);
                    var result = await service.GetProjectAsync(project.ID);
                    Assert.NotNull(result);
                });
            }
        }

        [Fact]
        public async Task GetProjectCountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var service = new ProjectService(Configuration, db);
                    var result = await service.GetProjectCountAsync();
                    Assert.NotNull(result);
                });
            }
        }

        [Fact]
        public async Task CreateProjectAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var productType = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProductType");
                ProjectDTO input = new ProjectDTO()
                {
                    ProjectNo = "UnitTest",
                    SapCode = "UnitTest",
                    ProjectNameTH = "UnitTest001",
                    ProjectNameEN = "UnitTest001",
                    IsActive = true,
                    ProductType = MasterCenterDropdownDTO.CreateFromModel(productType)
                };

                var service = new ProjectService(Configuration, db);
                var result = await service.CreateProjectAsync(input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteProjectAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var Project = await db.Projects.FirstAsync(f => f.IsDeleted == false);

                var service = new ProjectService(Configuration, db);
                await service.DeleteProjectAsync(Project.ID, "unit test");
                bool afterDelete = db.SpecMaterialItems.Any(o => o.ID == Project.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task GetExportBookingTemplateUrlAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var model = await db.Projects.FirstAsync();
                    var service = new ProjectService(Configuration, db);
                    ReportResult result = await service.GetExportBookingTemplateUrlAsync(model.ID);
                    Assert.NotNull(result.URL);
                });
            }
        }
        [Fact]
        public async Task GetExportAgreementTemplateUrlAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var model = await db.Projects.FirstAsync();
                    var service = new ProjectService(Configuration, db);
                    ReportResult result = await service.GetExportAgreementTemplateUrlAsync(model.ID);
                    Assert.NotNull(result.URL);
                });
            }
        }

        [Fact]
        public async Task UpdateProjectStatus()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new ProjectService(Configuration, db);


                var projectStatusMasterCenterID = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus &&
                  o.Key == ProjectStatusKeys.InActive);

                var projectStatusMasterCenter = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus &&
                    o.Key == ProjectStatusKeys.Active);

                var model = await db.Projects.Include(o => o.ProjectStatus).FirstOrDefaultAsync(f => f.ProjectStatusMasterCenterID == projectStatusMasterCenter.ID);


                MasterCenterDropdownDTO projectStatus = MasterCenterDropdownDTO.CreateFromModel(projectStatusMasterCenterID);
                await service.UpdateProjectStatus(model.ID, projectStatus);

                var project = await db.Projects.FirstOrDefaultAsync(f => f.ID == model.ID);

                Assert.Equal(projectStatus.Id, project.ProjectStatusMasterCenterID);

                await tran.RollbackAsync();
            });
        }



        [Fact]
        public async Task UpdateProjectStatusM()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new ProjectService(Configuration, db);


                var projectStatusMasterCenterID = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus &&
                  o.Key == ProjectStatusKeys.InActive);

                var projectStatusMasterCenter = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus &&
                    o.Key == ProjectStatusKeys.Active);

                var model = await db.Projects.Include(o => o.ProjectStatus).FirstOrDefaultAsync(f => f.ProjectStatusMasterCenterID == projectStatusMasterCenter.ID);


                MasterCenterDropdownDTO projectStatus = MasterCenterDropdownDTO.CreateFromModel(projectStatusMasterCenterID);
                var result = await service.UpdateProjectStatusM(model.ProjectNo, projectStatus.Key);
                Assert.Equal(projectStatus.Id, result.ProjectStatus.Id);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetDefaultProjectAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var service = new ProjectService(Configuration, db);
                    var result = await service.GetDefaultProjectAsync();
                    Assert.NotNull(result);
                });
            }
        }
    }
}
