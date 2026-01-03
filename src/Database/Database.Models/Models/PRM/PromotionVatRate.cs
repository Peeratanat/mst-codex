using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Master Vat Rate")]
    [Table("PromotionVatRate", Schema = Schema.PROMOTION)]
    public class PromotionVatRate : BaseEntity
    {
        [Description("รหัส Vat (SLTAX)")]
        [MaxLength(100)]
        public string Code { get; set; }
        [Description("Vat Rate (%)")]
        public double VatRate { get; set; }
        [Description("สถานะ Active")]
        public bool IsActive { get; set; }
    }
}
