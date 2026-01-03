using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("การอนุมัติ Sign Contract")]
    [Table("SignContractWorkflow", Schema = Schema.SALE)]
    public class SignContractWorkflow : BaseEntity
    {
        [Description("โครงการ")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("การทำรายการอนุมัติ Sign Contract")]
        public Guid? SignContractActionMasterCenterID { get; set; }
        [ForeignKey("SignContractActionMasterCenterID")]
        public MST.MasterCenter SignContractAction { get; set; }

        [Description("วัน-เวลาที่ทำรายการ")]
        public DateTime? ActionDate { get; set; }
        [Description("ตำแหน่งที่ต้องทำรายการ")]
        public Guid? ActionByRoleID { get; set; }
        [ForeignKey("ActionByRoleID")]
        public USR.Role ActionByRole { get; set; }
        [Description("ผู้ทำรายการ")]
        public Guid? ActionByUserID { get; set; }
        [ForeignKey("ActionByUserID")]
        public USR.User ActionBy { get; set; }

        [Description("หมายเหตุ")]
        public string Remark { get; set; }


    }
}
