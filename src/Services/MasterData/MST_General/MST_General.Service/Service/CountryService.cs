using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class CountryService : ICountryService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public CountryService(DatabaseContext db)
        {
            logModel = new LogModel("SubBGService", null);
            this.DB = db;
        }

        public async Task<List<CountryDTO>> GetCountryDropdownListAsync(CountryFilter filter, CancellationToken cancellationToken = default)
        {
            var query = DB.Countries.AsNoTracking().Where(o => o.NameEN != "Thailand");
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(o => o.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(o => o.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(o => o.Code.Contains(filter.Code));
            }

            var queryResults = query.OrderBy(o => o.NameTH).Take(99);
            var onlyThai = DB.Countries.Where(o => o.NameEN.Contains("Thailand"));

            var mixQuery = onlyThai.Concat(queryResults);
            var results = await mixQuery.Select(o => CountryDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }

        public async Task<CountryPaging> GetCountryListAsync(CountryFilter filter, PageParam pageParam, CountrySortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<CountryQueryResult> query = DB.Countries.AsNoTracking().Select(o => new CountryQueryResult
            {
                Country = o,
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(o => o.Country.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(o => o.Country.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(o => o.Country.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(o => o.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(o => o.Country.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(o => o.Country.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(o => o.Country.Updated >= filter.UpdatedFrom && o.Country.Updated <= filter.UpdatedTo);
            }
            #endregion

            CountryDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging(pageParam, ref query);
            var results = await query.Select(o => CountryDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new CountryPaging()
            {
                PageOutput = pageOutput,
                Countries = results
            };
        }

        public async Task<CountryDTO> GetCountryAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Countries.AsNoTracking()
            .Include(o => o.UpdatedBy)
            .FirstOrDefaultAsync(o => o.ID == id, cancellationToken: cancellationToken);
            var result = CountryDTO.CreateFromModel(model);
            return result;

        }

        public async Task<CountryDTO> FindCountryAsync(string code, CancellationToken cancellationToken = default)
        {
            var model = await DB.Countries.AsNoTracking()
                .Include(o => o.UpdatedBy)
                .FirstOrDefaultAsync(o => o.Code == code, cancellationToken: cancellationToken);
            var result = CountryDTO.CreateFromModel(model);
            return result;
        }

        public async Task<CountryDTO> CreateCountryAsync(CountryDTO input)
        {
            await input.ValidateAsync(DB);
            Country model = new Country();
            input.ToModel(ref model);
            await DB.Countries.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetCountryAsync(model.ID);
            return result;
        }

        public async Task<CountryDTO> UpdateCountryAsync(Guid id, CountryDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.Countries.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetCountryAsync(model.ID);
            return result;
        }

        public async Task DeleteCountryAsync(Guid id)
        {
            // await DB.Countries.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //     c.SetProperty(col => col.IsDeleted, true)
            //     .SetProperty(col => col.Updated, DateTime.Now)
            // );

            var model = await DB.Countries.FindAsync(id);
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

        }
    }
}
