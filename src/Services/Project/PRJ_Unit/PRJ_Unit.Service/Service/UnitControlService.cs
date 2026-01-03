using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.LOG;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using ExcelExtensions;
using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using PRJ_Unit.Services.Excels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Base.DTOs.FIN.BillPaymentHeaderDTO;

namespace PRJ_Unit.Services
{
    public class UnitControlService : IUnitControlService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }

        public UnitControlService(DatabaseContext db)
        {
            logModel = new LogModel("UnitControlService", null);
            this.DB = db;
        }

        public async Task<UnitControlInterestPaging> GetUnitControlInterestAsync(Guid projectID, UnitControlInterestFilter filter, PageParam pageParam, UnitControlInterestSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            IQueryable<UnitControlInterestQueryResult> query = from unit in DB.Units.AsNoTracking().Where(x => x.ProjectID == projectID)
                                                               .Include(x => x.Tower)
                                                               .Include(x => x.Floor)
                                                               .Include(x => x.UnitStatus)
                                                               join unitControlInterest in DB.UnitControlInterests on unit.ID equals unitControlInterest.UnitID
                                                                into unitControlInterests
                                                               from unitControlInterestModel in unitControlInterests.DefaultIfEmpty()

                                                               select new UnitControlInterestQueryResult
                                                               {
                                                                   Unit = unit,
                                                                   Floor = unit.Floor,
                                                                   Tower = unit.Tower,
                                                                   ProjectID = unit.ProjectID,
                                                                   FloorID = unit.FloorID,
                                                                   UnitID = unit.ID,
                                                                   EffectiveDate = unitControlInterestModel != null ? unitControlInterestModel.EffectiveDate : null,
                                                                   ExpiredDate = unitControlInterestModel != null ? unitControlInterestModel.ExpiredDate : null,
                                                                   InterestCounter = unitControlInterestModel != null ? unitControlInterestModel.InterestCounter : null,
                                                                   Remark = unitControlInterestModel != null ? unitControlInterestModel.Remark : null,
                                                                   UnitControlInterestID = unitControlInterestModel != null ? unitControlInterestModel.ID : new Guid(),
                                                               };

            #region Filter
            if (filter.TowerID != null)
            {
                query = query.Where(x => x.Tower.ID == filter.TowerID);
            }
            if (filter.FloorID != null)
            {
                query = query.Where(x => x.FloorID == filter.FloorID);
            }
            if (filter.EffectiveDate != null)
            {
                query = query.Where(x => x.EffectiveDate.Value.Date >= filter.EffectiveDate.Value.Date);
            }
            if (filter.ExpiredDate != null)
            {
                query = query.Where(x => x.ExpiredDate.Value.Date == null || x.ExpiredDate.Value.Date <= filter.ExpiredDate.Value.Date);
            }
            if (filter.InterestCounter != null)
            {
                query = query.Where(x => x.InterestCounter == filter.InterestCounter);
            }
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = (await DB.MasterCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Key == filter.UnitStatusKey
                                                                       && x.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus)
                                                                      )?.ID;
                if (unitStatusMasterCenterID is not null)
                    query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
            }
            #endregion

            UnitInterestDTO.SortBy(sortByParam, ref query);
            var queryGroup = query.GroupBy(x => x.UnitID).Select(x => x.Key);
            var pageOutput = PagingHelper.Paging<Guid?>(pageParam, ref queryGroup);
            var groupList = queryGroup.ToList();
            query = query.Where(x => groupList.Contains(x.UnitID));
            var queryResults = await query.ToListAsync(cancellationToken);
            var queryResultGroups = queryResults.GroupBy(x => x.UnitID)
                .Select(x => new UnitControlInterestGroupResult
                {
                    UnitID = x.Key,
                    FloorID = x.FirstOrDefault()?.FloorID,
                    ProjectID = x.FirstOrDefault()?.ProjectID,
                    Floor = x.FirstOrDefault()?.Floor,
                    Tower = x.FirstOrDefault()?.Tower,
                    Unit = x.FirstOrDefault()?.Unit,
                    Details = x.Where(o => o.UnitControlInterestID != null && o.UnitControlInterestID != new Guid()).OrderByDescending(o => o.EffectiveDate).ToList()
                }).ToList();
            var results = queryResultGroups.Select(o => UnitInterestDTO.CreateFromQueryResult(o)).ToList();
            return new UnitControlInterestPaging()
            {
                PageOutput = pageOutput,
                unitInterests = results
            };
        }
        public async Task<UnitControlInterestDTO> AddUnitControlInterestAsync(UnitControlInterestDTO input)
        {
            if (input != null)
            {
                await input.ValidateAddAsync(DB);

                var model = new UnitControlInterest
                {
                    ProjectID = input.ProjectID,
                    UnitID = input.UnitID,
                    EffectiveDate = input.EffectiveDate,
                    ExpiredDate = input.ExpiredDate,
                    Remark = input.Remark,
                    InterestCounter = input.InterestCounter
                };
                DB.UnitControlInterests.Add(model);
                await DB.SaveChangesAsync();
            }

            return input;
        }
        public async Task<UnitControlInterestDTO> UpdateUnitControlInterestAsync(UnitControlInterestDTO input)
        {
            if (input != null)
            {
                await input.ValidateUpdateAsync(DB);

                var model = await DB.UnitControlInterests.Where(x => x.ID == input.Id).FirstOrDefaultAsync();
                if (model != null)
                {
                    model.EffectiveDate = input.EffectiveDate;
                    model.ExpiredDate = input.ExpiredDate;
                    model.Remark = input.Remark;
                    model.InterestCounter = input.InterestCounter;
                    DB.UnitControlInterests.Update(model);
                }
                await DB.SaveChangesAsync();
            }
            return input;
        }
        public async Task DeleteUnitControlInterestAsync(Guid? input)
        {

            var model = await DB.UnitControlInterests.FirstOrDefaultAsync(x => x.ID == input);
            if (model != null)
            {
                model.IsDeleted = true;
            }
            await DB.SaveChangesAsync();
        }
    }
}
