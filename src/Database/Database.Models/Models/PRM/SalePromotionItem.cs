using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นขาย")]
    [Table("SalePromotionItem", Schema = Schema.PROMOTION)]
    public class SalePromotionItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขาย")]
        public Guid SalePromotionID { get; set; }
        [ForeignKey("SalePromotionID")]
        public SalePromotion SalePromotion { get; set; }
        [Description("ผูก Promotion Item")]
        public Guid? MasterSalePromotionItemID { get; set; }
        [ForeignKey("MasterSalePromotionItemID")]
        public MasterSalePromotionItem MasterPromotionItem { get; set; }
        [Description("ผูก Quotation Promotion Item")]
        public Guid? QuotationSalePromotionItemID { get; set; }
        [ForeignKey("QuotationSalePromotionItemID")]
        public QuotationSalePromotionItem QuotationSalePromotionItem { get; set; }

        [Description("ID ของ Promotion หลัก (กรณี Item นี้เป็น Promotion ย่อย)")]
        public Guid? MainSalePromotionItemID { get; set; }

        [Description("จำนวน")]
        public int Quantity { get; set; }
        [Description("ราคาต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal PricePerUnit { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }

        [Description("IsActive")]
        public bool IsActive { get; set; }
    }
}
