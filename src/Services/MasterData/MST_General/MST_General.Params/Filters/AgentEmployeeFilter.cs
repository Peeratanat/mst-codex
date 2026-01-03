using System;
using System.Collections.Generic;
using System.Text;

namespace MST_General.Params.Filters
{
    public class AgentEmployeeFilter : BaseFilter
    {
        public Guid? AgentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelNo { get; set; }
    }
}
