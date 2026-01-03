using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using ErrorHandling;
using System.Diagnostics;
using MST_General.Params.Filters;
using MST_General.Services;
using MST_General.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using OfficeOpenXml;
using Base.DTOs;
using System.Reflection.Metadata;


namespace MST_General.UnitTests
{
    public class SpecMaterialServiceTest
    {
        public SpecMaterialServiceTest()
        {
            Environment.SetEnvironmentVariable("minio_AccessKey", "XNTYE7HIMF6KK4BVEIXA");
            Environment.SetEnvironmentVariable("minio_DefaultBucket", "master-data");
            Environment.SetEnvironmentVariable("minio_PublicURL", "192.168.2.29:30050");
            Environment.SetEnvironmentVariable("minio_Endpoint", "192.168.2.29:9001");
            Environment.SetEnvironmentVariable("minio_SecretKey", "naD+esQ+uV7+xwfF3bPfAn5iC7C1XUyXeM8HkBlO");
            Environment.SetEnvironmentVariable("minio_TempBucket", "temp");
            Environment.SetEnvironmentVariable("minio_WithSSL", "false");
        }

        [Fact]
        public async Task GetSpecMaterialCollectionListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new SpecMaterialService(db); 
                        SpecMaterialCollectionFilter filter = FixtureFactory.Get().Build<SpecMaterialCollectionFilter>().Create();
                        PageParam pageParam = new PageParam();
                        SpecMaterialCollectionSortByParam sortByParam = new SpecMaterialCollectionSortByParam();

                        var results = await service.GetSpecMaterialCollectionListAsync(filter, pageParam, sortByParam);

                        filter = new SpecMaterialCollectionFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(SpecMaterialCollectionSortBy)).Cast<SpecMaterialCollectionSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new SpecMaterialCollectionSortByParam() { SortBy = item };
                            results = await service.GetSpecMaterialCollectionListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.SpecMaterialCollection);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetSpecMaterialDetailByItemIdAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var specMaterialCollection = await db.SpecMaterialCollectionDetails.FirstAsync();
                        var service = new SpecMaterialService(db);
                        SpecMaterialCollectionDetailFilter filter = new();

                        var result = await service.GetSpecMaterialDetailByItemIdAsync((Guid)specMaterialCollection.SpecMaterialItemID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task GetSpecMaterialCollectionDetailAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var specMaterialCollection = await db.SpecMaterialCollectionDetails.FirstAsync();
                var service = new SpecMaterialService(db);
                SpecMaterialCollectionDetailFilter filter = new();

                var result = service.GetSpecMaterialCollectionDetailAsync(specMaterialCollection.ID, filter, null, null);
                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task GetAllSpecMaterialCollectionItemsAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var service = new SpecMaterialService(db);
                SpecMaterialCollectionDetailFilter filter = new();
                PageParam pageParam = new()
                {
                    Page = 1,
                    PageSize = 1
                };
                SpecMaterialCollectionDetailSortByParam param = new()
                {
                    SortBy = SpecMaterialCollectionDetailSortBy.Name,
                    Ascending = false
                };

                var result = await service.GetAllSpecMaterialCollectionItemsAsync(filter, pageParam, param);
                Assert.NotNull(result.SpecMaterialCollectionDetail);
                await tran.RollbackAsync();
            });
        }


        [Fact]
        public async Task GetUnitModelByProjectAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var collection = await db.SpecMaterialCollections.FirstOrDefaultAsync();
                    var service = new SpecMaterialService(db);

                    var result = service.GetUnitModelByProjectAsync((Guid)collection.ProjectID, collection.ID);
                    Assert.NotNull(result);
                });
            }
        }
        [Fact]
        public async Task GetSpecMaterialItemByIdAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var model = await db.SpecMaterialItems.FirstAsync();
                    var service = new SpecMaterialService(db);

                    var result = service.GetSpecMaterialItemByIdAsync(model.ID);
                    Assert.NotNull(result);
                });
            }
        }
        [Fact]
        public async Task ExportTemplateSpecMaterialAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var service = new SpecMaterialService(db);
                    var model = await db.Projects.FirstAsync();
                    FileDTO result = await service.ExportTemplateSpecMaterialAsync(model.ID);
                    Assert.NotNull(result.Url);
                });
            }
        }
        [Fact]
        public async Task ExportSpecMaterialDetailAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var model = await db.SpecMaterialCollections.FirstAsync();
                    var service = new SpecMaterialService(db);
                    FileDTO result = await service.ExportSpecMaterialDetailAsync(model.ID);
                    Assert.NotNull(result.Url);
                });
            }
        }

        [Fact]
        public async Task EditSpecMaterialCollectionAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
            using var tran = await db.Database.BeginTransactionAsync();
            //Put unit test here
            var model = await db.SpecMaterialCollections.FirstAsync();
            var service = new SpecMaterialService(db);

            var models = await db.SpecMaterialCollectionDetails
                    .Include(o => o.SpecMaterialGroup)
                    .Include(o => o.UpdatedBy)
                .Where(o => o.SpecMaterialItemID == model.ID)
                .Select(s => SpecMaterialCollectionDetailDTO.CreatedFromModel(s, db)).ToListAsync();
                  


                var resultEdit = await service.EditSpecMaterialCollectionAsync(model.ID, (Guid)model.ProjectID, false, "UnitTest001", models);
                Assert.NotNull(resultEdit);
                Assert.Equal(false, resultEdit.IsActive);
                Assert.Equal("UnitTest001", resultEdit.Name);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task AddSpecMaterialItemsAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var Models = await db.Models.FirstOrDefaultAsync();
                var Group = await db.SpecMaterialCollectionDetails.Include(o => o.SpecMaterialGroup).FirstOrDefaultAsync();
                var SpecMaterialItem = await db.SpecMaterialItems.FirstOrDefaultAsync();
                SpecMaterialCollectionDTO input = new SpecMaterialCollectionDTO()
                {
                    Name = "UnitTest001",
                    IsActive = true,
                    Model = ModelDropdownDTO.CreateFromModel(Models),
                    Group = MasterCenterDropdownDTO.CreateFromModel(Group.SpecMaterialGroup),
                    SpecMaterialItem = SpecMaterialItemDTO.CreateFromModel(SpecMaterialItem)
                };

                var service = new SpecMaterialService(db);
                var result = await service.AddSpecMaterialItemsAsync(input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task EditSpecMaterialItemsAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var model = await db.SpecMaterialCollections.FirstOrDefaultAsync();
                var service = new SpecMaterialService(db);
                var modelEdit = await service.GetSpecMaterialCollectionAsync(model.ID);

                var modelItem = await db.SpecMaterialItems.FirstOrDefaultAsync();
                modelEdit.SpecMaterialItem = SpecMaterialItemDTO.CreateFromModel(modelItem);
                modelEdit.SpecMaterialItem.ItemDescription = "UnitTest001";
                modelEdit.SpecMaterialItem.Name = "UnitTest001";


                // ExecuteUpdateAsync not tracking model can't test
                // var resultEdit = await service.EditSpecMaterialItemsAsync(modelEdit);
                // Assert.NotNull(resultEdit);
                // Assert.Equal("UnitTest001", resultEdit.ItemDescription);
                // Assert.Equal("UnitTest001", resultEdit.Name);

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

                var specMaterialItems = await db.SpecMaterialItems.FirstAsync();

                var service = new SpecMaterialService(db);
                bool beforeDelete = db.SpecMaterialItems.Any(o => o.ID == specMaterialItems.ID && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteSpecMaterialItemAsync((Guid)specMaterialItems.ID);
                var afterDeletwee = await db.SpecMaterialItems.FirstOrDefaultAsync(o => o.ID == specMaterialItems.ID && o.IsDeleted == false);
                bool afterDelete = db.SpecMaterialItems.Any(o => o.ID == specMaterialItems.ID );
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task DeleteSpecMaterialCollectionAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var specMaterialCollection = await db.SpecMaterialCollections.FirstAsync();

                var service = new SpecMaterialService(db);
                bool beforeDelete = db.SpecMaterialCollections.Any(o => o.ID == specMaterialCollection.ID && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteSpecMaterialCollectionAsync(specMaterialCollection.ID);
                bool afterDelete = db.SpecMaterialCollections.Any(o => o.ID == specMaterialCollection.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }

    }
}
