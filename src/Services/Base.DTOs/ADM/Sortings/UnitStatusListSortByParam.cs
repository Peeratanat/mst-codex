using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.ADM.Sortings
{
    public class UnitStatusListSortByParam
    {
        public UnitStatusListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UnitStatusListSortBy
    {
        UnitNo,
        CustomerName,
        BookingNo,
        AgreementNo,
        TransferNo,
        TransferPromotionNo,
        UnitStatus
    }
}
