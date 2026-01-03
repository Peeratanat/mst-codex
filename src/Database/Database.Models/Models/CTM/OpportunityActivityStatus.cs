using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลสถานะกิจกรรมของ Opportunity (Walk)")]
    [Table("OpportunityActivityStatus", Schema = Schema.CUSTOMER)]
    public class OpportunityActivityStatus : BaseEntity
    {
        [Description("รหัส")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Description("รายละเอียดสถานะ")]
        [MaxLength(100)]
        public string Description { get; set; }
        [Description("ลำดับ")]
        public int Order { get; set; }
        [Description("สถานะของข้อมูล (Active/Inactive)")]
        public bool IsActive { get; set; }
        [Description("ชนิดของ Status (ActivityResult = 0, ActivityEnd = 1)")]
        public Guid? WalkActivityStatusTypeMasterCenterId { get; set; }
        [ForeignKey("WalkActivityStatusTypeMasterCenterId")]
        public MST.MasterCenter WalkActivityStatusType { get; set; }

    }
}
