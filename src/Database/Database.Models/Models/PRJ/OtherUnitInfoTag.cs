using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ตารางเชื่อมUnitกับTag")]
    [Table("OtherUnitInfoTag", Schema = Schema.PROJECT)]
    public class OtherUnitInfoTag : BaseEntity
    {
        [Description("รหัสแท็ค")]
        public Guid TagID { get; set; }
        [ForeignKey("TagID")]
        public UnitOtherUnitInfoTag UnitTag { get; set; }

        [Description("รหัสแปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }
    }
}
