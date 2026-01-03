using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.ACC
{
    public class dbqPostGLData : BaseDbQueries
    {
        public Guid? PostGLHeaderID { get; set; }
        public string DocumentNo { get; set; }
        public string ReferentNo { get; set; }
        public Guid? PostGLTypeID { get; set; }
        public string PostGLTypeName { get; set; }
        public string PostGLTypeKey { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime? DocumentDate { get; set; }
        public Guid? CompanyID { get; set; }
        public string SAPCompanyID { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? BankAccountID { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalFee { get; set; }
        public decimal? TotalBalance { get; set; }
        public string PostedBy { get; set; }
        public DateTime? Created { get; set; }
        public bool? IsCancel { get; set; }
        public string CancelReferentNo { get; set; }

        public decimal? SumAmount { get; set; }
        public decimal? SumFee { get; set; }
    }
}