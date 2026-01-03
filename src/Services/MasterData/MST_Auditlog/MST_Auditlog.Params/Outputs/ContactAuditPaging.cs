using Base.DTOs.CTM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Auditlog.Params.Outputs
{
    public class ContactAuditPaging
    {
        public List<ContactAuditDTO> ContactAudit { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
