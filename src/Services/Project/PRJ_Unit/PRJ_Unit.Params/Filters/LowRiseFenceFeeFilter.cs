using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Unit.Params.Filters
{
    public class LowRiseFenceFeeFilter : BaseFilter
    {
        public Guid? LandOfficeID { get; set; }
        public Guid? TypeOfRealEstateID { get; set; }
        public double? ConcreteRateFrom { get; set; }
        public double? ConcreteRateTo { get; set; }
        public double? IronRateFrom { get; set; }
        public double? IronRateTo { get; set; }
        public decimal? IronPriceFrom { get; set; }
        public decimal? IronPriceTo { get; set; }
        public decimal? ConcretePriceFrom { get; set; }
        public decimal? ConcretePriceTo { get; set; }
        public decimal? DepreciationPerYearFrom { get; set; }
        public decimal? DepreciationPerYearTo { get; set; }
        public bool? IsCalculateDepreciation { get; set; }
    }
}
