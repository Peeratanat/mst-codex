using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using MST_Unitmeter.Params.Filters;
using MST_Unitmeter.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_Unitmeter.Services
{
    public interface IUnitMeterService : BaseInterfaceService
    {
        Task<List<WaterMeterPriceDropdownDTO>> GetWaterMeterPriceDropdownListAsync(Guid unitID, CancellationToken cancellationToken = default);
        Task<List<ElectricMeterPriceDropdownDTO>> GetElectricMeterPriceDropdownListAsync(Guid unitID, CancellationToken cancellationToken = default);
        Task<UnitMeterPaging> GetUnitMeterListAsync(UnitMeterFilter request, PageParam pageParam, UnitMeterListSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<UnitMeterDTO> GetUnitMeterAsync(Guid unitID, CancellationToken cancellationToken = default);
        Task DeleteUnitMeterAsync(Guid unitID);
        Task<UnitMeterDTO> UpdateUnitMeterAsync(Guid unitID, UnitMeterDTO input, Guid? UserID = null);
        Task<UnitMeterExcelDTO> ImportUnitMeterExcelAsync(FileDTO input);

        Task<FileDTO> ExportUnitMeterExcelAsync(UnitMeterFilter request, UnitMeterListSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<UnitMeterExcelDTO> ImportUnitMeterStatusExcelAsync(FileDTO input);
        Task<FileDTO> ExportUnitMeterStatusExcelAsync(UnitMeterFilter filter, UnitMeterListSortByParam sortByParam, CancellationToken cancellationToken = default);
    }
}
