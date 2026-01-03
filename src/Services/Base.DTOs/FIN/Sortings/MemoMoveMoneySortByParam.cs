using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class MemoMoveMoneySortByParam
    {
        public MemoMoveMoneySortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MemoMoveMoneySortBy
    {
        Project,
        Unit,
        ReceiveDate,
        ReceiptNo,
        PayAmount,
        Company,
        Bank,
        BankAccount,
        PaymentMethod,
        DestinationCompany,
        PrintStatus,
        PrintBy,
        PrintDate,
        PaymentIsCancel
    }
}
