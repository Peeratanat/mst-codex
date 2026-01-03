using System;
namespace MST_Finacc.Params.Filters
{
    public class BankAccountFilter
    {
        public Guid? BankID { get; set; }
        public string BankBranchName { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountTypeKey { get; set; }
        public Guid? CompanyID { get; set; }
        public string GLAccountNo { get; set; }
        public bool? IsActive { get; set; }
        public bool? HasVat { get; set; }
        public string GLAccountTypeKey { get; set; }
        public string GLRefCode { get; set; }
        public string Name { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
        public string UpdatedBy { get; set; }

        public bool? IsTransferAccount { get; set; }
        public bool? IsDirectDebit { get; set; }
        public bool? IsDirectCredit { get; set; }
        public bool? IsDepositAccount { get; set; }
        public bool? IsPCard { get; set; }
        public bool? IsForeignTransfer { get; set; }
        public bool? IsQRCode { get; set; }
        public bool? IsBillPayment { get; set; }
    }
}
