using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ROI
{
    [Description("VWUnitStatus")] 
    public class VWUnitStatus 
    {
        public string BG { get; set; }
        public string SubBG { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? BookingID { get; set; }
        public string BookingNo { get; set; }
        public DateTime? BookingDate { get; set; }
        public Guid? AgreementID { get; set; }
        public string AgreementNo { get; set; }
        public DateTime? SignContractApprovedDate { get; set; }
        public DateTime? TransferDateInContract { get; set; }
        public Guid? TransferID { get; set; }
        public string TransferNo { get; set; }
        public DateTime? ScheduleTransferDate { get; set; }
        public DateTime? ActualTransferDate { get; set; }
        public string UnitStatus { get; set; }
        public string SAPWBSNo { get; set; }
    }
}
