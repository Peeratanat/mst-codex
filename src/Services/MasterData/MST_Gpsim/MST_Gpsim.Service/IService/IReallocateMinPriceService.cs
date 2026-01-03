using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using MST_Gpsim.Params.Filters;
using MST_Gpsim.Params.Outputs;
using MST_Gpsim.Services;
using PagingExtensions;
using Report.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_Gpsim.Services
{
    public interface IReallocateMinPriceService : BaseInterfaceService
    {
        Task<ReallocateMinPricePaging> GetReallocateMinPriceListAsync(ReallocateMinPriceFilter filter, PageParam pageParam, ReallocateMinPriceSortByParam sortByParam, Guid? userID, CancellationToken cancellationToken = default);
        Task<ReallocateMinPriceDTO> GetGPOriginalAsync(Guid ProjectID, CancellationToken cancellationToken = default);
        Task<ReallocateMinPriceDTO> SaveDraftReallocateMinPriceAsync(ReallocateMinPriceDTO input);
        Task CalReallocateMinPriceAsync(Guid? id);
        Task<ReallocateMinPriceDTO> GetReallocateMinPriceAsync(Guid VersionID, CancellationToken cancellationToken = default);
        Task<bool> DeleteReallocateMinPriceAsync(Guid? id);
        Task<GPUnitImportDTO> ImportReallocateMinPriceAsync(FileDTO input, Guid? projectID, Guid? userID);
        ReportResult PrintReallocateMinPrice(Guid? VersionID);
        Task<FileDTO> ExportTemplateReallocateMinPrice(Guid ProjectID);
    }
}
