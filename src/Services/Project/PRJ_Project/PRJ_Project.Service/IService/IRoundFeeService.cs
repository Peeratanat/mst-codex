using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_Project.Services
{
    public interface IRoundFeeService : BaseInterfaceService
    {
        Task<RoundFeePaging> GetRoundFeeListAsync(Guid projectID, RoundFeeFilter filter, PageParam pageParam, RoundFeeSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<RoundFeeDTO> GetRoundFeeAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<RoundFeeDTO> CreateRoundFeeAsync(Guid projectID, RoundFeeDTO input);
        Task<RoundFeeDTO> UpdateRoundFeeAsync(Guid projectID, Guid id, RoundFeeDTO input);
        Task<RoundFee> DeleteRoundFeeAsync(Guid projectID, Guid id);
    }
}
