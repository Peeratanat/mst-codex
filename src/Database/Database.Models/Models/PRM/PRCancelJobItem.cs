using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Item งานขอยกเลิก PR จาก SAP")]
    [Table("PRCancelJobItem", Schema = Schema.PROMOTION)]
    public class PRCancelJobItem : BaseEntity
    {
        [Description("สถานะของการยกเลิก PR")]
        public Guid? PRCancelJobStatusMasterCenterID { get; set; }
        [ForeignKey("PRCancelJobStatusMasterCenterID")]
        public MST.MasterCenter PRCancelJobStatus { get; set; }

        [Description("รายการโปรก่อนขายที่ขอเบิก")]
        public Guid? PreSalePromotionRequestItemID { get; set; }
        [ForeignKey("PreSalePromotionRequestItemID")]
        public PreSalePromotionRequestItem PreSalePromotionRequestItem { get; set; }

        [Description("รายการโปรขายที่ขอเบิก")]
        public Guid? SalePromotionRequestItemID { get; set; }
        [ForeignKey("SalePromotionRequestItemID")]
        public SalePromotionRequestItem SalePromotionRequestItem { get; set; }

        [Description("รายการโปรโอนที่ขอเบิก")]
        public Guid? TransferPromotionRequestItemID { get; set; }
        [ForeignKey("TransferPromotionRequestItemID")]
        public TransferPromotionRequestItem TransferPromotionRequestItem { get; set; }

        [Description("งานยกเลิก PR")]
        public Guid? PRCancelJobID { get; set; }
        [ForeignKey("PRCancelJobID")]
        public PRCancelJob PRCancelJob { get; set; }

        [Description("ครั้งที่ Retry")]
        public int Retry { get; set; }

        [Description("User Name from Web")]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Description("เลขที่ PR (PREQ_NO)")]
        [MaxLength(100)]
        public string PRNo { get; set; }
        [Description("เลขที่สิ่งของ (PREQ_ITEM)")]
        [MaxLength(100)]
        public string ItemNo { get; set; }
    }
}
