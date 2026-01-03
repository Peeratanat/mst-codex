using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class BrandSortByParam
    {
        public BrandSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BrandSortBy
    {
        BrandNo,
        Name,
        UnitNumberFormat,
        Updated,
        UpdatedBy
    }
}
