using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_Gpsim.Params.Outputs
{
    public class ReallocateMinPricePaging
    {
        public List<ReallocateMinPriceDTO> ReallocateMinPriceDTOs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
