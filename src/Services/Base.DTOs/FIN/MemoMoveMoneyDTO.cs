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
    public class MemoMoveMoneyDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal Amount { get; set; }


        /// <summary>
        /// ช่องทางการรับเงิน/ประเภทชำระเงิน
        /// </summary>
        [Description("ช่องทางการรับเงิน/ประเภทชำระเงิน")]
        public MST.MasterCenterDropdownDTO PaymentMethodType { get; set; }


        /// <summary>
        /// ธนาคาร
        /// </summary>
        [Description("ธนาคาร")]
        public MST.BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// บัญชีธนาคารที่รับเงินผิด
        /// </summary>
        [Description("บัญชีธนาคารที่รับเงินผิด")]
        public MST.BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// บริษัทที่โอนเงินผิด ,บริษัทที่รับเงินผิดจากลูกค้า
        /// </summary>
        [Description("บริษัทที่โอนเงินผิด")]
        public MST.CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// บริษัทที่รับเงินเข้า ,บริษัทที่รับย้ายเงินใน Memo
        /// </summary>
        [Description("บริษัทที่รับเงินเข้า")]
        public MST.CompanyDropdownDTO DestinationCompany { get; set; }

        /// <summary>
        /// วันที่พิมพ์ ล่าสุด
        /// </summary>
        [Description("วันที่พิมพ์ ล่าสุด")]
        public DateTime? PrintDate { get; set; }

        /// <summary>
        /// ผู้ที่พิมพ์ ล่าสุด
        /// </summary>
        [Description("ผู้ที่พิมพ์ ล่าสุด")]
        public USR.UserListDTO PrintBy { get; set; }

        /// <summary>
        /// สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์
        /// </summary>
        [Description("สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์")]
        public bool IsPrint { get; set; }
        public bool PaymentIsCancel { get; set; }

        /// <summary>
        /// วัตถุประสงค์
        /// </summary>
        [Description("วัตถุประสงค์")]
        public MST.MasterCenterDropdownDTO MoveMoneyReason { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        [Description("เลขที่ใบเสร็จ")]
        public string ReceiptNo { get; set; }

        /// <summary>
        /// วันที่รับเงิน
        /// </summary>
        [Description("วันที่รับเงิน")]
        public DateTime? ReceiptDate { get; set; }

        /// <summary>
        /// ช่องทางชำระ
        /// </summary>
        [Description("ช่องทางชำระ")]
        public Guid? PaymentMethodId { get; set; }

        /// <summary>
        /// ชำระเงินจาก UN , PAYMENT
        /// </summary>
        [Description("ชำระเงินจาก")]
        public string PaymentFrom { get; set; }

        public static MemoMoveMoneyDTO CreateFromModel(dbqMemoMoveMoneySP model)
        {
            if (model != null)
            {
                MemoMoveMoneyDTO result = new MemoMoveMoneyDTO();  
                result.Id = model.ID ;
                result.PaymentMethodId = model.PaymentMethodID;
                result.PaymentMethodType = new MasterCenterDropdownDTO();
                result.PaymentMethodType.Id = model.PaymentMethodID ?? new Guid();
                result.PaymentMethodType.Name = model.PaymentMethod;
                result.Project = new ProjectDropdownDTO();
                result.Project.Id = model.ProjectID;
                result.Project.ProjectNameTH = model.Project;
                result.Unit = new UnitDropdownDTO();
                result.Unit.Id = model.UnitID ?? new Guid();
                result.Unit.UnitNo = model.UnitNo; 
                result.Amount = model.Amount; 
                result.ReceiptNo = model.ReceiptTempNo;
                result.ReceiptDate = model.ReceiveDate;
                result.Company = new CompanyDropdownDTO();
                result.Company.Id = model.CompanyID ?? new Guid();
                result.Company.NameTH = model.Company;
                result.Bank = new BankDropdownDTO();
                result.Bank.Id = model.BankID ?? new Guid();
                result.Bank.NameTH = model.Bank;
                result.Bank.NameEN = model.Bank;
                result.BankAccount = new BankAccountDropdownDTO();
                result.BankAccount.Id = model.BankAccountID;
                result.BankAccount.BankAccountNo = model.BankAccount;
                result.DestinationCompany = new CompanyDropdownDTO();
                result.DestinationCompany.Id = model.DestinationCompanyID ?? new Guid();
                result.DestinationCompany.NameTH = model.DestinationCompany;
                result.PrintDate = model.PrintDate;
                result.PrintBy = new UserListDTO();
                result.PrintBy.DisplayName = model.PrintBy; 
                result.IsPrint = model.IsPrint ?? false;
                result.MoveMoneyReason = new MasterCenterDropdownDTO();
                result.MoveMoneyReason.Id = model.MoveMoneyReasonMasterCenterID ?? new Guid();
                result.MoveMoneyReason.Name = model.MoveMoneyReason;
                result.Remark = model.Remark;
                result.PaymentIsCancel = model.PaymentIsCancel??false;
                result.PaymentFrom = model.PaymentFrom;
                return result;
            }
            else
            {
                return null;
            }
        }

        private static BankAccount createBankAccount(Bank bank  )
        {
            BankAccount result = new BankAccount(); 
            if (bank != null)
            {
                result.Bank = bank;
                result.BankID = bank.ID;
            }
            return result;
        }

        public static void SortBy(MemoMoveMoneySortByParam sortByParam, ref IQueryable<MemoMoveMoneyQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MemoMoveMoneySortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Banks.NameTH);
                        else query = query.OrderByDescending(o => o.Banks.NameTH);
                        break;
                    case MemoMoveMoneySortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Projects.ProjectNo);
                        else query = query.OrderByDescending(o => o.Projects.ProjectNo);
                        break;
                    case MemoMoveMoneySortBy.Unit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Units.UnitNo);
                        else query = query.OrderByDescending(o => o.Units.UnitNo);
                        break;
                    case MemoMoveMoneySortBy.ReceiveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Payments.ReceiveDate);
                        else query = query.OrderByDescending(o => o.Payments.ReceiveDate);
                        break;
                    case MemoMoveMoneySortBy.ReceiptNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ReceiptNo);
                        else query = query.OrderByDescending(o => o.ReceiptNo);
                        break;
                    case MemoMoveMoneySortBy.PayAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Payments.TotalAmount);
                        else query = query.OrderByDescending(o => o.Payments.TotalAmount);
                        break;
                    case MemoMoveMoneySortBy.Company:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Companys.NameTH);
                        else query = query.OrderByDescending(o => o.Companys.NameTH);
                        break;
                    case MemoMoveMoneySortBy.BankAccount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccounts.BankAccountNo);
                        else query = query.OrderByDescending(o => o.BankAccounts.BankAccountNo);
                        break;
                    case MemoMoveMoneySortBy.PaymentMethod:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PaymentMethodTypes.Name);
                        else query = query.OrderByDescending(o => o.PaymentMethodTypes.Name);
                        break;
                    case MemoMoveMoneySortBy.DestinationCompany:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.DestinationCompany.NameTH);
                        else query = query.OrderByDescending(o => o.DestinationCompany.NameTH);
                        break;
                    case MemoMoveMoneySortBy.PrintStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MemoMoveMoneys.IsPrint);
                        else query = query.OrderByDescending(o => o.MemoMoveMoneys.IsPrint);
                        break;
                    case MemoMoveMoneySortBy.PrintBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PrintBy.DisplayName);
                        else query = query.OrderByDescending(o => o.PrintBy.DisplayName);
                        break;
                    case MemoMoveMoneySortBy.PrintDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MemoMoveMoneys.PrintDate);
                        else query = query.OrderByDescending(o => o.MemoMoveMoneys.PrintDate);
                        break;
                    default:
                        query = query.OrderBy(o => o.Projects.ProjectNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.PaymentMethods.PaymentID);
            }

        }

        public void ToModel(ref MemoMoveMoney model, bool isPrint, Guid? userLogin)
        {
            if (this.PaymentFrom.Equals("UN"))
            {
                model.UnknownPaymentID = this.PaymentMethodId;
            }else
            {
                model.PaymentMethodID = this.PaymentMethodId;
            }
            model.SourceCompanyID = this.Company.Id;
            model.DestinationCompanyID = this.DestinationCompany.Id;
            model.MoveMoneyReasonMasterCenterID = this.MoveMoneyReason.Id;
            model.BankAccountID = this.BankAccount?.Id;
            model.Remark = this.Remark;
            model.IsPrint = isPrint;
            model.PrintByID = userLogin;
            model.PrintDate = this.PrintDate;
        }

        public static async Task ValidateAsync(DatabaseContext db, List<MemoMoveMoneyDTO> listdata)
        {
            ValidateException ex = new ValidateException();
            // Row Count > 1
            if (listdata.Count > 0)
            {
                MemoMoveMoneyDTO modelFirst = listdata[0]; 
                if (modelFirst.MoveMoneyReason == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                    string desc = modelFirst.GetType().GetProperty(nameof(MemoMoveMoneyDTO.MoveMoneyReason)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (modelFirst.DestinationCompany != null )
                {
                    var checkCompany = listdata.Where(x => x.Company.Id == x.DestinationCompany.Id).ToList();
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

    }
    public class MemoMoveMoneyQueryResult
    {
        public PaymentMethod PaymentMethods { get; set; }
         
        public String ReceiptNo { get; set; }
         
        public BankAccount BankAccounts { get; set; }

        public Company DestinationCompany { get; set; }

        public Company Companys { get; set; }

        public Payment Payments { get; set; }

        public Booking Bookings { get; set; }

        public Unit Units { get; set; }

        public Project Projects { get; set; }

        public MasterCenter PaymentMethodTypes { get; set; }
        public MasterCenter MoveMoneyReasons { get; set; }

        public Bank Banks { get; set; } 

        public MemoMoveMoney MemoMoveMoneys { get; set; }

        public User PrintBy { get; set; }
    }
}
