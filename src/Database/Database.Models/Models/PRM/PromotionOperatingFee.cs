using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("PromotionOperatingFee")]
    [Table("PromotionOperatingFee", Schema = Schema.PROMOTION)]
    public class PromotionOperatingFee : BaseEntityWithoutMigrate
    {
        [Description("PromotionMaterialCode")]
        [MaxLength(100)]
        public string PromotionMaterialCode { get; set; } 
        [MaxLength(2500)]
        public string Remark { get; set; }
        public bool? IsActived { get; set; }
    }
}
