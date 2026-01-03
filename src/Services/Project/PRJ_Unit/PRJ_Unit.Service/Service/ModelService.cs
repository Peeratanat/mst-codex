using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace PRJ_Unit.Services
{
    public class ModelService : IModelService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public ModelService(DatabaseContext db)
        {
            logModel = new LogModel("ModelService", null);
            DB = db;
        }

        public async Task<List<ModelDropdownDTO>> GetModelDropdownListAsync(Guid? projectID = null, string name = null, CancellationToken cancellationToken = default)
        {
            var query = DB.Models.AsNoTracking();
            if (projectID != null)
            {
                query = query.Where(x => x.ProjectID == projectID);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }

            var results = await query.OrderBy(o => o.NameTH).Take(100).Select(o => ModelDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }

    }
}
