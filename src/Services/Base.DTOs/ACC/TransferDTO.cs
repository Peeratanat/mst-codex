using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MST;
using Database.Models.DbQueries.ACC; 
using Database.Models.SAL; 
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Database.Models.ACC;
using Database.Models.MasterKeys;

namespace Base.DTOs.ACC
{
    public class TransferDTO : BaseDTO
    {
        /// <summary>
        /// จำนวนเงินทำคืนฝ่ายโอน
        /// </summary>
        [Description("จำนวนเงินทำคืนฝ่ายโอน")]
        public decimal PettyCashAmount { get; set; }

        /// <summary>
        /// PCard กระทรวงการคลัง
        /// </summary>
        [Description("PCard กระทรวงการคลัง")]
        public decimal MinistryPCard { get; set; }
        /// <summary>
        /// ภาษีเงินได้
        /// </summary>
        [Description("ภาษีเงินได้")]
        public decimal CompanyIncomeTax { get; set; }
        /// <summary>
        /// ภาษีธุรกิจเฉพาะ
        /// </summary>
        [Description("ภาษีธุรกิจเฉพาะ")]
        public decimal BusinessTax { get; set; }
        /// <summary>
        /// ค่าธรรมเนียมการโอนบริษัทจ่าย
        /// </summary>
        [Description("ค่าธรรมเนียมการโอนบริษัทจ่าย")]
        public decimal CompanyPayFee { get; set; }
        /// <summary>
        /// ค่าธรรมเนียมการโอนลูกค้าจ่าย
        /// </summary>
        [Description("ค่าธรรมเนียมการโอนลูกค้าจ่าย")]
        public decimal CustomerPayFee { get; set; }
        /// <summary>
        /// ฟรีค่าใช้จ่าย
        /// </summary>
        [Description("ฟรีค่าใช้จ่าย")]
        public decimal FreeTransferExpenseAmount { get; set; }
        /// <summary>
        /// ฟรีดาวน์
        /// </summary>
        [Description("ฟรีดาวน์")]
        public decimal FreedownAmount { get; set; }
        /// <summary>
        /// รายได้ค่าดำเนินการเอกสาร
        /// </summary>
        [Description("รายได้ค่าดำเนินการเอกสาร")]
        public decimal DocumentFeeChargeAmount { get; set; }
        /// <summary>
        /// ฟรีค่าส่วนกลาง
        /// </summary>
        [Description("ฟรีค่าส่วนกลาง")]
        public decimal CommonFeeChargeAmount { get; set; }
        /// <summary>
        /// ฟรีค่ากองทุน
        /// </summary>
        [Description("ฟรีค่ากองทุน")]
        public decimal FirstSinkingFundAmount { get; set; }
        /// <summary>
        /// ฟรีค่ามิเตอร์น้ำ
        /// </summary>
        [Description("ฟรีค่ามิเตอร์น้ำ")]
        public decimal WaterMeterAmount { get; set; }
        /// <summary>
        /// ฟรีค่ามิเตอร์ไฟฟ้า
        /// </summary>
        [Description("ฟรีค่ามิเตอร์ไฟฟ้า")]
        public decimal ElectricMeterAmount { get; set; }
        /// <summary>
        /// ปัดเศษ
        /// </summary>
        [Description("ปัดเศษ")]
        public decimal NonGiveChangeAmount { get; set; }
        [Description("WelcomeHome")]
        public decimal WelcomeHome { get; set; }
        [Description("ActualDocumentFee")]
        public decimal ActualDocumentFee { get; set; }
        /// <summary>
        /// UnitNo 
        /// </summary>
        [Description("แปลง")]
        public UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// โครงการ 
        /// </summary>
        [Description("โครงการ")]
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// ชื่อ - สกุล 
        /// </summary>
        [Description("ชื่อ - สกุล")]
        public string CustomerName { get; set; }

        /// <summary>
        /// สถานะโอนกรรมสิทธิ์
        /// AccountApproved=บัญชีอนุมัติ
        /// PaymentConfirmed=ยืนยันชำระเงิน
        /// TransferConfirmed=ยืนยันโอนจริง
        /// ReadyToTransfer=พร้อมโอน
        /// Start=ตั้งเรื่องโอน
        /// </summary>
        [Description("สถานะโอนกรรมสิทธิ์")]
        public string TransferStatus { get; set; }
        /// <summary>
        /// สถานะโอนกรรมสิทธิ์
        /// AccountApproved=บัญชีอนุมัติ
        /// PaymentConfirmed=ยืนยันชำระเงิน
        /// TransferConfirmed=ยืนยันโอนจริง
        /// ReadyToTransfer=พร้อมโอน
        /// Start=ตั้งเรื่องโอน
        /// </summary>
        public string TransferStatusName { get; set; }

        /// <summary>
        /// บัญชีอนุมัติ 1=อนุมัติ  0=ยังไม่อนุมัติ
        /// </summary>
        [Description("บัญชีอนุมัติ")]
        public bool IsAccountApproved { get; set; }

        /// <summary>
        /// สถานะโพสค่าใช้จ่าย 1=Post แล้ว  0=ยังไม่ Post
        /// </summary>
        [Description("สถานะโพสค่าใช้จ่าย")]
        public bool PostKAStatus { get; set; }

        /// <summary>
        /// สถานะโพส RR 1=Post แล้ว  0=ยังไม่ Post
        /// </summary>
        [Description("สถานะโพส RR")]
        public bool PostRRStatus { get; set; }

        /// <summary>
        /// เลขที่ Doc (RR) 
        /// </summary>
        [Description("เลขที่ Doc (RR)")]
        public string DocNoRR { get; set; }

        /// <summary>
        /// เลขที่ Doc (KA)  
        /// </summary>
        [Description("เลขที่ Doc (KA)")]
        public string DocNoKA { get; set; }

        /// <summary>
        /// เลขที่ Doc (FD)  
        /// </summary>
        [Description("เลขที่ Doc (FD)")]
        public string DocNoFD { get; set; }

        /// <summary>
        /// วันที๋โอนจริง
        /// </summary>
        [Description("วันที๋โอนจริง")]
        public DateTime? ActualTransferDate { get; set; }

        /// <summary>
        /// วันที่Doc (KA)  
        /// </summary>
        [Description("วันที่Doc (KA)")]
        public DateTime? DocKADate { get; set; }

        /// <summary>
        /// วันที่Doc (RR)
        /// </summary>
        [Description("วันที่Doc (RR)")]
        public DateTime? DocRRDate { get; set; }

        /// <summary>
        /// ข้อมูล Post KR
        /// </summary>
        [Description("ข้อมูล Post KR")]
        public List<TransferPostKRDTO> TransferPostKRDTOList { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string RemarkError { get; set; }

        /// <summary>
        /// Error Post
        /// </summary>
        [Description("Error Post")]
        public bool ErrorPost { get; set; }
        /// <summary>
        /// จำนวนเงิน (บาท)
        /// </summary>
        [Description("จำนวนเงิน (บาท)")]
        public decimal Amount { get; set; }

        public decimal NetPrice { get; set; }
        /// <summary>
        /// WBS Number
        /// </summary>
        [Description("WBS Number")]
        public string SAPWBSNo { get; set; }

        /// <summary>
        /// ชื่อผู้โพส Doc (RR)  
        /// </summary>
        [Description("ชื่อผู้โพส Doc (RR)")]
        public string CreatedByKA { get; set; }

        /// <summary>
        /// ชื่อผู้โพส Doc (KA)  
        /// </summary>
        [Description("ชื่อผู้โพส Doc (KA)")]
        public string CreatedByRR { get; set; }
        public Guid? AgreementID { get; set; }
        public decimal? PCardFee { get; set; }// ค่าดำเนินการเอกสารจริง
        public bool? IsPCardBankWrong { get; set; }// ค่าดำเนินการเอกสารจริง

        public static async Task ValidateApproveAsync(DatabaseContext db, List<TransferDTO> listdata)
        {
            ValidateException ex = new ValidateException();
            // Row Count > 1
            if (listdata.Count > 0)
            {
                var model = listdata[0];
                //data ต้องมีสถานะ IsAccountApproved = fasle ทั้งหมด
                var checkIsAccApp = listdata.Where(x => x.IsAccountApproved == true).ToList();
                if (checkIsAccApp.Any())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefaultAsync();
                    var msg = "สถานะสิ้นสุดการโอนแล้ว ไม่สามารถอนุมัติได้";
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    throw ex;
                }
                //data ต้องมีสถานะ sPaymentConfirmed = true ทั้งหมด
                var checkSPaymentConfirmed = listdata.Where(x => !x.TransferStatus.Equals(TransferStatusAccKeys.PaymentConfirmed)).ToList();
                if (checkSPaymentConfirmed.Any())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefaultAsync();
                    string desc = model.GetType().GetProperty(nameof(TransferDTO.TransferStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                // ยังไม่ PostKAStatus 
                var listdataId = listdata.Select(x=>x.Id);
                var listDataInput = db.PostGLHeaders.Where(x => listdataId.Contains( x.ReferentID ) && x.IsCancel == false).ToList();
 
                if (listDataInput.Any())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefaultAsync();
                    var msg = "ไม่สามารถอนุมัติได้ เนื่องจากมีรายการ Post KR หรือ RR แล้ว";
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                } 
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public static async Task ValidateCancleAsync(DatabaseContext db, List<TransferDTO> listdata)
        {
            ValidateException ex = new ValidateException();
            // Row Count > 1
            if (listdata.Count > 0)
            {
                //data ต้องมีสถานะ IsAccountApproved = true ทั้งหมด
                var checkIsAccApp = listdata.Where(x => x.IsAccountApproved == false).ToList();
                var model = listdata[0];
                if (checkIsAccApp.Any())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefaultAsync();
                    var msg = "ไม่สามารถยกเลิกอนุมัติได้ เนื่องจากสถานะเป็นยืนยันการชำระเงินแล้ว";
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                // ยังไม่ PostKAStatus 
                var listdataId = listdata.Select(x => x.Id);
                var listDataInput = db.PostGLHeaders.Where(x => listdataId.Contains(x.ReferentID) && x.IsCancel == false).ToList();

                if (listDataInput.Any())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefaultAsync();
                    var msg = "ไม่สามารถอนุมัติได้ เนื่องจากมีรายการ Post KR หรือ RR แล้ว";
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public static TransferTmp CreateFromModel(dbqTransfer model)
        {
            if (model != null)
            {
                var result = new TransferTmp()
                {

                    PettyCashAmount = model.PettyCashAmount,
                    MinistryPCard = model.MinistryPCard ,
                    CompanyIncomeTax = model.CompanyIncomeTax,
                    BusinessTax = model.BusinessTax ,
                    CompanyPayFee = model.CompanyPayFee ,
                    CustomerPayFee = model.CustomerPayFee ,
                    FreeTransferExpenseAmount = model.FreeTransferExpenseAmount ,
                    FreedownAmount = model.FreedownAmount ,
                    DocumentFeeChargeAmount = model.DocumentFeeChargeAmount ,
                    CommonFeeChargeAmount = model.CommonFeeChargeAmount ,
                    FirstSinkingFundAmount = model.FirstSinkingFundAmount ,
                    WaterMeterAmount = model.WaterMeterAmount ,
                    ElectricMeterAmount = model.ElectricMeterAmount ,
                    NonGiveChangeAmount = model.NonGiveChangeAmount ,
                    CustomerName = model.CustomerName,
                    TransferStatus = model.TransferStatus,
                    TransferStatusName = model.TransferStatusName,
                    IsAccountApproved = model.IsAccountApproved ,
                    PostKAStatus = model.PostKAStatus ,
                    PostRRStatus = model.PostRRStatus,
                    DocNoRR = model.DocNoRR,
                    DocNoKA = model.DocNoKA,
                    DocNoFD = model.DocNoFD,
                    ID = model.ID,
                    ActualTransferDate = model.ActualTransferDate,
                    DocKADate = model.DocKADate,
                    DocRRDate = model.DocRRDate,
                    NetPrice = model.NetPrice ,
                    WelcomeHome = model.WelcomeHome ,
                    ActualDocumentFee = model.ActualDocumentFee,
                    PCardFee = model.PCardFee,
                    IsPCardBankWrong = model.IsPCardBankWrong,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedDate = model.UpdatedDate,
                    SumPettyCashAmount = model.SumPettyCashAmount ,
                    SumMinistryPCard = model.SumMinistryPCard ,
                    SumCompanyIncomeTax = model.SumCompanyIncomeTax ,
                    SumBusinessTax = model.SumBusinessTax ,
                    SumCompanyPayFee = model.SumCompanyPayFee ,
                    SumCustomerPayFee = model.SumCustomerPayFee ,
                    SumFreeTransferExpenseAmount = model.SumFreeTransferExpenseAmount ,
                    SumFreedownAmount = model.SumFreedownAmount ,
                    SumDocumentFeeChargeAmount = model.SumDocumentFeeChargeAmount ,
                    SumCommonFeeChargeAmount = model.SumCommonFeeChargeAmount ,
                    SumFirstSinkingFundAmount = model.SumFirstSinkingFundAmount ,
                    SumWaterMeterAmount = model.SumWaterMeterAmount ,
                    SumElectricMeterAmount = model.SumElectricMeterAmount ,
                    SumNonGiveChangeAmount = model.SumNonGiveChangeAmount ,
                    SumWelcomeHome = model.SumWelcomeHome ,
                    SumActualDocumentFee = model.SumActualDocumentFee,
                    SumPCardFee = model.SumPCardFee,
                    CreatedByRR = model.CreatedByRR,
                    CreatedByKA = model.CreatedByKA,
                    AgreementID = model.AgreementID,
                    Row = 1,
                };
                result.Unit = new UnitDropdownDTO();
                result.Unit.Id = model.UnitID ?? new Guid();
                result.Unit.UnitNo = model.UnitNo;
                result.Unit.HouseNo = model.HouseNo;
                result.Project = new ProjectDropdownDTO();
                result.Project.Id = model.ProjectID;
                result.Project.ProjectNameTH = model.ProjectName;
                var company = new CompanyDropdownDTO();
                company.Id = model.CompanyID ?? new Guid();
                company.NameTH = model.CompanyName;
                result.Project.Company = company;
                return result;
            }
            else
            {
                return null;
            }
        }

        public static TransferDTO CreateToTranferDTO(TransferTmp model)
        {
            if (model != null)
            {
                var result = new TransferDTO()
                {

                    PettyCashAmount = model.PettyCashAmount,
                    MinistryPCard = model.MinistryPCard,
                    CompanyIncomeTax = model.CompanyIncomeTax,
                    BusinessTax = model.BusinessTax,
                    CompanyPayFee = model.CompanyPayFee,
                    CustomerPayFee = model.CustomerPayFee,
                    FreeTransferExpenseAmount = model.FreeTransferExpenseAmount,
                    FreedownAmount = model.FreedownAmount,
                    DocumentFeeChargeAmount = model.DocumentFeeChargeAmount,
                    CommonFeeChargeAmount = model.CommonFeeChargeAmount,
                    FirstSinkingFundAmount = model.FirstSinkingFundAmount,
                    WaterMeterAmount = model.WaterMeterAmount,
                    ElectricMeterAmount = model.ElectricMeterAmount,
                    NonGiveChangeAmount = model.NonGiveChangeAmount,
                    CustomerName = model.CustomerName,
                    TransferStatus = model.TransferStatus,
                    TransferStatusName = model.TransferStatusName,
                    IsAccountApproved = model.IsAccountApproved,
                    AgreementID = model.AgreementID,
                    PostKAStatus = model.PostKAStatus,
                    PostRRStatus = model.PostRRStatus,
                    DocNoRR = model.DocNoRR,
                    DocNoKA = model.DocNoKA,
                    DocNoFD = model.DocNoFD,
                    Id = model.ID,
                    ActualTransferDate = model.ActualTransferDate,
                    DocKADate = model.DocKADate,
                    DocRRDate = model.DocRRDate,
                    NetPrice = model.NetPrice, 
                    WelcomeHome = model.WelcomeHome,
                    ActualDocumentFee = model.ActualDocumentFee, 
                    PCardFee = model.PCardFee,
                    IsPCardBankWrong = model.IsPCardBankWrong,
                    Unit = model.Unit,
                    Project = model.Project,
                    CreatedByRR = model.CreatedByRR,
                    CreatedByKA = model.CreatedByKA,
                    Updated = model.UpdatedDate,
                    UpdatedBy = model.UpdatedBy
                }; 
                return result;
            }
            else
            {
                return null;
            }
        }

        public bool ValidateListPostKRAsync(ref string ErrorMsg, TransferDTO model, DatabaseContext db)
        {
            bool result = false;
            var errMsgERR0089 = db.ErrorMessages.Where(o => o.Key == "ERR0121").FirstOrDefault();
            ErrorMsg = errMsgERR0089.Message + " : ";
            List<TransferPostKRDTO> listPostKR = model.TransferPostKRDTOList;
            // GLAccount ต้องมีค่า
            {
                var GLAccountNull = listPostKR.Where(x => x.GLAccount == null).ToList();
                if (GLAccountNull.Count > 0)
                {
                    result = true;
                    var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefault();
                    string desc = GLAccountNull.FirstOrDefault().GetType().GetProperty(nameof(TransferPostKRDTO.GLAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ErrorMsg += msg + " ";
                }
                else
                {
                    // GL Code ต้องมีค่า
                    var GLCodeNull = listPostKR.Where(x => string.IsNullOrEmpty(x.GLAccount.GLAccountNo)).ToList();
                    if (GLCodeNull.Count > 0)
                    {
                        result = true;
                        var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefault();
                        string desc = GLCodeNull.FirstOrDefault().GetType().GetProperty(nameof(TransferPostKRDTO.GLAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ErrorMsg += msg + " ";
                    }
                    else
                    {
                        // GL Name ต้องมีค่า
                        var GLNameNull = listPostKR.Where(x => string.IsNullOrEmpty(x.GLAccount.GLAccountType != null ? x.GLAccount.GLAccountType.Name : string.Empty)).ToList();
                        if (GLNameNull.Count > 0)
                        {
                            result = true;
                            var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefault();
                            string desc = GLNameNull.FirstOrDefault().GetType().GetProperty(nameof(TransferPostKRDTO.GLAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ErrorMsg += msg + " ";
                        }
                    }
                }
            }
            // ผลรวม Dr = Cr
            decimal SumDr = listPostKR.Sum(x => x.DebitAmount);
            decimal SumCr = listPostKR.Sum(x => x.CreditAmount);
            if (SumDr != SumCr)
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0122").FirstOrDefault();
                ErrorMsg += errMsg.Message + " ";
            }
            var TransferModel = db.Transfers.Where(x => x.ID == model.Id).FirstOrDefault() ?? new Transfer();
            if (!TransferModel.IsAccountApproved)
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefault();
                var msg = "สถานะไม่ถูกต้อง";
                ErrorMsg += msg + " ";
            }
            var PostGLHeaderModel = db.PostGLHeaders.Where(x => x.ReferentID == model.Id && x.ReferentType.Equals(PostGLDocumentTypeKeys.KM) && x.IsCancel == false).FirstOrDefault() ?? new PostGLHeader();
            if (PostGLHeaderModel.ReferentID != null)
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefault();
                //string desc = model.GetType().GetProperty(nameof(TransferDTO.PostKAStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = "รายการนี้สถานะเป็น Posted แล้ว";
                ErrorMsg += msg + " ";
            } 
            return result;
        }
        public bool ValidateProcessPostKRAsync(ref string ErrorMsg, Transfer model, DatabaseContext db)
        {
            bool result = false;
            var errMsgERR0089 = db.ErrorMessages.Where(o => o.Key == "ERR0121").FirstOrDefault();
            ErrorMsg = errMsgERR0089.Message + " : ";
            // SAPWBSNo,SAPWBSObject ต้องมีค่า 
            if (string.IsNullOrEmpty(model.Agreement.Unit.SAPWBSObject))
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefault();
                string desc = model.Agreement.Unit.GetType().GetProperty(nameof(UnitDTO.SapwbsObject)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ErrorMsg += msg + " ";
            }
            if (string.IsNullOrEmpty(model.Agreement.Unit.SAPWBSNo))
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefault();
                string desc = model.Agreement.Unit.GetType().GetProperty(nameof(UnitDTO.SapwbsNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ErrorMsg += msg + " ";
            }
            var PostGLHeaderModel = db.PostGLHeaders.Where(x => x.ReferentID == model.ID && x.ReferentType.Equals(PostGLDocumentTypeKeys.KM) && x.IsCancel == false).FirstOrDefault() ?? new PostGLHeader();
            if (PostGLHeaderModel.ReferentID != null)
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefault();
                //string desc = model.GetType().GetProperty(nameof(TransferDTO.PostKAStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = "รายการนี้สถานะเป็น Posted แล้ว";
                ErrorMsg += msg + " ";
            }
            var TransferModel = db.Transfers.Where(x => x.ID == model.ID).FirstOrDefault() ?? new Transfer();
            if (!TransferModel.IsAccountApproved)
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefault();
                var msg = "สถานะไม่ถูกต้อง";
                ErrorMsg += msg + " ";
            }
            return result;
        }
        public bool ValidateProcessPostRRAsync(ref string ErrorMsg, Transfer model, DatabaseContext db)
        {
            bool result = false;
            var errMsgERR0089 = db.ErrorMessages.Where(o => o.Key == "ERR0121").FirstOrDefault();
            ErrorMsg = errMsgERR0089.Message + " : ";
            // SAPWBSNo,SAPWBSObject ต้องมีค่า 
            if (string.IsNullOrEmpty(model.Agreement.Unit.SAPWBSObject))
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefault();
                string desc = model.Agreement.Unit.GetType().GetProperty(nameof(UnitDTO.SapwbsObject)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ErrorMsg += msg + " ";
            }
            if (string.IsNullOrEmpty(model.Agreement.Unit.SAPWBSNo))
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefault();
                string desc = model.Agreement.Unit.GetType().GetProperty(nameof(UnitDTO.SapwbsNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ErrorMsg += msg + " ";
            }
            var TransferModel = db.Transfers.Where(x => x.ID == model.ID).FirstOrDefault() ?? new Transfer();
            if (!TransferModel.IsAccountApproved)
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefault();
                var msg = "สถานะไม่ถูกต้อง";
                ErrorMsg += msg + " ";
            }
            var PostGLHeaderModel = db.PostGLHeaders.Where(x => x.ReferentID == model.ID && x.ReferentType.Equals(PostGLDocumentTypeKeys.RR) && x.IsCancel == false).FirstOrDefault() ?? new PostGLHeader();
            if (PostGLHeaderModel.ReferentID != null)
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefault();
                //string desc = model.GetType().GetProperty(nameof(TransferDTO.PostKAStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = "รายการนี้สถานะเป็น Posted แล้ว";
                ErrorMsg += msg + " ";
            } 
            return result;
        }
        public bool ValidateBankAccAsync(ref string ErrorMsg, BankAccount creditLowAcc, BankAccount creditHighAcc, BankAccount debitAcc, DatabaseContext db)
        {
            bool result = false;
            var errMsgERR0089 = db.ErrorMessages.Where(o => o.Key == "ERR0121").FirstOrDefault();
            ErrorMsg = errMsgERR0089.Message + " : ";
            TransferDTO tamp = new TransferDTO();
            // GL Code ต้องมีค่า 
            if (string.IsNullOrEmpty(creditLowAcc.GLAccountNo) || string.IsNullOrEmpty(creditLowAcc.GLAccountNo) || string.IsNullOrEmpty(creditHighAcc.GLAccountNo)) 
            {
                result = true;
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefault();
                var TransferPostKRDTOTMP = new TransferPostKRDTO();
                string desc = TransferPostKRDTOTMP.GetType().GetProperty(nameof(TransferPostKRDTO.GLAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                //string desc = GetType().GetProperty(nameof(TransferDTO.GLAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ErrorMsg += msg + " ";
            } 
            return result;
        }
    }

    public class TransferTmp
    {
        public decimal PettyCashAmount { get; set; }
        public decimal MinistryPCard { get; set; }
        public decimal CompanyIncomeTax { get; set; }
        public decimal BusinessTax { get; set; }
        public decimal CompanyPayFee { get; set; }
        public decimal CustomerPayFee { get; set; }
        public decimal FreeTransferExpenseAmount { get; set; }
        public decimal FreedownAmount { get; set; }
        public decimal DocumentFeeChargeAmount { get; set; }
        public decimal CommonFeeChargeAmount { get; set; }
        public decimal FirstSinkingFundAmount { get; set; }
        public decimal WaterMeterAmount { get; set; }
        public decimal ElectricMeterAmount { get; set; }
        public decimal NonGiveChangeAmount { get; set; }
        public decimal WelcomeHome { get; set; }
        public decimal ActualDocumentFee { get; set; }
        public UnitDropdownDTO Unit { get; set; }
        public ProjectDropdownDTO Project { get; set; }
        public string CustomerName { get; set; }
        public string TransferStatus { get; set; }
        public string TransferStatusName { get; set; }
        public bool IsAccountApproved { get; set; }
        public Guid? AgreementID { get; set; }
        public bool PostKAStatus { get; set; }
        public bool PostRRStatus { get; set; }
        public string DocNoRR { get; set; }
        public string DocNoKA { get; set; }
        public string DocNoFD { get; set; }
        public DateTime? ActualTransferDate { get; set; }
        public DateTime? DocKADate { get; set; }
        public DateTime? DocRRDate { get; set; }
        public List<TransferPostKRDTO> TransferPostKRDTOList { get; set; }
        public string RemarkError { get; set; }
        public bool ErrorPost { get; set; }
        public decimal Amount { get; set; }
        public decimal NetPrice { get; set; }
        public string SAPWBSNo { get; set; }
        public decimal? PCardFee { get; set; }// ค่าดำเนินการเอกสารจริง
        public bool? IsPCardBankWrong { get; set; }// ค่าดำเนินการเอกสารจริง
        public int Row { get; set; }
        public decimal SumMinistryPCard { get; set; }
        public decimal SumCompanyIncomeTax { get; set; }
        public decimal SumBusinessTax { get; set; }
        public decimal SumCompanyPayFee { get; set; }
        public decimal SumCustomerPayFee { get; set; }
        public decimal SumPettyCashAmount { get; set; }
        public decimal SumFreeTransferExpenseAmount { get; set; }
        public decimal SumFreedownAmount { get; set; }
        public decimal SumDocumentFeeChargeAmount { get; set; }
        public decimal SumCommonFeeChargeAmount { get; set; }
        public decimal SumFirstSinkingFundAmount { get; set; }
        public decimal SumWaterMeterAmount { get; set; }
        public decimal SumElectricMeterAmount { get; set; }
        public decimal SumNonGiveChangeAmount { get; set; }
        public decimal SumWelcomeHome { get; set; }
        public decimal SumActualDocumentFee { get; set; }
        public decimal SumPCardFee { get; set; }
        public Guid? ID { get; set; }
        public string CreatedByKA { get; set; }
        public string CreatedByRR { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
