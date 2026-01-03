using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.DTOs.SAL
{
    public class SalePromotionItemDTO : BaseDTO
    {
        /// <summary>
        /// ID ของ MasterSalePromotionItem/MasterSalePromotionFreeItem/MasterBookingCreditCardItem
        /// </summary>
        public Guid? FromMasterSalePromotionItemID { get; set; }
        /// <summary>
        /// ID ของ QuotationSalePromotionItem/QuotationSalePromotionFreeItem/QuotationBookingCreditCardItem
        /// </summary>
        public Guid? FromQuotationSalePromotionItemID { get; set; }
        /// <summary>
        /// ชื่อผลิตภัณฑ์ (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย (บาท)
        /// </summary>
        public decimal PricePerUnit { get; set; }
        /// <summary>
        /// ราคารวม (บาท)
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public string UnitTH { get; set; }
        /// <summary>
        /// เลขที่ PR
        /// </summary>
        public string PRNo { get; set; }
        /// <summary>
        /// ชนิดของรายการโปรโมชั่น
        /// Item = 0,
        /// FreeItem = 1,
        /// CreditCard = 2
        /// </summary>
        public PromotionItemType ItemType { get; set; }
        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<SalePromotionItemDTO> SubItems { get; set; }
        /// <summary>
        /// รายการที่ถูกเลือก
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// เบิกแล้ว
        /// </summary>
        public int RequestedQuantity { get; set; }
        /// <summary>
        /// คงเหลือเบิก
        /// </summary>
        public int RemainingRequestQuantity { get; set; }
        /// <summary>
        /// จำนวนที่เบิก
        /// </summary>
        public int RequestQuantity { get; set; }
        /// <summary>
        /// วันที่คาดว่าจะได้รับ (default = วันที่ปัจจุบัน)
        /// </summary>
        public DateTime? ReceiveDate { get; set; }
        /// <summary>
        /// หมายเหตุไม่เบิก
        /// </summary>
        public string DenyRemark { get; set; }
        /// <summary>
        /// เลขที่ใบเบิก
        /// </summary>
        public string RequestNo { get; set; }

        /// <summary>
        /// SalePromotionID
        /// </summary>
        public Guid? SalePromotionID { get; set; }
        /// <summary>
        ///  การจัดซื้อ?
        /// </summary>
        public bool IsPurchasing { get; set; }
        /// <summary>
        /// masterTotalPrice
        /// </summary>
        public decimal MasterTotalprice { get; set; }

        /// <summary>
        /// เลขที่ SerialNo
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// เลขที่ใบส่งมอบ
        /// </summary>
        public string DeliveryNo { get; set; }
        /// <summary>
        /// จำนวนที่ส่งมอบ
        /// </summary>
        public int DeliveryQuantity { get; set; }

        public Guid? SalePromotionRequestItemID { get; set; }

        public bool? IsDisabled { get; set; }
        public string MaterialGroupKey { get; set; }

        public static SalePromotionItemDTO CreateFromMasterModel(MasterSalePromotionItem itemModel, MasterSalePromotionFreeItem freeItemModel, MasterSalePromotionCreditCardItem creditModel, DatabaseContext db)
        {
            SalePromotionItemDTO result = new SalePromotionItemDTO();
            if (itemModel != null)
            {
                result.MaterialGroupKey = itemModel.MaterialGroupKey;
                result.FromMasterSalePromotionItemID = itemModel.ID;
                result.NameTH = itemModel.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.PricePerUnit;
                result.TotalPrice = itemModel.TotalPrice;
                result.MasterTotalprice = itemModel.TotalPrice;
                result.UnitTH = itemModel.UnitTH;
                result.ItemType = PromotionItemType.Item;
                result.IsSelected = false;
                result.IsPurchasing = itemModel.IsPurchasing;

                var subItems = db.MasterSalePromotionItems.Where(o => o.MainPromotionItemID == itemModel.ID && o.ExpireDate >= DateTime.Now).ToList();
                result.SubItems = new List<SalePromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new SalePromotionItemDTO()
                        {
                            FromMasterSalePromotionItemID = item.ID,
                            NameTH = item.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.UnitTH,
                            ItemType = PromotionItemType.Item,
                            IsSelected = false,
                        };

                        result.SubItems.Add(promoItem);
                    }
                }

                return result;
            }
            else if (freeItemModel != null)
            {
                result.FromMasterSalePromotionItemID = freeItemModel.ID;
                result.NameTH = freeItemModel.NameTH;
                result.Quantity = freeItemModel.Quantity;
                result.UnitTH = freeItemModel.UnitTH;
                result.ItemType = PromotionItemType.FreeItem;
                result.IsSelected = false;
                return result;
            }
            else if (creditModel != null)
            {
                result.FromMasterSalePromotionItemID = creditModel.ID;
                result.NameTH = creditModel.NameTH;
                result.Quantity = creditModel.Quantity;
                result.PricePerUnit = (decimal)creditModel.Fee;
                result.TotalPrice = creditModel.TotalPrice;
                result.MasterTotalprice = creditModel.TotalPrice;
                result.UnitTH = creditModel.UnitTH;
                result.ItemType = PromotionItemType.CreditCard;
                result.IsSelected = false;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static SalePromotionItemDTO CreateFromModel(SalePromotionItem itemModel, SalePromotionFreeItem freeItemModel, SalePromotionCreditCardItem creditModel, DatabaseContext db)
        {
            SalePromotionItemDTO result = new SalePromotionItemDTO();
            if (itemModel != null)
            {
                result.MaterialGroupKey = itemModel.MasterPromotionItem?.MaterialGroupKey;
                result.FromMasterSalePromotionItemID = itemModel.MasterSalePromotionItemID;
                result.FromQuotationSalePromotionItemID = itemModel.QuotationSalePromotionItemID;
                result.NameTH = itemModel.MasterPromotionItem?.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.PricePerUnit;
                result.TotalPrice = itemModel.TotalPrice;
                result.MasterTotalprice = itemModel.TotalPrice / itemModel.Quantity;
                result.UnitTH = itemModel.MasterPromotionItem?.UnitTH;
                result.ItemType = PromotionItemType.Item;
                result.Id = itemModel.ID;
                result.Updated = itemModel.Updated;
                result.UpdatedBy = itemModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                result.SalePromotionID = itemModel.SalePromotionID;
                result.IsPurchasing = (itemModel.MasterPromotionItem != null) ? itemModel.MasterPromotionItem.IsPurchasing : false;
                var subItems = db.SalePromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainSalePromotionItemID == itemModel.ID).ToList();
                result.SubItems = new List<SalePromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new SalePromotionItemDTO()
                        {
                            FromMasterSalePromotionItemID = item.MasterSalePromotionItemID,
                            FromQuotationSalePromotionItemID = itemModel.QuotationSalePromotionItemID,
                            NameTH = item.MasterPromotionItem?.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.MasterPromotionItem?.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = item.ID,
                            Updated = itemModel.Updated,
                            UpdatedBy = itemModel.UpdatedBy?.DisplayName,
                            IsSelected = true,
                            SalePromotionID = itemModel.SalePromotionID
                        };

                        result.SubItems.Add(promoItem);
                    }
                }

                return result;
            }
            else if (freeItemModel != null)
            {
                result.FromMasterSalePromotionItemID = freeItemModel.MasterSalePromotionFreeItemID;
                result.FromQuotationSalePromotionItemID = freeItemModel.QuotationSalePromotionFreeItemID;
                result.NameTH = freeItemModel.MasterSalePromotionFreeItem?.NameTH;
                result.Quantity = freeItemModel.Quantity;
                result.UnitTH = freeItemModel.MasterSalePromotionFreeItem?.UnitTH;
                result.ItemType = PromotionItemType.FreeItem;
                result.Id = freeItemModel.ID;
                result.Updated = freeItemModel.Updated;
                result.UpdatedBy = freeItemModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                return result;
            }
            else if (creditModel != null)
            {
                result.FromMasterSalePromotionItemID = creditModel.MasterSalePromotionCreditCardItemID;
                result.FromQuotationSalePromotionItemID = creditModel.QuotationSalePromotionCreditCardItemID;
                result.NameTH = creditModel.MasterSalePromotionCreditCardItem?.NameTH;
                result.Quantity = creditModel.Quantity;
                result.PricePerUnit = (decimal)creditModel.MasterSalePromotionCreditCardItem.Fee;
                result.TotalPrice = creditModel.TotalPrice;
                result.MasterTotalprice = creditModel.TotalPrice / creditModel.Quantity;
                result.UnitTH = creditModel.MasterSalePromotionCreditCardItem?.UnitTH;
                result.ItemType = PromotionItemType.CreditCard;
                result.Id = creditModel.ID;
                result.Updated = creditModel.Updated;
                result.UpdatedBy = creditModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                return result;
            }
            else
            {
                return null;
            }
        }

        public static SalePromotionItemDTO CreateFromRequestModelAsync(SalePromotionItem itemModel, DatabaseContext db)
        {
            SalePromotionItemDTO result = new SalePromotionItemDTO();

            if (itemModel != null)
            {
                result.FromMasterSalePromotionItemID = itemModel.MasterSalePromotionItemID;
                result.FromQuotationSalePromotionItemID = itemModel.QuotationSalePromotionItemID;
                result.NameTH = itemModel.MasterPromotionItem?.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.PricePerUnit;
                result.TotalPrice = itemModel.TotalPrice;
                result.UnitTH = itemModel.MasterPromotionItem?.UnitTH;
                result.ItemType = PromotionItemType.Item;
                result.Id = itemModel.ID;
                result.Updated = itemModel.Updated;
                result.UpdatedBy = itemModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                result.SalePromotionID = itemModel.SalePromotionID;

                var request = db.SalePromotionRequestItems
                                .Include(o => o.SalePromotionItem)
                                .Include(o => o.SalePromotionRequest)
                                .ThenInclude(o => o.PromotionRequestPRStatus)
                                .Where(o => o.SalePromotionItemID == itemModel.ID
                                            //&& (o.SalePromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.Approve
                                            //    || o.SalePromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.ApproveSomeUnit
                                            //    || o.SalePromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.WaitApprove)
                                            && (
                                                !string.IsNullOrEmpty(o.SalePromotionRequest.RequestNo)
                                                || !string.IsNullOrEmpty(o.DenyRemark)
                                            ) 
                                            && o.SalePromotionRequest.IsDeleted == false
                                        //&& !o.SalePromotionItem.MainSalePromotionItemID.HasValue
                                        ).ToList() ?? new List<SalePromotionRequestItem>();

                var masterReceiveDate = db.MasterSalePromotionItems.Where(o => o.ID == itemModel.MasterSalePromotionItemID).Select(o => o.ReceiveDays).FirstOrDefault();
                var EstimateRequestDate = request.Where(o => o.EstimateRequestDate.HasValue).OrderByDescending(o => o.EstimateRequestDate).Select(o => o.EstimateRequestDate).FirstOrDefault();
                result.ReceiveDate = EstimateRequestDate.HasValue ? EstimateRequestDate : DateTime.Now.AddDays(Convert.ToDouble(masterReceiveDate));

                result.RequestedQuantity = request.Sum(o => o.Quantity);
                result.RemainingRequestQuantity = itemModel.Quantity - result.RequestedQuantity; // request.Sum(o => o.Quantity);
                result.RequestQuantity = 0;

                if (request.Where(o => !String.IsNullOrEmpty(o.DenyRemark)).ToList().Any())
                {
                    result.DenyRemark = String.Join(Environment.NewLine, request.Select(o => o.DenyRemark ?? "").ToList());
                }
                else
                {
                    result.DenyRemark = "";
                }

                if (request.Where(o => !String.IsNullOrEmpty(o.SalePromotionRequest?.RequestNo)).ToList().Any())
                {
                    result.RequestNo = String.Join(Environment.NewLine, request.OrderBy(o => (o.SalePromotionRequest?.RequestNo ?? "")).Select(o => (o.SalePromotionRequest?.RequestNo ?? "")).ToList());
                }
                else
                {
                    result.RequestNo = "";
                }

                if (request.Where(o => !String.IsNullOrEmpty(o.PRNo)).ToList().Any())
                {
                    result.PRNo = String.Join(Environment.NewLine, request.OrderBy(o => (o.PRNo ?? "")).Select(o => (o.PRNo ?? "")).ToList());
                }
                else
                {
                    result.PRNo = "";
                }

                var subItems = db.SalePromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainSalePromotionItemID == itemModel.ID
                            && o.SalePromotionID == itemModel.SalePromotionID).ToList();

                result.SubItems = new List<SalePromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new SalePromotionItemDTO()
                        {
                            FromMasterSalePromotionItemID = item.MasterSalePromotionItemID,
                            FromQuotationSalePromotionItemID = itemModel.QuotationSalePromotionItemID,
                            NameTH = item.MasterPromotionItem?.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.MasterPromotionItem?.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = item.ID,
                            Updated = itemModel.Updated,
                            UpdatedBy = itemModel.UpdatedBy?.DisplayName,
                            IsSelected = true,
                            SalePromotionID = itemModel.SalePromotionID,

                        };

                        var requestSub = db.SalePromotionRequestItems
                                .Include(o => o.SalePromotionRequest)
                                .ThenInclude(o => o.PromotionRequestPRStatus)
                                .Where(o => o.SalePromotionItemID == item.ID
                                        && o.SalePromotionRequest.IsDeleted == false
                                ).FirstOrDefault();

                        promoItem.RequestedQuantity = result.RequestedQuantity;
                        promoItem.RemainingRequestQuantity = result.RemainingRequestQuantity;
                        promoItem.RequestQuantity = result.RequestQuantity;
                        promoItem.ReceiveDate = result.ReceiveDate;
                        promoItem.DenyRemark = result.DenyRemark;

                        if (requestSub != null)
                        {
                            promoItem.RequestNo = requestSub.SalePromotionRequest?.RequestNo;
                            promoItem.PRNo = requestSub.PRNo;

                            if (string.IsNullOrEmpty(promoItem.PRNo))
                            {
                                var prResult = db.PRRequestJobItemResults
                                        .Include(o => o.PRRequestJobItem)
                                        .Where(o => o.PRRequestJobItem.SalePromotionRequestItemID == requestSub.ID).FirstOrDefault();

                                if (prResult != null && prResult.IsError)
                                {
                                    promoItem.PRNo = prResult.ErrorCode + " : " + prResult.ErrorDescription;
                                    var msg = db.MasterCenters
                                       .Where(o => o.MasterCenterGroupKey == "SapMessage"
                                               && o.Key == prResult.ErrorCode.Replace("-", ""))
                                       .FirstOrDefault();

                                    if (msg != null)
                                    {
                                        promoItem.PRNo = msg.Name;
                                    }
                                }
                            }
                        }

                        result.SubItems.Add(promoItem);
                    }
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
