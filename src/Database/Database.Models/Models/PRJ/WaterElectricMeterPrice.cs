using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลราคามิเตอร์น้ำ มิเตอร์ไฟในแบบบ้าน")]
    [Table("WaterElectricMeterPrice", Schema = Schema.PROJECT)]
    public class WaterElectricMeterPrice : BaseEntity
    {
        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public Model Model { get; set; }

        [Description("Version")]
        public int? Version { get; set; }

        [Description("ราคามิเตอร์น้ำ")]
        [Column(TypeName = "Money")]
        public decimal? WaterMeterPrice { get; set; }

        [Description("ราคามิเตอร์ไฟ")]
        [Column(TypeName = "Money")]
        public decimal? ElectricMeterPrice { get; set; }

        [Description("ขนาดมิเตอร์ไฟฟ้า (แอมป์)")]
        [MaxLength(100)]
        public string ElectricMeterSize { get; set; }

        [Description("ขนาดมิเตอร์น้ำ (นิ้ว)")]
        [MaxLength(100)]
        public string WaterMeterSize { get; set; }
    }
}
