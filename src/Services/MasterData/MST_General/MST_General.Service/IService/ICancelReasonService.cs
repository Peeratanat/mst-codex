using Database.Models.MST;
using Base.DTOs.MST;
using PagingExtensions;
using MST_General.Params.Outputs;
using MST_General.Params.Filters;

namespace MST_General.Services
{
    public interface ICancelReasonService : BaseInterfaceService
    {
        Task<List<CancelReasonDropdownDTO>> GetCancelReasonDropdownListAsync(string name, CancellationToken cancellationToken = default);
        Task<CancelReasonPaging> GetCancelReasonListAsync(CancelReasonFilter filter, PageParam pageParam, CancelReasonSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<CancelReasonDTO> GetCancelReasonAsync(Guid id, CancellationToken cancellationToken = default);
        Task<CancelReasonDTO> CreateCancelReasonAsync(CancelReasonDTO input);
        Task<CancelReasonDTO> UpdateCancelReasonAsync(Guid id, CancelReasonDTO input);
        Task DeleteCancelReasonAsync(Guid id);
    }
}