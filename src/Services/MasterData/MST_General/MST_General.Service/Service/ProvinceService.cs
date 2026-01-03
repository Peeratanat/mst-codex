using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.MST;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using MST_General.Params.Outputs;
using Base.DTOs;
using Common.Helper.Logging;
using LinqKit;

namespace MST_General.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }

        public ProvinceService(DatabaseContext db)
        {
            logModel = new LogModel("ProvinceService", null);
            this.DB = db;
        }

        public async Task<ProvinceListDTO> FindProvinceAsync(string name, CancellationToken cancellationToken = default)
        {
            var model = await DB.Provinces.AsNoTracking().FirstOrDefaultAsync(o => o.NameTH == name || o.NameEN == name, cancellationToken);
            return ProvinceListDTO.CreateFromModel(model);
        }

        public async Task<List<ProvinceListDTO>> GetProvinceDropdownListAsync(string name, CancellationToken cancellationToken = default)
        {
            IQueryable<Province> query = DB.Provinces.AsNoTracking();
            var predicate = PredicateBuilder.New<Province>(true);
            if (!string.IsNullOrEmpty(name))
            {
                predicate = predicate.And(o => o.NameTH.Contains(name));
            }

            var results = await query.Where(predicate).OrderBy(o => o.NameTH).Take(100)
                    .Select(o => ProvinceListDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }

        public async Task<ProvincePaging> GetProvinceListAsync(ProvinceFilter filter, PageParam pageParam, ProvinceSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<ProvinceQueryResult> query = DB.Provinces.AsNoTracking().Select(o => new ProvinceQueryResult
            {
                Province = o,
                UpdatedBy = o.UpdatedBy
            });

            var predicate = PredicateBuilder.New<ProvinceQueryResult>(true);
            #region Filter
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                predicate = predicate.And(x => x.Province.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                predicate = predicate.And(x => x.Province.NameEN.Contains(filter.NameEN));
            }
            if (filter.IsShow != null)
            {
                predicate = predicate.And(x => x.Province.IsShow == filter.IsShow);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                predicate = predicate.And(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                predicate = predicate.And(x => x.Province.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                predicate = predicate.And(x => x.Province.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                predicate = predicate.And(x => x.Province.Updated >= filter.UpdatedFrom && x.Province.Updated <= filter.UpdatedTo);
            }
            #endregion

            query = query.Where(predicate);
            ProvinceDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<ProvinceQueryResult>(pageParam, ref query);

            var results = await query.Select(o => ProvinceDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new ProvincePaging()
            {
                PageOutput = pageOutput,
                Provinces = results
            };
        }

        public async Task<ProvinceDTO> GetProvincePostalCodeAsync(string postalCode, CancellationToken cancellationToken = default)
        {
            var model = await (from province in DB.Provinces
                               join district in DB.Districts on province.ID equals district.ProvinceID
                               join subdistrict in DB.SubDistricts on district.ID equals subdistrict.DistrictID
                               where district.PostalCode == postalCode || subdistrict.PostalCode == postalCode
                               select province
                               ).FirstOrDefaultAsync(cancellationToken);

            var result = ProvinceDTO.CreateFromModel(model);
            return result;
        }

        public async Task<ProvinceDTO> GetProvinceAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Provinces.Include(o => o.UpdatedBy).FirstOrDefaultAsync(o => o.ID == id, cancellationToken: cancellationToken);
            var result = ProvinceDTO.CreateFromModel(model);
            return result;
        }

        public async Task<ProvinceDTO> CreateProvinceAsync(ProvinceDTO input)
        {
            await input.ValidateAsync(DB, false);
            Province model = new Province();
            input.ToModel(ref model);

            await DB.Provinces.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await GetProvinceAsync(model.ID);
            return result;
        }

        public async Task<ProvinceDTO> UpdateProvinceAsync(Guid id, ProvinceDTO input)
        {
            await input.ValidateAsync(DB, true);
            var model = await DB.Provinces.FindAsync(id);
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await GetProvinceAsync(model.ID);
            return result;
        }

        public async Task DeleteProvinceAsync(Guid id)
        {
            var model = await DB.Provinces.FindAsync(id);
            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            // await DB.Provinces.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //   c.SetProperty(col => col.IsDeleted, true)
            //   .SetProperty(col => col.Updated, DateTime.Now)
            // );
        }
    }
}
