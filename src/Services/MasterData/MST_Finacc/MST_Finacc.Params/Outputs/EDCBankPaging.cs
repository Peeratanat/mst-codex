using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_Finacc.Params.Outputs
{
    public class EDCBankPaging
    {
        public List<EDCBankDTO> EDCBanks { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
