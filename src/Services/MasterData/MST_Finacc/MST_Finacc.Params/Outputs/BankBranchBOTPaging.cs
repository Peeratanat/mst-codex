using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_Finacc.Params.Outputs
{
    public class BankBranchBOTPaging
    {
        public List<BankBranchBOTDTO> BankBrancheBOTs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
