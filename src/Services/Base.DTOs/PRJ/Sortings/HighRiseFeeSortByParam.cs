using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class HighRiseFeeSortByParam
    {
        public HighRiseFeeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum HighRiseFeeSortBy
    {
        Tower,
        Floor,
        Unit,
        CalculateParkArea,
        EstimatePriceArea,
        EstimatePriceUsageArea,
        EstimatePriceBalconyArea,
        EstimatePriceAirArea,
        EstimatePricePoolArea,
        Updated,
        UpdatedBy
    }
}
