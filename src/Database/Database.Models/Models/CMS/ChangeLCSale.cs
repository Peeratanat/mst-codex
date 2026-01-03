using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("ข้อมูลเปลี่ยนแปลงพนักงานขาย")]
    [Table("ChangeLCSale", Schema = Schema.COMMISSION)]
    public class ChangeLCSale : BaseEntity
    {
        [Description("วันที่ Active")]
        public DateTime? ActiveDate { get; set; }


        [Description("เลขที่สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("ประเภทพนักงานปิดการขาย(เดิม)")]
        public Guid? OldSaleOfficerTypeMasterCenterID { get; set; }
        [ForeignKey("OldSaleOfficerTypeMasterCenterID")]
        public MST.MasterCenter OldSaleOfficerType { get; set; }


        [Description("รหัส Agent(เดิม)")]
        public Guid? OldAgentID { get; set; }
        [ForeignKey("OldAgentID")]
        public MST.Agent OldAgent { get; set; }

        [Description("รหัสพนักงาน Agent(เดิม)")]
        public Guid? OldAgentEmployeeID { get; set; }
        [ForeignKey("OldAgentEmployeeID")]
        public MST.AgentEmployee OldAgentEmployee { get; set; }

        [Description("รหัส Sale(เดิม)")]
        public Guid? OldSaleUserID { get; set; }
        [ForeignKey("OldSaleUserID")]
        public USR.User OldSaleUser { get; set; }


        [Description("รหัส Sale ประจำโครงการ(เดิม)")]
        public Guid? OldProjectSaleUserID { get; set; }
        [ForeignKey("OldProjectSaleUserID")]
        public USR.User OldProjectSaleUser { get; set; }


        [Description("ประเภทพนักงานปิดการขาย(ใหม่)")]
        public Guid? NewSaleOfficerTypeMasterCenterID { get; set; }
        [ForeignKey("NewSaleOfficerTypeMasterCenterID")]
        public MST.MasterCenter NewSaleOfficerType { get; set; }


        [Description("รหัส Agent(ใหม่)")]
        public Guid? NewAgentID { get; set; }
        [ForeignKey("NewAgentID")]
        public MST.Agent NewAgent { get; set; }

        [Description("รหัสพนักงาน Agent(ใหม่)")]
        public Guid? NewAgentEmployeeID { get; set; }
        [ForeignKey("NewAgentEmployeeID")]
        public MST.AgentEmployee NewAgentEmployee { get; set; }


        [Description("รหัส Sale(ใหม่)")]
        public Guid? NewSaleUserID { get; set; }
        [ForeignKey("NewSaleUserID")]
        public USR.User NewSaleUser { get; set; }


        [Description("รหัส Sale ประจำโครงการ(ใหม่)")]
        public Guid? NewProjectSaleUserID { get; set; }
        [ForeignKey("NewProjectSaleUserID")]
        public USR.User NewProjectSaleUser { get; set; }


        [Description("หมายเหตุ")]
        public string Remark { get; set; }
    }
}
