using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("ไฟล์แนบการอนุมัติเปลี่ยนแปลงโปรโมชั่น")]
    [Table("ChangePromotionWorkflowFile", Schema = Schema.PROMOTION)]
    public class ChangePromotionWorkflowFile : BaseEntity
    {
        public Guid ChangePromotionWorkflowID { get; set; }
        [ForeignKey("ChangePromotionWorkflowID")]
        public ChangePromotionWorkflow ChangePromotionWorkflow { get; set; }

        [Description("ชื่อ")]
        [MaxLength(1000)]
        public string Name { get; set; }
        [Description("ไฟล์แนบ")]
        [MaxLength(1000)]
        public string File { get; set; }
    }
}
