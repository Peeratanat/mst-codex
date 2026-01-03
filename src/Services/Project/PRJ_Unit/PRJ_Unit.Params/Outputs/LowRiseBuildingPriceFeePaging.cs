using Base.DTOs.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Params.Outputs
{
    public class LowRiseBuildingPriceFeePaging
    {
        public List<LowRiseBuildingPriceFeeDTO> LowRiseBuildingPriceFees { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
