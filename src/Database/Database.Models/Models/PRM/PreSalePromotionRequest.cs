using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("ใบเบิกโปรโมชั่นก่อนขาย")]
    [Table("PreSalePromotionRequest", Schema = Schema.PROMOTION)]
    public class PreSalePromotionRequest : BaseEntity
    {
        [Description("เลขที่เบิก")]
        [MaxLength(100)]
        public string RequestNo { get; set; }
        [Description("โครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
        [Description("วันที่เบิก")]
        public DateTime? RequestDate { get; set; }
        [Description("วันที่อนุมัติ (PR ทั้งหมด)")]
        public DateTime? PRCompletedDate { get; set; }
        [Description("สถานะอนุมัติ (PR ทั้งหมด)")]
        public Guid? PromotionRequestPRStatusMasterCenterID { get; set; }
        [ForeignKey("PromotionRequestPRStatusMasterCenterID")]
        public MST.MasterCenter PromotionRequestPRStatus { get; set; }

        public List<PreSalePromotionRequestUnit> RequestUnits { get; set; }

        [Description("โปรโมชั่นก่อนขาย")]
        public Guid? MasterPreSalePromotionID { get; set; }
        [ForeignKey("MasterPreSalePromotionID")]
        public PRM.MasterPreSalePromotion MasterPreSalePromotion { get; set; }


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
