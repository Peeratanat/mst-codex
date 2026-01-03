using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqBookingOfflineList : BaseDbQueries
    {
        public Guid? ID { get; set; }
        public string BookingNumber { get; set; }
        public Guid? ProjectID { get; set; }
        public string UnitNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? ContractDueDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ReceiptNo { get; set; }
        public decimal? SellPrice { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public string ConfirmBy { get; set; }
        public string BookingType { get; set; }
        public bool? IsConfirm { get; set; }
        public bool? IsCancel { get; set; }
        public Guid UnitID { get; set; }
    }
}
