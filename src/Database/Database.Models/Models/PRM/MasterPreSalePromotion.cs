using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("Master โปรโมชั่นก่อนขาย")]
    [Table("MasterPreSalePromotion", Schema = Schema.PROMOTION)]
    public class MasterPreSalePromotion : BaseEntity
    {
        [Description("รหัสโปรโมชั่น")]
        [MaxLength(100)]
        public string PromotionNo { get; set; }
        [Description("ชื่อโปรโมชั่น")]
        public string Name { get; set; }

        [Description("ผูกโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("รหัสบริษัท")]
        [MaxLength(100)]
        public string CompanyCode { get; set; }
        [Description("Plant")]
        [MaxLength(100)]
        public string Plant { get; set; }

        [Description("สถานะ Active")]
        public Guid? PromotionStatusMasterCenterID { get; set; }
        [ForeignKey("PromotionStatusMasterCenterID")]
        public MST.MasterCenter PromotionStatus { get; set; }

        [Description("สถานะการนำไปใช้งาน")]
        public bool IsUsed { get; set; }
        [Description("วันที่ถูกนำไปใช้งาน")]
        public DateTime? UsedDate { get; set; }
        [Description("สถานะ Send PR")]
        public bool IsSendPR { get; set; }

        [Description("อนุมัติแล้ว")]
        public bool IsApproved { get; set; }
        [Description("วันที่อนุมัติ")]
        public DateTime? ApprovedDate { get; set; }

        public List<MasterPreSalePromotionItem> Items { get; set; }

        [Description("IsActive")]
        public bool IsActive { get; set; }
    }
}
