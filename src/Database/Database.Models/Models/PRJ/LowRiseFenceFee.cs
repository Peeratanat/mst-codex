using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ค่าทำเนียมโอน-ค่ารั้วแนวราบ")]
    [Table("LowRiseFenceFee", Schema = Schema.PROJECT)]
    public class LowRiseFenceFee : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("ที่ตั้งสำนักงานที่ดิน")]
        public Guid? LandOfficeID { get; set; }
        [ForeignKey("LandOfficeID")]
        public MST.LandOffice LandOffice { get; set; }

        [Description("ประเภทบ้าน")]
        public Guid? TypeOfRealEstateID { get; set; }
        [ForeignKey("TypeOfRealEstateID")]
        public MST.TypeOfRealEstate TypeOfRealEstate { get; set; }

        [Description("อัตรารั้วคอนกรีต")]
        public double? ConcreteRate { get; set; }
        [Description("อัตรารั้วเหล็ก")]
        public double? IronRate { get; set; }
        [Description("ราคารั้วคอนกรีต")]
        [Column(TypeName = "Money")]
        public decimal? ConcretePrice { get; set; }
        [Description("ราคารั้วเหล็ก")]
        [Column(TypeName = "Money")]
        public decimal? IronPrice { get; set; }
        [Description("ค่าเสื่อมราคาต่อปี")]
        [Column(TypeName = "Money")]
        public decimal? DepreciationPerYear { get; set; }
        [Description("คำนวนค่าเสื่อมรั้ว")]
        public bool IsCalculateDepreciation { get; set; }

    }
}
