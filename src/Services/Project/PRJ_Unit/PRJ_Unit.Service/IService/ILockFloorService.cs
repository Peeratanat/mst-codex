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
    public interface ILockFloorService : BaseInterfaceService
    {
        Task<UnitControlLockPaging> GetLockFloorAsync(Guid projectID, UnitControlLockFilter unitControlLockFilter, PageParam pageParam, UnitControlLockByParam sortByParam, CancellationToken cancellationToken = default);
        Task<UnitControlLockDTO> UpdateLockFloorAsync(UnitControlLockDTO input);
        Task DeleteLockFloorAsync(Guid? ID);

    }
}
