using Database.Models;
using Database.Models.MST;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using MST_General.Params.Outputs;
using PagingExtensions;
using ErrorHandling;
using System.Reflection;
using System.ComponentModel;
using LinqKit;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class BGService : IBGService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public BGService(DatabaseContext db)
        {
            logModel = new LogModel("BGService", null);
            DB = db;
        }

        public async Task<List<BGDropdownDTO>> GetBGDropdownListAsync(string? productTypeKey, string? name, CancellationToken cancellationToken = default)
        {
            IQueryable<BG> query = DB.BGs.AsNoTracking();
            var predicate = PredicateBuilder.New<BG>(true);
            if (!string.IsNullOrEmpty(productTypeKey))
            {
                var productTypeMasterCenter = await DB.MasterCenters.AsNoTracking().FirstAsync(x => x.Key == productTypeKey
                                                                       && x.MasterCenterGroupKey == "ProductType", cancellationToken: cancellationToken);
                predicate = predicate.And(o => o.ProductTypeMasterCenterID == productTypeMasterCenter.ID);
            }
            if (!string.IsNullOrEmpty(name))
            {
                predicate = predicate.And(o => o.Name.Contains(name));
            }

            return await query.Where(predicate).OrderBy(o => o.Name).Take(100)
            .Select(o => BGDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
        }

        public async Task<BGPaging> GetBGListAsync(BGFilter filter, PageParam pageParam, BGSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<BGQueryResult> query = DB.BGs.AsNoTracking()
                                                .Select(o => new BGQueryResult
                                                {
                                                    BG = o,
                                                    ProductType = o.ProductType,
                                                    UpdatedBy = o.UpdatedBy
                                                });

            var predicate = PredicateBuilder.New<BGQueryResult>(true);
            #region filter
            if (!string.IsNullOrEmpty(filter.BgNo))
            {
                predicate = predicate.And(x => x.BG.BGNo.Contains(filter.BgNo));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                predicate = predicate.And(x => x.BG.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter.ProductTypeKey))
            {
                var productTypeMasterCenter = await DB.MasterCenters.AsNoTracking().FirstAsync(x => x.Key == filter.ProductTypeKey
                                                                       && x.MasterCenterGroupKey == "ProductType", cancellationToken: cancellationToken);
                predicate = predicate.And(o => o.BG.ProductTypeMasterCenterID == productTypeMasterCenter.ID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                predicate = predicate.And(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                predicate = predicate.And(x => x.BG.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                predicate = predicate.And(x => x.BG.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                predicate = predicate.And(x => x.BG.Updated >= filter.UpdatedFrom && x.BG.Updated <= filter.UpdatedTo);
            }
            #endregion

            query = query.Where(predicate);
            BGDTO.SortBy(sortByParam, ref query);

            var pageOuput = PagingHelper.Paging(pageParam, ref query);
            var results = await query.Select(o => BGDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new()
            {
                BGs = results,
                PageOutput = pageOuput
            };
        }

        public async Task<BGDTO> GetBGAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.BGs
                .AsNoTracking()
                .Include(o => o.ProductType)
                .Include(o => o.UpdatedBy)
                .FirstAsync(o => o.ID == id, cancellationToken: cancellationToken);
            var result = BGDTO.CreateFromModel(model);
            return result;
        }

        public async Task<BGDTO> CreateBGAsync(BGDTO input)
        {
            await input.ValidateAsync(DB);

            BG model = new BG();
            input.ToModel(ref model);

            await DB.BGs.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await GetBGAsync(model.ID);
            return result;
        }

        public async Task<BGDTO> UpdateBGAsync(Guid id, BGDTO input)
        {
            await input.ValidateUpdateAsync(DB);
            var model = await DB.BGs.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await GetBGAsync(model.ID);
            return result;
        }

        public async Task DeleteBGAsync(Guid id)
        {
            #region Validate Check Used Data
            ValidateException ex = new ValidateException();
            var bgUsed = await DB.Projects.FirstOrDefaultAsync(o => o.BGID == id);
            if (bgUsed != null)
            {
                var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR0146");
                string desc = typeof(BrandDTO).GetProperty(nameof(BrandDTO.BrandNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
            #endregion


            var model = await DB.BGs.FindAsync(id);
            model.IsDeleted = true;

            var modelSB = await DB.SubBGs.Where(o => o.BGID == id).ToListAsync();
            modelSB?.ForEach(f => f.IsDeleted = true);

            await DB.SaveChangesAsync();

            // await DB.BGs.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //     c.SetProperty(col => col.IsDeleted, true)
            //     .SetProperty(col => col.Updated, DateTime.Now)
            // );

            // await DB.SubBGs.Where(o => o.BGID == id).ExecuteUpdateAsync(c =>
            //   c.SetProperty(col => col.IsDeleted, true)
            //   .SetProperty(col => col.Updated, DateTime.Now)
            // );
        }


    }
}
