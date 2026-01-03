using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นขาย (ค่าธรรมเนียมบัตรเครดิต)")]
    [Table("SalePromotionCreditCardItem", Schema = Schema.PROMOTION)]
    public class SalePromotionCreditCardItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขาย")]
        public Guid SalePromotionID { get; set; }
        [ForeignKey("SalePromotionID")]
        public SalePromotion SalePromotion { get; set; }

        [Description("ผูก Promotion Item")]
        public Guid? MasterSalePromotionCreditCardItemID { get; set; }
        [ForeignKey("MasterSalePromotionCreditCardItemID")]
        public MasterSalePromotionCreditCardItem MasterSalePromotionCreditCardItem { get; set; }

        [Description("ผูก Quotation Promotion Item")]
        public Guid? QuotationSalePromotionCreditCardItemID { get; set; }
        [ForeignKey("QuotationSalePromotionCreditCardItemID")]
        public QuotationSalePromotionCreditCardItem QuotationSalePromotionCreditCardItem { get; set; }

        [Description("ค่าธรรมเนียม (%)")]
        public double Fee { get; set; }
        [Description("จำนวน")]
        public int Quantity { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }

        [Description("IsActive")]
        public bool IsActive { get; set; }
    }
}
