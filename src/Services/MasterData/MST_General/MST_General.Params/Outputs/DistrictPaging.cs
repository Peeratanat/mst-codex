using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class DistrictPaging
    {
        public List<DistrictDTO> Districts { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
