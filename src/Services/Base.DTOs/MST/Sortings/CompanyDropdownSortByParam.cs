using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class CompanyDropdownSortByParam
    {
        public CompanyDropdownSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CompanyDropdownSortBy
    {
        Code,
        NameTH,
        NameEN,
        SAPCompanyID
    }
}
