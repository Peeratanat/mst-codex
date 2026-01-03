using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class ContactListSortByParam
    {
        public ContactListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ContactListSortBy
    {
        ContactNo,
        FirstNameTH,
        LastNameTH,
        PhoneNumber,
        CreatedDate,
        UpdatedDate,
        LastOpportunityDate,
        OpportunityCount,
        CitizenIdentityNo
    }
}
