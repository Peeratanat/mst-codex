using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MappingAgreementSortByParam
    {
        public MappingAgreementSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MappingAgreementSortBy
    {
        OldAgreement,
        OldItem,
        OldMaterialCode,
        NewAgreement,
        NewItem,
        NewMaterialCode,
        Created,
        CreateBy
    }
}
