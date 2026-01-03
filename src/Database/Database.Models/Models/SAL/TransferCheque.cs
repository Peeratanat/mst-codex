using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("เช็ครับเงินค่าโอนกรรมสิทธิ์")]
    [Table("TransferCheque", Schema = Schema.SALE)]
    public class TransferCheque : BaseEntity
    {
        [Description("โอนกรรมสิทธิ์")]
        public Guid TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }

        [Description("จ่ายให้กับ")]
        public Guid? ChequePayToMasterCenterID { get; set; }
        [ForeignKey("ChequePayToMasterCenterID")]
        public MST.MasterCenter ChequePayTo { get; set; }

        [Description("ธนาคาร")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("เลขที่เช็ค")]
        [MaxLength(100)]
        public string ChequeNo { get; set; }
        [Description("วันที่เช็ค")]
        public DateTime? PayDate { get; set; }

        [Description("สั่งจ่ายบริษัท")]
        public Guid? PayToCompanyID { get; set; }
        [ForeignKey("PayToCompanyID")]
        public MST.Company PayToCompany { get; set; }

        [Description("สั่งจ่ายผิดบริษัท")]
        public bool IsWrongCompany { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Description("สั่งจ่ายนิติ")]
        public Guid? PayToLegalEntityID { get; set; }
        [ForeignKey("PayToLegalEntityID")]
        public MST.LegalEntity PayToLegalEntity { get; set; }
    }
}
