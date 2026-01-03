using Database.Models;
using Database.Models.MST;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;
using MST_General.Params.Outputs;
using Base.DTOs;
using Database.Models.DbQueries;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class MasterPriceItemService : IMasterPriceItemService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public MasterPriceItemService(DatabaseContext db)
        {
            logModel = new LogModel("MasterPriceItemService", null);
            DB = db;
        }

        public async Task<List<MasterPriceItemDTO>> GetMasterPriceItemDropdownListAsync(string detail, CancellationToken cancellationToken = default)
        {
            IQueryable<MasterPriceItem> query = DB.MasterPriceItems.AsNoTracking();
            if (!string.IsNullOrEmpty(detail))
            {
                query = query.Where(o => o.Detail.Contains(detail));
            }

            query = query.OrderBy(o => o.Order);
            return await query.Select(o => MasterPriceItemDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
        }
    }
}
