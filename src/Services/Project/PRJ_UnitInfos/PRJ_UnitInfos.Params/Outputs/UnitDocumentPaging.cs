using System.Collections.Generic;
using Base.DTOs.SAL;
using PagingExtensions;

namespace PRJ_UnitInfos.Params.Outputs
{
    public class UnitDocumentPaging
    {
        public List<UnitDocumentDTO> UnitDocuments { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
