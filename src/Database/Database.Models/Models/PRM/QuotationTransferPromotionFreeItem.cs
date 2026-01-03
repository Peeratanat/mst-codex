using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นโอนในใบเสนอราคา")]
    [Table("QuotationTransferPromotionFreeItem", Schema = Schema.PROMOTION)]
    public class QuotationTransferPromotionFreeItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นโอนในใบเสนอราคา")]
        public Guid QuotationTransferPromotionID { get; set; }
        [ForeignKey("QuotationTransferPromotionID")]
        public QuotationTransferPromotion QuotationTransferPromotion { get; set; }

        [Description("ผูกสิ่งของ (ที่ไม่ต้องจัดซื้อ)")]
        public Guid? MasterTransferPromotionFreeItemID { get; set; }
        [ForeignKey("MasterTransferPromotionFreeItemID")]
        public MasterTransferPromotionFreeItem MasterPromotionFreeItem { get; set; }


        [Description("จำนวน")]
        public int Quantity { get; set; }

    }
}
