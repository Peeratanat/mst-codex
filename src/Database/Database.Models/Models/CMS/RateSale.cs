using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("ข้อมูล Rate % คอมมิสชั่นขาย")]
    [Table("RateSale", Schema = Schema.COMMISSION)]
    public class RateSale : BaseEntity
    {
        [Description("BG")]
        public string BGNo { get; set; }

        [Description("ลำดับแสดงผล")]
        public int Sequence { get; set; }

        [Description("Rate %")]
        public double Rate { get; set; }

    }
}
