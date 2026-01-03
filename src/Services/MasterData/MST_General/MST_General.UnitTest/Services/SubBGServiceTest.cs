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
    public class SubBGServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();


        [Fact]
        public async Task GetSubBGListAsync()
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

                        var service = new SubBGService(db);
                        SubBGFilter filter = FixtureFactory.Get().Build<SubBGFilter>().Create();
                        PageParam pageParam = new PageParam();
                        SubBGSortByParam sortByParam = new SubBGSortByParam();

                        var results = await service.GetSubBGListAsync(filter, pageParam, sortByParam);

                        filter = new SubBGFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(SubBGSortBy)).Cast<SubBGSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new SubBGSortByParam() { SortBy = item };
                            results = await service.GetSubBGListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.SubBGs);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }


        [Fact]
        public async Task CreateSubBGAsync()
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
                        var bg = await db.BGs.FirstAsync();
                        var input = new SubBGDTO
                        {
                            Name = "SubBGTEST",
                            SubBGNo = "555",
                            BG = BGListDTO.CreateFromModel(bg)
                        };

                        var service = new SubBGService(db);

                        var result = await service.CreateSubBGAsync(input);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetSubBGAsync()
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
                        var bg = await db.BGs.FirstAsync();
                        var input = new SubBGDTO
                        {
                            Name = "SubBGTEST",
                            SubBGNo = "555",
                            BG = BGListDTO.CreateFromModel(bg)
                        };

                        var service = new SubBGService(db);

                        var resultCreate = await service.CreateSubBGAsync(input);

                        var result = await service.GetSubBGAsync(resultCreate.Id.Value);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateSubBGAsync()
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
                        var bg = await db.BGs.FirstAsync();
                        var input = new SubBGDTO
                        {
                            Name = "SubBGTEST",
                            SubBGNo = "555",
                            BG = BGListDTO.CreateFromModel(bg)
                        };

                        var service = new SubBGService(db);

                        var resultCreate = await service.CreateSubBGAsync(input);
                        resultCreate.Name = "SubBGTEST2";
                        var result = await service.UpdateSubBGAsync(resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);
                        Assert.Equal("SubBGTEST2", result.Name);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteSubBGAsync()
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
                        var bg = await db.BGs.FirstAsync();
                        var input = new SubBGDTO
                        {
                            Name = "SubBGTEST",
                            SubBGNo = "555",
                            BG = BGListDTO.CreateFromModel(bg)
                        };

                        var service = new SubBGService(db);

                        var resultCreate = await service.CreateSubBGAsync(input);


                        bool beforeDelete = db.SubBGs.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.True(beforeDelete);
                        await service.DeleteSubBGAsync(resultCreate.Id.Value);
                        bool afterDelete = db.SubBGs.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
