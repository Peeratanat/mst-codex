using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbq_GetRemainInstallment_ApprovedDirectDebitCredit
    {
        public Guid? DirectCreditDebitApprovalFormID { get; set; }
        public string AccountNO { get; set; }
        
        public Guid? BookingID { get; set; }
        public string BookingNo { get; set; }
        public string DirectApprovalFormType { get; set; }
        public Guid? UnitPriceInstallmentID { get; set; }
        public int Period { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? ItemAmount { get; set; }
        public decimal? PayAmount { get; set; }
        public decimal? Remain { get; set; }
        public decimal? PaidAmount { get; set; }
        public string AgreementNo { get; set; }
        public string AgreementName { get; set; }
        public decimal? LastRemainDownTotalAmount { get; set; }
        public decimal? SumPaidAfterPaymentLetter { get; set; }
        public decimal? UnknowPaymentWaitingAmount { get; set; }
        public decimal? TotalRemainAmount { get; set; }
    }
}
