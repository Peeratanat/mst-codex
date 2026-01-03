using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class FloorSortByParam
    {
        public FloorSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum FloorSortBy
    {
        NameTH,
        NameEN,
        Description,
        Updated,
        UpdatedBy
    }
}
