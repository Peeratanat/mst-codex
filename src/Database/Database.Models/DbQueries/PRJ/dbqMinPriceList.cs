using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.PRJ
{
    public class dbqMinPriceList : BaseDbQueries
    {
        public Guid? MinPriceID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public string ProjectNo { get; set; }
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public double? SaleArea { get; set; }
        public double? TitledeedArea { get; set; }
        public Guid? MinPriceTypeID { get; set; }
        public string MinPriceTypeKey { get; set; }
        public string MinPriceTypeName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? ROIMinprice { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? ApproveMinprice { get; set; }
        public Guid? MinPriceStageID { get; set; }
        public string MinPriceStageKey { get; set; }
        public string MinPriceStageName { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedByUserID { get; set; }
        public string CreatedByName { get; set; }
        public string SAPWBSNo { get; set; }
        public Guid? UnitStatusID { get; set; }
    }
}
