using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class UnitInfoListSortByParam
    {
        public UnitInfoListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UnitInfoListSortBy
    {
        UnitNo,
        HouseNo,
        FullName,
        BookingNo,
        AgreementNo,
        TransferPromotionNo,
        BankAccountName,
        TransferNo,
        ProjectNo,
        UnitStatus
    }
}
