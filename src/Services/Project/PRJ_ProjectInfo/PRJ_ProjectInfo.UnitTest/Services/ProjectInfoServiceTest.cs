using Moq;
using Xunit;
using System.Threading.Tasks;
using PRJ_ProjectInfo.Services;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_ProjectInfo.Params.Outputs;
using static PRJ_ProjectInfo.Params.Outputs.ProjectInformationPaging;
using Database.Models.PRJ;
using Base.DTOs.MST;
namespace PRJ_ProjectInfo.Service.UnitTests
{
    public class ProjectInfoServiceTest
    {
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

                        var service = new ProjectInfoService(db);

                        var project = await db.Projects.FirstAsync();
                        var results = await service.GetProjectInfoAsync(project.ID);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }




        [Fact]
        public async Task TestProjectDataStatusSale()
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
                        var serviceInfo = new ProjectInfoService(db);
                        var project = await db.Projects.FirstAsync();
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();
                        var brand = await db.Brands.Where(o => !o.IsDeleted).FirstAsync();
                        var company = await db.Companies.Where(o => !o.IsDeleted).FirstAsync();
                        var bg = await db.BGs.Where(o => o.Name == "1").FirstAsync();
                        var subbg = await db.SubBGs.Where(o => o.BGID == bg.ID).FirstAsync();

                        var resultInfo = await serviceInfo.GetProjectInfoAsync(project.ID);
                        resultInfo.Brand = BrandDropdownDTO.CreateFromModel(brand);
                        resultInfo.Company = CompanyDropdownDTO.CreateFromModel(company);
                        resultInfo.BG = BGDropdownDTO.CreateFromModel(bg);
                        resultInfo.SubBG = SubBGDropdownDTO.CreateFromModel(subbg);
                        resultInfo.CostCenterCode = "1111";
                        resultInfo.ProfitCenterCode = "22222";
                        resultInfo.ProjectNameEN = "TestProject";

                        //var testUpdateProjectInfo = await serviceInfo.UpdateProjectInfoAsync(resultInfo.Id.Value, resultInfo);
                        //Assert.NotNull(testUpdateProjectInfo);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
