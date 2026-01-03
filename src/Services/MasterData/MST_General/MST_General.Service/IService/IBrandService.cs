using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public interface IBrandService: BaseInterfaceService
    {
        Task<List<BrandDropdownDTO>> GetBrandDropdownListAsync(string name, CancellationToken cancellationToken = default);
        Task<BrandPaging> GetBrandListAsync(BrandFilter filter, PageParam pageParam, BrandSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<BrandDTO> GetBrandAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BrandDTO> CreateBrandAsync(BrandDTO input);
        Task<BrandDTO> UpdateBrandAsync(Guid id, BrandDTO input);
        Task DeleteBrandAsync(Guid id);
    }
}
