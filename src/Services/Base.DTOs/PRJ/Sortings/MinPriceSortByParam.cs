using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class MinPriceSortByParam
    {
        public MinPriceSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MinPriceSortBy
    {
        Unit_UnitNo,
        Unit_HouseNo,
        Unit_SaleArea,
        Titledeed_TitledeedArea,
        Cost,
        MinPriceType,
        ROIMinprice,
        SalePrice,
        ApprovedMinPrice,
        DocType,
        Updated,
        UpdatedBy
    }
}
