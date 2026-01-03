using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL.Sortings
{
    public class TransferStatusListSortByParam
    {
        public TransferStatusListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum TransferStatusListSortBy
    {
        UnitNo,
        UnitStatus,
        ScheduleTransferDate,
        IsPrepareTransfer,
        PrepareTransferDate,
        LandStatus,
        PreferStatus,
        LandStatusDate,
        HouseNo,
    }
}
