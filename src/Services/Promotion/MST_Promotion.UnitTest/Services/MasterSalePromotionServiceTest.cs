using System;
using System.Collections.Generic;
using System.Linq;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models.MST;
using Database.Models.PRM;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Services;
using AutoFixture;

namespace MST_Promotion.UnitTests
{
    public class MasterSalePromotionServiceTest
    {

        [Fact]
        public async Task GetMasterSalePromotionListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);

                        MasterSalePromotionListFilter filter = FixtureFactory.Get().Build<MasterSalePromotionListFilter>().Create();
                        filter.PromotionStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "PromotionStatus")
                                                                          .Select(x => x.Key).FirstAsync();
                        PageParam pageParam = new PageParam();
                        MasterSalePromotionSortByParam sortByParam = new MasterSalePromotionSortByParam();
                        var results = await service.GetMasterSalePromotionListAsync(filter, pageParam, sortByParam);

                        filter = new MasterSalePromotionListFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterSalePromotionSortBy)).Cast<MasterSalePromotionSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterSalePromotionSortByParam() { SortBy = item };
                            results = await service.GetMasterSalePromotionListAsync(filter, pageParam, sortByParam);
                        }
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterSalePromotionDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var data = FixtureFactory.Get().Build<MasterSalePromotion>().With(o => o.IsDeleted, false).Create();
                        var service = new MasterSalePromotionService(db);
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterSalePromotion = new MasterSalePromotionDTO();
                        masterSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterSalePromotionAsync(masterSalePromotion);
                        var result = await service.GetMasterSalePromotionDetailAsync(resultCreate.Id.Value);
                        Assert.NotNull(result);

                        System.Diagnostics.Trace.WriteLine(JsonConvert.SerializeObject(result));
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterSalePromotionAsync()
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
                        var data = FixtureFactory.Get().Build<MasterSalePromotion>().With(o => o.IsDeleted, false).Create();
                        var service = new MasterSalePromotionService(db);
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterSalePromotion = new MasterSalePromotionDTO();
                        masterSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterSalePromotionAsync(masterSalePromotion);

                        resultCreate.Name = "กกก   กกกก11111";
                        resultCreate.StartDate = DateTime.Now;
                        resultCreate.EndDate = DateTime.Now;
                        resultCreate.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(masterCenterPromotionStatusID);

                        var result = await service.UpdateMasterSalePromotionAsync(resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task CreateMasterSalePromotionAsync()
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
                        var service = new MasterSalePromotionService(db);
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterSalePromotion = new MasterSalePromotionDTO();
                        masterSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var result = await service.CreateMasterSalePromotionAsync(masterSalePromotion);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteMasterSalePromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterSalePromotion = new MasterSalePromotionDTO();
                        masterSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        //Put unit test here
                        var resultCreate = await service.CreateMasterSalePromotionAsync(masterSalePromotion);
                        await service.DeleteMasterSalePromotionAsync(resultCreate.Id.Value);
                        var result = await db.MasterSalePromotions.FirstOrDefaultAsync(x => x.ID == resultCreate.Id.Value);
                        Assert.NotNull(result);
                        Assert.True(result.IsDeleted);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterSalePromotionItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                         .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();
                        MasterSalePromotionItemSortByParam sortByParam = new MasterSalePromotionItemSortByParam();
                        var results = await service.GetMasterSalePromotionItemListAsync(masterSalePromotion.ID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterSalePromotionItemSortBy)).Cast<MasterSalePromotionItemSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterSalePromotionItemSortByParam() { SortBy = item };
                            results = await service.GetMasterSalePromotionItemListAsync(masterSalePromotion.ID, pageParam, sortByParam);
                            Assert.NotNull(results.MasterSalePromotionItemDTOs);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterSalePromotionItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);

                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
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
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var resultsCreate = await service.CreateMasterSalePromotionItemFromMaterialAsync(masterSalePromotion.ID, dataPromotionMaterials);

                        resultsCreate[0].NameEN = "Test";
                        resultsCreate[0].NameTH = "เทส";

                        var result = await service.UpdateMasterSalePromotionItemAsync(masterSalePromotion.ID, resultsCreate[0].Id.Value, resultsCreate[0]);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterSalePromotionItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
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
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var resultsCreate = await service.CreateMasterSalePromotionItemFromMaterialAsync(masterSalePromotion.ID, dataPromotionMaterials);


                        foreach (var item in resultsCreate)
                        {
                            item.NameTH = "เทส";
                            item.NameEN = "test";
                            item.UnitEN = "test";
                        }
                        var resultUpdates = await service.UpdateMasterSalePromotionItemListAsync(masterSalePromotion.ID, resultsCreate);
                        Assert.NotNull(resultUpdates);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMasterSalePromotionItemFromMaterialAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
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

                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var results = await service.CreateMasterSalePromotionItemFromMaterialAsync(masterSalePromotion.ID, dataPromotionMaterials);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateSubMasterSalePromotionItemFromMaterialAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                              .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
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
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var results = await service.CreateMasterSalePromotionItemFromMaterialAsync(masterSalePromotion.ID, dataPromotionMaterials);


                        var masterSalePromotionItem = results.First();

                        var resultsSub = await service.CreateSubMasterSalePromotionItemFromMaterialAsync(masterSalePromotion.ID, masterSalePromotionItem.Id.Value, dataPromotionMaterials);
                        Assert.NotNull(resultsSub);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterSalePromotionItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
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
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();
                        filter.AgreementNo = "00000";
                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var resultsCreate = await service.CreateMasterSalePromotionItemFromMaterialAsync(masterSalePromotion.ID, dataPromotionMaterials);
                        var model = await db.Models.Where(o => !o.IsDeleted).FirstAsync();
                        IList<MasterSaleHouseModelItem> masterSaleHouseModelItems = new List<MasterSaleHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterSaleHouseModelItem>()
                                   .With(o=>o.MasterSalePromotionItemID,resultsCreate[0].Id)
                                   .With(o=>o.ModelID,model.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSaleHouseModelItem>()
                                   .With(o=>o.MasterSalePromotionItemID,resultsCreate[0].Id)
                                   .With(o=>o.ModelID,model.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        await db.AddRangeAsync(masterSaleHouseModelItems);
                        await db.SaveChangesAsync();
                        var resultsModelItem = await service.GetMasterSalePromotionItemModelListAsync(resultsCreate[0].Id.Value);
                        Assert.NotNull(resultsModelItem);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task AddMasterSalePromotionItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
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
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var resultsCreate = await service.CreateMasterSalePromotionItemFromMaterialAsync(masterSalePromotion.ID, dataPromotionMaterials);

                        var model = await db.Models.Where(o => !o.IsDeleted).FirstAsync();
                        List<ModelListDTO> modelListDTOs = new List<ModelListDTO>()
                        {
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,model.ID)
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .Create(),
                        };
                        var resultAddModel = await service.AddMasterSalePromotionItemModelListAsync(resultsCreate[0].Id.Value, modelListDTOs);
                        var resultsModelItem = await service.GetMasterSalePromotionItemModelListAsync(resultsCreate[0].Id.Value);
                        Assert.NotNull(resultsModelItem);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterSalePromotionFreeItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                            .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();
                        MasterSalePromotionFreeItemSortByParam sortByParam = new MasterSalePromotionFreeItemSortByParam();
                        var results = await service.GetMasterSalePromotionFreeItemListAsync(masterSalePromotion.ID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterSalePromotionFreeItemSortBy)).Cast<MasterSalePromotionFreeItemSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterSalePromotionFreeItemSortByParam() { SortBy = item };
                            results = await service.GetMasterSalePromotionFreeItemListAsync(masterSalePromotion.ID, pageParam, sortByParam);
                            Assert.NotNull(results);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMasterSalePromotionFreeItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                                            .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
                        var data = FixtureFactory.Get().Build<MasterSalePromotionFreeItemDTO>()
                                          .With(o => o.NameTH, "เทส")
                                          .With(o => o.NameEN, "Test")
                                          .With(o => o.UnitTH, "เทส")
                                          .With(o => o.UnitEN, "Test")
                                          .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                          .Create();
                        var results = await service.CreateMasterSalePromotionFreeItemAsync(masterSalePromotion.ID, data);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterSalePromotionFreeItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                      .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();

                        var data = FixtureFactory.Get().Build<MasterSalePromotionFreeItemDTO>()
                                           .With(o => o.NameTH, "เทส")
                                           .With(o => o.NameEN, "Test")
                                           .With(o => o.UnitTH, "เทส")
                                           .With(o => o.UnitEN, "Test")
                                           .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                           .Create();
                        var resultCreate = await service.CreateMasterSalePromotionFreeItemAsync(masterSalePromotion.ID, data);

                        resultCreate.Quantity = 5;
                        resultCreate.UnitTH = "หน่วย";
                        resultCreate.UnitEN = "Unit";

                        var listData = new List<MasterSalePromotionFreeItemDTO>();
                        listData.Add(resultCreate);

                        var results = await service.UpdateMasterSalePromotionFreeItemListAsync(masterSalePromotion.ID, listData);
                        Assert.NotNull(results);


                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterSalePromotionFreeItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                     .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();

                        var data = FixtureFactory.Get().Build<MasterSalePromotionFreeItemDTO>()
                                          .With(o => o.NameTH, "เทส")
                                          .With(o => o.NameEN, "Test")
                                          .With(o => o.UnitTH, "เทส")
                                          .With(o => o.UnitEN, "Test")
                                          .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                          .Create();
                        var resultCreate = await service.CreateMasterSalePromotionFreeItemAsync(masterSalePromotion.ID, data);

                        resultCreate.Quantity = 5;
                        resultCreate.UnitTH = "หน่วย";
                        resultCreate.UnitEN = "Unit";

                        var result = await service.UpdateMasterSalePromotionFreeItemAsync(masterSalePromotion.ID, resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteMasterSalePromotionFreeItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                    .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();

                        var data = FixtureFactory.Get().Build<MasterSalePromotionFreeItemDTO>()
                                           .With(o => o.NameTH, "เทส")
                                           .With(o => o.NameEN, "Test")
                                           .With(o => o.UnitTH, "เทส")
                                           .With(o => o.UnitEN, "Test")
                                           .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                           .Create();
                        var resultCreate = await service.CreateMasterSalePromotionFreeItemAsync(masterSalePromotion.ID, data);

                        await service.DeleteMasterSalePromotionFreeItemAsync(resultCreate.Id.Value);
                        var result = await db.MasterSalePromotionFreeItems.FirstAsync(x => x.ID == resultCreate.Id.Value);
                        Assert.True(result.IsDeleted);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterSalePromotionFreeItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();

                        var data = FixtureFactory.Get().Build<MasterSalePromotionFreeItemDTO>()
                                           .With(o => o.NameTH, "เทส")
                                           .With(o => o.NameEN, "Test")
                                           .With(o => o.UnitTH, "เทส")
                                           .With(o => o.UnitEN, "Test")
                                           .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                           .Create();
                        var resultCreate = await service.CreateMasterSalePromotionFreeItemAsync(masterSalePromotion.ID, data);

                        var model = await db.Models.Where(o => !o.IsDeleted).FirstAsync();
                        List<ModelListDTO> modelListDTOs = new List<ModelListDTO>()
                        {
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,model.ID)
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .Create(),
                        };
                        var resultAddModel = await service.AddMasterSalePromotionFreeItemModelListAsync(resultCreate.Id.Value, modelListDTOs);
                        var resultsModelItem = await service.GetMasterSalePromotionFreeItemModelListAsync(resultCreate.Id.Value);
                        Assert.NotNull(resultsModelItem);


                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task AddMasterSalePromotionFreeItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();

                        var data = FixtureFactory.Get().Build<MasterSalePromotionFreeItemDTO>()
                                           .With(o => o.NameTH, "เทส")
                                           .With(o => o.NameEN, "Test")
                                           .With(o => o.UnitTH, "เทส")
                                           .With(o => o.UnitEN, "Test")
                                           .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                           .Create();
                        var resultCreate = await service.CreateMasterSalePromotionFreeItemAsync(masterSalePromotion.ID, data);

                        var model = await db.Models.Where(o => !o.IsDeleted).FirstAsync();
                        List<ModelListDTO> modelListDTOs = new List<ModelListDTO>()
                        {
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,model.ID)
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .Create(),
                        };
                        var resultAddModel = await service.AddMasterSalePromotionFreeItemModelListAsync(resultCreate.Id.Value, modelListDTOs);
                        var resultsModelItem = await service.GetMasterSalePromotionFreeItemModelListAsync(resultCreate.Id.Value);
                        Assert.NotNull(resultsModelItem);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterSalePromotionCreditCardItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                            .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();
                        MasterSalePromotionCreditCardItemSortByParam sortByParam = new MasterSalePromotionCreditCardItemSortByParam();
                        var results = await service.GetMasterSalePromotionCreditCardItemAsync(masterSalePromotion.ID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterSalePromotionCreditCardItemSortBy)).Cast<MasterSalePromotionCreditCardItemSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterSalePromotionCreditCardItemSortByParam() { SortBy = item };
                            results = await service.GetMasterSalePromotionCreditCardItemAsync(masterSalePromotion.ID, pageParam, sortByParam);
                            Assert.NotNull(results.MasterSalePromotionCreditCardItemDTOs);
                        }
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterSalePromotionCreditCardItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                              .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        var banks = await db.Banks.Where(o => !o.IsDeleted).ToListAsync();
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").ToListAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").ToListAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").ToListAsync();


                        //Put unit test here
                        List<EDCFee> eDCFees = new List<EDCFee>()
                        {
                            FixtureFactory.Get().Build<EDCFee>()
                                   .With(o=>o.ID,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.BankID,banks[0].ID)
                                   .With(o=>o.PaymentCardTypeMasterCenterID,paymentCardType[0].ID)
                                   .With(o=>o.CreditCardTypeMasterCenterID,creditCardType[0].ID)
                                   .With(o=>o.CreditCardPaymentTypeMasterCenterID,creditCardPaymentType[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(eDCFees);
                        await db.SaveChangesAsync();

                        List<EDCFeeDTO> eDCFeeDTOs = new List<EDCFeeDTO>()
                        {
                            FixtureFactory.Get().Build<EDCFeeDTO>()
                                   .With(o=>o.Id,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.Bank,BankDropdownDTO.CreateFromModel(banks[0]))
                                   .With(o=>o.PaymentCardType,MasterCenterDropdownDTO.CreateFromModel(paymentCardType[0]))
                                   .With(o=>o.CreditCardType,MasterCenterDropdownDTO.CreateFromModel(creditCardType[0]))
                                   .With(o=>o.CreditCardPaymentType,MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType[0]))
                                   .Create(),
                        };
                        var resultCreate = await service.CreateMasterSalePromotionCreditCardItemsAsync(masterSalePromotion.ID, eDCFeeDTOs);
                        foreach (var item in resultCreate)
                        {
                            item.NameTH = "เทส";
                            item.NameEN = "Test";
                        }
                        var resultupdates = await service.UpdateMasterSalePromotionCreditCardItemListAsync(masterSalePromotion.ID, resultCreate);
                        Assert.NotNull(resultupdates);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterSalePromotionCreditCardItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        var banks = await db.Banks.Where(o => !o.IsDeleted).ToListAsync();
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").ToListAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").ToListAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").ToListAsync();


                        //Put unit test here
                        List<EDCFee> eDCFees = new List<EDCFee>()
                        {
                            FixtureFactory.Get().Build<EDCFee>()
                                   .With(o=>o.ID,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.BankID,banks[0].ID)
                                   .With(o=>o.PaymentCardTypeMasterCenterID,paymentCardType[0].ID)
                                   .With(o=>o.CreditCardTypeMasterCenterID,creditCardType[0].ID)
                                   .With(o=>o.CreditCardPaymentTypeMasterCenterID,creditCardPaymentType[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(eDCFees);
                        await db.SaveChangesAsync();

                        List<EDCFeeDTO> eDCFeeDTOs = new List<EDCFeeDTO>()
                        {
                            FixtureFactory.Get().Build<EDCFeeDTO>()
                                   .With(o=>o.Id,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.Bank,BankDropdownDTO.CreateFromModel(banks[0]))
                                   .With(o=>o.PaymentCardType,MasterCenterDropdownDTO.CreateFromModel(paymentCardType[0]))
                                   .With(o=>o.CreditCardType,MasterCenterDropdownDTO.CreateFromModel(creditCardType[0]))
                                   .With(o=>o.CreditCardPaymentType,MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType[0]))
                                   .Create(),
                        };
                        var resultCreate = await service.CreateMasterSalePromotionCreditCardItemsAsync(masterSalePromotion.ID, eDCFeeDTOs);

                        resultCreate[0].NameTH = "เทส";
                        resultCreate[0].NameEN = "Test";

                        var resultupdate = await service.UpdateMasterSalePromotionCreditCardItemAsync(masterSalePromotion.ID, resultCreate[0].Id.Value, resultCreate[0]);
                        Assert.NotNull(resultupdate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteMasterSalePromotionCreditCardItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        var banks = await db.Banks.Where(o => !o.IsDeleted).ToListAsync();
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").ToListAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").ToListAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").ToListAsync();


                        //Put unit test here
                        List<EDCFee> eDCFees = new List<EDCFee>()
                        {
                            FixtureFactory.Get().Build<EDCFee>()
                                   .With(o=>o.ID,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.BankID,banks[0].ID)
                                   .With(o=>o.PaymentCardTypeMasterCenterID,paymentCardType[0].ID)
                                   .With(o=>o.CreditCardTypeMasterCenterID,creditCardType[0].ID)
                                   .With(o=>o.CreditCardPaymentTypeMasterCenterID,creditCardPaymentType[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(eDCFees);
                        await db.SaveChangesAsync();

                        List<EDCFeeDTO> eDCFeeDTOs = new List<EDCFeeDTO>()
                        {
                            FixtureFactory.Get().Build<EDCFeeDTO>()
                                   .With(o=>o.Id,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.Bank,BankDropdownDTO.CreateFromModel(banks[0]))
                                   .With(o=>o.PaymentCardType,MasterCenterDropdownDTO.CreateFromModel(paymentCardType[0]))
                                   .With(o=>o.CreditCardType,MasterCenterDropdownDTO.CreateFromModel(creditCardType[0]))
                                   .With(o=>o.CreditCardPaymentType,MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType[0]))
                                   .Create(),
                        };
                        var resultCreate = await service.CreateMasterSalePromotionCreditCardItemsAsync(masterSalePromotion.ID, eDCFeeDTOs);

                        await service.DeleteMasterSalePromotionCreditCardItemAsync(resultCreate[0].Id.Value);
                        var result = await db.MasterSalePromotionCreditCardItems.FindAsync(resultCreate[0].Id.Value);
                        Assert.True(result.IsDeleted);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CloneMasterSalePromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();
                        var whenPromotionReceiveStatusMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
                        var promotionItemStatusMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                        .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var MasterBooking = new MasterSalePromotionDTO();
                        MasterBooking.Name = "Test";
                        MasterBooking.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(masterCenterPromotionStatusID);
                        MasterBooking.Project = ProjectDTO.CreateFromModel(project);
                        //Put unit test here
                        var masterSalePromotion = await service.CreateMasterSalePromotionAsync(MasterBooking);

                        //Put unit test here
                        List<MasterSalePromotionItem> masterSalePromotionItems = new List<MasterSalePromotionItem>()
                        {
                            FixtureFactory.Get().Build<MasterSalePromotionItem>()
                                   .With(o=>o.ID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.Id.Value)
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSalePromotionItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.Id.Value)
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.ExpireDate,new DateTime(2019,03,15))
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSalePromotionItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.Id.Value)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSalePromotionItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.Id.Value)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .Create()
                        };
                        List<MasterSaleHouseModelItem> masterSalePromotionHouseItems = new List<MasterSaleHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterSaleHouseModelItem>()
                                   .With(o=>o.MasterSalePromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSaleHouseModelItem>()
                                   .With(o=>o.MasterSalePromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        List<MasterSalePromotionFreeItem> masterSalePromotionFreeItems = new List<MasterSalePromotionFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterSalePromotionFreeItem>()
                                   .With(o=>o.ID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.Id.Value)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSalePromotionFreeItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.Id.Value)
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .Create(),
                        };
                        List<MasterSaleHouseModelFreeItem> masterSaleHouseModelFreeItems = new List<MasterSaleHouseModelFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterSaleHouseModelFreeItem>()
                                   .With(o=>o.MasterSalePromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSaleHouseModelFreeItem>()
                                   .With(o=>o.MasterSalePromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        List<MasterSalePromotionCreditCardItem> masterSalePromotionCreditCardItems = new List<MasterSalePromotionCreditCardItem>()
                        {
                            FixtureFactory.Get().Build<MasterSalePromotionCreditCardItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.Id.Value)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSalePromotionCreditCardItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.Id.Value)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterSalePromotionItems.AddRangeAsync(masterSalePromotionItems);
                        await db.MasterSaleHouseModelItems.AddRangeAsync(masterSalePromotionHouseItems);
                        await db.MasterSalePromotionFreeItems.AddRangeAsync(masterSalePromotionFreeItems);
                        await db.MasterSaleHouseModelFreeItems.AddRangeAsync(masterSaleHouseModelFreeItems);
                        await db.MasterSalePromotionCreditCardItems.AddRangeAsync(masterSalePromotionCreditCardItems);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();

                        MasterSalePromotionSortByParam sortParam = new MasterSalePromotionSortByParam();
                        MasterSalePromotionItemSortByParam sortParamItem = new MasterSalePromotionItemSortByParam();
                        MasterSalePromotionFreeItemSortByParam sortParamFreeItem = new MasterSalePromotionFreeItemSortByParam();
                        MasterSalePromotionCreditCardItemSortByParam sortParamCardItem = new MasterSalePromotionCreditCardItemSortByParam();

                        var resultsItem = await service.GetMasterSalePromotionItemListAsync(masterSalePromotion.Id.Value, pageParam, sortParamItem);
                        var resultModel = await service.GetMasterSalePromotionItemModelListAsync(new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"));
                        var resultsFreeItems = await service.GetMasterSalePromotionFreeItemListAsync(masterSalePromotion.Id.Value, pageParam, sortParamFreeItem);
                        var resultsModelFreeItems = await service.GetMasterSalePromotionFreeItemModelListAsync(new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"));
                        var resultsCreditItems = await service.GetMasterSalePromotionCreditCardItemAsync(masterSalePromotion.Id.Value, pageParam, sortParamCardItem);
                        var dataClone = await service.CloneMasterSalePromotionAsync(masterSalePromotion.Id.Value);

                        var resultsItemClone = await service.GetMasterSalePromotionItemListAsync(dataClone.Id.Value, pageParam, sortParamItem);
                        var resultModelClone = await service.GetMasterSalePromotionItemModelListAsync(resultsItemClone.MasterSalePromotionItemDTOs.Where(o => o.NameEN == "Test").First().Id.Value);
                        var resultsFreeItemsClone = await service.GetMasterSalePromotionFreeItemListAsync(dataClone.Id.Value, pageParam, sortParamFreeItem);
                        var resultsModelFreeItemsClone = await service.GetMasterSalePromotionFreeItemModelListAsync(resultsFreeItemsClone.MasterSalePromotionFreeItemDTOs.Where(o => o.NameEN == "Test").First().Id.Value);
                        var resultsCreditItemsClone = await service.GetMasterSalePromotionCreditCardItemAsync(dataClone.Id.Value, pageParam, sortParamCardItem);


                        MasterSalePromotionListFilter filter = new MasterSalePromotionListFilter();
                        var testResults = await service.GetMasterSalePromotionListAsync(filter, pageParam, sortParam);
                        Assert.NotNull(testResults);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMasterSalePromotionCreditCardItemsAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);

                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();

                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        var banks = await db.Banks.Where(o => !o.IsDeleted).ToListAsync();
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").ToListAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").ToListAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").ToListAsync();


                        //Put unit test here
                        List<EDCFee> eDCFees = new List<EDCFee>()
                        {
                            FixtureFactory.Get().Build<EDCFee>()
                                   .With(o=>o.ID,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.BankID,banks[0].ID)
                                   .With(o=>o.PaymentCardTypeMasterCenterID,paymentCardType[0].ID)
                                   .With(o=>o.CreditCardTypeMasterCenterID,creditCardType[0].ID)
                                   .With(o=>o.CreditCardPaymentTypeMasterCenterID,creditCardPaymentType[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(eDCFees);
                        await db.SaveChangesAsync();

                        List<EDCFeeDTO> eDCFeeDTOs = new List<EDCFeeDTO>()
                        {
                            FixtureFactory.Get().Build<EDCFeeDTO>()
                                   .With(o=>o.Id,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.Bank,BankDropdownDTO.CreateFromModel(banks[0]))
                                   .With(o=>o.PaymentCardType,MasterCenterDropdownDTO.CreateFromModel(paymentCardType[0]))
                                   .With(o=>o.CreditCardType,MasterCenterDropdownDTO.CreateFromModel(creditCardType[0]))
                                   .With(o=>o.CreditCardPaymentType,MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType[0]))
                                   .Create(),
                        };
                        var results = await service.CreateMasterSalePromotionCreditCardItemsAsync(masterSalePromotion.ID, eDCFeeDTOs);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetCloneMasterSalePromotionConfirmAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterSalePromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var masterSalePromotion = FixtureFactory.Get().Build<MasterSalePromotion>()
                                                            //.With(o => o.Status, PromotionStatus.Active)
                                                            .With(o => o.IsDeleted, false)
                                                            .Create();

                        //Put unit test here
                        IList<MasterSalePromotionItem> masterSalePromotionItems = new List<MasterSalePromotionItem>()
                        {
                            FixtureFactory.Get().Build<MasterSalePromotionItem>()
                                   .With(o=>o.ID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.ID)
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSalePromotionItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.ID)
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.ExpireDate,new DateTime(2019,03,15))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSalePromotionItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.ID)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSalePromotionItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.ID)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o => o.IsDeleted, false)
                                   .Create()
                        };
                        IList<MasterSaleHouseModelItem> masterSalePromotionHouseItems = new List<MasterSaleHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterSaleHouseModelItem>()
                                   .With(o=>o.MasterSalePromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSaleHouseModelItem>()
                                   .With(o=>o.MasterSalePromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterSalePromotionFreeItem> masterSalePromotionFreeItems = new List<MasterSalePromotionFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterSalePromotionFreeItem>()
                                   .With(o=>o.ID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSalePromotionFreeItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterSaleHouseModelFreeItem> masterSaleHouseModelFreeItems = new List<MasterSaleHouseModelFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterSaleHouseModelFreeItem>()
                                   .With(o=>o.MasterSalePromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSaleHouseModelFreeItem>()
                                   .With(o=>o.MasterSalePromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterSalePromotionCreditCardItem> masterSalePromotionCreditCardItems = new List<MasterSalePromotionCreditCardItem>()
                        {
                            FixtureFactory.Get().Build<MasterSalePromotionCreditCardItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterSalePromotionCreditCardItem>()
                                   .With(o=>o.MasterSalePromotionID,masterSalePromotion.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterSalePromotions.AddAsync(masterSalePromotion);
                        await db.AddRangeAsync(masterSalePromotionItems);
                        await db.AddRangeAsync(masterSalePromotionHouseItems);
                        await db.AddRangeAsync(masterSalePromotionFreeItems);
                        await db.AddRangeAsync(masterSaleHouseModelFreeItems);
                        await db.AddRangeAsync(masterSalePromotionCreditCardItems);
                        await db.SaveChangesAsync();

                        var dataCloneConfirm = await service.GetCloneMasterSalePromotionConfirmAsync(masterSalePromotion.ID);
                        Assert.NotNull(dataCloneConfirm);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
