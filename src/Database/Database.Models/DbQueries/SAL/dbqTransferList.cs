using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqTransferList : BaseDbQueries
    {
        public Guid? TransferID { get; set; }
        public string TransferNo { get; set; }
        public DateTime ScheduleTransferDate { get; set; }
        public DateTime? ActualTransferDate { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? TransferStatusID { get; set; }
        public Guid? TransferOwnerID { get; set; }
        public Guid? CreditBankingTypeID { get; set; }
        public string CreditBankingTypeKey { get; set; }
        public string CreditBankingStatusName { get; set; }
        public string BankName { get; set; }

        public Guid? AgreementID { get; set; }
        public Guid? AgreementOwnerID { get; set; }
        public string OwnerName { get; set; }
        public string MarriageStatusKey { get; set; }
        public Guid? MarriageStatusID { get; set; }
        public string TransferStatusKey { get; set; }
        public bool? IsAssignAuthority { get; set; }

    }
}
