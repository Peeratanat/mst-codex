using Database.Models.PRM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class BookingPreSalePromotionItemDTO : BaseDTO
    {
        /// <summary>
        /// ID ของ MasterPreSalePromotionItem
        /// </summary>
        public Guid? FromMasterPreSalePromotionItemID { get; set; }
        /// <summary>
        /// ID ของ PreSalePromotionRequestItem
        /// </summary>
        public Guid? FromPreSalePromotionRequestItemID { get; set; }
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
        /// จำนวนที่ส่งมอบ
        /// </summary>
        public int DeliveryQuantity { get; set; }
        /// <summary>
        /// จำนวนที่เบิก
        /// </summary>
        public int RequestQuantity { get; set; }
        /// <summary>
        /// เลขที่ SerialNo
        /// </summary>
        public string SerialNo { get; set; }

        public static BookingPreSalePromotionItemDTO CreateFromModel(PreSalePromotionItem model)
        {
            BookingPreSalePromotionItemDTO result = new BookingPreSalePromotionItemDTO();
            if (model != null)
            {
                result.FromMasterPreSalePromotionItemID = model.MasterPreSalePromotionItemID;
                result.FromPreSalePromotionRequestItemID = model.PreSalePromotionRequestItemID;
                result.NameTH = model.MasterPreSalePromotionItem?.NameTH;
                result.Quantity = model.Quantity;
                result.PricePerUnit = model.PricePerUnit;
                result.TotalPrice = model.TotalPrice;
                result.UnitTH = model.Unit;
                result.PRNo = model.PreSalePromotionRequestItem.PRNo;
                result.RequestQuantity = model.PreSalePromotionRequestItem.Quantity;
                result.Id = model.ID;
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
