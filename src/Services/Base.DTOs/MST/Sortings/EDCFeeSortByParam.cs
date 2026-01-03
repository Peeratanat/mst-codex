using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class EDCFeeSortByParam
    {
        public EDCFeeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum EDCFeeSortBy
    {
        PaymentCardType,
        CreditCardType,
        CreditCardPaymentType,
        Fee,
        Bank,
        CreditCardPromotionName,
        IsEDCBankCreditCard
    }
}
