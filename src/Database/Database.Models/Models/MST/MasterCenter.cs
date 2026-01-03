using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Database.Models.MST
{
    [Description("ข้อมูลพื้นฐานทั่วไป")]
    [Table("MasterCenter", Schema = Schema.MASTER)]
    public class MasterCenter : BaseEntity
    {
        [Description("กลุ่มของข้อมูลพื้นฐานทั่วไป")]
        [MaxLength(50)]
        public string MasterCenterGroupKey { get; set; }
        [ForeignKey("MasterCenterGroupKey")]
        public MasterCenterGroup MasterCenterGroup { get; set; }

        [Description("ลำดับ")]
        public int Order { get; set; }

        [Description("ชื่อ")]
        [MaxLength(1000)]
        public string Name { get; set; }

        [Description("ชื่อ (English)")]
        [MaxLength(1000)]
        public string NameEN { get; set; }

        [Description("รหัส")]
        [MaxLength(50)]
        public string Key { get; set; }

        [Description("Active อยู่หรือไม่")]
        public bool IsActive { get; set; }
    }
}
