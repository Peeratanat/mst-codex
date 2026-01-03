using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class DepositSortByParam
    {
        public DepositSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum DepositSortBy
    {
        Project,
        Unit,
        ReceiveDate,
        ReceiptTempNo,
        Paymentype,
        TotalAmount,
        Fee,
        Vat,
        DepositStatus,
        PostStatus,
        PINumber,
        FeeIncludingVat,
        Number
    }
}

