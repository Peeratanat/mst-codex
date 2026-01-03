using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.PRM
{
    public class dbqPreSalePromotionRequestList : BaseDbQueries
    {
        public Guid? PreSalePromotionRequestID { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public Guid? MasterPreSalePromotionID { get; set; }
        public string PromotionNo { get; set; }
        public DateTime? PRCompletedDate { get; set; }
        public Guid? PromotionRequestPRStatusMasterCenterID { get; set; }
        public string PromotionRequestPRStatusKey { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? Updated { get; set; }
        public string DisplayName { get; set; }
        public string UnitNo { get; set; }
        public string UnitID { get; set; }
    }
}
