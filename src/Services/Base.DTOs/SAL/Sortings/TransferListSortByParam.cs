using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL.Sortings
{
    public class TransferListSortByParam
    {
        public TransferListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum TransferListSortBy
    {
        UnitNo,
        TransferNo,
        FullName,
        CreditBankingType,
        ScheduleTransferDate,
        ActualTransferDate,
        IsAssignAuthority,
        MarriageStatus,
        TransferStatus
    }
}
