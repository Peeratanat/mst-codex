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
    public interface ILockUnitService : BaseInterfaceService
    {

        Task<UnitControlLockPaging> GetLockUnitAsync(Guid projectID, UnitControlLockFilter unitControlLockFilter, PageParam pageParam, UnitControlLockByParam sortByParam, CancellationToken cancellationToken = default);
        Task<UnitControlLockDTO> UpdateLockUnitAsync(UnitControlLockDTO input);
        Task DeleteLockUnitAsync(Guid? input);
    }
}
