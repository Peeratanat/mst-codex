using Base.DbQueries;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.EXT
{
    public class dbqExecutiveReportTotalBooking
    {
        public Guid? ProjectID { get; set; }
        public string ProductID { get; set; }
        public string Project { get; set; }
        public string BU { get; set; }
        public DateTime? StartSale { get; set; }
        public int? TotalUnit { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? EmptyUnit { get; set; }
        public decimal? EmptyPrice { get; set; }
        public decimal? BookingTotalPrice { get; set; }
        public int? BookingTotalUnit { get; set; }
        public decimal? CancelTotalPrice { get; set; }
        public int? CancelTotalUnit { get; set; }
        public decimal? ContractPrice { get; set; }
        public decimal? TransferDiscount { get; set; }
        public decimal? AreaPrice { get; set; }
        public decimal? ExtraDiscount { get; set; }
        public decimal? TransferTotalPrice { get; set; }
        public int? TransferTotalUnit { get; set; }
        public decimal? PTDBookingTotalPrice { get; set; }
        public int? PTDBookingTotalUnit { get; set; }
        public decimal? PTDTransferTotalPrice { get; set; }
        public int? PTDTransferTotalUnit { get; set; }
        public string ProjectGroup { get; set; }


    }
}


