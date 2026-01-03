using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.CMS
{
    public class dbqChangeLCTransferList : BaseDbQueries
    {
        public Guid? TransferID { get; set; }
        public Guid? AgreementID { get; set; }
        public string AgreementNo { get; set; }
        public string TransferNo { get; set; }
        public Guid? TransferSaleUserID { get; set; }
        public string CurrentLCName { get; set; }
        public Guid? TransferOwnerID { get; set; }
        public string CustomerName { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string Remark { get; set; }
        public Guid? OldTransferSaleUserID { get; set; }
        public string OldTransferSaleName { get; set; }

    }
}
