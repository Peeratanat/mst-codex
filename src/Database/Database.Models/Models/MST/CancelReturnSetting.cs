using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("ตั้งค่าการยกเลิกคืนเงิน")]
    [Table("CancelReturnSetting", Schema = Schema.MASTER)]
    public class CancelReturnSetting : BaseEntity
    {
        [Description("Chief อนุมัติเมื่อคืนเงินน้อยกว่า (%)")]
        public double ChiefReturnLessThanPercent { get; set; }

        [Description("หักค่าดำเนินการ (บาท)")]
        [Column(TypeName = "Money")]
        public decimal HandlingFee { get; set; }

    }
}
