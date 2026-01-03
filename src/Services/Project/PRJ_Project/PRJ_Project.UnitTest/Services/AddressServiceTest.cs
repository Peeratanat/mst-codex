using Base.DTOs.MST;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PRJ_Project.Services;
using Base.DTOs.PRJ;
using PagingExtensions;
using Base.DTOs;
using Database.Models.MasterKeys;


namespace PRJ_Project.UnitTests
{
    public class ProjectAddressServiceTest
    {
        [Fact]
        public async Task GetProjectAddressDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Addresses.FirstAsync();
                        var service = new AddressService(db);

                        var result = await service.GetProjectAddressDropdownListAsync(model.ProjectID, null, null);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetProjectAddressListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Addresses.FirstAsync();
                        var service = new AddressService(db);

                        PageParam pageParam = new()
                        {
                            Page = 1,
                            PageSize = 1
                        };
                        SortByParam param = new()
                        {
                            SortBy = "",
                            Ascending = false
                        };
                        var result = await service.GetProjectAddressListAsync(model.ProjectID, pageParam, param);
                        Assert.NotEmpty(result.ProjectAddresses);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetProjectAddressAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Addresses.FirstAsync();
                        var service = new AddressService(db);
                        PageParam pageParam = new()
                        {
                            Page = 1,
                            PageSize = 1
                        };
                        SortByParam param = new()
                        {
                            SortBy = "",
                            Ascending = false
                        };
                        var result = await service.GetProjectAddressAsync(model.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateProjectAddressAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var model = await db.Projects.FirstAsync();
                var projectType = await db.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectAddressType);
                ProjectAddressDTO input = new()
                {
                    AddressNameEN = "AddressNameEN",
                    AddressNameTH = "AddressNameTH",
                    TitleDeedNo = "TitleDeedNo",
                    LandNo = "LandNo",
                    InspectionNo = "InspectionNo",
                    PostalCode = "PostalCode",
                    HouseMoo = "HouseMoo",
                    HouseSoiEN = "HouseSoiEN",
                    HouseSoiTH = "HouseSoiTH",
                    HouseRoadEN = "HouseRoadEN",
                    HouseRoadTH = "HouseRoadTH",
                    TitledeedMoo = "TitledeedMoo",
                    TitledeedSoiEN = "TitledeedSoiEN",
                    TitledeedSoiTH = "TitledeedSoiTH",
                    TitledeedRoadTH = "TitledeedRoadTH",
                    TitledeedRoadEN = "TitledeedRoadEN",
                    Moo = "Moo",
                    RoadTH = "RoadTH",
                    RoadEN = "RoadEN",
                    SoiTH = "SoiTH",
                    SoiEN = "SoiEN",
                    ProjectAddressType = MasterCenterDropdownDTO.CreateFromModel(projectType),
                };

                var service = new AddressService(db);
                var result = await service.CreateProjectAddressAsync(model.ID, input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
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
                var model = await db.Addresses.FirstOrDefaultAsync();
                var project = await db.Projects.FirstOrDefaultAsync();
                var address = await db.Projects.FirstOrDefaultAsync();
                var service = new AddressService(db);
                var modelEdit = await service.GetProjectAddressOnlyAsync(model.ID);
                modelEdit.AddressNameEN = "UnitTest001";
                modelEdit.AddressNameTH = "UnitTest001";

                // ExecuteUpdateAsync not tracking model can't test
                var resultEdit = await service.UpdateProjectAddressAsync(project.ID, model.ID, modelEdit);
                Assert.NotNull(resultEdit);
                Assert.Equal("UnitTest001", resultEdit.AddressNameEN);
                Assert.Equal("UnitTest001", resultEdit.AddressNameTH);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task DeleteSpecMaterialItemAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var service = new AddressService(db);
                var addresse = await db.Addresses.FirstAsync();
                var model = await service.GetProjectAddressOnlyAsync(addresse.ID);

                bool beforeDelete = db.Addresses.Any(o => o.ID == addresse.ID && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteProjectAddressAsync(addresse.ID);
                bool afterDelete = db.Addresses.Any(o => o.ID == addresse.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }

    }
}
