using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class OpportunityListSortByParam
    {
        public OpportunityListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum OpportunityListSortBy
    {
        FullName,
        ArriveDate,
        ContactNo,
        PhoneNumber,
        SalesOpportunity,
        Project,
        Owner,
        StatusQuestionaire,
        LastActivity,
        RevisitCount,
        UpdatedDate
    }
}
