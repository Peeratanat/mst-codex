using System;
using System.Collections.Generic;
using Base.DTOs.PRM;
using PagingExtensions;

namespace MST_Promotion.Params.Outputs
{
    public class MaterialSyncJobPaging
    {
        public List<MaterialSyncJobDTO> MaterialSyncJobDTOs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
