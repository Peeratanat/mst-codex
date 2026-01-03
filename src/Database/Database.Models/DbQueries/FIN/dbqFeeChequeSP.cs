using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqFeeChequeSP : BaseDbQueries
    {
        public Guid? ID { get; set; }
        public Guid? CompanyID { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public string DepositNo { get; set; }
        public bool? DepositStatus { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public Guid? BankID { get; set; }
        public string BankName { get; set; }
        public string ChequeNumber { get; set; }
        public string ReceiptTempNo { get; set; }
        public string ChequeTypeName { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Fee { get; set; }
        public decimal? FeePercent { get; set; }
        public decimal? NetAmount { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsFeeConfirm { get; set; }
        public bool? IsPostPI { get; set; }
        public string PINumber { get; set; }
        
    }
}
