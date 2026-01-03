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
    public class UnitControlInterestDTO : BaseDTO
    {
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string Remark { get; set; }
        public int? InterestCounter { get; set; }
        public static UnitControlInterestDTO CreateFromQueryResult(UnitControlInterestQueryResult model)
        {
            if (model != null)
            {
                var result = new UnitControlInterestDTO()
                {
                    ProjectID = model.ProjectID,
                    UnitID = model.UnitID,
                    EffectiveDate = model.EffectiveDate,
                    ExpiredDate = model.ExpiredDate,
                    Id = model.UnitControlInterestID,
                    Remark = model.Remark,
                    InterestCounter = model.InterestCounter
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public async Task ValidateAddAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            if (UnitID == null || UnitID == new Guid())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                string desc = "แปลง";
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (EffectiveDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                string desc = "วันที่เวลาเริ่มต้น";
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ExpiredDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                string desc = "วันที่เวลาสิ้นสุด";
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (EffectiveDate != null && ExpiredDate != null)
            {
                {
                    var oldIn = await db.UnitControlInterests.Where(x => x.UnitID == UnitID && (x.EffectiveDate <= EffectiveDate && x.ExpiredDate >= EffectiveDate)).FirstOrDefaultAsync();
                    if (oldIn != null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                        string desc = "ช่วงวันที่เริ่มต้นไม่ถูกต้อง";
                        var msg = desc;
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                {
                    var oldIn = await db.UnitControlInterests.Where(x => x.UnitID == UnitID && (x.EffectiveDate <= ExpiredDate && x.EffectiveDate >= EffectiveDate)).FirstOrDefaultAsync();
                    if (oldIn != null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                        string desc = "ช่วงวันที่สิ้นสุดไม่ถูกต้อง";
                        var msg = desc;
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
        public async Task ValidateUpdateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            var oldData = await db.UnitControlInterests.FindAsync(Id);
            if (oldData == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefaultAsync();
                string desc = errMsg.Message.Replace("[field]", "ที่ต้องการแก้ไข");
                var msg = desc;
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (UnitID == null || UnitID == new Guid())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                string desc = "แปลง";
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (UnitID == null || UnitID == new Guid())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                string desc = "แปลง";
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (EffectiveDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                string desc = "วันที่เวลาเริ่มต้น";
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ExpiredDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                string desc = "วันที่เวลาสิ้นสุด";
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (EffectiveDate != null && ExpiredDate != null)
            {
                {
                    var oldIn = await db.UnitControlInterests.Where(x => x.ID != Id && x.UnitID == UnitID && (x.EffectiveDate <= EffectiveDate && x.ExpiredDate >= EffectiveDate)).FirstOrDefaultAsync();
                    if (oldIn != null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                        string desc = "ช่วงวันที่เริ่มต้นไม่ถูกต้อง";
                        var msg = desc;
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                {
                    var oldIn = await db.UnitControlInterests.Where(x => x.ID != Id && x.UnitID == UnitID && (x.EffectiveDate <= ExpiredDate && x.EffectiveDate >= EffectiveDate)).FirstOrDefaultAsync();
                    if (oldIn != null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                        string desc = "ช่วงวันที่สิ้นสุดไม่ถูกต้อง";
                        var msg = desc;
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
        public async Task ValidateDeleteAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            var oldData = await db.UnitControlInterests.AnyAsync(x => x.ID == Id);
            if (!oldData)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefaultAsync();
                string desc = errMsg.Message.Replace("[field]", "ที่ต้องการลบ");
                var msg = desc;
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
}
