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


namespace MST_General.UnitTests
{
    public class BGsServiceTest
    {
        [Fact]
        public async Task GetBGListAsync()
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

                        var service = new BGService(db);
                        BGFilter filter = FixtureFactory.Get().Build<BGFilter>().Create();
                        filter.ProductTypeKey = "1";
                        PageParam pageParam = new PageParam();
                        BGSortByParam sortByParam = new BGSortByParam();

                        var results = await service.GetBGListAsync(filter, pageParam, sortByParam);

                        filter = new BGFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(BGSortBy)).Cast<BGSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new BGSortByParam() { SortBy = item };
                            results = await service.GetBGListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.BGs);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateBGAsync()
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
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType").FirstAsync();
                        var input = new BGDTO
                        {
                            BGNo = "BG0001",
                            Name = "BG0001",
                            ProductType = MasterCenterDropdownDTO.CreateFromModel(productType)
                        };

                        var service = new BGService(db);
                        var result = await service.CreateBGAsync(input);

                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetBGAsync()
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
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType").FirstAsync();
                        var input = new BGDTO
                        {
                            BGNo = "BG0001",
                            Name = "BG0001",
                            ProductType = MasterCenterDropdownDTO.CreateFromModel(productType)
                        };

                        var service = new BGService(db);
                        var resultCreate = await service.CreateBGAsync(input);
                        var result = await service.GetBGAsync(resultCreate.Id.Value);

                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateBGAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var productType = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProductType");
                var input = new BGDTO
                {
                    BGNo = "BG0001",
                    Name = "BG0001",
                    ProductType = MasterCenterDropdownDTO.CreateFromModel(productType)
                };

                var service = new BGService(db);
                var resultCreate = await service.CreateBGAsync(input);
                resultCreate.Name = "BG0002";
                var result = await service.UpdateBGAsync(resultCreate.Id.Value, resultCreate);

                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task DeleteBGAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var productType = await db.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == "ProductType");
                var input = new BGDTO
                {
                    BGNo = "BG0001",
                    Name = "BG0001",
                    ProductType = MasterCenterDropdownDTO.CreateFromModel(productType)
                };

                var service = new BGService(db);
                var resultCreate = await service.CreateBGAsync(input);
                resultCreate.Name = "BG0002";

                var subBGService = new SubBGService(db);
                var subBGInput = new SubBGDTO()
                {
                    Name = "SUBBG0001",
                    SubBGNo = "S0001",
                    BG = new BGListDTO()
                    {
                        Id = resultCreate.Id.Value
                    }
                };
                await subBGService.CreateSubBGAsync(subBGInput);
                subBGInput = new SubBGDTO()
                {
                    Name = "SUBBG0002",
                    SubBGNo = "S0002",
                    BG = new BGListDTO()
                    {
                        Id = resultCreate.Id.Value
                    }
                };
                await subBGService.CreateSubBGAsync(subBGInput);

                var project = await db.Projects.FirstAsync();
                project.BGID = resultCreate.Id;
                db.Update(project);
                await db.SaveChangesAsync();
                try
                {
                    await service.DeleteBGAsync(resultCreate.Id.Value);
                }
                catch (ValidateException ex)
                {
                    Trace.WriteLine("Validate Test: " + ex.ErrorResponse.PopupErrors[0].Message);
                }
                project.BGID = null;
                db.Update(project);
                await db.SaveChangesAsync();

                bool beforeDelete = db.SubBGs.Any(o => o.BGID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteBGAsync(resultCreate.Id.Value);
                bool afterDelete = db.SubBGs.Any(o => o.BGID == resultCreate.Id.Value && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
    }
}
