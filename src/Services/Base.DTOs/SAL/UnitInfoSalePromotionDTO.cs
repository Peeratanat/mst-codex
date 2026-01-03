using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class UnitInfoSalePromotionDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// โอนกรรมสิทธิ์ภายในวันที่
        /// </summary>
        public DateTime? TransferDateBefore { get; set; }
        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<UnitInfoSalePromotionItemDTO> Items { get; set; }

        public async static Task<UnitInfoSalePromotionDTO> CreateFromModelAsync(SalePromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new UnitInfoSalePromotionDTO()
                {
                    PromotionNo = model.MasterPromotion?.PromotionNo,
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    TransferDateBefore = model.TransferDateBefore
                };

                var promotionItems = new List<UnitInfoSalePromotionItemDTO>();
                var itemModels = await db.SalePromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.QuotationSalePromotionItem)
                    .Where(o => o.SalePromotionID == model.ID && o.MainSalePromotionItemID == null).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => UnitInfoSalePromotionItemDTO.CreateFromModel(o, null, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var freeModels = await db.SalePromotionFreeItems
                    .Include(o => o.MasterSalePromotionFreeItem)
                    .Include(o => o.QuotationSalePromotionFreeItem)
                    .Where(o => o.SalePromotionID == model.ID).ToListAsync();

                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => UnitInfoSalePromotionItemDTO.CreateFromModel(null, o, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var creditModels = await db.SalePromotionCreditCardItems
                    .Include(o => o.MasterSalePromotionCreditCardItem)
                    .Include(o => o.QuotationSalePromotionCreditCardItem)
                    .Where(o => o.SalePromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => UnitInfoSalePromotionItemDTO.CreateFromModel(null, null, o, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var promotionItemIDs = new List<Guid?>();
                if (promotionItems.Count > 0)
                {
                    promotionItemIDs.AddRange(promotionItems.Select(o => o.FromMasterSalePromotionItemID).ToList());
                }

                var itemResults = new List<UnitInfoSalePromotionItemDTO>();
                if (model.MasterPromotion != null)
                {
                    var masterPromotionItems = new List<UnitInfoSalePromotionItemDTO>();
                    var promotionitemStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();

                    var qutation = await db.Bookings.Where(o => o.ID == model.BookingID).FirstOrDefaultAsync();
                    var projectID = await db.Units.Where(o => o.ID == qutation.UnitID).FirstAsync();
                    var query = from a in db.MasterSaleHouseModelItems.Where(o => o.ModelID == projectID.ModelID)
                                join ms in db.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID == promotionitemStatusActive)
                                on a.MasterSalePromotionItemID equals ms.ID
                                select ms;
                    var masterItemModels = query.ToList();

                   // var masterItemModels = await db.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                    if (masterItemModels.Count > 0)
                    {
                        var items = masterItemModels.Select(o => UnitInfoSalePromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var query2 = from a in db.MasterSaleHouseModelFreeItems.Where(o => o.ModelID == projectID.ModelID)
                                join ms in db.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID )
                                on a.MasterSalePromotionFreeItemID equals ms.ID
                                select ms;
                    var masterFreeModels = query2.ToList();


                   // var masterFreeModels = await db.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterFreeModels.Count > 0)
                    {
                        var items = masterFreeModels.Select(o => UnitInfoSalePromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterCreditModels = await db.MasterSalePromotionCreditCardItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterCreditModels.Count > 0)
                    {
                        var items = masterCreditModels.Select(o => UnitInfoSalePromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    if (masterPromotionItems.Count > 0)
                    {
                        var masterItems = masterPromotionItems.Where(o => !promotionItemIDs.Contains(o.FromMasterSalePromotionItemID)).ToList();
                        itemResults.AddRange(masterItems);
                    }
                }

                result.Items = new List<UnitInfoSalePromotionItemDTO>();
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

        public async static Task<UnitInfoSalePromotionDTO> CreateFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var projectID = await db.Units.Where(o => o.ID == unitID).Select(o => o.ProjectID).FirstAsync();
            var promotionStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionStatus && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();
            var model = await db.MasterSalePromotions.Where(o => o.PromotionStatusMasterCenterID == promotionStatusActive && o.ProjectID == projectID).FirstOrDefaultAsync();

            if (model != null)
            {
                UnitInfoSalePromotionDTO result = new UnitInfoSalePromotionDTO();
                result.PromotionNo = model.PromotionNo;
                result.Items = new List<UnitInfoSalePromotionItemDTO>();
                var itemModels = await db.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == model.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => UnitInfoSalePromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var freeModels = await db.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => UnitInfoSalePromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var creditModels = await db.MasterSalePromotionCreditCardItems.Where(o => o.MasterSalePromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => UnitInfoSalePromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                    result.Items.AddRange(items);
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
