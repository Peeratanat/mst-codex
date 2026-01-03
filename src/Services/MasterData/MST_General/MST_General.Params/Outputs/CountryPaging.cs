using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class CountryPaging
    {
        public List<CountryDTO> Countries { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
