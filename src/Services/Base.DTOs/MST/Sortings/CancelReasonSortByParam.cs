using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class CancelReasonSortByParam
    {
        public CancelReasonSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }
    public enum CancelReasonSortBy
    {
        Updated,
        UpdatedBy,
        Key,
        Description,
        GroupOfCancelReason,
        CancelApproveFlow,
    }
}