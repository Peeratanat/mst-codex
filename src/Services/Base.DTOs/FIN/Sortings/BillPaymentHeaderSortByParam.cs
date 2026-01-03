using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class BillPaymentHeaderSortByParam
    {
        public BillPaymentSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BillPaymentSortBy
    {
        BatchID,
        Bank,
        ReceiveDate,
        CreateDate,
        TotalRecord,
        TotalSuccessRecord,
        TotalWatingRecord,
        TotalAmount,
        ImportBy,
        Company,
        BankAccount
    }
}
