using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Database.Models.SAL;

namespace Database.Models.PRM
{
    [Description("ใบเบิกโปรโมชั่นขาย")]
    [Table("SalePromotionRequest", Schema = Schema.PROMOTION)]
    public class SalePromotionRequest : BaseEntity
    {
        [Description("โปรขาย")]
        public Guid? SalePromotionID { get; set; }
        [ForeignKey("SalePromotionID")]
        public SalePromotion SalePromotion { get; set; }

        [Description("เลขที่เบิก")]
        [MaxLength(100)]
        public string RequestNo { get; set; }
        [Description("วันที่เบิก")]
        public DateTime? RequestDate { get; set; }

        [Description("วันที่อนุมัติ (PR ทั้งหมด)")]
        public DateTime? PRCompletedDate { get; set; }
        [Description("สถานะอนุมัติ (PR ทั้งหมด)")]
        public Guid? PromotionRequestPRStatusMasterCenterID { get; set; }
        [ForeignKey("PromotionRequestPRStatusMasterCenterID")]
        public MST.MasterCenter PromotionRequestPRStatus { get; set; }


        [Description("จำนวนครั้งที่พิมพ์")]
        public int? PrintCount { get; set; }
        [Description("วันที่พิมพ์")]
        public DateTime? PrintDate { get; set; }
        [Description("ผู้พิมพ์")]
        public Guid? PrintUserID { get; set; }
        [ForeignKey("PrintUserID")]
        public USR.User PrintUser { get; set; }
    }
}
