using Base.DTOs.SAL;
using Database.Models;
using Database.Models.DbQueries.PRM;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายการสิ่งของที่ส่งมอบโปรโอน
    /// </summary>
    public class TransferPromotionDeliveryItemDTO : BaseDTO
    {
        /// <summary>
        /// ผูกการส่งมอบโปรโมชั่นโอน
        /// </summary>
        public Guid? TransferPromotionDeliveryID { get; set; }

        /// <summary>
        /// ผูกสิ่งของเบิกโปรโมชั่นโอน
        /// </summary>
        public Guid? TransferPromotionRequestItemID { get; set; }

        /// <summary>
        /// ข้อมูลมาจาก Master
        /// </summary>
        public bool IsMaster { get; set; }

        /// <summary>
        /// ชื่อผลิตภัณฑ์ (TH)
        /// </summary>
        public string NameTH { get; set; }

        /// <summary>
        /// จำนวน
        /// </summary>
        public int RequestQuantity { get; set; }

        /// <summary>
        /// ราคาต่อหน่วย (บาท)
        /// </summary>
        public decimal PricePerUnit { get; set; }

        /// <summary>
        /// ราคารวม (บาท)
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// ส่งมอบแล้ว
        /// </summary>
        public int DeliveryQuantity { get; set; }

        /// <summary>
        /// คงเหลือส่งมอบ
        /// </summary>
        public int RemainingDeliveryQuantity { get; set; }

        /// <summary>
        /// จำนวนที่ส่งมอบ
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// เลขที่ SerialNo
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// เลขที่ใบส่งมอบ
        /// </summary>
        public string DeliveryNo { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public string UnitTH { get; set; }

        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<TransferPromotionItemDTO> SubItems { get; set; }

        ///// <summary>
        ///// รายการสตอค
        ///// </summary>
        //public List<TransferPromotionDeliveryStockItem> DeliveryStockItems { get; set; }

        /// <summary>
        /// รายการเลือกของจากสตอค
        /// </summary>
        public List<dbqStockItemList> SelectStockItems { get; set; }


        public async static Task<TransferPromotionDeliveryItemDTO> CreateFromModelAsync(TransferPromotionDeliveryItem model, DatabaseContext DB)
        {
            if (model != null)
            {
                var itemResults = await DB.TransferPromotionDeliveryItems
                                   .Include(o => o.TransferPromotionDelivery)
                                   .Include(o => o.TransferPromotionRequestItem)
                                   .ThenInclude(o => o.TransferPromotionItem)
                                   .ThenInclude(o => o.MasterPromotionItem)
                                   .Include(o => o.TransferPromotionRequestItem)
                                   .Include(o => o.TransferPromotionRequestItem.TransferPromotionItem)
                                   .Include(o => o.TransferPromotionRequestItem.TransferPromotionItem.MasterPromotionItem)
                                   .Where(o => o.TransferPromotionRequestItemID == model.TransferPromotionRequestItemID).ToListAsync();

                var result = new TransferPromotionDeliveryItemDTO();

                result.Id = model.ID;
                result.TransferPromotionDeliveryID = model.TransferPromotionDelivery.ID;
                result.TransferPromotionRequestItemID = model.TransferPromotionRequestItem.ID;
                result.IsMaster = !model.TransferPromotionRequestItem.TransferPromotionItem.MainTransferPromotionItemID.HasValue;
                result.NameTH = model.TransferPromotionRequestItem.TransferPromotionItem.MasterPromotionItem.NameTH;
                result.RequestQuantity = itemResults.Select(o => o.ReceiveQuantity).FirstOrDefault();//model.TransferPromotionRequestItem.Quantity;
                result.PricePerUnit = model.TransferPromotionRequestItem.TransferPromotionItem.PricePerUnit;
                result.TotalPrice = model.TransferPromotionRequestItem.TransferPromotionItem.TotalPrice;

                result.DeliveryQuantity = model.DeliveryQuantity;
                result.RemainingDeliveryQuantity = model.RemainingDeliveryQuantity;
                result.Quantity = model.Quantity;

                result.Remark = model.Remark;
                //result.SerialNo = model.SerialNo;
                result.UnitTH = model?.TransferPromotionRequestItem?.TransferPromotionItem?.MasterPromotionItem?.UnitTH;
                result.DeliveryNo = model?.TransferPromotionDelivery?.DeliveryNo;

                var promotionItem = model?.TransferPromotionRequestItem?.TransferPromotionItem ?? new TransferPromotionItem();
                var subItems = DB.TransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainTransferPromotionItemID == promotionItem.ID).ToList();
                result.SubItems = new List<TransferPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new TransferPromotionItemDTO()
                        {
                            FromMasterTansferPromotionItemID = item.MasterTransferPromotionItemID,
                            FromQuotationTansferPromotionItemID = promotionItem.QuotationTransferPromotionItemID,
                            NameTH = item.MasterPromotionItem.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.MasterPromotionItem.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = promotionItem.ID,
                            Updated = promotionItem.Updated,
                            UpdatedBy = promotionItem.UpdatedBy?.DisplayName,
                            IsSelected = true,
                            TransferPromotionID = promotionItem.TransferPromotionID
                        };

                        var requestSub = DB.TransferPromotionRequestItems
                                .Include(o => o.TransferPromotionRequest)
                                .ThenInclude(o => o.PromotionRequestPRStatus)
                                .Where(o => o.TransferPromotionItemID == item.ID
                                        && o.TransferPromotionRequest.PromotionRequestPRStatus.Key != "4").ToList();

                        promoItem.RequestedQuantity = result.DeliveryQuantity;
                        promoItem.RemainingRequestQuantity = result.RemainingDeliveryQuantity;
                        promoItem.RequestQuantity = result.Quantity;
                        promoItem.ReceiveDate = DateTime.Now.Date;

                        promoItem.DenyRemark = String.Join(",", requestSub.Select(o => o.DenyRemark ?? "").ToList());
                        promoItem.RequestNo = String.Join(",", requestSub.Select(o => o.TransferPromotionRequest?.RequestNo ?? "").ToList());
                        promoItem.PRNo = String.Join(",", requestSub.Select(o => o.PRNo ?? "").ToList());

                        result.SubItems.Add(promoItem);
                    }
                }

                var stockItems = DB.TransferPromotionDeliveryStockItems
                    .Where(o => o.TransferPromotionDeliveryItemID == model.ID).ToList();
                //result.DeliveryStockItems = stockItems;

                if (stockItems.Count > 0)
                {
                    result.SerialNo = String.Join(",", stockItems.Select(o => (o.IsSerial == true) ? o.SerialNo ?? "" : o.ReferenceStockId).ToList());
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<TransferPromotionDeliveryItemDTO> CreateFromMasterModelAsync(TransferPromotionRequestItem model, Guid TransferPromotionRequestItemID, DatabaseContext DB)
        {
            if (model != null)
            {
                var itemResults = await DB.TransferPromotionDeliveryItems
                                .Include(o => o.TransferPromotionDelivery)
                                .Include(o => o.TransferPromotionRequestItem)
                                .ThenInclude(o => o.TransferPromotionItem)
                                .ThenInclude(o => o.MasterPromotionItem)
                                .Where(o => o.TransferPromotionRequestItem.ID == TransferPromotionRequestItemID).ToListAsync();

                var result = new TransferPromotionDeliveryItemDTO();
                result.Id = model.ID;
                result.TransferPromotionDeliveryID = null;
                result.TransferPromotionRequestItemID = null;
                result.IsMaster = !model.TransferPromotionItem.MainTransferPromotionItemID.HasValue;
                result.NameTH = model.TransferPromotionItem.MasterPromotionItem.NameTH;
                result.RequestQuantity = model.Quantity;
                result.PricePerUnit = model.TransferPromotionItem.PricePerUnit;
                result.TotalPrice = model.TransferPromotionItem.TotalPrice;

                result.DeliveryQuantity = model.RequestQuantity;
                result.RemainingDeliveryQuantity = model.RemainingRequestQuantity;
                result.Quantity = model.RequestQuantity;

                result.Remark = "";
                result.SerialNo = "";
                result.UnitTH = model?.TransferPromotionItem?.MasterPromotionItem?.UnitTH;

                var promotionItem = model?.TransferPromotionItem ?? new TransferPromotionItem();
                var subItems = DB.TransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainTransferPromotionItemID == promotionItem.ID).ToList();
                result.SubItems = new List<TransferPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new TransferPromotionItemDTO()
                        {
                            FromMasterTansferPromotionItemID = item.MasterTransferPromotionItemID,
                            FromQuotationTansferPromotionItemID = promotionItem.QuotationTransferPromotionItemID,
                            NameTH = item.MasterPromotionItem.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.MasterPromotionItem.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = promotionItem.ID,
                            Updated = promotionItem.Updated,
                            UpdatedBy = promotionItem.UpdatedBy?.DisplayName,
                            IsSelected = true,
                            TransferPromotionID = promotionItem.TransferPromotionID
                        };

                        var requestSub = DB.TransferPromotionRequestItems
                                .Include(o => o.TransferPromotionRequest)
                                .ThenInclude(o => o.PromotionRequestPRStatus)
                                .Where(o => o.TransferPromotionItemID == item.ID
                                        && o.TransferPromotionRequest.PromotionRequestPRStatus.Key != PromotionRequestPRStatusKeys.Reject).ToList();

                        promoItem.RequestedQuantity = result.DeliveryQuantity;
                        promoItem.RemainingRequestQuantity = result.RemainingDeliveryQuantity;
                        promoItem.RequestQuantity = result.Quantity;

                        promoItem.ReceiveDate = DateTime.Now.Date;
                        promoItem.DenyRemark = String.Join(",", requestSub.Select(o => o.DenyRemark ?? "").ToList());
                        promoItem.RequestNo = String.Join(",", requestSub.Select(o => o.TransferPromotionRequest?.RequestNo ?? "").ToList());
                        promoItem.PRNo = String.Join(",", requestSub.Select(o => o.PRNo ?? "").ToList());

                        result.SubItems.Add(promoItem);
                    }
                }

                //result.DeliveryStockItems = new List<TransferPromotionDeliveryStockItem>();

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
