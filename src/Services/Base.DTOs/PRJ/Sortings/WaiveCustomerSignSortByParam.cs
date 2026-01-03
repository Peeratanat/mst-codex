using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class WaiveCustomerSignSortByParam
    {
        public WaiveCustomerSignSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum WaiveCustomerSignSortBy
    {
        Unit,
        Unit_UnitStatus,
        ActualTransferDate,
        WaiveSignDate,
        UpdatedBy,
        Updated
    }
}
