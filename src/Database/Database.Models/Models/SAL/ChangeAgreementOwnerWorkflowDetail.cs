using Database.Models.MST;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("รายละเอียดการอนุมัติเปลี่ยนแปลงชื่อผู้ทำสัญญา")]
    [Table("ChangeAgreementOwnerWorkflowDetail", Schema = Schema.SALE)]
    public class ChangeAgreementOwnerWorkflowDetail : BaseEntity
    {
        [Description("Workflow การเปลี่ยนผู้ทำสัญญา")]
        public Guid? ChangeAgreementOwnerWorkflowID { get; set; }
        [ForeignKey("ChangeAgreementOwnerWorkflowID")]
        public ChangeAgreementOwnerWorkflow ChangeAgreementOwnerWorkflow { get; set; }

        [Description("ผู้ทำสัญญา")]
        public Guid? AgreementOwnerID { get; set; }
        [ForeignKey("AgreementOwnerID")]
        public AgreementOwner AgreementOwner { get; set; }

        [Description("ประเภทผู้ทำสัญญาเข้าหรือออก ตอนตั้งเรื่องเปลี่ยนแปลงชื่อ (true = ผู้เพิ่มชื่อ,ผู้รับโอนสิทธิ์ / false = ผู้สละชื่อ,ผู้โอนสิทธิ์)")]
        public bool ChangeAgreementOwnerInType { get; set; }

        [Description("ลำดับ")]
        public int? Order { get; set; }
    }
}
