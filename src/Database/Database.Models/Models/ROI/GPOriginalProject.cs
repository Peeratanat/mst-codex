using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ROI
{
    [Description("GPOriginalProject")]
    [Table("GPOriginalProject", Schema = Schema.ROI)]
    public class GPOriginalProject : BaseEntityWithoutMigrate
    {
        public Guid? SyncID { get; set; }
        public DateTime? SyncDate { get; set; }
        public Guid? ProjectID { get; set; }
        public int? Y { get; set; }
        public int? Q { get; set; }
        public int? M { get; set; }
        public decimal? Ori_COGS_LD { get; set; }
        public decimal? Ori_COGS_LI { get; set; }
        public decimal? Ori_COGS_CO { get; set; }
        public decimal? Ori_WIP_COC1 { get; set; }
        public decimal? Ori_Budget_CO01 { get; set; }
        public decimal? Ori_Budget_COP1 { get; set; }
        public decimal? Ori_COGS_UT { get; set; }
        public decimal? Ori_COGS_AC { get; set; }
        public decimal? Ori_NetPrice { get; set; }
        public bool? Transfer { get; set; }
    }
}
