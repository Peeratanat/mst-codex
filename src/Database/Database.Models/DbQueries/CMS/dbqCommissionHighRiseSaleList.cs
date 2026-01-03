using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;


namespace Database.Models.DbQueries.CMS
{
    public class dbqCommissionHighRiseSaleList : BaseDbQueries
    {
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? AgreementID { get; set; }
        public Guid? SaleUserID { get; set; }
        public string SaleEmployeeNo { get; set; }
        public Guid? ProjectSaleUserID { get; set; }
        public string ProjectSaleEmployeeNo { get; set; }
        public Guid? AgentID { get; set; }
        public decimal? CommissionPercentRate { get; set; }
        public Guid? CommissionPercentTypeMasterCenterID { get; set; }
        public string CommissionPercentType { get; set; }
        public decimal? TotalContractNetAmount { get; set; }
        public DateTime? SignAgreementDate { get; set; }
        public decimal? SaleUserSalePaid { get; set; }
        public decimal? ProjectSaleSalePaid { get; set; }
        public decimal? TotalSalePaid { get; set; }
        public decimal? CommissionForThisMonth { get; set; }
        public int? flag { get; set; }
        public DateTime? ReceiptDate { get; set; }
    }
}
