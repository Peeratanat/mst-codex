using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นโอนในใบเสนอราคา")]
    [Table("QuotationTransferPromotionItem", Schema = Schema.PROMOTION)]
    public class QuotationTransferPromotionItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นโอนในใบเสนอราคา")]
        public Guid QuotationTransferPromotionID { get; set; }
        [ForeignKey("QuotationTransferPromotionID")]
        public QuotationTransferPromotion QuotationTransferPromotion { get; set; }

        [Description("ผูกสิ่งของ")]
        public Guid? MasterTransferPromotionItemID { get; set; }
        [ForeignKey("MasterTransferPromotionItemID")]
        public MasterTransferPromotionItem MasterPromotionItem { get; set; }


        [Description("จำนวน")]
        public int Quantity { get; set; }

        [Description("ID ของ Promotion หลัก (กรณี Item นี้เป็น Promotion ย่อย)")]
        public Guid? MainQuotationTransferPromotionID { get; set; }

    }
}
