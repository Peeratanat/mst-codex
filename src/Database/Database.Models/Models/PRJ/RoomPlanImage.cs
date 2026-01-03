using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("รูปห้อง")]
    [Table("RoomPlanImage", Schema = Schema.PROJECT)]
    public class RoomPlanImage : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
        [Description("ชื่อ")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Description("ชื่อไฟล์")]
        [MaxLength(1000)]
        public string FileName { get; set; }

    }
}
