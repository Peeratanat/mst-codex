using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("รูปชั้น")]
    [Table("FloorPlanImage", Schema = Schema.PROJECT)]
    public class FloorPlanImage : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
        [Description("ชื่อ")]
        [MaxLength(1000)]
        public string Name { get; set; }
        [Description("ชื่อไฟล์")]
        public string FileName { get; set; }

    }
}
