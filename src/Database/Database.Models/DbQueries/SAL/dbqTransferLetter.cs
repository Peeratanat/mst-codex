using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqTransferLetter : BaseDbQueries
    {
        public bool? IsSelected { get; set; }
        public Guid? AgreementID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public string ChangeAgreementOwnerType { get; set; }
        public string ProjectType { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string UnitNo { get; set; }
        public string DisplayName { get; set; }
        public string NowTransferLetterType { get; set; }
        public Guid? NowTransferLetterID { get; set; }
        public string NowTransferLetterName { get; set; }
        public string LastTransferLetterType { get; set; }
        public Guid? LastTransferLetterTypeID { get; set; }
        public string LastTransferLetterTypeName { get; set; }
        public DateTime? LastResponseDate { get; set; }
        public string LastLetterStatusName { get; set; }
        public DateTime? TransferOwnershipDate { get; set; }
        public DateTime? AppointmentTransferDate { get; set; }
        public DateTime? AppointmentTransferTime { get; set; }
        public string LastDownPaymentLetterType { get; set; }
        public Guid? LastDownPaymentLetterTypeID { get; set; }
        public string LastDownPaymentLetterTypeName { get; set; }
        public string PostalCode { get; set; }
        public string CountryNameTH { get; set; }
        public string PostTrackingNo { get; set; }
        public DateTime? NewTransferOwnershipDate { get; set; }
        public DateTime? PostponeTransferDate { get; set; }
        public Guid ContactID { get; set; }
        public Guid? LastTransferLetterID { get; set; }
        public bool? CanCreateLetter { get; set; }
        public DateTime? LastTransferLetterDate { get; set; }
        public bool? WaitForCancel { get; set; }
        public string AllContactID { get; set; }
        public string AgreementStatusName { get; set; }
        public DateTime? MailConfirmCancelSendDate { get; set; }
        public DateTime? MailConfirmCancelResponseDate { get; set; }
        public string MailConfirmCancelResponseTypeName { get; set; }
        public bool? CanCreateMailConfirmCancel { get; set; }
        public int? CountDueAmount { get; set; }
    }
}
