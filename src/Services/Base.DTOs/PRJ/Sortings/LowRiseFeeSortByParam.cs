using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class LowRiseFeeSortByParam
    {
        public LowRiseFeeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum LowRiseFeeSortBy
    {
        Unit,
        EstimatePriceArea,
        Updated,
        UpdatedBy
    }
}
