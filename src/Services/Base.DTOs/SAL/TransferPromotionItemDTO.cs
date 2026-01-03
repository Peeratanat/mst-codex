using Base.DTOs.MST;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.DTOs.SAL
{
    public class TransferPromotionItemDTO : BaseDTO
    {
        /// <summary>
        /// ID ของ MasterTransferPromotionItem/MasterTransferPromotionFreeItem/MasterTransferCreditCardItem
        /// </summary>
        public Guid? FromMasterTansferPromotionItemID { get; set; }
        /// <summary>
        /// ID ของ TransferPromotionItem/QuotationTransferPromotionFreeItem/QuotationTransferCreditCardItem
        /// </summary>
        public Guid? FromQuotationTansferPromotionItemID { get; set; }
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
        /// ชนิดของรายการโปรโมชั่น
        /// Item = 0,
        /// FreeItem = 1,
        /// CreditCard = 2
        /// </summary>
        public PromotionItemType ItemType { get; set; }
        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<TransferPromotionItemDTO> SubItems { get; set; }
        /// <summary>
        /// รายการที่ถูกเลือก
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// เลขที่ PR
        /// </summary>
        public string PRNo { get; set; }
        /// <summary>
        /// Serial Number
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// หมายเหตุไม่เบิกโปร
        /// </summary>
        public string Remark { get; set; }
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
        /// TransferPromotionID
        /// </summary>
        public Guid? TransferPromotionID { get; set; }
        /// <summary>
        ///  การจัดซื้อ?
        /// </summary>
        public bool IsPurchasing { get; set; }


        /// <summary>
        /// MasterTotalprice
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
        /// จำนวนส่งมอบ
        /// </summary>
        public int DeliveryQuantity { get; set; }

        public bool? IsDisabled { get; set; }
        public string MaterialGroupKey { get; set; }

        public static TransferPromotionItemDTO CreateFromModel(TransferPromotionItem itemModel, TransferPromotionFreeItem freeItemModel, TransferPromotionCreditCardItem creditModel, DatabaseContext db)
        {
            TransferPromotionItemDTO result = new TransferPromotionItemDTO();
            if (itemModel != null)
            {
                //itemModel.MasterTransferPromotionItem =  db.MasterTransferPromotionItems.Where(o=> o.ID == itemModel.MasterTransferPromotionItemID).FirstOrDefaultAsync();
                result.MaterialGroupKey = itemModel.MasterPromotionItem?.MaterialGroupKey;
                result.FromMasterTansferPromotionItemID = itemModel.MasterPromotionItem.ID; //.MasterTransferPromotionItemID;
                result.FromQuotationTansferPromotionItemID = itemModel.QuotationTransferPromotionItemID;
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
                result.IsPurchasing = itemModel.MasterPromotionItem.IsPurchasing;

                var subItems = db.TransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainTransferPromotionItemID == itemModel.ID).ToList();

                result.SubItems = new List<TransferPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new TransferPromotionItemDTO()
                        {
                            FromMasterTansferPromotionItemID = item.MasterPromotionItem.ID, //.MasterTransferPromotionItemID,
                            FromQuotationTansferPromotionItemID = itemModel.QuotationTransferPromotionItemID,
                            NameTH = item.MasterPromotionItem?.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.MasterPromotionItem?.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = item.ID,
                            Updated = itemModel.Updated,
                            UpdatedBy = itemModel.UpdatedBy?.DisplayName,
                            IsSelected = true
                        };

                        result.SubItems.Add(promoItem);
                    }
                }

                return result;
            }
            else if (freeItemModel != null)
            {
                result.FromMasterTansferPromotionItemID = freeItemModel.MasterTransferPromotionFreeItemID;
                result.FromQuotationTansferPromotionItemID = freeItemModel.QuotationTransferPromotionFreeItemID;
                result.NameTH = freeItemModel.MasterTransferPromotionFreeItem?.NameTH;
                result.Quantity = freeItemModel.Quantity;
                result.UnitTH = freeItemModel.MasterTransferPromotionFreeItem?.UnitTH;
                result.ItemType = PromotionItemType.FreeItem;
                result.Id = freeItemModel.ID;
                result.Updated = freeItemModel.Updated;
                result.UpdatedBy = freeItemModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                return result;
            }
            else if (creditModel != null)
            {
                result.FromMasterTansferPromotionItemID = creditModel.MasterTransferPromotionCreditCardItemID;
                result.FromQuotationTansferPromotionItemID = creditModel.QuotationTransferPromotionCreditCardItemID;
                result.NameTH = creditModel.MasterTransferPromotionCreditCardItem?.NameTH;
                result.Quantity = creditModel.Quantity;
                result.UnitTH = creditModel.MasterTransferPromotionCreditCardItem?.UnitTH;
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

        public static TransferPromotionItemDTO CreateFromMasterModel(MasterTransferPromotionItem itemModel, MasterTransferPromotionFreeItem freeItemModel, MasterTransferPromotionCreditCardItem creditModel, DatabaseContext db)
        {
            TransferPromotionItemDTO result = new TransferPromotionItemDTO();
            if (itemModel != null)
            {
                result.FromMasterTansferPromotionItemID = itemModel.ID;
                result.NameTH = itemModel.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.PricePerUnit;
                result.TotalPrice = itemModel.TotalPrice;
                result.UnitTH = itemModel.UnitTH;
                result.MasterTotalprice = itemModel.TotalPrice;
                result.ItemType = PromotionItemType.Item;
                result.IsSelected = false;
                result.IsPurchasing = itemModel.IsPurchasing;

                var subItems = db.MasterTransferPromotionItems.Where(o => o.MainPromotionItemID == itemModel.ID && o.ExpireDate >= DateTime.Now).ToList();
                result.SubItems = new List<TransferPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new TransferPromotionItemDTO()
                        {
                            FromMasterTansferPromotionItemID = item.ID,
                            NameTH = item.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.UnitTH,
                            ItemType = PromotionItemType.Item,
                            IsSelected = false
                        };

                        result.SubItems.Add(promoItem);
                    }
                }

                return result;
            }
            else if (freeItemModel != null)
            {
                result.FromMasterTansferPromotionItemID = freeItemModel.ID;
                result.NameTH = freeItemModel.NameTH;
                result.Quantity = freeItemModel.Quantity;
                result.UnitTH = freeItemModel.UnitTH;
                result.ItemType = PromotionItemType.FreeItem;
                result.IsSelected = false;
                return result;
            }
            else if (creditModel != null)
            {
                result.FromMasterTansferPromotionItemID = creditModel.ID;
                result.NameTH = creditModel.NameTH;
                result.Quantity = creditModel.Quantity;
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

        public static TransferPromotionItemDTO CreateFromRequestModelAsync(TransferPromotionItem itemModel, DatabaseContext db)
        {
            TransferPromotionItemDTO result = new TransferPromotionItemDTO();

            if (itemModel != null)
            {
                result.FromMasterTansferPromotionItemID = itemModel.MasterTransferPromotionItemID;
                result.FromQuotationTansferPromotionItemID = itemModel.QuotationTransferPromotionItemID;
                result.NameTH = itemModel.MasterPromotionItem.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.PricePerUnit;
                result.TotalPrice = itemModel.TotalPrice;
                result.UnitTH = itemModel.MasterPromotionItem.UnitTH;
                result.ItemType = PromotionItemType.Item;
                result.Id = itemModel.ID;
                result.Updated = itemModel.Updated;
                result.UpdatedBy = itemModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                result.TransferPromotionID = itemModel.TransferPromotionID;
                result.IsPurchasing = itemModel.MasterPromotionItem.IsPurchasing;

                var request = db.TransferPromotionRequestItems
                                .Include(o => o.TransferPromotionItem)
                                .Include(o => o.TransferPromotionRequest)
                                .ThenInclude(o => o.PromotionRequestPRStatus)
                                .Where(o => o.TransferPromotionItemID == itemModel.ID
                                            //&& (o.TransferPromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.Approve
                                            //   || o.TransferPromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.ApproveSomeUnit
                                            //   || o.TransferPromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.WaitApprove)
                                            && (
                                                !string.IsNullOrEmpty(o.TransferPromotionRequest.RequestNo)
                                                || !string.IsNullOrEmpty(o.DenyRemark)
                                            )
                                            && o.TransferPromotionRequest.IsDeleted == false
                                        //&& !o.TransferPromotionItem.MainTransferPromotionItemID.HasValue
                                        ).ToList();

                result.RequestedQuantity = request.Sum(o => o.Quantity);
                result.RemainingRequestQuantity = itemModel.Quantity - request.Sum(o => o.Quantity);
                result.RequestQuantity = 0;

                var masterReceiveDate = db.MasterTransferPromotionItems.Where(o => o.ID == itemModel.MasterTransferPromotionItemID).Select(o => o.ReceiveDays).FirstOrDefault();
                var EstimateRequestDate = request.Where(o => o.EstimateRequestDate.HasValue).OrderByDescending(o => o.EstimateRequestDate).Select(o => o.EstimateRequestDate).FirstOrDefault();
                result.ReceiveDate = EstimateRequestDate.HasValue ? EstimateRequestDate : DateTime.Now.AddDays(Convert.ToDouble(masterReceiveDate));

                if (request.Where(o => !String.IsNullOrEmpty(o.DenyRemark)).ToList().Any())
                {
                    result.DenyRemark = String.Join(Environment.NewLine, request.Select(o => o.DenyRemark ?? "").ToList());
                }
                else
                {
                    result.DenyRemark = "";
                }

                if (request.Where(o => !String.IsNullOrEmpty(o.TransferPromotionRequest?.RequestNo)).ToList().Any())
                {
                    result.RequestNo = String.Join(Environment.NewLine, request.OrderBy(o => (o.TransferPromotionRequest?.RequestNo ?? "")).Select(o => (o.TransferPromotionRequest?.RequestNo ?? "")).ToList());
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

                var subItems = db.TransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainTransferPromotionItemID == itemModel.ID
                            && o.TransferPromotionID == itemModel.TransferPromotionID).ToList();

                result.SubItems = new List<TransferPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new TransferPromotionItemDTO()
                        {
                            FromMasterTansferPromotionItemID = item.MasterTransferPromotionItemID,
                            FromQuotationTansferPromotionItemID = itemModel.QuotationTransferPromotionItemID,
                            NameTH = item.MasterPromotionItem.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.MasterPromotionItem.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = item.ID,
                            Updated = itemModel.Updated,
                            UpdatedBy = itemModel.UpdatedBy?.DisplayName,
                            IsSelected = true,
                            TransferPromotionID = itemModel.TransferPromotionID
                        };


                        var requestSub = db.TransferPromotionRequestItems
                                .Include(o => o.TransferPromotionRequest)
                                .ThenInclude(o => o.PromotionRequestPRStatus)
                                .Where(o => o.TransferPromotionItemID == item.ID
                                        && o.TransferPromotionRequest.IsDeleted == false
                                ).FirstOrDefault();

                        promoItem.RequestedQuantity = result.RequestedQuantity;
                        promoItem.RemainingRequestQuantity = result.RemainingRequestQuantity;
                        promoItem.RequestQuantity = result.RequestQuantity;
                        promoItem.ReceiveDate = result.ReceiveDate;
                        promoItem.DenyRemark = result.DenyRemark;

                        if (requestSub != null)
                        {
                            promoItem.RequestNo = requestSub.TransferPromotionRequest?.RequestNo;
                            promoItem.PRNo = requestSub.PRNo;

                            if (string.IsNullOrEmpty(promoItem.PRNo))
                            {
                                var prResult = db.PRRequestJobItemResults
                                        .Include(o => o.PRRequestJobItem)
                                        .Where(o => o.PRRequestJobItem.TransferPromotionRequestItemID == requestSub.ID).FirstOrDefault();

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
