using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.PRJ
{
    public class dbqMinPriceSAPList
    {
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public string ProjectNo { get; set; }
        public string SapCode { get; set; }
        public string UnitNo { get; set; }
        public double? SaleArea { get; set; }
        public string SAPWBSNo { get; set; }
        public string SAPWBSObject { get; set; }
        public string SAPWBSStatus { get; set; }
        public string BOQStyle { get; set; }
        public string HomeStyle { get; set; }
        public string CompanyCode { get; set; }
        public string MinPriceTypeKey { get; set; }
        public string MinPriceTypeName { get; set; }
        public decimal? Minprice { get; set; }
        public decimal? ROIMinprice { get; set; }
    }
}
