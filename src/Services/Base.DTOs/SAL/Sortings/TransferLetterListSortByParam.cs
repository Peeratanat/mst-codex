using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL.Sortings
{
    public class TransferLetterListSortByParam
    {
        public TransferLetterListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum TransferLetterListSortBy
    {
        UnitNo,
        FullName,
        TransferLetterType,
        AppointmentTransferDate,
        TransferOwnershipDate,
        NewTransferOwnershipDate,
        PostponeTransferDate,
        TransferLetterDate,
        ResponseDate,
        LetterStatus,
        LetterReasonResponse,
        PostTrackingNo
    }
}
