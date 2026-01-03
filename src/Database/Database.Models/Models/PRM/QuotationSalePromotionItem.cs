using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นขายในใบเสนอราคา")]
    [Table("QuotationSalePromotionItem", Schema = Schema.PROMOTION)]
    public class QuotationSalePromotionItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขายในใบเสนอราคา")]
        public Guid QuotationSalePromotionID { get; set; }
        [ForeignKey("QuotationSalePromotionID")]
        public QuotationSalePromotion QuotationSalePromotion { get; set; }

        [Description("ผูกสิ่งของ")]
        public Guid? MasterSalePromotionItemID { get; set; }
        [ForeignKey("MasterSalePromotionItemID")]
        public MasterSalePromotionItem MasterPromotionItem { get; set; }


        [Description("จำนวน")]
        public int Quantity { get; set; }

        [Description("ID ของ Promotion หลัก (กรณี Item นี้เป็น Promotion ย่อย)")]
        public Guid? MainQuotationSalePromotionID { get; set; }

    }
}
