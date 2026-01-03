using System;
using Database.Models.DbQueries.FIN;

namespace Base.DTOs.FIN

{
    public class UnknownPaymentAutoCCDTO
    {
        public Guid? ID { get; set; }
        public decimal? Amount { get; set; }
        public Guid? BookingID { get; set; }
        public string BookingInfo { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public Guid? BankAccountID { get; set; }
        public string CancelRemark { get; set; }
        public string Remark { get; set; }
        public Guid? UnknownPaymentStatusID { get; set; }
        public string SAPRemark { get; set; }
        public string UnknownPaymentCode { get; set; }
        public Guid? BankID { get; set; }
        public Guid? CreditCardPaymentTypeMasterCenterID { get; set; }
        public Guid? CreditCardTypeMasterCenterID { get; set; }
        public Guid? EDCBankID { get; set; }
        public decimal? Fee { get; set; }
        public Guid? FeeConfirmByUserID { get; set; }
        public DateTime? FeeConfirmDate { get; set; }
        public decimal? FeeIncludingVat { get; set; }
        public decimal? FeePercent { get; set; }
        public Guid? ForeignTransferTypeMasterCenterID { get; set; }
        public string IR { get; set; }
        public bool? IsFeeConfirm { get; set; }
        public bool? IsForeignCreditCard { get; set; }
        public bool? IsNotifyFET { get; set; }
        public bool? IsRequestFET { get; set; }
        public bool? IsWrongAccount { get; set; }
        public string NotifyFETMemo { get; set; }
        public string Number { get; set; }
        public Guid? PaymentMethodTypeMasterCenterID { get; set; }
        public string TransferorName { get; set; }
        public decimal? Vat { get; set; }
        public Guid? CompanyID { get; set; }
        public DateTime? PostGLDate { get; set; }
        public string PostGLDocumentNo { get; set; }
        public Guid? BillPaymentDetailID { get; set; }
        public Guid? ProjectID { get; set; }
        public bool? IsRefund { get; set; }
        public DateTime? RefundDate { get; set; }
        public Guid? RefundUserID { get; set; }
        public int? IsDownPaymentLetter { get; set; }
        public Guid? DownPaymentLetterID { get; set; }
        public string DownPaymentLetterNo { get; set; }
        public DateTime? DownPaymentLetterDate { get; set; }
        public string DownPaymentLetterTypDesc { get; set; }
        public decimal? RemainDownTotalAmount { get; set; }
        public int? IsTransferLetter { get; set; }
        public Guid? TransferLetterID { get; set; }
        public string TransferLetterNo { get; set; }
        public DateTime? TransferLetterDate { get; set; }
        public string TransferLetterTypDesc { get; set; }
        public string CompanyName { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountTypeDesc { get; set; }
        public string BankCode { get; set; }
        public string UnknownPaymentStatusDesc { get; set; }
        public string PaymentMethodTypeDesc { get; set; }
        public decimal? TotalUnknownPayment { get; set; }
        public decimal? TotalRemainUnknownPayment { get; set; }
        public bool? IsPostGL { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? AgreementID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string CreatedByUserDisplayName { get; set; }
        public int? TotalPeriodOverDue { get; set; }
        public decimal? TotalAmountOverDue { get; set; }
        public string PostCANo { get; set; }
        public Guid? DownPaymentLetterTypeMasterCenterID { get; set; }
        public Guid? TransferLetterTypeMasterCenterID { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedByUserID { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedByUserID { get; set; }
        public string UpdateddByUser { get; set; }
        public string BankAccountDisplayName { get; set; }

        public static UnknownPaymentAutoCCDTO CreateFromDBQuery(dbqSPSearchvwUnknownPaymentLetter model)
        {
            if (model != null)
            {
                var result = new UnknownPaymentAutoCCDTO();

                result.ID = model.ID;
                result.Amount = model.Amount;
                result.BookingID = model.BookingID;
                result.ReceiveDate = model.ReceiveDate;
                result.BankAccountID = model.BankAccountID;
                result.Created = model.Created;
                result.Updated = model.Updated;
                result.IsDeleted = model.IsDeleted;
                result.CreatedByUserID = model.CreatedByUserID;
                result.UpdatedByUserID = model.UpdatedByUserID;
                result.CancelRemark = model.CancelRemark;
                result.Remark = model.Remark;
                result.UnknownPaymentStatusID = model.UnknownPaymentStatusID;
                result.SAPRemark = model.SAPRemark;
                result.UnknownPaymentCode = model.UnknownPaymentCode;
                result.BankID = model.BankID;
                result.CreditCardPaymentTypeMasterCenterID = model.CreditCardPaymentTypeMasterCenterID;
                result.CreditCardTypeMasterCenterID = model.CreditCardTypeMasterCenterID;
                result.EDCBankID = model.EDCBankID;
                result.Fee = model.Fee;
                result.FeeConfirmByUserID = model.FeeConfirmByUserID;
                result.FeeConfirmDate = model.FeeConfirmDate;
                result.FeeIncludingVat = model.FeeIncludingVat;
                result.FeePercent = model.FeePercent;
                result.ForeignTransferTypeMasterCenterID = model.ForeignTransferTypeMasterCenterID;
                result.IR = model.IR;
                result.IsFeeConfirm = model.IsFeeConfirm;
                result.IsForeignCreditCard = model.IsForeignCreditCard;
                result.IsNotifyFET = model.IsNotifyFET;
                result.IsRequestFET = model.IsRequestFET;
                result.IsWrongAccount = model.IsWrongAccount;
                result.NotifyFETMemo = model.NotifyFETMemo;
                result.Number = model.Number;
                result.PaymentMethodTypeMasterCenterID = model.PaymentMethodTypeMasterCenterID;
                result.TransferorName = model.TransferorName;
                result.Vat = model.Vat;
                result.CompanyID = model.CompanyID;
                result.PostGLDate = model.PostGLDate;
                result.PostGLDocumentNo = model.PostGLDocumentNo;
                result.BillPaymentDetailID = model.BillPaymentDetailID;
                result.ProjectID = model.ProjectID;
                result.IsRefund = model.IsRefund;
                result.RefundDate = model.RefundDate;
                result.RefundUserID = model.RefundUserID;
                result.IsDownPaymentLetter = model.IsDownPaymentLetter;
                result.DownPaymentLetterID = model.DownPaymentLetterID;
                result.DownPaymentLetterNo = model.DownPaymentLetterNo;
                result.DownPaymentLetterDate = model.DownPaymentLetterDate;
                result.DownPaymentLetterTypDesc = model.DownPaymentLetterTypDesc;
                result.RemainDownTotalAmount = model.RemainDownTotalAmount;
                result.IsTransferLetter = model.IsTransferLetter;
                result.TransferLetterID = model.TransferLetterID;
                result.TransferLetterNo = model.TransferLetterNo;
                result.TransferLetterDate = model.TransferLetterDate;
                result.TransferLetterTypDesc = model.TransferLetterTypDesc;
                result.CompanyName = model.CompanyName;
                result.BankAccountNo = model.BankAccountNo;
                result.BankAccountTypeDesc = model.BankAccountTypeDesc;
                result.BankCode = model.BankCode;
                result.UnknownPaymentStatusDesc = model.UnknownPaymentStatusDesc;
                result.PaymentMethodTypeDesc = model.PaymentMethodTypeDesc;
                result.TotalUnknownPayment = model.TotalUnknownPayment;
                result.TotalRemainUnknownPayment = model.TotalRemainUnknownPayment;
                result.IsPostGL = model.IsPostGL;
                result.UnitID = model.UnitID;
                result.UnitNo = model.UnitNo;
                result.AgreementID = model.AgreementID;
                result.ProjectNo = model.ProjectNo;
                result.ProjectName = model.ProjectName;
                result.CreatedByUserDisplayName = model.CreatedByUserDisplayName;
                result.TotalPeriodOverDue = model.TotalPeriodOverDue;
                result.TotalAmountOverDue = model.TotalAmountOverDue;
                result.PostCANo = model.PostCANo;
                result.DownPaymentLetterTypeMasterCenterID = model.DownPaymentLetterTypeMasterCenterID;
                result.TransferLetterTypeMasterCenterID = model.TransferLetterTypeMasterCenterID;
                result.UpdateddByUser = model.UpdateddByUser;
                result.BankAccountDisplayName = model.BankAccountDisplayName;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

