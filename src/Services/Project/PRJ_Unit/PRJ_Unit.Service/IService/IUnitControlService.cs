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
    public interface IUnitControlService : BaseInterfaceService
    {
        Task<UnitControlInterestPaging> GetUnitControlInterestAsync(Guid projectID, UnitControlInterestFilter filter, PageParam pageParam, UnitControlInterestSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<UnitControlInterestDTO> AddUnitControlInterestAsync(UnitControlInterestDTO input);
        Task<UnitControlInterestDTO> UpdateUnitControlInterestAsync(UnitControlInterestDTO input);
        Task DeleteUnitControlInterestAsync(Guid? input);
    }
}
