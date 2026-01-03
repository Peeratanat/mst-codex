using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class BankSortByParam
    {
        public BankSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BankSortBy
    {
        BankNo,
        NameTH,
        NameEN,
        Alias,
        IsCreditCard,
        IsNonBank,
        IsCoorperative,
        IsFreeMortgage,
        Updated,
        UpdatedBy,
        SwiftCode
    }
}

