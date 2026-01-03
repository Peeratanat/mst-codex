using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqFETDetailListSP : BaseDbQueries
    {
        public Guid? ID { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? FETStatusID { get; set; }
        public string FETStatusName { get; set; }
        public string FETStatusKey { get; set; }
        public Guid? FETRequesterID { get; set; }
        public string FETRequesterName { get; set; }
        public string FETRequesterKey { get; set; }

        public Guid? PaymentMethodID { get; set; }
        public Guid? PaymentMethodTypeID { get; set; }
        public string PaymentMethodTypeName { get; set; }
        public string PaymentMethodTypeKey { get; set; }
        public string DepositNo { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Guid? BookingID { get; set; }
        public string RejectRemark { get; set; }
        public bool IsReject { get; set; }
        public string CancelRemark { get; set; }
        public Guid? BankID { get; set; }
        public string BankName { get; set; }
        public string CustomerName { get; set; }
        public string ReceiptTempNo { get; set; }
        public string AttachFile { get; set; }
        public string AttachFileName { get; set; }
        public bool IsCancel { get; set; } 
        public string IR { get; set; }
        public Guid? BankConID { get; set; }
        public string BankConName { get; set; }        
        public Guid? ForeignTransferTypeID { get; set; }
        public string ForeignTransferType { get; set; }

    }
}
