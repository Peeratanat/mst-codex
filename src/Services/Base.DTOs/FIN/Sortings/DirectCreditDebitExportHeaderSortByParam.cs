using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class DirectCreditDebitExportHeaderSortByParam
    {
        public DirectCreditDebitExportHeaderSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum DirectCreditDebitExportHeaderSortBy
    {
        BatchID,
        Bank,
        BankAccount,
        Company,
        PeriodDate,
        ReceiveDate,
        DirectCreditDebitType,
        TotalRecord,
        TotalErrorRecord,
        TotalAmount,
        ImportDate,
        ImportBy,
        ExportDate
    }
}
