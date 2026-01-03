using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM.Sortings
{
    public class OppBookingListSortByParam
    {
        public OppBookingListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum OppBookingListSortBy
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
