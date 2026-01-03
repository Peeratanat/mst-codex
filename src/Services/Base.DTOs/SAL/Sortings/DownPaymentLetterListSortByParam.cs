using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL.Sortings
{
    public class DownPaymentLetterListSortByParam
    {
        public DownPaymentLetterListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum DownPaymentLetterListSortBy
    {
        UnitNo,
        FullName,
        RemainDownPeriodCount,
        RemainDownTotalAmount,
        DownPaymentLetterType,
        DownPaymentLetterDate,
        LetterStatus,
        ResponseDate,
        LetterReasonResponse,
        PostTrackingNo
    }
}
