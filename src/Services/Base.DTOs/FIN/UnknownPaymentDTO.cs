using Database.Models.MST;
using System;
using System.ComponentModel;
using System.Linq;
using Database.Models.ACC;
using Database.Models.FIN;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using System.Threading.Tasks;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.MasterKeys;
using Database.Models.DbQueries.FIN;
using Database.Models.PRJ;

namespace Base.DTOs.FIN
{
    public class UnknownPaymentDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ PI
        /// </summary>
        [Description("เลขที่ PI")]
        public string UnknownPaymentCode { get; set; }

        /// <summary>
        /// วันที่เงินเข้า
        /// </summary>
        [Description("วันที่เงินเข้า")]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        [Description("บริษัท")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// บัญชีธนาคาร filter ตามข้อมูลบริษัท
        /// </summary>
        [Description("บัญชีธนาคาร")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// เลขที่ Booking
        /// </summary>
        [Description("เลขที่ Booking")]
        public Guid? BookingID { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public SoldUnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// เงินตั้งพัก
        /// </summary>
        [Description("เงินตั้งพัก")]
        public decimal UnknownAmount { get; set; }

        /// <summary>
        /// เงินกลับรายการ
        /// </summary>
        [Description("เงินกลับรายการ")]
        public decimal ReverseAmount { get; set; }

        /// <summary>
        /// เงินคงเหลือ
        /// </summary>
        [Description("เงินคงเหลือ")]
        public decimal BalanceAmount { get; set; }

        /// <summary>
        /// Post PO
        /// </summary>
        [Description("Post PO")]
        public bool IsPostUN { get; set; }

        /// <summary>
        /// เงินตั้งพัก
        /// </summary>
        [Description("เลขที่ RV")]
        public string RVNumber { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [Description("สถานะ")]
        public MasterCenterDropdownDTO UnknownPaymentStatus { get; set; }

        /// <summary>
        /// ผู้บันทึก
        /// </summary>
        [Description("ผู้บันทึก")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// ผู้กลับรายการ
        /// </summary>
        [Description("ผู้กลับรายการ")]
        public string ReversedBy { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        /// <summary>
        /// CancelRemark
        /// </summary>
        [Description("หมายเหตุยกเลิก")]
        public string CancelRemark { get; set; }

        /// <summary>
        /// หมายเหตุรายการด้าน SAP
        /// </summary>
        [Description("หมายเหตุรายการด้าน SAP")]
        public string SAPRemark { get; set; }

        /// <summary>
        /// หมายเหตุรายการด้าน SAP
        /// </summary>
        [Description("ชนิดของช่องทางการชำระเงิน")]
        public MasterCenterDropdownDTO PaymentMethodType { get; set; }

        public List<UnknownPaymentReverseDTO> UnknownPaymentReverse { get; set; }

        [Description("เป็นบัตรต่างประเทศหรือไม่ => CreditCard")]
        public bool? IsForeignCreditCard { get; set; }
        [Description("ค่าธรรมเนียม => CreditCard,DebitCard,ForeignBankTransfer")]
        public decimal? Fee { get; set; }
        [Description("เปอร์เซ็นต์ธรรมเนียม => CreditCard,DebitCard")]
        public decimal? FeePercent { get; set; }
        [Description("Vat => CreditCard")]
        public decimal? Vat { get; set; }
        [Description("ค่าธรรมเนียม (หลัง Vat) => CreditCard,DebitCard")]
        public decimal? FeeIncludingVat { get; set; }
        [Description("เลขที่เช็ค,เลขที่บัตรเครดิต => CreditCard,DebitCard")]
        public string Number { get; set; }
        [Description("รูปแบบการจ่ายเงิน (รูดเต็ม หรือ ผ่อน) => CreditCard, DebitCard")]
        public MasterCenterDropdownDTO CreditCardPaymentType { get; set; }

        [Description("ประเภทบัตร (Visa, Master, JCB) => CreditCard, DebitCard")]
        public MasterCenterDropdownDTO CreditCardType { get; set; }

        [Description("ธนาคารของเครื่องรูดบัตร => CreditCard,DebitCard")]
        public BankDropdownDTO EDCBank { get; set; }
        [Description("ธนาคาร => CashierCheque,PersonalCheque, CreditCard, DebitCard")]
        public BankDropdownDTO Bank { get; set; }
        [Description("ผิดบัญชี หรือ ผิดบริษัท => เงินโอนผ่านธนาคาร, BillPayment, ForeignBankTransfer, QR, CashierCheque, PersonalCheque, CreditCard, DebitCard")]
        public bool? IsWrongAccount { get; set; }
        [Description("ประเภทการโอนเงินต่างประเทศ => ForeignBankTransfer")]
        public MasterCenterDropdownDTO ForeignTransferType { get; set; }
        [Description("IR => ForeignBankTransfer")]
        public string IR { get; set; }

        [Description("ชื่อผู้โอน => ForeignBankTransfer")]
        public string TransferorName { get; set; }
        [Description("ต้องขอ FET หรือไม่ => ForeignBankTransfer")]
        public bool? IsRequestFET { get; set; }

        [Description("แจ้งแก้ไข FET => ForeignBankTransfer")]
        public bool? IsNotifyFET { get; set; }

        [Description("ข้อความแจ้งเตือน => ForeignBankTransfer")]
        public string NotifyFETMemo { get; set; }

        [Description("BillPaymentDetailID")]
        public Guid? BillPaymentDetailID { get; set; }

        [Description("วันที่บันทึก")]
        public DateTime? CreateDate { get; set; }

        [Description("ชื่อลูกค้า")]
        public string CustomerName { get; set; }

        [Description("Ref1")]
        public string Ref1 { get; set; }

        [Description("Ref2")]
        public string Ref2 { get; set; }        
        
        [Description("สถานะห้อง")]
        public string AgreementStatus { get; set; }
        [Description("สถานะห้อง Key")]
        public string AgreementStatusKey { get; set; }
         
        [Description("เลขที่ Post CA")]
        public string PostCANo { get; set; }

        [Description("รายการ AutoCancel")]
        public bool? IsAutoCC { get; set; }

        public static async Task<UnknownPaymentDTO> CreateFromQueryResultAsync(UnknownPaymentQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {

                var sumReverseAmount = await db.PaymentMethods.Where(o => o.UnknownPaymentID == model.UnknownPayment.ID).Select(o => o.PayAmount).SumAsync();
                UnknownPaymentDTO result = new UnknownPaymentDTO()
                {
                    Id = model.UnknownPayment.ID,
                    UnknownPaymentCode = model.UnknownPayment.UnknownPaymentCode,
                    ReceiveDate = model.UnknownPayment.ReceiveDate,
                    Company = CompanyDropdownDTO.CreateFromModel(model.UnknownPayment?.Company),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.UnknownPayment?.BankAccount),
                    BookingID = model.UnknownPayment.BookingID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.UnknownPayment?.Project ??model.UnknownPayment?.Booking?.Project ),
                    Unit = SoldUnitDropdownDTO.CreateFromBooking(model.UnknownPayment?.Booking),
                    UnknownAmount = model.UnknownPayment.Amount,
                    ReverseAmount = sumReverseAmount,
                    BalanceAmount = model.UnknownPayment.Amount - sumReverseAmount,
                    IsPostUN = (model.UnknownPayment.PostGLDocumentNo ?? "") == "" ? false : true,
                    //RVNumber = null, // #kim
                    UnknownPaymentStatus = MasterCenterDropdownDTO.CreateFromModel(model.UnknownPayment.UnknownPaymentStatus),
                    CreatedBy = model.UnknownPayment.CreatedBy?.DisplayName,
                    ReversedBy = model?.PaymentMethod.CreatedBy?.DisplayName,
                    Remark = model.UnknownPayment.Remark,
                    CancelRemark = model.UnknownPayment.CancelRemark,
                    SAPRemark = model.UnknownPayment.SAPRemark,
                    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(model.UnknownPayment?.PaymentMethodType),
                    Bank = BankDropdownDTO.CreateFromModel(model.UnknownPayment?.Bank),
                    CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(model.UnknownPayment?.CreditCardPaymentType),
                    CreditCardType = MasterCenterDropdownDTO.CreateFromModel(model.UnknownPayment?.CreditCardType),
                    EDCBank = BankDropdownDTO.CreateFromModel(model.UnknownPayment?.EDCBank),
                    ForeignTransferType = MasterCenterDropdownDTO.CreateFromModel(model.UnknownPayment?.ForeignTransferType),
                    NotifyFETMemo = model.UnknownPayment.NotifyFETMemo,
                    IsNotifyFET = model.UnknownPayment.IsNotifyFET,
                    IsRequestFET = model.UnknownPayment.IsRequestFET,
                    TransferorName = model.UnknownPayment.TransferorName,
                    IR = model.UnknownPayment.IR,
                    IsWrongAccount = model.UnknownPayment.IsWrongAccount, 
                    Number = model.UnknownPayment.Number,
                    FeeIncludingVat = model.UnknownPayment.FeeIncludingVat,
                    Vat = model.UnknownPayment.Vat,
                    FeePercent = model.UnknownPayment.FeePercent,
                    Fee = model.UnknownPayment.Fee,
                    IsForeignCreditCard = model.UnknownPayment.IsForeignCreditCard,
                    BillPaymentDetailID = model.UnknownPayment.BillPaymentDetailID
                };
                result.Updated = (model.PaymentMethod?.Updated != null) ? model.PaymentMethod.Updated : model.UnknownPayment.Updated;
                if (model.UnknownPayment.BillPaymentDetail != null)
                {
                    result.CustomerName = model.UnknownPayment.BillPaymentDetail.CustomerName;
                    result.Ref1 = model.UnknownPayment.BillPaymentDetail.BankRef1;
                    result.Ref2 = model.UnknownPayment.BillPaymentDetail.BankRef2;  
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task<UnknownPaymentDTO> CreateFromDBQueryAsync(dbqSPUnknownPaymentList model, DatabaseContext db)
        {
            if (model != null)
            {

                var BankAccount = await db.BankAccounts.Include(o => o.Bank).Include(o => o.BankAccountType).Where(o => o.ID == model.BankAccountID).FirstOrDefaultAsync();

                UnknownPaymentDTO result = new UnknownPaymentDTO()
                {
                    Id = model.UnknownPaymentID,
                    UnknownPaymentCode = model.UnknownPaymentCode,
                    ReceiveDate = model.ReceiveDate,
                    Company = new CompanyDropdownDTO
                    {
                        Id = model.CompanyID ?? Guid.Empty,
                        Code = model.CompanyCode,
                        NameTH = model.CompanyName,
                        SAPCompanyID = model.SAPCompanyID
                    },

                    BankAccount = BankAccountDropdownDTO.CreateFromModel(BankAccount),
                    BookingID = model.BookingID,
                    Project = new ProjectDropdownDTO { Id = model.ProjectID, ProjectNo = model.ProjectNo, ProjectNameTH = model.ProjectName },
                    Unit = new SoldUnitDropdownDTO { Id = model.UnitID ?? Guid.Empty, UnitNo = model.UnitNo, BookingID = model.BookingID },
                    UnknownAmount = model.UnknownAmount ?? 0,
                    ReverseAmount = model.ReverseAmount ?? 0,
                    BalanceAmount = model.BalanceAmount ?? 0,
                    IsPostUN = model.IsPostUN ?? false,
                    RVNumber = model.RVDocumentNo,
                    UnknownPaymentStatus = new MasterCenterDropdownDTO { Id = model.UnknownPaymentStatusID ?? Guid.Empty, Name = model.UnknownPaymentStatusDetail },
                    CreatedBy = model.CreateName,
                    ReversedBy = model.ReverseName,
                    Remark = model.Remark,
                    CancelRemark = model.CancelRemark,
                    SAPRemark = model.SAPRemark,
                    Updated = model.UpdateDate,
                    PaymentMethodType = new MasterCenterDropdownDTO { Id = model.PaymentMethodTypeID ?? Guid.Empty, Name = model.PaymentMethodType },
                    Number = model.Number,
                    CreditCardType = new MasterCenterDropdownDTO { Id = model.CreditCardTypeID ?? Guid.Empty, Name = model.CreditCardType },
                    Bank = new BankDropdownDTO { Id = model.BankID ?? Guid.Empty, NameTH = model.Bank },
                    IsWrongAccount = model.IsWrongAccount ?? false,
                    BillPaymentDetailID = model.BillPaymentDetailID,
                    CreateDate = model.CreateDate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task<UnknownPaymentDTO> CreateFromQuerySPAsync(UnknownPaymentQuerySP model, DatabaseContext db)
        {
            if (model != null)
            {

                var BankAccount = await db.BankAccounts.Include(o => o.Bank).Include(o => o.BankAccountType).Where(o => o.ID == model.main.BankAccountID).FirstOrDefaultAsync();

                UnknownPaymentDTO result = new UnknownPaymentDTO()
                {
                    Id = model.main.UnknownPaymentID,
                    UnknownPaymentCode = model.main.UnknownPaymentCode,
                    ReceiveDate = model.main.ReceiveDate,  
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(BankAccount),
                    BookingID = (model.main.UnBookingID??false ) ? model.main.BookingID : null,
                    UnknownAmount = model.main.UnknownAmount ?? 0,
                    ReverseAmount = model.detail.Sum(x=>x.ReverseAmount) ?? 0,
                    BalanceAmount = model.main.BalanceAmount ?? 0,
                    IsPostUN = model.main.IsPostUN ?? false, 
                    UnknownPaymentStatus = new MasterCenterDropdownDTO { Id = model.main.UnknownPaymentStatusID ?? Guid.Empty, Name = model.main.UnknownPaymentStatusDetail,Key = model.main.UnknownPaymentStatusKey },
                    CreatedBy = model.main.CreateName, 
                    Remark = model.main.Remark,
                    CancelRemark = model.main.CancelRemark,
                    SAPRemark = model.main.SAPRemark,
                    Updated = model.main.UpdateDate,
                    PaymentMethodType = new MasterCenterDropdownDTO { Id = model.main.PaymentMethodTypeID ?? Guid.Empty, Name = model.main.PaymentMethodType },
                    Number = model.main.Number,
                    CreditCardType = new MasterCenterDropdownDTO { Id = model.main.CreditCardTypeID ?? Guid.Empty, Name = model.main.CreditCardType },
                    Bank = new BankDropdownDTO { Id = model.main.BankID ?? Guid.Empty, NameTH = model.main.Bank },
                    IsWrongAccount = model.main.IsWrongAccount ?? false,
                    PostCANo = model.main.PostCANo,
                    Company = new CompanyDropdownDTO
                    {
                        Id = model.main.CompanyID ?? Guid.Empty,
                        Code = model.main.CompanyCode,
                        NameTH = model.main.CompanyName,
                        SAPCompanyID = model.main.SAPCompanyID
                    },
                    BillPaymentDetailID = model.main.BillPaymentDetailID,
                    CreateDate = model.main.CreateDate,
                    IsRequestFET = model.main.IsRequestFET,
                    AgreementStatus = model.main.AgreementStatus,
                    AgreementStatusKey = model.main.AgreementStatusKey,
                    Project = (model.main.UnBookingID ?? false) ? new ProjectDropdownDTO { Id = model.main.ProjectID, ProjectNo = model.main.ProjectNo, ProjectNameTH = model.main.ProjectNo + "-" + model.main.ProjectName } : null,
                    Unit = (model.main.UnBookingID ?? false) ? new SoldUnitDropdownDTO { Id = model.main.UnitID ?? Guid.Empty, UnitNo = model.main.UnitNo } : null,
                }; 
                result.UnknownPaymentReverse = model.detail.Select(async o => await UnknownPaymentReverseDTO.CreateFromQuerySPAsync(o)).Select(o => o.Result).ToList();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<UnknownPayment> ToModelAsync(DatabaseContext db)
        {
            UnknownPayment model = new UnknownPayment();
            model.Amount = UnknownAmount;
            model.BookingID = BookingID;
            model.ReceiveDate = ReceiveDate ?? DateTime.Now;
            model.IsDeleted = false;
            model.CancelRemark = null;

            model.UnknownPaymentStatusID = model.UnknownPaymentStatusID;

            model.SAPRemark = null;
            model.Remark = Remark;
            model.PaymentMethodTypeMasterCenterID = PaymentMethodType.Id;
            model.CompanyID = this.Company.Id;
            model.BillPaymentDetailID = this.BillPaymentDetailID;
            model.BankAccountID = BankAccount?.Id;
            model.IsRequestFET = this.IsRequestFET;
            model.IsAutoCC = this.IsAutoCC.HasValue ? this.IsAutoCC.Value : false;
            model.AutoCCDate = DateTime.Now;
            //model.AutoCCUserID = this.AutoCCUserID;
            model.IsRefund = null; //requirement by PM

            #region CreditCard
            if (PaymentMethodType.Key == PaymentMethodKeys.CreditCard)
            {
                var paymentMethodCreditCardMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                            && o.Key == PaymentMethodKeys.CreditCard)
                                                                                           .Select(o => o.ID)
                                                                                           .FirstOrDefaultAsync();
                model.PaymentMethodTypeMasterCenterID = paymentMethodCreditCardMasterCenterID;
                model.IsForeignCreditCard = IsForeignCreditCard;
                model.Fee = Fee ?? 0;
                model.FeePercent = FeePercent ?? 0;
                model.Vat = Vat.Value;
                model.FeeIncludingVat = (UnknownAmount - (Fee ?? 0) - (Vat ?? 0));
                model.Number = Number;
                model.CreditCardPaymentTypeMasterCenterID = CreditCardPaymentType?.Id;
                if(CreditCardType?.Id != null && CreditCardType?.Id != Guid.Empty)
                model.CreditCardTypeMasterCenterID = CreditCardType?.Id;
                model.BankID = Bank?.Id;
                model.EDCBankID = EDCBank?.Id;
                model.IsWrongAccount = IsWrongAccount ?? false;
                model.ProjectID = Project.Id;

            }
            #endregion CreditCard

            #region DebitCard
            if (PaymentMethodType.Key == PaymentMethodKeys.DebitCard)
            {
                var paymentMethodDebitCardMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                           && o.Key == PaymentMethodKeys.DebitCard)
                                                                                           .Select(o => o.ID)
                                                                                           .FirstOrDefaultAsync();
                model.PaymentMethodTypeMasterCenterID = paymentMethodDebitCardMasterCenterID;
                model.Fee = Fee.Value;
                model.FeePercent = FeePercent.Value;
                model.Vat = Vat.Value;
                model.FeeIncludingVat = (UnknownAmount - (Fee ?? 0) - (Vat ?? 0));
                model.Number = Number;
                model.BankID = Bank?.Id;
                model.EDCBankID = EDCBank?.Id;
                model.IsWrongAccount = IsWrongAccount ?? false;
                model.ProjectID = Project.Id;
            }
            #endregion Debit

            #region PaymentBankTransfer
            if (PaymentMethodType.Key == PaymentMethodKeys.BankTransfer)
            {
                var paymentMethodBankTransferMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                           && o.Key == PaymentMethodKeys.BankTransfer)
                                                                                           .Select(o => o.ID)
                                                                                           .FirstAsync();
                model.PaymentMethodTypeMasterCenterID = paymentMethodBankTransferMasterCenterID;
               
                model.IsWrongAccount = IsWrongAccount ?? false;
            }
            #endregion

            #region PaymentForeignBankTransfer
            if (PaymentMethodType.Key == PaymentMethodKeys.ForeignBankTransfer)
            {
                var paymentMethodForeignBankTransferMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                           && o.Key == PaymentMethodKeys.ForeignBankTransfer)
                                                                                           .Select(o => o.ID)
                                                                                           .FirstAsync();
                model.PaymentMethodTypeMasterCenterID = paymentMethodForeignBankTransferMasterCenterID;
                model.Fee = Fee ?? 0; 
                model.IsWrongAccount = IsWrongAccount ?? false;
                model.BankID = Bank?.Id;
                model.ForeignTransferTypeMasterCenterID = ForeignTransferType?.Id;
                model.FeeIncludingVat = (UnknownAmount - (Fee ?? 0));
                model.IR = IR;
                model.TransferorName = TransferorName;
                model.IsRequestFET = IsRequestFET;
                model.IsNotifyFET = IsNotifyFET;
                model.NotifyFETMemo = NotifyFETMemo;
                if (model.Fee > 0)
                {
                    model.ProjectID = Project?.Id;
                }
            }
            #endregion PaymentForeignBankTransfer
            #region BillPayment
            if (PaymentMethodType.Key == PaymentMethodKeys.BillPayment)
            {
                var paymentMethodBillPaymentMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                           && o.Key == PaymentMethodKeys.BillPayment)
                                                                                           .Select(o => o.ID)
                                                                                           .FirstAsync();
                model.PaymentMethodTypeMasterCenterID = paymentMethodBillPaymentMasterCenterID;
                model.IsWrongAccount = IsWrongAccount ?? false;
                model.BankID = Bank?.Id;
                model.BillPaymentDetailID = model.BillPaymentDetailID;
            }
            #endregion UnknowPayment

            return model;
        }

        public async Task ValidateOnUpdateAsync(DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();

            var Company = DB.Companies.Where(o => o.ID == this.Company.Id).FirstOrDefault();
            //await DB.CheckCalendarThrowErrorAsync(Company.ID, ReceiveDate ?? DateTime.Now);

            if (Company == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(Company)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ReceiveDate == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                string desc = GetType().GetProperty(nameof(ReceiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (UnknownAmount == 0)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                string desc = GetType().GetProperty(nameof(UnknownAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            #region CreditCard
            if (this.PaymentMethodType.Key == PaymentMethodKeys.CreditCard)
            {
                if (this.Fee == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(Fee)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (string.IsNullOrEmpty(this.Number))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.Number.IsOnlyNumberWithMaxLength(16, 16))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0035").FirstAsync();
                        string desc = GetType().GetProperty(nameof(Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }

                if (this.CreditCardPaymentType == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(CreditCardPaymentType)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                //if (this.CreditCardType == null)
                //{
                //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                //    string desc = GetType().GetProperty(nameof(CreditCardType)).GetCustomAttribute<DescriptionAttribute>().Description;
                //    var msg = errMsg.Message.Replace("[field]", desc);
                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                //}

                if (this.IsForeignCreditCard == false)
                {
                    if (this.Bank == null)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = GetType().GetProperty(nameof(Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                if (this.EDCBank == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(EDCBank)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.Project == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(Project)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(Fee)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (string.IsNullOrEmpty(this.Number))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.Number.IsOnlyNumberWithMaxLength(16, 16))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0035").FirstAsync();
                        string desc = GetType().GetProperty(nameof(Number)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                if (this.Bank == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }


                if (this.EDCBank == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(EDCBank)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.Project == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            #endregion

            #region BankTransfer
            if (this.PaymentMethodType.Key == PaymentMethodKeys.BankTransfer)
            {
                if (this.BankAccount == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            #endregion

            #region ForeignBankTransfer
            if (this.PaymentMethodType.Key == PaymentMethodKeys.ForeignBankTransfer)
            {
                if (this.BankAccount == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (string.IsNullOrEmpty(this.TransferorName))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = GetType().GetProperty(nameof(TransferorName)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            #endregion


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task ValidateBeforeUpdateAsync(int UpdateType, DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            if (UpdateType == 0)
                return;

            var headerModel = await db.UnknownPayments.Include(o => o.UnknownPaymentStatus).Where(e => e.ID == Id).FirstOrDefaultAsync() ?? new UnknownPayment();
            var detailModel = await db.PaymentMethods.Include(o => o.Payment).Where(e => e.UnknownPaymentID == Id && e.Payment.IsCancel == false).ToListAsync();

            var postUNDocNo = await db.PostGLHeaders.Where(e => e.ReferentID == headerModel.ID).Select(o => o.DocumentNo).FirstOrDefaultAsync();

            bool IsPostUN = !string.IsNullOrEmpty(postUNDocNo); // True = โพสแล้ว , False = ยังไม่โพส
            bool IsSentToSAP = headerModel.UnknownPaymentStatus.Key == UnknownPaymentStatusKeys.SAP ? true : false;
            bool IsReversed = detailModel.Any() ? true : false;

            bool canEdit = false;
            bool canReverse = false;

            bool canTransferToSAP = false;
            bool canDelete = false;

            //if (!IsPostUN && !IsSentToSAP && !IsReversed)
            if (!IsSentToSAP && !IsReversed)
                canEdit = true;

            if (IsPostUN && !IsSentToSAP)
            //if (!IsSentToSAP)
                canReverse = true;

            //if (IsPostUN && !IsSentToSAP && !IsReversed)
            if (!IsSentToSAP && !IsReversed && IsPostUN)
                canTransferToSAP = true;

            //if (!IsPostUN && !IsReversed && !IsSentToSAP)
            if (!IsReversed && !IsSentToSAP)
                canDelete = true;

            var msg = new ErrorMessage();

            if (UpdateType == 1 && !canEdit)
            {
                if (!IsPostUN)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0095").FirstOrDefaultAsync() ?? new ErrorMessage();

                if (IsReversed)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0096").FirstOrDefaultAsync() ?? new ErrorMessage();

                if (IsSentToSAP)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0097").FirstOrDefaultAsync() ?? new ErrorMessage();

                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0092").FirstOrDefaultAsync() ?? new ErrorMessage();
                ex.AddError(errMsg.Key, errMsg.Message.Replace("[msg]", msg.Message ?? ""), (int)errMsg.Type);
            }

            if (UpdateType == 2 && !canReverse)
            {
                if (!IsPostUN)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0098").FirstOrDefaultAsync() ?? new ErrorMessage();

                if (IsSentToSAP)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0097").FirstOrDefaultAsync() ?? new ErrorMessage();

                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0093").FirstOrDefaultAsync() ?? new ErrorMessage();
                ex.AddError(errMsg.Key, errMsg.Message.Replace("[msg]", msg.Message ?? ""), (int)errMsg.Type);
            }

            if (UpdateType == 3 && !canTransferToSAP)
            {
                if (!IsPostUN)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0098").FirstOrDefaultAsync() ?? new ErrorMessage();

                if (IsReversed)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0096").FirstOrDefaultAsync() ?? new ErrorMessage();

                if (IsSentToSAP)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0097").FirstOrDefaultAsync() ?? new ErrorMessage();

                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0092").FirstOrDefaultAsync() ?? new ErrorMessage();
                ex.AddError(errMsg.Key, errMsg.Message.Replace("[msg]", msg.Message ?? ""), (int)errMsg.Type);
            }

            if (UpdateType == 4 && !canDelete)
            {
                if (!IsPostUN)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0095").FirstOrDefaultAsync() ?? new ErrorMessage();

                if (IsReversed)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0096").FirstOrDefaultAsync() ?? new ErrorMessage();

                if (IsSentToSAP)
                    msg = await db.ErrorMessages.Where(o => o.Key == "ERR0097").FirstOrDefaultAsync() ?? new ErrorMessage();

                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0094").FirstOrDefaultAsync() ?? new ErrorMessage();
                ex.AddError(errMsg.Key, errMsg.Message.Replace("[msg]", msg.Message ?? ""), (int)errMsg.Type);
            }

            if (ex.HasError)
                throw ex;
        }
    }

    public class UnknownPaymentQueryResult
    {
        public UnknownPayment UnknownPayment { get; set; }

        public PaymentMethod PaymentMethod { get; set; } 
         
    }

    public class UnknownPaymentQuerySP{
        public dbqSPUnknownPaymentList main { get; set; }
        public List<dbqSPUnknownPaymentList> detail { get; set; }
    }
}
