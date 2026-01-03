using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class ContactAuditSortByParam
    {
        public ContactAuditSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ContactAuditSortBy
    {
        ContactNo,
        FirstNameTH,
        LastNameTH,
        UpdatedDate,
        UpdatedBy,
    }
}
