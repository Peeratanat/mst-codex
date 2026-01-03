using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.PRM
{
    public class dbqPromotionRequestNotifictionMail : BaseDbQueries
    {
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string UnitNo { get; set; }
        public DateTime ActualTransferDate { get; set; }
        public string EmployeeNo { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public int SalePromotionAmount { get; set; }
        public int SalePromotionRequestAmount { get; set; }
        public int TransferPromotionAmount { get; set; }
        public int TransferPromotionRequestAmount { get; set; }
        public bool IsSalePromotion { get; set; }
        public bool IsTransferPromotion { get; set; }
    }
}
