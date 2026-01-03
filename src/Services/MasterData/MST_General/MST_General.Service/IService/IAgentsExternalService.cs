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
    public interface IAgentsExternalService : BaseInterfaceService
    {
        Task<AgentExternalPaging> AgentExternalListAsync(AgentExternalFilter filter, PageParam pageParam, AgentExternalSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<AgentExternalDTO> AgentExternaDetailAsync(Guid agentID, CancellationToken cancellationToken = default);
        Task<AgentExternalResp> UpdateAgentExternalAsync(AgentExternalDTO input);
        Task<AgentExternalResp> CreateAgentExternalAsync(AgentExternalDTO input);
        Task<List<AgentExternalPrefixDTO>> GetPrefixListAsync(int isCorporate, CancellationToken cancellationToken = default);
        Task<List<AgentBusinessTypeDropdownDTO>> GetBusinessTypeListAsync(CancellationToken cancellationToken = default);
        Task<AgentExternalResp> CheckDuplicateAgentExternalAsync(AgentExternalFilter filter, CancellationToken cancellationToken = default);
        Task<List<AgentExternalDTO>> AgentExternalAPOwnerListAsync(AgentExternalFilter filter, CancellationToken cancellationToken = default);
        Task<UserAuthBGDTO> CheckAuthUserBGAsync(Guid? userID, CancellationToken cancellationToken = default);
    }
}
