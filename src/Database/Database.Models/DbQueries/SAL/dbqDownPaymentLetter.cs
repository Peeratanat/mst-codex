using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqDownPaymentLetter : BaseDbQueries
    {
        public bool? IsSelected { get; set; }
        public Guid? AgreementID { get; set; }
        public Guid? ProjectID { get; set; }
        public string ChangeAgreementOwnerType { get; set; }
        public string ProjectType { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public string DisplayName { get; set; }
        public int? CountDueAmount { get; set; }
        public decimal? SumDueAmount { get; set; }
        public DateTime? LastReceiveDate { get; set; }
        public bool? IsOverTwelvePointFivePercent { get; set; }
        public string NowDownPaymentLetterType { get; set; }
        public Guid? NowDownPaymentLetterID { get; set; }
        public string NowDownPaymentLetterName { get; set; }
        public string LastDownPaymentLetterType { get; set; }
        public Guid? LastDownPaymentLetterTypeID { get; set; }
        public string LastDownPaymentLetterTypeName { get; set; }
        public DateTime? LastResponseDate { get; set; }
        public string LastLetterStatusName { get; set; }
        public int? RemainDownPeriodStart { get; set; }
        public int? RemainDownPeriodEnd { get; set; }
        public int? CTM2Amount { get; set; }
        public string LastTransferLetterType { get; set; }
        public Guid? LastTransferLetterTypeID { get; set; }
        public string LastTransferLetterTypeName { get; set; }
        public string PostalCode { get; set; }
        public string CountryNameTH { get; set; }
        public string PostTrackingNo { get; set; }
        public Guid ContactID { get; set; }
        public Guid? LastDownPaymentLetterID { get; set; }
        public bool? WaitForCancel { get; set; }    
        public bool? CanCreateLetter { get; set; }
        public int? InstallmentAmount { get; set; }
        public int? CTM2Amount_2 { get; set; }
        public DateTime? LastCancelDownPaymentLetterDate { get; set; }
        public string AllContactID { get; set; }
        public string AgreementStatusName { get; set; }
        public DateTime? MailConfirmCancelSendDate { get; set; }
        public DateTime? MailConfirmCancelResponseDate { get; set; }
        public string MailConfirmCancelResponseTypeName { get; set; }
        public bool? CanCreateMailConfirmCancel { get; set; }
        public bool? IsHaveLastReceiveDate { get; set; }
        public decimal? UnknownPaymentAmount { get; set; }
        public DateTime? LastDownPaymentLetterDate { get; set; }


    }
}
