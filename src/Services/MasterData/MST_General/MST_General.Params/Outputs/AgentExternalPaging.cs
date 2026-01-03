using Base.DTOs.MST;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MST_General.Params.Outputs
{
    public class AgentExternalPaging
    {
        public List<AgentExternalDTO> AgentExternals { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
