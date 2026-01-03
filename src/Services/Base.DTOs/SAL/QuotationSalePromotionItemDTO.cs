using System;
using System.Collections.Generic;
using System.Linq;
using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// รายการโปรขายในใบเสนอราคา
    /// </summary>
    public class QuotationSalePromotionItemDTO : BaseDTO
    {
        /// <summary>
        /// ID ของ MasterSalePromotionItem/MasterSalePromotionFreeItem/MasterBookingCreditCardItem
        /// </summary>
        public Guid? FromMasterSalePromotionItemID { get; set; }
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
        /// รายการที่ถูกเลือก
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<QuotationSalePromotionItemDTO> SubItems { get; set; }
        /// <summary>
        /// การจัดซื้อ
        /// </summary>
        public bool IsPurchasing { get; set; }

        /// <summary>
        /// masterTotalPrice
        /// </summary>
        public decimal MasterTotalprice { get; set; }

        public static QuotationSalePromotionItemDTO CreateFromModel(QuotationSalePromotionItem itemModel, QuotationSalePromotionFreeItem freeItemModel, QuotationSalePromotionCreditCardItem creditModel, DatabaseContext db)
        {
            QuotationSalePromotionItemDTO result = new QuotationSalePromotionItemDTO();
            if (itemModel != null)
            {
                result.FromMasterSalePromotionItemID = itemModel.MasterSalePromotionItemID;
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
                var subItems = db.QuotationSalePromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainQuotationSalePromotionID == itemModel.ID).ToList();
                result.SubItems = new List<QuotationSalePromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new QuotationSalePromotionItemDTO()
                        {
                            FromMasterSalePromotionItemID = item.MasterSalePromotionItemID,
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
                result.FromMasterSalePromotionItemID = freeItemModel.MasterSalePromotionFreeItemID;
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
                result.FromMasterSalePromotionItemID = creditModel.MasterSalePromotionCreditCardItemID;
                result.NameTH = creditModel.MasterSalePromotionCreditCardItem.NameTH;
                result.UnitTH = creditModel.MasterSalePromotionCreditCardItem.UnitTH;
                result.ItemType = PromotionItemType.CreditCard;
                result.Id = creditModel.ID;
                result.Updated = creditModel.Updated;
                result.PricePerUnit = (decimal)creditModel.Fee;
                result.MasterTotalprice = creditModel.MasterSalePromotionCreditCardItem.TotalPrice;
                result.Quantity = creditModel.Quantity;
                result.TotalPrice = creditModel.MasterSalePromotionCreditCardItem.TotalPrice* creditModel.Quantity;
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

        public static QuotationSalePromotionItemDTO CreateFromMasterModel(MasterSalePromotionItem itemModel, MasterSalePromotionFreeItem freeItemModel, MasterSalePromotionCreditCardItem creditModel, DatabaseContext db)
        {
            QuotationSalePromotionItemDTO result = new QuotationSalePromotionItemDTO();
            if (itemModel != null)
            {
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
                result.SubItems = new List<QuotationSalePromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new QuotationSalePromotionItemDTO()
                        {
                            FromMasterSalePromotionItemID = item.ID,
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
                result.MasterTotalprice = creditModel.TotalPrice;
                result.Quantity = creditModel.Quantity;
                result.TotalPrice = creditModel.TotalPrice;
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
