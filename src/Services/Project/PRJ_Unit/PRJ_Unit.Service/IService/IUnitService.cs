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
    public interface IUnitService : BaseInterfaceService
    {
        Task<List<UnitDropdownDTO>> GetUnitDropdownListAsync(Guid projectID, Guid? towerID = null, Guid? floorID = null, string name = null, string unitStatusKey = null, bool? allUnit = null, CancellationToken cancellationToken = default);
        Task<List<UnitDropdownDTO>> GetUnitQuotationDropdownListAsync(Guid projectID, Guid? towerID = null, Guid? floorID = null, string name = null, string unitStatusKey = null, bool? allUnit = null, CancellationToken cancellationToken = default);
        Task<List<UnitDropdownSellPriceDTO>> GetUnitDropdownWithSellPriceListAsync(Guid projectID, string name, string unitStatusKey = null, CancellationToken cancellationToken = default);
        Task<UnitPaging> GetUnitListAsync(Guid projectID, UnitFilter request, PageParam pageParam, UnitListSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<UnitDTO> GetUnitAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<UnitDTO> CreateUnitAsync(Guid projectID, UnitDTO input);
        Task<UnitDTO> UpdateUnitAsync(Guid projectID, Guid id, UnitDTO input, Guid? userID = null);
        Task<Unit> DeleteUnitAsync(Guid projectID, Guid id);
        Task<UnitInfoDTO> GetUnitInfoAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<UnitInitialExcelDTO> ImportUnitInitialAsync(Guid projectID, FileDTO input, Guid? UserID = null);
        Task<FileDTO> ExportExcelUnitInitialAsync(Guid projectID);
        Task<UnitGeneralExcelDTO> ImportUnitGeneralAsync(Guid projectID, FileDTO input, Guid? UserID = null);
        Task<FileDTO> ExportExcelUnitGeneralAsync(Guid projectID);
        Task<FileDTO> ExportExcelUnitFenceAreaAsync(Guid projectID);
        Task<UnitFenceAreaExcelDTO> ImportUnitFenceAreaAsync(Guid projectID, FileDTO input);
  
        Task<List<UnitDropdownDTO>> GetUnitPosition(string ProjectNo, CancellationToken cancellationToken = default);
        Task<UnitMasterPlanDTO> GetUnitMasterPlanDetail(Guid UnitID, CancellationToken cancellationToken = default);
        Task<bool> ClearPointUnitAsync(Guid projectID );
        Task<List<UnitDropdownDefectDTO>> GetUnitDropdownDefectListAsync(Guid projectID, string KeySearch , CancellationToken cancellationToken = default);
    }
}
