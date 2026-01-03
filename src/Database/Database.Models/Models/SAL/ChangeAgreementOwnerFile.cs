using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("แนบไฟล์การอนุมัติเปลี่ยนแปลงชื่อผู้ทำสัญญา")]
    [Table("ChangeAgreementOwnerFile", Schema = Schema.SALE)]
    public class ChangeAgreementOwnerFile : BaseEntity
    {
        public Guid ChangeAgreementOwnerWorkflowID { get; set; }
        [ForeignKey("ChangeAgreementOwnerWorkflowID")]
        public ChangeAgreementOwnerWorkflow ChangeAgreementOwnerWorkflow { get; set; }

        [Description("ชื่อ")]
        [MaxLength(1000)]
        public string Name { get; set; }
        [Description("ไฟล์แนบ")]
        [MaxLength(1000)]
        public string File { get; set; }
    }
}
