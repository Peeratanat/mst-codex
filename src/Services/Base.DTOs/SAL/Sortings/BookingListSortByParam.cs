using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL.Sortings
{
    public class BookingListSortByParam
    {
        public BookingListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BookingListSortBy
    {
        BookingNo,
        UnitNo,
        FullName,
        BookingDate,
        ApproveDate,
        ContractDate,
        BookingStatus,
        CreateBookingFrom,
        ConfirmBy,
        ConfirmDate,
        SellingPrice,
        ReceiptTempNo
    }
}
