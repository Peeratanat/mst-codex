using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MST_General.Params.Filters
{
    public class CompanyFilter : BaseFilter
    {
        public string APAuthorizeRefID { get; set; }
        public string Code { get; set; }
        public string NameTH { get; set; }
        public string NameEN { get; set; }
        public string TaxID { get; set; }
        public string AddressTH { get; set; }
        public string AddressEN { get; set; }
        public string BuildingTH { get; set; }
        public string BuildingEN { get; set; }
        public string SoiTH { get; set; }
        public string SoiEN { get; set; }
        public string RoadTH { get; set; }
        public string RoadEN { get; set; }
        public Guid? SubDistrictID { get; set; }
        public Guid? DistrictID { get; set; }
        public Guid? ProvinceID { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string SapCompanyID { get; set; }
        public string NameTHOld { get; set; }
        public string NameENOld { get; set; }
        public bool? IsUseInCRM { get; set; }
    }
}
