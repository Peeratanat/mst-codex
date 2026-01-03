using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Unit.Params.Filters
{
    public class MinPriceFilter : BaseFilter
    {
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public double? SaleAreaFrom { get; set; }
        public double? SaleAreaTo { get; set; }
        public double? TitledeedAreaFrom { get; set; }
        public double? TitledeedAreaTo { get; set; }
        public decimal? CostFrom { get; set; }
        public decimal? CostTo { get; set; }
        public decimal? SalePriceFrom { get; set; }
        public decimal? SalePriceTo { get; set; }
        public string MinPriceTypeKey { get; set; }
        public decimal? ROIMinpriceFrom { get; set; }
        public decimal? ROIMinpriceTo { get; set; }
        public decimal? ApprovedMinPriceFrom { get; set; }
        public decimal? ApprovedMinPriceTo { get; set; }
        public string DocTypeKey { get; set; }
    }
}
