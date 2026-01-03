using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.CMS
{
    public class dbqCommissionHighRiseTransferList : BaseDbQueries
    {
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? TransferID { get; set; }
        public Guid? AgreementID { get; set; }
        public Guid? LCTransferID { get; set; }
        public string LCTransferEmployeeNo { get; set; }
        public decimal? CommissionPercentRate { get; set; }
        public Guid? CommissionPercentTypeMasterCenterID { get; set; }
        public string CommissionPercentType { get; set; }
        public decimal? NetSellPrice { get; set; }
        public DateTime? TransferDate { get; set; }
        public decimal? LCTransferPaid { get; set; }
        public decimal? CommissionForThisMonth { get; set; }

    }
}
