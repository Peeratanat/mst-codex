using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.PRJ
{
    public class dbqGetMasterPlanUnitDetail
    {

        public string ProjectNameTH { get; set; }
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public double? SalePrice { get; set; }
        public double? SaleArea { get; set; }
        public string CustomerName { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? TransferDate { get; set; }
        public DateTime? QCPassDate { get; set; }
        public DateTime? InspectionDate { get; set; }
    }
}
