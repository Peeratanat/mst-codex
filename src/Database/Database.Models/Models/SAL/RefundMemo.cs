using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("Memo คืนเงินลูกค้า")]
    [Table("RefundMemo", Schema = Schema.SALE)]
    public class RefundMemo : BaseEntity
    {
        [Description("โอนกรรมสิทธิ์")]
        public Guid TransferID { get; set; }
        [ForeignKey("TransferID")]
        public Transfer Transfer { get; set; }

        //[Description("ชื่อนิติบุคคล")]
        //public Guid? LegalEntityID { get; set; }
        //[ForeignKey("LegalEntityID")]
        //public MST.LegalEntity LegalEntity { get; set; }

        [Description("ธนาคารนิติบุคคล")]
        public Guid? LegalEntityBankID { get; set; }
        [ForeignKey("LegalEntityBankID")]
        public MST.Bank LegalEntityBank { get; set; }

        [Description("สาขาธนาคารนิติบุคคล")]
        public string LegalEntityBankBranch { get; set; }
        //[ForeignKey("LegalEntityBankBranchID")]
        //public MST.BankBranch LegalEntityBankBranch { get; set; }

        [Description("เลขที่บัญชีธนาคารนิติบุคคล")]
        public string LegalEntityBankAccount { get; set; }


        [Description("ธนาคารลูกค้า")]
        public Guid? CustomerBankID { get; set; }
        [ForeignKey("CustomerBankID")]
        public MST.Bank CustomerBank { get; set; }

        [Description("สาขาธนาคารลูกค้า")]
        public string CustomerBankBranch { get; set; }
        //[ForeignKey("CustomerBankBranchID")]
        //public MST.BankBranch CustomerBankBranch { get; set; }

        [Description("เลขที่บัญชีธนาคารลูกค้า")]
        public string CustomerBankAccount { get; set; }

        //[Description("รหัสลูกค้า")]
        //public string ContactNo { get; set; }

        //[Description("ชื่อลูกค้า")]
        //public string ContactName { get; set; }


        [Description("ประเภท Memo")]
        public Guid? RefundMemoTypeMasterCenterID { get; set; }
        [ForeignKey("RefundMemoTypeMasterCenterID")]
        public MST.MasterCenter RefundMemoType { get; set; }

        [Description("วันที่นัดชำระ (คืนลูกค้า)")]
        public DateTime? RefundDueDate { get; set; }
    }
}
