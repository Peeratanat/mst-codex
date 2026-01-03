using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class sp_CheckPaidDownPayment
    {
        public int? StatusCode { get; set; }
        public string StatusMsg { get; set; }
        public Guid? DownPaymentLetterID { get; set; }
        public string DownPaymentLetterNo { get; set; }
        public int? TotalPeriodOverDue { get; set; }
        public decimal? TotalAmountOverDue { get; set; }
        public bool? IsProjectTransferLetter { get; set; }
        public decimal? UnknowPaymentWaitingAmount { get; set; }
        public decimal? TotalPaidAfterDownPaymentDate { get; set; }
        public decimal? TotalNetPaidAmount { get; set; }
    }
}
