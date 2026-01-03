using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ROI
{
    [Description("GPCal2")]
    [Table("GPCal2", Schema = Schema.ROI)]
    public class GPCal2 : BaseEntityWithoutMigrate
    {
        public Guid? GPVersionID { get; set; }
        public Guid? GPUnitID { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Budget_LD { get; set; }
        public decimal? Budget_LI { get; set; }
        public decimal? Budget_CO { get; set; }
        public decimal? Budget_CO01 { get; set; }
        public decimal? Budget_COC1 { get; set; }
        public decimal? Budget_UT { get; set; }
        public decimal? Budget_AC { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? GrossProfit_New { get; set; }
        public decimal? PercentGP_New { get; set; }
        public int? AssetType { get; set; }
    }
}
