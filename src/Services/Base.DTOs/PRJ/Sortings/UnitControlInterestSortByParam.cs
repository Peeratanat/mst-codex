using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class UnitControlInterestSortByParam
    {
        public UnitControlInterestSortByParamSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UnitControlInterestSortByParamSortBy
    {
        UnitNo,
        TowerName,
        FloorName,
        UnitStatus,
    }
}
