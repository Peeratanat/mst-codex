using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models.MST;
using PRJ_CombineUnit.Params.Filters;
using PRJ_CombineUnit.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_CombineUnit.Services
{
    public interface ICombineUnitService : BaseInterfaceService
    {
        Task<CombineUnitPaging> GetCombineUnitList(CombineUnitFilter filter, PageParam pageParam, CombineUnitSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<List<CombineUnitDTO>> CreateCombineUnitAsync(List<CombineUnitDTO> input);
        Task<List<CombineUnitDTO>> CreateAndApproveCombineUnitAsync(List<CombineUnitDTO> input);
        Task<List<CombineUnitDTO>> SendApproveAsync(List<CombineUnitDTO> input);
        Task<List<UnitDropdownDTO>> GetUnitDropdownCanCombineAsync(CombineUnitDDLDTO input, CancellationToken cancellationToken = default);
        Task<CombineUnitDTO> ApproveAsync(CombineUnitDTO input);
        Task<CombineUnitDTO> EditCombineAsync(CombineUnitDTO input);
        Task<CombineUnitDTO> DeleteCombineAsync(CombineUnitDTO input);
        Task<CombineUnitPaging> GetCombineHistoryList(Guid? CombineID, PageParam pageParam, CancellationToken cancellationToken = default);
        Task<List<ProjectDropdownDTO>> GetProjectDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, Guid? UserID = null, CancellationToken cancellationToken = default);
    }
}
