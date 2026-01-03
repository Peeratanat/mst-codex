using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_Event.Params.Outputs
{
    public class LetterOfGuaranteePaging
    {
        public List<LetterOfGuaranteeDTO> LetterOfGuarantee { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
