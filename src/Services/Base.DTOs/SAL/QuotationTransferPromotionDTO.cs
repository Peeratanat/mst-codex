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
    /// โปรโอนในใบเสนอราคา
    /// </summary>
    public class QuotationTransferPromotionDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<QuotationTransferPromotionItemDTO> Items { get; set; }

        public async static Task<QuotationTransferPromotionDTO> CreateFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var projectID = await db.Units.Where(o => o.ID == unitID).FirstOrDefaultAsync();
            var promotionStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();
            var model = await db.MasterTransferPromotions.Where(o => o.PromotionStatusMasterCenterID == promotionStatusActive && o.ProjectID == projectID.ProjectID).FirstOrDefaultAsync();
            if (model != null)
            {
                QuotationTransferPromotionDTO result = new QuotationTransferPromotionDTO();
                result.PromotionNo = model.PromotionNo;
                result.Items = new List<QuotationTransferPromotionItemDTO>();
                var promotionitemStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();



                var query = from a in db.MasterTransferHouseModelItems.Where(o => o.ModelID == projectID.ModelID)
                            join ms in db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == model.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID == promotionitemStatusActive)
                            on a.MasterTransferPromotionItemID equals ms.ID
                            select ms;
                var itemModels = query.ToList();

              //  var itemModels = await db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == model.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID== promotionitemStatusActive).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var query2 = from a in db.MasterTransferHouseModelFreeItems.Where(o => o.ModelID == projectID.ModelID)
                            join ms in db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == model.ID)
                            on a.MasterTransferPromotionFreeItemID equals ms.ID
                            select ms;
                var freeModels = query2.ToList();

                //var freeModels = await db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var creditModels = await db.MasterTransferPromotionCreditCardItems.Where(o => o.MasterTransferPromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                    result.Items.AddRange(items);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<QuotationTransferPromotionDTO> CreateFromQuotationAsync(QuotationTransferPromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new QuotationTransferPromotionDTO()
                {
                    PromotionNo = model.MasterPromotion?.PromotionNo,
                    Remark = model.Remark,
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                var promotionItems = new List<QuotationTransferPromotionItemDTO>();
                var itemModels = await db.QuotationTransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationTransferPromotionID == model.ID && o.MainQuotationTransferPromotionID == null).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromModel(o, null, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var freeModels = await db.QuotationTransferPromotionFreeItems
                    .Include(o => o.MasterPromotionFreeItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationTransferPromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromModel(null, o, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var creditModels = await db.QuotationTransferPromotionCreditCardItems
                    .Include(o => o.MasterTransferPromotionCreditCardItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationTransferPromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromModel(null, null, o, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var promotionItemIDs = new List<Guid?>();
                if (promotionItems.Count > 0)
                {
                    promotionItemIDs.AddRange(promotionItems.Select(o => o.FromMasterTansferPromotionItemID).ToList());
                }

                var itemResults = new List<QuotationTransferPromotionItemDTO>();
                if(model.MasterPromotion != null)
                {
                    var masterPromotionItems = new List<QuotationTransferPromotionItemDTO>();

                    var qutation = await db.Quotations.Where(o => o.ID == model.QuotationID).FirstOrDefaultAsync();
                    var projectID = await db.Units.Where(o => o.ID == qutation.UnitID).FirstAsync();
                    var promotionitemStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();

                    var query = from a in db.MasterTransferHouseModelItems.Where(o => o.ModelID == projectID.ModelID)
                                join ms in db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == model.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID == promotionitemStatusActive)
                                on a.MasterTransferPromotionItemID equals ms.ID
                                select ms;
                    var masterItemModels = query.ToList();
                  //  var masterItemModels = await db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == model.MasterPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                    if (masterItemModels.Count > 0)
                    {
                        var items = masterItemModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var query2 = from a in db.MasterTransferHouseModelFreeItems.Where(o => o.ModelID == projectID.ModelID)
                                 join ms in db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == model.ID)
                                 on a.MasterTransferPromotionFreeItemID equals ms.ID
                                 select ms;
                    var masterFreeModels = query2.ToList();

                    //var masterFreeModels = await db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterFreeModels.Count > 0)
                    {
                        var items = masterFreeModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterCreditModels = await db.MasterTransferPromotionCreditCardItems.Where(o => o.MasterTransferPromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterCreditModels.Count > 0)
                    {
                        var items = masterCreditModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    if (masterPromotionItems.Count > 0)
                    {
                        var masterItems = masterPromotionItems.Where(o => !promotionItemIDs.Contains(o.FromMasterTansferPromotionItemID)).ToList();
                        itemResults.AddRange(masterItems);
                    }
                }

                result.Items = new List<QuotationTransferPromotionItemDTO>();
                result.Items.AddRange(promotionItems);
                if (itemResults.Count > 0)
                {
                    result.Items.AddRange(itemResults);
                }
                
                

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
