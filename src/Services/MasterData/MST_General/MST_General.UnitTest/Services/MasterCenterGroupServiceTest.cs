using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
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
    public class MasterCenterGroupServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetMasterCenterGroupListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here

                var service = new MasterCenterGroupService(db);
                MasterCenterGroupFilter filter = FixtureFactory.Get().Build<MasterCenterGroupFilter>().Create();
                PageParam pageParam = new PageParam();
                MasterCenterGroupSortByParam sortByParam = new MasterCenterGroupSortByParam();

                var results = await service.GetMasterCenterGroupListAsync(filter, pageParam, sortByParam);

                filter = new MasterCenterGroupFilter();
                pageParam = new PageParam() { Page = 1, PageSize = 10 };

                var sortByParams = Enum.GetValues(typeof(MasterCenterGroupSortBy)).Cast<MasterCenterGroupSortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new MasterCenterGroupSortByParam() { SortBy = item };
                    results = await service.GetMasterCenterGroupListAsync(filter, pageParam, sortByParam);
                    Assert.NotEmpty(results.MasterCenterGroups);
                }

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task CreateMasterCenterGroupAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here

                var service = new MasterCenterGroupService(db);
                var input = new MasterCenterGroupDTO
                {
                    Key = "UnitTestMasterCenterGroup",
                    Name = "เทสกรุ๊ป"
                };

                var result = await service.CreateMasterCenterGroupAsync(input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task GetMasterCenterGroupAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new MasterCenterGroupService(db);
                var input = new MasterCenterGroupDTO
                {
                    Key = "UnitTestMasterCenterGroup",
                    Name = "เทสกรุ๊ป"
                };

                var resultCreate = await service.CreateMasterCenterGroupAsync(input);

                var result = await service.GetMasterCenterGroupAsync(resultCreate.Key);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task UpdateMasterCenterGroupAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new MasterCenterGroupService(db);
                var input = new MasterCenterGroupDTO
                {
                    Key = "UnitTestMasterCenterGroup",
                    Name = "เทสกรุ๊ป"
                };

                var resultCreate = await service.CreateMasterCenterGroupAsync(input);
                resultCreate.Name = "สื่อโฆษณา";
                var result = await service.UpdateMasterCenterGroupAsync(resultCreate.Key, resultCreate);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteMasterCenterGroupAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var service = new MasterCenterGroupService(db);
                var input = new MasterCenterGroupDTO
                {
                    Key = "UnitTestMasterCenterGroup",
                    Name = "เทสกรุ๊ป"
                };

                var resultCreate = await service.CreateMasterCenterGroupAsync(input);

                bool beforeDelete = db.MasterCenterGroups.Any(o => o.Key == resultCreate.Key && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteMasterCenterGroupAsync(resultCreate.Key);
                bool afterDelete = db.MasterCenterGroups.Any(o => o.Key == resultCreate.Key && o.IsDeleted == false);
                Assert.False(afterDelete);


                await tran.RollbackAsync();
            });
        }

    }
}
