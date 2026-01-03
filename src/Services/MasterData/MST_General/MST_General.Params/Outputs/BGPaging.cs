using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class BGPaging
    {
        public List<BGDTO> BGs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
