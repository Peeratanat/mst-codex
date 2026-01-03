using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class LegalEntitySortByParam
    {
        public LegalEntitySortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum LegalEntitySortBy
    {
        NameTH,
        NameEN,
        Bank,
        BankAccountNo,
        BankAccountType,
        IsActive,
        VendorCode,
        Updated,
        UpdatedBy
    }
}
