using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class AgentEmployeeService : IAgentEmployeeService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public AgentEmployeeService(DatabaseContext db)
        {
            logModel = new LogModel("AgentEmployeeService", null);
            DB = db;
        }
        public async Task<List<AgentEmployeeDropdownDTO>> GetAgentEmployeeDropdownListAsync(string name, Guid? agentID = null, CancellationToken cancellationToken = default)
        {
            IQueryable<AgentEmployee> query = DB.AgentEmployees.AsNoTracking();

            #region Filter
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name));
            }
            if (agentID != null)
            {
                query = query.Where(o => o.AgentID == agentID);
            }
            #endregion

            var results = await query.OrderBy(o => o.FirstName).ThenBy(o => o.LastName).Take(100)
                        .Select(o => AgentEmployeeDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }

        public async Task<AgentEmployeePaging> GetAgentEmployeeListAsync(AgentEmployeeFilter filter, PageParam pageParam, AgentEmployeeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<AgentEmployeeQueryResult> query = DB.AgentEmployees.AsNoTracking()
                                                         .Select(o => new AgentEmployeeQueryResult
                                                         {
                                                             AgentEmployee = o,
                                                             UpdatedBy = o.UpdatedBy
                                                         });

            #region filter
            if (filter.AgentID != null)
            {
                query = query.Where(o => o.AgentEmployee.AgentID == filter.AgentID);
            }
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(x => x.AgentEmployee.FirstName.Contains(filter.FirstName));
            }
            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(x => x.AgentEmployee.LastName.Contains(filter.LastName));
            }
            if (!string.IsNullOrEmpty(filter.TelNo))
            {
                query = query.Where(x => x.AgentEmployee.TelNo.Contains(filter.TelNo));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.AgentEmployee.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.AgentEmployee.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.AgentEmployee.Updated >= filter.UpdatedFrom
                                    && x.AgentEmployee.Updated <= filter.UpdatedTo);
            }
            #endregion

            AgentEmployeeDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<AgentEmployeeQueryResult>(pageParam, ref query);

            var results = await query.Select(o => AgentEmployeeDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new AgentEmployeePaging()
            {
                AgentEmployees = results,
                PageOutput = pageOutput
            };
        }

        public async Task<AgentEmployeeDTO> GetAgentEmployeeAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.AgentEmployees.Include(o => o.UpdatedBy)
                .FirstOrDefaultAsync(x => x.ID == id, cancellationToken);
            var result = AgentEmployeeDTO.CreateFromModel(model);
            return result;
        }

        public async Task<AgentEmployeeDTO> CreateAgentEmployeeAsync(AgentEmployeeDTO input)
        {
            await input.ValidateAsync(DB);
            AgentEmployee model = new AgentEmployee();
            input.ToModel(ref model);

            await DB.AgentEmployees.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await GetAgentEmployeeAsync(model.ID);
            return result;
        }

        public async Task<AgentEmployeeDTO> UpdateAgentEmployeeAsync(Guid id, AgentEmployeeDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.AgentEmployees.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await GetAgentEmployeeAsync(model.ID);
            return result;
        }

        public async Task DeleteAgentEmployeeAsync(Guid id)
        {
            var model = await DB.AgentEmployees.FindAsync(id);
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            // await DB.AgentEmployees.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //     c.SetProperty(col => col.IsDeleted, true)
            //     .SetProperty(col => col.Updated, DateTime.Now)
            // );
        }
    }
}
