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
    public interface IAgentsService : BaseInterfaceService
    {
        Task<List<AgentDropdownDTO>> GetAgentDropdownListAsync(string name, CancellationToken cancellationToken = default);
        Task<AgentPaging> GetAgentListAsync(AgentFilter filter, PageParam pageParam, AgentSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<AgentDTO> GetAgentAsync(Guid id, CancellationToken cancellationToken = default);
        Task<AgentDTO> CreateAgentAsync(AgentDTO input);
        Task<AgentDTO> UpdateAgentAsync(Guid id, AgentDTO input);
        Task DeleteAgentAsync(Guid id);
    }
}
