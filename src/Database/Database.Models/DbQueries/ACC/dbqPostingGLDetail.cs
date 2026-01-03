using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.ACC
{
    public class dbqPostingGLDetail
    {
        public string PostingGLDocumentType { get; set; }
        public Guid? DocumentID { get; set; }
        public string ReceiptTempNo { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string SAPCompanyID { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime PostingDate { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string UnitNo { get; set; }
        public decimal TotalAmount { get; set; }

        public string PostGLType { get; set; }
        public string GLAccount { get; set; }
        public string GLAccountName { get; set; }
        public decimal Amount { get; set; }
        public string PostingKey { get; set; }
        public string GLAccountID { get; set; }
        public string FormatTextFileID { get; set; }
        public string BookingID { get; set; }
        public string AccountCode { get; set; }
        public string WBSNumber { get; set; }
        public string ProfitCenter { get; set; }
        public string CostCenter { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string Assignment { get; set; }
        public string TaxCode { get; set; }
        public string ObjectNumber { get; set; }
        public string CustomerName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public Guid? SessionKey { get; set; }
    }

    public class dbqSessionKeySearch
    {
        public Guid? SessionKey { get; set; }
    }

}