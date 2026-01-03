using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ROI
{
    [Description("GPFix")]
    [Table("GPFix", Schema = Schema.ROI)]
    public class GPFix : BaseEntityWithoutMigrate
    {
        public int? MenuId { get; set; }
        public int? Y { get; set; }
        public int? Q { get; set; }
        public int? M { get; set; }
        public int? BG { get; set; }
        public string SubBG { get; set; }
        public decimal? FixValue { get; set; }
    }
}
