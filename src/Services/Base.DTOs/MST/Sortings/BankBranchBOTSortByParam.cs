using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class BankBranchBOTSortByParam
    {
        public BankBranchBOTSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BankBranchBOTSortBy
    {
        BankBranchCode,
        BankBranchName
    }
}
