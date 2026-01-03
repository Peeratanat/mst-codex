using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูล Budget Min Price")]
    [Table("BudgetMinPrice", Schema = Schema.PROJECT)]
    public class BudgetMinPrice : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("ประเภท Budget")]
        public Guid? BudgetMinPriceTypeMasterCenterID { get; set; }
        [ForeignKey("BudgetMinPriceTypeMasterCenterID")]
        public MST.MasterCenter BudgetMinPriceType { get; set; }
        [Description("ปี")]
        public int Year { get; set; }
        [Description("ควอเตอร์")]
        public int Quarter { get; set; }
        [Description("Total Amount")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }
        [Description("Budget ที่ใช้ไป")]
        [Column(TypeName = "Money")]
        public decimal UsedAmount { get; set; }
        [Description("Unit Amount")]
        [Column(TypeName = "Money")]
        public decimal UnitAmount { get; set; }
        [Description("วันที่ Active")]
        public DateTime ActiveDate { get; set; }
    }
}
