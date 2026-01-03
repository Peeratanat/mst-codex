using Database.Models;
using Database.Models.MST;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class BOConfigurationService : IBOConfigurationService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }

        public BOConfigurationService(DatabaseContext db)
        {
            logModel = new LogModel("BOConfigurationService", null);
            this.DB = db;
        }
        public async Task<BOConfigurationDTO> GetBOConfigurationAsync(CancellationToken cancellationToken = default)
        {
            var data = await DB.BOConfigurations.Include(o => o.UpdatedBy).FirstOrDefaultAsync(cancellationToken);
            if (data == null)
            {
                BOConfiguration model = new BOConfiguration();
                await DB.BOConfigurations.AddAsync(model);
                await DB.SaveChangesAsync();
                data = await DB.BOConfigurations.FirstOrDefaultAsync(cancellationToken);
            }
            var result = BOConfigurationDTO.CreateFromModel(data);
            return result;
        }

        public async Task<BOConfigurationDTO> UpdateBOConfigurationAsync(Guid id, BOConfigurationDTO input)
        {
            await input.ValidateAsync(DB);
            await input.ValidateByFieldAsync(DB);

            var model = await DB.BOConfigurations.FindAsync(id) ;

            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = BOConfigurationDTO.CreateFromModel(model);
            return result;
        }
    }

}
