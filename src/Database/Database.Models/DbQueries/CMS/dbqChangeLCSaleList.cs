using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.CMS
{
    public class dbqChangeLCSaleList : BaseDbQueries
    {
        public Guid? AgreementID { get; set; }
        public string AgreementNo { get; set; }
        public string BookingNo { get; set; }
        public Guid? SaleUserID { get; set; }
        public string CurrentLCName { get; set; }
        public Guid? AgreementOwnerID { get; set; }
        public string CustomerName { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string Remark { get; set; }
        public Guid? OldSaleOfficerTypeMasterCenterID { get; set; }
        public Guid? OldSaleUserID { get; set; }
        public string OldSaleName { get; set; }
        public Guid? OldProjectSaleUserID { get; set; }
        public string OldProjectSaleName { get; set; }
        public Guid? OldAgentID { get; set; } 
        public string OldAgentName { get; set; }
        public Guid? OldAgentEmployeeID { get; set; }
        public string OldAgentEmployeeName { get; set; }

    }
}
