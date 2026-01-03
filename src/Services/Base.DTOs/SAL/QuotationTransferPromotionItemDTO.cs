using System;
using System.Collections.Generic;
using System.Linq;
using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// รายการโปรโอนในใบเสนอราคา
    /// </summary>
    public class QuotationTransferPromotionItemDTO : BaseDTO
    {
        /// <summary>
        /// ID ของ MasterTransferPromotionItem/MasterTransferPromotionFreeItem/MasterTransferCreditCardItem
        /// </summary>
        public Guid? FromMasterTansferPromotionItemID { get; set; }
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
        /// รายการที่ถูกเลือก
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<QuotationTransferPromotionItemDTO> SubItems { get; set; }
        /// <summary>
        /// การจัดซื้อ
        /// </summary>
        public bool IsPurchasing { get; set; }
        /// <summary>
        /// masterTotalPrice
        /// </summary>
        public decimal MasterTotalprice { get; set; }

        public static QuotationTransferPromotionItemDTO CreateFromModel(QuotationTransferPromotionItem itemModel, QuotationTransferPromotionFreeItem freeItemModel, QuotationTransferPromotionCreditCardItem creditModel, DatabaseContext db)
        {
            QuotationTransferPromotionItemDTO result = new QuotationTransferPromotionItemDTO();
            if (itemModel != null)
            {
                result.FromMasterTansferPromotionItemID = itemModel.MasterTransferPromotionItemID;
                result.NameTH = itemModel.MasterPromotionItem.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.MasterPromotionItem.PricePerUnit;
                result.TotalPrice = itemModel.MasterPromotionItem.TotalPrice * itemModel.Quantity;
                result.MasterTotalprice = itemModel.MasterPromotionItem.TotalPrice;
                result.UnitTH = itemModel.MasterPromotionItem.UnitTH;
                result.ItemType = PromotionItemType.Item;
                result.Id = itemModel.ID;
                result.Updated = itemModel.Updated;
                result.UpdatedBy = itemModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                result.IsPurchasing = itemModel.MasterPromotionItem.IsPurchasing;
                var subItems = db.QuotationTransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainQuotationTransferPromotionID == itemModel.ID).ToList();
                result.SubItems = new List<QuotationTransferPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new QuotationTransferPromotionItemDTO()
                        {
                            FromMasterTansferPromotionItemID = item.MasterTransferPromotionItemID,
                            NameTH = item.MasterPromotionItem.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.MasterPromotionItem.PricePerUnit,
                            TotalPrice = item.MasterPromotionItem.TotalPrice,
                            UnitTH = item.MasterPromotionItem.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = itemModel.ID,
                            Updated = itemModel.Updated,
                            UpdatedBy = itemModel.UpdatedBy?.DisplayName,
                            IsSelected = true,
                            IsPurchasing = item.MasterPromotionItem.IsPurchasing
                        };

                        result.SubItems.Add(promoItem);
                    }
                }

                return result;
            }
            else if (freeItemModel != null)
            {
                result.FromMasterTansferPromotionItemID = freeItemModel.MasterTransferPromotionFreeItemID;
                result.NameTH = freeItemModel.MasterPromotionFreeItem.NameTH;
                result.Quantity = freeItemModel.Quantity;
                result.UnitTH = freeItemModel.MasterPromotionFreeItem.UnitTH;
                result.ItemType = PromotionItemType.FreeItem;
                result.Id = freeItemModel.ID;
                result.Updated = freeItemModel.Updated;
                result.UpdatedBy = freeItemModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                result.IsPurchasing = false;
                return result;
            }
            else if (creditModel != null)
            {
                result.FromMasterTansferPromotionItemID = creditModel.MasterTransferPromotionCreditCardItemID;
                result.NameTH = creditModel.MasterTransferPromotionCreditCardItem.NameTH;
                result.Quantity = creditModel.MasterTransferPromotionCreditCardItem.Quantity;
                result.UnitTH = creditModel.MasterTransferPromotionCreditCardItem.UnitTH;
                result.ItemType = PromotionItemType.CreditCard;
                result.Id = creditModel.ID;
                result.Updated = creditModel.Updated;
                result.UpdatedBy = creditModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                result.IsPurchasing = false;
                return result;
            }
            else
            {
                return null;
            }
        }

        public static QuotationTransferPromotionItemDTO CreateFromMasterModel(MasterTransferPromotionItem itemModel, MasterTransferPromotionFreeItem freeItemModel, MasterTransferPromotionCreditCardItem creditModel, DatabaseContext db)
        {
            QuotationTransferPromotionItemDTO result = new QuotationTransferPromotionItemDTO();
            if (itemModel != null)
            {
                result.FromMasterTansferPromotionItemID = itemModel.ID;
                result.NameTH = itemModel.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.PricePerUnit;
                result.TotalPrice = itemModel.TotalPrice;
                result.MasterTotalprice = itemModel.TotalPrice;
                result.UnitTH = itemModel.UnitTH;
                result.ItemType = PromotionItemType.Item;
                result.IsSelected = false;
                result.IsPurchasing = itemModel.IsPurchasing;
                var subItems = db.MasterTransferPromotionItems.Where(o => o.MainPromotionItemID == itemModel.ID && o.ExpireDate >= DateTime.Now).ToList();
                result.SubItems = new List<QuotationTransferPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new QuotationTransferPromotionItemDTO()
                        {
                            FromMasterTansferPromotionItemID = item.ID,
                            NameTH = item.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.UnitTH,
                            ItemType = PromotionItemType.Item,
                            IsSelected = false,
                            IsPurchasing = item.IsPurchasing
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
    }
}
