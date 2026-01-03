using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.ADM
{
    public class dbqUnitStatusList : BaseDbQueries
    {
        public string ProjectNo { get; set; }
        public string UnitNo { get; set; }
        public Guid? BookingID { get; set; }
        public string BookingNo { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? BookingApproveDate { get; set; }
        public DateTime? BookingCancelDate { get; set; }
        public Guid? AgreementID { get; set; }
        public string AgreementNo { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ContractApproveDate { get; set; }
        public DateTime? ContractCancelDate { get; set; }
        public Guid? TransferID { get; set; }
        public string TransferNo { get; set; }
        public DateTime? TransferDate { get; set; }
        public DateTime? ActualTransferDate { get; set; }
        public Guid? TransferPromotionID { get; set; }
        public string TransferPromotionNo { get; set; }
        public DateTime? TransferPromotionDate { get; set; }
        public DateTime? TransferPromotionApproveDate { get; set; }
        public string StatusID { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string StatusName { get; set; }
        public string ModelID { get; set; }
        public string ModelName { get; set; }
        public Guid? UnitID { get; set; }
    }
}
