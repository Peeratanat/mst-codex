using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลการตั้งค่านับการ Visit ของแต่ละโครงการ (ใช้โดยระบบ Visitor)")]
    [Table("ProjectVisitCounterSetting", Schema = Schema.CUSTOMER)]
    public class ProjectVisitCounterSetting : BaseEntity
    {
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
        public int ResetCounter { get; set; }
    }
}
