using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class VisitorHistoryListSortByParam
    {
        public VisitorHistoryListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; } 
    }

    public enum VisitorHistoryListSortBy
    {
        Project,
        VisitDateIn,
        VisitDateOut,
        SalesOpportunity
    }
}
