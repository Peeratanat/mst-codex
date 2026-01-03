using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class SpecMaterialCollectionSortByParam
    {
        public SpecMaterialCollectionSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum SpecMaterialCollectionSortBy
    {
        Name,
        Updated,
        UpdatedBy
    }
}
