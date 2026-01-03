using Base.DbQueries;
using System;
namespace Database.Models.DbQueries.FIN
{
    public class dbqSPReceiptInfo : BaseDbQueries
    {
        public Guid? ID { get; set; }
        public string ReceiptTempNo { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string Project { get; set; }
        public string Unit { get; set; }
        public string BankAccount { get; set; }
        public string PaymentMethod { get; set; }
        public string ReceiptDescription { get; set; }
        public decimal? Amount { get; set; }
        public string DepositNo { get; set; }
        public string RVNumber { get; set; }
        public bool? ReceiptStatus { get; set; }
        public string CustomerName { get; set; }
        public bool? IsCancel { get; set; }
        public Guid? BookingID { get; set; }

        public Guid? PaymentStateID { get; set; }
        public string PaymentStateKey { get; set; }
        public string PaymentStateDetail { get; set; }
    }
}
