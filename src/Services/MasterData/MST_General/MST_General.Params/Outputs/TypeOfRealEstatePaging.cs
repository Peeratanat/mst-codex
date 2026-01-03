using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class TypeOfRealEstatePaging
    {
        public List<TypeOfRealEstateDTO> TypeOfRealEstates { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
