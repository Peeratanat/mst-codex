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
    public class LockFloorService : ILockFloorService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public LockFloorService(DatabaseContext db)
        {
            logModel = new LogModel("LockFloorService", null);
            this.DB = db;
        }

        public async Task<UnitControlLockPaging> GetLockFloorAsync(Guid projectID, UnitControlLockFilter filter, PageParam pageParam, UnitControlLockByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            var query = from floor in DB.Floors
                      .AsNoTracking()
                      .Where(x => x.ProjectID == projectID)
                      .Include(x => x.Tower)
                        join unitControlLock in DB.UnitControlLocks
                            .AsNoTracking() on floor.ID equals unitControlLock.FloorID into unitControlLocks
                        from unitControlLockModel in unitControlLocks.DefaultIfEmpty()
                        select new UnitControlLockQueryResult
                        {
                            Tower = floor.Tower,
                            Floor = floor,
                            ProjectID = floor.ProjectID,
                            FloorID = unitControlLockModel.FloorID,
                            ExpiredDate = unitControlLockModel.ExpiredDate,
                            EffectiveDate = unitControlLockModel.EffectiveDate,
                            Remark = unitControlLockModel.Remark,
                            UnitControlLockID = unitControlLockModel.ID,
                            StatusLock = unitControlLockModel != null && unitControlLockModel.EffectiveDate <= now && (unitControlLockModel.ExpiredDate == null || unitControlLockModel.ExpiredDate >= now) ? "Lock Floor" : ""
                        };

            #region Filter
            if (filter.TowerID != null)
            {
                query = query.Where(x => x.Tower.ID == filter.TowerID);
            }
            if (!string.IsNullOrEmpty(filter.FloorNameEN))
            {
                query = query.Where(x => x.Floor.NameEN.Contains(filter.FloorNameEN));
            }
            if (!string.IsNullOrEmpty(filter.FloorNameTH))
            {
                query = query.Where(x => x.Floor.NameTH.Contains(filter.FloorNameTH));
            }
            if (filter.EffectiveDate != null)
            {
                query = query.Where(x => x.EffectiveDate.Value.Date >= filter.EffectiveDate.Value.Date);
            }
            if (filter.ExpiredDate != null)
            {
                query = query.Where(x => x.UnitControlLockID != new Guid()
                    && (x.ExpiredDate.Value.Date == null || x.ExpiredDate.Value.Date <= filter.ExpiredDate.Value.Date));
            }
            if (filter.LockStatus != null)
            {
                if (filter.LockStatus == 1)
                {
                    query = query.Where(x => x.StatusLock.Equals("Lock Floor"));
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
        public async Task<UnitControlLockDTO> UpdateLockFloorAsync(UnitControlLockDTO input)
        {
            if (input != null)
            {
                await input.ValidateFloorAsync(DB);

                var UnitLock = await DB.UnitControlLocks.Where(x => x.FloorID == input.floorDTO.Id).FirstOrDefaultAsync();
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
                        FloorID = input.floorDTO.Id,
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
        public async Task DeleteLockFloorAsync(Guid? input)
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
