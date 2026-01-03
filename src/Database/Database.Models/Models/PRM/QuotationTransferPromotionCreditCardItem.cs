using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นค่าธรรมเนียมบัตรในใบเสนอราคา")]
    [Table("QuotationTransferPromotionCreditCardItem", Schema = Schema.PROMOTION)]
    public class QuotationTransferPromotionCreditCardItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นโอนในใบเสนอราคา")]
        public Guid QuotationTransferPromotionID { get; set; }
        [ForeignKey("QuotationTransferPromotionID")]
        public QuotationTransferPromotion QuotationTransferPromotion { get; set; }

        [Description("ผูกสิ่งของ Master")]
        public Guid? MasterTransferPromotionCreditCardItemID { get; set; }
        [ForeignKey("MasterTransferPromotionCreditCardItemID")]
        public MasterTransferPromotionCreditCardItem MasterTransferPromotionCreditCardItem { get; set; }

        [Description("ค่าธรรมเนียม (%)")]
        public double Fee { get; set; }
        [Description("จำนวน")]
        public int Quantity { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }
    }
}
