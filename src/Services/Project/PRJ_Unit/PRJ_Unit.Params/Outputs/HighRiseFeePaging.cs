using Base.DTOs.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Params.Outputs
{
    public class HighRiseFeePaging
    {
        public List<HighRiseFeeDTO> HighRiseFees { get; set; }

        public PageOutput PageOutput { get; set; }
    }
}
