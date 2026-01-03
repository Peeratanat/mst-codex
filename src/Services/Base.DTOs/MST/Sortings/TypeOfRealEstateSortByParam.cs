using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class TypeOfRealEstateSortByParam
    {
        public TypeOfRealEstateSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum TypeOfRealEstateSortBy
    {
        Code,
        Name,
        RealEstateCategory,
        StandardCost,
        StandardPrice,
        Updated,
        UpdatedBy
    }
}
