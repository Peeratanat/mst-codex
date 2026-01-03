using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลกิจกรรมของ Opportunity")]
    [Table("OpportunityActivity", Schema = Schema.CUSTOMER)]
    public class OpportunityActivity : BaseEntity
    {
        [Description("รหัสของ Opportunity")]
        public Guid OpportunityID { get; set; }
        [ForeignKey("OpportunityID")]
        public Opportunity Opportunity { get; set; }

        [Description("ประเภทกิจกรรม (Walk)")]
        public Guid? OpportunityActivityTypeMasterCenterID { get; set; }
        [ForeignKey("OpportunityActivityTypeMasterCenterID")]
        public MST.MasterCenter OpportunityActivityType { get; set; }

        [Description("เวลาที่สะดวกให้ติดต่อกลับ")]
        public Guid? ConvenientTimeMasterCenterID { get; set; }
        [ForeignKey("ConvenientTimeMasterCenterID")]
        public MST.MasterCenter ConvenientTime { get; set; }

        [Description("วันที่ต้องทำ")]
        public DateTime? DueDate { get; set; }
        [Description("วันที่ทำจริง")]
        public DateTime? ActualDate { get; set; }
        [Description("วันที่นัดหมาย")]
        public DateTime? AppointmentDate { get; set; }
        [Description("รายละเอียดเพิ่มเติม")]
        [MaxLength(5000)]
        public string Description { get; set; }
        [Description("สำเร็จแล้วหรือไม่")]
        public bool IsCompleted { get; set; }


        [Description("วันที่เวลาเริ่มต้นโทรหาลูกค้า")]
        public DateTime? StartCallTime { get; set; }
        [Description("วันที่เวลาสิ้นสุดโทรหาลูกค้า")]
        public DateTime? EndCallTime { get; set; }
    }
}
