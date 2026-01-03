using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class TransferSortByParam
    {
        public TransferSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum TransferSortBy
    {
        UnitNo,
        CustomerName,
        TransferConfirmedDate,
        APBalanceTo,
        CostAmount,
        FreeDownAmount,
        ChequeAmount,
        BankTransferAmount,
        CashAmount,
        APChangeAmount,
        PaymentConfirmedStatus,
        AccountApprovedStatus
    }
}
