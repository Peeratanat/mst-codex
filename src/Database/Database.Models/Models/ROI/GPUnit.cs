using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ROI
{
    [Description("GPUnit")]
    [Table("GPUnit", Schema = Schema.ROI)]
    public class GPUnit : BaseEntityWithoutMigrate
    {
        public Guid? GPVersionID { get; set; }
        public Guid? UnitID { get; set; }
        public string WBSBlock { get; set; }
        public string BlockNumber { get; set; }
        public decimal? Budget_LI { get; set; }
        public decimal? Budget_CO01 { get; set; }
        public decimal? Budget_CO01_Block { get; set; }
        public decimal? Budget_COC1 { get; set; }
        public decimal? Budget_CO_A1 { get; set; }
        public decimal? Budget_UT { get; set; }
        public decimal? Budget_AC { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? PercentGPNew { get; set; }
        public decimal? PriceCommit { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? Ori_COGS_LD { get; set; }
        public decimal? Ori_COGS_LI { get; set; }
        public decimal? Ori_COGS_CO { get; set; }
        public decimal? Ori_Budget_CO01 { get; set; }
        public decimal? Ori_WIP_COC1 { get; set; }
        public decimal? Ori_COGS_UT { get; set; }
        public decimal? Ori_COGS_AC { get; set; }
        public decimal? Ori_NetPrice { get; set; }
        public decimal? Ori_PercentGPCommit { get; set; }
        public decimal? Ori_MinPrice { get; set; }
    }
}
