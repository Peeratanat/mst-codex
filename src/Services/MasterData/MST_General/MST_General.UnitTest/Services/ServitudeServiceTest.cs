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


namespace MST_General.UnitTests
{
    public class ServitudeServiceTest
    {
        [Fact]
        public async Task GetServitudeListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.Projects.FirstAsync();
                        
                        var service = new ServitudeService(db);
                        
                        var pageParam = new PageParam { Page = 1, PageSize = 1 };
                        var sortByParam = new ServitudeSortByParam();

                        var resultCreate = await service.GetServitudeListAsync(new ServitudeFilter(),pageParam,sortByParam);
                        Assert.NotNull(resultCreate);
                        await tran.RollbackAsync();
                    }
                });
            }
        }



        [Fact]
        public async Task AddServitudeAsync()
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
                        var project = await db.Projects.FirstAsync();
                        var input = new ServitudeDTO
                        {
                            MsgTH = "BG0001",
                            MsgEN = "BG0001",
                            Project = ProjectDropdownDTO.CreateFromModel(project)
                        };

                        var service = new ServitudeService(db);
                        var result = await service.AddServitudeAsync(input);

                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }



        [Fact]
        public async Task EditServitudeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var project = await db.Projects.FirstAsync();
                var input = new ServitudeDTO
                {
                    MsgTH = "BG0001",
                    MsgEN = "BG0001",
                    Project = ProjectDropdownDTO.CreateFromModel(project)
                };

                var service = new ServitudeService(db);
                var resultCreate = await service.AddServitudeAsync(input);
                resultCreate.MsgTH = "BG0002";
                var result = await service.EditServitudeAsync(resultCreate);
                Assert.NotNull(result);
                Assert.Equal("BG0002", result.MsgTH);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteServitudeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var project = await db.Projects.FirstAsync();
                var input = new ServitudeDTO
                {
                    MsgTH = "BG0001",
                    MsgEN = "BG0001",
                    Project = ProjectDropdownDTO.CreateFromModel(project)
                };

                var service = new ServitudeService(db);
                var resultCreate = await service.AddServitudeAsync(input);

                bool beforeDelete = db.Servitude.Any(o => o.ID == resultCreate.ID && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteServitudeAsync(resultCreate);
                bool afterDelete = db.Servitude.Any(o => o.ID == resultCreate.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
    }
}
