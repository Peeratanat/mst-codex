using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลกิจกรรมของ Lead")]
    [Table("LeadActivity", Schema = Schema.CUSTOMER)]
    public class LeadActivity : BaseEntity
    {
        [Description("รหัสของ Lead")]
        public Guid LeadID { get; set; }
        [ForeignKey("LeadID")]
        public Lead Lead { get; set; }

        [Description("ประเภทกิจกรรม (Lead)")]
        public Guid? LeadActivityTypeMasterCenterID { get; set; }
        [ForeignKey("LeadActivityTypeMasterCenterID")]
        public MST.MasterCenter LeadActivityType { get; set; }

        [Description("วันที่ต้องทำ (Plan)")]
        public DateTime? DueDate { get; set; }
        [Description("วันที่ทำจริง")]
        public DateTime? ActualDate { get; set; }

        [Description("เวลาที่สะดวกให้ติดต่อกลับ")]
        public Guid? ConvenientTimeMasterCenterID { get; set; }
        [ForeignKey("ConvenientTimeMasterCenterID")]
        public MST.MasterCenter ConvenientTime { get; set; }

        [Description("วันที่นัดหมาย")]
        public DateTime? AppointmentDate { get; set; }
        [Description("รหัสของข้อมูลสถานะกิจกรรมของ Lead")]
        public Guid? StatusID { get; set; }
        [ForeignKey("StatusID")]
        public LeadActivityStatus LeadActivityStatus { get; set; }
        [Description("Due date ของ Status แบบ Follow Up")]
        public DateTime? StatusDueDate { get; set; }
        [Description("รายละเอียดเพิ่มเติม")]
        [MaxLength(5000)]
        public string Description { get; set; }
        [Description("สำเร็จแล้วหรือไม่")]
        public bool IsCompleted { get; set; }
        [Description("เป็น Activity ที่สร้างจากการ FollowUp โดยอัตโนมัติ")]
        public bool IsFollowUpActivity { get; set; }

        [Description("วันที่เวลาเริ่มต้นโทรหาลูกค้า")]
        public DateTime? StartCallTime { get; set; }
        [Description("วันที่เวลาสิ้นสุดโทรหาลูกค้า")]
        public DateTime? EndCallTime { get; set; }
    }
}
