using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลสถานะกิจกรรมของ Lead")]
    [Table("LeadActivityStatus", Schema = Schema.CUSTOMER)]
    public class LeadActivityStatus : BaseEntity
    {
        [Description("รหัส")]
        public string Code { get; set; }
        [Description("ลำดับ")]
        public int Order { get; set; }
        [Description("รายละเอียดสถานะ")]
        [MaxLength(100)]
        public string Description { get; set; }
        [Description("สถานะของข้อมูล (Active/Inactive)")]
        public bool IsActive { get; set; }

        [Description("ประเภทของสถานะ (CorrectNumber = 0, IncorrectNumber = 1, CannotAppointment = 2)")]
        public Guid? LeadActivityStatusTypeMasterCenterID { get; set; }
        [ForeignKey("LeadActivityStatusTypeMasterCenterID")]
        public MST.MasterCenter LeadActivityStatusType { get; set; }

        [Description("สถานะการติดตาม (Disqualify = 0, FollowUp = 1)")]
        public Guid? LeadActivityFollowUpTypeMasterCenterID { get; set; }
        [ForeignKey("LeadActivityFollowUpTypeMasterCenterID")]
        public MST.MasterCenter LeadActivityFollowUpType { get; set; }

        public bool? isSHused { get; set; }
        public bool? isTHused { get; set; }
        public bool? isCDused { get; set; }

}
}
