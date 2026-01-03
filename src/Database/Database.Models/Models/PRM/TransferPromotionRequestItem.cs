using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการเบิกโปรโมชั่นโอน")]
    [Table("TransferPromotionRequestItem", Schema = Schema.PROMOTION)]
    public class TransferPromotionRequestItem : BaseEntity
    {
        [Description("ผูกการเบิกโปรโมชั่นโอน")]
        public Guid? TransferPromotionRequestID { get; set; }
        [ForeignKey("TransferPromotionRequestID")]
        public TransferPromotionRequest TransferPromotionRequest { get; set; }
        [Description("ผูกสิ่งของโปรโมชั่นขาย")]
        public Guid? TransferPromotionItemID { get; set; }
        [ForeignKey("TransferPromotionItemID")]
        public TransferPromotionItem TransferPromotionItem { get; set; }
        [Description("เบิกแล้ว")]
        public int RequestQuantity { get; set; }
        [Description("คงเหลือเบิก")]
        public int RemainingRequestQuantity { get; set; }
        [Description("จำนวนที่เบิก")]
        public int Quantity { get; set; }
        [Description("วันที่คาดว่าจะได้รับ")]
        public DateTime? EstimateRequestDate { get; set; }
        [Description("เลขที่ PR")]
        [MaxLength(100)]
        public string PRNo { get; set; }
        [Description("หมายเหตุไม่เบิกโปร")]
        [MaxLength(5000)]
        public string DenyRemark { get; set; }

    }
}
