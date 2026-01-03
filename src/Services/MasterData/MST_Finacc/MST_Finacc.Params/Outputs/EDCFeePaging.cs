using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_Finacc.Params.Outputs
{
    public class EDCFeePaging
    {
        public List<EDCFeeDTO> EDCFees { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
