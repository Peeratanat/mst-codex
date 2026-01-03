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
using PagingExtensions;
using MST_General.Params.Outputs;
using Base.DTOs;
using Database.Models.DbQueries;
using Database.Models.DbQueries.MST;
using ErrorHandling;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class MasterCenterService : IMasterCenterService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public MasterCenterService(DatabaseContext db)
        {
            logModel = new LogModel("MasterCenterGroupService", null);
            DB = db;
        }

        public async Task<List<MasterCenterDropdownDTO>> GetMasterCenterDropdownListAsync(string masterCenterGroupKey, string name, string bg,string brandId, CancellationToken cancellationToken = default)
        {
            IQueryable<MasterCenter> query = DB.MasterCenters.AsNoTracking();
            if (!string.IsNullOrEmpty(masterCenterGroupKey))
            {
                query = query.Where(o => o.MasterCenterGroupKey == masterCenterGroupKey);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(bg))
            {
                query = query.Where(o => o.RefMigrateID3.Contains(bg));
            }
            
            if (masterCenterGroupKey == "LeadType")
            {
                query = query.Where(o => o.RefMigrateID1 == null);
            }


            query = query.OrderBy(o => o.MasterCenterGroupKey).ThenBy(o => o.Order);

            var queryResults = query.Where(o => o.IsActive).OrderBy(o => o.Order);

            var results = new List<MasterCenterDropdownDTO>();

            if (masterCenterGroupKey == "LeadType")
            {
                results = await queryResults.Select(o => MasterCenterDropdownDTO.CreateMasterLeadTypeFromModel(o)).ToListAsync(cancellationToken); ;
            }
            else
            {
                results = await queryResults.Select(o => MasterCenterDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken); ;
            }



            if (brandId != null)
            {
                var LeadTypeFollowBrand = await DB.MasterCenters
                    .Where(o => o.MasterCenterGroupKey == masterCenterGroupKey && o.RefMigrateID1.Contains(brandId))
                    .ToListAsync(cancellationToken);

                var leadTypeDtos = masterCenterGroupKey == "LeadType"
                    ? LeadTypeFollowBrand.Select(o => MasterCenterDropdownDTO.CreateMasterLeadTypeFromModel(o)).ToList()
                    : LeadTypeFollowBrand.Select(o => MasterCenterDropdownDTO.CreateFromModel(o)).ToList();

                results.AddRange(leadTypeDtos);
            }




            return results;
        }

        public async Task<MasterCenterPaging> GetMasterCenterListAsync(MasterCenterFilter filter, PageParam pageParam, MasterCenterSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<MasterCenterQueryResult> query = DB.MasterCenters.AsNoTracking()
                                                          .Select(o => new MasterCenterQueryResult
                                                          {
                                                              MasterCenter = o,
                                                              MasterCenterGroup = o.MasterCenterGroup,
                                                              UpdatedBy = o.UpdatedBy
                                                          });

            #region Filter
            if (!string.IsNullOrEmpty(filter.MasterCenterGroupKey))
            {
                query = query.Where(x => x.MasterCenter.MasterCenterGroupKey == filter.MasterCenterGroupKey);
            }
            if (!string.IsNullOrEmpty(filter.Key))
            {
                query = query.Where(x => x.MasterCenter.Key.Contains(filter.Key));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.MasterCenter.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.MasterCenter.NameEN.Contains(filter.NameEN));
            }
            if (filter.Order != null)
            {
                query = query.Where(x => x.MasterCenter.Order == filter.Order);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.MasterCenter.IsActive == filter.IsActive);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.MasterCenter.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterCenter.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterCenter.Updated >= filter.UpdatedFrom && x.MasterCenter.Updated <= filter.UpdatedTo);
            }
            #endregion

            MasterCenterDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<MasterCenterQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => MasterCenterDTO.CreateFromQueryResult(o)).ToList();

            return new MasterCenterPaging()
            {
                PageOutput = pageOutput,
                MasterCenters = results
            };
        }

        public async Task<MasterCenterDropdownDTO> GetFindMasterCenterDropdownItemAsync(string masterCenterGroupKey, string key, CancellationToken cancellationToken = default)
        {
            var model = await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == masterCenterGroupKey && o.Key == key, cancellationToken);
            var result = MasterCenterDropdownDTO.CreateFromModel(model);
            return result;
        }

        public async Task<MasterCenterDTO> GetMasterCenterAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.MasterCenters.AsNoTracking()
                                              .Include(o => o.MasterCenterGroup)
                                              .Include(o => o.UpdatedBy)
                                              .FirstAsync(o => o.ID == id, cancellationToken);
            var result = MasterCenterDTO.CreateFromModel(model);
            return result;
        }

        public async Task<MasterCenterDTO> CreateMasterCenterAsync(MasterCenterDTO input)
        {
            await input.ValidateAsync(DB);

            MasterCenter model = new MasterCenter();
            input.ToModel(ref model);
            await DB.MasterCenters.AddAsync(model);
            await DB.SaveChangesAsync();


            var result = await GetMasterCenterAsync(model.ID);
            return result;
        }

        public async Task<MasterCenterDTO> UpdateMasterCenterAsync(Guid id, MasterCenterDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.MasterCenters.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await GetMasterCenterAsync(model.ID);
            return result;
        }

        public async Task DeleteMasterCenterAsync(Guid id)
        {
            var model = await DB.Projects.Where(o => o.ProjectTypeMasterCenterID == id).ToListAsync();

            ValidateException ex = new ValidateException();
            if (model.Count > 0)
            {
                var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR0055");
                var msg = errMsg.Message;
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            var masterCenters = await DB.MasterCenters.FindAsync(id);
            masterCenters.IsDeleted = true;
            await DB.SaveChangesAsync();
            //     await DB.MasterCenters.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //        c.SetProperty(col => col.IsDeleted, true)
            //        .SetProperty(col => col.Updated, DateTime.Now)
            //    );
        }




        //Dropdown BankBeanchBOT
        public async Task<List<BankBranchBOTDropdownDTO>> GetBankBranchDropdownAsync(string bankCode, string bankBranchName, CancellationToken cancellationToken = default)
        {
            IQueryable<BankBranchBOT> query = DB.BankBranchBOTs.AsNoTracking();
            if (!string.IsNullOrEmpty(bankCode))
            {
                query = query.Where(o => o.BankCode == bankCode);
            }
            if (!string.IsNullOrEmpty(bankBranchName))
            {
                query = query.Where(o => o.BankBranchName.Contains(bankBranchName));
            }

            query = query.OrderBy(o => o.BankCode).ThenBy(o => o.BankCode);

            var queryResults = await query.OrderBy(o => o.BankBranchName).ToListAsync(cancellationToken);

            var results = queryResults.Select(o => BankBranchBOTDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }


        public async Task<List<MasterCenterDropdownDTO>> GetLGMasterCenterDropdownListAsync(string masterCenterGroupKey, string name, decimal countNumber, CancellationToken cancellationToken = default)
        {
            IQueryable<MasterCenter> query = DB.MasterCenters.AsNoTracking();
            if (!string.IsNullOrEmpty(masterCenterGroupKey))
            {
                query = query.Where(o => o.MasterCenterGroupKey == masterCenterGroupKey);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }

            query = query.OrderBy(o => o.MasterCenterGroupKey).ThenBy(o => o.Order);

            var queryResults = await query.Where(o => o.IsActive).OrderBy(o => o.Order).ToListAsync(cancellationToken);

            var results = new List<MasterCenterDropdownDTO>();

            if (masterCenterGroupKey == "LeadType")
            {
                results = queryResults.Select(o => MasterCenterDropdownDTO.CreateMasterLeadTypeFromModel(o)).ToList();
            }
            else
            {
                results = queryResults.Select(o => MasterCenterDropdownDTO.CreateFromModel(o)).ToList();
            }


            return results;
        }
    }
}
