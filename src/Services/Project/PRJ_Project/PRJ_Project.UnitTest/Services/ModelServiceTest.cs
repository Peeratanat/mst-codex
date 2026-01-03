using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Services;

namespace PRJ_Project.UnitTests
{
    public class ModelServiceTest
    {
        [Fact]
        public async Task GetModelDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Projects.AsNoTracking().FirstAsync();
                        var service = new ModelService(db);

                        var result = await service.GetModelDropdownListAsync(model.ID, null);
                        Assert.NotEmpty(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Projects.AsNoTracking().FirstAsync();
                        var service = new ModelService(db);
                        var filter = new ModelsFilter();
                        var pageParam = new PageParam { Page = 1, PageSize = 1 };
                        var sortByParam = new ModelListSortByParam();

                        var result = await service.GetModelListAsync(model.ID, filter, pageParam, sortByParam, default);
                        Assert.NotEmpty(result.Models);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetModelListAllAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Projects.AsNoTracking().FirstAsync();
                        var service = new ModelService(db);
                        var filter = new ModelsFilter();
                        var pageParam = new PageParam { Page = 1, PageSize = 1 };
                        var sortByParam = new ModelListSortByParam();

                        var result = await service.GetModelListAllAsync(model.ID, filter, pageParam, sortByParam, default);
                        Assert.NotEmpty(result.Models);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetModelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.Projects.AsNoTracking().FirstAsync();
                        var model = await db.Models.AsNoTracking().FirstAsync();
                        var service = new ModelService(db);

                        var result = await service.GetModelAsync(project.ID, model.ID, default);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateModelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.Projects.AsNoTracking().FirstAsync();
                        var service = new ModelService(db);
                        var input = new ModelDTO { NameTH = "Test Model" };

                        var result = await service.CreateModelAsync(project.ID, input);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateModelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.Projects.AsNoTracking().FirstAsync();
                        var model = await db.Models.AsNoTracking().FirstAsync();
                        var service = new ModelService(db);
                        var input = new ModelDTO { NameTH = "Updated Model" };

                        var result = await service.UpdateModelAsync(project.ID, model.ID, input);
                        Assert.NotNull(result);
                        Assert.Equal("Updated Model", result.NameTH);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var model = await db.Models.AsNoTracking().FirstAsync();
                        var service = new ModelService(db);
                        var modelEdit = ModelDTO.CreateFromModel(model);
                        modelEdit.PreferUnit = 99;

                        var result = await service.UpdateModelListAsync(model.ProjectID, new List<ModelDTO> { modelEdit }, new List<Guid> { (Guid)modelEdit.Id });
                        var modelAfter = await db.Models.AsNoTracking().FirstAsync(f => f.ID == (Guid)modelEdit.Id);
                        Assert.Equal(99, modelAfter.PreferUnit);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteModelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.Projects.AsNoTracking().FirstAsync();
                        var model = await db.Models.AsNoTracking().FirstAsync();
                        var service = new ModelService(db);

                        var result = await service.DeleteModelAsync(project.ID, model.ID);
                        bool afterDelete = db.Models.Any(o => o.ID == model.ID && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
