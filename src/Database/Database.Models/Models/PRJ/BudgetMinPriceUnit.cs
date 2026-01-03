using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRJ
{
    [Description("ข้อมูล Budget Min Price ราย Unit")]
    [Table("BudgetMinPriceUnit", Schema = Schema.PROJECT)]
    public class BudgetMinPriceUnit : BaseEntity
    {
        [Description("Budget Min Price ปี และ ควอเตอร์ (Type=Quarterly)")]
        public Guid BudgetMinPriceID { get; set; }
        [ForeignKey("BudgetMinPriceID")]
        public PRJ.BudgetMinPrice BudgetMinPrice { get; set; }

        [Description("แปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("Min Price")]
        [Column(TypeName = "Money")]
        public decimal? Amount { get; set; }
    }
}
