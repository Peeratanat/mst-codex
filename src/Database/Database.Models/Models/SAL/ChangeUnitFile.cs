using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("ไฟล์แนบการอนุมัติย้ายแปลง จอง หรือ สัญญา")]
    [Table("ChangeUnitFile", Schema = Schema.SALE)]
    public class ChangeUnitFile : BaseEntity
    {
        public Guid ChangeUnitWorkflowID { get; set; }
        [ForeignKey("ChangeUnitWorkflowID")]
        public ChangeUnitWorkflow ChangeUnitWorkflow { get; set; }

        [Description("ชื่อ")]
        [MaxLength(1000)]
        public string Name { get; set; }
        [Description("ไฟล์แนบ")]
        [MaxLength(1000)]
        public string File { get; set; }
    }
}
