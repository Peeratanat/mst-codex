using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_Finacc.Params.Outputs
{
    public class EDCPaging
    {
        public List<EDCDTO> EDCs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
