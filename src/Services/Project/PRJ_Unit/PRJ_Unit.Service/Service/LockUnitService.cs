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
    public class LockUnitService : ILockUnitService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }

        public LockUnitService(DatabaseContext db)
        {
            logModel = new LogModel("LockUnitService", null);
            this.DB = db;
        }

        public async Task<UnitControlLockPaging> GetLockUnitAsync(Guid projectID, UnitControlLockFilter filter, PageParam pageParam, UnitControlLockByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            IQueryable<UnitControlLockQueryResult> query = from unit in DB.Units.Where(x => x.ProjectID == projectID)
                                                               .Include(x => x.Tower)
                                                               .Include(x => x.Floor)
                                                               .Include(x => x.UnitStatus)

                                                           join unitControlLock in DB.UnitControlLocks on unit.ID equals unitControlLock.UnitID
                                                            into unitControlLocks
                                                           from unitControlLockModel in unitControlLocks.DefaultIfEmpty()

                                                           let floorLock = DB.UnitControlLocks.Where(x => x.FloorID == unit.FloorID && x.EffectiveDate <= now && (x.ExpiredDate == null || x.ExpiredDate >= now)).FirstOrDefault()

                                                           select new UnitControlLockQueryResult
                                                           {
                                                               Tower = unit.Tower,
                                                               Floor = unit.Floor,
                                                               ProjectID = unit.ProjectID,
                                                               FloorID = unitControlLockModel.FloorID,
                                                               ExpiredDate = unitControlLockModel.ExpiredDate,
                                                               EffectiveDate = unitControlLockModel.EffectiveDate,
                                                               Remark = unitControlLockModel.Remark,
                                                               UnitControlLockID = unitControlLockModel.ID,
                                                               Unit = unit,
                                                               StatusLock = (unitControlLockModel != null && unitControlLockModel.EffectiveDate <= now && (unitControlLockModel.ExpiredDate == null || unitControlLockModel.ExpiredDate >= now) ? ("Lock Unit" + (floorLock != null ? "," : "")) : "") +
                                                               ((unit.Floor != null && floorLock != null) ? "Lock Floor" : ""),
                                                           }
                                                      ;

            #region Filter
            if (filter.TowerID != null)
            {
                query = query.Where(x => x.Tower.ID == filter.TowerID);
            }
            if (filter.FloorID != null)
            {
                query = query.Where(x => x.Floor.ID == filter.FloorID);
            }
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (filter.EffectiveDate != null)
            {
                query = query.Where(x => x.EffectiveDate.HasValue &&
                         filter.EffectiveDate.HasValue &&
                         x.EffectiveDate.Value.Date >= filter.EffectiveDate.Value.Date);
            }
            if (filter.ExpiredDate != null)
            {
                query = query.Where(x => x.UnitControlLockID != Guid.Empty &&
                       x.ExpiredDate.HasValue &&
                       filter.ExpiredDate.HasValue &&
                       x.ExpiredDate.Value.Date >= filter.ExpiredDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(x => x.Key == filter.UnitStatusKey
                                                                       && x.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus)
                                                                      )?.ID;
                query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
            }
            if (filter.LockStatus != null)
            {
                if (filter.LockStatus == 1)
                {
                    query = query.Where(x => x.StatusLock.Equals("Lock Floor"));
                }
                else if (filter.LockStatus == 2)
                {
                    query = query.Where(x => x.StatusLock.Equals("Lock Unit"));
                }
                else if (filter.LockStatus == 3)
                {
                    query = query.Where(x => x.StatusLock.Equals("Lock Unit,Lock Floor"));
                }
                else if (filter.LockStatus == 0)
                {
                    query = query.Where(x => x.StatusLock.Equals(""));
                }
            }
            #endregion

            UnitControlLockDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<UnitControlLockQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => UnitControlLockDTO.CreateFromQueryResult(o)).ToList();

            return new UnitControlLockPaging()
            {
                PageOutput = pageOutput,
                unitControlLocks = results
            };
        }
        public async Task<UnitControlLockDTO> UpdateLockUnitAsync(UnitControlLockDTO input)
        {
            if (input != null)
            {
                await input.ValidateUnitAsync(DB);

                var UnitLock = await DB.UnitControlLocks.Where(x => x.UnitID == input.unitDTO.Id).FirstOrDefaultAsync();
                if (UnitLock != null)
                {
                    UnitLock.EffectiveDate = input.EffectiveDate;
                    UnitLock.ExpiredDate = input.ExpiredDate;
                    UnitLock.Remark = input.Remark;
                    DB.UnitControlLocks.Update(UnitLock);
                }
                else
                {
                    var model = new UnitControlLock
                    {
                        ProjectID = input.ProjectID,
                        UnitID = input.unitDTO.Id,
                        EffectiveDate = input.EffectiveDate,
                        ExpiredDate = input.ExpiredDate,
                        Remark = input.Remark
                    };
                    DB.UnitControlLocks.Add(model);
                }
                await DB.SaveChangesAsync();
            }

            return input;
        }
        public async Task DeleteLockUnitAsync(Guid? input)
        {
            if (input != null)
            {
                var UnitLock = await DB.UnitControlLocks.FirstOrDefaultAsync(x => x.ID == input);
                if (UnitLock != null)
                {
                    UnitLock.IsDeleted = true;
                }
                await DB.SaveChangesAsync();
            }
        }


    }
}
