using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_General.Params.Outputs
{
    public class ServitudePaging
    {
        public List<ServitudeDTO> Servitude { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
