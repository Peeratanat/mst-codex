using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class VisitorLeadListSortByParam
    {
        public VisitorLeadListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum VisitorLeadListSortBy
    {
        Project,
        CreatedDate,
        Advertisement,
        Remark
    }
}
