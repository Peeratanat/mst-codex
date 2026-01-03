using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class SubBGSortByParam
    {
        public SubBGSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum SubBGSortBy
    {
        SubBGNo,
        Name,
        BG,
        Updated,
        UpdatedBy
    }
}
