using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class SpecMaterialCollectionDetailSortByParam
    {
        public SpecMaterialCollectionDetailSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum SpecMaterialCollectionDetailSortBy
    {
        Group,
        Name,
        ItemDesc,
        NameEN,
        ItemDescEN,
        updated,
        updatedBy
    }
}
