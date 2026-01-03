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
    public interface IWaterElectricMeterPriceService : BaseInterfaceService
    {
        Task<WaterElectricMeterPricePaging> GetWaterElectricMeterPriceListAsync(Guid modelID, WaterElectricMeterPriceFilter filter, PageParam pageParam, SortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<WaterElectricMeterPriceDTO> GetWaterElectricMeterPriceAsync(Guid modelID, Guid id, CancellationToken cancellationToken = default);
    }
}
