using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class TowerSortByParam
    {
        public TowerSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum TowerSortBy
    {
        Code,
        FloorCount,
        NoTH,
        NoEN,
        CondominiumName,
        CondominiumNo,
        Description,
        UpdatedBy,
        Updated
    }
}
