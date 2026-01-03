using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models;
using Database.Models.DbQueries.PRM;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRM;
using ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Params.Outputs;
using Common.Helper.Logging;
using System.Data.Common;
using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace MST_Promotion.Services
{
    public class MasterTransferPromotionService : IMasterTransferPromotionService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }

        public MasterTransferPromotionService(DatabaseContext db)
        {
            logModel = new LogModel("MasterTransferPromotionService", null);
            this.DB = db;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358678/preview
        /// </summary>
        /// <returns>The master transfer promotion async.</returns>
        /// <param name="input">Input.</param>
        public async Task<MasterTransferPromotionDTO> CreateMasterTransferPromotionAsync(MasterTransferPromotionDTO input)
        {
            await input.ValidateAsync(DB);
            var masterCenterPromotionStatusActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                      .Select(o => o.ID)
                                                                      .FirstAsync();
            var masterCenterPromotionStatusInActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .Select(o => o.ID)
                                                                      .FirstAsync();

            MasterTransferPromotion model = new MasterTransferPromotion();
            input.ToModel(ref model);
            string year = Convert.ToString(DateTime.Today.Year);
            var key = "PT" + input.Project?.ProjectNo + year[2] + year[3];
            var type = "PRM.MasterTransferPromotion";
            var runningno = await DB.RunningNumberCounters.Where(o => o.Key == key && o.Type == type).FirstOrDefaultAsync();
            if (runningno == null)
            {
                var runningNumberCounter = new RunningNumberCounter
                {
                    Key = key,
                    Type = type,
                    Count = 1
                };
                await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
                await DB.SaveChangesAsync();

                model.PromotionNo = key + runningNumberCounter.Count.ToString("000");
                runningNumberCounter.Count++;
                DB.Entry(runningNumberCounter).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }
            else
            {
                model.PromotionNo = key + runningno.Count.ToString("000");
                runningno.Count++;
                DB.Entry(runningno).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }

            if (input.PromotionStatus?.Id == masterCenterPromotionStatusActiveID)
            {
                var allPromotionStatusActive = await DB.MasterTransferPromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != model.ID && o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
                                                                             .ToListAsync();

                allPromotionStatusActive.ForEach(o => o.PromotionStatusMasterCenterID = masterCenterPromotionStatusInActiveID);
                DB.MasterTransferPromotions.UpdateRange(allPromotionStatusActive);
                await DB.SaveChangesAsync();
            }

            await DB.MasterTransferPromotions.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetMasterTransferPromotionDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358680/preview
        /// </summary>
        /// <returns>The master transfer promotion detail async.</returns>
        /// <param name="id">MasterTransferPromotion.ID</param>
        public async Task<MasterTransferPromotionDTO> GetMasterTransferPromotionDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.MasterTransferPromotions.AsNoTracking().Where(o => o.ID == id)
                                                         .Include(o => o.Project)
                                                         .ThenInclude(o => o.Company)
                                                         .Include(o => o.PromotionStatus)
                                                         .Include(o => o.UpdatedBy)
                                                         .FirstAsync(cancellationToken);
            var result = MasterTransferPromotionDTO.CreateFromModel(model);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358678/preview
        /// </summary>
        /// <returns>The master transfer promotion list async.</returns>
        /// <param name="filter">Filter.</param>
        public async Task<MasterTransferPromotionPaging> GetMasterTransferPromotionListAsync(MasterTransferPromotionListFilter filter, PageParam pageParam, MasterTransferPromotionSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<MasterTransferPromotionQueryResult> query = DB.MasterTransferPromotions.AsNoTracking().Select(o =>
                                                                             new MasterTransferPromotionQueryResult
                                                                             {
                                                                                 Project = o.Project,
                                                                                 MasterTransferPromotion = o,
                                                                                 PromotionStatus = o.PromotionStatus,
                                                                                 UpdatedBy = o.UpdatedBy
                                                                             });

            #region Filter
            if (!string.IsNullOrEmpty(filter.PromotionNo))
            {
                query = query.Where(o => o.MasterTransferPromotion.PromotionNo.Contains(filter.PromotionNo));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(o => o.MasterTransferPromotion.Name.Contains(filter.Name));
            }
            if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
            {
                query = query.Where(o => o.Project.ID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.PromotionStatusKey))
            {
                var promotionStatusMasterCenterID = await DB.MasterCenters.AsNoTracking().Where(x => x.Key == filter.PromotionStatusKey
                                                                      && x.MasterCenterGroupKey == "PromotionStatus")
                                                                     .Select(x => x.ID).FirstAsync(cancellationToken);
                query = query.Where(x => x.MasterTransferPromotion.PromotionStatusMasterCenterID == promotionStatusMasterCenterID);
            }
            if (filter.TransferDiscountFrom != null)
            {
                query = query.Where(o => o.MasterTransferPromotion.TransferDiscount >= filter.TransferDiscountFrom);
            }
            if (filter.TransferDiscountTo != null)
            {
                query = query.Where(o => o.MasterTransferPromotion.TransferDiscount <= filter.TransferDiscountTo);
            }
            if (filter.TransferDiscountFrom != null && filter.TransferDiscountTo != null)
            {
                query = query.Where(o => o.MasterTransferPromotion.TransferDiscount >= filter.TransferDiscountFrom
                                    && o.MasterTransferPromotion.TransferDiscount <= filter.TransferDiscountTo);
            }
            if (filter.IsUsed != null)
            {
                query = query.Where(o => o.MasterTransferPromotion.IsUsed == filter.IsUsed);
            }
            if (filter.StartDateFrom != null)
            {
                query = query.Where(o => o.MasterTransferPromotion.StartDate >= filter.StartDateFrom);
            }
            if (filter.StartDateTo != null)
            {
                query = query.Where(o => o.MasterTransferPromotion.StartDate <= filter.StartDateTo);
            }
            if (filter.StartDateFrom != null && filter.StartDateTo != null)
            {
                query = query.Where(o => o.MasterTransferPromotion.StartDate >= filter.StartDateFrom
                                    && o.MasterTransferPromotion.StartDate <= filter.StartDateTo);
            }
            if (filter.EndDateFrom != null)
            {
                query = query.Where(o => o.MasterTransferPromotion.EndDate >= filter.EndDateFrom);
            }
            if (filter.EndDateTo != null)
            {
                query = query.Where(o => o.MasterTransferPromotion.EndDate <= filter.EndDateTo);
            }
            if (filter.EndDateFrom != null && filter.EndDateTo != null)
            {
                query = query.Where(o => o.MasterTransferPromotion.EndDate >= filter.EndDateFrom
                                    && o.MasterTransferPromotion.EndDate <= filter.EndDateTo);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.MasterTransferPromotion.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterTransferPromotion.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterTransferPromotion.Updated >= filter.UpdatedFrom && x.MasterTransferPromotion.Updated <= filter.UpdatedTo);
            }


            #endregion

            MasterTransferPromotionDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<MasterTransferPromotionQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => MasterTransferPromotionDTO.CreateFromQueryResult(o)).ToList();

            return new MasterTransferPromotionPaging()
            {
                PageOutput = pageOutput,
                MasterTransferPromotionDTOs = results
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358679/preview
        /// </summary>
        /// <returns>The master transfer promotion async.</returns>
        /// <param name="input">Input.</param>
        public async Task<MasterTransferPromotionDTO> UpdateMasterTransferPromotionAsync(Guid id, MasterTransferPromotionDTO input)
        {
            await input.ValidateAsync(DB, true);
            var model = await DB.MasterTransferPromotions.Where(o => o.ID == id).FirstAsync();
            var masterCenterPromotionStatusActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                      .Select(o => o.ID)
                                                                      .FirstAsync();
            var masterCenterPromotionStatusInActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .Select(o => o.ID)
                                                                      .FirstAsync();
            var countPromotionStatusActive = await DB.MasterTransferPromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != id && o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
                                                                              .CountAsync();
            ValidateException ex = new ValidateException();
            if (input.StartDate != null && input.EndDate != null)
            {
                int totalDay = input.EndDate.Value.Subtract(input.StartDate.Value).Days;
                if (totalDay > 180)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0038").FirstAsync();
                    string desc = "วันที่สิ้นสุดการใช้งาน";
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (input.PromotionStatus?.Id == masterCenterPromotionStatusActiveID)
            {
                if (countPromotionStatusActive < 1)
                {
                    input.ToModel(ref model);
                }
                else
                {
                    var allPromotionStatusActive = await DB.MasterTransferPromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != id
                                                                     && o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
                                                              .ToListAsync();

                    allPromotionStatusActive.ForEach(o => o.PromotionStatusMasterCenterID = masterCenterPromotionStatusInActiveID);
                    DB.MasterTransferPromotions.UpdateRange(allPromotionStatusActive);
                    await DB.SaveChangesAsync();
                    input.ToModel(ref model);
                }
            }
            else
            {
                input.ToModel(ref model);
            }

            if (ex.HasError)
            {
                throw ex;
            }

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetMasterTransferPromotionDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// ลบ Master โปรโอน
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358678/preview
        /// </summary>
        /// <returns>The master transfer promotion async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteMasterTransferPromotionAsync(Guid id)
        {
            var model = await DB.MasterTransferPromotions.FindAsync(id);
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master transfer promotion item list async.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<MasterTransferPromotionItemPaging> GetMasterTransferPromotionItemListAsync(Guid masterTransferPromotionID, PageParam pageParam, MasterTransferPromotionItemSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<MasterTransferPromotionItemQueryResult> query = DB.MasterTransferPromotionItems
                                                                     .Where(o => o.MasterTransferPromotionID == masterTransferPromotionID)
                                                                     .Select(o =>
                                                                     new MasterTransferPromotionItemQueryResult
                                                                     {
                                                                         PromotionMaterialItem = o.PromotionMaterialItem,
                                                                         MasterTransferPromotionItem = o,
                                                                         PromotionItemStatus = o.PromotionItemStatus,
                                                                         WhenPromotionReceive = o.WhenPromotionReceive,
                                                                         UpdatedBy = o.UpdatedBy,
                                                                         Created = o.Created
                                                                     });
            var statusDeleteFromSap = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MaterialItemStatus && o.Key == "102")
                            .Select(o => o.ID).FirstOrDefaultAsync(cancellationToken);

            MasterTransferPromotionItemDTO.SortBy(sortByParam, ref query);

            //var queryResults = await query.OrderByDescending(o => o.MasterTransferPromotionItem.IsUsed == false).ToListAsync();
            var queryResults = await query.ToListAsync(cancellationToken);
            //var results = queryResults.Select(o => MasterTransferPromotionItemDTO.CreateFromQueryResult(o)).ToList();
            var results = queryResults.Select(o => MasterTransferPromotionItemDTO.CreateFromQueryResult(o, statusDeleteFromSap)).ToList();

            List<MasterTransferPromotionItemDTO> subItems = new List<MasterTransferPromotionItemDTO>();
            foreach (var item in results.ToList())
            {
                if (item.MainPromotionItemID != null)
                {
                    subItems.Add(item);
                    results.Remove(item);
                }
            }

            var i = 0;
            foreach (var item in results.ToList())
            {
                var subs = subItems.Where(o => o.MainPromotionItemID == item.Id).ToList();
                if (subs.Count() > 0)
                {
                    MasterTransferPromotionItemDTO.SortByDTO(sortByParam, ref subs);
                    results.InsertRange(i + 1, subs);
                    i++;
                    i += subs.Count();
                }
                else
                {
                    i++;
                }
            }

            var pageOutput = PagingHelper.PagingList<MasterTransferPromotionItemDTO>(pageParam, ref results);

            return new MasterTransferPromotionItemPaging()
            {
                PageOutput = pageOutput,
                MasterTransferPromotionItemDTOs = results
            };
        }

        /// <summary>
        /// เรียกใช้ตอนกดปุ่ม "บันทึก"
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master transfer promotion item list async.</returns>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<MasterTransferPromotionItemDTO>> UpdateMasterTransferPromotionItemListAsync(Guid masterTransferPromotionID, List<MasterTransferPromotionItemDTO> inputs)
        {
            foreach (var item in inputs)
            {
                await item.ValidateAsync(DB);
            }
            var listMasterTransferPromotionItems = new List<MasterTransferPromotionItem>();
            var allMasterTransferPromotionItems = await DB.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == masterTransferPromotionID).ToListAsync();
            foreach (var item in inputs)
            {
                var existingItem = allMasterTransferPromotionItems.Where(o => o.ID == item.Id).FirstOrDefault();
                if (existingItem != null)
                {
                    item.ToModel(ref existingItem);
                    //if (existingItem.MainPromotionItemID == null)
                    //{
                    existingItem.TotalPrice = existingItem.Quantity * existingItem.PricePerUnit;
                    //}
                    listMasterTransferPromotionItems.Add(existingItem);
                }
            }

            DB.UpdateRange(listMasterTransferPromotionItems);
            await DB.SaveChangesAsync();

            var queryResults = await DB.MasterTransferPromotionItems
                                        .Where(o => o.MasterTransferPromotionID == masterTransferPromotionID)
                                        .Include(o => o.PromotionMaterialItem)
                                        .Include(o => o.PromotionItemStatus)
                                        .Include(o => o.WhenPromotionReceive)
                                        .Include(o => o.UpdatedBy)
                                        .ToListAsync();
            var results = queryResults.Select(o => MasterTransferPromotionItemDTO.CreateFromModel(o)).ToList();
            return results;
        }

        /// <summary>
        /// แก้ไขโปร ทีละรายการ
        /// </summary>
        /// <returns>The master transfer promotion item async.</returns>
        /// <param name="masterTransferPromotionID">Master transfer promotion identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<MasterTransferPromotionItemDTO> UpdateMasterTransferPromotionItemAsync(Guid masterTransferPromotionID, Guid masterTransferPromotionItemID, MasterTransferPromotionItemDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == masterTransferPromotionID && o.ID == masterTransferPromotionItemID).FirstAsync();
            input.ToModel(ref model);
            if (model.MainPromotionItemID != null)
            {
                model.TotalPrice = model.Quantity * model.PricePerUnit;
            }

            var childList = await DB.MasterTransferPromotionItems.Where(o => o.MainPromotionItemID == model.ID).ToListAsync();
            var childMasterTransferPromotionItem = new List<MasterTransferPromotionItem>();
            foreach (MasterTransferPromotionItem mpsp in childList)
            {
                mpsp.WhenPromotionReceiveMasterCenterID = input.WhenPromotionReceive?.Id;
                childMasterTransferPromotionItem.Add(mpsp);
            }

            DB.UpdateRange(childMasterTransferPromotionItem);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            if (input.PromotionItemStatus?.Key == "0") //InActive
            {
                var promotionItemStatusID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "0").Select(o => o.ID).FirstOrDefaultAsync();
                //Update Inactive all
                if (model.MainPromotionItemID != null) //input child >> update main and child
                {
                    var main = await DB.MasterTransferPromotionItems.Where(o => o.ID == model.MainPromotionItemID).FirstOrDefaultAsync();
                    main.PromotionItemStatusMasterCenterID = promotionItemStatusID;
                    DB.Entry(main).State = EntityState.Modified;

                    var childLists = await DB.MasterTransferPromotionItems.Where(o => o.MainPromotionItemID == model.MainPromotionItemID).ToListAsync();
                    var childMasterTransferPromotionItems = new List<MasterTransferPromotionItem>();
                    foreach (MasterTransferPromotionItem mpsp in childLists)
                    {
                        mpsp.PromotionItemStatusMasterCenterID = promotionItemStatusID;
                        childMasterTransferPromotionItems.Add(mpsp);
                    }
                    DB.UpdateRange(childMasterTransferPromotionItems);
                    await DB.SaveChangesAsync();
                }
                else //input main >> update child
                {
                    if (model.ID != null)
                    {
                        var childLists = await DB.MasterTransferPromotionItems.Where(o => o.MainPromotionItemID == model.ID).ToListAsync();
                        var childMasterTransferPromotionItems = new List<MasterTransferPromotionItem>();
                        foreach (MasterTransferPromotionItem mpsp in childLists)
                        {
                            mpsp.PromotionItemStatusMasterCenterID = promotionItemStatusID;
                            childMasterTransferPromotionItems.Add(mpsp);
                        }
                        DB.UpdateRange(childMasterTransferPromotionItems);
                        await DB.SaveChangesAsync();
                    }
                }
            }

            var dataResult = await DB.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == masterTransferPromotionID
                                                                        && o.ID == masterTransferPromotionItemID)
                                                                  .Include(o => o.PromotionMaterialItem)
                                                                    .Include(o => o.PromotionItemStatus)
                                                                    .Include(o => o.WhenPromotionReceive)
                                                                    .Include(o => o.UpdatedBy)
                                                                  .FirstAsync();
            var result = MasterTransferPromotionItemDTO.CreateFromModel(dataResult);
            return result;
        }

        /// <summary>
        /// ลบทีละอัน ตรงปุ่ม ถังขยะ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master transfer promotion item async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteMasterTransferPromotionItemAsync(Guid id)
        {
            var model = await DB.MasterTransferPromotionItems.FindAsync(id);
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            //Update Parent
            if (model.MainPromotionItemID != null)
            {
                var modelMain = await DB.MasterTransferPromotionItems.Where(x => x.ID == model.MainPromotionItemID).FirstAsync();
                //modelMain.PricePerUnit = (int)Math.Ceiling(modelMain.PricePerUnit - model.TotalPrice);
                modelMain.TotalPrice = (int)Math.Ceiling(modelMain.TotalPrice - model.TotalPrice);
                DB.UpdateRange(modelMain);
                await DB.SaveChangesAsync();
            }
        }

        /// <summary>
        /// เพิ่มรายการโปรโมชั่นโดยเลือกจาก Material ที่ดึงจาก SAP
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358682/preview
        /// </summary>
        /// <returns>The master transfer promotion item from material async.</returns>
        /// <param name="inputs">ส่งเฉพาะ Item ที่เลือก</param>
        public async Task<List<MasterTransferPromotionItemDTO>> CreateMasterTransferPromotionItemFromMaterialAsync(Guid masterTransferPromotionID, List<PromotionMaterialDTO> inputs)
        {
            var masterTransferPromotionItemsCreate = new List<MasterTransferPromotionItem>();
            var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
            var whenPromotionReceiveAfterContract = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
            foreach (var item in inputs)
            {
                await ValidatePromotionMaterial(masterTransferPromotionID, item);

                //if (await MasterTransferPromotionMaterialHasValue(item, masterTransferPromotionID))
                //{
                //    continue;
                //}

                MasterTransferPromotionItem model = new MasterTransferPromotionItem();
                item.ToMasterTransferPromotionItemModel(ref model);
                model.MasterTransferPromotionID = masterTransferPromotionID;
                model.MainPromotionItemID = null;
                model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
                model.WhenPromotionReceiveMasterCenterID = whenPromotionReceiveAfterContract.ID;
                var promotionMaterialItem = await DB.PromotionMaterialItems.Where(o => o.ID == item.Id).FirstOrDefaultAsync();
                // SapInfo
                if (promotionMaterialItem != null)
                {
                    model.StartDate = promotionMaterialItem.StartDate;
                    //model.ExpireDate = promotionMaterialItem.ExpireDate;
                    model.ItemNo = promotionMaterialItem.ItemNo;
                    model.BrandEN = promotionMaterialItem.BrandEN;
                    model.BrandTH = promotionMaterialItem.BrandTH;
                    model.UnitEN = promotionMaterialItem.UnitEN;
                    model.RemarkTH = promotionMaterialItem.RemarkTH;
                    model.RemarkEN = promotionMaterialItem.RemarkEN;
                    model.SpecEN = promotionMaterialItem.SpecEN;
                    model.SpecTH = promotionMaterialItem.SpecTH;
                    model.Plant = promotionMaterialItem.Plant;
                    model.SAPCompanyID = promotionMaterialItem.SAPCompanyID;
                    model.AgreementNo = promotionMaterialItem.AgreementNo;
                    model.SAPPurchasingOrg = promotionMaterialItem.SAPPurchasingOrg;
                    model.MaterialGroupKey = promotionMaterialItem.MaterialGroupKey;
                    model.DocType = promotionMaterialItem.DocType;
                    model.GLAccountNo = promotionMaterialItem.GLAccountNo;
                    model.MaterialPrice = promotionMaterialItem.Price;
                    model.MaterialBasePrice = promotionMaterialItem.BasePrice;
                    model.Vat = promotionMaterialItem.Vat;
                    model.SAPSaleTaxCode = promotionMaterialItem.SAPSaleTaxCode;
                    model.SAPBaseUnit = promotionMaterialItem.SAPBaseUnit;
                    model.SAPVendor = promotionMaterialItem.SAPVendor;
                    model.SAPPurchasingGroup = promotionMaterialItem.SAPPurchasingGroup;
                    model.MaterialCode = promotionMaterialItem.MaterialCode;
                    //model.TotalPrice = promotionMaterialItem.Price;
                    //model.PricePerUnit = promotionMaterialItem.Price;
                }
                masterTransferPromotionItemsCreate.Add(model);
            }
            await DB.AddRangeAsync(masterTransferPromotionItemsCreate);
            await DB.SaveChangesAsync();

            var createdIDs = masterTransferPromotionItemsCreate.Select(o => o.ID).ToList();
            var dataResult = await DB.MasterTransferPromotionItems.Where(o => createdIDs.Contains(o.ID))
                                                                  .Include(o => o.PromotionMaterialItem)
                                                                    .Include(o => o.PromotionItemStatus)
                                                                    .Include(o => o.WhenPromotionReceive)
                                                                    .Include(o => o.UpdatedBy)
                                                                  .ToListAsync();

            //HouseModel
            var masterTransferHouseModelItemsCreate = new List<MasterTransferHouseModelItem>();
            var projectID = await DB.MasterTransferPromotions.Where(o => o.ID == masterTransferPromotionID).Select(o => o.ProjectID).FirstOrDefaultAsync();
            var models = await DB.Models.Where(o => o.ProjectID == projectID).ToListAsync();
            foreach (var item in dataResult)
            {
                foreach (var itemModel in models)
                {
                    MasterTransferHouseModelItem modelH = new MasterTransferHouseModelItem();
                    modelH.MasterTransferPromotionItemID = item.ID;
                    modelH.ModelID = itemModel.ID;
                    masterTransferHouseModelItemsCreate.Add(modelH);
                }
            }

            await DB.AddRangeAsync(masterTransferHouseModelItemsCreate);
            await DB.SaveChangesAsync();

            var result = dataResult.Select(o => MasterTransferPromotionItemDTO.CreateFromModel(o)).ToList();
            return result;
        }

        /// <summary>
        /// เพิ่มรายการย่อยจากการเลือก Material 
        /// รายการหลักกับรายการย่อยใช้ DTO เดียวกัน เหมือนกับ Model
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358682/preview
        /// </summary>
        /// <returns>The sub master transfer promotion item from material async.</returns>
        /// <param name="inputs">ส่งมาเฉพาะรายการที่เลือกมาเท่านั้น</param>
        public async Task<List<MasterTransferPromotionItemDTO>> CreateSubMasterTransferPromotionItemFromMaterialAsync(Guid masterTransferPromotionID, Guid mainMasterTransferPromotionItemID, List<PromotionMaterialDTO> inputs)
        {
            var masterTransferPromotionItemsCreate = new List<MasterTransferPromotionItem>();
            var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
            var whenPromotionReceiveAfterContract = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
            decimal sumPriceChildOld = DB.MasterTransferPromotionItems.Where(o => o.MainPromotionItemID == mainMasterTransferPromotionItemID).Sum(o => o.TotalPrice);

            foreach (var item in inputs)
            {
                await ValidatePromotionMaterial(masterTransferPromotionID, item);
                if (await SubMasterTransferPromotionMaterialHasValue(item, mainMasterTransferPromotionItemID, masterTransferPromotionID))
                {
                    continue;
                }
                MasterTransferPromotionItem model = new MasterTransferPromotionItem();
                item.ToMasterTransferPromotionItemModel(ref model);
                model.MasterTransferPromotionID = masterTransferPromotionID;
                model.MainPromotionItemID = mainMasterTransferPromotionItemID;
                model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
                model.WhenPromotionReceiveMasterCenterID = whenPromotionReceiveAfterContract.ID;
                var promotionMaterialItem = await DB.PromotionMaterialItems.Where(o => o.ID == item.Id).FirstOrDefaultAsync();
                // SapInfo
                if (promotionMaterialItem != null)
                {
                    model.StartDate = promotionMaterialItem.StartDate;
                    //model.ExpireDate = promotionMaterialItem.ExpireDate;
                    model.ItemNo = promotionMaterialItem.ItemNo;
                    model.BrandEN = promotionMaterialItem.BrandEN;
                    model.BrandTH = promotionMaterialItem.BrandTH;
                    model.UnitEN = promotionMaterialItem.UnitEN;
                    model.RemarkTH = promotionMaterialItem.RemarkTH;
                    model.RemarkEN = promotionMaterialItem.RemarkEN;
                    model.SpecEN = promotionMaterialItem.SpecEN;
                    model.SpecTH = promotionMaterialItem.SpecTH;
                    model.Plant = promotionMaterialItem.Plant;
                    model.SAPCompanyID = promotionMaterialItem.SAPCompanyID;
                    model.AgreementNo = promotionMaterialItem.AgreementNo;
                    model.SAPPurchasingOrg = promotionMaterialItem.SAPPurchasingOrg;
                    model.MaterialGroupKey = promotionMaterialItem.MaterialGroupKey;
                    model.DocType = promotionMaterialItem.DocType;
                    model.GLAccountNo = promotionMaterialItem.GLAccountNo;
                    model.MaterialPrice = promotionMaterialItem.Price;
                    model.MaterialBasePrice = promotionMaterialItem.BasePrice;
                    model.Vat = promotionMaterialItem.Vat;
                    model.SAPSaleTaxCode = promotionMaterialItem.SAPSaleTaxCode;
                    model.SAPBaseUnit = promotionMaterialItem.SAPBaseUnit;
                    model.SAPVendor = promotionMaterialItem.SAPVendor;
                    model.SAPPurchasingGroup = promotionMaterialItem.SAPPurchasingGroup;
                    model.MaterialCode = promotionMaterialItem.MaterialCode;
                    //model.TotalPrice = promotionMaterialItem.Price;
                    //model.PricePerUnit = promotionMaterialItem.Price;
                }
                masterTransferPromotionItemsCreate.Add(model);
            }
            await DB.AddRangeAsync(masterTransferPromotionItemsCreate);
            await DB.SaveChangesAsync();

            //Calc Main
            decimal sumPriceChildNew = DB.MasterTransferPromotionItems.Where(o => o.MainPromotionItemID == mainMasterTransferPromotionItemID).Sum(o => o.TotalPrice);
            var masterMain = await DB.MasterTransferPromotionItems.Where(o => o.ID == mainMasterTransferPromotionItemID).FirstOrDefaultAsync();
            //masterMain.PricePerUnit = (int)Math.Ceiling((masterMain.PricePerUnit - sumPriceChildOld) + sumPriceChildNew);
            masterMain.TotalPrice = (int)Math.Ceiling((masterMain.TotalPrice - sumPriceChildOld) + sumPriceChildNew);

            DB.UpdateRange(masterMain);
            await DB.SaveChangesAsync();

            var createdIDs = masterTransferPromotionItemsCreate.Select(o => o.ID).ToList();
            var dataResult = await DB.MasterTransferPromotionItems.Where(o => createdIDs.Contains(o.ID))
                                                                  .Include(o => o.PromotionMaterialItem)
                                                                    .Include(o => o.PromotionItemStatus)
                                                                    .Include(o => o.WhenPromotionReceive)
                                                                    .Include(o => o.UpdatedBy)
                                                                  .ToListAsync();

            var result = dataResult.Select(o => MasterTransferPromotionItemDTO.CreateFromModel(o)).ToList();
            return result;
        }

        /// <summary>
        /// ดึงแบบบ้านจาก Item โปรโอน
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
        /// </summary>
        /// <returns>The master transfer promotion item model list async.</returns>
        /// <param name="masterTransferPromotionItemID">Master transfer promotion item identifier.</param>
        public async Task<List<ModelListDTO>> GetMasterTransferPromotionItemModelListAsync(Guid masterTransferPromotionItemID, CancellationToken cancellationToken = default)
        {
            var listModel = await DB.MasterTransferHouseModelItems.Where(o => o.MasterTransferPromotionItemID == masterTransferPromotionItemID).ToListAsync(cancellationToken);
            var results = new List<ModelListDTO>();
            foreach (var item in listModel)
            {
                ModelQueryResult model = await DB.Models.AsNoTracking().Where(o => o.ID == item.ModelID)
                                                        .Select(o =>
                                                                new ModelQueryResult
                                                                {
                                                                    Model = o,
                                                                    ModelShortName = o.ModelShortName,
                                                                    ModelType = o.ModelType,
                                                                    ModelUnitType = o.ModelUnitType,
                                                                    TypeOfRealEstate = o.TypeOfRealEstate,
                                                                    WaterElectricMeterPrice = DB.WaterElectricMeterPrices.Where(p => p.ModelID == o.ID).OrderByDescending(p => p.Version).FirstOrDefault()
                                                                })
                                                        .FirstOrDefaultAsync(cancellationToken);
                if (model != null)
                {
                    results.Add(ModelListDTO.CreateFromQueryResult(model));
                }
            }
            return results;
        }

        /// <summary>
        /// เพิ่มแบบบ้านเข้าไปใน Item โปรโอน
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
        /// </summary>
        /// <returns>The master transfer promotion item model list async.</returns>
        /// <param name="masterTransferPromotionItemID">Master transfer promotion item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<ModelListDTO>> AddMasterTransferPromotionItemModelListAsync(Guid masterTransferPromotionItemID, List<ModelListDTO> inputs)
        {
            var masterTransferHouseModelItemsCreate = new List<MasterTransferHouseModelItem>();
            var masterTransferHouseModelItemsUpdate = new List<MasterTransferHouseModelItem>();
            var masterTransferHouseModelItemsDelete = new List<MasterTransferHouseModelItem>();
            var masterTransferHouseModelItems = await DB.MasterTransferHouseModelItems.Where(o => o.MasterTransferPromotionItemID == masterTransferPromotionItemID).ToListAsync();

            foreach (var item in inputs)
            {
                var existingItem = masterTransferHouseModelItems.Where(o => o.ModelID == item.Id).FirstOrDefault();
                if (existingItem == null)
                {
                    MasterTransferHouseModelItem model = new MasterTransferHouseModelItem();
                    model.MasterTransferPromotionItemID = masterTransferPromotionItemID;
                    model.ModelID = item.Id;
                    masterTransferHouseModelItemsCreate.Add(model);
                }
                else
                {
                    masterTransferHouseModelItemsUpdate.Add(existingItem);
                }
            }
            foreach (var item in masterTransferHouseModelItems)
            {
                var existingInput = inputs.Where(o => o.Id == item.ModelID).FirstOrDefault();
                if (existingInput == null)
                {
                    item.IsDeleted = true;
                    masterTransferHouseModelItemsDelete.Add(item);
                }
            }
            DB.UpdateRange(masterTransferHouseModelItemsUpdate);
            DB.UpdateRange(masterTransferHouseModelItemsDelete);
            await DB.AddRangeAsync(masterTransferHouseModelItemsCreate);
            await DB.SaveChangesAsync();

            var results = await this.GetMasterTransferPromotionItemModelListAsync(masterTransferPromotionItemID);
            return results;
        }

        /// <summary>
        /// ดึงรายการที่ไม่ต้องจัดซื้อ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master transfer promotion free item list async.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<MasterTransferPromotionFreeItemPaging> GetMasterTransferPromotionFreeItemListAsync(Guid masterTransferPromotionID, PageParam pageParam, MasterTransferPromotionFreeItemSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<MasterTransferPromotionFreeItemQueryResult> query = DB.MasterTransferPromotionFreeItems.AsNoTracking()
                                                                     .Where(o => o.MasterTransferPromotionID == masterTransferPromotionID)
                                                                     .Select(o =>
                                                                     new MasterTransferPromotionFreeItemQueryResult
                                                                     {
                                                                         MasterTransferPromotionFreeItem = o,
                                                                         WhenPromotionReceive = o.WhenPromotionReceive,
                                                                         UpdatedBy = o.UpdatedBy
                                                                     });

            MasterTransferPromotionFreeItemDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<MasterTransferPromotionFreeItemQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => MasterTransferPromotionFreeItemDTO.CreateFromQueryResult(o)).ToList();

            return new MasterTransferPromotionFreeItemPaging()
            {
                PageOutput = pageOutput,
                MasterTransferPromotionFreeItemDTOs = results
            };
        }

        /// <summary>
        /// สร้างรายการที่ไม่ต้องจัดซื้อ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master transfer promotion free item async.</returns>
        /// <param name="input">Input.</param>
        public async Task<MasterTransferPromotionFreeItemDTO> CreateMasterTransferPromotionFreeItemAsync(Guid masterTransferPromotionID, MasterTransferPromotionFreeItemDTO input)
        {
            await input.ValidateAsync(DB);
            MasterTransferPromotionFreeItem model = new MasterTransferPromotionFreeItem();
            input.ToModel(ref model);
            model.MasterTransferPromotionID = masterTransferPromotionID;
            await DB.MasterTransferPromotionFreeItems.AddAsync(model);
            await DB.SaveChangesAsync();

            model = await DB.MasterTransferPromotionFreeItems
                .Include(o => o.WhenPromotionReceive)
                .Include(o => o.UpdatedBy)
                .FirstAsync(o => o.ID == model.ID);

            //HouseModel
            var masterTransferHouseModelFreeItemsCreate = new List<MasterTransferHouseModelFreeItem>();
            var projectID = await DB.MasterTransferPromotions.Where(o => o.ID == masterTransferPromotionID).Select(o => o.ProjectID).FirstOrDefaultAsync();
            var models = await DB.Models.Where(o => o.ProjectID == projectID).ToListAsync();

            foreach (var itemModel in models)
            {
                MasterTransferHouseModelFreeItem modelH = new MasterTransferHouseModelFreeItem();
                modelH.MasterTransferPromotionFreeItemID = model.ID;
                modelH.ModelID = itemModel.ID;
                masterTransferHouseModelFreeItemsCreate.Add(modelH);
            }

            await DB.AddRangeAsync(masterTransferHouseModelFreeItemsCreate);
            await DB.SaveChangesAsync();

            var result = MasterTransferPromotionFreeItemDTO.CreateFromModel(model);
            return result;
        }

        /// <summary>
        /// แก้ไขรายการที่ไม่ต้องจัดซื้อ (แบบที่ละหลายรายการ)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master transfer promotion free item list async.</returns>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<MasterTransferPromotionFreeItemDTO>> UpdateMasterTransferPromotionFreeItemListAsync(Guid masterTransferPromotionID, List<MasterTransferPromotionFreeItemDTO> inputs)
        {
            foreach (var item in inputs)
            {
                await item.ValidateAsync(DB);
            }
            var masterTransferPromotionFreeItemsUpdate = new List<MasterTransferPromotionFreeItem>();
            var allMasterTransferPromotionFreeItem = await DB.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == masterTransferPromotionID).ToListAsync();
            foreach (var item in inputs)
            {
                var existingItem = allMasterTransferPromotionFreeItem.Where(o => o.ID == item.Id).FirstOrDefault();
                if (existingItem != null)
                {
                    item.ToModel(ref existingItem);
                    masterTransferPromotionFreeItemsUpdate.Add(existingItem);
                }
            }

            DB.UpdateRange(masterTransferPromotionFreeItemsUpdate);
            await DB.SaveChangesAsync();

            var updatedIDs = masterTransferPromotionFreeItemsUpdate.Select(o => o.ID).ToList();
            var dataResults = await DB.MasterTransferPromotionFreeItems
                .Include(o => o.WhenPromotionReceive)
                .Include(o => o.UpdatedBy)
                .Where(o => updatedIDs.Contains(o.ID)).ToListAsync();

            var listresult = dataResults.Select(o => MasterTransferPromotionFreeItemDTO.CreateFromModel(o)).ToList();
            return listresult;
        }

        /// <summary>
        /// แก้ไขรายการที่ไม่ต้องจัดซื้อ (แบบที่ละรายการ)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master Transfer promotion free item async.</returns>
        /// <param name="masterTransferPromotionID">Master Transfer promotion identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<MasterTransferPromotionFreeItemDTO> UpdateMasterTransferPromotionFreeItemAsync(Guid masterTransferPromotionID, Guid masterTransferPromotionFreeItemID, MasterTransferPromotionFreeItemDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == masterTransferPromotionID && o.ID == masterTransferPromotionFreeItemID).FirstAsync();
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var dataResult = await DB.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == masterTransferPromotionID
                                                                        && o.ID == masterTransferPromotionFreeItemID)
                                                                    .Include(o => o.WhenPromotionReceive)
                                                                    .Include(o => o.UpdatedBy)
                                                                  .FirstAsync();
            var result = MasterTransferPromotionFreeItemDTO.CreateFromModel(dataResult);
            return result;
        }

        /// <summary>
        /// ลบรายการที่ไม่ต้องจัดซื้อ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master transfer promotion free item async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteMasterTransferPromotionFreeItemAsync(Guid id)
        {
            var model = await DB.MasterTransferPromotionFreeItems.FindAsync(id);
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// ดึงแบบบ้านของ Item ที่ไม่ต้องจัดซื้อ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
        /// </summary>
        /// <returns>The master transfer promotion free item model list async.</returns>
        /// <param name="masterTransferPromotionItemID">Master transfer promotion item identifier.</param>
        public async Task<List<ModelListDTO>> GetMasterTransferPromotionFreeItemModelListAsync(Guid masterTransferPromotionFreeItemID, CancellationToken cancellationToken = default)
        {
            var listModel = await DB.MasterTransferHouseModelFreeItems.Where(o => o.MasterTransferPromotionFreeItemID == masterTransferPromotionFreeItemID).ToListAsync(cancellationToken);
            var results = new List<ModelListDTO>();
            foreach (var item in listModel)
            {
                ModelQueryResult model = await DB.Models.AsNoTracking().Where(o => o.ID == item.ModelID)
                                                        .Select(o =>
                                                                new ModelQueryResult
                                                                {
                                                                    Model = o,
                                                                    ModelShortName = o.ModelShortName,
                                                                    ModelType = o.ModelType,
                                                                    ModelUnitType = o.ModelUnitType,
                                                                    TypeOfRealEstate = o.TypeOfRealEstate,
                                                                    WaterElectricMeterPrice = DB.WaterElectricMeterPrices.Where(p => p.ModelID == o.ID).OrderByDescending(p => p.Version).FirstOrDefault()
                                                                })
                                                        .FirstOrDefaultAsync(cancellationToken);
                if (model != null)
                {
                    results.Add(ModelListDTO.CreateFromQueryResult(model));
                }
            }
            return results;
        }

        /// <summary>
        /// เพิ่มแบบบ้านไปใน Item ที่ไม่ต้องจัดซื้อ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
        /// </summary>
        /// <returns>The master transfer promotion free item model list async.</returns>
        /// <param name="masterTransferPromotionFreeItemID">Master transfer promotion free item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<ModelListDTO>> AddMasterTransferPromotionFreeItemModelListAsync(Guid masterTransferPromotionFreeItemID, List<ModelListDTO> inputs)
        {
            var masterTransferHouseModelFreeItemsCreate = new List<MasterTransferHouseModelFreeItem>();
            var masterTransferHouseModelFreeItemsUpdate = new List<MasterTransferHouseModelFreeItem>();
            var masterTransferHouseModelFreeItemsDelete = new List<MasterTransferHouseModelFreeItem>();
            var masterTransferHouseModelFreeItems = await DB.MasterTransferHouseModelFreeItems.Where(o => o.MasterTransferPromotionFreeItemID == masterTransferPromotionFreeItemID).ToListAsync();

            foreach (var item in inputs)
            {
                var existingItem = masterTransferHouseModelFreeItems.Where(o => o.ModelID == item.Id).FirstOrDefault();
                if (existingItem == null)
                {
                    MasterTransferHouseModelFreeItem model = new MasterTransferHouseModelFreeItem();
                    model.MasterTransferPromotionFreeItemID = masterTransferPromotionFreeItemID;
                    model.ModelID = item.Id;
                    masterTransferHouseModelFreeItemsCreate.Add(model);
                }
                else
                {
                    masterTransferHouseModelFreeItemsUpdate.Add(existingItem);
                }
            }
            foreach (var item in masterTransferHouseModelFreeItems)
            {
                var existingInput = inputs.Where(o => o.Id == item.ModelID).FirstOrDefault();
                if (existingInput == null)
                {
                    item.IsDeleted = true;
                    masterTransferHouseModelFreeItemsDelete.Add(item);
                }
            }
            DB.UpdateRange(masterTransferHouseModelFreeItemsUpdate);
            DB.UpdateRange(masterTransferHouseModelFreeItemsDelete);
            await DB.AddRangeAsync(masterTransferHouseModelFreeItemsCreate);
            await DB.SaveChangesAsync();

            var results = await this.GetMasterTransferPromotionFreeItemModelListAsync(masterTransferPromotionFreeItemID);
            return results;
        }

        /// <summary>
        /// ดึงรายการค่าธรรมเนียมรูดบัตร
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
        /// </summary>
        /// <returns>The master Transfer credit card item async.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<MasterTransferCreditCardItemPaging> GetMasterTransferCreditCardItemAsync(Guid masterTransferPromotionID, PageParam pageParam, MasterTransferCreditCardItemSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<MasterTransferCreditCardItemQueryResult> query = DB.MasterTransferPromotionCreditCardItems.AsNoTracking()
                                                                     .Where(o => o.MasterTransferPromotionID == masterTransferPromotionID)
                                                                     .OrderBy(o => o.BankName)
                                                                     .Select(o =>
                                                                     new MasterTransferCreditCardItemQueryResult
                                                                     {
                                                                         MasterTransferCreditCardItem = o,
                                                                         Bank = o.Bank,
                                                                         EDCFee = o.EDCFee,
                                                                         PromotionItemStatus = o.PromotionItemStatus,
                                                                         UpdatedBy = o.UpdatedBy,
                                                                         BankName = o.BankName
                                                                     });

            MasterTransferCreditCardItemDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<MasterTransferCreditCardItemQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => MasterTransferCreditCardItemDTO.CreateFromQueryResult(o)).ToList();

            return new MasterTransferCreditCardItemPaging()
            {
                PageOutput = pageOutput,
                MasterTransferCreditCardItemDTOs = results
            };
        }

        /// <summary>
        /// แก้ไขรายการค่าธรรมเนียมรูดบัตร (ทีละหลายรายการ)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
        /// </summary>
        /// <returns>The master Transfer credit card item list async.</returns>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<MasterTransferCreditCardItemDTO>> UpdateMasterTransferCreditCardItemListAsync(Guid masterTransferPromotionID, List<MasterTransferCreditCardItemDTO> inputs)
        {
            foreach (var item in inputs)
            {
                await item.ValidateAsync(DB);
            }
            var masterTransferCreditCardItemsUpdate = new List<MasterTransferPromotionCreditCardItem>();
            var masterTransferCreditCardItems = await DB.MasterTransferPromotionCreditCardItems.Where(o => o.MasterTransferPromotionID == masterTransferPromotionID).ToListAsync();
            foreach (var item in inputs)
            {
                var existingItem = masterTransferCreditCardItems.Where(o => o.ID == item.Id).FirstOrDefault();
                if (existingItem != null)
                {
                    item.ToModel(ref existingItem);
                    masterTransferCreditCardItemsUpdate.Add(existingItem);
                }
            }

            DB.UpdateRange(masterTransferCreditCardItemsUpdate);
            await DB.SaveChangesAsync();

            var updatedIDs = masterTransferCreditCardItemsUpdate.Select(o => o.ID).ToList();
            var dataResults = await DB.MasterTransferPromotionCreditCardItems
                .Include(o => o.Bank)
                .Include(o => o.EDCFee)
                .Include(o => o.PromotionItemStatus)
                .Include(o => o.UpdatedBy)
                .Where(o => updatedIDs.Contains(o.ID))
                .ToListAsync();

            var listresult = dataResults.Select(o => MasterTransferCreditCardItemDTO.CreateFromModel(o)).ToList();
            return listresult;
        }

        /// <summary>
        /// แก้ไขรายการค่าธรรมเนียมรูดบัตร (ทีละรายการ)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
        /// </summary>
        /// <returns>The master Transfer credit card item async.</returns>
        /// <param name="masterTransferPromotionID">Master Transfer promotion identifier.</param>
        /// <param name="masterTransferCreditCardItemID">Master Transfer promotion credit card item identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<MasterTransferCreditCardItemDTO> UpdateMasterTransferCreditCardItemAsync(Guid masterTransferPromotionID, Guid masterTransferCreditCardItemID, MasterTransferCreditCardItemDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.MasterTransferPromotionCreditCardItems.Where(o => o.MasterTransferPromotionID == masterTransferPromotionID && o.ID == masterTransferCreditCardItemID).FirstAsync();
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var dataResult = await DB.MasterTransferPromotionCreditCardItems.Where(o => o.MasterTransferPromotionID == masterTransferPromotionID
                                                                        && o.ID == masterTransferCreditCardItemID)
                                                                    .Include(o => o.Bank)
                                                                    .Include(o => o.EDCFee)
                                                                    .Include(o => o.PromotionItemStatus)
                                                                    .Include(o => o.UpdatedBy)
                                                                  .FirstAsync();
            var result = MasterTransferCreditCardItemDTO.CreateFromModel(dataResult);
            return result;
        }

        /// <summary>
        /// ลบรายการค่าธรรมเนียมรูดบัตร
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
        /// </summary>
        /// <returns>The master Transfer credit card item async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteMasterTransferCreditCardItemAsync(Guid id)
        {
            var model = await DB.MasterTransferPromotionCreditCardItems.FindAsync(id);
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358686/preview
        /// EDCFee จะดึงมาจาก GET EDCFees API โดยมีเงื่อนไขดังนี้
        /// บัตรที่รูด เป็นธนาคารเดียวกัน, รูปแบบการรูดเฉพาะการผ่อน
        /// </summary>
        /// <returns>The master Transfer credit card items.</returns>
        /// <param name="masterTransferPromotionID">Master transfer promotion identifier.</param>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<MasterTransferCreditCardItemDTO>> CreateMasterTransferCreditCardItemsAsync(Guid masterTransferPromotionID, List<PromotionMaterialDTO> inputs)
        {
            var masterTransferCreditCardItemsCreate = new List<MasterTransferPromotionCreditCardItem>();
            var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
            int i = 1;
            foreach (var item in inputs)
            {
                MasterTransferPromotionCreditCardItem model = new MasterTransferPromotionCreditCardItem();
                item.ToMasterTransferPromotionCreditCardItemModel(ref model);
                model.MasterTransferPromotionID = masterTransferPromotionID;
                model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
                model.Order = 0;
                masterTransferCreditCardItemsCreate.Add(model);
                i++;
            }

            await DB.AddRangeAsync(masterTransferCreditCardItemsCreate);
            await DB.SaveChangesAsync();

            var updatedIDs = masterTransferCreditCardItemsCreate.Select(o => o.ID).ToList();
            var dataResults = await DB.MasterTransferPromotionCreditCardItems
                .Include(o => o.Bank)
                .Include(o => o.EDCFee)
                .Include(o => o.PromotionItemStatus)
                .Include(o => o.UpdatedBy)
                .Where(o => updatedIDs.Contains(o.ID))
                .ToListAsync();

            var results = dataResults.Select(o => MasterTransferCreditCardItemDTO.CreateFromModel(o)).ToList();
            return results;
        }
        public async Task<List<MasterTransferCreditCardItemDTO>> CreateMasterTransferCreditCardItemsTestAsync(Guid masterTransferPromotionID, List<EDCFeeDTO> inputs)
        {
            var masterTransferCreditCardItemsCreate = new List<MasterTransferPromotionCreditCardItem>();
            var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
            foreach (var item in inputs)
            {
                MasterTransferPromotionCreditCardItem model = new MasterTransferPromotionCreditCardItem();
                item.ToMasterTransferCreditCardItemModel(ref model);
                model.MasterTransferPromotionID = masterTransferPromotionID;
                model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
                masterTransferCreditCardItemsCreate.Add(model);
            }
            await DB.AddRangeAsync(masterTransferCreditCardItemsCreate);
            await DB.SaveChangesAsync();

            var updatedIDs = masterTransferCreditCardItemsCreate.Select(o => o.ID).ToList();
            var dataResults = await DB.MasterTransferPromotionCreditCardItems
                .Include(o => o.Bank)
                .Include(o => o.EDCFee)
                .Include(o => o.PromotionItemStatus)
                .Include(o => o.UpdatedBy)
                .Where(o => updatedIDs.Contains(o.ID))
                .ToListAsync();

            var results = dataResults.Select(o => MasterTransferCreditCardItemDTO.CreateFromModel(o)).ToList();
            return results;
        }
        /// <summary>
        /// Clone Promotion ให้ Copy ทุกอย่างใน MasterPromotion สร้างเป็น Promotion ใหม่
        /// </summary>
        /// <returns>The master transfer promotion async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<MasterTransferPromotionDTO> CloneMasterTransferPromotionAsync(Guid id)
        {
            var statusDeleteFromSapId = await DB.MasterCenters
                            .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MaterialItemStatus
                                        && o.Key == "102")
                            .Select(o => o.ID)
                            .FirstOrDefaultAsync();

            var statusActiveId = await DB.MasterCenters
                            .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionItemStatus
                                        && o.Key == "1")
                            .Select(o => o.ID)
                            .FirstOrDefaultAsync();

            var promotionMaterialGroups = await DB.PromotionMaterialGroups.ToListAsync();
            var promotionMaterialAddPrices = await DB.PromotionMaterialAddPrices.ToListAsync();

            var masterTransferPromotion = await DB.MasterTransferPromotions.Where(o => o.ID == id).Include(o => o.Project).FirstAsync();
            var masterTransferPromotionItems = await DB.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == id
                                                                                            && !string.IsNullOrEmpty(o.ItemNo)
                                                                                            && (!string.IsNullOrEmpty(o.MaterialCode) || o.AgreementNo.ToLower().Contains("welcomehome"))
                                                                                            && (!string.IsNullOrEmpty(o.GLAccountNo) || o.AgreementNo.ToLower().Contains("welcomehome"))
                                                                                            && !string.IsNullOrEmpty(o.MaterialGroupKey)
                                                                                            && o.ExpireDate > DateTime.Now
                                                                                            && o.PromotionItemStatusMasterCenterID == statusActiveId
                                                                                            && o.PromotionMaterialItemID != null //1
                                                                                         ).ToListAsync();
            //2
            var promotionMaterialItems = new List<PromotionMaterialItem>();
            if (masterTransferPromotionItems.Count > 0)
            {
                var promotionMaterialItemsID = masterTransferPromotionItems.Select(o => o.PromotionMaterialItemID).ToList();
                promotionMaterialItems = await DB.PromotionMaterialItems.Where(o => promotionMaterialItemsID.Contains(o.ID)).ToListAsync();
            }


            var masterTransferPromotionFreeItems = await DB.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == id).ToListAsync();
            var masterTransferCreditCardItems = await DB.MasterTransferPromotionCreditCardItems.Where(o => o.MasterTransferPromotionID == id
                                                                                                       && o.PromotionItemStatusMasterCenterID == statusActiveId).ToListAsync();
            var masterCenterPromotionStatusID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                       .Select(o => o.ID)
                                                                       .FirstAsync();
            var productType = await DB.MasterCenters.Where(o => o.ID == masterTransferPromotion.Project.ProductTypeMasterCenterID).FirstOrDefaultAsync();

            var newMasterTransferPromotion = new MasterTransferPromotion
            {
                Name = masterTransferPromotion.Name,
                ProjectID = masterTransferPromotion.ProjectID,
                //StartDate = masterTransferPromotion.StartDate,
                //EndDate = masterTransferPromotion.EndDate,
                StartDate = DateTime.Now,
                TransferDiscount = masterTransferPromotion.TransferDiscount,
                PromotionStatusMasterCenterID = masterCenterPromotionStatusID,
                IsUsed = false,
                RefMigrateID1 = "ClonedFromProm.No : " + masterTransferPromotion.PromotionNo?.ToString()
            };
            string year = Convert.ToString(DateTime.Today.Year);
            var key = "PT" + masterTransferPromotion.Project?.ProjectNo + year[2] + year[3];
            var type = "PRM.MasterTransferPromotion";
            var runningno = await DB.RunningNumberCounters.Where(o => o.Key == key && o.Type == type).FirstOrDefaultAsync();
            if (runningno == null)
            {
                var runningNumberCounter = new RunningNumberCounter
                {
                    Key = key,
                    Type = type,
                    Count = 1
                };
                await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
                await DB.SaveChangesAsync();

                newMasterTransferPromotion.PromotionNo = key + runningNumberCounter.Count.ToString("000");
                runningNumberCounter.Count++;
                DB.Entry(runningNumberCounter).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }
            else
            {
                newMasterTransferPromotion.PromotionNo = key + runningno.Count.ToString("000");
                runningno.Count++;
                DB.Entry(runningno).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }

            var newMasterTransferPromotionItems = new List<MasterTransferPromotionItem>();
            var newMasterTransferHouseModelItems = new List<MasterTransferHouseModelItem>();
            var newMasterTransferPromotionFreeItems = new List<MasterTransferPromotionFreeItem>();
            var newMasterTransferHouseModelFreeItems = new List<MasterTransferHouseModelFreeItem>();
            var newMasterTransferCreditCardItem = new List<MasterTransferPromotionCreditCardItem>();
            var listPromotionMaterialitem = new List<Guid?>();
            //Add ParentPromotionItems.
            foreach (var item in masterTransferPromotionItems.Where(o => o.MainPromotionItemID == null))
            {   //3
                var sapSaleTaxCode = promotionMaterialItems.Where(o => o.ID == item.PromotionMaterialItemID).Select(o => o.SAPSaleTaxCode).FirstOrDefault();
                var masterItem = promotionMaterialItems.Where(o => o.ID == item.PromotionMaterialItemID).FirstOrDefault();
                double percentMarkUP = 0;
                double totalPriceInCludeVat = 0;
                var promotionmaterialGroup = promotionMaterialGroups.Where(o => o.Key == item.MaterialGroupKey).FirstOrDefault();
                var promotionmaterialAddPrice = promotionmaterialGroup != null ? promotionMaterialAddPrices.Where(o => o.PromotionMaterialGroupID == promotionmaterialGroup.ID).FirstOrDefault() : null;

                if (productType.Key.Equals("1"))
                {
                    percentMarkUP = promotionmaterialAddPrice == null ? 0 : promotionmaterialAddPrice.LowRisePercent;
                }
                else
                {
                    percentMarkUP = promotionmaterialAddPrice == null ? 0 : promotionmaterialAddPrice.HighRisePercent;
                }

                //var vat7Pct = item.SAPSaleTaxCode == "VX"
                //             || string.IsNullOrEmpty(item.SAPSaleTaxCode) ? 0 : Convert.ToDecimal(1.07);
                //4
                var vat7Pct = sapSaleTaxCode == "VX"
                               || string.IsNullOrEmpty(sapSaleTaxCode) ? 0 : Convert.ToDecimal(1.07);

                //case vat7 = 0 : (BasePrice) + (BasePrice * (Low/HighRisePercent / 100))
                //case vat7 = 7 : (BasePrice * 1.07) + ((BasePrice * 1.07) * (Low/HighRisePercent / 100))
                var pricePerUnitIncludeVat7 = vat7Pct == 0 ?
                               (masterItem.BasePrice) +
                               (((masterItem.BasePrice) * Convert.ToDecimal(percentMarkUP)) / 100) :
                               (masterItem.BasePrice * Convert.ToDecimal(1.07)) +
                               (((masterItem.BasePrice * Convert.ToDecimal(1.07)) * Convert.ToDecimal(percentMarkUP)) / 100);

                totalPriceInCludeVat += (int)Math.Ceiling(pricePerUnitIncludeVat7);

                // check duplicate promotion
                var checkSubPromotionitem = await DB.MasterTransferPromotionItems.Where(o => o.MainPromotionItemID == item.ID).ToListAsync();
                if (checkSubPromotionitem.Count == 0)
                {
                    if (listPromotionMaterialitem.Contains(item.PromotionMaterialItemID))
                    {
                        continue;
                    }
                    listPromotionMaterialitem.Add(item.PromotionMaterialItemID);
                }

                using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
                string sqlQuery = sqlDupTransferPromotion.QueryString;
                DynamicParameters ParamList = sqlDupTransferPromotion.QueryFilter(ref sqlQuery, id);
                CommandDefinition commandDefinition = new(
                                                      commandText: sqlQuery,
                                                      parameters: ParamList,
                                                      transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                                      commandType: CommandType.Text);
                var getDupSalePromo = (await cmd.Connection.QueryAsync<sqlDupTransferPromotion.QueryResult>(commandDefinition))?.ToList() ?? new();
                var listGetDupSalePromo = getDupSalePromo.Select(o => o.MasterTransferPromoItemID).ToList().FirstOrDefault();

                //if (listGetDupSalePromo == item.ID)
                //{
                //    continue;
                //}

                var model = new MasterTransferPromotionItem
                {
                    MasterTransferPromotionID = newMasterTransferPromotion.ID,
                    NameTH = item.NameTH,
                    NameEN = item.NameEN,
                    Quantity = item.Quantity,
                    UnitTH = item.UnitTH,
                    UnitEN = item.UnitEN,
                    //PricePerUnit = item.PricePerUnit,
                    PricePerUnit = (int)Math.Ceiling(pricePerUnitIncludeVat7),
                    //TotalPrice = item.TotalPrice,
                    ReceiveDays = item.ReceiveDays,
                    WhenPromotionReceiveMasterCenterID = item.WhenPromotionReceiveMasterCenterID,
                    IsPurchasing = item.IsPurchasing,
                    IsShowInContract = item.IsShowInContract,
                    PromotionItemStatusMasterCenterID = item.PromotionItemStatusMasterCenterID,
                    ExpireDate = item.ExpireDate,
                    PromotionMaterialItemID = item.PromotionMaterialItemID,
                    MainPromotionItemID = null,
                    IsUsed = false
                };

                #region evoucher aurora
                if (!string.IsNullOrEmpty(item.SAPVendor) && item.SAPVendor.Equals("0000107009") && item.MaterialGroupKey.Equals("EST100"))
                {
                    // find fee mat in PromotionOperatingFee
                    var matfee = await DB.PromotionOperatingFees.Where(x => x.IsActived == true).Select(x => x.PromotionMaterialCode).ToListAsync();
                    // fine Mat Fee in PromotionMaterialItems
                    var matFeeItem = await DB.PromotionMaterialItems.Where(x => x.AgreementNo.Equals(item.AgreementNo) && matfee.Contains(x.MaterialCode) && x.SAPDeleteIndicator != "L").FirstOrDefaultAsync();
                    decimal fee = 0;
                    if (matFeeItem != null)
                    {
                        fee = matFeeItem.BasePrice;
                    }
                    totalPriceInCludeVat = (int)Math.Ceiling(pricePerUnitIncludeVat7 + fee);
                    model.PricePerUnit = (int)Math.Ceiling(totalPriceInCludeVat);
                }
                #endregion

                //Sap info
                model.StartDate = item.StartDate;
                model.ItemNo = item.ItemNo;
                model.BrandEN = item.BrandEN;
                model.BrandTH = item.BrandTH;
                model.UnitEN = item.UnitEN;
                model.RemarkTH = item.RemarkTH;
                model.RemarkEN = item.RemarkEN;
                model.SpecEN = item.SpecEN;
                model.SpecTH = item.SpecTH;
                model.Plant = item.Plant;
                model.SAPCompanyID = item.SAPCompanyID;
                model.AgreementNo = item.AgreementNo;
                model.SAPPurchasingOrg = item.SAPPurchasingOrg;
                model.MaterialGroupKey = item.MaterialGroupKey;
                model.DocType = item.DocType;
                model.GLAccountNo = item.GLAccountNo;
                model.MaterialPrice = masterItem.Price;
                model.MaterialBasePrice = masterItem.BasePrice;
                model.Vat = masterItem.Vat;
                model.SAPSaleTaxCode = masterItem.SAPSaleTaxCode;
                model.SAPBaseUnit = item.SAPBaseUnit;
                model.SAPVendor = item.SAPVendor;
                model.SAPPurchasingGroup = item.SAPPurchasingGroup;
                model.MaterialCode = item.MaterialCode;
                var masterTransferHouseItem = await DB.MasterTransferHouseModelItems.Where(o => o.MasterTransferPromotionItemID == item.ID).ToListAsync();
                foreach (var house in masterTransferHouseItem)
                {
                    var newhouse = new MasterTransferHouseModelItem
                    {
                        MasterTransferPromotionItemID = model.ID,
                        ModelID = house.ModelID
                    };
                    newMasterTransferHouseModelItems.Add(newhouse);
                }
                //newMasterTransferPromotionItems.Add(model);

                //Add SubPromotionItems
                var listSub = masterTransferPromotionItems.Where(o => o.MainPromotionItemID == item.ID).ToList();
                foreach (var item1 in listSub)
                {
                    var sapSaleTaxCodeSub = promotionMaterialItems.Where(o => o.ID == item1.PromotionMaterialItemID).Select(o => o.SAPSaleTaxCode).FirstOrDefault();
                    var masterSubItem = promotionMaterialItems.Where(o => o.ID == item1.PromotionMaterialItemID).FirstOrDefault();
                    //var subvat7Pct = item1.SAPSaleTaxCode == "VX" || string.IsNullOrEmpty(item1.SAPSaleTaxCode) ? 0 : Convert.ToDecimal(1.07);
                    var subvat7Pct = sapSaleTaxCodeSub == "VX" || string.IsNullOrEmpty(sapSaleTaxCodeSub) ? 0 : Convert.ToDecimal(1.07);

                    //case vat7 = 0 : (BasePrice) + (BasePrice * (Low/HighRisePercent / 100))
                    //case vat7 = 7 : (BasePrice * 1.07) + ((BasePrice * 1.07) * (Low/HighRisePercent / 100))
                    var subpricePerUnitIncludeVat7 = subvat7Pct == 0 ?
                                   (masterSubItem.BasePrice) +
                                   (((masterSubItem.BasePrice) * Convert.ToDecimal(percentMarkUP)) / 100) :
                                   (masterSubItem.BasePrice * Convert.ToDecimal(1.07)) +
                                   (((masterSubItem.BasePrice * Convert.ToDecimal(1.07)) * Convert.ToDecimal(percentMarkUP)) / 100);

                    totalPriceInCludeVat += (int)Math.Ceiling(subpricePerUnitIncludeVat7);

                    var modelSub = new MasterTransferPromotionItem
                    {
                        MasterTransferPromotionID = newMasterTransferPromotion.ID,
                        NameTH = item1.NameTH,
                        NameEN = item1.NameEN,
                        Quantity = item1.Quantity,
                        UnitTH = item1.UnitTH,
                        UnitEN = item1.UnitEN,
                        //PricePerUnit = item1.PricePerUnit,
                        //TotalPrice = item1.TotalPrice,
                        PricePerUnit = (int)Math.Ceiling(subpricePerUnitIncludeVat7),
                        TotalPrice = (int)Math.Ceiling(subpricePerUnitIncludeVat7),
                        ReceiveDays = item1.ReceiveDays,
                        WhenPromotionReceiveMasterCenterID = item1.WhenPromotionReceiveMasterCenterID,
                        IsPurchasing = item1.IsPurchasing,
                        IsShowInContract = item1.IsShowInContract,
                        PromotionItemStatusMasterCenterID = item1.PromotionItemStatusMasterCenterID,
                        ExpireDate = item1.ExpireDate,
                        MainPromotionItemID = model.ID,
                        PromotionMaterialItemID = item1.PromotionMaterialItemID,
                        IsUsed = false
                    };

                    //Sap info
                    modelSub.StartDate = item1.StartDate;
                    modelSub.ItemNo = item1.ItemNo;
                    modelSub.BrandEN = item1.BrandEN;
                    modelSub.BrandTH = item1.BrandTH;
                    modelSub.UnitEN = item1.UnitEN;
                    modelSub.RemarkTH = item1.RemarkTH;
                    modelSub.RemarkEN = item1.RemarkEN;
                    modelSub.SpecEN = item1.SpecEN;
                    modelSub.SpecTH = item1.SpecTH;
                    modelSub.Plant = item1.Plant;
                    modelSub.SAPCompanyID = item1.SAPCompanyID;
                    modelSub.AgreementNo = item1.AgreementNo;
                    modelSub.SAPPurchasingOrg = item1.SAPPurchasingOrg;
                    modelSub.MaterialGroupKey = item1.MaterialGroupKey;
                    modelSub.DocType = item1.DocType;
                    modelSub.GLAccountNo = item1.GLAccountNo;
                    modelSub.MaterialPrice = masterSubItem.Price;
                    modelSub.MaterialBasePrice = masterSubItem.BasePrice;
                    modelSub.Vat = masterSubItem.Vat;
                    modelSub.SAPSaleTaxCode = masterSubItem.SAPSaleTaxCode;
                    modelSub.SAPBaseUnit = item1.SAPBaseUnit;
                    modelSub.SAPVendor = item1.SAPVendor;
                    modelSub.SAPPurchasingGroup = item1.SAPPurchasingGroup;
                    modelSub.MaterialCode = item1.MaterialCode;
                    newMasterTransferPromotionItems.Add(modelSub);
                    var masterTransferHouseItemSub = await DB.MasterTransferHouseModelItems.Where(o => o.MasterTransferPromotionItemID == item1.ID).ToListAsync();
                    foreach (var house1 in masterTransferHouseItemSub)
                    {
                        var newhousesub = new MasterTransferHouseModelItem
                        {
                            MasterTransferPromotionItemID = modelSub.ID,
                            ModelID = house1.ModelID
                        };
                        newMasterTransferHouseModelItems.Add(newhousesub);
                    }
                }
                //Add Total MainPromotionItems
                model.TotalPrice = (int)Math.Ceiling(totalPriceInCludeVat);
                newMasterTransferPromotionItems.Add(model);
            }
            foreach (var item in masterTransferPromotionFreeItems)
            {
                var model = new MasterTransferPromotionFreeItem
                {
                    MasterTransferPromotionID = newMasterTransferPromotion.ID,
                    NameTH = item.NameTH,
                    NameEN = item.NameEN,
                    Quantity = item.Quantity,
                    UnitTH = item.UnitTH,
                    UnitEN = item.UnitEN,
                    ReceiveDays = item.ReceiveDays,
                    WhenPromotionReceiveMasterCenterID = item.WhenPromotionReceiveMasterCenterID,
                    IsShowInContract = item.IsShowInContract,
                };
                newMasterTransferPromotionFreeItems.Add(model);
                var masterTransferHouseFreeItem = await DB.MasterTransferHouseModelFreeItems.Where(o => o.MasterTransferPromotionFreeItemID == item.ID).ToListAsync();
                foreach (var item1 in masterTransferHouseFreeItem)
                {
                    var house = new MasterTransferHouseModelFreeItem
                    {
                        MasterTransferPromotionFreeItemID = model.ID,
                        ModelID = item1.ModelID
                    };
                    newMasterTransferHouseModelFreeItems.Add(house);
                }
            }
            //foreach (var item in masterTransferCreditCardItems)
            //{
            //    var model = new MasterTransferPromotionCreditCardItem()
            //    {
            //        MasterTransferPromotionID = newMasterTransferPromotion.ID,
            //        BankID = item.BankID,
            //        NameTH = item.NameTH,
            //        NameEN = item.NameEN,
            //        Fee = item.Fee,
            //        UnitTH = item.UnitTH,
            //        UnitEN = item.UnitEN,
            //        PromotionItemStatusMasterCenterID = item.PromotionItemStatusMasterCenterID,
            //        Quantity = item.Quantity,
            //        EDCFeeID = item.EDCFeeID
            //    };

            //    newMasterTransferCreditCardItem.Add(model);
            //}
            var masterTransferPromotionCreditCardItemsCreate = new List<MasterTransferPromotionCreditCardItem>();
            foreach (var item in masterTransferPromotionCreditCardItemsCreate)
            {
                MasterTransferPromotionCreditCardItem model = new MasterTransferPromotionCreditCardItem();
                model.MasterTransferPromotionID = newMasterTransferPromotion.ID;
                model.Quantity = 1;
                model.UnitTH = item.UnitTH;
                model.UnitEN = item.UnitEN;
                model.Fee = item.Fee;
                model.NameTH = item.NameTH;
                model.NameEN = item.NameEN;
                model.BankName = item.BankName;
                model.PromotionItemStatusMasterCenterID = item.PromotionItemStatusMasterCenterID;
                model.Order = 0;
                masterTransferPromotionCreditCardItemsCreate.Add(model);
            }
            await DB.AddRangeAsync(masterTransferPromotionCreditCardItemsCreate);

            await DB.MasterTransferPromotions.AddAsync(newMasterTransferPromotion);

            if (newMasterTransferPromotionItems.Count() > 0)
            {
                await DB.MasterTransferPromotionItems.AddRangeAsync(newMasterTransferPromotionItems);
            }
            if (newMasterTransferHouseModelItems.Count() > 0)
            {
                await DB.MasterTransferHouseModelItems.AddRangeAsync(newMasterTransferHouseModelItems);
            }
            if (newMasterTransferPromotionFreeItems.Count() > 0)
            {
                await DB.MasterTransferPromotionFreeItems.AddRangeAsync(newMasterTransferPromotionFreeItems);
            }
            if (newMasterTransferHouseModelFreeItems.Count() > 0)
            {
                await DB.MasterTransferHouseModelFreeItems.AddRangeAsync(newMasterTransferHouseModelFreeItems);
            }
            if (newMasterTransferCreditCardItem.Count() > 0)
            {
                await DB.MasterTransferPromotionCreditCardItems.AddRangeAsync(newMasterTransferCreditCardItem);
            }
            await DB.SaveChangesAsync();

            return await this.GetMasterTransferPromotionDetailAsync(newMasterTransferPromotion.ID);
        }

        public async Task<CloneMasterPromotionConfirmDTO> GetCloneMasterTransferPromotionConfirmAsync(Guid id, CancellationToken cancellationToken = default)
        {
            CloneMasterPromotionConfirmDTO result = new CloneMasterPromotionConfirmDTO();

            var statusActiveId = await DB.MasterCenters.AsNoTracking()
                                        .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionItemStatus
                                                    && o.Key == "1")
                                        .Select(o => o.ID)
                                        .FirstOrDefaultAsync(cancellationToken);
            //Clone NotExpire and Active
            result.CloneItemCount = await DB.MasterTransferPromotionItems.AsNoTracking()
                                            .CountAsync(o => o.MasterTransferPromotionID == id && o.ExpireDate != null
                                            && !string.IsNullOrEmpty(o.ItemNo)
                                            && (!string.IsNullOrEmpty(o.MaterialCode) || o.AgreementNo.ToLower().Contains("welcomehome"))
                                            && (!string.IsNullOrEmpty(o.GLAccountNo) || o.AgreementNo.ToLower().Contains("welcomehome"))
                                            && !string.IsNullOrEmpty(o.MaterialGroupKey)
                                            && o.ExpireDate > DateTime.Now
                                            && o.PromotionItemStatusMasterCenterID == statusActiveId);

            //Expire or NotActive
            result.ExpiredItemCount = await DB.MasterTransferPromotionItems.AsNoTracking()
                                            .CountAsync(o => o.MasterTransferPromotionID == id && o.ExpireDate != null
                                            && (o.ExpireDate <= DateTime.Now || o.PromotionItemStatusMasterCenterID != statusActiveId));


            return result;
        }

        private async Task ValidatePromotionMaterial(Guid masterTransferPromotionID, PromotionMaterialDTO input)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(input.AgreementNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = input.GetType().GetProperty(nameof(PromotionMaterialDTO.AgreementNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (string.IsNullOrEmpty(input.ItemNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = input.GetType().GetProperty(nameof(PromotionMaterialDTO.ItemNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        private async Task<bool> SubMasterTransferPromotionMaterialHasValue(PromotionMaterialDTO input, Guid mainPromotionItemID, Guid masterTransferPromotionID)
        {
            var childmasterTransferPromotionItem = await DB.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == masterTransferPromotionID && o.MainPromotionItemID == mainPromotionItemID
                                                && o.PromotionMaterialItemID == input.Id && o.IsDeleted == false).ToListAsync();

            var parentmasterTransferPromotionItem = await DB.MasterTransferPromotionItems.Where(o => o.ID == mainPromotionItemID
                                    && o.PromotionMaterialItemID == input.Id && o.IsDeleted == false).ToListAsync();

            int count = childmasterTransferPromotionItem.Count + parentmasterTransferPromotionItem.Count;

            return count > 0 ? true : false;
        }

    }
}
