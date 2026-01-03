using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL.Sortings
{
    public class PaymentCardListSortByParam
    {
        public PaymentCardListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum PaymentCardListSortBy
    {
        UnitNo,
        AgreementNo,
        FullName,
        PhoneNumber,
        IsPrintPaymentCard,
        PrintPaymentCardDate
    }
}
