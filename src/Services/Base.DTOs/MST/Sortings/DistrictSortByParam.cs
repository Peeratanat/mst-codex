using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class DistrictSortByParam
    {
        public DistrictSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum DistrictSortBy
    {
        NameEN,
        NameTH,
        Province,
        PostalCode,
        Updated,
        UpdatedBy
    }
}
