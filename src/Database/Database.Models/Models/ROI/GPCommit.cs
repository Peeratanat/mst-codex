using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ROI
{
    [Description("GPCommit")]
    [Table("GPCommit", Schema = Schema.ROI)]
    public class GPCommit : BaseEntityWithoutMigrate
    {
        public int? Y { get; set; }
        public int? Q { get; set; }
        public int? BG { get; set; }
        public string SubBG { get; set; }
        public Guid? ProjectID { get; set; }
        public decimal? PercentGPCommit { get; set; }
        public decimal? PriceCommit { get; set; }
    }
}
