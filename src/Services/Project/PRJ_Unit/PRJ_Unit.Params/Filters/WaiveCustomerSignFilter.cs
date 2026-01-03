using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Params.Filters
{
    public class WaiveCustomerSignFilter : BaseFilter
    {
        public Guid? UnitID { get; set; }
        public DateTime? ActualTransferDateFrom { get; set; }
        public DateTime? ActualTransferDateTo { get; set; }
        public DateTime? WaiveSignDateFrom { get; set; }
        public DateTime? WaiveSignDateTo { get; set; }
        public string UnitStatusKey { get; set; }
    }
}
