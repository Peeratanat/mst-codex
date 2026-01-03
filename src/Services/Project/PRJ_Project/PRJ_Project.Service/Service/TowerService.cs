using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Project.Services;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Helper.Logging;

namespace PRJ_Project.Services
{
    public class TowerService : ITowerService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }
        public TowerService(DatabaseContext db)
        {
            logModel = new LogModel("TowerService", null);
            DB = db;
        }
        public async Task<List<TowerDropdownDTO>> GetTowerDropdownListAsync(Guid projectID, string Code, CancellationToken cancellationToken = default)
        {
            IQueryable<Tower> query = DB.Towers.AsNoTracking().Where(x => x.ProjectID == projectID);
            if (!string.IsNullOrEmpty(Code))
            {
                query = query.Where(o => o.TowerCode.Contains(Code));
            }

            var results = await query.OrderBy(o => o.TowerCode).Take(100).Select(o => TowerDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
            return results;
        }
        public async Task<TowerPaging> GetTowerListAsync(Guid projectID, TowerFilter filter, PageParam pageParam, TowerSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<TowerQueryResult> query = DB.Towers.AsNoTracking().Where(o => o.ProjectID == projectID)
                                                              .Select(o => new TowerQueryResult
                                                              {
                                                                  Tower = o,
                                                                  //FloorCount = DB.Floors.Where(p => p.TowerID == o.ID && p.ProjectID==projectID).Count(),
                                                                  FloorCount = DB.Floors.AsNoTracking().Count(w => w.TowerID == o.ID && w.ProjectID == projectID),
                                                                  UpdatedBy = o.UpdatedBy
                                                              });

                #region Filter

                if (!string.IsNullOrEmpty(filter.Code))
                {
                    query = query.Where(o => o.Tower.TowerCode.Contains(filter.Code));
                }
                if (!string.IsNullOrEmpty(filter.NoTH))
                {
                    query = query.Where(o => o.Tower.TowerNoTH.Contains(filter.NoTH));
                }
                if (!string.IsNullOrEmpty(filter.NoEN))
                {
                    query = query.Where(o => o.Tower.TowerNoEN.Contains(filter.NoEN));
                }
                if (!string.IsNullOrEmpty(filter.CondominiumName))
                {
                    query = query.Where(o => o.Tower.CondominiumName.Contains(filter.CondominiumName));
                }
                if (!string.IsNullOrEmpty(filter.CondominiumNo))
                {
                    query = query.Where(o => o.Tower.CondominiumNo.Contains(filter.CondominiumNo));
                }
                if (!string.IsNullOrEmpty(filter.Description))
                {
                    query = query.Where(o => o.Tower.TowerDescription.Contains(filter.Description));
                }
                if (!string.IsNullOrEmpty(filter.UpdatedBy))
                {
                    query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
                }
                if (filter.UpdatedFrom != null)
                {
                    query = query.Where(x => x.Tower.Updated >= filter.UpdatedFrom);
                }
                if (filter.UpdatedTo != null)
                {
                    query = query.Where(x => x.Tower.Updated <= filter.UpdatedTo);
                }
                if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
                {
                    query = query.Where(x => x.Tower.Updated >= filter.UpdatedFrom && x.Tower.Updated <= filter.UpdatedTo);
                }
                #endregion

                TowerDTO.SortBy(sortByParam, ref query);

                var test = query.ToList();

                var results = await query.Select(o => TowerDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

                var pageOutput = PagingHelper.PagingList<TowerDTO>(pageParam, ref results);


                return new TowerPaging()
                {
                    PageOutput = pageOutput,
                    Towers = results
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TowerDTO> GetTowerAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Towers.Include(o => o.UpdatedBy).FirstAsync(o => o.ProjectID == projectID && o.ID == id, cancellationToken);
            var result = await TowerDTO.CreateFromModelAsync(model, DB);
            return result;
        }

        public async Task<TowerDTO> CreateTowerAsync(Guid projectID, TowerDTO input)
        {

            await ValidateTower(projectID, input);
            var project = await DB.Projects.FirstAsync(o => o.ID == projectID);
            Tower model = new Tower();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            await DB.Towers.AddAsync(model);
            await DB.SaveChangesAsync();

            var towerDataStatusMasterCenterID = await TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await GetTowerAsync(projectID, model.ID);
            return result;
        }

        public async Task<TowerDTO> UpdateTowerAsync(Guid projectID, Guid id, TowerDTO input)
        {
            await ValidateTower(projectID, input);

            var project = await DB.Projects.FindAsync(projectID);
            var model = await DB.Towers.FirstAsync(o => o.ProjectID == projectID && o.ID == id);
            input.ToModel(ref model);
            model.ProjectID = projectID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var towerDataStatusMasterCenterID = await TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await GetTowerAsync(projectID, model.ID);
            return result;
        }

        public async Task DeleteTowerAsync(Guid projectID, Guid id)
        {

            #region Validate Check Used Data
            ValidateException ex = new ValidateException();

            var towerUsed = await DB.Units.FirstOrDefaultAsync(o => o.TowerID == id);

            if (towerUsed != null)
            {
                var tower = await DB.Towers.FindAsync(id);
                var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR0154");
                string desc = tower.TowerCode;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
            #endregion

            var project = await DB.Projects.FirstOrDefaultAsync(o => o.ID == projectID);
            var model = await DB.Towers.FirstOrDefaultAsync(o => o.ProjectID == projectID && o.ID == id);
            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            var towerDataStatusMasterCenterID = await TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
        }

        private async Task<Guid> TowerDataStatus(Guid projectID)
        {
            var towers = await DB.Towers.Where(o => o.ProjectID == projectID).ToListAsync();
            var floors = await DB.Floors.Where(o => o.ProjectID == projectID).ToListAsync();
            var towerDataStatusPrepareMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft)).ID; //อยู่ระหว่างจัดเตรียม
            var towerDataStatusSaleMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Sale)).ID; //พร้อมขาย

            var towerDataStatusMasterCenterID = towerDataStatusPrepareMasterCenterID;

            if (towers.Count == 0 || floors.Count == 0)
            {
                return towerDataStatusMasterCenterID;
            }

            if (towers.TrueForAll(o =>
                    !string.IsNullOrEmpty(o.TowerCode)
                    //&& !string.IsNullOrEmpty(o.TowerNoTH)
                    //&& !string.IsNullOrEmpty(o.TowerNoEN)
                    //&& !string.IsNullOrEmpty(o.CondominiumName)
                    //&& !string.IsNullOrEmpty(o.CondominiumNo)
                    )
                    && floors.TrueForAll(o =>
                        !string.IsNullOrEmpty(o.NameTH)
                      && !string.IsNullOrEmpty(o.NameEN)
                       )
              )
            {

                towerDataStatusMasterCenterID = towerDataStatusSaleMasterCenterID;
            }
            return towerDataStatusMasterCenterID;
        }

        private async Task ValidateTower(Guid projectID, TowerDTO input)
        {
            ValidateException ex = new ValidateException();
            //validate unique
            if (!string.IsNullOrEmpty(input.Code))
            {
                var checkUniqueCode = input.Id != (Guid?)null
               ? DB.Towers.Any(o => o.ProjectID == projectID && o.ID != input.Id && o.TowerCode == input.Code)
               : DB.Towers.Any(o => o.ProjectID == projectID && o.TowerCode == input.Code);
                if (checkUniqueCode)
                {
                    var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR0042");
                    string desc = input.GetType().GetProperty(nameof(TowerDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                    // Modified by Suchat S. 2020-06-26 for Jira Job CRM-5459
                    // var msg = errMsg.Message.Replace("[field]", desc);
                    var msg = errMsg.Message.Replace("[field]", "ชื่ออาคาร");
                    msg = msg.Replace("[value]", input.Code);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(input.NoTH))
            {
                var checkUniqueNoTH = input.Id != (Guid?)null
               ? DB.Towers.Any(o => o.ProjectID == projectID && o.ID != input.Id && o.TowerNoTH == input.NoTH)
               : DB.Towers.Any(o => o.ProjectID == projectID && o.TowerNoTH == input.NoTH);
                if (checkUniqueNoTH)
                {
                    var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR0042");
                    string desc = input.GetType().GetProperty(nameof(TowerDTO.NoTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.NoTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(input.NoEN))
            {
                var checkUniqueNoEN = input.Id != (Guid?)null
               ? DB.Towers.Any(o => o.ProjectID == projectID && o.ID != input.Id && o.TowerNoEN == input.NoEN)
               : DB.Towers.Any(o => o.ProjectID == projectID && o.TowerNoEN == input.NoEN);
                if (checkUniqueNoEN)
                {
                    var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR0042");
                    string desc = input.GetType().GetProperty(nameof(TowerDTO.NoEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.NoEN);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(input.CondominiumNo))
            {
                if (!input.CondominiumNo.IsOnlyNumberWithSpecialCharacter(true))
                {
                    var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR0016");
                    string desc = input.GetType().GetProperty(nameof(TowerDTO.CondominiumNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

    }
}
