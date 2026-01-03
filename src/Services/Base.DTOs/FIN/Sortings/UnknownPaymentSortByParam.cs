namespace Base.DTOs.FIN
{
    public class UnknownPaymentSortByParam
    {
        public UnknownPaymentSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UnknownPaymentSortBy
    {
        UnknownPaymentCode,
        ReceiveDate,
        SAPCompanyID,
        BankAccountName,
        ProjectNo,
        UnitNo,
        UnknownAmount,
        ReverseAmount,
        BalanceAmount,
        IsPostUN,
        RVDocumentNo,
        UnknownPaymentStatusDetail,
        CreateName,
        ReverseName,
        UpdateDate,
        PaymentMethodType,
        Number,
        CreditCardType,
        Bank,
        IsWrongAccount,
        IsRequestFET,
        AgreementStatus, 
    }
}
