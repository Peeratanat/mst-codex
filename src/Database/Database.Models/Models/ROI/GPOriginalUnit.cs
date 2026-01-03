using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ROI
{
    [Description("GPOriginalUnit")]
    [Table("GPOriginalUnit", Schema = Schema.ROI)]
    public class GPOriginalUnit : BaseEntityWithoutMigrate
    {
        public Guid? SyncID { get; set; }
        public DateTime? SyncDate { get; set; }
        public Guid? OriginalProjectID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public int? Y { get; set; }
        public int? Q { get; set; }
        public int? M { get; set; }
        public string WBSBlock { get; set; }
        public string BlockNumber { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? COGS_LD { get; set; }
        public decimal? COGS_LI { get; set; }
        public decimal? COGS_CO { get; set; }
        public decimal? WIP_COC1 { get; set; }
        public decimal? Budget_CO01 { get; set; }
        public decimal? COGS_UT { get; set; }
        public decimal? COGS_AC { get; set; }
        public decimal? Promotion_CRM { get; set; }
        public decimal? Budget_COP1 { get; set; }
        public decimal? Assigned_1_2 { get; set; }
        public decimal? Assigned_1 { get; set; }
        public decimal? Cost { get; set; }
        public decimal? GrossProfit { get; set; }
        public decimal? PercentGP { get; set; }
        public decimal? Revenue_Inpro { get; set; }
        public decimal? Minprice_Inpro { get; set; }
        public decimal? COGSLD_Inpro { get; set; }
        public decimal? COGSLI_Inpro { get; set; }
        public decimal? COGSCO_Inpro { get; set; }
        public decimal? WIPCOC1_Inpro { get; set; }
        public decimal? BudgetCO01_Inpro { get; set; }
        public decimal? COGSUT_Inpro { get; set; }
        public decimal? COGSAC_Inpro { get; set; }
        public decimal? Cost_Inpro { get; set; }
        public decimal? GrossProfit_Inpro { get; set; }
        public decimal? PercentGP_Inpro { get; set; }
        public bool? Transfer { get; set; }
        public int? AssetType { get; set; }
    }
}
