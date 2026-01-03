using Base.DTOs.CTM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Auditlog.Params.Outputs
{
    public class ContactChangeLogPaging
    {
        public List<ContactChangeLogDTO> ContactChangeLog { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
