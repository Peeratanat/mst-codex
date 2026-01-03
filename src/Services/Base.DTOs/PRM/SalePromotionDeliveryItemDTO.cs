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
    /// รายการสิ่งของที่ส่งมอบโปรขาย
    /// </summary>
    public class SalePromotionDeliveryItemDTO : BaseDTO
    {
        /// <summary>
        /// ผูกการส่งมอบโปรโมชั่นขาย
        /// </summary>
        public Guid? SalePromotionDeliveryID { get; set; }

        /// <summary>
        /// ผูกสิ่งของเบิกโปรโมชั่นขาย
        /// </summary>
        public Guid? SalePromotionRequestItemID { get; set; }

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
        public List<SalePromotionItemDTO> SubItems { get; set; }

        ///// <summary>
        ///// รายการสตอค
        ///// </summary>
        //public List<SalePromotionDeliveryStockItem> DeliveryStockItems { get; set; }

        /// <summary>
        /// รายการเลือกของจากสตอค
        /// </summary>
        public List<dbqStockItemList> SelectStockItems { get; set; }


        public async static Task<SalePromotionDeliveryItemDTO> CreateFromModelAsync(SalePromotionDeliveryItem model, DatabaseContext DB)
        {
            if (model != null)
            {
                var itemResults = await DB.SalePromotionDeliveryItems
                                   .Include(o => o.SalePromotionDelivery)
                                   .Include(o => o.SalePromotionRequestItem)
                                   .ThenInclude(o => o.SalePromotionItem)
                                   .ThenInclude(o => o.MasterPromotionItem)
                                   .Include(o => o.SalePromotionRequestItem)
                                   .Include(o => o.SalePromotionRequestItem.SalePromotionItem)
                                   .Include(o => o.SalePromotionRequestItem.SalePromotionItem.MasterPromotionItem)
                                   .Where(o => o.SalePromotionRequestItemID == model.SalePromotionRequestItemID).ToListAsync();

                var result = new SalePromotionDeliveryItemDTO();

                result.Id = model.ID;
                result.SalePromotionDeliveryID = model.SalePromotionDelivery.ID;
                result.SalePromotionRequestItemID = model.SalePromotionRequestItem.ID;
                result.IsMaster = !model.SalePromotionRequestItem.SalePromotionItem.MainSalePromotionItemID.HasValue;
                result.NameTH = model.SalePromotionRequestItem.SalePromotionItem.MasterPromotionItem.NameTH;
                result.RequestQuantity = itemResults.Select(o => o.ReceiveQuantity).FirstOrDefault();//itemResults.Select(o=>o.SalePromotionRequestItem.Quantity).FirstOrDefault();
                result.PricePerUnit = model.SalePromotionRequestItem.SalePromotionItem.PricePerUnit;
                result.TotalPrice = model.SalePromotionRequestItem.SalePromotionItem.TotalPrice;
               
                result.DeliveryQuantity = model.DeliveryQuantity;
                result.RemainingDeliveryQuantity = model.RemainingDeliveryQuantity;
                result.Quantity = model.Quantity;
               
                result.Remark = model?.Remark;
                //result.SerialNo = model?.SerialNo;
                result.UnitTH = model?.SalePromotionRequestItem?.SalePromotionItem?.MasterPromotionItem?.UnitTH;
                result.DeliveryNo = model?.SalePromotionDelivery?.DeliveryNo;

                var promotionItem = model?.SalePromotionRequestItem?.SalePromotionItem ?? new SalePromotionItem();
                var subItems = DB.SalePromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainSalePromotionItemID == promotionItem.ID).ToList();
                result.SubItems = new List<SalePromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new SalePromotionItemDTO()
                        {
                            FromMasterSalePromotionItemID = item.MasterSalePromotionItemID,
                            FromQuotationSalePromotionItemID = promotionItem.QuotationSalePromotionItemID,
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
                            SalePromotionID = promotionItem.SalePromotionID
                        };

                        var requestSub = DB.SalePromotionRequestItems
                                .Include(o => o.SalePromotionRequest)
                                .ThenInclude(o => o.PromotionRequestPRStatus)
                                .Where(o => o.SalePromotionItemID == item.ID
                                        && o.SalePromotionRequest.PromotionRequestPRStatus.Key != "4").ToList();

                        promoItem.RequestedQuantity = result.DeliveryQuantity;
                        promoItem.RemainingRequestQuantity = result.RemainingDeliveryQuantity;
                        promoItem.RequestQuantity = result.Quantity;
                        promoItem.ReceiveDate = DateTime.Now.Date;

                        promoItem.ReceiveDate = DateTime.Now.Date;
                        promoItem.DenyRemark = String.Join(",", requestSub.Select(o => o.DenyRemark ?? "").ToList());
                        promoItem.RequestNo = String.Join(",", requestSub.Select(o => o.SalePromotionRequest?.RequestNo ?? "").ToList());
                        promoItem.PRNo = String.Join(",", requestSub.Select(o => o.PRNo ?? "").ToList());

                        result.SubItems.Add(promoItem);
                    }
                }

                var stockItems = DB.SalePromotionDeliveryStockItems
                    .Where(o => o.SalePromotionDeliveryItemID == model.ID).ToList();
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

        public async static Task<SalePromotionDeliveryItemDTO> CreateFromMasterModelAsync(SalePromotionRequestItem model, Guid SalePromotionRequestItemID, DatabaseContext DB)
        {
            if (model != null)
            {
                var itemResults = await DB.SalePromotionDeliveryItems
                                .Include(o => o.SalePromotionDelivery)
                                .Include(o => o.SalePromotionRequestItem)
                                .ThenInclude(o => o.SalePromotionItem)
                                .ThenInclude(o => o.MasterPromotionItem)
                                .Where(o => o.SalePromotionRequestItem.ID == SalePromotionRequestItemID).ToListAsync();

                var result = new SalePromotionDeliveryItemDTO();
                result.Id = model.ID;
                result.SalePromotionDeliveryID = null;
                result.SalePromotionRequestItemID = null;
                result.IsMaster = !model.SalePromotionItem.MainSalePromotionItemID.HasValue;
                result.NameTH = model.SalePromotionItem.MasterPromotionItem.NameTH;
                result.RequestQuantity = model.Quantity;
                result.PricePerUnit = model.SalePromotionItem.PricePerUnit;
                result.TotalPrice = model.SalePromotionItem.TotalPrice;

                result.DeliveryQuantity = model.RequestQuantity;
                result.RemainingDeliveryQuantity = model.RemainingRequestQuantity;
                result.Quantity = model.RequestQuantity;

                result.Remark = "";
                result.SerialNo = "";
                result.UnitTH = model.SalePromotionItem?.MasterPromotionItem?.UnitTH;

                var promotionItem = model?.SalePromotionItem ?? new SalePromotionItem();
                var subItems = DB.SalePromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainSalePromotionItemID == promotionItem.ID).ToList();
                result.SubItems = new List<SalePromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new SalePromotionItemDTO()
                        {
                            FromMasterSalePromotionItemID = item.MasterSalePromotionItemID,
                            FromQuotationSalePromotionItemID = promotionItem.QuotationSalePromotionItemID,
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
                            SalePromotionID = promotionItem.SalePromotionID
                        };

                        var requestSub = DB.SalePromotionRequestItems
                                .Include(o => o.SalePromotionRequest)
                                .ThenInclude(o => o.PromotionRequestPRStatus)
                                .Where(o => o.SalePromotionItemID == item.ID
                                        && o.SalePromotionRequest.PromotionRequestPRStatus.Key != PromotionRequestPRStatusKeys.Reject).ToList();

                        promoItem.RequestedQuantity = result.DeliveryQuantity;
                        promoItem.RemainingRequestQuantity = result.RemainingDeliveryQuantity;
                        promoItem.RequestQuantity = result.Quantity;

                        promoItem.ReceiveDate = DateTime.Now.Date;
                        promoItem.DenyRemark = String.Join(",", requestSub.Select(o => o.DenyRemark ?? "").ToList());
                        promoItem.RequestNo = String.Join(",", requestSub.Select(o => o.SalePromotionRequest?.RequestNo ?? "").ToList());
                        promoItem.PRNo = String.Join(",", requestSub.Select(o => o.PRNo ?? "").ToList());

                        result.SubItems.Add(promoItem);
                    }
                }

                //result.DeliveryStockItems = new List<SalePromotionDeliveryStockItem>();

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
