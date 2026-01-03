using Database.Models.MST;
using Database.Models.USR;
using System;
using System.ComponentModel;
using System.Linq;
using Database.Models.ACC;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.FIN;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using System.Threading.Tasks;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.DbQueries.FIN;

namespace Base.DTOs.FIN
{
    public class MemoMoveMoneySPDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        public string Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }

        /// <summary>
        /// เงินตั้งพัก
        /// </summary>
        [Description("เงินตั้งพัก")]
        public decimal Amount { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        [Description("เลขที่ใบเสร็จ")]
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ วันที่รับเงิน
        /// </summary>
        [Description("วันที่ใบเสร็จ วันที่รับเงิน")]
        public DateTime? ReceiveDate { get; set; }


        /// <summary>
        /// ช่องทางการรับเงิน/ประเภทชำระเงิน
        /// </summary>
        [Description("ช่องทางการรับเงิน/ประเภทชำระเงิน")]
        public Guid? PaymentMethodID { get; set; }
        public string PaymentMethod { get; set; }
        /// <summary>
        /// ธนาคารที่รับเงินผิด
        /// </summary>
        [Description("ธนาคารที่รับเงินผิด")]
        public Guid? BankID { get; set; }
        public string Bank { get; set; }
        /// <summary>
        /// บัญชีธนาคารที่รับเงินผิด
        /// </summary>
        [Description("บัญชีธนาคารที่รับเงินผิด")]
        public Guid? BankAccountID { get; set; }
        public string BankAccount { get; set; }

        /// <summary>
        /// บริษัท ที่สั่งจ่าย/บริษัทที่รับย้ายเงินใน Memo
        /// </summary>
        [Description("บริษัท ที่สั่งจ่าย/บริษัทที่รับย้ายเงินใน Memo")]
        public Guid? DestinationCompanyID { get; set; }
        public string DestinationCompany { get; set; }

        /// <summary>
        /// บริษัทที่โอนเงินผิด
        /// </summary>
        [Description("บริษัทที่โอนเงินผิด")]
        public Guid? CompanyID { get; set; }
        public string Company { get; set; }
        /// <summary>
        /// วันที่พิมพ์ ล่าสุด
        /// </summary>
        [Description("วันที่พิมพ์ ล่าสุด")]
        public DateTime? PrintDate { get; set; }

        /// <summary>
        /// ผู้ที่พิมพ์ ล่าสุด
        /// </summary>
        [Description("ผู้ที่พิมพ์ ล่าสุด")]
        public string PrintBy { get; set; }
        public USR.UserListDTO PrintByDTO { get; set; }
        /// <summary>
        /// สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์
        /// </summary>
        [Description("สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์")]
        public bool IsPrint { get; set; }

        /// <summary>
        /// วัตถุประสงค์
        /// </summary>
        [Description("วัตถุประสงค์")]
        public Guid? MoveMoneyReasonMasterCenterID { get; set; }
        public string MoveMoneyReason { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        public void ToModel(ref MemoMoveMoney model, bool isPrint, Guid? userLogin)
        {
            model.PaymentMethodID = this.PaymentMethodID;
            model.DestinationCompanyID = this.DestinationCompanyID??new Guid();
            model.MoveMoneyReasonMasterCenterID = this.MoveMoneyReasonMasterCenterID?? new Guid();
            model.Remark = this.Remark;
            model.IsPrint = isPrint;
            model.PrintByID = userLogin;
            model.PrintDate = this.PrintDate;
        }

        public static async Task ValidateAsync(DatabaseContext db, List<MemoMoveMoneySPDTO> listdata)
        {
            ValidateException ex = new ValidateException();
            // Row Count > 1
            if (listdata.Count > 0)
            {
                MemoMoveMoneySPDTO modelFirst = listdata[0];
                if (modelFirst.DestinationCompanyID == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                    string desc = modelFirst.GetType().GetProperty(nameof(MemoMoveMoneyDTO.DestinationCompany)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (modelFirst.MoveMoneyReasonMasterCenterID == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                    string desc = modelFirst.GetType().GetProperty(nameof(MemoMoveMoneyDTO.MoveMoneyReason)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (modelFirst.DestinationCompanyID != null)
                {
                    var checkCompany = listdata.Where(x => x.CompanyID == x.DestinationCompanyID).ToList();
                    if (checkCompany.Count > 0)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0106").FirstOrDefaultAsync();
                        string desc = modelFirst.GetType().GetProperty(nameof(MoveMoneyReason)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message;
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
        public static MemoMoveMoneySPDTO CreateFromQueryResult(dbqMemoMoveMoneySP model)
        {
            if (model != null)
            {
                var result = new MemoMoveMoneySPDTO()
                {
                    Id = model.ID,
                    PaymentMethodID = model.PaymentMethodID,
                    ProjectID = model.ProjectID,
                    Project = model.Project,
                    UnitID = model.UnitID,
                    UnitNo = model.UnitNo,
                    Amount = model.Amount,
                    ReceiptTempNo = model.ReceiptTempNo,
                    ReceiveDate = model.ReceiveDate,
                    PaymentMethod = model.PaymentMethod,
                    CompanyID = model.CompanyID,
                    Company = model.Company,
                    BankID = model.BankID,
                    Bank = model.Bank,
                    BankAccountID = model.BankAccountID,
                    BankAccount = model.BankAccount,
                    DestinationCompanyID = model.DestinationCompanyID,
                    DestinationCompany = model.DestinationCompany,
                    PrintDate = model.PrintDate,
                    PrintBy = model.PrintBy,
                    IsPrint = model.IsPrint ?? false,
                    MoveMoneyReasonMasterCenterID = model.MoveMoneyReasonMasterCenterID,
                    MoveMoneyReason = model.MoveMoneyReason,
                    Remark = model.Remark
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
