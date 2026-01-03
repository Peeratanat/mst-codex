using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_Finacc.Params.Outputs
{
    public class BankAccountPaging
    {
        public List<BankAccountDTO> BankAccounts { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
