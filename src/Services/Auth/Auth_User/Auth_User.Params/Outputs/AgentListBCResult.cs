using System;
using System.Collections.Generic;
using System.Text;

namespace Auth_User.Params.Outputs
{
    public class AgentListBCResult
    {
        public Guid agent_id { get; set; }
        public bool? is_thai { get; set; }
        public int agent_type_id { get; set; }
        public string agent_type_name { get; set; }
        public string prefix_name_th { get; set; }
        public string first_name_th { get; set; }
        public string last_name_th { get; set; }
        public string prefix_name_eng { get; set; }
        public string first_name_eng { get; set; }
        public string last_name_eng { get; set; }
        public string id_card { get; set; }
        public string passport_no { get; set; }
        public string tax_id { get; set; }
        public string agent_owner { get; set; }
        public DateTime? last_activity_date { get; set; }
        public string create_by { get; set; }
        public DateTime? create_date { get; set; }
        public string modify_by { get; set; }
        public DateTime? modify_date { get; set; }
    }
}
