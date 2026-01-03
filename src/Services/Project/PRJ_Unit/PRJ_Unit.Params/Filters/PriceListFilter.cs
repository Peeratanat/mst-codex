using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Unit.Params.Filters
{
    public class PriceListFilter : BaseFilter
    {
        public string UnitNo { get; set; }

        public double? SaleAreaFrom { get; set; }
        public double? SaleAreaTo { get; set; }

        public double? TitleDeedAreaFrom { get; set; }
        public double? TitleDeedAreaTo { get; set; }

        public double? OffsetAreaFrom { get; set; }
        public double? OffsetAreaTo { get; set; }

        public decimal? OffsetAreaUnitPriceFrom { get; set; }
        public decimal? OffsetAreaUnitPriceTo { get; set; }

        public decimal? OffsetAreaPriceFrom { get; set; }
        public decimal? OffsetAreaPriceTo { get; set; }

        public decimal? TotalSalePriceFrom { get; set; }
        public decimal? TotalSalePriceTo { get; set; }
 
        public double? PercentDownPaymentFrom { get; set; }
        public double? PercentDownPaymentTo { get; set; }

        public decimal? BookingAmountFrom { get; set; }
        public decimal? BookingAmountTo { get; set; }

        public decimal? ContractAmountFrom { get; set; }
        public decimal? ContractAmountTo { get; set; }

        public double? DownPaymentPeriodFrom { get; set; }
        public double? DownPaymentPeriodTo { get; set; }

        public decimal? DownPaymentPerPeriodFrom { get; set; }
        public decimal? DownPaymentPerPeriodTo { get; set; }

        public decimal? DownAmountFrom { get; set; }
        public decimal? DownAmountTo { get; set; }
      
        public string SpecialDown { get; set; }

        public string SpecialDownPrice { get; set; }

        public string UnitStatusKey { get; set; }
    }
}
