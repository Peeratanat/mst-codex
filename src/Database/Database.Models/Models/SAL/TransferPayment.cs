using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.USR;

namespace Database.Models.SAL
{
    [Description("ค่าใช้จ่าย เคลียร์เงินหลังโอน")]
    [Table("TransferPayment", Schema = Schema.SALE)]
    public class TransferPayment : BaseEntity
    {
		[Description("ID การโอนกรรมสิทธิ์")]
		public Guid TransferID { get; set; }

		[ForeignKey("TransferID")]
		public SAL.Transfer Transfer { get; set; }

		[Description("รหัสโอนกรรมสิทธิ์")]
		public string TransferNo { get; set; }

		[Description("ชื่อบริษัท")]
		public string CompanyName { get; set; }

		[Description("ID บริษัท")]
		public Guid CompanyID { get; set; }

		[ForeignKey("CompanyID")]
		public MST.Company Company { get; set; }

		[Description("ID โครงการ")]
		public Guid ProjectID { get; set; }

		[ForeignKey("ProjectID")]
		public PRJ.Project Project { get; set; }

		[Description("รหัสโครงการ")]
		public string ProjectNo { get; set; }

		[Description("ชื่อโครงการ")]
		public string ProjectName { get; set; }

		[Description("วันที่โอนกรรมสิทธิ์")]
		public DateTime? ActualTransferDate { get; set; }

		[Description("รหัส Unit")]
		public string UnitNo { get; set; }

		[Description("")]
		public decimal CashAmount { get; set; }

		[Description("")]
		public decimal MinistryOfFinanceCheque { get; set; }

		[Description("")]
		public decimal SumChequeAP { get; set; }

		[Description("")]
		public decimal MinistryCash { get; set; }

		[Description("")]
		public decimal TransferToUtil { get; set; }

		[Description("")]
		public decimal SumChequeAPUtil { get; set; }

		[Description("")]
		public decimal APMeterAmount { get; set; }

		[Description("")]
		public decimal APLandsAmount { get; set; }

		[Description("")]
		public decimal RemainingAPAmount { get; set; }

		[Description("")]
		public decimal AllIncomeTax { get; set; }

		[Description("")]
		public decimal FeeAPAmount { get; set; }

		[Description("")]
		public decimal FeeCustomerAmount { get; set; }

		[Description("")]
		public decimal FeeAccountReturnAmount { get; set; }

		[Description("")]
		public decimal GreetingAmount { get; set; }

		[Description("")]
		public decimal XeroxAmount { get; set; }

		[Description("")]
		public decimal FareAmount { get; set; }

		[Description("")]
		public decimal FeeMortgageAmount { get; set; }

		[Description("")]
		public decimal CashChangeAmount { get; set; }

		[Description("")]
		public decimal ChangeAmount { get; set; }

		[Description("")]
		public decimal ReceiveCash { get; set; }

		[Description("")]
		public decimal CashAPAmount { get; set; }

		[Description("")]
		public decimal ChequeAPAmount { get; set; }

		[Description("")]
		public decimal TransferAPAmount { get; set; }

		[Description("")]
		public decimal ChequeAPLandsAmount { get; set; }

		[Description("")]
		public decimal CashAPMeterAmount { get; set; }

		[Description("")]
		public decimal SumAPMeterAmount { get; set; }

		[Description("")]
		public decimal TransferAPMeterAmount { get; set; }

		[Description("")]
		public decimal ChequeAPMeterAmount { get; set; }

		[Description("")]
		public decimal APAmount { get; set; }

		[Description("")]
		public decimal Change { get; set; }

		[Description("")]
		public decimal Ministry { get; set; }

		[Description("")]
		public decimal ReturnAmount { get; set; }

		[Description("")]
		public decimal ChangeCustomer { get; set; }

		[Description("")]
		public decimal SumChangeCustomer { get; set; }

		[Description("")]
		public decimal ChangeTransfer { get; set; }

		[Description("")]
		public decimal SumChangeTransfer { get; set; }

		[Description("")]
		public decimal SumFeeAcountReturnAmount { get; set; }

		[Description("")]
		public decimal SumGreetingAmount { get; set; }

		[Description("")]
		public decimal SumXeroxAmount { get; set; }

		[Description("")]
		public decimal SumFareAmount { get; set; }

		[Description("")]
		public decimal SumFeeMortgageAmount { get; set; }

		[Description("")]
		public decimal SumAccountReturn { get; set; }

		[Description("")]
		public decimal SumCashUnit { get; set; }

		[Description("")]
		public decimal SumCash { get; set; }

		[Description("")]
		public decimal SumCashChangeAmount { get; set; }
	}
}
