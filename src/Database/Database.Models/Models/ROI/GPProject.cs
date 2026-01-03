using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ROI
{
    [Description("GPProject")]
    [Table("GPProject", Schema = Schema.ROI)]
    public class GPProject : BaseEntityWithoutMigrate
    {
        public Guid? GPVersionID { get; set; }
        public decimal? BudgetLI { get; set; }
        public decimal? BudgetCO01 { get; set; }
        public decimal? BudgetCOC1 { get; set; }
        public decimal? BudgetCOA1 { get; set; }
        public decimal? BudgetUT { get; set; }
        public decimal? BudgetAC { get; set; }
        public decimal? PercentGPCommit { get; set; }
        public decimal? PriceCommit { get; set; }
        public decimal? OriCOGSLD { get; set; }
        public decimal? OriCOGSLI { get; set; }
        public decimal? OriCOGSCO { get; set; }
        public decimal? OriWIPCOC1 { get; set; }
        public decimal? OriBudgetCO01 { get; set; }
        public decimal? OriBudgetCOP1 { get; set; }
        public decimal? OriCOGSUT { get; set; }
        public decimal? OriCOGSAC { get; set; }
        public decimal? OriNetPrice { get; set; }
        public decimal? NetPrice { get; set; }
    }
}
