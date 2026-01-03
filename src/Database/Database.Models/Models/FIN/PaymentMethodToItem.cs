using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    //ช่องทางนี้ จ่ายค่าอะไรไปบ้าง เท่าไหร่?
    [Description("ผูกข้อมูลค่าใช้จ่ายที่รับเงินมาและช่องทางการชำระเงิน")]
    [Table("PaymentMethodToItem", Schema = Schema.FINANCE)]
    public class PaymentMethodToItem : BaseEntity
    {
        [Description("ผูกช่องทางการชำระ")]
        public Guid? PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }
        [Description("ผูกค่าใช้จ่ายที่ชำระ")]
        public Guid? PaymentItemID { get; set; }
        [ForeignKey("PaymentItemID")]
        public PaymentItem PaymentItem { get; set; }
        //เงินที่จ่าย
        [Description("จำนวนเงินที่ชำระ")]
        [Column(TypeName = "Money")]
        public decimal PayAmount { get; set; }

    }
}
