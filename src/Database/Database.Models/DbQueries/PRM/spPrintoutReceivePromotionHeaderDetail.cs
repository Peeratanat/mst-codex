using System;
using System.Collections.Generic;
using System.Text;
//using Base.DbQueries;

namespace Database.Models.DbQueries.PRM
{
    public class spPrintoutReceivePromotionHeaderDetail
    {
        public string ReceivePromotionDate { get; set; }
        public string PromotionDescription { get; set; }
        public int? ReceiveAmount { get; set; }
        public double? PricePerUnit { get; set; }
        public double? TotalPrice { get; set; }
        public string ReceiveDate { get; set; }
        public string Remark { get; set; }
        public string PRNo { get; set; }
        public string ItemID { get; set; }
    }
}
