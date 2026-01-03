using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class SpecMaterialCollectionPaging
    {
        public List<SpecMaterialCollectionDTO> SpecMaterialCollection { get; set; }
        public PageOutput PageOutput { get; set; }
    }

    public class SpecMaterialCollectionDetailPaging
    {
        public List<SpecMaterialCollectionDetailDTO> SpecMaterialCollectionDetail { get; set; }
        public PageOutput PageOutput { get; set; }
    }
    public class SpecMaterialItemPaging
    {
        public List<SpecMaterialItemDTO> SpecMaterialItem { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
