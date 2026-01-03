using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class BGSortByParam
    {
        public BGSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BGSortBy
    {
        BGNo,
        Name,
        ProductType,
        Updated,
        UpdatedBy
    }
}
