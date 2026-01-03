using Database.Models.USR;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("การรับเงิน")]
    [Table("CancelDepositPrebookHistory", Schema = Schema.FINANCE)]
    public class CancelDepositPrebookHistory : BaseEntityWithoutMigrate
    {
        [Description("ID อ้างอิง PaymentPrebook")]
        public Guid PaymentPrebookID { get; set; }
        [ForeignKey("PaymentPrebookID")]
        public PaymentPrebook PaymentPrebook { get; set; }

        [Description("ID อ้างอิง PaymentPrebook")]
        public Guid PaymentMethodPrebookID { get; set; }
        [ForeignKey("PaymentMethodPrebookID")]
        public PaymentMethodPrebook PaymentMethodPrebook { get; set; }
        [Description("เลขที่นำฝากที่ถูกยกเลิก")]
        [MaxLength(100)]
        public string DepositNo { get; set; }
        [Description("วันที่นำมาฝากที่ถูกยกเลิก")]
        public DateTime DepositDate { get; set; }
        [Description("บันทึกข้อความ")]
        [MaxLength(5000)]
        public string Remark { get; set; }


    }
}
