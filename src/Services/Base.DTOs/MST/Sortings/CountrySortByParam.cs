using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class CountrySortByParam
    {
        public CountrySortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }
    public enum CountrySortBy
    {
        Code,
        NameTH,
        NameEN,
        Updated,
        UpdatedBy
    }
}
