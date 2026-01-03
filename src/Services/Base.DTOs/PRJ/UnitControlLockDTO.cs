using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class UnitControlLockDTO : BaseDTO
    {
        public Guid? ProjectID { get; set; }
        public UnitDropdownDTO unitDTO { get; set; }
        public FloorDropdownDTO floorDTO { get; set; }
        public TowerDropdownDTO towerDTO { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string Remark { get; set; }
        public string StatusLock { get; set; }
        public static UnitControlLockDTO CreateFromQueryResult(UnitControlLockQueryResult model)
        {
            if (model != null)
            {
                var result = new UnitControlLockDTO()
                {
                    ProjectID = model.ProjectID,
                    unitDTO = UnitDropdownDTO.CreateFromModel(model.Unit),
                    floorDTO = FloorDropdownDTO.CreateFromModel(model.Floor),
                    towerDTO = TowerDropdownDTO.CreateFromModel(model.Tower),
                    EffectiveDate = model.EffectiveDate,
                    ExpiredDate = model.ExpiredDate,
                    Id = model.UnitControlLockID,
                    StatusLock = model.StatusLock,
                    Remark = model.Remark
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(UnitControlLockByParam sortByParam, ref IQueryable<UnitControlLockQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case UnitControlLockSortBy.TowerName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.TowerCode);
                        else query = query.OrderByDescending(o => o.Tower.TowerCode);
                        break;
                    case UnitControlLockSortBy.FloorNameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Floor.NameEN);
                        else query = query.OrderByDescending(o => o.Floor.NameEN);
                        break;
                    case UnitControlLockSortBy.FloorNameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Floor.NameTH);
                        else query = query.OrderByDescending(o => o.Floor.NameTH);
                        break;
                    case UnitControlLockSortBy.StatusLock:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.StatusLock);
                        else query = query.OrderByDescending(o => o.StatusLock);
                        break;
                    case UnitControlLockSortBy.EffectiveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.EffectiveDate);
                        else query = query.OrderByDescending(o => o.EffectiveDate);
                        break;
                    case UnitControlLockSortBy.ExpiredDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ExpiredDate);
                        else query = query.OrderByDescending(o => o.ExpiredDate);
                        break;
                    case UnitControlLockSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case UnitControlLockSortBy.UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.Unit.UnitStatus.Name);
                        break;
                    default:
                        query = query.OrderBy(o => o.Tower.TowerCode).ThenBy(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Tower.TowerCode).ThenBy(o => o.Floor.NameTH);
            }
        }



        public async Task ValidateFloorAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            if (floorDTO == null || floorDTO.Id == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                string desc = "ชั้น";
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
        public async Task ValidateUnitAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            if (unitDTO == null || unitDTO.Id == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                string desc = "แปลง";
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
    public class UnitControlLockQueryResult
    {
        public Floor Floor { get; set; }
        public Tower Tower { get; set; }
        public Unit Unit { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? FloorID { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string Remark { get; set; }
        public string StatusLock { get; set; }
        public Guid? UnitControlLockID { get; set; }
    }
}
