using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นขายในใบเสนอราคา")]
    [Table("QuotationPromotionTemplate", Schema = Schema.PROMOTION)]
    public class QuotationPromotionTemplate : BaseEntityWithoutMigrate
    {
        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelId { get; set; }
        [ForeignKey("ModelId")]
        public PRJ.Model Model { get; set; }

        [Description("ชื่อแบบบ้าน")]
        [MaxLength(100)]
        public string ModelName { get; set; }

        [Description("ลำดับ")]
        public int? SeqNo { get; set; }

        [Description("ชื่อ Promotion ")]
        public string PromotionDescription { get; set; }

        [Description("จำนวน")]
        public int? Quantity { get; set; }

    }
}
