using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class MasterCenterPaging
    {
        public List<MasterCenterDTO> MasterCenters { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
