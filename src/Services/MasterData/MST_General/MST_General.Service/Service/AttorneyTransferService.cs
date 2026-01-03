using Base.DTOs.MST;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.MST;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MST_General.Services
{
    public class AttorneyTransferService : IAttorneyTransferService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public AttorneyTransferService(DatabaseContext db)
        {
            logModel = new LogModel("AttorneyTransferService", null);
            DB = db;
        }

        public async Task<List<AttorneyTransferListDTO>> GetAttorneyTransferDropdownListAsync(string name, CancellationToken cancellationToken = default)
        {
            IQueryable<AttorneyTransfer> query = DB.AttorneyTransfers.AsNoTracking();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Atty_FullName.Contains(name));
            }

            return await query.OrderBy(o => o.Atty_FullName).Take(100)
                 .Select(o => AttorneyTransferListDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
        }
    }
}
