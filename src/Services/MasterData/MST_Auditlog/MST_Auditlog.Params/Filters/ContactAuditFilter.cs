using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Auditlog.Params.Filters
{
    public class ContactAuditFilter
    {
        public string Actions { get; set; }
        public string ContactNo { get; set; }
        public string FirstNameTH { get; set; }
        public string LastNameTH { get; set; }
        public DateTime? UpdatedDateFrom { get; set; }
        public DateTime? UpdatedDateTo { get; set; }
        public string UpdatedBy { get; set; }
    }
}
