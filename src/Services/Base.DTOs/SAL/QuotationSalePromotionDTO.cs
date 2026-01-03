using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// โปรขายในใบเสนอราคา
    /// </summary>
    public class QuotationSalePromotionDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<QuotationSalePromotionItemDTO> Items { get; set; }

        public async static Task<QuotationSalePromotionDTO> CreateFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var projectID = await db.Units.Where(o => o.ID == unitID).FirstAsync();


            var promotionStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();
            var model = await db.MasterSalePromotions.Where(o => o.PromotionStatusMasterCenterID == promotionStatusActive && o.ProjectID == projectID.ProjectID).FirstOrDefaultAsync();

            if(model != null)
            {
                QuotationSalePromotionDTO result = new QuotationSalePromotionDTO();
                result.PromotionNo = model.PromotionNo;
                result.Items = new List<QuotationSalePromotionItemDTO>();
                var promotionitemStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();
                var test = await db.MasterSaleHouseModelItems.Include(o=>o.MasterSalePromotionItem).Where(o => o.MasterSalePromotionItem.MasterSalePromotionID == model.ID && o.MasterSalePromotionItem.MainPromotionItemID == null && o.MasterSalePromotionItem.ExpireDate >= DateTime.Now.Date && o.MasterSalePromotionItem.PromotionItemStatusMasterCenterID == promotionitemStatusActive && o.ModelID==projectID.ModelID).ToListAsync();


                var query = from a in db.MasterSaleHouseModelItems.Where(o => o.ModelID == projectID.ModelID)
                            join ms in db.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == model.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID == promotionitemStatusActive)
                            on a.MasterSalePromotionItemID equals ms.ID
                            select ms;

                var itemModels = query.ToList();

                //var itemModels = await db.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == model.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID == promotionitemStatusActive).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => QuotationSalePromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var query2 = from a in db.MasterSaleHouseModelFreeItems.Where(o => o.ModelID == projectID.ModelID)
                            join ms in db.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == model.ID )
                            on a.MasterSalePromotionFreeItemID equals ms.ID
                            select ms;
                var freeModels = query2.ToList();

                //var freeModels = await db.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => QuotationSalePromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var creditModels = await db.MasterSalePromotionCreditCardItems.Where(o => o.MasterSalePromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => QuotationSalePromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                    result.Items.AddRange(items);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<QuotationSalePromotionDTO> CreateFromQuotationAsync(QuotationSalePromotion model, DatabaseContext db)
        {
            if(model != null)
            {
                var result = new QuotationSalePromotionDTO()
                {
                    PromotionNo = model.MasterPromotion?.PromotionNo,
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                var promotionItems = new List<QuotationSalePromotionItemDTO>();
                var itemModels = await db.QuotationSalePromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationSalePromotionID == model.ID && o.MainQuotationSalePromotionID == null).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => QuotationSalePromotionItemDTO.CreateFromModel(o, null, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var freeModels = await db.QuotationSalePromotionFreeItems
                    .Include(o => o.MasterPromotionFreeItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationSalePromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => QuotationSalePromotionItemDTO.CreateFromModel(null, o, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var creditModels = await db.QuotationSalePromotionCreditCardItems
                    .Include(o => o.MasterSalePromotionCreditCardItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationSalePromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => QuotationSalePromotionItemDTO.CreateFromModel(null, null, o, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var promotionItemIDs = new List<Guid?>();
                if (promotionItems.Count > 0)
                {
                    promotionItemIDs.AddRange(promotionItems.Select(o => o.FromMasterSalePromotionItemID).ToList());
                }

                var itemResults = new List<QuotationSalePromotionItemDTO>();
                if(model.MasterPromotion != null)
                {
                    var promotionitemStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();

                    var masterPromotionItems = new List<QuotationSalePromotionItemDTO>();

                    var qutation = await db.Quotations.Where(o => o.ID == model.QuotationID).FirstOrDefaultAsync();
                    var projectID = await db.Units.Where(o => o.ID == qutation.UnitID).FirstAsync();
                    var query = from a in db.MasterSaleHouseModelItems.Where(o => o.ModelID == projectID.ModelID)
                                join ms in db.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID == promotionitemStatusActive)
                                on a.MasterSalePromotionItemID equals ms.ID
                                select ms;
                    var masterItemModels = query.ToList();
               //     var masterItemModels = await db.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID == promotionitemStatusActive).ToListAsync();
                    if (masterItemModels.Count > 0)
                    {
                        var items = masterItemModels.Select(o => QuotationSalePromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var query2 = from a in db.MasterSaleHouseModelFreeItems.Where(o => o.ModelID == projectID.ModelID)
                                 join ms in db.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID)
                                 on a.MasterSalePromotionFreeItemID equals ms.ID
                                 select ms;
                    var masterFreeModels = query2.ToList();

                    //var masterFreeModels = await db.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterFreeModels.Count > 0)
                    {
                        var items = masterFreeModels.Select(o => QuotationSalePromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterCreditModels = await db.MasterSalePromotionCreditCardItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterCreditModels.Count > 0)
                    {
                        var items = masterCreditModels.Select(o => QuotationSalePromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }


                    if (masterPromotionItems.Count > 0)
                    {
                        var masterItems = masterPromotionItems.Where(o => !promotionItemIDs.Contains(o.FromMasterSalePromotionItemID)).ToList();
                        itemResults.AddRange(masterItems);
                    }
                }

                result.Items = new List<QuotationSalePromotionItemDTO>();
                result.Items.AddRange(promotionItems);
                if (itemResults.Count > 0)
                {
                    result.Items.AddRange(itemResults);
                }
               // result.Items.AddRange(promotionItems);

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
