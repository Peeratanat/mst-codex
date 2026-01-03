using Base.DTOs.MST;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.MST;
using Microsoft.EntityFrameworkCore;

namespace MST_General.Services
{
    /// <summary>
    /// ตั้งค่าการยกเลิกคืนเงิน
    /// Model: CancelReturnSetting
    /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17484404/367792590/preview
    /// </summary>
    public class CancelReturnSettingService : ICancelReturnSettingService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public CancelReturnSettingService(DatabaseContext db)
        {
            logModel = new LogModel("CancelReturnSettingService", null);
            this.DB = db;
        }

        public async Task<CancelReturnSettingDTO> GetCancelReturnSettingAsync(CancellationToken cancellationToken = default)
        {
            var data = await DB.CancelReturnSettings.Include(o => o.UpdatedBy).FirstOrDefaultAsync();
            if (data == null)
            {
                CancelReturnSetting model = new CancelReturnSetting
                {
                    ChiefReturnLessThanPercent = 70,
                    HandlingFee = 2000
                };
                await DB.CancelReturnSettings.AddAsync(model);
                await DB.SaveChangesAsync();
                data = await DB.CancelReturnSettings.FirstOrDefaultAsync(cancellationToken);
            }
            var result = CancelReturnSettingDTO.CreateFromModel(data);
            return result;
        }

        public async Task<CancelReturnSettingDTO> UpdateCancelReturnSettingAsync(Guid id, CancelReturnSettingDTO input)
        {
            var model = await DB.CancelReturnSettings.FindAsync(id);

            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;

            await DB.SaveChangesAsync();
            var result = CancelReturnSettingDTO.CreateFromModel(model);
            return result;
        }
    }
}
