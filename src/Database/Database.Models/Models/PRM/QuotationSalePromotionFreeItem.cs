using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นขายในใบเสนอราคา")]
    [Table("QuotationSalePromotionFreeItem", Schema = Schema.PROMOTION)]
    public class QuotationSalePromotionFreeItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขายในใบเสนอราคา")]
        public Guid QuotationSalePromotionID { get; set; }
        [ForeignKey("QuotationSalePromotionID")]
        public QuotationSalePromotion QuotationSalePromotion { get; set; }

        [Description("ผูกสิ่งของ (ที่ไม่ต้องจัดซื้อ)")]
        public Guid? MasterSalePromotionFreeItemID { get; set; }
        [ForeignKey("MasterSalePromotionFreeItemID")]
        public MasterSalePromotionFreeItem MasterPromotionFreeItem { get; set; }


        [Description("จำนวน")]
        public int Quantity { get; set; }

    }
}
