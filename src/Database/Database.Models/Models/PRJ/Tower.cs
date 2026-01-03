using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลอาคาร")]
    [Table("Tower", Schema = Schema.PROJECT)]
    public class Tower : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
        [Description("รหัสอาคาร")]
        [MaxLength(50)]
        public string TowerCode { get; set; }
        [Description("อาคารเลขที่ (ภาษาไทย)")]
        [MaxLength(100)]
        public string TowerNoTH { get; set; }
        [Description("อาคารเลขที่ (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string TowerNoEN { get; set; }
        [Description("ทะเบียนอาคารชุดเลขที่")]
        [MaxLength(50)]
        public string CondominiumNo { get; set; }
        [Description("ชื่ออาคารชุด")]
        [MaxLength(100)]
        public string CondominiumName { get; set; }
        [Description("คำอธิบาย")]
        [MaxLength(1000)]
        public string TowerDescription { get; set; }

    }
}
