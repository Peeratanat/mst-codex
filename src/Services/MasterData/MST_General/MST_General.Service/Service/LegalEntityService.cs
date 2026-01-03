using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class LegalEntityService : ILegalEntityService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public LegalEntityService(DatabaseContext db)
        {
            logModel = new LogModel("LegalEntityService", null);
            DB = db;
        }
        public async Task<List<LegalEntityDropdownDTO>> GetLegalEntityDropdownListAsync(string name, CancellationToken cancellationToken = default)
        {
            IQueryable<LegalEntity> query = DB.LegalEntities.AsNoTracking();
            var predicate = PredicateBuilder.New<LegalEntity>(true);

            #region Filter
            if (!string.IsNullOrEmpty(name))
            {
                predicate = predicate.And(x => x.NameTH.Contains(name));
            }
            #endregion
            return await query.Select(o => LegalEntityDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
        }

        public async Task<LegalEntityPaging> GetLegalEntityListAsync(LegalFilter filter, PageParam pageParam, LegalEntitySortByParam sortByParam, CancellationToken cancellationToken = default)
        {


            IQueryable<LegalEntityQueryResult> query = from o in DB.LegalEntities.AsNoTracking()
                                                       join ag in DB.AgreementConfigs.AsNoTracking() on o.ID equals ag.LegalEntityID into agData
                                                       from agModel in agData.DefaultIfEmpty()

                                                       join prj in DB.Projects.AsNoTracking() on agModel.ProjectID equals prj.ID into prjData
                                                       from prjModel in prjData.DefaultIfEmpty()
                                                       select new LegalEntityQueryResult
                                                       {
                                                           LegalEntity = o,
                                                           Bank = o.Bank,
                                                           BankAccountType = o.BankAccountType,
                                                           UpdatedBy = o.UpdatedBy,
                                                           Project = prjModel
                                                       };

            #region filter
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.LegalEntity.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.LegalEntity.NameEN.Contains(filter.NameEN));
            }
            if (filter.BankID != null & filter.BankID != Guid.Empty)
            {
                query = query.Where(x => x.LegalEntity.BankID == filter.BankID);
            }
            if (!string.IsNullOrEmpty(filter.BankAccountTypeKey))
            {
                var bankAccountTypeID = await (DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BankAccountType && o.Key == filter.BankAccountTypeKey).Select(o => o.ID)).FirstOrDefaultAsync();
                query = query.Where(o => o.LegalEntity.BankAccountTypeMasterCenterID == bankAccountTypeID);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.LegalEntity.IsActive == filter.IsActive);
            }
            if (!string.IsNullOrEmpty(filter.BankAccountNo))
            {
                query = query.Where(x => x.LegalEntity.BankAccountNo.Contains(filter.BankAccountNo.Replace("-", string.Empty)));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.LegalEntity.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LegalEntity.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LegalEntity.Updated >= filter.UpdatedFrom && x.LegalEntity.Updated <= filter.UpdatedTo);
            }
            if (!string.IsNullOrEmpty(filter.VendorCode))
            {
                query = query.Where(x => x.LegalEntity.AgentCode.Contains(filter.VendorCode));
            }
            if (filter.ProjectID != null)
            {
                query = query.Where(o => o.Project.ID == filter.ProjectID);
            }
            #endregion

            LegalEntityDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<LegalEntityQueryResult>(pageParam, ref query);

            var results = await query
            .Select(o => LegalEntityDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new LegalEntityPaging()
            {
                LegalEntities = results,
                PageOutput = pageOutput
            };
        }

        public async Task<LegalEntityDTO> GetLegalEntityAsync(Guid id, Guid? projectID, CancellationToken cancellationToken = default)
        {
            var model = await DB.LegalEntities.AsNoTracking()
                                              .Include(o => o.Bank)
                                              .Include(o => o.UpdatedBy)
                                              .Include(o => o.BankAccountType)
                                              .FirstOrDefaultAsync(o => o.ID == id, cancellationToken);
            var result = LegalEntityDTO.CreateFromModel(model);
            var modelProject = await DB.AgreementConfigs.AsNoTracking()
                                              .Include(o => o.Project)
                                              .FirstOrDefaultAsync(o => o.LegalEntityID == id && o.ProjectID == projectID, cancellationToken);
            if (modelProject != null)
            {
                result.Project = ProjectDropdownDTO.CreateFromModel(modelProject.Project);
            }
            return result;
        }

        public async Task<LegalEntityDTO> CreateLegalEntityAsync(LegalEntityDTO input)
        {
            await input.ValidateAsync(DB);
            LegalEntity model = new LegalEntity();
            input.ToModel(ref model);

            await DB.LegalEntities.AddAsync(model);
            await DB.SaveChangesAsync();
            //get All AgreementConfigs use Legal 
            if (input.Project?.Id != null && input.Project?.Id != Guid.Empty)
            {
                var modelAgreementConfig = await DB.AgreementConfigs.FirstOrDefaultAsync(x => x.ProjectID == input.Project.Id);
                if (modelAgreementConfig != null)
                {
                    modelAgreementConfig.LegalEntityID = model.ID;
                    DB.Entry(modelAgreementConfig).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            var result = await GetLegalEntityAsync(model.ID, input?.Project?.Id);
            return result;
        }

        public async Task<LegalEntityDTO> UpdateLegalEntityAsync(Guid id, LegalEntityDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.LegalEntities.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;

            //get All AgreementConfigs use Legal
            var AgreementConfigList = await DB.AgreementConfigs.Where(x => x.LegalEntityID == input.Id)
                               .ToListAsync();
            if (AgreementConfigList.Any())
            {
                AgreementConfigList.ForEach(x => x.LegalEntityID = null);
                DB.UpdateRange(AgreementConfigList);
            }

            if (input.Project?.Id != null && input.Project?.Id != Guid.Empty)
            {
                var modelAgreementConfig = await DB.AgreementConfigs.FirstOrDefaultAsync(x => x.ProjectID == input.Project.Id);
                if (modelAgreementConfig != null)
                {
                    modelAgreementConfig.LegalEntityID = input.Id;
                    DB.Entry(modelAgreementConfig).State = EntityState.Modified;
                }
            }
            await DB.SaveChangesAsync();

            var result = await GetLegalEntityAsync(model.ID, input?.Project?.Id);
            return result;
        }

        public async Task DeleteLegalEntityAsync(Guid id)
        {
            var model = await DB.LegalEntities.FindAsync(id);
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            //     await DB.LegalEntities.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //        c.SetProperty(col => col.IsDeleted, true)
            //        .SetProperty(col => col.Updated, DateTime.Now)
            //    );
        }
    }
}
