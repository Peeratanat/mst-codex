using Database.Models;
using Database.Models.DbQueries.ADM;
using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.PRJ;
using System.ComponentModel;
using Base.DTOs.MST;
using Database.Models.PRJ;
using Database.Models.MST;
using Database.Models.FIN;
using Database.Models.SAL;
using static Database.Models.ExtensionAttributes;

namespace Base.DTOs.ADM
{
    public class ChequePaymentDTO : BaseDTOFromAdmin
    {
        [DeviceInformation("โครงการ", "")]
        public ProjectDropdownDTO Project { get; set; }

        [DeviceInformation("เลขที่แปลง", "")]
        public string UnitNo { get; set; }

        [DeviceInformation("สั่งจ่ายให้บริษัท", "SAL.TransferCheque.PayToCompanyID,FIN.PaymentMethod.PayToCompany")]
        public CompanyDropdownDTO PayToCompany { get; set; }

        [DeviceInformation("ผิดบัญชี หรือ ผิดบริษัท", "SAL.TransferCheque.IsWrongCompany,FIN.PaymentMethod.IsWrongAccount")]
        public bool? IsWrongAccount { get; set; }

        [DeviceInformation("วันที่หน้าเช็ค", "SAL.TransferCheque.PayDate,FIN.PaymentMethod.ChequeDate")]
        public DateTime? ChequeDate { get; set; }

        [DeviceInformation("จ่ายให้...", "")]
        public MasterCenterDropdownDTO PaymentReceiver { get; set; }

        [DeviceInformation("ธนาคาร", "SAL.TransferCheque.BankID,FIN.PaymentMethod.BankID")]
        public BankDropdownDTO Bank { get; set; }

        [DeviceInformation("สาขาธนาคาร", "FIN.PaymentMethod.BankBranchName")]
        public string BankBranchName { get; set; }

        [DeviceInformation("เลขที่เช็ค,เลขที่บัตรเครดิต", "SAL.TransferCheque.ChequeNo,FIN.PaymentMethod.Number")]
        public string Number { get; set; }

        [DeviceInformation("จำนวนเงินที่ชำระ", "")]
        public decimal PayAmount { get; set; }

        [DeviceInformation("วันที่ชำระ", "")]
        public DateTime? ReceiveDate { get; set; }

        [DeviceInformation("เป็นใบเสร็จก่อนโอน", "")]
        public bool IsBeforeTransfer { get; set; }

        [DeviceInformation("PaymentMethod.ID || TransferCheques.ID", "")]
        public Guid? Id { get; set; } = new Guid();

        public static ChequePaymentDTO CreateFromQueryResult(ChequePaymentQueryResult model )
        {
            if (model != null)
            {
                ChequePaymentDTO result = new ChequePaymentDTO();

                //result.Id = model.UnitID; 
                result.Project           = ProjectDropdownDTO.CreateFromModel( model.Project) ;
                result.UnitNo            = model.Unit.UnitNo ;
                result.PayToCompany      = CompanyDropdownDTO.CreateFromModel( model.PayToCompany);
                result.IsWrongAccount    = model.IsWrongAccount;
                result.ChequeDate        = model.ChequeDate;
                result.PaymentReceiver   = MasterCenterDropdownDTO.CreateFromModel( model.PaymentReceiver);
                result.Bank              = BankDropdownDTO.CreateFromModel(model.Bank);
                result.BankBranchName    = model.BankBranchName;
                result.Number            = model.Number;
                result.PayAmount         = model.PayAmount;
                result.ReceiveDate       = model.ReceiveDate;
                result.IsBeforeTransfer  = model.IsBeforeTransfer;
                result.Id = model.Id;
                return result;
            }
            else
            {
                return null;
            }
        }

        public void UpdateToPaymentMethodModel(ref PaymentMethod model)
        {
            model.ChequeDate = this.ChequeDate; 
            model.Number = this.Number; 
            model.PayToCompanyID = this.PayToCompany?.Id; 
            model.BankID = this.Bank?.Id ; 
            model.BankBranchName = this.BankBranchName;
            model.IsWrongAccount = this.IsWrongAccount;
        }
        public void UpdateToTransferChequesModel(ref TransferCheque model)
        {
            model.PayDate = this.ChequeDate;
            model.ChequeNo = this.Number;
            model.PayToCompanyID = this.PayToCompany?.Id;
            model.BankID = this.Bank?.Id;
            model.IsWrongCompany = this.IsWrongAccount ?? false;
        }
    }
    public class ChequePaymentQueryResult
    {
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public Company PayToCompany { get; set; }
        public MasterCenter PaymentReceiver { get; set; }
        public Bank Bank { get; set; }
        public string Number { get; set; }
        public string BankBranchName { get; set; }
        public DateTime? ChequeDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public decimal PayAmount { get; set; }
        public Guid? Id { get; set; } = new Guid();
        public bool IsBeforeTransfer { get; set; }
        public bool? IsWrongAccount { get; set; }
    }
}
