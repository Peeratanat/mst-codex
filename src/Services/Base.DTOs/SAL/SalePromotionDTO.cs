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
    public class SalePromotionDTO : BaseDTO
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
        public List<SalePromotionItemDTO> Items { get; set; }
        public string MaterialGroupKey { get; set; }

        public async static Task<SalePromotionDTO> CreateFromModelAsync(SalePromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new SalePromotionDTO()
                {
                    PromotionNo = model.MasterPromotion?.PromotionNo,
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    TransferDateBefore = model.TransferDateBefore
                };

                var promotionItems = new List<SalePromotionItemDTO>();
                var itemModels = await db.SalePromotionItems
                    .Include(o => o.MasterPromotionItem).ThenInclude(o => o.PromotionMaterialItem)
                    .Include(o => o.QuotationSalePromotionItem)
                    .Where(o => o.SalePromotionID == model.ID && o.MainSalePromotionItemID == null).ToListAsync();

                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => SalePromotionItemDTO.CreateFromModel(o, null, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var freeModels = await db.SalePromotionFreeItems
                    .Include(o => o.MasterSalePromotionFreeItem)
                    .Include(o => o.QuotationSalePromotionFreeItem)
                    .Where(o => o.SalePromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => SalePromotionItemDTO.CreateFromModel(null, o, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var creditModels = await db.SalePromotionCreditCardItems
                    .Include(o => o.MasterSalePromotionCreditCardItem)
                    .Include(o => o.QuotationSalePromotionCreditCardItem)
                    .Where(o => o.SalePromotionID == model.ID).ToListAsync();

                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => SalePromotionItemDTO.CreateFromModel(null, null, o, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var promotionItemIDs = new List<Guid?>();
                if (promotionItems.Count > 0)
                {
                    promotionItemIDs.AddRange(promotionItems.Select(o => o.FromMasterSalePromotionItemID).ToList());
                }

                var itemResults = new List<SalePromotionItemDTO>();
                if (model.MasterPromotion != null)
                {
                    var promotionitemStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();

                    var masterPromotionItems = new List<SalePromotionItemDTO>();

                    var qutation = await db.Bookings.Where(o => o.ID == model.BookingID).FirstOrDefaultAsync();
                    var projectID = await db.Units.Where(o => o.ID == qutation.UnitID).FirstAsync();
                    var query = from a in db.MasterSaleHouseModelItems.Where(o => o.ModelID == projectID.ModelID)
                                join ms in db.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == model.MasterSalePromotionID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID == promotionitemStatusActive)
                                on a.MasterSalePromotionItemID equals ms.ID
                                select ms;
                    var masterItemModels = query.ToList();

                    //   var masterItemModels = await db.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID == promotionitemStatusActive).ToListAsync();
                    if (masterItemModels.Count > 0)
                    {
                        var items = masterItemModels.Select(o => SalePromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var query2 = from a in db.MasterSaleHouseModelFreeItems.Where(o => o.ModelID == projectID.ModelID)
                                 join ms in db.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == model.MasterSalePromotionID)
                                 on a.MasterSalePromotionFreeItemID equals ms.ID
                                 select ms;
                    var masterFreeModels = query2.ToList();

                    //  var masterFreeModels = await db.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterFreeModels.Count > 0)
                    {
                        var items = masterFreeModels.Select(o => SalePromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterCreditModels = await db.MasterSalePromotionCreditCardItems.Where(o => o.MasterSalePromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterCreditModels.Count > 0)
                    {
                        var items = masterCreditModels.Select(o => SalePromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }


                    if (masterPromotionItems.Count > 0)
                    {
                        var masterItems = masterPromotionItems.Where(o => !promotionItemIDs.Contains(o.FromMasterSalePromotionItemID)).ToList();
                        itemResults.AddRange(masterItems);
                    }
                }

                result.Items = new List<SalePromotionItemDTO>();
                result.Items.AddRange(promotionItems);

                if (itemResults.Count > 0)
                {
                    result.Items.AddRange(itemResults);
                }

                if (result.Items.Count > 0)
                {
                    var itemIds = itemModels.Select(o => o.ID).ToList() ?? new List<Guid>();

                    var resultList = result.Items.Where(o => itemIds.Contains((o.Id ?? Guid.Empty))).ToList() ?? new List<SalePromotionItemDTO>();
                    foreach (var itemX in resultList)
                    {
                        var subItems = itemX.SubItems ?? new List<SalePromotionItemDTO>();
                        foreach (var itemZ in subItems)
                        {
                            var subiItemId = itemZ.Id ?? Guid.Empty;
                            itemIds.Add(subiItemId);
                        }
                    }

                    var reqItems = await db.SalePromotionRequestItems
                            .Include(o => o.SalePromotionRequest)
                            .Include(o => o.SalePromotionRequest.SalePromotion)
                            .Include(o => o.SalePromotionRequest.PromotionRequestPRStatus)
                            .Include(o => o.SalePromotionItem)
                            .Where(o =>
                                    itemIds.Contains((o.SalePromotionItemID ?? Guid.Empty))
                                    && o.SalePromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.Approve
                                    && o.SalePromotionRequest.SalePromotion.ID == result.Id
                                    && !string.IsNullOrEmpty(o.PRNo)
                                ).ToListAsync() ?? new List<SalePromotionRequestItem>();


                    foreach (var reqItem in reqItems)
                    {
                        result.Items.Where(o => o.Id == reqItem.SalePromotionItemID).ToList().ForEach(o => o.SalePromotionRequestItemID = reqItem.ID);
                    }

                    var reqIds = reqItems.Select(o => (o.SalePromotionRequestID ?? Guid.Empty)).ToList() ?? new List<Guid>();

                    var deliveryItems = await db.SalePromotionDeliveryItems
                            .Include(o => o.SalePromotionDelivery)
                            .Where(o => reqIds.Contains(o.SalePromotionDelivery.SalePromotionRequestID) && !string.IsNullOrEmpty(o.SalePromotionDelivery.DeliveryNo))
                            .ToListAsync() ?? new List<SalePromotionDeliveryItem>();

                    var deliveryItemIds = deliveryItems.Select(o => o.ID).ToList() ?? new List<Guid>();

                    var stockItems = await db.SalePromotionDeliveryStockItems
                        .Where(o => deliveryItemIds.Contains((o.SalePromotionDeliveryItemID ?? Guid.Empty)))
                        .ToListAsync() ?? new List<SalePromotionDeliveryStockItem>();

                    foreach (var itemA in result.Items)
                    {
                        var reqItemByItems = reqItems
                                        .Where(o => o.SalePromotionItemID == itemA.Id)
                                        .ToList() ?? new List<SalePromotionRequestItem>();

                        var reqByItemIds = reqItemByItems.Select(o => (o.SalePromotionRequestID ?? Guid.Empty)).ToList() ?? new List<Guid>();

                        //var deliveryItemByItems = deliveryItems
                        //                .Where(o => reqByItemIds.Contains(o.SalePromotionDelivery.SalePromotionRequestID))
                        //                .ToList() ?? new List<SalePromotionDeliveryItem>();

                        var deliveryItemByItems = deliveryItems
                                        .Where(o => o.SalePromotionRequestItemID == itemA.SalePromotionRequestItemID)
                                        .ToList() ?? new List<SalePromotionDeliveryItem>();

                        var deliveryByItemIds = deliveryItemByItems.Select(o => o.ID).ToList() ?? new List<Guid>();

                        var stockItemByItems = stockItems
                            .Where(o => deliveryByItemIds.Contains((o.SalePromotionDeliveryItemID ?? Guid.Empty)))
                            .ToList() ?? new List<SalePromotionDeliveryStockItem>();

                        itemA.PRNo = string.Join(Environment.NewLine, reqItemByItems.OrderBy(o => (o.PRNo ?? "")).Select(o => (o.PRNo ?? "")).ToList());
                        itemA.RequestQuantity = reqItemByItems.Sum(o => o.Quantity);
                        itemA.DeliveryQuantity = deliveryItemByItems.Sum(o => o.Quantity);
                        itemA.SerialNo = string.Join(",", stockItemByItems.Select(o => (o.IsSerial == true) ? o.SerialNo ?? "" : o.ReferenceStockId).ToList());

                        var SubItems = itemA.SubItems ?? new List<SalePromotionItemDTO>();
                        foreach (var itemB in SubItems)
                        {
                            var reqItemBySubItems = reqItems
                                            .Where(o => o.SalePromotionItemID == itemB.Id)
                                            .ToList() ?? new List<SalePromotionRequestItem>();

                            var reqBySubItemIds = reqItemBySubItems.Select(o => (o.SalePromotionRequestID ?? Guid.Empty)).ToList() ?? new List<Guid>();

                            var deliveryItemBySubItems = deliveryItems
                                            .Where(o => reqBySubItemIds.Contains(o.SalePromotionDelivery.SalePromotionRequestID))
                                            .ToList() ?? new List<SalePromotionDeliveryItem>();

                            var deliveryBySubItemIds = deliveryItemBySubItems.Select(o => o.ID).ToList() ?? new List<Guid>();

                            var stockItemBySubItems = stockItems
                                .Where(o => deliveryBySubItemIds.Contains((o.SalePromotionDeliveryItemID ?? Guid.Empty)))
                                .ToList() ?? new List<SalePromotionDeliveryStockItem>();

                            itemB.PRNo = String.Join(Environment.NewLine, reqItemBySubItems.OrderBy(o => (o.PRNo ?? "")).Select(o => (o.PRNo ?? "")).ToList());

                        }

                        itemA.IsDisabled = CreateDisabled(itemA.MaterialGroupKey, itemA.IsSelected, itemA.ItemType.ToString());
                        //

                    }



                }

                return result;
            }
            else
            {
                return null;
            }
        }


        public static bool CreateDisabled(string MaterialGroupKey, bool IsSelect, string creditCardType)
        {
            //
            List<string> key = new List<string>();
            key.Add("PLV100");
            key.Add("7V700");
            key.Add("EST100");
            key.Add("HST100");
            key.Add("NST100");
            bool isCreditCard = false;
            if (creditCardType.Equals("CreditCard"))
            {
                isCreditCard = true;
            }

            // 

            if (!key.Contains(MaterialGroupKey) && !isCreditCard)
            {
                return true;
            } 
            //else if (!key.Contains(MaterialGroupKey) && IsSelect)
            //{
            //    return true;
            //}
            else
            {
                return false;
            }
        }
    }
}
