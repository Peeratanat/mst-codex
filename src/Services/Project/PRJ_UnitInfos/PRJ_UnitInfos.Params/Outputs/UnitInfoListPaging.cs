using Base.DTOs.SAL;
using Database.Models.DbQueries.SAL;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_UnitInfos.Params.Outputs
{
    public class UnitInfoListPaging
    {
        public List<UnitInfoListDTO> Units { get; set; }
        public PageOutput PageOutput { get; set; }
    }
    public class UnitInfoListPagingByContact
    {
        public List<dbqUnitInfoListByContact> Units { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
