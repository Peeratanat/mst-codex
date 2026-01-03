using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.SAL;

using ErrorHandling;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    public class CancelMemoAdminDTO : BaseDTO
    {
        /// <summary>
        /// ข้อมูลโครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// ข้อมูลแปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// ข้อมูลผู้ทำสัญญา
        /// </summary>
        public AgreementOwnerDTO AgreementOwner { get; set; }

        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDTO Booking { get; set; }

        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }

        /// <summary>
        /// ราคาบ้าน
        /// </summary>
        public decimal? SellingPrice { get; set; }

        /// <summary>
        /// รูปแบบการยกเลิก
        /// Master/api/MasterCenters?masterCenterGroupKey=CancelReturnType
        /// </summary>
        public MST.MasterCenterDropdownDTO CancelReturn { get; set; }
        /// <summary>
        /// เหตุผลที่ยกเลิก
        /// Master/api/CancelReasons/DropdownList
        /// </summary>
        [Description("เหตุผลที่ยกเลิก")]
        public MST.CancelReasonDropdownDTO CancelReason { get; set; }
        ///// <summary>
        ///// หลักฐานกรณีกู้เงินไม่ผ่าน
        ///// </summary>
        //public FileDTO BankRejectDocument { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string CancelRemark { get; set; }
        /// <summary>
        /// รับเงินจากลูกค้า
        /// </summary>
        public decimal? TotalReceivedAmount { get; set; }
        /// <summary>
        /// มูลค่ารายการของที่ส่งมอบไปแล้ว
        /// </summary>
        public decimal? TotalPromotionDeliverAmount { get; set; }
        /// <summary>
        /// เบื้ยปรับ
        /// </summary>
        public decimal? PenaltyAmount { get; set; }
        /// <summary>
        /// เงินคืนลูกค้า
        /// </summary>
        public decimal? ReturnAmount { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - ธนาคาร
        /// </summary>
        public MST.BankDropdownDTO ReturnBank { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - บัญชีธนาคาร
        /// </summary>
        public string ReturnBankAccount { get; set; }
        /// <summary>
        /// จังหวัด
        /// Master/api/Provinces/DropdownList
        /// </summary>
        public MST.ProvinceListDTO Province { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - สาขา
        /// </summary>
        public MST.BankBranchDropdownDTO ReturnBankBranch { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - ชื่อบัญชี
        /// </summary>
        public string ReturnBankAccountName { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - เลขบัตรประชาชน
        /// </summary>
        public string ReturnCitizenIdentityNo { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - C=เลขบัตรประชาชน/P=เลขที่ Paspport/T=เลขประจำตัวผู้เสียภาษี
        /// </summary>
        public string ReturnCitizenIdentityType { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - จังหวัดที่ทำการคืน
        /// </summary>
        public Guid? ReturnProvinceID { get; set; }

        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - วันที่อนุมัติ
        /// </summary>
        public DateTime? ApproveDate { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - ผู้อนุมัติ
        /// </summary>
        public USR.UserDTO ApproveByUser { get; set; }
        ///// <summary>
        ///// ข้อมูลการจ่ายเงินคืนลูกค้า - สำเนา Book Bank
        ///// </summary>
        //public FileDTO ReturnBookBankFile { get; set; }
        /// <summary>
        /// ตรวจสอบการแก้ไขข้อมูล
        /// </summary>
        public bool IsUpdate { get; set; }

        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }


        public SAL.QuotationDTO Quotation { get; set; }

        public Guid? PaymentPreBookID { get; set; }

        public Guid? PaymentMethodPreBookID { get; set; }

        public string CustomerName { get; set; }

        public string CustomerLastName { get; set; }

        public void ToModel(ref CancelMemo model)
        {
            model.BookingID = this.Booking?.Id;
            model.AgreementID = this.Agreement?.Id;
            model.HasAgreemnt = this.Agreement != null;
            model.CancelReturnMasterCenterID = this.CancelReturn?.Id;
            model.CancelReasonID = this.CancelReason?.Id;
            model.CancelRemark = this.CancelRemark;
            model.TotalReceivedAmount = this.TotalReceivedAmount;
            model.TotalPromotionDeliverAmount = this.TotalPromotionDeliverAmount;
            model.PenaltyAmount = this.PenaltyAmount;
            model.ReturnAmount = this.ReturnAmount;
            model.ReturnBankID = this.ReturnBank?.Id;
            model.ReturnBankAccount = this.ReturnBankAccount;
            model.ReturnBankBranchID = this.ReturnBankBranch?.Id;
            model.ReturnBankAccountName = this.ReturnBankAccountName;
            model.ReturnCitizenIdentityNo = this.ReturnCitizenIdentityNo;
            //model.ReturnProvinceID = this.ReturnProvinceID != null ? this.ReturnProvinceID : this.Province != null ? this.Province.Id : null;

            if (this.Province != null)
            {
                model.ReturnProvinceID = this.Province?.Id;
            }
            else if (this.ReturnProvinceID != null)
            {
                model.ReturnProvinceID = this.ReturnProvinceID;
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            //TODO: [NP] Final Validate CancelReason
            ValidateException ex = new ValidateException();
            if (this.CancelReason == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CancelMemoDTO.CancelReason)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public static async Task<CancelMemoAdminDTO> CreateFromModelAsync(CancelMemo model, DatabaseContext DB)
        {
            if (model != null)
            {
                var agreeOwmer = await DB.AgreementOwners.Where(o => o.AgreementID == model.AgreementID && o.IsMainOwner == true).FirstOrDefaultAsync();
                var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == model.BookingID && o.IsActive == true).FirstOrDefaultAsync();

                CancelMemoAdminDTO result = new CancelMemoAdminDTO()
                {
                    Id = model.ID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Booking.Project),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Booking.Unit),
                    AgreementOwner = AgreementOwnerDTO.CreateFromModel(agreeOwmer),
                    Booking = BookingDTO.CreateFromModel(model.Booking, DB),
                    Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    SellingPrice = unitPrice?.SellingPrice ?? 0,
                    CancelReturn = MasterCenterDropdownDTO.CreateFromModel(model.CancelReturn),
                    CancelReason = CancelReasonDropdownDTO.CreateFromModel(model.CancelReason),
                    CancelRemark = model.CancelRemark,
                    TotalReceivedAmount = model.TotalReceivedAmount,
                    TotalPromotionDeliverAmount = model.TotalPromotionDeliverAmount,
                    PenaltyAmount = model.PenaltyAmount,
                    ReturnAmount = model.ReturnAmount,
                    ReturnBank = BankDropdownDTO.CreateFromModel(model.ReturnBank),
                    ReturnBankAccount = model.ReturnBankAccount,
                    Province = ProvinceListDTO.CreateFromModel(model.ReturnProvince),
                    ReturnBankBranch = BankBranchDropdownDTO.CreateFromModel(model.ReturnBankBranch),
                    ReturnBankAccountName = model.ReturnBankAccountName,
                    ReturnCitizenIdentityNo = model.ReturnCitizenIdentityNo,
                    //ReturnCitizenIdentityType = model.ReturnCitizenIdentityType,
                    ReturnProvinceID = model.ReturnProvinceID,
                    ApproveDate = model.ApproveDate,
                    ApproveByUser = UserDTO.CreateFromModel(model.ApproveByUser)
                };

                return result;
            }
            else
            {
                return null;
            }
        }


    }
}
