using Base.DTOs.PRM;
using PagingExtensions;

namespace MST_Promotion.Params.Outputs
{
    public class MappingAgreementPaging
    {
        public List<MappingAgreementDTO> MappingAgreements { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
