using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Unit.Params.Filters
{
    public class LowRiseFeeFilter : BaseFilter
    {
        public Guid? UnitID { get; set; }
        public decimal? EstimatePriceAreaFrom { get; set; }
        public decimal? EstimatePriceAreaTo { get; set; }
    }
}
