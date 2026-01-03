

using Base.DTOs.MST;
using Common.Helper.Logging;
using Database.Models.MST;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using PagingExtensions;

namespace MST_General.Services
{
    public interface IBGService : BaseInterfaceService
    {
        Task<List<BGDropdownDTO>> GetBGDropdownListAsync(string? productTypeKey, string? name, CancellationToken cancellationToken);
        Task<BGPaging> GetBGListAsync(BGFilter filter, PageParam pageParam, BGSortByParam sortByParam, CancellationToken cancellationToken);
        Task<BGDTO> GetBGAsync(Guid id, CancellationToken cancellationToken);
        Task<BGDTO> CreateBGAsync(BGDTO input);
        Task<BGDTO> UpdateBGAsync(Guid id, BGDTO input);
        Task DeleteBGAsync(Guid id);
    }
}
