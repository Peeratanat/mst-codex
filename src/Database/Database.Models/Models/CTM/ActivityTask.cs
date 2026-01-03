using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.CTM
{
    [Description("ข้อมูล Task ของ Activity")]
    [Table("ActivityTask", Schema = Schema.CUSTOMER)]
    public class ActivityTask : BaseEntity
    {
        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("ชื่อลูกค้า")]
        [MaxLength(1000)]
        public string ContactFirstName { get; set; }
        [Description("นามสกุลลูกค้า")]
        [MaxLength(1000)]
        public string ContactLastName { get; set; }
        [Description("เบอร์โทรศัพท์")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        [Description("วันที่ต้องทำ (Plan)")]
        public DateTime? DueDate { get; set; }
        [Description("สถานะ Overdue")]
        public Guid? ActivityTaskOverdueStatusMasterCenterID { get; set; }
        [ForeignKey("ActivityTaskOverdueStatusMasterCenterID")]
        public MST.MasterCenter OverdueStatus { get; set; }
        [Description("จำนวนวันที่ Overdue (บวก = ยังไม่ overdue, ลบ = overdue แล้ว, 0 = ถึงกำหนดแล้ว)")]
        public int OverdueDays { get; set; }

        [Description("จำนวนครั้ง เช่น Revisit ครั้งที่ 2 = 2")]
        public int RepeatCount { get; set; }

        [Description("สถานะของ Activity Task")]
        public Guid? ActivityTaskStatusMasterCenterID { get; set; }
        [ForeignKey("ActivityTaskStatusMasterCenterID")]
        public MST.MasterCenter Status { get; set; }
        [Description("หัวข้อของ Activity Task")]
        public Guid? ActivityTaskTopicMasterCenterID { get; set; }
        [ForeignKey("ActivityTaskTopicMasterCenterID")]
        public MST.MasterCenter Topic { get; set; }
        [Description("ชนิดของ Activity Task")]
        public Guid? ActivityTaskTypeMasterCenterID { get; set; }
        [ForeignKey("ActivityTaskTypeMasterCenterID")]
        public MST.MasterCenter Type { get; set; }

        [Description("ชื่อของประเภท Activity ของแต่ละชนิด (Lead, Walk, Revisit)")]
        [MaxLength(100)]
        public string ActivityTypeName { get; set; }

        [Description("ผู้ดูแล")]
        public Guid? OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public USR.User Owner { get; set; }

        [Description("Ref Lead Activity")]
        public Guid? LeadActivityID { get; set; }
        [ForeignKey("LeadActivityID")]
        public LeadActivity LeadActivity { get; set; }

        [Description("Ref Opportunity Activity")]
        public Guid? OpportunityActivityID { get; set; }
        [ForeignKey("OpportunityActivityID")]
        public OpportunityActivity OpportunityActivity { get; set; }

        [Description("Ref Revisit Activity")]
        public Guid? RevisitActivityID { get; set; }
        [ForeignKey("RevisitActivityID")]
        public RevisitActivity RevisitActivity { get; set; }
    }
}
