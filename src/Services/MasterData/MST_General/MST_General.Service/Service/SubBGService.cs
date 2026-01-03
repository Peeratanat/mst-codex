using Database.Models;
using Database.Models.MST;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;
using Base.DTOs;
using MST_General.Params.Outputs;
using MST_General.Services;
using MST_General.Params.Filters;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class SubBGService : ISubBGService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public SubBGService(DatabaseContext db)
        {
            logModel = new LogModel("SubBGService", null);
            DB = db;
        }

        public async Task<List<SubBGDropdownDTO>> GetSubBGDropdownListAsync(string name, Guid? bGID, CancellationToken cancellationToken = default)
        {
            IQueryable<SubBG> query = DB.SubBGs.AsNoTracking();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }
            if (bGID != null)
            {
                query = query.Where(o => o.BGID == bGID);
            }

            var results = await query.OrderBy(o => o.Name).Take(100)
            .Select(o => SubBGDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }

        public async Task<SubBGPaging> GetSubBGListAsync(SubBGFilter request, PageParam pageParam, SubBGSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<SubBGQueryResult> query = DB.SubBGs.AsNoTracking().Select(o => new SubBGQueryResult
            {
                SubBG = o,
                BG = o.BG,
                UpdatedBy = o.UpdatedBy
            });

            #region filter
            if (!string.IsNullOrEmpty(request.SubBGNo))
            {
                query = query.Where(x => x.SubBG.SubBGNo.Contains(request.SubBGNo));
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.SubBG.Name.Contains(request.Name));
            }
            if (request.BgID != null && request.BgID != Guid.Empty)
            {
                query = query.Where(x => x.SubBG.BGID == request.BgID);
            }
            if (!string.IsNullOrEmpty(request.UpdatedBy))
            {
                query = query.Where(x => x.SubBG.UpdatedBy.DisplayName.Contains(request.UpdatedBy));
            }
            if (request.UpdatedFrom != null)
            {
                query = query.Where(x => x.SubBG.Updated >= request.UpdatedFrom);
            }
            if (request.UpdatedTo != null)
            {
                query = query.Where(x => x.SubBG.Updated <= request.UpdatedTo);
            }
            if (request.UpdatedFrom != null && request.UpdatedTo != null)
            {
                query = query.Where(x => x.SubBG.Updated >= request.UpdatedFrom && x.SubBG.Updated <= request.UpdatedTo);
            }
            #endregion

            SubBGDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<SubBGQueryResult>(pageParam, ref query);

            var results = await query.Select(o => SubBGDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new SubBGPaging()
            {
                PageOutput = pageOutput,
                SubBGs = results
            };
        }

        public async Task<SubBGDTO> GetSubBGAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.SubBGs.AsNoTracking()
                                       .Include(o => o.BG)
                                       .Include(o => o.UpdatedBy)
                                       .FirstOrDefaultAsync(o => o.ID == id, cancellationToken);
            var result = SubBGDTO.CreateFromModel(model);
            return result;
        }

        public async Task<SubBGDTO> CreateSubBGAsync(SubBGDTO input)
        {
            await input.ValidateAsync(DB);
            SubBG model = new SubBG();
            input.ToModel(ref model);

            await DB.SubBGs.AddAsync(model);
            await DB.SaveChangesAsync();


            var result = await GetSubBGAsync(model.ID);
            return result;
        }

        public async Task<SubBGDTO> UpdateSubBGAsync(Guid id, SubBGDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.SubBGs.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await GetSubBGAsync(model.ID);
            return result;
        }

        public async Task DeleteSubBGAsync(Guid id)
        {
            var model = await DB.SubBGs.FindAsync(id);
            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            //     await DB.SubBGs.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //        c.SetProperty(col => col.IsDeleted, true)
            //        .SetProperty(col => col.Updated, DateTime.Now)
            //    );
        }
    }
}
