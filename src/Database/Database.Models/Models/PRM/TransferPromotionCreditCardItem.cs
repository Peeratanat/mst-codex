using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นโอน (ค่าธรรมเนียมบัตรเครดิต)")]
    [Table("TransferPromotionCreditCardItem", Schema = Schema.PROMOTION)]
    public class TransferPromotionCreditCardItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นโอน")]
        public Guid TransferPromotionID { get; set; }
        [ForeignKey("TransferPromotionID")]
        public TransferPromotion TransferPromotion { get; set; }

        [Description("ผูก Promotion Item")]
        public Guid? MasterTransferPromotionCreditCardItemID { get; set; }
        [ForeignKey("MasterTransferPromotionCreditCardItemID")]
        public MasterTransferPromotionCreditCardItem MasterTransferPromotionCreditCardItem { get; set; }

        [Description("ผูก Quotation Promotion Item")]
        public Guid? QuotationTransferPromotionCreditCardItemID { get; set; }
        [ForeignKey("QuotationTransferPromotionCreditCardItemID")]
        public QuotationTransferPromotionCreditCardItem QuotationTransferPromotionCreditCardItem { get; set; }

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
