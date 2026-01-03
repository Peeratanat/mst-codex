using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class VisitorListSortByParam
    {
        public VisitorListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum VisitorListSortBy
    {
        ReceiveNumber,
        ContactNo,
        FullName,
        PhoneNumber,
        VisitBy,
        WalkStatus,
        VehicleDescription,
        Owner,
        VisitDateIn,
        VisitDateOut,
        ContactStatus
    }
}
