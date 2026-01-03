using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Unit.Params.Filters
{
    public class WaiveQCFilter : BaseFilter
    {
        public Guid? UnitID { get; set; }
        public DateTime? ActualTransferDateFrom { get; set; }
        public DateTime? ActualTransferDateTo { get; set; }
        public DateTime? WaiveQCDateFrom { get; set; }
        public DateTime? WaiveQCDateTo { get; set; }
        public string UnitStatusKey { get; set; }
    }
}
