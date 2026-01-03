using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ROI
{
    [Description("GPCal1")]
    [Table("GPCal1", Schema = Schema.ROI)]
    public class GPCal1 : BaseEntityWithoutMigrate
    {
        public Guid? GPVersionID { get; set; }
        public Guid? GPUnitID { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Budget_LD { get; set; }
        public decimal? Budget_LI { get; set; }
        public decimal? Budget_COC1 { get; set; }
        public decimal? Budget_UT { get; set; }
        public decimal? Budget_AC { get; set; }
        public decimal? Budget_CO01 { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? fixrate { get; set; }
        public int? AssetType { get; set; }
    }
}
