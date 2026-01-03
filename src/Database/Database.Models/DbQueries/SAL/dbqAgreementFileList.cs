using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqAgreementFileList : BaseDbQueries
    {   public Guid BookingID { get; set; }
        public DateTime? BookingDate { get; set; }
        public string BookingNo { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string UnitNo { get; set; }
        public Guid? AgreementID { get; set; }
        public string AgreementNo { get; set; }
        public bool? IsPrintApproved { get; set; }
        public Guid? PrintApprovedByUserID { get; set; }
        public DateTime? PrintApprovedDate { get; set; }
        public string FileName { get; set; }
        public string DocType { get; set; }
        public string DocYearMonth { get; set; }
        public string FilePath { get; set; }
        public string Uploader { get; set; }
        public DateTime? UploadDate { get; set; }
        public string AGOwnerName { get; set; }
        public string Remark { get; set; }
        public Guid? AgreementFileID { get; set; }
    }
}
