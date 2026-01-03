using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class ProvinceSortByParam
    {
        public ProvinceSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ProvinceSortBy
    {
        NameTH,
        NameEN,
        IsShow,
        Updated,
        UpdatedBy
    }
}
