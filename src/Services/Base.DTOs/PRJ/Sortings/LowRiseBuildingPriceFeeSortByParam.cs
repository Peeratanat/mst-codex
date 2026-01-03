using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class LowRiseBuildingPriceFeeSortByParam
    {
        public LowRiseBuildingPriceFeeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum LowRiseBuildingPriceFeeSortBy
    {
        Model,
        Unit,
        Price,
        Updated,
        UpdatedBy,
        TypeOfRealEstate,
        BuildingPermitPricePerArea
    }
}
