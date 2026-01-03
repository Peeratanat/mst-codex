using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class UnitControlLockByParam
    {
        public UnitControlLockSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UnitControlLockSortBy
    {
        TowerName,
        FloorNameEN,
        FloorNameTH,
        StatusLock,
        EffectiveDate,
        ExpiredDate,
        UnitNo,
        UnitStatus,
    }
}
