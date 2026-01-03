using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class BankAccountDropdownSortByParam
    {
        public BankAccountDropdownSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BankAccountDropdownSortBy
    {
        BankAccountNo,
        BankAccountType,
        DisplayName,
        PrefixName,
    }
}

