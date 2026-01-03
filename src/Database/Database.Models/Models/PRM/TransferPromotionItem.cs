using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นโอน")]
    [Table("TransferPromotionItem", Schema = Schema.PROMOTION)]
    public class TransferPromotionItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นโอน")]
        public Guid TransferPromotionID { get; set; }
        [ForeignKey("TransferPromotionID")]
        public TransferPromotion TransferPromotion { get; set; }
        [Description("ผูก Promotion Item")]
        public Guid? MasterTransferPromotionItemID { get; set; }
        [ForeignKey("MasterTransferPromotionItemID")]
        public MasterTransferPromotionItem MasterPromotionItem { get; set; }
        [Description("ผูก Quotation Promotion Item")]
        public Guid? QuotationTransferPromotionItemID { get; set; }
        [ForeignKey("QuotationTransferPromotionItemID")]
        public QuotationTransferPromotionItem QuotationTransferPromotionItem { get; set; }

        [Description("ID ของ Promotion หลัก (กรณี Item นี้เป็น Promotion ย่อย)")]
        public Guid? MainTransferPromotionItemID { get; set; }

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
