using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRJ
{
    [Description("เก็บข้อมูล lock ชั้น/หรือห้อง")]
    [Table("UnitControlLock", Schema = Schema.PROJECT)]
    public class UnitControlLock : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("รหัสแปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }

        [Description("ชั้น")]
        public Guid? FloorID { get; set; }
        [ForeignKey("FloorID")]
        public Floor Floor { get; set; }

        [Description("EffectiveDate")]
        public DateTime? EffectiveDate { get; set; }

        [Description("ExpiredDate")]
        public DateTime? ExpiredDate { get; set; }

        [Description("Remark")]
        [MaxLength(5000)]
        public string Remark { get; set; }
    }
}

