using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นโอน (ที่ไม่ต้องจัดซื้อ)")]
    [Table("TransferPromotionFreeItem", Schema = Schema.PROMOTION)]
    public class TransferPromotionFreeItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นโอน")]
        public Guid TransferPromotionID { get; set; }
        [ForeignKey("TransferPromotionID")]
        public TransferPromotion TransferPromotion { get; set; }
        [Description("ผูก Promotion Item")]
        public Guid? MasterTransferPromotionFreeItemID { get; set; }
        [ForeignKey("MasterTransferPromotionFreeItemID")]
        public MasterTransferPromotionFreeItem MasterTransferPromotionFreeItem { get; set; }
        [Description("ผูก Quotation Promotion Item")]
        public Guid? QuotationTransferPromotionFreeItemID { get; set; }
        [ForeignKey("QuotationTransferPromotionFreeItemID")]
        public QuotationTransferPromotionFreeItem QuotationTransferPromotionFreeItem { get; set; }

        [Description("จำนวน")]
        public int Quantity { get; set; }
    }
}
