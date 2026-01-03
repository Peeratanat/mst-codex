using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นขาย (ที่ไม่ต้องจัดซื้อ)")]
    [Table("SalePromotionFreeItem", Schema = Schema.PROMOTION)]
    public class SalePromotionFreeItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขาย")]
        public Guid SalePromotionID { get; set; }
        [ForeignKey("SalePromotionID")]
        public SalePromotion SalePromotion { get; set; }
        [Description("ผูก Promotion Item")]
        public Guid? MasterSalePromotionFreeItemID { get; set; }
        [ForeignKey("MasterSalePromotionFreeItemID")]
        public MasterSalePromotionFreeItem MasterSalePromotionFreeItem { get; set; }
        [Description("ผูก Quotation Promotion Item")]
        public Guid? QuotationSalePromotionFreeItemID { get; set; }
        [ForeignKey("QuotationSalePromotionFreeItemID")]
        public QuotationSalePromotionFreeItem QuotationSalePromotionFreeItem { get; set; }

        [Description("จำนวน")]
        public int Quantity { get; set; }
    }
}
