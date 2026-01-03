using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class ProvincePaging
    {
        public List<ProvinceDTO> Provinces { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
