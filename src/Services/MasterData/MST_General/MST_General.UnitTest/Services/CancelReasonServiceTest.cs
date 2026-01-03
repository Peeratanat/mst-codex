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
using ErrorHandling;

namespace MST_General.UnitTests
{
    public class CancelReasonServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetCancelReasonListAsync()
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

                        var service = new CancelReasonService(db);
                        CancelReasonFilter filter = FixtureFactory.Get().Build<CancelReasonFilter>().Create();
                        PageParam pageParam = new PageParam();
                        CancelReasonSortByParam sortByParam = new CancelReasonSortByParam();
                        filter.GroupOfCancelReasonKey = "1";
                        filter.CancelApproveFlowKey = "1";
                        var results = await service.GetCancelReasonListAsync(filter, pageParam, sortByParam);

                        filter = new CancelReasonFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CancelReasonSortBy)).Cast<CancelReasonSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CancelReasonSortByParam() { SortBy = item };
                            results = await service.GetCancelReasonListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.CancelReasons);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateCancelReasonAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new CancelReasonService(db);

                        var groupOfCancelReason = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "GroupOfCancelReason").FirstAsync();
                        var cancelApproveFlow = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CancelApproveFlow").FirstAsync();
                        //Put unit test here
                        var input = new CancelReasonDTO
                        {
                            Key = "792",
                            Description = "ทดสอบabc123#, -, _ ",
                            GroupOfCancelReason = MasterCenterDropdownDTO.CreateFromModel(groupOfCancelReason),
                            CancelApproveFlow = MasterCenterDropdownDTO.CreateFromModel(cancelApproveFlow)
                        };
                        var result = await service.CreateCancelReasonAsync(input);

                        //Test Unique "Description"
                        input = new CancelReasonDTO
                        {
                            Key = "876",
                            Description = "ทดสอบabc123#, -, _ ",
                            GroupOfCancelReason = MasterCenterDropdownDTO.CreateFromModel(groupOfCancelReason),
                            CancelApproveFlow = MasterCenterDropdownDTO.CreateFromModel(cancelApproveFlow)
                        };
                        try
                        {
                            result = await service.CreateCancelReasonAsync(input);
                            Assert.NotNull(result);
                        }
                        catch (ValidateException ex)
                        {
                            Assert.NotEmpty(ex.ErrorResponse.FieldErrors.Where(o => o.Code == "ERR0042").ToList());
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetCancelReasonAsync()
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
                        var service = new CancelReasonService(db);

                        var groupOfCancelReason = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "GroupOfCancelReason").FirstAsync();
                        var cancelApproveFlow = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CancelApproveFlow").FirstAsync();
                        //Put unit test here
                        var input = new CancelReasonDTO
                        {
                            Key = "792",
                            Description = "ทดสอบ",
                            GroupOfCancelReason = MasterCenterDropdownDTO.CreateFromModel(groupOfCancelReason),
                            CancelApproveFlow = MasterCenterDropdownDTO.CreateFromModel(cancelApproveFlow)
                        };
                        var resultCreate = await service.CreateCancelReasonAsync(input);

                        var result = await service.GetCancelReasonAsync(resultCreate.Id.Value);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateCancelReasonAsync()
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
                        var service = new CancelReasonService(db);

                        var groupOfCancelReason = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "GroupOfCancelReason").FirstAsync();
                        var cancelApproveFlow = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CancelApproveFlow").FirstAsync();
                        //Put unit test here
                        var input = new CancelReasonDTO
                        {
                            Key = "792",
                            Description = "ทดสอบ",
                            GroupOfCancelReason = MasterCenterDropdownDTO.CreateFromModel(groupOfCancelReason),
                            CancelApproveFlow = MasterCenterDropdownDTO.CreateFromModel(cancelApproveFlow)
                        };
                        var resultCreate = await service.CreateCancelReasonAsync(input);
                        resultCreate.Key = "5555";

                        var result = await service.UpdateCancelReasonAsync(resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteCancelReasonAsync()
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
                        var service = new CancelReasonService(db);

                        var groupOfCancelReason = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "GroupOfCancelReason").FirstAsync();
                        var cancelApproveFlow = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CancelApproveFlow").FirstAsync();
                        //Put unit test here
                        var input = new CancelReasonDTO
                        {
                            Key = "792",
                            Description = "ทดสอบ",
                            GroupOfCancelReason = MasterCenterDropdownDTO.CreateFromModel(groupOfCancelReason),
                            CancelApproveFlow = MasterCenterDropdownDTO.CreateFromModel(cancelApproveFlow)
                        };
                        var resultCreate = await service.CreateCancelReasonAsync(input);

                        bool beforeDelete = db.CancelReasons.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.True(beforeDelete);
                        await service.DeleteCancelReasonAsync(resultCreate.Id.Value);
                        bool afterDelete = db.CancelReasons.Any(o => o.ID == resultCreate.Id.Value && o.IsDeleted == false);
                        Assert.False(afterDelete);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
