using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqSPPaymentInfoList : BaseDbQueries
    {
        public Guid ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }

        public Guid UnitID { get; set; }
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public Guid? UnitStatusID { get; set; }
        public string UnitStatusKey { get; set; }

        public Guid? BookingID { get; set; }
        public string BookingNo { get; set; }
        public DateTime? BookingDate { get; set; }

        public Guid? AgreementID { get; set; }
        public string AgreementNo { get; set; }
        public DateTime? AgreementDate { get; set; }

        public Guid? TransferID { get; set; }
        public string TransferNo { get; set; }
        public DateTime? TransferDate { get; set; }

        public string CustomerName { get; set; }

        public bool IsPreTransferPayment { get; set; }
    }
}
