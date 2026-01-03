using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.Models.MST;
using Database.UnitTestExtensions;
using MST_General.Params.Filters;
using MST_General.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MST_General.UnitTests
{
    public class MasterCenterServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetMasterCenterListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here

                var service = new MasterCenterService(db);
                MasterCenterFilter filter = FixtureFactory.Get().Build<MasterCenterFilter>().Create();
                PageParam pageParam = new PageParam();
                MasterCenterSortByParam sortByParam = new MasterCenterSortByParam();

                var results = await service.GetMasterCenterListAsync(filter, pageParam, sortByParam);

                filter = new MasterCenterFilter();
                pageParam = new PageParam() { Page = 1, PageSize = 10 };

                var sortByParams = Enum.GetValues(typeof(MasterCenterSortBy)).Cast<MasterCenterSortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new MasterCenterSortByParam() { SortBy = item };
                    results = await service.GetMasterCenterListAsync(filter, pageParam, sortByParam);
                    Assert.NotEmpty(results.MasterCenters);
                }

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetFindMasterCenterDropdownItemAsync()
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

                        var service = new MasterCenterService(db);
                        var result = await service.GetFindMasterCenterDropdownItemAsync("UnitDirection", "N");
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMasterCenterAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var masterCenterGroupKey = new MasterCenterGroup
                {
                    Key = "Test",
                    Name = "Test"
                };
                await db.MasterCenterGroups.AddAsync(masterCenterGroupKey);
                await db.SaveChangesAsync();

                var service = new MasterCenterService(db);
                var input = FixtureFactory.Get().Build<MasterCenterDTO>()
                                   .With(o => o.Id, (Guid?)null)
                                   .With(o => o.MasterCenterGroup, MasterCenterGroupListDTO.CreateFromModel(masterCenterGroupKey))
                                   .With(o => o.Key, "UnitTestMasterCenter")
                                   .With(o => o.Name, "เทส")
                                   .Create();

                var result = await service.CreateMasterCenterAsync(input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetMasterCenterAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var masterCenterGroupKey = new MasterCenterGroup
                {
                    Key = "Test",
                    Name = "Test"
                };
                await db.MasterCenterGroups.AddAsync(masterCenterGroupKey);
                await db.SaveChangesAsync();

                var service = new MasterCenterService(db);
                var input = FixtureFactory.Get().Build<MasterCenterDTO>()
                                   .With(o => o.Id, (Guid?)null)
                                   .With(o => o.MasterCenterGroup, MasterCenterGroupListDTO.CreateFromModel(masterCenterGroupKey))
                                   .With(o => o.Key, "UnitTestMasterCenter")
                                   .With(o => o.Name, "เทส")
                                   .Create();

                var resultCreate = await service.CreateMasterCenterAsync(input);

                var result = await service.GetMasterCenterAsync(resultCreate.Id.Value);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task UpdateMasterCenterAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var masterCenterGroupKey = new MasterCenterGroup
                {
                    Key = "Test",
                    Name = "Test"
                };
                await db.MasterCenterGroups.AddAsync(masterCenterGroupKey);
                await db.SaveChangesAsync();

                var service = new MasterCenterService(db);
                var input = FixtureFactory.Get().Build<MasterCenterDTO>()
                                   .With(o => o.Id, (Guid?)null)
                                   .With(o => o.MasterCenterGroup, MasterCenterGroupListDTO.CreateFromModel(masterCenterGroupKey))
                                   .With(o => o.Key, "UnitTestMasterCenter")
                                   .With(o => o.Name, "เทส")
                                   .Create();

                var resultCreate = await service.CreateMasterCenterAsync(input);
                resultCreate.Name = "เทส1";
                var result = await service.UpdateMasterCenterAsync(resultCreate.Id.Value, resultCreate);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteMasterCenterAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var masterCenterGroupKey = new MasterCenterGroup
                {
                    Key = "Test",
                    Name = "Test"
                };
                await db.MasterCenterGroups.AddAsync(masterCenterGroupKey);
                await db.SaveChangesAsync();

                var service = new MasterCenterService(db);
                var input = FixtureFactory.Get().Build<MasterCenterDTO>()
                                   .With(o => o.Id, (Guid?)null)
                                   .With(o => o.MasterCenterGroup, MasterCenterGroupListDTO.CreateFromModel(masterCenterGroupKey))
                                   .With(o => o.Key, "UnitTestMasterCenter")
                                   .With(o => o.Name, "เทส")
                                   .Create();

                var resultCreate = await service.CreateMasterCenterAsync(input);

                bool beforeDelete = db.MasterCenters.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteMasterCenterAsync(resultCreate.Id.Value);
                bool afterDelete = db.MasterCenters.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }

    }
}
