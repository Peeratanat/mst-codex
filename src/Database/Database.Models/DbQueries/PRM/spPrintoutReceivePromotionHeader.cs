using System;
using System.Collections.Generic;
using System.Text;
//using Base.DbQueries;

namespace Database.Models.DbQueries.PRM
{
    public class spPrintoutReceivePromotionHeader
    {
      
        public string PrintOutName { get; set; }
        public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }

        public string ContractNumber { get; set; }
        public string UnitNumber { get; set; }
        public string Status { get; set; }

        public string ReceivePromotionID { get; set; }

        public int? PrintNo { get; set; }

        public string PrintUser { get; set; }

        public string PrintDate { get; set; }
        public string PrintTime { get; set; }
        public string ContractPayDate { get; set; }

        public string TransferRealDate { get; set; }
        public Guid? LCMID { get; set; }
        public string LCMName { get; set; }
        public Guid? LCID { get; set; }
        public string LCName { get; set; }
        public string SystemInfo { get; set; }
    }
}
