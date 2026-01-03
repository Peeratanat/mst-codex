using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_Unit.Services
{
    public interface ILowRiseFeeService: BaseInterfaceService
    {
        Task<LowRiseFeePaging> GetLowRiseFeeListAsync(Guid projectID, LowRiseFeeFilter filter, PageParam pageParam, LowRiseFeeSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<LowRiseFeeDTO> GetLowRiseFeeAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<LowRiseFeeDTO> CreateLowRiseFeeAsync(Guid projectID, LowRiseFeeDTO input);
        Task<LowRiseFeeDTO> UpdateLowRiseFeeAsync(Guid projectID, Guid id, LowRiseFeeDTO input);
        Task<LowRiseFee> DeleteLowRiseFeeAsync(Guid projectID, Guid id);
    }
}
