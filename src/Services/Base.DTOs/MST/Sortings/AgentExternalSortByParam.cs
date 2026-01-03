using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class AgentExternalSortByParam
    { 
        public AgentExternalSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum AgentExternalSortBy
    {
        first_name_th,
        last_name_th,
        first_name_eng,
        last_name_eng,
        id_card,
        passport_no,
        tax_id,
        last_activity_date,
        modify_by,
        modify_date,
        agent_type_name,
        agent_owner,
        allow_sdh,
        allow_th,
        allow_cd
    }
}
