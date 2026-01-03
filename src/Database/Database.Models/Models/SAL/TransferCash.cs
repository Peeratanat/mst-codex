using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("รับเงินสดค่าโอนกรรมสิทธิ์")]
    [Table("TransferCash", Schema = Schema.SALE)]
    public class TransferCash : BaseEntity
    {
        [Description("โอนกรรมสิทธิ์")]
        public Guid TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }

        [Description("จ่ายให้กับ")]
        public Guid? CashPayToMasterCenterID { get; set; }
        [ForeignKey("CashPayToMasterCenterID")]
        public MST.MasterCenter CashPayTo { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Description("จ่ายให้กับนิติ")]
        public Guid? PayToLegalEntityID { get; set; }
        [ForeignKey("PayToLegalEntityID")]
        public MST.LegalEntity PayToLegalEntity { get; set; }
    }
}
