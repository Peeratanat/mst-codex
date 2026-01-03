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
using MST_Finacc.Params.Outputs;
using PagingExtensions;
using Base.DTOs;
using Common.Helper.Logging;

namespace MST_Finacc.Services
{
    public class BankService : IBankService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public BankService(DatabaseContext db)
        {
            logModel = new LogModel("BankService", null);
            this.DB = db;
        }

        public async Task<List<BankDropdownDTO>> GetBankDropdownListAsync(string name, bool? IsCreditCard, CancellationToken cancellationToken = default)
        {
            IQueryable<Bank> query = DB.Banks;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }
            if (IsCreditCard ?? false)
            {
                query = query.Where(o => o.IsCreditCard == true);
            }
            query = query.Where(o => !o.BankNo.Equals("102"));
            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync(cancellationToken);

            var results = queryResults.Select(o => BankDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<BankPaging> GetBankListAsync(BankFilter filter, PageParam pageParam, BankSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<BankQueryResult> query = DB.Banks.AsNoTracking()
                                                  .Select(o => new BankQueryResult()
                                                  {
                                                      Bank = o,
                                                      UpdatedBy = o.UpdatedBy
                                                  });

            #region Filter
            if (!string.IsNullOrEmpty(filter.BankNo))
            {
                query = query.Where(x => x.Bank.BankNo.Contains(filter.BankNo));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.Bank.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.Bank.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.Alias))
            {
                query = query.Where(x => x.Bank.Alias.Contains(filter.Alias));
            }
            if (filter.IsCreditCard != null)
            {
                query = query.Where(x => x.Bank.IsCreditCard == filter.IsCreditCard);
            }
            if (filter.IsNonBank != null)
            {
                query = query.Where(x => x.Bank.IsNonBank == filter.IsNonBank);
            }
            if (filter.IsCoorperative != null)
            {
                query = query.Where(x => x.Bank.IsCoorperative == filter.IsCoorperative);
            }
            if (filter.IsFreeMortgage != null)
            {
                query = query.Where(x => x.Bank.IsFreeMortgage == filter.IsFreeMortgage);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Bank.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Bank.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Bank.Updated >= filter.UpdatedFrom && x.Bank.Updated <= filter.UpdatedTo);
            }
            if (!string.IsNullOrEmpty(filter.SwiftCode))
            {
                query = query.Where(x => x.Bank.SwiftCode.Contains(filter.SwiftCode));
            }
            query = query.Where(x => !x.Bank.BankNo.Equals("102"));
            #endregion

            BankDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<BankQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => BankDTO.CreateFromQueryResult(o)).ToList();

            return new BankPaging()
            {
                PageOutput = pageOutput,
                Banks = results
            };
        }

        public async Task<BankDTO> GetBankAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Banks.Include(o => o.UpdatedBy).FirstAsync(o => o.ID == id, cancellationToken);
            var result = BankDTO.CreateFromModel(model);
            return result;
        }

        public async Task<BankDTO> CreateBankAsync(BankDTO input)
        {
            await input.ValidateAsync(DB);

            Bank model = new Bank();
            input.ToModel(ref model);

            await DB.Banks.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = BankDTO.CreateFromModel(model);
            return result;
        }

        public async Task<BankDTO> UpdateBankAsync(Guid id, BankDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.Banks.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = BankDTO.CreateFromModel(model);
            return result;
        }

        public async Task<Bank> DeleteBankAsync(Guid id)
        {
            var model = await DB.Banks.FindAsync(id);
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<List<BankDropdownDTO>> GetBankOnlyDropdownListAsync(string name, CancellationToken cancellationToken = default)
        {
            IQueryable<Bank> query = DB.Banks.Where(o => o.IsNonBank == false && (o.BankNo == "002" || o.BankNo == "025" || o.BankNo == "004" || o.BankNo == "014" || o.BankNo == "030"));
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }

            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync(cancellationToken);

            var results = queryResults.Select(o => BankDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }
    }
}
