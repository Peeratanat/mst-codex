using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นค่าธรรมเนียมบัตรในใบเสนอราคา")]
    [Table("QuotationSalePromotionCreditCardItem", Schema = Schema.PROMOTION)]
    public class QuotationSalePromotionCreditCardItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขายในใบเสนอราคา")]
        public Guid QuotationSalePromotionID { get; set; }
        [ForeignKey("QuotationSalePromotionID")]
        public QuotationSalePromotion QuotationSalePromotion { get; set; }

        [Description("ผูกสิ่ง Master")]
        public Guid? MasterSalePromotionCreditCardItemID { get; set; }
        [ForeignKey("MasterSalePromotionCreditCardItemID")]
        public MasterSalePromotionCreditCardItem MasterSalePromotionCreditCardItem { get; set; }

        [Description("ค่าธรรมเนียม (%)")]
        public double Fee { get; set; }
        [Description("จำนวน")]
        public int Quantity { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }
    }
}
