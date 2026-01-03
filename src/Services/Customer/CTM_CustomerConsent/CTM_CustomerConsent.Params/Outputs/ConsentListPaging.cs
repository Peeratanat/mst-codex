using Base.DTOs.CTM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTM_CustomerConsent.Params.Outputs
{
    public class ConsentListPaging
    {
        public List<ConsentListDTO> Consent{ get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
