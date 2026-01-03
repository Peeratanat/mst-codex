using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqDownPaymentLetterHistory : BaseDbQueries
    {
        public Guid? DownPaymentLetterID { get; set; }
        public Guid? AgreementID { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? AgreementOwnerID { get; set; }
        public string FullnameTH { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? DownPaymentLetterDate { get; set; }
        public Guid? DownPaymentLetterTypeID { get; set; }
        public string DownPaymentLetterTypeKey { get; set; }
        public string DownPaymentLetterTypeName { get; set; }
        public Guid? LetterStatusID { get; set; }
        public string LetterStatusKey { get; set; }
        public string LetterStatusName { get; set; }
        public Guid? LetterReasonResponseID { get; set; }
        public string LetterReasonResponseKey { get; set; }
        public string LetterReasonResponseName { get; set; }
        public string LetterReasonResponseNameEN { get; set; }
        public Guid? PostTrackingID { get; set; }
        public string PostTrackingNo { get; set; }
        public bool? IsUsed { get; set; }
        public DateTime? MailConfirmCancelSendDate { get; set; }
        public DateTime? MailConfirmCancelResponseDate { get; set; }
        public string MailConfirmCancelResponseTypeName { get; set; }
        public bool? CanCreateMailConfirmCancel { get; set; }
        public string DownPaymentLetterNo { get; set; }
        public int RemainDownPeriodCount { get; set; }
        public decimal RemainDownTotalAmount { get; set; }
        public int? InstallmentAmount { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? TotalPeriodOverDue { get; set; }
        public decimal? TotalAmountOverDue { get; set; }
    }
}
