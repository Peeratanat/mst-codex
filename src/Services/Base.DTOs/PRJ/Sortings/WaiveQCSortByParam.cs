using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class WaiveQCSortByParam
    {
        public WaiveQCSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum WaiveQCSortBy
    {
        Unit,
        Unit_UnitStatus,
        ActualTransferDate,
        WaiveQCDate,
        UpdatedBy,
        Updated
    }
}
