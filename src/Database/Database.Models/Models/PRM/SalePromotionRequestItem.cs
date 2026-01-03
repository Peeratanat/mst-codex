using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการเบิกโปรโมชั่นขาย")]
    [Table("SalePromotionRequestItem", Schema = Schema.PROMOTION)]
    public class SalePromotionRequestItem : BaseEntity
    {
        [Description("ผูกการเบิกโปรโมชั่นขาย")]
        public Guid? SalePromotionRequestID { get; set; }
        [ForeignKey("SalePromotionRequestID")]
        public SalePromotionRequest SalePromotionRequest { get; set; }
        [Description("ผูกสิ่งของโปรโมชั่นขาย")]
        public Guid? SalePromotionItemID { get; set; }
        [ForeignKey("SalePromotionItemID")]
        public SalePromotionItem SalePromotionItem { get; set; }
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
