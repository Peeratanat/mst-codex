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
    public interface ILowRiseBuildingPriceFeeService : BaseInterfaceService
    {
        Task<LowRiseBuildingPriceFeePaging> GetLowRiseBuildingPriceFeeListAsync(Guid projectID, LowRiseBuildingPriceFeeFilter filter, PageParam pageParam, LowRiseBuildingPriceFeeSortByParam sortByParam,CancellationToken cancellationToken = default);
        Task<LowRiseBuildingPriceFeeDTO> GetLowRiseBuildingPriceFeeAsync(Guid projectID, Guid id,CancellationToken cancellationToken = default);
        Task<LowRiseBuildingPriceFeeDTO> CreateLowRiseBuildingPriceFeeAsync(Guid projectID, LowRiseBuildingPriceFeeDTO input);
        Task<LowRiseBuildingPriceFeeDTO> UpdateLowRiseBuildingPriceFeesync(Guid projectID, Guid id, LowRiseBuildingPriceFeeDTO input);
        Task<LowRiseBuildingPriceFee> DeleteLowRiseBuildingPriceFeeAsync(Guid projectID, Guid id);
    }
}
