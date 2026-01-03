using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class BankBranchSortByParam
    {
        public BankBranchSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BankBranchSortBy
    {
        Bank,
        Name,
        Address,
        Building,
        Soi,
        Road,
        SubDistrict,
        District,
        Province,
        PostalCode,
        Telephone,
        Fax,
        IsCreditBank,
        IsDirectCredit,
        IsDirectDebit,
        AreaCode,
        OldBankID,
        OldBranchID,
        Updated,
        UpdatedBy,
        IsActive
    }
}
