using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqDepositSP : BaseDbQueries
    {
        public Guid? ID { get; set; }
        public Guid? CompanyID { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? DepositID { get; set; }
        public string DepositNo { get; set; }
        public DateTime? DepositDate { get; set; }
        public string DepositRemark { get; set; }
        public Guid? BankAccountID { get; set; }
        public string BankAccountNo { get; set; }
        public Guid? BankID { get; set; }
        public string BankName { get; set; }
        public string PaymentItemName { get; set; }
        public string ReceiptTempNo { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public Guid? PaymentID { get; set; }
        public Guid? PaymentMethodID { get; set; }
        public bool? IsChequeConfirm { get; set; }
        public Guid? PaymentMethodTypeID { get; set; }
        public string PaymentMethodTypeName { get; set; }
        public string PaymentMethodTypeKey { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal Vat { get; set; }
        public bool IsPostPI { get; set; }
        public int? IsDeposit { get; set; }
        public bool? IsFeeConfirm { get; set; }
        public decimal FeeIncludingVat { get; set; }
        public string Number { get; set; }
    }
}
