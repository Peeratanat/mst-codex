using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class EDCBankSortByParam
    {
        public EDCBankSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum EDCBankSortBy
    {
        Bank,
        Updated,
        UpdatedBy
    }
}
