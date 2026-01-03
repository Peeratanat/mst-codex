using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class SubBGPaging
    {
        public List<SubBGDTO> SubBGs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
