using Database.Models;
using Database.Models.MST;
using MST_Finacc.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;
using MST_Finacc.Params.Outputs;
using Base.DTOs;
using Common.Helper.Logging;

namespace MST_Finacc.Services
{
    public class BankBranchBOTService : IBankBranchBOTService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }

        public BankBranchBOTService(DatabaseContext db)
        {
            logModel = new LogModel("BankBranchBOTService", null);
            DB = db;
        }

        public async Task<BankBranchBOTPaging> GetBankBranchBOTListAsync(BankBranchBOTFilter filter, PageParam pageParam, BankBranchBOTSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<BankBranchBOTQueryResult> query = DB.BankBranchBOTs
                                             .Select(o => new BankBranchBOTQueryResult()
                                             {
                                                 Bank = o.Bank,
                                                 BankBranchBOT = o,
                                                 UpdatedBy = o.UpdatedBy
                                             });

            #region Filter
            if (!string.IsNullOrEmpty(filter.BankBranchName))
            {
                query = query.Where(x => x.BankBranchBOT.BankBranchName.Contains(filter.BankBranchName));
            }
            if (!string.IsNullOrEmpty(filter.BankBranchCode))
            {
                query = query.Where(x => x.BankBranchBOT.BankBranchCode.Contains(filter.BankBranchCode));
            }
            if (filter.BankId != null)
            {
                query = query.Where(x => x.BankBranchBOT.BankID == filter.BankId);
            }
            #endregion

            BankBranchBOTDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<BankBranchBOTQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => BankBranchBOTDTO.CreateFromQueryResult(o)).ToList();

            return new BankBranchBOTPaging()
            {
                PageOutput = pageOutput,
                BankBrancheBOTs = results
            };
        }
        public async Task<BankBranchBOTDTO> GetBankBranchBOTAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.BankBranchBOTs.Where(o => o.ID == id)
                                            .Include(o => o.Bank)
                                            .Include(o => o.UpdatedBy)
                                            .FirstOrDefaultAsync(cancellationToken);

            var result = BankBranchBOTDTO.CreateFromModel(model);
            return result;
        }

        public async Task<BankBranchBOTDTO> CreateBankBranchBOTAsync(BankBranchBOTDTO input)
        {
            await input.ValidateAsync(DB,true);
            var bank = await DB.Banks.FirstOrDefaultAsync(o => o.ID == input.Bank.Id);

            BankBranchBOT model = new BankBranchBOT();
            model.BankCode = bank.BankNo;
            input.ToModel(ref model);

            await DB.BankBranchBOTs.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await GetBankBranchBOTAsync(model.ID);
            return result;
        }

        public async Task<BankBranchBOTDTO> UpdateBankBranchBOTAsync(Guid id, BankBranchBOTDTO input)
        {
            await input.ValidateAsync(DB,false);

            var model = await DB.BankBranchBOTs.FindAsync(id);
            if (model is not null)
            {
                input.ToModel(ref model);
                DB.Entry(model).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }
            var result = await GetBankBranchBOTAsync(id);
            return result;
        }

        public async Task<BankBranchBOT> DeleteBankBranchBOTAsync(Guid id)
        {

            var model = await DB.BankBranchBOTs.FindAsync(id);
            if (model is not null)
            {
                model.IsDeleted = true;
                await DB.SaveChangesAsync();
            }
            return model;
        }
    }
}
