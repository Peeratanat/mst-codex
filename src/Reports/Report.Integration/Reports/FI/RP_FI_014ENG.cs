using System;
namespace Report.Integration.Reports.FI
{
    public class RP_FI_014ENG
    {
        /// <summary>
        /// หนังสือรับรองการชำระเงิน (ภาษาอังกฤษ)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public string UserName { get; set; }
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
        public string PaymentType { get; set; }
        public string PaymentType2 { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
    }
}
