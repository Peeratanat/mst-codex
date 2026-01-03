using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class LandOfficeSortByParam
    {
        public LandOfficeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum LandOfficeSortBy
    {
        NameTH,
        NameEN,
        Province,
        District,
        SubDistrict,
        Updated,
        UpdatedBy
    }
}
