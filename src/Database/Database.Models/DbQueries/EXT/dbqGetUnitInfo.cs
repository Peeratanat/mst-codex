using Base.DbQueries;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.EXT
{
    public class dbqGetUnitInfo
    {
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string UnitNo { get; set; }
        public string ContactNo { get; set; }
        public string MainOwnerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? TransferDate { get; set; }
        public string Mobile { get; set; }
        public string SellerID { get; set; }
        public string SellerFullName { get; set; }
        public string ContractNumber { get; set; }
        public string BookingNumber { get; set; }
        

    }
}


