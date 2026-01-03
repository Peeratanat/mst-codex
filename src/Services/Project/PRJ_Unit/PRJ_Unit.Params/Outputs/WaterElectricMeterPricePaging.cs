using Base.DTOs.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Params.Outputs
{
    public class WaterElectricMeterPricePaging
    {
        public List<WaterElectricMeterPriceDTO> WaterElectricMeterPrices { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
