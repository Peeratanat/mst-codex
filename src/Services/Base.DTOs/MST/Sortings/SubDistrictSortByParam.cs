using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class SubDistrictSortByParam
    {
        public SubDistrictSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum SubDistrictSortBy
    {
        NameTH,
        NameEN,
        LandOffice,
        District,
        PostalCode,
        Updated,
        UpdatedBy
    }
}
