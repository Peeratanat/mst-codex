using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Unit.Params.Filters
{
    public class HighRiseFeeFilter : BaseFilter
    {
        public Guid? TowerID { get; set; }
        public Guid? FloorID { get; set; }
        public Guid? UnitID { get; set; }
        public string CalculateParkAreaKey { get; set; }
        public decimal? EstimatePriceAreaFrom { get; set; }
        public decimal? EstimatePriceAreaTo { get; set; }
        public decimal? EstimatePriceUsageAreaFrom { get; set; }
        public decimal? EstimatePriceUsageAreaTo { get; set; }
        public decimal? EstimatePriceBalconyAreaFrom { get; set; }
        public decimal? EstimatePriceBalconyAreaTo { get; set; }
        public decimal? EstimatePriceAirAreaFrom { get; set; }
        public decimal? EstimatePriceAirAreaTo { get; set; }
        public decimal? EstimatePricePoolAreaFrom { get; set; }
        public decimal? EstimatePricePoolAreaTo { get; set; }
    }
}
