using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลชั้น")]
    [Table("Floor", Schema = Schema.PROJECT)]
    public class Floor : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [Description("รหัสอาคาร")]
        public Guid TowerID { get; set; }
        [ForeignKey("TowerID")]
        public Tower Tower { get; set; }
        [Description("ชื่อชั้น (ภาษาไทย)")]
        [MaxLength(100)]
        public string NameTH { get; set; }
        [Description("ชื่อชั้น (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string NameEN { get; set; }
        [Description("รายละเอียด")]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Description("ชื่อรูปไฟล์แนบ")]
        public string FileAttachment { get; set; }
        [Description("วันที่ Due โอน")]
        public DateTime? DueTransferDate { get; set; }

    }
}
