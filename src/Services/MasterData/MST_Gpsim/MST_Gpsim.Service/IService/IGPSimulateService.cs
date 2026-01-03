using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using MST_Gpsim.Params.Filters;
using MST_Gpsim.Params.Outputs;
using PagingExtensions;
using Report.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_Gpsim.Services
{
    public interface IGPSimulateService : BaseInterfaceService
    {
        Task<GPSimulatePaging> GetGPSimulateListAsync(GPSimulateFilter filter, PageParam pageParam, GPSimulateSortByParam sortByParam, Guid? userID, CancellationToken cancellationToken = default);
        Task<GPSimulateDTO> GetGPOriginalAsync(Guid ProjectID, CancellationToken cancellationToken = default);
        Task<GPSimulateDTO> SaveDraftGPSimulateAsync(GPSimulateDTO input);
        Task<bool> CalGPSimulateAsync(Guid? id);
        Task<GPSimulateDTO> GetGPVersionAsync(Guid VersionID, CancellationToken cancellationToken = default);
        Task<bool> DeleteGPSimulateAsync(Guid? id);
        Task<GPUnitImportDTO> ImportUnitAsync(FileDTO input, Guid? projectID, Guid? userID);
        Task<GPBlockImportDTO> ImportBlockAsync(FileDTO input, Guid? projectID, Guid? userID);
        Task<GPUnitImportDTO> ImportPriceAsync(FileDTO input, Guid? projectID, Guid? userID);
        Task<ReportResult> PrintChangeBudget(Guid? VersionID);
        Task<ReportResult> PrintChangePrice(Guid? VersionID);
        Task<FileDTO> ExportTemplatePriceAsync(Guid ProjectID, CancellationToken cancellationToken = default);
        Task<FileDTO> ExportTemplateCO01BlockAsync();
        Task<FileDTO> ExportTemplateCO01UnitAsync();
        Task<FileDTO> ExportTemplateCO01UnitFromGPAsync(Guid ProjectID);
        Task<FileDTO> ExportTemplateCO01BlockFromGPAsync(Guid ProjectID);
        Task<FileDTO> ExportTemplatePriceAndCO01Async(Guid ProjectID);
        Task<GPCO01UnitImportDTO> ImportCO01UnitAsync(FileDTO input, Guid? projectID, Guid? userID);
        Task<GPCO01UnitImportDTO> ImportPriceAndCO01UnitAsync(FileDTO input, Guid? projectID, Guid? userID);
        Task<GPCO01BlockImportDTO> ImportCO01BlockAsync(FileDTO input, Guid? projectID, Guid? userID);

    }
}
