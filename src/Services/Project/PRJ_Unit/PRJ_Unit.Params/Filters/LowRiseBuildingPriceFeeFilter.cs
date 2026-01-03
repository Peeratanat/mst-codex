using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Unit.Params.Filters
{
    public class LowRiseBuildingPriceFeeFilter : BaseFilter
    {
        public Guid? ModelID { get; set; }
        public Guid? UnitID { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public Guid? TypeOfRealEstateID { get; set; }
        public decimal? BuildingPermitPricePerAreaFrom { get; set; }
        public decimal? BuildingPermitPricePerAreaTo { get; set; }
    }
}
