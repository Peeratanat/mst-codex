using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("Min Price")]
    [Table("MinPrice", Schema = Schema.PROJECT)]
    public class MinPrice : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
        [Description("แปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }
        [Description("ราคาทุน")]
        [Column(TypeName = "Money")]
        public decimal? Cost { get; set; }
        [Description("ราคาขาย")]
        [Column(TypeName = "Money")]
        public decimal? SalePrice { get; set; }
        [Description("ชนิดราคาทุน")]
        [MaxLength(100)]
        public string CostType { get; set; }
        [Description("ROI Minprice")]
        [Column(TypeName = "Money")]
        public decimal? ROIMinprice { get; set; }
        [Description("Min อนุมัติ")]
        [Column(TypeName = "Money")]
        public decimal? ApprovedMinPrice { get; set; }
        [Description("Min Price Type")]
        public Guid? MinPriceTypeMasterCenterID { get; set; }
        [ForeignKey("MinPriceTypeMasterCenterID")]
        public MST.MasterCenter MinPriceType { get; set; }
        [Description("วันที่นำเข้า")]
        public DateTime? ActiveDate { get; set; }
        [Description("Doc Type")]
        public Guid? DocTypeMasterCenterID { get; set; }
        [ForeignKey("DocTypeMasterCenterID")]
        public MST.MasterCenter DocType { get; set; }

    }
}
