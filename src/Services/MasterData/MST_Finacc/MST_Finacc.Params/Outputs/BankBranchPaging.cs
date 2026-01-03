using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_Finacc.Params.Outputs
{
    public class BankBranchPaging
    {
        public List<BankBranchDTO> BankBranches { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
