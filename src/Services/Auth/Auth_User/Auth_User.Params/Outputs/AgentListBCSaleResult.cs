using System;
using System.Collections.Generic;
using System.Text;

namespace Auth_User.Params.Outputs
{
    public class AgentListBCSaleResult
    {
        public Guid? agent_sale_id { get; set; }
        public Guid? agent_id { get; set; }

        public string prefix_name_th { get; set; }
        public string first_name_th { get; set; }
        public string last_name_th { get; set; }

    }
}
