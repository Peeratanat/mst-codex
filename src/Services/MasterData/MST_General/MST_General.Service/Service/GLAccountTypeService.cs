using Base.DTOs.MST;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MST_General.Services
{
    public class GLAccountTypeService : IGLAccountTypeService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public GLAccountTypeService(DatabaseContext db)
        {
            logModel = new LogModel("GLAccountTypeService", null);
            DB = db;
        }

        public async Task<List<GLAccountTypeDropdownDTO>> GetGLAccountTypeDropdownListAsync(string key, string name)
        {
            IQueryable<GLAccountType> query = DB.GLAccountTypes;
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(o => o.Key.Contains(key));
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }
            query = query.OrderBy(o => o.Name).ThenBy(o => o.Order);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => GLAccountTypeDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

    }
}
