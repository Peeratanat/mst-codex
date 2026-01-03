using System;

namespace PRJ_UnitInfos.Params.Filters
{
    public class UnitDocumentFilter
    {
        public Guid? CompanyID { get; set; }
        public string DocumentNo { get; set; }
        public Guid? ProjectID { get; set; }
        public string UnitNo { get; set; }
        public string CustomerName { get; set; }
    }
}
