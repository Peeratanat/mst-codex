using Base.DTOs.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Project.Params.Outputs
{
    public class RoundFeePaging
    {
        public List<RoundFeeDTO> RoundFees { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
