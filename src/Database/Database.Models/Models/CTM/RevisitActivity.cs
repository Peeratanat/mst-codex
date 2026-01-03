using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.CTM
{
    [Description("ข้อมูลกิจกรรมของ Revisit")]
    [Table("RevisitActivity", Schema = Schema.CUSTOMER)]
    public class RevisitActivity : BaseEntity
    {
        [Description("รหัสของ Opportunity")]
        public Guid OpportunityID { get; set; }
        [ForeignKey("OpportunityID")]
        public Opportunity Opportunity { get; set; }

        [Description("ประเภทกิจกรรม (Revisit)")]
        public Guid? RevisitActivityTypeMasterCenterID { get; set; }
        [ForeignKey("RevisitActivityTypeMasterCenterID")]
        public MST.MasterCenter RevisitActivityType { get; set; }

        [Description("เวลาที่สะดวกให้ติดต่อกลับ")]
        public Guid? ConvenientTimeMasterCenterID { get; set; }
        [ForeignKey("ConvenientTimeMasterCenterID")]
        public MST.MasterCenter ConvenientTime { get; set; }

        [Description("วันที่ Revisit")]
        public DateTime? ActualDate { get; set; }
        [Description("วันที่นัดหมาย")]
        public DateTime? AppointmentDate { get; set; }
        [Description("รายละเอียดเพิ่มเติม")]
        [MaxLength(5000)]
        public string Description { get; set; }
        [Description("สำเร็จแล้วหรือไม่")]
        public bool IsCompleted { get; set; }

    }
}
