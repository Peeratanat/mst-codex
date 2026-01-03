using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Params.Filters
{
    public class WaterElectricMeterPriceFilter : BaseFilter
    {
        public int? Version { get; set; }
        public decimal? WaterMeterPriceFrom { get; set; }
        public decimal? WaterMeterPriceTo { get; set; }
        public decimal? ElectricMeterPriceFrom { get; set; }
        public decimal? ElectricMeterPriceTo { get; set; }
        public string ElectricMeterSize { get; set;}
        public string WaterMeterSize { get; set; }
    }
}
