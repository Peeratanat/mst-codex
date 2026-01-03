using System;
using System.Collections.Generic;
using System.Text;

namespace MST_General.Params.Filters
{
    public class AgentExternalFilter
    {
        public Guid AgentID { get; set; }
        public string AgentOwner { get; set; }
        public string NameTH { get; set; }
        public string NameEng { get; set; }
        public string AgentTypeName { get; set; }
        public string IDCard { get; set; }
        public string TaxID { get; set; }
        public string PassportNo { get; set; }
        public string AllowSdh { get; set; }
        public string AllowTh { get; set; }
        public string AllowCd { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
    }
    public class AgentEmployeeExternalFilter
    {
        public Guid AgentID { get; set; }
        public string NameTH { get; set; }
        public string SurnameTH { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
    }
}
