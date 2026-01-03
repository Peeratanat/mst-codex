using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ค่าทำเนียมโอน-ราคาประเมินที่ดินแนวราบ")]
    [Table("LowRiseFee", Schema = Schema.PROJECT)]
    public class LowRiseFee : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("รหัสแปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }

        [Description("ราคาประเมินที่ดิน")]
        [Column(TypeName = "Money")]
        public decimal? EstimatePriceArea { get; set; }

    }
}
