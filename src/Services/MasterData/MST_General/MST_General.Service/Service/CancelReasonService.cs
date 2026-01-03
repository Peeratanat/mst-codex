using Database.Models;
using Database.Models.MST;
using Base.DTOs.MST;
using MST_General.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using MST_General.Params.Filters;
using ErrorHandling;
using Common.Helper.Logging;

namespace MST_General.Services
{
    /// <summary>
    /// เหตุผลยกเลิกการจองหรือสัญญา
    /// CancelReason
    /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17484404/367792587/preview
    /// </summary>
    public class CancelReasonService : ICancelReasonService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        public CancelReasonService(DatabaseContext db)
        {
            logModel = new LogModel("CancelReasonService", null);
            this.DB = db;
        }
        public async Task<List<CancelReasonDropdownDTO>> GetCancelReasonDropdownListAsync(string name, CancellationToken cancellationToken = default)
        {
            IQueryable<CancelReason> query = DB.CancelReasons.AsNoTracking().Where(o => o.Key != "23");
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Description.Contains(name));
            }

            var results = await query.OrderBy(o => o.Key).Take(100)
                    .Select(o => CancelReasonDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }
        public async Task<CancelReasonPaging> GetCancelReasonListAsync(CancelReasonFilter filter, PageParam pageParam, CancelReasonSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<CancelReasonQueryResult> query = DB.CancelReasons.AsNoTracking().Select(o => new CancelReasonQueryResult()
            {
                CancelReason = o,
                GroupOfCancelReason = o.GroupOfCancelReason,
                CancelApproveFlow = o.CancelApproveFlow,
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.Key))
            {
                query = query.Where(o => o.CancelReason.Key.Contains(filter.Key));
            }
            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(o => o.CancelReason.Description.Contains(filter.Description));
            }
            if (!string.IsNullOrEmpty(filter.GroupOfCancelReasonKey))
            {
                var groupOfCancelReasonKeyMasterCenterID = await DB.MasterCenters.FirstAsync(o => o.Key == filter.GroupOfCancelReasonKey
                                                      && o.MasterCenterGroupKey == "GroupOfCancelReason");
                query = query.Where(o => o.GroupOfCancelReason.ID == groupOfCancelReasonKeyMasterCenterID.ID);
            }
            if (!string.IsNullOrEmpty(filter.CancelApproveFlowKey))
            {
                var cancelApproveFlowKeyMasterCenterID = await DB.MasterCenters.FirstAsync(o => o.Key == filter.CancelApproveFlowKey
                                                    && o.MasterCenterGroupKey == "CancelApproveFlow");
                query = query.Where(o => o.CancelApproveFlow.ID == cancelApproveFlowKeyMasterCenterID.ID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(o => o.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(o => o.CancelReason.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(o => o.CancelReason.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(o => o.CancelReason.Updated >= filter.UpdatedFrom && o.CancelReason.Updated <= filter.UpdatedTo);
            }
            #endregion

            CancelReasonDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<CancelReasonQueryResult>(pageParam, ref query);
            var results = await query.Select(o => CancelReasonDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);
            return new CancelReasonPaging()
            {
                PageOutput = pageOutput,
                CancelReasons = results
            };
        }
        public async Task<CancelReasonDTO> GetCancelReasonAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.CancelReasons.AsNoTracking()
                                            .Include(o => o.GroupOfCancelReason)
                                            .Include(o => o.CancelApproveFlow)
                                            .Include(o => o.UpdatedBy)
                                            .FirstAsync(o => o.ID == id, cancellationToken);
            var result = CancelReasonDTO.CreateFromModel(model);
            return result;
        }
        public async Task<CancelReasonDTO> CreateCancelReasonAsync(CancelReasonDTO input)
        {
            await input.ValidateAsync(DB);
            CancelReason model = new CancelReason();
            input.ToModel(ref model);
            await DB.CancelReasons.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetCancelReasonAsync(model.ID);
            return result;
        }
        public async Task<CancelReasonDTO> UpdateCancelReasonAsync(Guid id, CancelReasonDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.CancelReasons.FindAsync(id);
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetCancelReasonAsync(model.ID);
            return result;
        }
        public async Task DeleteCancelReasonAsync(Guid id)
        {
            var cancelMemos = await DB.CancelMemos.Where(o => o.CancelReasonID == id).ToListAsync();
            ValidateException ex = new ValidateException();
            if (cancelMemos.Count > 0)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0055").FirstOrDefaultAsync();
                var msg = errMsg.Message;
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            // await DB.CancelReasons.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //          c.SetProperty(col => col.IsDeleted, true)
            //          .SetProperty(col => col.Updated, DateTime.Now)
            //      );

            var model = await DB.CancelReasons.FindAsync(id);
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }
    }
}
