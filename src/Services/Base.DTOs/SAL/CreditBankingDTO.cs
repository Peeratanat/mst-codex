using Database.Models.SAL;
using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.MST;
using Database.Models;
using ErrorHandling;
using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Database.Models.MasterKeys;
using Database.Models.PRM;

namespace Base.DTOs.SAL
{
    public class CreditBankingDTO : BaseDTO
    {
        [Description("ใบจอง")]
        public BookingDropdownDTO Booking { get; set; }

        [Description("หมายเหตุ")]
        public string Remark { get; set; }


        [Description("สถาบันการเงิน")]
        public MasterCenterDropdownDTO FinancialInstitution { get; set; }
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }
        [Description("สาขา")]
        public BankBranchDropdownDTO BankBranch { get; set; }
        [Description("จังหวัด")]
        public ProvinceListDTO Province { get; set; }

        [Description("ธนาคารอื่นๆ")]
        public string OtherBank { get; set; }

        [Description("สาขาอื่นๆ")]
        public string OtherBankBranch { get; set; }


        [Description("วันที่ขอสินเชื่อ")]
        public DateTime? LoanSubmitDate { get; set; }


        [Description("ยอดขอกู้")]
        public decimal LoanAmount { get; set; }
        [Description("ยอดอนุมัติ AP")]
        public decimal ApprovedLoanAPAmount { get; set; }
        [Description("เบี้ยประกัน")]
        public decimal InsuranceAmount { get; set; }
        [Description("เบี้ยประกันอัคคีภัย")]
        public decimal InsuranceOnFireAmount { get; set; }
        [Description("เงินหักล่วงหน้างวดแรก")]
        public decimal FirstDeductAmount { get; set; }
        [Description("เงินคืนลูกค้า")]
        public decimal ReturnCustomerAmount { get; set; }
        [Description("ยอดอนุมัติรวม (ธนาคาร)")]
        public decimal ApprovedAmount { get; set; }


        [Description("สถานะสินเชื่อ")]
        public MasterCenterDropdownDTO LoanStatus { get; set; }
        [Description("วันที่ทราบผล")]
        public DateTime? ResultDate { get; set; }


        [Description("สถานะการเลือกใช้ธนาคาร")]
        public bool? IsUseBank { get; set; }

        [Description("เหตุผลธนาคาร")]
        public Guid? BankReasonMasterCenterID { get; set; }
        public MasterCenterDropdownDTO BankReason { get; set; }
        [Description("เหตุผลการเลือกใช้ธนาคาร")]
        public MasterCenterDropdownDTO UseBankReason { get; set; }
        [Description("เหตุผลการเลือกใช้ธนาคารอื่นๆ")]
        public string UseBankOtherReason { get; set; }
        [Description("เหตุผลการเลือกไม่ใช้ธนาคาร")]
        public MasterCenterDropdownDTO NotUseBankReason { get; set; }
        [Description("เหตุผลการเลือกไม่ใช้ธนาคารอื่นๆ")]
        public string NotUseBankOtherReason { get; set; }
        [Description("เหตุผลการปฏิเสธสถานะสินเชื่อ")]
        public MasterCenterDropdownDTO BankRejectReason { get; set; }
        [Description("เหตุผลการรอผลสถานะสินเชื่อ")]
        public MasterCenterDropdownDTO BankWaitingReason { get; set; }
        /// <summary>
        /// พร้อมโอนแล้ว
        /// </summary>
        public bool IsTransferConfirmed { get; set; }

        /// <summary>
        /// หมายเหตุอื่นๆที่รอผลธนาคาร
        /// </summary>
        public string BankWaitingOtherReason { get; set; }

        //public DateTime? Updated { get; set; }
        public string UpdatedNo { get; set; }
        //public string UpdatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedNo { get; set; }
        public string CreatedBy { get; set; }

        /// <summary>
        /// ตั้งโอนแล้ว
        /// </summary>
        public bool IsTransferCreated { get; set; }

        /// <summary>
        /// สถานะโอน
        /// </summary>
        public MasterCenterDropdownDTO TransferStatus { get; set; }


        public DateTime? DocumentCompletetDate { get; set; }


        public static CreditBankingDTO CreateFromModel(CreditBanking model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new CreditBankingDTO();

                result.Id = model.ID;
                result.Booking = SAL.BookingDropdownDTO.CreateFromModel(model.Booking);
                result.Remark = model.Remark;
                result.FinancialInstitution = MST.MasterCenterDropdownDTO.CreateFromModel(model.FinancialInstitution);
                result.Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank);
                result.BankBranch = MST.BankBranchDropdownDTO.CreateFromModel(model.BankBranch);
                result.OtherBank = model.OtherBank;
                result.LoanSubmitDate = model.LoanSubmitDate;
                result.LoanAmount = model.LoanAmount;
                result.ApprovedLoanAPAmount = model.ApprovedLoanAPAmount;
                result.InsuranceAmount = model.InsuranceAmount;
                result.InsuranceOnFireAmount = model.InsuranceOnFireAmount;
                result.FirstDeductAmount = model.FirstDeductAmount;
                result.ReturnCustomerAmount = model.ReturnCustomerAmount;
                result.ApprovedAmount = model.ApprovedAmount;
                result.LoanStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LoanStatus);
                result.ResultDate = model.ResultDate;
                result.IsUseBank = model.IsUseBank;
                result.BankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankReason);
                result.UseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.UseBankReason);
                result.UseBankOtherReason = model.UseBankOtherReason;
                result.NotUseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.NotUseBankReason);
                result.NotUseBankOtherReason = model.NotUseBankOtherReason;
                result.BankRejectReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankRejectReason);
                result.BankWaitingReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankWaitingReason);
                result.Province = ProvinceListDTO.CreateFromModel(model.Province);
                result.OtherBankBranch = model.OtherBankBranch;

                //result.UpdatedBy = model.Booking?.UpdatedBy != null ? model.Booking?.UpdatedBy?.DisplayName : model.Booking?.CreatedBy?.DisplayName;

                result.Created = model.Created;
                result.CreatedBy = model.CreatedBy?.DisplayName;
                result.CreatedNo = model.CreatedBy?.EmployeeNo;
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;
                result.UpdatedNo = model.UpdatedBy?.EmployeeNo;

                result.IsTransferCreated = false;
                result.IsTransferConfirmed = false;

                var agreement = DB.Agreements
                    .Where(o =>
                            o.BookingID == model.BookingID
                            && o.IsCancel == false
                            && o.IsDeleted == false
                        )
                    .FirstOrDefault();
                if (agreement != null)
                {
                    var transfer = DB.Transfers
                        .Include(o => o.TransferStatus)
                        .Where(o =>
                                o.AgreementID == agreement.ID
                                && o.IsDeleted == false
                            )
                        .FirstOrDefault();
                    if (transfer != null)
                    {
                        result.IsTransferCreated = true;

                        if (transfer.IsReadyToTransfer == true)
                        {
                            result.IsTransferConfirmed = true;
                        }

                        result.TransferStatus = MST.MasterCenterDropdownDTO.CreateFromModel(transfer.TransferStatus);
                    }

                }

                result.BankWaitingOtherReason = model.BankWaitingOtherReason;


                result.DocumentCompletetDate = model.DocumentCompletetDate;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task<CreditBankingDTO> CreateFromModelAsync(models.SAL.CreditBanking model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                CreditBankingDTO result = new CreditBankingDTO()
                {
                    Id = model.ID,
                    Booking = SAL.BookingDropdownDTO.CreateFromModel(model.Booking),
                    Remark = model.Remark,
                    FinancialInstitution = MST.MasterCenterDropdownDTO.CreateFromModel(model.FinancialInstitution),
                    Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
                    BankBranch = MST.BankBranchDropdownDTO.CreateFromModel(model.BankBranch),
                    OtherBank = model.OtherBank,
                    LoanSubmitDate = model.LoanSubmitDate,
                    LoanAmount = model.LoanAmount,
                    ApprovedLoanAPAmount = model.ApprovedLoanAPAmount,
                    InsuranceAmount = model.InsuranceAmount,
                    InsuranceOnFireAmount = model.InsuranceOnFireAmount,
                    FirstDeductAmount = model.FirstDeductAmount,
                    ReturnCustomerAmount = model.ReturnCustomerAmount,
                    ApprovedAmount = model.ApprovedAmount,
                    LoanStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LoanStatus),
                    ResultDate = model.ResultDate,
                    IsUseBank = model.IsUseBank,
                    BankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankReason),
                    UseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.UseBankReason),
                    UseBankOtherReason = model.UseBankOtherReason,
                    NotUseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.NotUseBankReason),
                    NotUseBankOtherReason = model.NotUseBankOtherReason,
                    BankRejectReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankRejectReason),
                    BankWaitingReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankWaitingReason),
                    //Province = ProvinceListDTO.CreateFromModel(model.BankBranch?.Province),
                    Province = ProvinceListDTO.CreateFromModel(model?.Province),
                    OtherBankBranch = model.OtherBankBranch,
                    BankWaitingOtherReason = model.BankWaitingOtherReason

                };

                return result;
            }
            else
            {
                return new CreditBankingDTO();
            }
        }
        //public static CreditBankingDTO CreateFromQueryResult(CreditBankingQueryResult model)
        //{
        //    if (model != null)
        //    {
        //        var result = new CreditBankingDTO()
        //        {
        //            Id = model.CreditBanking?.ID,
        //            Booking = SAL.BookingDropdownDTO.CreateFromModel(model.Booking),
        //            Remark = model.CreditBanking.Remark,
        //            FinancialInstitution = MST.MasterCenterDropdownDTO.CreateFromModel(model.FinancialInstitution),
        //            Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
        //            BankBranch = MST.BankBranchDropdownDTO.CreateFromModel(model.BankBranch),
        //            OtherBank = model.CreditBanking.OtherBank,
        //            LoanSubmitDate = model.CreditBanking.LoanSubmitDate,
        //            LoanAmount = model.CreditBanking.LoanAmount,
        //            ApprovedLoanAPAmount = model.CreditBanking.ApprovedLoanAPAmount,
        //            InsuranceAmount = model.CreditBanking.InsuranceAmount,
        //            InsuranceOnFireAmount = model.CreditBanking.InsuranceOnFireAmount,
        //            FirstDeductAmount = model.CreditBanking.FirstDeductAmount,
        //            ReturnCustomerAmount = model.CreditBanking.ReturnCustomerAmount,
        //            ApprovedAmount = model.CreditBanking.ApprovedAmount,
        //            LoanStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LoanStatus),
        //            ResultDate = model.CreditBanking.ResultDate,
        //            IsUseBank = model.CreditBanking.IsUseBank,
        //            BankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankReason),
        //            UseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.UseBankReason),
        //            UseBankOtherReason = model.CreditBanking.UseBankOtherReason,
        //            NotUseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.NotUseBankReason),
        //            NotUseBankOtherReason = model.CreditBanking.NotUseBankOtherReason,
        //            BankRejectReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankRejectReason),
        //            BankWaitingReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankWaitingReason)
        //        };

        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            this.Id = this.Id ?? new Guid();

            if (this.Bank == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CreditBankingDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            //var CheckBank = await db.CreditBankings.Where(e => e.BookingID == this.Booking.Id && e.ID != this.Id && e.Bank.ID == this.Bank.Id && e.Bank.Alias != "OTH").ToListAsync() ?? new List<CreditBanking>();
            //if (CheckBank.Any())
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(CreditBankingDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            //var CheckBankOther = await db.CreditBankings.Where(e => e.BookingID == this.Booking.Id && e.ID != this.Id && e.Bank.ID == this.Bank.Id && e.Bank.Alias == "OTH" && e.OtherBank == this.OtherBank).ToListAsync() ?? new List<CreditBanking>();
            //if (CheckBankOther.Any())
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(CreditBankingDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            var CheckBank = await db.CreditBankings
                .Include(o => o.Bank)
                .Include(o => o.Province)
                .Include(o => o.BankBranch)
                .Where(e =>
                    e.BookingID == this.Booking.Id
                    && e.ID != this.Id
                    && e.Bank.Alias != "OTH"
                    && e.Bank.ID == this.Bank.Id
                //&& e.Province.ID == this.Province.Id
                //&& e.BankBranch.ID == (this.BankBranch == null ? Guid.Empty : this.BankBranch.Id)
                ).ToListAsync() ?? new List<CreditBanking>();

            if (CheckBank.Any())
            {
                //var msg = "กรุณาระบุชื่อธนาคาร จังหวัด สาขา ไม่ซ้ำกัน";
                var msg = "กรุณาระบุชื่อธนาคารไม่ซ้ำกัน";
                ex.AddError("ERRX001", msg, 0);
            }

            var CheckBankOther = await db.CreditBankings
                .Include(o => o.Bank)
                .Include(o => o.Province)
                .Include(o => o.BankBranch)
                .Where(e =>
                    e.BookingID == this.Booking.Id
                    && e.ID != this.Id
                    && e.Bank.Alias == "OTH"
                    && e.Bank.ID == this.Bank.Id
                    && e.OtherBank == this.OtherBank
                //&& e.Province.ID == this.Province.Id
                //&& e.OtherBankBranch == this.OtherBankBranch
                ).ToListAsync() ?? new List<CreditBanking>();

            if (CheckBankOther.Any())
            {
                //var msg = "กรุณาระบุชื่อธนาคาร จังหวัด สาขา ไม่ซ้ำกัน";
                var msg = "กรุณาระบุชื่อธนาคารไม่ซ้ำกัน";
                ex.AddError("ERRX001", msg, 0);
            }

            var CheckUseBank = await db.CreditBankings.Where(e => e.BookingID == this.Booking.Id && e.ID != this.Id && e.LoanStatus.Key == "1" && e.IsUseBank == true && this.IsUseBank == true).ToListAsync() ?? new List<CreditBanking>();
            if (CheckUseBank.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0126").FirstAsync();
                var msg = errMsg.Message;
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            #region New Validate

            var bookingID = this.Booking.Id ?? Guid.Empty;

            var unitPrice_TP = await db.UnitPrices
                       .Include(o => o.Booking)
                       .Include(o => o.UnitPriceStage)
                       .Where(o => o.BookingID == bookingID
                               && o.UnitPriceStage.Key == UnitPriceStageKeys.TransferPromotion
                            )
                       .OrderByDescending(o => o.Created)
                       .FirstOrDefaultAsync();

            if (unitPrice_TP != null)
            {

                var agreementPrice = unitPrice_TP.AgreementPrice ?? 0;

                var expMortgageList_TP = await db.TransferPromotionExpenses
                           .Include(o => o.TransferPromotion)
                           .Include(o => o.ExpenseReponsibleBy)
                           .Include(o => o.MasterPriceItem)
                           .Where(o => o.TransferPromotion.BookingID == bookingID
                                   && (
                                        o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                                        || o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan
                                    )
                                )
                           .ToListAsync() ?? new List<TransferPromotionExpense>();

                var expMortgage_TP = expMortgageList_TP
                        .Find(o => o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee)
                          ?? new();

                var expMortgageOver_TP = expMortgageList_TP
                        .Find(o => o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan)
                          ?? new();

                var freeMortgageAmount = await db.UnitPriceItems
                        .Include(o => o.MasterPriceItem)
                        .Where(o =>
                                o.UnitPriceID == unitPrice_TP.ID
                                && o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                            )
                        .SumAsync(o => (o.PricePerUnitAmount ?? 0));

                var freeMortgageOverAmount = await db.UnitPriceItems
                        .Include(o => o.MasterPriceItem)
                        .Where(o =>
                                o.UnitPriceID == unitPrice_TP.ID
                                && o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan
                            )
                        .SumAsync(o => (o.PricePerUnitAmount ?? 0));

                var salePromotion_AG = await db.SalePromotions
                        .Include(o => o.SalePromotionStage)
                        .Where(o =>
                                o.BookingID == bookingID
                                && o.SalePromotionStage.Key == SalePromotionStageKeys.Agreement
                            )
                        .OrderByDescending(o => o.Created)
                        .FirstOrDefaultAsync();

                var expMortgage_AG = await db.SalePromotionExpenses
                           .Include(o => o.ExpenseReponsibleBy)
                           .Include(o => o.MasterPriceItem)
                           .Where(o =>
                                o.SalePromotionID == salePromotion_AG.ID
                                && o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                            ).FirstOrDefaultAsync() ?? new SalePromotionExpense();

                decimal approvedAmount = this.ApprovedAmount;
                decimal approvedAmountNew = (freeMortgageOverAmount + agreementPrice);

                var msgX = "";
                msgX += "คำเตือน !! การเปลี่ยนแปลงยอดขอสินเชื่อ จะมีผลกระทบกับโปรโมชั่นโอน 'ฟรีค่าจดจำนอง'";
                msgX += Environment.NewLine;
                msgX += "กรุณายกเลิกโปรโมชั่นเดิม และทำรายการขอสินเชื่อให้เรียบร้อยก่อนให้โปรโอนใหม่อีกครั้ง";

                //เคส 1  ให้ฟรีตอนสัญญา แล้วมาให้กู้เกินฟรีที่หน้าโปรโอน และมาแก้ยอดกู้เพิ่มทีหลัง
                if (
                        expMortgage_AG.ExpenseReponsibleBy?.Key == ExpenseReponsibleByKeys.Company
                        && expMortgageOver_TP.ExpenseReponsibleBy?.Key == ExpenseReponsibleByKeys.Company
                        && (
                                //ยอดอนุมัติรวม > (ค่าจดจำนองที่ให้ฟรีตอนโปรโอน + ราคาบ้านในสัญญา)
                                approvedAmount > approvedAmountNew
                            )
                        && this.LoanStatus?.Key == LoanStatusKeys.Approve
                        && (this.IsUseBank ?? false) == true
                    )
                {
                    var msg = msgX;
                    ex.AddError("ERRX001", msg, 0);
                }
                //เคส 2 จดจำนองไม่ได้ให้ฟรีตอนสัญญา มาให้ฟรีตอนโปรโอน และมาแก้ยอดกู้เพิ่มทีหลัง
                else if (
                        expMortgage_AG.ExpenseReponsibleBy?.Key == ExpenseReponsibleByKeys.Customer
                        && expMortgage_TP.ExpenseReponsibleBy?.Key == ExpenseReponsibleByKeys.Company
                        && (
                                //ยอดอนุมัติรวม > ค่าจดจำนองที่ให้ฟรีตอนโปรโอน
                                approvedAmount > freeMortgageAmount
                            )
                        && this.LoanStatus?.Key == LoanStatusKeys.Approve
                        && (this.IsUseBank ?? false) == true
                        && (
                            //ถ้าค่าจดจำนองที่ให้ฟรีตอนโปรโอน เท่ากับ AgreementPrice ในสัญญา ให้แก้ขอสินเชื่อได้อิสระ 
                            //แต่ถ้าไม่เท่าค่อยเข้าเงื่อนไขในภาพ (ปรับเฉพาะเคสที่ 2)
                            agreementPrice != freeMortgageAmount
                        )
                    )
                {
                    var msg = msgX;
                    ex.AddError("ERRX001", msg, 0);
                }

            }

            #endregion

            if (ex.HasError)
            {
                throw ex;
            }
        }

        //public async Task ValidateCreditBankingTypeAsync(DatabaseContext db)
        //{
        //    ValidateException ex = new ValidateException();

        //    this.Id = this.Id ?? new Guid();

        //    if (this.Bank == null)
        //    {
        //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
        //        string desc = this.GetType().GetProperty(nameof(CreditBankingDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
        //        var msg = errMsg.Message.Replace("[field]", desc);
        //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
        //    }

        //    if (ex.HasError)
        //    {
        //        throw ex;
        //    }
        //}

        public void ToModel(ref CreditBanking model)
        {
            model.BookingID = this.Booking.Id;
            model.Remark = this.Remark;
            model.FinancialInstitutionMasterCenterID = this.FinancialInstitution?.Id;
            model.BankID = this.Bank?.Id;
            model.BankBranchID = this.BankBranch?.Id;
            model.OtherBank = this.OtherBank;
            model.LoanSubmitDate = this.LoanSubmitDate;
            model.LoanAmount = this.LoanAmount;
            model.ApprovedLoanAPAmount = this.ApprovedLoanAPAmount;
            model.InsuranceAmount = this.InsuranceAmount;
            model.InsuranceOnFireAmount = this.InsuranceOnFireAmount;
            model.FirstDeductAmount = this.FirstDeductAmount;
            model.ReturnCustomerAmount = this.ReturnCustomerAmount;
            model.ApprovedAmount = this.ApprovedAmount;
            model.LoanStatusMasterCenterID = this.LoanStatus?.Id;
            model.ResultDate = this.ResultDate;
            model.IsUseBank = this.IsUseBank;
            model.BankReasonMasterCenterID = this.BankReason?.Id;
            model.UseBankReasonMasterCenterID = this.UseBankReason?.Id;
            model.UseBankOtherReason = this.UseBankOtherReason;
            model.NotUseBankReasonMasterCenterID = this.NotUseBankReason?.Id;
            model.NotUseBankOtherReason = this.NotUseBankOtherReason;
            model.BankRejectReasonMasterCenterID = this.BankRejectReason?.Id;
            model.BankWaitingReasonMasterCenterID = this.BankWaitingReason?.Id;
            model.OtherBankBranch = this.OtherBankBranch;
            model.ProvinceID = this.Province?.Id;
            model.BankWaitingOtherReason = this.BankWaitingOtherReason;
            model.DocumentCompletetDate = this.DocumentCompletetDate;
        }
    }

    //public class CreditBankingQueryResult
    //{
    //    public CreditBanking CreditBanking { get; set; }
    //    public Booking Booking { get; set; }
    //    public Bank Bank { get; set; }
    //    public BankBranch BankBranch { get; set; }
    //    public MasterCenter FinancialInstitution { get; set; }
    //    public MasterCenter LoanStatus { get; set; }
    //    public MasterCenter BankReason { get; set; }
    //    public MasterCenter UseBankReason { get; set; }
    //    public MasterCenter NotUseBankReason { get; set; }
    //    public MasterCenter BankRejectReason { get; set; }
    //    public MasterCenter BankWaitingReason { get; set; }
    //}
}
