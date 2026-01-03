using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class FeeCreditDebitCardSortByParam
    {
        public FeeCreditDebitCardSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum FeeCreditDebitCardSortBy
    {
        Project,
        ReceiveDate,
        EDC,
        Bank,
        CreditDebitCardType,
        CreditCardPaymentType,
        CreditDebitNo,
        ReceiveNo,
        UnitNo,
        FeePercent,
        FeeAmount,
        PayAmount,
        NetAmount,
        UpdatedDate,
        UpdatedByName,
        DepositStatus,
        DepositNo,
        postPI,
        Vat,
        receiveType,
        CompanyName,
        IsWrongAccount,
        IsForeignCreditCard
    }
}
