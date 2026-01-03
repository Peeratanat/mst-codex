using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลแท็ค")]
    [Table("UnitOtherUnitInfoTag", Schema = Schema.PROJECT)]
    public class UnitOtherUnitInfoTag : BaseEntity
    {
        [Description("ชื่อแท็ค")]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
