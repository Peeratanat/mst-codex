using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_General.Services
{
    public interface IAgentEmployeeService : BaseInterfaceService
    {
        Task<List<AgentEmployeeDropdownDTO>> GetAgentEmployeeDropdownListAsync(string name, Guid? agentID = null, CancellationToken cancellationToken = default);
        Task<AgentEmployeePaging> GetAgentEmployeeListAsync(AgentEmployeeFilter filter, PageParam pageParam, AgentEmployeeSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<AgentEmployeeDTO> GetAgentEmployeeAsync(Guid id, CancellationToken cancellationToken = default);
        Task<AgentEmployeeDTO> CreateAgentEmployeeAsync(AgentEmployeeDTO input);
        Task<AgentEmployeeDTO> UpdateAgentEmployeeAsync(Guid id, AgentEmployeeDTO input);
        Task DeleteAgentEmployeeAsync(Guid id);
    }
}
