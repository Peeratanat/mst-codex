using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("Master โปรโมชั่นส่งเสริมการโอน")]
    [Table("MasterTransferPromotion", Schema = Schema.PROMOTION)]
    public class MasterTransferPromotion : BaseEntity
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

        [Description("วันที่เริ่มต้น")]
        public DateTime? StartDate { get; set; }
        [Description("วันที่สิ้นสุด")]
        public DateTime? EndDate { get; set; }
        [Description("ส่วนลดวันโอน")]
        [Column(TypeName = "Money")]
        public decimal? TransferDiscount { get; set; }

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

        [Description("IsActive")]
        public bool IsActive { get; set; }
    }
}
