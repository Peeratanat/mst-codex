using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Project.Params.Filters
{
    public class AddresssFilter : BaseFilter
    {
        public string CategoryType { get; set; }
        public string AddressNameTH { get; set; }
        public string AddressNameEN { get; set; }
        public string TitleDeedNo { get; set; }
        public string PostalCode { get; set; }
        public Guid? provinceID { get; set; }
        public Guid? districtID { get; set; }
        public Guid? subDistrictID { get; set; }
        public string VillageNo { get; set; }
        public string LaneTH { get; set; }
        public string LaneEN { get; set; }
        public string RoadTH { get; set; }
        public string RoadEN { get; set; }
    }
}
