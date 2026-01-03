using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models.PRM;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Services;
using Xunit;

namespace MST_Promotion.UnitTests
{
    public class MasterPreSalePromotionServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetMasterPreSalePromotionDropdownAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var service = new MasterPreSalePromotionService(db);
                    string promotionNo = "A";
                    string name = "A";
                    var results = service.GetMasterPreSalePromotionDropdownAsync(promotionNo, name);
                    Assert.NotNull(results);
                });
            }
        }

        [Fact]
        public async Task GetMasterPreSalePromotionListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        //Put unit test here
                        MasterPreSalePromotionListFilter filter = FixtureFactory.Get().Build<MasterPreSalePromotionListFilter>().Create();
                        filter.PromotionStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "PromotionStatus")
                                                                          .Select(x => x.Key).FirstAsync();
                        PageParam pageParam = new PageParam();
                        MasterPreSalePromotionSortByParam sortByParam = new MasterPreSalePromotionSortByParam();
                        var results = await service.GetMasterPreSalePromotionListAsync(filter, pageParam, sortByParam);

                        filter = new MasterPreSalePromotionListFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterPreSalePromotionSortBy)).Cast<MasterPreSalePromotionSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterPreSalePromotionSortByParam() { SortBy = item };
                            results = await service.GetMasterPreSalePromotionListAsync(filter, pageParam, sortByParam);
                            Assert.NotNull(results.MasterPreSalePromotionDTOs);
                        }
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterPreSalePromotionDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        //Put unit test here

                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                                    .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);

                        var results = await service.GetMasterPreSalePromotionDetailAsync(resultCreate.Id.Value);
                        Assert.NotNull(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetActiveMasterPreSalePromotionDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        //Put unit test here

                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .FirstAsync();
                        var project = await db.Projects.Where(o => o.ProjectNo == "70014").FirstAsync();
                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(masterCenterPromotionStatusID);
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);

                        var results = await service.GetActiveMasterPreSalePromotionDetailAsync(project.ID);
                        Assert.NotNull(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMasterPreSalePromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);

                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                                    .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var result = await service.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterPreSalePromotionAsync()
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
                        var service = new MasterPreSalePromotionService(db);

                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);

                        resultCreate.Name = "TestUpdate";
                        resultCreate.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(masterCenterPromotionStatus);

                        var resultUpdate = await service.UpdateMasterPreSalePromotionAsync(resultCreate.Id.Value, resultCreate);
                        var promotionstatusActive = await db.MasterPreSalePromotions.Where(o => o.ProjectID == project.ID && o.PromotionStatusMasterCenterID == masterCenterPromotionStatus.ID).ToListAsync();
                        Assert.NotNull(resultUpdate);
                        Assert.Equal("TestUpdate", resultUpdate.Name);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteMasterPreSalePromotionAsync()
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
                        var service = new MasterPreSalePromotionService(db);
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                                   .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);

                        await service.DeleteMasterPreSalePromotionAsync(resultCreate.Id.Value);
                        var result = await db.MasterPreSalePromotions.FirstOrDefaultAsync(x => x.ID == resultCreate.Id.Value);
                        Assert.True(result.IsDeleted);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterPreSalePromotionItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                        .Select(o => o.ID).FirstAsync();
                        var masterPreSalePromotion = FixtureFactory.Get().Build<MasterPreSalePromotion>()
                                                            .With(o => o.IsDeleted, false)
                                                            .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                            .Create();

                        await db.MasterPreSalePromotions.AddAsync(masterPreSalePromotion);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();
                        MasterPreSalePromotionItemSortByParam sortByParam = new MasterPreSalePromotionItemSortByParam();
                        var results = await service.GetMasterPreSalePromotionItemListAsync(masterPreSalePromotion.ID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterPreSalePromotionItemSortBy)).Cast<MasterPreSalePromotionItemSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterPreSalePromotionItemSortByParam() { SortBy = item };
                            results = await service.GetMasterPreSalePromotionItemListAsync(masterPreSalePromotion.ID, pageParam, sortByParam);
                            Assert.NotNull(results.MasterPreSalePromotionItemDTOs);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterPreSalePromotionItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);

                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();
                        var masterPreSalePromotion = FixtureFactory.Get().Build<MasterPreSalePromotion>()
                                                            .With(o => o.IsDeleted, false)
                                                            .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                            .Create();
                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create()
                        };
                        await db.MasterPreSalePromotions.AddAsync(masterPreSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreate = await service.CreateMasterPreSalePromotionItemFromMaterialAsync(masterPreSalePromotion.ID, resultpromotions.PromotionMaterialDTOs);


                        resultCreate.ForEach(o => { o.UnitEN = "Test"; o.NameEN = "Test"; });
                        var resultUpdates = await service.UpdateMasterPreSalePromotionItemListAsync(masterPreSalePromotion.ID, resultCreate);
                        Assert.NotNull(resultUpdates);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterPreSalePromotionItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);

                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                      .Select(o => o.ID).FirstAsync();
                        var masterPreSalePromotion = FixtureFactory.Get().Build<MasterPreSalePromotion>()
                                                            .With(o => o.IsDeleted, false)
                                                            .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                            .Create();
                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create()
                        };
                        await db.MasterPreSalePromotions.AddAsync(masterPreSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreate = await service.CreateMasterPreSalePromotionItemFromMaterialAsync(masterPreSalePromotion.ID, resultpromotions.PromotionMaterialDTOs);

                        resultCreate[0].NameTH = "เทส";
                        resultCreate[0].NameEN = "Test";
                        resultCreate[0].UnitEN = "Test";
                        var resultUpdate = await service.UpdateMasterPreSalePromotionItemAsync(masterPreSalePromotion.ID, resultCreate[0].Id.Value, resultCreate[0]);
                        Assert.NotNull(resultUpdate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMasterPreSalePromotionItemFromMaterialAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);

                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                     .Select(o => o.ID).FirstAsync();
                        var masterPreSalePromotion = FixtureFactory.Get().Build<MasterPreSalePromotion>()
                                                            .With(o => o.IsDeleted, false)
                                                            .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                            .Create();
                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create()
                        };
                        await db.MasterPreSalePromotions.AddAsync(masterPreSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreate = await service.CreateMasterPreSalePromotionItemFromMaterialAsync(masterPreSalePromotion.ID, resultpromotions.PromotionMaterialDTOs);
                        Assert.NotNull(resultCreate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateSubMasterPreSalePromotionItemFromMaterialAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);

                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                     .Select(o => o.ID).FirstAsync();
                        var masterPreSalePromotion = FixtureFactory.Get().Build<MasterPreSalePromotion>()
                                                            .With(o => o.IsDeleted, false)
                                                            .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                            .Create();
                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create()
                        };
                        await db.MasterPreSalePromotions.AddAsync(masterPreSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreate = await service.CreateMasterPreSalePromotionItemFromMaterialAsync(masterPreSalePromotion.ID, resultpromotions.PromotionMaterialDTOs);

                        var masterPreSalePromotionItem = resultCreate[0];

                        var resultsSub = await service.CreateSubMasterPreSalePromotionItemFromMaterialAsync(masterPreSalePromotion.ID, masterPreSalePromotionItem.Id.Value, resultpromotions.PromotionMaterialDTOs);
                        Assert.NotNull(resultsSub);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterPreSalePromotionItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);

                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                     .Select(o => o.ID).FirstAsync();
                        var masterPreSalePromotion = FixtureFactory.Get().Build<MasterPreSalePromotion>()
                                                            .With(o => o.IsDeleted, false)
                                                            .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                            .Create();
                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create()
                        };
                        await db.MasterPreSalePromotions.AddAsync(masterPreSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreate = await service.CreateMasterPreSalePromotionItemFromMaterialAsync(masterPreSalePromotion.ID, resultpromotions.PromotionMaterialDTOs);
                        var models = await db.Models.Where(o => !o.IsDeleted).ToListAsync();
                        IList<MasterPreSaleHouseModelItem> masterBookingHouseModelItems = new List<MasterPreSaleHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterPreSaleHouseModelItem>()
                                   .With(o=>o.MasterPreSalePromotionItemID,resultCreate[0].Id.Value)
                                   .With(o=>o.ModelID,models[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterPreSaleHouseModelItem>()
                                   .With(o=>o.MasterPreSalePromotionItemID,resultCreate[0].Id.Value)
                                   .With(o=>o.ModelID,models[1].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        await db.AddRangeAsync(masterBookingHouseModelItems);
                        await db.SaveChangesAsync();

                        var resultsModelItem = await service.GetMasterPreSalePromotionItemModelListAsync(resultCreate[0].Id.Value);
                        Assert.NotNull(resultsModelItem);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task AddMasterPreSalePromotionItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);

                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                            .Select(o => o.ID).FirstAsync();
                        var masterPreSalePromotion = FixtureFactory.Get().Build<MasterPreSalePromotion>()
                                                            .With(o => o.IsDeleted, false)
                                                            .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                            .Create();
                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .Create()
                        };
                        await db.MasterPreSalePromotions.AddAsync(masterPreSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreate = await service.CreateMasterPreSalePromotionItemFromMaterialAsync(masterPreSalePromotion.ID, resultpromotions.PromotionMaterialDTOs);
                        var models = await db.Models.Where(o => !o.IsDeleted).ToListAsync();
                        List<ModelListDTO> modelListDTOs = new List<ModelListDTO>()
                        {
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,models[0].ID)
                                   .Create(),
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,models[1].ID)
                                   .Create()
                        };
                        var resultAdd = await service.AddMasterPreSalePromotionItemModelListAsync(resultCreate[0].Id.Value, modelListDTOs);
                        var resultsModelItem = await service.GetMasterPreSalePromotionItemModelListAsync(resultCreate[0].Id.Value);
                        Assert.NotNull(resultsModelItem);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CloneMasterPreSalePromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .FirstAsync();
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                                    .FirstAsync();
                        var project = await db.Projects.Where(o => o.ProjectNo == "60022").FirstAsync();
                        var masterPreSale = new MasterPreSalePromotionDTO();
                        masterPreSale.Name = "Test";
                        masterPreSale.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(masterCenterPromotionStatusID);
                        masterPreSale.Project = ProjectDTO.CreateFromModel(project);

                        var masterPreSalePromotion = await service.CreateMasterPreSalePromotionAsync(masterPreSale);

                        var whenPromotionReceiveStatusMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
                        var promotionItemStatusMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();

                        IQueryable<ModelQueryResult> models = db.Models.Where(o => o.ProjectID == project.ID).Select(o => new ModelQueryResult
                        {
                            Model = o,
                            ModelShortName = o.ModelShortName,
                            ModelType = o.ModelType,
                            ModelUnitType = o.ModelUnitType,
                            TypeOfRealEstate = o.TypeOfRealEstate,
                            WaterElectricMeterPrice = db.WaterElectricMeterPrices.Where(b => b.ModelID == o.ID).OrderByDescending(b => b.Version).FirstOrDefault()
                        });
                        var modelResults = models.Select(o => ModelListDTO.CreateFromQueryResult(o)).ToList();
                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create()
                        };
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);

                        var resultCreate = await service.CreateMasterPreSalePromotionItemFromMaterialAsync(masterPreSalePromotion.Id.Value, resultpromotions.PromotionMaterialDTOs);

                        var modelHouseItem = new List<ModelListDTO>()
                        {
                            modelResults[0],
                            modelResults[1],
                        };
                        await service.AddMasterPreSalePromotionItemModelListAsync(resultCreate[0].Id.Value, modelHouseItem);

                        MasterPreSalePromotionListFilter filter = new MasterPreSalePromotionListFilter();
                        MasterPreSalePromotionSortByParam sortByParam = new MasterPreSalePromotionSortByParam();
                        MasterPreSalePromotionItemSortByParam sortByParamItem = new MasterPreSalePromotionItemSortByParam();

                        var resultsItem = await service.GetMasterPreSalePromotionItemListAsync(masterPreSalePromotion.Id.Value, pageParam, sortByParamItem);
                        var resultModel = await service.GetMasterPreSalePromotionItemModelListAsync(resultCreate[0].Id.Value);

                        var dataClone = await service.CloneMasterPreSalePromotionAsync(masterPreSalePromotion.Id.Value);

                        var resultsItemClone = await service.GetMasterPreSalePromotionItemListAsync(dataClone.Id.Value, pageParam, sortByParamItem);
                        var resultModelClone = await service.GetMasterPreSalePromotionItemModelListAsync(resultsItemClone.MasterPreSalePromotionItemDTOs.Last().Id.Value);


                        var testResults = await service.GetMasterPreSalePromotionListAsync(filter, pageParam, sortByParam);
                        Assert.NotNull(testResults);

                        await tran.RollbackAsync();

                    }
                });
            }
        }

        [Fact]
        public async Task GetCloneMasterPreSalePromotionConfirmAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterPreSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                     .FirstAsync();
                        var masterPreSalePromotion = FixtureFactory.Get().Build<MasterPreSalePromotion>()
                                                            .With(o => o.IsDeleted, false)
                                                            .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenter.ID)
                                                            .Create();
                        var whenPromotionReceiveStatusMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
                        var promotionItemStatusMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
                        var models = await db.Models.Where(o => !o.IsDeleted).ToListAsync();

                        //Put unit test here
                        IList<MasterPreSalePromotionItem> masterPreSalePromotionItems = new List<MasterPreSalePromotionItem>()
                        {
                            FixtureFactory.Get().Build<MasterPreSalePromotionItem>()
                                   .With(o=>o.ID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.ExpireDate,new DateTime(2022,8,25))
                                   .With(o=>o.MasterPreSalePromotionID,masterPreSalePromotion.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterPreSalePromotionItem>()
                                   .With(o=>o.MasterPreSalePromotionID,masterPreSalePromotion.ID)
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.ExpireDate,new DateTime(2019,5,11))
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterPreSalePromotionItem>()
                                   .With(o=>o.MasterPreSalePromotionID,masterPreSalePromotion.ID)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,8,25))
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterPreSalePromotionItem>()
                                   .With(o=>o.MasterPreSalePromotionID,masterPreSalePromotion.ID)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,8,25))
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .Create()
                        };
                        IList<MasterPreSaleHouseModelItem> masterPreSalePromotionHouseItems = new List<MasterPreSaleHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterPreSaleHouseModelItem>()
                                   .With(o=>o.MasterPreSalePromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ModelID,models[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterPreSaleHouseModelItem>()
                                   .With(o=>o.MasterPreSalePromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ModelID,models[1].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterPreSalePromotions.AddAsync(masterPreSalePromotion);
                        await db.AddRangeAsync(masterPreSalePromotionItems);
                        await db.AddRangeAsync(masterPreSalePromotionHouseItems);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        PageParam pageParam = new PageParam();
                        MasterPreSalePromotionItemSortByParam sortByParam = new MasterPreSalePromotionItemSortByParam();

                        var resultsItem = await service.GetMasterPreSalePromotionItemListAsync(masterPreSalePromotion.ID, pageParam, sortByParam);
                        var resultModel = await service.GetMasterPreSalePromotionItemModelListAsync(new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"));
                        var dataCloneConfirm = await service.GetCloneMasterPreSalePromotionConfirmAsync(masterPreSalePromotion.ID);
                        Assert.NotNull(resultsItem);
                        Assert.NotNull(resultModel);
                        Assert.NotNull(dataCloneConfirm);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
