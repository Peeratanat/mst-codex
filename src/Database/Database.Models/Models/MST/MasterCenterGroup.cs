using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("กลุ่มของข้อมูลทั่วไป")]
    [Table("MasterCenterGroup", Schema = Schema.MASTER)]
    public class MasterCenterGroup : BaseEntityWithoutKey
    {
        [Description("รหัส")]
        [Key]
        [MaxLength(50)]
        public string Key { get; set; }
        [Description("ชื่อ")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Description("ใช้กับระบบเท่านั้น (ไม่เปิดให้ User แก้ไขได้)")]
        public bool IsSystemOnly { get; set; }

    }
}
