using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class LowRiseFenceFeeSortByParam
    {
        public LowRiseFenceFeeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum LowRiseFenceFeeSortBy
    {
        LandOffice,
        TypeOfRealEstate,
        ConcreteRate,
        ConcretePrice,
        IronRate,
        IronPrice,
        DepreciationPerYear,
        IsCalculateDepreciation,
        Updated,
        UpdatedBy
    }
}
