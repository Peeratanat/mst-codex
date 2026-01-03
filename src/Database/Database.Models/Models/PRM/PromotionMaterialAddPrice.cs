using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Master Promotion Material Add Price")]
    [Table("PromotionMaterialAddPrice", Schema = Schema.PROMOTION)]
    public class PromotionMaterialAddPrice : BaseEntity
    {
        public Guid PromotionMaterialGroupID { get; set; }
        [ForeignKey("PromotionMaterialGroupID")]
        public PromotionMaterialGroup PromotionMaterialGroup { get; set; }

        [Description("เปอร์เซ็นต์ที่ต้องการเพิ่ม สำหรับโครงการแนวราบ")]
        public double LowRisePercent { get; set; }
        [Description("เปอร์เซ็นต์ที่ต้องการเพิ่ม สำหรับโครงการแนวสูง")]
        public double HighRisePercent { get; set; }

        [Description("วันที่ Active")]
        public DateTime ActiveDate { get; set; }
    }
}
