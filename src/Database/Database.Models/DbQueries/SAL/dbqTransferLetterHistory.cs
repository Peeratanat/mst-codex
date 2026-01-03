using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqTransferLetterHistory : BaseDbQueries
    {
        public Guid? TransferLetterID { get; set; }
        public Guid? AgreementID { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? ProjectID { get; set; }
        public DateTime? TransferOwnershipDate { get; set; }
        public Guid? AgreementOwnerID { get; set; }
        public string FullnameTH { get; set; }
        public DateTime? AppointmentTransferDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? TransferLetterDate { get; set; }
        public DateTime? NewTransferOwnershipDate { get; set; }
        public Guid? TransferLetterTypeID { get; set; }
        public string TransferLetterTypeKey { get; set; }
        public string TransferLetterTypeName { get; set; }
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
        public DateTime? PostponeTransferDate { get; set; }
        public Guid? ContactAddressTypeMasterCenterID { get; set; }
        public string ChangeAgreementOwnerType { get; set; }
        public DateTime? MailConfirmCancelSendDate { get; set; }
        public DateTime? MailConfirmCancelResponseDate { get; set; }
        public string MailConfirmCancelResponseTypeName { get; set; }
        public bool? CanCreateMailConfirmCancel { get; set; }
    }
}
