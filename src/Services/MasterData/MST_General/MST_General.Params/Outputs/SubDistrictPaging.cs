using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class SubDistrictPaging
    {
        public List<SubDistrictDTO> SubDistricts { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
