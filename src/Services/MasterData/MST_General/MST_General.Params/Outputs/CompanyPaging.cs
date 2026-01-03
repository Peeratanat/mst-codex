using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class CompanyPaging
    {
        public List<CompanyDTO> Companies { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
