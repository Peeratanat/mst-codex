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
    public interface IAgentsEmployeeExternalService : BaseInterfaceService
    {
        Task<AgentExternalEmployeesPaging> AgentsExternalEmployeeListAsync(  PageParam pageParam, AgentExternalSortByParam sortByParam, AgentEmployeeExternalFilter filter, CancellationToken cancellationToken = default);

        Task<AgentEmployeeExternalDTO> AgentsExternalEmployeeDetailAsync(Guid agentSaleID, CancellationToken cancellationToken = default);

        Task<AgentExternalEmployeesResp> UpdateAgentsExternalEmployeeAsync(AgentEmployeeExternalDTO input);

        Task<AgentExternalEmployeesResp> CreateAgentsExternalEmployeeAsync(AgentEmployeeExternalDTO input);
    }
}
