using Base.DTOs.MST;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MST_General.Params.Outputs
{
    public class CancelReasonPaging
    {
        public List<CancelReasonDTO> CancelReasons { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
