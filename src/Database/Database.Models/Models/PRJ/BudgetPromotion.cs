using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูล Budget Promotion")]
    [Table("BudgetPromotion", Schema = Schema.PROJECT)]
    public class BudgetPromotion : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("เลขที่แปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }

        [Description("ชนิดของ Budget Promotion (1=ขาย, 2=โอน)")]
        public Guid? BudgetPromotionTypeMasterCenterID { get; set; }
        [ForeignKey("BudgetPromotionTypeMasterCenterID")]
        public MST.MasterCenter BudgetPromotionType { get; set; }

        [Description("Budget โปรขาย หรือ โปรโอน")]
        [Column(TypeName = "Money")]
        public decimal? Budget { get; set; }

        [Description("วันที่นำเข้า")]
        public DateTime? ActiveDate { get; set; }

    }
}
