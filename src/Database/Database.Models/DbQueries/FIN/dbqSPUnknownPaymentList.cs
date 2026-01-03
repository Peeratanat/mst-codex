using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqSPUnknownPaymentList : BaseDbQueries
    {
        public Guid? UnknownPaymentID { get; set; }
        public Guid? BookingID { get; set; }
        public Guid? CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string SAPCompanyID { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? BankAccountID { get; set; }
        public string BankAccountName { get; set; }
        public string UnknownPaymentCode { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? ReverseDate { get; set; }
        public decimal? UnknownAmount { get; set; }
        public decimal? ReverseAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string CreateName { get; set; }
        public string ReverseName { get; set; }
        public string RVDocumentNo { get; set; }
        public Guid? UnknownPaymentStatusID { get; set; }
        public string UnknownPaymentStatusKey { get; set; }
        public string UnknownPaymentStatusDetail { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsPostUN { get; set; }
        public string Remark { get; set; }
        public string CancelRemark { get; set; }
        public string SAPRemark { get; set; }
        public Guid? PaymentMethodTypeID { get; set; }
        public string PaymentMethodType { get; set; }
        public string Number { get; set; }
        public Guid? CreditCardTypeID { get; set; }
        public string CreditCardType { get; set; }
        public Guid? BankID { get; set; }
        public string Bank { get; set; }
        public bool? IsWrongAccount { get; set; }
        public Guid? BillPaymentDetailID { get; set; } 
        public DateTime? CreateDate { get; set; }
        public Guid? PaymentID { get; set; }
        public bool? IsRequestFET { get; set; }
        public string AgreementStatus { get; set; }
        public string AgreementStatusKey { get; set; }
        public bool? UnBookingID { get; set; }

        public string PostCANo { get; set; }
    }
}
