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
    public class MasterCenterGroupService : IMasterCenterGroupService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public MasterCenterGroupService(DatabaseContext db)
        {
            logModel = new LogModel("MasterCenterGroupService", null);
            DB = db;
        }

        public async Task<MasterCenterGroupPaging> GetMasterCenterGroupListAsync(MasterCenterGroupFilter request, PageParam pageParam, MasterCenterGroupSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<MasterCenterGroupQueryResult> query = DB.MasterCenterGroups.AsNoTracking()
                                                               .Select(o => new MasterCenterGroupQueryResult
                                                               {
                                                                   MasterCenterGroup = o,
                                                                   UpdatedBy = o.UpdatedBy
                                                               });

            #region Filter
            if (!string.IsNullOrEmpty(request.Key))
            {
                query = query.Where(x => x.MasterCenterGroup.Key.Contains(request.Key));
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.MasterCenterGroup.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(request.UpdatedBy));
            }
            if (request.UpdatedFrom != null)
            {
                query = query.Where(x => x.MasterCenterGroup.Updated >= request.UpdatedFrom);
            }
            if (request.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterCenterGroup.Updated <= request.UpdatedTo);
            }
            if (request.UpdatedFrom != null && request.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterCenterGroup.Updated >= request.UpdatedFrom && x.MasterCenterGroup.Updated <= request.UpdatedTo);
            }
            #endregion

            MasterCenterGroupDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<MasterCenterGroupQueryResult>(pageParam, ref query);

            var results = await query.Select(o => MasterCenterGroupDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new MasterCenterGroupPaging()
            {
                MasterCenterGroups = results,
                PageOutput = pageOutput
            };
        }

        public async Task<MasterCenterGroupDTO> GetMasterCenterGroupAsync(string key, CancellationToken cancellationToken = default)
        {
            var model = await DB.MasterCenterGroups.AsNoTracking().Include(o => o.UpdatedBy).FirstOrDefaultAsync(o => o.Key == key, cancellationToken);
            var result = MasterCenterGroupDTO.CreateFromModel(model);
            return result;
        }

        public async Task<MasterCenterGroupDTO> CreateMasterCenterGroupAsync(MasterCenterGroupDTO input)
        {
            MasterCenterGroup model = new MasterCenterGroup();
            input.ToModel(ref model);
            await DB.MasterCenterGroups.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await GetMasterCenterGroupAsync(model.Key);
            return result;
        }

        public async Task<MasterCenterGroupDTO> UpdateMasterCenterGroupAsync(string key, MasterCenterGroupDTO input)
        {
            var model = await DB.MasterCenterGroups.FirstOrDefaultAsync(o => o.Key == key);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await GetMasterCenterGroupAsync(model.Key);
            return result;
        }

        public async Task DeleteMasterCenterGroupAsync(string key)
        {
            var model = await DB.MasterCenterGroups.FirstOrDefaultAsync(o => o.Key == key);
            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            // await DB.MasterCenterGroups.Where(o => o.Key == key).ExecuteUpdateAsync(c =>
            //    c.SetProperty(col => col.IsDeleted, true)
            //    .SetProperty(col => col.Updated, DateTime.Now)
            // );
        }
    }
}
