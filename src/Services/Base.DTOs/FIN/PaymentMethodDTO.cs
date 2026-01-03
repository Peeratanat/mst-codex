using Base.DTOs.MST;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    /// <summary>
    /// ช่องทางชำระ
    /// Model: PaymentMethod
    /// </summary>
    public class PaymentMethodDTO : BaseDTO
    {
        /// <summary>
        /// จำนวนเงินที่จ่าย
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// ชนิดของช่องทางชำระ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PaymentMethod
        /// </summary>
        [Description("ชนิดของช่องทางชำระ")]
        public MasterCenterDropdownDTO PaymentMethodType { get; set; }

        [Description("ผูกกับการชำระเงิน")]
        public Guid PaymentID { get; set; }


        /* ------- โครงสร้างใหม่ ณ 2019-01-17 ---------------------------- */
        //เลขที่เช็ค,เลขที่บัตรเครดิต => CashierCheque, PersonalCheque, CreditCard, DebitCard
        [Description("เลขที่เช็ค,เลขที่บัตรเครดิต")]
        public string Number { get; set; }

        //บัญชีธนาคารที่โอนเข้า => เงินโอนผ่านธนาคาร,ForeignBankTransfer, QR
        [Description("บัญชีธนาคาร")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        //ธนาคาร => CashierCheque,PersonalCheque, CreditCard, DebitCard
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }

        //สาขาธนาคาร => CashierCheque, PersonalCheque
        [Description("สาขาธนาคาร")]
        public string BankBranchName { get; set; }

        //สั่งจ่ายให้บริษัท => CashierCheque, PersonalCheque
        [Description("สั่งจ่ายให้บริษัท")]
        public CompanyDropdownDTO PayToCompany { get; set; }

        //ผิดบัญชี หรือ ผิดบริษัท => เงินโอนผ่านธนาคาร, BillPayment, ForeignBankTransfer, QR, CashierCheque, PersonalCheque, CreditCard, DebitCard
        [Description("ผิดบัญชี หรือ ผิดบริษัท")]
        public bool? IsWrongAccount { get; set; }

        //วันที่หน้าเช็ค => CashierCheque,PersonalCheque
        [Description("วันที่หน้าเช็ค")]
        public DateTime? ChequeDate { get; set; }

        //ค่าธรรมเนียม => CashierCheque,PersonalCheque,CreditCard,DebitCard,ForeignBankTransfer,QR
        [Description("ค่าธรรมเนียม")]
        public decimal? Fee { get; set; }

        // CashierCheque,PersonalCheque,CreditCard,DebitCard
        [Description("สถานะตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
        public bool? IsFeeConfirm { get; set; }

        //วันที่ ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก
        [Description("วันที่ ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
        public DateTime? FeeConfirmDate { get; set; }

        //ผู้ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก
        [Description("ผู้ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
        public UserDTO FeeConfirmByUser { get; set; }

        //เปอร์เซ็นต์ธรรมเนียม
        [Description("เปอร์เซ็นต์ธรรมเนียม")]
        public decimal? FeePercent { get; set; }

        //Vat => CreditCard
        [Description("Vat")]
        public decimal? Vat { get; set; }

        //ค่าธรรมเนียม (หลัง Vat)
        [Description("ค่าธรรมเนียม (หลัง Vat)")]
        public decimal? FeeIncludingVat { get; set; }

        //เป็นบัตรต่างประเทศหรือไม่ => CreditCard
        [Description("เป็นบัตรต่างประเทศหรือไม่")]
        public bool? IsForeignCreditCard { get; set; }

        //รูปแบบการจ่ายเงิน (รูดเต็ม หรือ ผ่อน) => CreditCard, DebitCard
        [Description("รูปแบบการจ่ายเงิน (รูดเต็ม หรือ ผ่อน)")]
        public MasterCenterDropdownDTO CreditCardPaymentType { get; set; }

        //ประเภทบัตร (Visa, Master, JCB) => CreditCard, DebitCard
        [Description("ประเภทบัตร (Visa, Master, JCB)")]
        public MasterCenterDropdownDTO CreditCardType { get; set; }

        //ธนาคารของเครื่องรูดบัตร => CreditCard,DebitCard
        [Description("ธนาคารของเครื่องรูดบัตร")]
        public BankDropdownDTO EDCBank { get; set; }

        [Description("หมายเหตุยกเลิก")]
        public string CancelRemark { get; set; }

        //ประเภทการโอนเงินต่างประเทศ => ForeignBankTransfer
        [Description("ประเภทการโอนเงินต่างประเทศ")]
        public MasterCenterDropdownDTO ForeignTransferType { get; set; }

        //IR => ForeignBankTransfer
        [Description("IR")]
        public string IR { get; set; }

        //ชื่อผู้โอน => ForeignBankTransfer
        [Description("ชื่อผู้โอน")]
        public string TransferorName { get; set; }

        //ต้องขอ FET หรือไม่ => ForeignBankTransfer
        [Description("ต้องขอ FET หรือไม่")]
        public bool? IsRequestFET { get; set; }

        //แจ้งแก้ไข FET => ForeignBankTransfer
        [Description("แจ้งแก้ไข FET")]
        public bool? IsNotifyFET { get; set; }

        //ข้อความแจ้งเตือน => ForeignBankTransfer
        [Description("ข้อความแจ้งเตือน")]
        public string NotifyFETMemo { get; set; }

        //ผูกกับรายการผลการตัดเงินอัตโนมัติ => DirectCreditDebit
        [Description("ผูกกับรายการผลการตัดเงินอัตโนมัติ")]
        public Guid? DirectCreditDebitExportDetailID { get; set; }

        //ผูกข้อมูลรายการผลการชำระเงิน => BillPayment
        [Description("ผูกข้อมูลรายการผลการชำระเงิน")]
        public Guid? BillPaymentDetailID { get; set; }

        //ID เงินโอนไม่ทราบผู้โอน (ถ้ามี) => UnknownPayment,ForeignBankTransfer
        [Description("ID เงินโอนไม่ทราบผู้โอน (ถ้ามี)")]
        public Guid? UnknownPaymentID { get; set; }

        public Guid? ChangeWorkFlowID { get; set; }
        
        [Description("สถานะ เช็ครอนำฝาก ที่หน้านำฝาก 1=เช็ครอนำฝาก 0= => CashierCheque,PersonalCheque")]
        public bool? IsChequeConfirm { get; set; }

        [Description("จ่ายให้...")]
        public MasterCenterDropdownDTO PaymentReceiver { get; set; }

        public string CustomerName { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }

        public bool? IsDeposited { get; set; }

        public string DepositNo { get; set; }

        public DateTime? DepositDate { get; set; }

        public string DepositRemark { get; set; }

        public static PaymentMethodDTO CreateFromModel(PaymentMethod model)
        {
            if (model != null)
            {
                PaymentMethodDTO result = new PaymentMethodDTO()
                {
                    Id = model.ID,
                    FeeIncludingVat = model.FeeIncludingVat,
                    IsForeignCreditCard = model.IsForeignCreditCard,
                    BankBranchName = model.BankBranchName,
                    Number = model.Number,
                    Bank = BankDropdownDTO.CreateFromModel(model.DirectCreditDebitExportDetailID != null ? model.DirectCreditDebitExportDetail.DirectCreditDebitApprovalForm.BankAccount.Bank : model.Bank),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.DirectCreditDebitExportDetailID != null ? model.DirectCreditDebitExportDetail.DirectCreditDebitApprovalForm.BankAccount :  model.BankAccount),
                    BillPaymentDetailID = model.BillPaymentDetailID,
                    CancelRemark = model.CancelRemark,
                    ChequeDate = model.ChequeDate,
                    CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(model.CreditCardPaymentType),
                    CreditCardType = MasterCenterDropdownDTO.CreateFromModel(model.CreditCardType) ,
                    DirectCreditDebitExportDetailID = model.DirectCreditDebitExportDetailID,
                    EDCBank = BankDropdownDTO.CreateFromModel(model.EDCBank),
                    Fee = model.Fee,
                    FeeConfirmByUser = UserDTO.CreateFromModel(model.FeeConfirmByUser),
                    FeeConfirmDate = model.FeeConfirmDate,
                    FeePercent = model.FeePercent,
                    ForeignTransferType = MasterCenterDropdownDTO.CreateFromModel(model.ForeignTransferType),
                    IR = model.IR,
                    IsFeeConfirm = model.IsFeeConfirm,
                    IsNotifyFET = model.IsNotifyFET,
                    IsRequestFET = model.IsRequestFET,
                    IsWrongAccount = model.IsWrongAccount ?? false,
                    NotifyFETMemo = model.NotifyFETMemo,
                    PayAmount = model.PayAmount,
                    PaymentID = model.PaymentID,
                    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(model.PaymentMethodType),
                    PayToCompany = CompanyDropdownDTO.CreateFromModel(model.PayToCompany),
                    TransferorName = model.TransferorName,
                    UnknownPaymentID = model.UnknownPaymentID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    Vat = model.Vat,
                    ChangeWorkFlowID = model.ChangeUnitWorkFlowID,
                    CustomerName = model.BillPaymentDetail?.CustomerName,
                    Ref1 = model.BillPaymentDetail?.BankRef1,
                    Ref2 = model.BillPaymentDetail?.BankRef2
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static PaymentMethodDTO CreatePaymentOfflineFromModel(OfflinePaymentDetail model,MasterCenter CreditCardPaymentType, bool IsWrongAccount = false)
        {
            if (model != null)
            { PaymentMethodDTO result = new PaymentMethodDTO()
                {
                    Id = model.ID,  
                    Number = model.ReferentNO,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount), 
                    CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(CreditCardPaymentType),
                    CreditCardType = MasterCenterDropdownDTO.CreateFromModel(model.CreditCardType), 
                    EDCBank = BankDropdownDTO.CreateFromModel(model.Bank),    
                    PayAmount = model.PayAmount, 
                    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(model.PaymentMethod),
                    PayToCompany = CompanyDropdownDTO.CreateFromModel(model.Company),  
                    ChequeDate   = model.ReferentDate,
                    IsWrongAccount = IsWrongAccount,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db, PaymentFormType paymentFormType, bool? fromOffline = false)
        {
            ValidateException ex = new ValidateException();
            PaymentMethodDTO PaymentMethod = new PaymentMethodDTO();

            if (PaymentMethodType == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.PaymentMethodType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {

                #region CreditCard
                if (this.PaymentMethodType.Key == PaymentMethodKeys.CreditCard && fromOffline == true )
                {
                    if (this.Fee == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Fee)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }

                    if (string.IsNullOrEmpty(this.Number))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else
                    {
                        if (!this.Number.IsOnlyNumberWithMaxLength(16, 16))
                        {
                            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0035").FirstAsync();
                            string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (this.CreditCardPaymentType == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.CreditCardPaymentType)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }

                    if (this.CreditCardType == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.CreditCardType)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }

                    if (this.IsForeignCreditCard == false)
                    {
                        if (this.Bank == null)
                        {
                            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                    if (this.EDCBank == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.EDCBank)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }

                #endregion

                #region DebitCard
                if (this.PaymentMethodType.Key == PaymentMethodKeys.DebitCard)
                {
                    if (this.Fee == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Fee)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (string.IsNullOrEmpty(this.Number))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else
                    {
                        if (!this.Number.IsOnlyNumberWithMaxLength(16, 16))
                        {
                            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0035").FirstAsync();
                            string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                    if (this.Bank == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }

           
                    if (this.EDCBank == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.EDCBank)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                #endregion

                #region PaymentPersonalCheque
                if (this.PaymentMethodType.Key == PaymentMethodKeys.PersonalCheque)
                {
                    if (this.ChequeDate == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.ChequeDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (string.IsNullOrEmpty(this.Number))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    //else
                    //{
                    //    if (!this.Number.IsOnlyNumber())
                    //    {
                    //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                    //        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //        var msg = errMsg.Message.Replace("[field]", desc);
                    //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //    }
                    //}
                    if (this.PayToCompany == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.PayToCompany)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (this.Bank == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    //if (this.BankBranchName == null)
                    //{
                    //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    //    string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.BankBranchName)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //    var msg = errMsg.Message.Replace("[field]", desc);
                    //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //}
                }
                #endregion

                #region CashierCheque
                if (this.PaymentMethodType.Key == PaymentMethodKeys.CashierCheque)
                {
                    if (this.ChequeDate == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.ChequeDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (string.IsNullOrEmpty(this.Number))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    //else
                    //{
                    //    if (!this.Number.IsOnlyNumber())
                    //    {
                    //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                    //        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //        var msg = errMsg.Message.Replace("[field]", desc);
                    //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //    }
                    //}

                    if (paymentFormType != PaymentFormType.PreTransfer && this.PayToCompany == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.PayToCompany)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (this.Bank == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    //if (this.BankBranchName == null)
                    //{
                    //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    //    string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.BankBranchName)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //    var msg = errMsg.Message.Replace("[field]", desc);
                    //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //}
                }
                #endregion

                #region BankTransfer
                if (this.PaymentMethodType.Key == PaymentMethodKeys.BankTransfer)
                {
                    //if (this.BankAccount == null)
                    //{
                    //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    //    string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //    var msg = errMsg.Message.Replace("[field]", desc);
                    //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //}
                }
                #endregion

                #region QRCode
                if (this.PaymentMethodType.Key == PaymentMethodKeys.QRCode)
                {
                    //if (this.BankAccount == null)
                    //{
                    //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    //    string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //    var msg = errMsg.Message.Replace("[field]", desc);
                    //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //}
                }
                #endregion

                #region ForeignBankTransfer
                if (this.PaymentMethodType.Key == PaymentMethodKeys.ForeignBankTransfer)
                {
                    if (this.BankAccount == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (string.IsNullOrEmpty(this.TransferorName))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = PaymentMethod.GetType().GetProperty(nameof(PaymentMethod.TransferorName)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                #endregion

            }
            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
}
