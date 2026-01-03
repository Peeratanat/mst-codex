using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ค่าทำเนียมโอน-ราคาประเมินที่ดินแนวราบ")]
    [Table("HighRiseFee", Schema = Schema.PROJECT)]
    public class HighRiseFee : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
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

        [Description("ตึก")]
        public Guid? TowerID { get; set; }
        [ForeignKey("TowerID")]
        public Tower Tower { get; set; }

        [Description("คำนวณที่จอดรถตามพื้นที่")]
        public Guid? CalculateParkAreaMasterCenterID { get; set; }
        [ForeignKey("CalculateParkAreaMasterCenterID")]
        public MST.MasterCenter CalculateParkArea { get; set; }

        [Description("ราคาประเมินพื้นที่จอดรถ")]
        [Column(TypeName = "Money")]
        public decimal? EstimatePriceArea { get; set; }
        [Description("ราคาประเมินพื้นที่ใช้สอย")]
        [Column(TypeName = "Money")]
        public decimal? EstimatePriceUsageArea { get; set; }
        [Description("ราคาประเมินพื้นที่ใช้สอยระเบียง")]
        [Column(TypeName = "Money")]
        public decimal? EstimatePriceBalconyArea { get; set; }
        [Description("ราคาประเมินพื้นที่วางแอร์")]
        [Column(TypeName = "Money")]
        public decimal? EstimatePriceAirArea { get; set; }
        [Description("ราคาประเมินพื้นที่สระว่ายน้ำ")]
        [Column(TypeName = "Money")]
        public decimal? EstimatePricePoolArea { get; set; }

    }
}
