using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class MasterCenterGroupPaging
    {
        public List<MasterCenterGroupDTO> MasterCenterGroups { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
