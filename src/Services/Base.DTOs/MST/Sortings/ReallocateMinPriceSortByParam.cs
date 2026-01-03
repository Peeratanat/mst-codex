using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class ReallocateMinPriceSortByParam
    {
        public ReallocateMinPriceSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ReallocateMinPriceSortBy
    {
        Version,
        Remark,
        Project,
        Created,
        Updated,
    }
}
