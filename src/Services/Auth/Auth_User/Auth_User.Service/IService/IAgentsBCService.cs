using Base.DTOs.USR;
using Auth_User.Params.Filters;
using Auth_User.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Helper.Logging;
using Auth;
using Base.DTOs.MST;

namespace Auth_User.Services
{
    public interface IAgentsBCService : BaseInterfaceService
    {
        Task<List<AgentDropdownDTO>> AgentBCListAsync(string agentOwner, string name);
        Task<List<AgentEmployeeDropdownDTO>> AgentBCSaleListAsync(Guid? agentID, string name);
    }
}
