using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models.MST;
using Database.Models.PRM;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Services;
using Xunit;

namespace MST_Promotion.UnitTests
{
    public class MasterTransferPromotionServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async Task GetMasterTransferPromotionListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        MasterTransferPromotionListFilter filter = FixtureFactory.Get().Build<MasterTransferPromotionListFilter>().Create();
                        filter.PromotionStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "PromotionStatus")
                                                                          .Select(x => x.Key).FirstAsync();
                        PageParam pageParam = new PageParam();
                        MasterTransferPromotionSortByParam sortByParam = new MasterTransferPromotionSortByParam();
                        var results = await service.GetMasterTransferPromotionListAsync(filter, pageParam, sortByParam);

                        filter = new MasterTransferPromotionListFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterTransferPromotionSortBy)).Cast<MasterTransferPromotionSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterTransferPromotionSortByParam() { SortBy = item };
                            results = await service.GetMasterTransferPromotionListAsync(filter, pageParam, sortByParam);
                            Assert.NotNull(results.MasterTransferPromotionDTOs);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterTransferPromotionDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterTransferPromotion = new MasterTransferPromotionDTO();
                        masterTransferPromotion.Name = "Test";
                        masterTransferPromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterTransferPromotionAsync(masterTransferPromotion);

                        var result = await service.GetMasterTransferPromotionDetailAsync(resultCreate.Id.Value);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMasterTransferPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var project = await db.Projects.FirstAsync();
                        var masterTransferPromotion = new MasterTransferPromotionDTO();
                        masterTransferPromotion.Name = "Test";
                        masterTransferPromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterTransferPromotionAsync(masterTransferPromotion);
                        Assert.NotNull(resultCreate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterTransferPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var project = await db.Projects.FirstAsync();
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1").FirstOrDefaultAsync();

                        var masterTransferPromotion = new MasterTransferPromotionDTO();
                        masterTransferPromotion.Name = "Test";
                        masterTransferPromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterTransferPromotionAsync(masterTransferPromotion);
                        resultCreate.Name = "BBBBB";
                        resultCreate.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(masterCenterPromotionStatus);
                        resultCreate.StartDate = DateTime.Now;
                        resultCreate.EndDate = DateTime.Now;
                        var resultUpdate = await service.UpdateMasterTransferPromotionAsync(resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(resultUpdate);
                        Assert.Equal(resultCreate.Name, resultUpdate.Name);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteMasterTransferPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var project = await db.Projects.FirstAsync();
                        var masterTransferPromotion = new MasterTransferPromotionDTO();
                        masterTransferPromotion.Name = "Test";
                        masterTransferPromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterTransferPromotionAsync(masterTransferPromotion);

                        await service.DeleteMasterTransferPromotionAsync(resultCreate.Id.Value);
                        var result = await db.MasterTransferPromotions.FindAsync(resultCreate.Id.Value);
                        Assert.NotNull(result);
                        Assert.True(result.IsDeleted);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterTransferPromotionItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                      .Select(o => o.ID).FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                                                 .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                                 .With(o => o.IsDeleted, false)
                                                                 .Create();

                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();
                        MasterTransferPromotionItemSortByParam sortByParam = new MasterTransferPromotionItemSortByParam();
                        var results = await service.GetMasterTransferPromotionItemListAsync(masterTransferPromotion.ID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterTransferPromotionItemSortBy)).Cast<MasterTransferPromotionItemSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterTransferPromotionItemSortByParam() { SortBy = item };
                            results = await service.GetMasterTransferPromotionItemListAsync(masterTransferPromotion.ID, pageParam, sortByParam);
                            Assert.NotNull(results.MasterTransferPromotionItemDTOs);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterTransferPromotionItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                      .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
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
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);

                        var resultCreate = await service.CreateMasterTransferPromotionItemFromMaterialAsync(masterTransferPromotion.ID, resultpromotions.PromotionMaterialDTOs);

                        resultCreate[0].NameEN = "Test";
                        resultCreate[0].NameTH = "เทส";
                        resultCreate[0].UnitEN = "Test";

                        var resultUpdate = await service.UpdateMasterTransferPromotionItemAsync(masterTransferPromotion.ID, resultCreate[0].Id.Value, resultCreate[0]);
                        Assert.NotNull(resultUpdate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterTransferPromotionItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                      .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
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
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);

                        var resultCreate = await service.CreateMasterTransferPromotionItemFromMaterialAsync(masterTransferPromotion.ID, resultpromotions.PromotionMaterialDTOs);
                        foreach (var item in resultCreate)
                        {
                            item.NameEN = "xxxxxx";
                            item.NameTH = "xxxxxx";
                            item.UnitEN = "Test";
                        }
                        var resultsUpdate = await service.UpdateMasterTransferPromotionItemListAsync(masterTransferPromotion.ID, resultCreate);
                        Assert.NotNull(resultsUpdate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMasterTransferPromotionItemFromMaterialAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                      .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
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
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);

                        var resultCreate = await service.CreateMasterTransferPromotionItemFromMaterialAsync(masterTransferPromotion.ID, resultpromotions.PromotionMaterialDTOs);
                        Assert.NotNull(resultCreate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateSubMasterTransferPromotionItemFromMaterialAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                      .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
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
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);

                        var resultCreate = await service.CreateMasterTransferPromotionItemFromMaterialAsync(masterTransferPromotion.ID, resultpromotions.PromotionMaterialDTOs);

                        var resulltcreateSub = await service.CreateSubMasterTransferPromotionItemFromMaterialAsync(masterTransferPromotion.ID, resultCreate[0].Id.Value, resultpromotions.PromotionMaterialDTOs);
                        Assert.NotNull(resulltcreateSub);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterTransferPromotionItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                      .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
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
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);

                        var resultCreate = await service.CreateMasterTransferPromotionItemFromMaterialAsync(masterTransferPromotion.ID, resultpromotions.PromotionMaterialDTOs);

                        var model = await db.Models.Where(o => !o.IsDeleted).FirstAsync();
                        IList<MasterTransferHouseModelItem> masterTransferHouseModelItems = new List<MasterTransferHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferHouseModelItem>()
                                   .With(o=>o.MasterTransferPromotionItemID,resultCreate[0].Id.Value)
                                   .With(o=>o.ModelID,model.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        await db.AddRangeAsync(masterTransferHouseModelItems);
                        await db.SaveChangesAsync();

                        var resultsModelItem = await service.GetMasterTransferPromotionItemModelListAsync(resultCreate[0].Id.Value);
                        Assert.NotNull(resultsModelItem);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task AddMasterTransferPromotionItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                      .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
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
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);

                        var resultCreate = await service.CreateMasterTransferPromotionItemFromMaterialAsync(masterTransferPromotion.ID, resultpromotions.PromotionMaterialDTOs);

                        var models = await db.Models.Where(o => !o.IsDeleted).ToListAsync();
                        List<ModelListDTO> modelListDTOs = new List<ModelListDTO>()
                        {
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,models[0].ID)
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .Create(),
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,models[1].ID)
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .Create()
                        };
                        var resultAddModel = await service.AddMasterTransferPromotionItemModelListAsync(resultCreate[0].Id.Value, modelListDTOs);
                        var resultsModelItem = await service.GetMasterTransferPromotionItemModelListAsync(resultCreate[0].Id.Value);
                        Assert.NotNull(resultsModelItem);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterTransferPromotionFreeItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                   .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
                                          .Create();

                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();
                        MasterTransferPromotionFreeItemSortByParam sortByParam = new MasterTransferPromotionFreeItemSortByParam();
                        var results = await service.GetMasterTransferPromotionFreeItemListAsync(masterTransferPromotion.ID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterTransferPromotionFreeItemSortBy)).Cast<MasterTransferPromotionFreeItemSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterTransferPromotionFreeItemSortByParam() { SortBy = item };
                            results = await service.GetMasterTransferPromotionFreeItemListAsync(masterTransferPromotion.ID, pageParam, sortByParam);
                            Assert.NotNull(results.MasterTransferPromotionFreeItemDTOs);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMasterTransferPromotionFreeItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                   .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
                                          .Create();
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.SaveChangesAsync();

                        var masterCenterWhenPromotionReceive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1")
                                                                                    .FirstAsync();
                        //Put unit test here
                        var data = FixtureFactory.Get().Build<MasterTransferPromotionFreeItemDTO>()
                                       .With(o => o.NameTH, "เทส")
                                       .With(o => o.NameEN, "Test")
                                       .With(o => o.UnitTH, "บาท")
                                       .With(o => o.UnitEN, "Bath")
                                       .With(o => o.ReceiveDays, 6)
                                       .With(o => o.IsShowInContract, true)
                                       .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(masterCenterWhenPromotionReceive))
                                       .Create();

                        var results = await service.CreateMasterTransferPromotionFreeItemAsync(masterTransferPromotion.ID, data);
                        Assert.NotNull(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterTransferPromotionFreeItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                   .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
                                          .Create();
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.SaveChangesAsync();

                        var masterCenterWhenPromotionReceive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1")
                                                                                    .FirstAsync();
                        //Put unit test here
                        var data = FixtureFactory.Get().Build<MasterTransferPromotionFreeItemDTO>()
                                       .With(o => o.NameTH, "เทส")
                                       .With(o => o.NameEN, "Test")
                                       .With(o => o.UnitTH, "บาท")
                                       .With(o => o.UnitEN, "Bath")
                                       .With(o => o.ReceiveDays, 5)
                                       .With(o => o.IsShowInContract, true)
                                       .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(masterCenterWhenPromotionReceive))
                                       .Create();

                        var resultCreate = await service.CreateMasterTransferPromotionFreeItemAsync(masterTransferPromotion.ID, data);

                        List<MasterTransferPromotionFreeItemDTO> masterTransferPromotionFreeItems = new List<MasterTransferPromotionFreeItemDTO>();

                        masterTransferPromotionFreeItems.Add(resultCreate);

                        resultCreate.NameTH = "ทดสอบ";

                        var resultsUpdate = await service.UpdateMasterTransferPromotionFreeItemListAsync(masterTransferPromotion.ID, masterTransferPromotionFreeItems);
                        Assert.NotNull(resultsUpdate);


                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterTransferPromotionFreeItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                   .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
                                          .Create();
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.SaveChangesAsync();

                        var masterCenterWhenPromotionReceive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1")
                                                                                    .FirstAsync();
                        //Put unit test here
                        var data = FixtureFactory.Get().Build<MasterTransferPromotionFreeItemDTO>()
                                       .With(o => o.NameTH, "เทส")
                                       .With(o => o.NameEN, "Test")
                                       .With(o => o.UnitTH, "บาท")
                                       .With(o => o.UnitEN, "Bath")
                                       .With(o => o.ReceiveDays, 5)
                                       .With(o => o.IsShowInContract, true)
                                       .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(masterCenterWhenPromotionReceive))
                                       .Create();

                        var resultCreate = await service.CreateMasterTransferPromotionFreeItemAsync(masterTransferPromotion.ID, data);

                        resultCreate.Quantity = 5;
                        resultCreate.UnitTH = "หน่วย";
                        resultCreate.UnitEN = "Unit";

                        var resultUpdate = await service.UpdateMasterTransferPromotionFreeItemAsync(masterTransferPromotion.ID, resultCreate.Id.Value, resultCreate);
                        Assert.NotNull(resultUpdate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteMasterTransferPromotionFreeItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                   .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
                                          .Create();
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.SaveChangesAsync();

                        var masterCenterWhenPromotionReceive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1")
                                                                                    .FirstAsync();
                        //Put unit test here
                        var data = FixtureFactory.Get().Build<MasterTransferPromotionFreeItemDTO>()
                                       .With(o => o.NameTH, "เทส")
                                       .With(o => o.NameEN, "Test")
                                       .With(o => o.UnitTH, "บาท")
                                       .With(o => o.UnitEN, "Bath")
                                       .With(o => o.ReceiveDays, 5)
                                       .With(o => o.IsShowInContract, true)
                                       .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(masterCenterWhenPromotionReceive))
                                       .Create();

                        var resultCreate = await service.CreateMasterTransferPromotionFreeItemAsync(masterTransferPromotion.ID, data);

                        await service.DeleteMasterTransferPromotionFreeItemAsync(resultCreate.Id.Value);
                        var result = await db.MasterTransferPromotionFreeItems.FindAsync(resultCreate.Id.Value);
                        Assert.NotNull(result);
                        Assert.True(result.IsDeleted);

                        await tran.RollbackAsync();
                    }

                });
            }
        }

        [Fact]
        public async Task GetMasterTransferPromotionFreeItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                            .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
                                          .Create();
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.SaveChangesAsync();

                        var masterCenterWhenPromotionReceive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1")
                                                                                    .FirstAsync();
                        //Put unit test here
                        var data = FixtureFactory.Get().Build<MasterTransferPromotionFreeItemDTO>()
                                       .With(o => o.NameTH, "เทส")
                                       .With(o => o.NameEN, "Test")
                                       .With(o => o.UnitTH, "บาท")
                                       .With(o => o.UnitEN, "Bath")
                                       .With(o => o.ReceiveDays, 5)
                                       .With(o => o.IsShowInContract, true)
                                       .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(masterCenterWhenPromotionReceive))
                                       .Create();

                        var resultCreate = await service.CreateMasterTransferPromotionFreeItemAsync(masterTransferPromotion.ID, data);

                        var models = await db.Models.Where(o => !o.IsDeleted).ToListAsync();

                        List<ModelListDTO> modelListDTOs = new List<ModelListDTO>()
                        {
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,models[0].ID)
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .Create(),
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,models[1].ID)
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .Create()
                        };
                        var resultAddModel = await service.AddMasterTransferPromotionFreeItemModelListAsync(resultCreate.Id.Value, modelListDTOs);
                        var resultsModelItem = await service.GetMasterTransferPromotionFreeItemModelListAsync(resultCreate.Id.Value);
                        Assert.NotNull(resultsModelItem);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task AddMasterTransferPromotionFreeItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var masterCenterPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                            .FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                          .With(o => o.Name, "Test")
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.PromotionStatusMasterCenterID, masterCenterPromotionStatus.ID)
                                          .Create();
                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.SaveChangesAsync();

                        var masterCenterWhenPromotionReceive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1")
                                                                                    .FirstAsync();
                        //Put unit test here
                        var data = FixtureFactory.Get().Build<MasterTransferPromotionFreeItemDTO>()
                                       .With(o => o.NameTH, "เทส")
                                       .With(o => o.NameEN, "Test")
                                       .With(o => o.UnitTH, "บาท")
                                       .With(o => o.UnitEN, "Bath")
                                       .With(o => o.ReceiveDays, 5)
                                       .With(o => o.IsShowInContract, true)
                                       .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(masterCenterWhenPromotionReceive))
                                       .Create();

                        var resultCreate = await service.CreateMasterTransferPromotionFreeItemAsync(masterTransferPromotion.ID, data);

                        var models = await db.Models.Where(o => !o.IsDeleted).ToListAsync();

                        List<ModelListDTO> modelListDTOs = new List<ModelListDTO>()
                        {
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,models[0].ID)
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .Create(),
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,models[1].ID)
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .Create()
                        };
                        var resultAddModel = await service.AddMasterTransferPromotionFreeItemModelListAsync(resultCreate.Id.Value, modelListDTOs);
                        var resultsModelItem = await service.GetMasterTransferPromotionFreeItemModelListAsync(resultCreate.Id.Value);
                        Assert.NotNull(resultsModelItem);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMasterTransferCreditCardItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                      .Select(o => o.ID).FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                                                 .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                                 .With(o => o.IsDeleted, false)
                                                                 .Create();

                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();
                        MasterTransferCreditCardItemSortByParam sortByParam = new MasterTransferCreditCardItemSortByParam();
                        var results = await service.GetMasterTransferCreditCardItemAsync(masterTransferPromotion.ID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterTransferCreditCardItemSortBy)).Cast<MasterTransferCreditCardItemSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterTransferCreditCardItemSortByParam() { SortBy = item };
                            results = await service.GetMasterTransferCreditCardItemAsync(masterTransferPromotion.ID, pageParam, sortByParam);
                            Assert.NotNull(results.MasterTransferCreditCardItemDTOs);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterTransferCreditCardItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                      .Select(o => o.ID).FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                                                 .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                                 .With(o => o.IsDeleted, false)
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

                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
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
                        var resultCreate = await service.CreateMasterTransferCreditCardItemsTestAsync(masterTransferPromotion.ID, eDCFeeDTOs);
                        foreach (var item in resultCreate)
                        {
                            item.NameTH = "เทส";
                            item.NameEN = "Test";
                        }
                        var resultupdates = await service.UpdateMasterTransferCreditCardItemListAsync(masterTransferPromotion.ID, resultCreate);
                        Assert.NotNull(resultupdates);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMasterTransferCreditCardItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                      .Select(o => o.ID).FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                                                 .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                                 .With(o => o.IsDeleted, false)
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

                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
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
                        var resultCreate = await service.CreateMasterTransferCreditCardItemsTestAsync(masterTransferPromotion.ID, eDCFeeDTOs);
                        foreach (var item in resultCreate)
                        {
                            item.NameTH = "เทส";
                            item.NameEN = "Test";
                        }
                        var resultupdates = await service.UpdateMasterTransferCreditCardItemAsync(masterTransferPromotion.ID, resultCreate[0].Id.Value, resultCreate[0]);
                        Assert.NotNull(resultupdates);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteMasterTransferCreditCardItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                         .Select(o => o.ID).FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                                                 .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                                 .With(o => o.IsDeleted, false)
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

                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
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
                        var resultCreate = await service.CreateMasterTransferCreditCardItemsTestAsync(masterTransferPromotion.ID, eDCFeeDTOs);

                        await service.DeleteMasterTransferCreditCardItemAsync(resultCreate[0].Id.Value);
                        var result = await db.MasterTransferPromotionCreditCardItems.FindAsync(resultCreate[0].Id.Value);
                        Assert.NotNull(result);
                        Assert.True(result.IsDeleted);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CloneMasterTransferPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                         .Select(o => o.ID).FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterTransfer = new MasterTransferPromotionDTO();
                        masterTransfer.Name = "Test";
                        masterTransfer.Project = ProjectDTO.CreateFromModel(project);

                        var masterTransferPromotion = await service.CreateMasterTransferPromotionAsync(masterTransfer);

                        var models = await db.Models.Where(o => !o.IsDeleted).ToListAsync();
                        var masterCenterPromotionItemStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                                                                    .FirstAsync();
                        var masterCenterWhenPromotionReceive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1")
                                                                                    .FirstAsync();

                        //Put unit test here
                        IList<MasterTransferPromotionItem> masterTransferPromotionItems = new List<MasterTransferPromotionItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferPromotionItem>()
                                   .With(o=>o.ID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.Id.Value)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o=>o.ExpireDate,new DateTime(2022,8,25))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferPromotionItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.Id.Value)
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.ExpireDate,new DateTime(2011,8,25))
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferPromotionItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.Id.Value)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,8,25))
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferPromotionItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.Id.Value)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,8,25))
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create()
                        };
                        IList<MasterTransferHouseModelItem> masterTransferPromotionHouseItems = new List<MasterTransferHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferHouseModelItem>()
                                   .With(o=>o.MasterTransferPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ModelID,models[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferHouseModelItem>()
                                   .With(o=>o.MasterTransferPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ModelID,models[1].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterTransferPromotionFreeItem> masterTransferPromotionFreeItems = new List<MasterTransferPromotionFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferPromotionFreeItem>()
                                   .With(o=>o.ID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.Id.Value)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferPromotionFreeItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.Id.Value)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterTransferHouseModelFreeItem> masterTransferHouseModelFreeItems = new List<MasterTransferHouseModelFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferHouseModelFreeItem>()
                                   .With(o=>o.MasterTransferPromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o=>o.ModelID,models[2].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferHouseModelFreeItem>()
                                   .With(o=>o.MasterTransferPromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o=>o.ModelID,models[3].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterTransferPromotionCreditCardItem> masterTransferCreditCardItems = new List<MasterTransferPromotionCreditCardItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferPromotionCreditCardItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.Id.Value)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferPromotionCreditCardItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.Id.Value)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterTransferPromotionItems.AddRangeAsync(masterTransferPromotionItems);
                        await db.MasterTransferHouseModelItems.AddRangeAsync(masterTransferPromotionHouseItems);
                        await db.MasterTransferPromotionFreeItems.AddRangeAsync(masterTransferPromotionFreeItems);
                        await db.MasterTransferHouseModelFreeItems.AddRangeAsync(masterTransferHouseModelFreeItems);
                        await db.MasterTransferPromotionCreditCardItems.AddRangeAsync(masterTransferCreditCardItems);
                        await db.SaveChangesAsync();

                        MasterTransferPromotionListFilter filter = new MasterTransferPromotionListFilter();
                        PageParam pageParam = new PageParam();
                        MasterTransferPromotionSortByParam sortByParam = new MasterTransferPromotionSortByParam();
                        MasterTransferPromotionItemSortByParam sortByParamItem = new MasterTransferPromotionItemSortByParam();
                        MasterTransferPromotionFreeItemSortByParam sortByParamFreeItem = new MasterTransferPromotionFreeItemSortByParam();
                        MasterTransferCreditCardItemSortByParam sortByParamCreditCardItem = new MasterTransferCreditCardItemSortByParam();

                        var resultsItem = await service.GetMasterTransferPromotionItemListAsync(masterTransferPromotion.Id.Value, pageParam, sortByParamItem);
                        var resultModel = await service.GetMasterTransferPromotionItemModelListAsync(new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"));
                        var resultsFreeItems = await service.GetMasterTransferPromotionFreeItemListAsync(masterTransferPromotion.Id.Value, pageParam, sortByParamFreeItem);
                        var resultsModelFreeItems = await service.GetMasterTransferPromotionFreeItemModelListAsync(new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"));
                        var resultsCreditItems = await service.GetMasterTransferCreditCardItemAsync(masterTransferPromotion.Id.Value, pageParam, sortByParamCreditCardItem);
                        var dataClone = await service.CloneMasterTransferPromotionAsync(masterTransferPromotion.Id.Value);

                        var resultsItemClone = await service.GetMasterTransferPromotionItemListAsync(dataClone.Id.Value, pageParam, sortByParamItem);
                        var resultModelClone = await service.GetMasterTransferPromotionItemModelListAsync(resultsItemClone.MasterTransferPromotionItemDTOs.Where(o => o.NameEN == "Test").First().Id.Value);
                        var resultsFreeItemsClone = await service.GetMasterTransferPromotionFreeItemListAsync(dataClone.Id.Value, pageParam, sortByParamFreeItem);
                        var resultsModelFreeItemsClone = await service.GetMasterTransferPromotionFreeItemModelListAsync(resultsFreeItemsClone.MasterTransferPromotionFreeItemDTOs.Where(o => o.NameEN == "Test").First().Id.Value);
                        var resultsCreditItemsClone = await service.GetMasterTransferCreditCardItemAsync(dataClone.Id.Value, pageParam, sortByParamCreditCardItem);

                        var testResults = await service.GetMasterTransferPromotionListAsync(filter, pageParam, sortByParam);
                        Assert.NotNull(testResults);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateMasterTransferCreditCardItemsTestAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);

                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                      .Select(o => o.ID).FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                                                 .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                                 .With(o => o.IsDeleted, false)
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

                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
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
                        var resultCreate = await service.CreateMasterTransferCreditCardItemsTestAsync(masterTransferPromotion.ID, eDCFeeDTOs);
                        Assert.NotNull(resultCreate);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetCloneMasterTransferPromotionConfirmAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new MasterTransferPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                         .Select(o => o.ID).FirstAsync();
                        var masterTransferPromotion = FixtureFactory.Get().Build<MasterTransferPromotion>()
                                                                 .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                                 .With(o => o.IsDeleted, false)
                                                                 .Create();
                        var models = await db.Models.Where(o => !o.IsDeleted).ToListAsync();
                        var masterCenterPromotionItemStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                                                                    .FirstAsync();
                        var masterCenterWhenPromotionReceive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1")
                                                                                    .FirstAsync();

                        //Put unit test here
                        IList<MasterTransferPromotionItem> masterTransferPromotionItems = new List<MasterTransferPromotionItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferPromotionItem>()
                                   .With(o=>o.ID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.ID)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o=>o.ExpireDate,new DateTime(2022,8,25))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferPromotionItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.ID)
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.ExpireDate,new DateTime(2011,8,25))
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferPromotionItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.ID)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,8,25))
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferPromotionItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.ID)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,8,25))
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create()
                        };
                        IList<MasterTransferHouseModelItem> masterTransferPromotionHouseItems = new List<MasterTransferHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferHouseModelItem>()
                                   .With(o=>o.MasterTransferPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ModelID,models[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferHouseModelItem>()
                                   .With(o=>o.MasterTransferPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ModelID,models[1].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterTransferPromotionFreeItem> masterTransferPromotionFreeItems = new List<MasterTransferPromotionFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferPromotionFreeItem>()
                                   .With(o=>o.ID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferPromotionFreeItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,masterCenterWhenPromotionReceive.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterTransferHouseModelFreeItem> masterTransferHouseModelFreeItems = new List<MasterTransferHouseModelFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferHouseModelFreeItem>()
                                   .With(o=>o.MasterTransferPromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o=>o.ModelID,models[2].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferHouseModelFreeItem>()
                                   .With(o=>o.MasterTransferPromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o=>o.ModelID,models[3].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterTransferPromotionCreditCardItem> masterTransferCreditCardItems = new List<MasterTransferPromotionCreditCardItem>()
                        {
                            FixtureFactory.Get().Build<MasterTransferPromotionCreditCardItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.ID)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterTransferPromotionCreditCardItem>()
                                   .With(o=>o.MasterTransferPromotionID,masterTransferPromotion.ID)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,masterCenterPromotionItemStatus.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);
                        await db.MasterTransferPromotionItems.AddRangeAsync(masterTransferPromotionItems);
                        await db.MasterTransferHouseModelItems.AddRangeAsync(masterTransferPromotionHouseItems);
                        await db.MasterTransferPromotionFreeItems.AddRangeAsync(masterTransferPromotionFreeItems);
                        await db.MasterTransferHouseModelFreeItems.AddRangeAsync(masterTransferHouseModelFreeItems);
                        await db.MasterTransferPromotionCreditCardItems.AddRangeAsync(masterTransferCreditCardItems);
                        await db.SaveChangesAsync();
                        MasterTransferPromotionListFilter filter = new MasterTransferPromotionListFilter();
                        PageParam pageParam = new PageParam();
                        MasterTransferPromotionSortByParam sortByParam = new MasterTransferPromotionSortByParam();
                        MasterTransferPromotionItemSortByParam sortByParamItem = new MasterTransferPromotionItemSortByParam();
                        MasterTransferPromotionFreeItemSortByParam sortByParamFreeItem = new MasterTransferPromotionFreeItemSortByParam();
                        MasterTransferCreditCardItemSortByParam sortByParamCreditCardItem = new MasterTransferCreditCardItemSortByParam();

                        var resultsItem = await service.GetMasterTransferPromotionItemListAsync(masterTransferPromotion.ID, pageParam, sortByParamItem);
                        var resultModel = await service.GetMasterTransferPromotionItemModelListAsync(new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"));
                        var resultsFreeItems = await service.GetMasterTransferPromotionFreeItemListAsync(masterTransferPromotion.ID, pageParam, sortByParamFreeItem);
                        var resultsModelFreeItems = await service.GetMasterTransferPromotionFreeItemModelListAsync(new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"));
                        var resultsCreditItems = await service.GetMasterTransferCreditCardItemAsync(masterTransferPromotion.ID, pageParam, sortByParamCreditCardItem);
                        var dataCloneConfirm = await service.GetCloneMasterTransferPromotionConfirmAsync(masterTransferPromotion.ID);
                        Assert.NotNull(resultsItem);
                        Assert.NotNull(resultModel);
                        Assert.NotNull(resultsFreeItems);
                        Assert.NotNull(resultsModelFreeItems);
                        Assert.NotNull(resultsCreditItems);
                        Assert.NotNull(dataCloneConfirm);

                        await tran.RollbackAsync();
                    }
                });
            }
        }


    }
}
