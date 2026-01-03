using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_Unit.Services
{
    public class WaterElectricMeterPriceService : IWaterElectricMeterPriceService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        public WaterElectricMeterPriceService(DatabaseContext db)
        {
            logModel = new LogModel("AgreementService", null);
            DB = db;
        }

        public async Task<WaterElectricMeterPricePaging> GetWaterElectricMeterPriceListAsync(Guid modelID, WaterElectricMeterPriceFilter request, PageParam pageParam, SortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<WaterElectricMeterPriceQueryResult> query = DB.WaterElectricMeterPrices.AsNoTracking().Where(x => x.ModelID == modelID)
                                                                      .Select(x => new WaterElectricMeterPriceQueryResult
                                                                      {
                                                                          WaterElectricMeterPrice = x,
                                                                          UpdatedBy = x.UpdatedBy
                                                                      });


            #region Filter
            if (request.Version != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.Version == request.Version);
            }
            if (request.ElectricMeterPriceFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice >= request.ElectricMeterPriceFrom);
            }
            if (request.ElectricMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice <= request.ElectricMeterPriceTo);
            }
            if (request.ElectricMeterPriceFrom != null && request.ElectricMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice <= request.ElectricMeterPriceTo
                                   && x.WaterElectricMeterPrice.ElectricMeterPrice >= request.ElectricMeterPriceFrom);
            }
            if (!string.IsNullOrEmpty(request.ElectricMeterSize))
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterSize.Contains(request.ElectricMeterSize));
            }

            if (request.WaterMeterPriceFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice >= request.WaterMeterPriceFrom);
            }
            if (request.WaterMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice <= request.WaterMeterPriceTo);
            }
            if (request.WaterMeterPriceFrom != null && request.WaterMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice <= request.WaterMeterPriceTo
                                   && x.WaterElectricMeterPrice.WaterMeterPrice >= request.WaterMeterPriceFrom);
            }

            if (!string.IsNullOrEmpty(request.WaterMeterSize))
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterSize.Contains(request.WaterMeterSize));
            }
            if (request.UpdatedFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.Updated >= request.UpdatedFrom);
            }
            if (request.UpdatedTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.Updated <= request.UpdatedTo);
            }
            if (request.UpdatedFrom != null && request.UpdatedTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.Updated >= request.UpdatedFrom && x.WaterElectricMeterPrice.Updated <= request.UpdatedTo);
            }
            if (!string.IsNullOrEmpty(request.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(request.UpdatedBy));
            }
            #endregion

            WaterElectricMeterPriceDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<WaterElectricMeterPriceQueryResult>(pageParam, ref query);

            var results = await query.Select(o => WaterElectricMeterPriceDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new WaterElectricMeterPricePaging()
            {
                PageOutput = pageOutput,
                WaterElectricMeterPrices = results
            };
        }

        public async Task<WaterElectricMeterPriceDTO> GetWaterElectricMeterPriceAsync(Guid modelID, Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.WaterElectricMeterPrices.FirstAsync(x => x.ModelID == modelID && x.ID == id, cancellationToken);
            return WaterElectricMeterPriceDTO.CreateFromModel(model);
        }

    }
}
