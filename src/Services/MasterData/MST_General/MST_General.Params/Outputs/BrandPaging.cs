using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class BrandPaging
    {
        public List<BrandDTO> Brands { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
