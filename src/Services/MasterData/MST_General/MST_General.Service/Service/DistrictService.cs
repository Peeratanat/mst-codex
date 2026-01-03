using Database.Models;
using Database.Models.MST;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MST_General.Params.Outputs;
using PagingExtensions;
using Base.DTOs;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }

        public DistrictService(DatabaseContext db)
        {
            logModel = new LogModel("DistrictService", null);
            DB = db;
        }

        public async Task<DistrictListDTO> FindDistrictAsync(Guid provinceID, string name, CancellationToken cancellationToken = default)
        {
            var model = await DB.Districts.FirstOrDefaultAsync(o => o.ProvinceID == provinceID && (o.NameTH == name || o.NameEN == name), cancellationToken);
            return DistrictListDTO.CreateFromModel(model);
        }

        public async Task<List<DistrictListDTO>> GetDistrictDropdownListAsync(string name, Guid? provinceID, CancellationToken cancellationToken = default)
        {
            IQueryable<District> query = DB.Districts.AsNoTracking();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }
            if (provinceID != null)
            {
                query = query.Where(o => o.ProvinceID == provinceID);
            }

            var results = await query.OrderBy(o => o.NameTH).Take(100)
                    .Select(o => DistrictListDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
            return results;
        }

        public async Task<DistrictPaging> GetDistrictListAsync(DistrictFilter filter, PageParam pageParam, DistrictSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<DistrictQueryResult> query = DB.Districts.AsNoTracking().Include(o => o.SubDistricts).Select(o => new DistrictQueryResult
            {
                District = o,
                Province = o.Province,
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.District.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.District.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.PostalCode))
            {
                query = query.Where(x => x.District.SubDistricts.Any(o => o.PostalCode.Contains(filter.PostalCode)));
            }
            if (filter.ProvinceID != Guid.Empty && filter.ProvinceID != null)
            {
                query = query.Where(x => x.District.ProvinceID == filter.ProvinceID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.District.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.District.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.District.Updated >= filter.UpdatedFrom && x.District.Updated <= filter.UpdatedTo);
            }
            #endregion

            DistrictDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<DistrictQueryResult>(pageParam, ref query);

            var results = await query.Select(o => DistrictDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new DistrictPaging()
            {
                PageOutput = pageOutput,
                Districts = results
            };
        }

        public async Task<DistrictDTO> GetDistrictAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Districts.AsNoTracking()
                                          .Include(o => o.Province)
                                          .Include(o => o.UpdatedBy)
                                          .FirstOrDefaultAsync(f => f.ID == id, cancellationToken);
            var result = DistrictDTO.CreateFromModel(model);
            return result;
        }

        public async Task<DistrictDTO> CreateDistrictAsync(DistrictDTO input)
        {
            await input.ValidateAsync(DB, false);
            District model = new District();
            input.ToModel(ref model);

            await DB.Districts.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await GetDistrictAsync(model.ID);
            return result;
        }

        public async Task<DistrictDTO> UpdateDistrictAsync(Guid id, DistrictDTO input)
        {
            await input.ValidateAsync(DB, true);
            var model = await DB.Districts.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await GetDistrictAsync(model.ID);
            return result;
        }

        public async Task DeleteDistrictAsync(Guid id)
        {
            // await DB.Districts.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //     c.SetProperty(col => col.IsDeleted, true)
            //     .SetProperty(col => col.Updated, DateTime.Now)
            // );

            var model = await DB.Districts.FindAsync(id);
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
        }
    }
}
