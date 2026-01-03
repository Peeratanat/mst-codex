using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbpDirectCreditDebit : BaseDbQueries
    {
        public Guid? CompanyID { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? AgreementID { get; set; }
        public string AgreementNo { get; set; }
        public string MainOwnerName { get; set; }
        public Guid? BankID { get; set; }
        public string BankName { get; set; }
        public string BankAccNo { get; set; }
        public string AccountNO { get; set; }
        public DateTime? Expire { get; set; } 
        public string ExpireStr { get; set; }
        public string CustomerName { get; set; }
        public DateTime? StartDate { get; set; }
        public string DirectType { get; set; }
        public string DirectTypKey { get; set; }
        public int? DirectPeriod { get; set; }
        public DateTime ? SaveDate { get; set; }
        public string StatusName { get; set; }
        public string StatusKey { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? DirectCreditDebitApprovalFormID { get; set; }
        public string ContactNo { get; set; }
        public string CitizenIdentityNo { get; set; }
        public string Remark { get; set; }
        public decimal? InstallmentAmount { get; set; }
        public DateTime? ApproveDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? RejectDate { get; set; }
    }
}
