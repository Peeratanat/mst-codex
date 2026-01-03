using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;

namespace PRJ_Unit.Services
{
    public interface ILowRiseFenceFeeService : BaseInterfaceService
    {
        Task<LowRiseFenceFeePaging> GetLowRiseFenceFeeListAsync(Guid projectID, LowRiseFenceFeeFilter filter, PageParam pageParam, LowRiseFenceFeeSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<LowRiseFenceFeeDTO> GetLowRiseFenceFeeAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<LowRiseFenceFeeDTO> CreateLowRiseFenceFeeAsync(Guid projectID, LowRiseFenceFeeDTO input);
        Task<LowRiseFenceFeeDTO> UpdateLowRiseFenceFeeAsync(Guid projectID, Guid id, LowRiseFenceFeeDTO input);
        Task<LowRiseFenceFee> DeleteLowRiseFenceFeeAsync(Guid projectID, Guid id);
    }
}
