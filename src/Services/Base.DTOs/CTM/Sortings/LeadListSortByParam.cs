using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class LeadListSortByParam
    {
        public LeadListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum LeadListSortBy
    {
        FirstName,
        LastName,
        PhoneNumber,
        LeadType,
        Owner,
        Project,
        CreatedDate,
        SubLeadType
    }
}
