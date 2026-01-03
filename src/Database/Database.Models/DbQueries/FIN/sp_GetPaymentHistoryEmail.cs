using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.Finance
{
    public class sp_GetPaymentHistoryEmail
    {
        public Guid ReceiptHeaderID { get; set; }
        public string EmailCustomer { get; set; }
        public string CompanyNameTH { get; set; }
        public string ProjectNameTH { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
