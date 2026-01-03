using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("รายการเงินที่มีการเกลี่ยใหม่จาก Table FIN.PaymentItem")]
    [Table("PaymentConvertItem", Schema = Schema.FINANCE)]
    public class PaymentConvertItem : BaseEntityWithoutMigrate
    {        
        [Description("ID เงินต้นทาง ที่ถูก Convert มาเพิ่ม")]
        public Guid OriginalPaymentItemID { get; set; }

        [Description("ID เงินปลายทาง ที่ถูก Convert ไป")]
        public Guid TerminalPaymentItemID { get; set; }

        //เงินที่ Convert
        [Description("เงินที่ถูก Convert มาเพิ่มจากต้นทาง")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

    }
}
