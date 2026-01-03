using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.ACC;
using Database.Models.DbQueries.FIN;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    public class DepositDetailDTO : BaseDTO
    {
        /// <summary>
        /// สถานะเลือกรายการ
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// ข้อมูล DepositHeader
        /// </summary>
        public DepositHeaderDTO DepositHeader { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public ProjectDropdownDTO Project { get; set; }

        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// ข้อมูลใบเสร็จ
        /// </summary>
        public PaymentMethodDTO PaymentMethod { get; set; }

        /// <summary>
        /// ประเภทการชำระ
        /// </summary>
        public string PaymenyMethodItemText { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// จำนวนเงินที่นำฝาก
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Vat
        /// </summary>
        public decimal Vat { get; set; }

        /// <summary>
        /// สถานะนำฝาก
        /// null=All  
        /// 0=รอนำฝาก   
        /// 1=นำฝากแล้ว
        /// 2=เช็ครอนำฝาก
        /// </summary>
        public int? IsDeposit { get; set; }
        public decimal FeeIncludingVat { get; set; }
        

        public static void SortBy(DepositSortByParam sortByParam, ref string sortby)
        {
            #region Sort  
            sortby = nameof(dbqDepositSP.ReceiveDate);
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case DepositSortBy.Project:
                        sortby = nameof(dbqDepositSP.ProjectName);
                        break;
                    case DepositSortBy.Unit:
                        sortby = nameof(dbqDepositSP.UnitNo);
                        break;
                    case DepositSortBy.ReceiveDate:
                        sortby = nameof(dbqDepositSP.ReceiveDate);
                        break;
                    case DepositSortBy.ReceiptTempNo:
                        sortby = nameof(dbqDepositSP.ReceiptTempNo);
                        break;
                    case DepositSortBy.TotalAmount:
                        sortby = nameof(dbqDepositSP.Amount);
                        break;
                    case DepositSortBy.Fee:
                        sortby = nameof(dbqDepositSP.Fee);
                        break;
                    case DepositSortBy.Vat:
                        sortby = nameof(dbqDepositSP.Vat);
                        break;
                    case DepositSortBy.DepositStatus:
                        sortby = nameof(dbqDepositSP.IsDeposit);
                        break;
                    case DepositSortBy.PostStatus:
                        sortby = nameof(dbqDepositSP.IsPostPI);
                        break;
                    case DepositSortBy.PINumber:
                        sortby = nameof(dbqDepositSP.DepositNo);
                        break;
                    case DepositSortBy.FeeIncludingVat:
                        sortby = nameof(dbqDepositSP.FeeIncludingVat);
                        break;
                    case DepositSortBy.Number:
                        sortby = nameof(dbqDepositSP.Number);
                        break;
                    default:
                        sortby = nameof(dbqDepositSP.ReceiveDate);
                        break;
                }
            }

            #endregion
        }

        public static DepositDetailDTO CreateFromQueryResult(DepositDetailQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                DepositDetailDTO result = new DepositDetailDTO()
                {
                    IsSelected = false,
                    DepositHeader = DepositHeaderDTO.CreateFromDepositDetailQueryResult(model),
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    IsDeposit = model.IsDeposit,
                    PaymentMethod = new PaymentMethodDTO
                    {
                        Id = model.PaymentMethod.ID,
                        PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(model.PaymentMethodType),
                        PayAmount = model.PaymentMethod.PayAmount,
                        //IsChequeConfirm = model.PaymentMethod.IsChequeConfirm
                    },

                    PayAmount = model.Payment.TotalAmount,
                    ReceiptTempNo = model.ReceiptTempHeader.ReceiptTempNo,
                    ReceiveDate = model.ReceiptTempHeader.ReceiveDate
                };

                var PaymentItemText = "";

                var PaymentItemModel = DB.PaymentItems.Where(o => o.PaymentID == model.Payment.ID)
                        .Include(o => o.MasterPriceItem).ToList() ?? new List<PaymentItem>();

                if (PaymentItemModel.Any())
                {
                    foreach (var item in PaymentItemModel)
                    {
                        PaymentItemText += "," + item.MasterPriceItem.Detail;
                    }
                }

                PaymentItemText = (PaymentItemText.Length > 2) ? PaymentItemText.Substring(1, PaymentItemText.Length - 1) : PaymentItemText;

                result.PaymenyMethodItemText = PaymentItemText;

                result.Fee = model.PaymentMethod.Fee ?? 0;
                result.Vat = model.PaymentMethod.FeeIncludingVat ?? 0;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static DepositDetailDTO CreateFromQuerySPResult(dbqDepositSP model, DatabaseContext DB)
        {
            if (model != null)
            {
                DepositDetailDTO result = new DepositDetailDTO()
                {
                    IsSelected = false,
                    PayAmount = model.Amount,
                    ReceiptTempNo = model.ReceiptTempNo,
                    ReceiveDate = model.ReceiveDate,
                    IsDeposit = model.IsDeposit,
                };

                result.DepositHeader = new DepositHeaderDTO();

                result.DepositHeader.Id = model.DepositID;
                result.DepositHeader.DepositNo = model.DepositNo;
                result.DepositHeader.DepositDate = model.DepositDate;
                result.DepositHeader.Company = new CompanyDTO();
                result.DepositHeader.Company.Id = model.CompanyID;
                result.DepositHeader.Company.NameTH = model.CompanyName;
                result.DepositHeader.BankAccount = new BankAccountDropdownDTO();
                result.DepositHeader.BankAccount.Id = model.BankAccountID;
                result.DepositHeader.BankAccount.DisplayName = model.BankAccountNo;
                result.DepositHeader.BankAccount.Bank = new BankDropdownDTO();
                result.DepositHeader.BankAccount.Bank.Id = model.BankID ?? new Guid();
                result.DepositHeader.BankAccount.Bank.NameTH = model.BankName;
                result.DepositHeader.DepositStatus = string.IsNullOrEmpty(model.DepositNo) ? false : true;
                result.DepositHeader.Remark = model.DepositRemark;
                result.DepositHeader.IsPostGL = model.IsPostPI;
                result.Project = new ProjectDropdownDTO();
                result.Project.Id = model.ProjectID;
                result.Project.ProjectNameTH = model.ProjectName;
                result.Unit = new UnitDropdownDTO();
                result.Unit.Id = model.UnitID ?? new Guid();
                result.Unit.UnitNo = model.UnitNo;

                result.PaymentMethod = new PaymentMethodDTO();
                result.PaymentMethod.Id = model.PaymentMethodID;
                result.PaymentMethod.IsChequeConfirm = model.IsChequeConfirm;
                result.PaymentMethod.PayAmount = model.Amount;
                result.PaymentMethod.PaymentMethodType = new MasterCenterDropdownDTO();
                result.PaymentMethod.PaymentMethodType.Id = model.PaymentMethodTypeID ?? new Guid();
                result.PaymentMethod.PaymentMethodType.Name = model.PaymentMethodTypeName;
                result.PaymentMethod.PaymentMethodType.Key = model.PaymentMethodTypeKey;
                result.PaymentMethod.Number = model.Number;
                result.PaymentMethod.IsFeeConfirm = model.IsFeeConfirm; 
                result.PaymenyMethodItemText = model.PaymentItemName;

                result.Fee = model.Fee;
                result.Vat = model.Vat;
                result.FeeIncludingVat = model.FeeIncludingVat;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task ValidateAsync(DatabaseContext DB, List<DepositDetailDTO> Model)
        {
            ValidateException ex = new ValidateException();

            var PaymentMethodType = Model.Select(o => o.PaymentMethod.PaymentMethodType).GroupBy(o => o.Id).ToList() ?? new List<IGrouping<Guid, MasterCenterDropdownDTO>>();

            var Company = Model.GroupBy(o => o.DepositHeader.Company.Id).Select(o => o.Key).ToList() ?? new List<Guid?>();
            var BankAccount = Model.GroupBy(o => o.DepositHeader.BankAccount.Id).Select(o => o.Key).ToList() ?? new List<Guid?>();

            await DB.CheckCalendarThrowErrorAsync(Company.FirstOrDefault() ?? Guid.Empty, Model.FirstOrDefault()?.DepositHeader.DepositDate ?? DateTime.Now);

            if (PaymentMethodType.Count > 1)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0090").FirstAsync();
                string desc = Model.FirstOrDefault().PaymentMethod.GetType().GetProperty(nameof(PaymentMethodDTO.PaymentMethodType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            //else
            //{
            //    //เพ่ิมเงื่อนไข กรณีเป็นเช็ค ต้องเป็นสถานะ เช็ครอนำฝากเท่านั้นถึงจะนำฝากได้
            //    var paymentType = PaymentMethodType?.FirstOrDefault().FirstOrDefault();
            //    if (paymentType != null)
            //    {
            //        if (PaymentMethodKeys.PersonalCheque.Equals(paymentType.Key)
            //            || PaymentMethodKeys.PostDateCheque.Equals(paymentType.Key)
            //            || PaymentMethodKeys.CashierCheque.Equals(paymentType.Key)
            //            )
            //        {
            //            //var IschequeFalse = Model.Where(x => x.PaymentMethod.IsChequeConfirm ?? false == false).ToList();
            //            var IschequeFalse = Model.Where(x => !(x.PaymentMethod.IsChequeConfirm ?? false)).ToList();
            //            if (IschequeFalse.Any())
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0117").FirstAsync();
            //                var msg = errMsg.Message;
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }
            //    }
            //}

            if (Company.Count > 1)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0090").FirstAsync();
                string desc = Model.FirstOrDefault().DepositHeader.GetType().GetProperty(nameof(Database.Models.MST.Company)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (BankAccount.Count > 1)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0090").FirstAsync();
                string desc = Model.FirstOrDefault().DepositHeader.GetType().GetProperty(nameof(Database.Models.MST.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            //กรณีชำระแบบ เดบิต เครดิต เช็คเงินสด เช็คบุคคล ต้องตรวจสอบสถานะ isfeecomfirm == true 
            var chkFeeComfirm = Model.Where(x => (PaymentMethodKeys.PersonalCheque.Equals(x.PaymentMethod.PaymentMethodType.Key)
            || PaymentMethodKeys.CashierCheque.Equals(x.PaymentMethod.PaymentMethodType.Key)
            || PaymentMethodKeys.CreditCard.Equals(x.PaymentMethod.PaymentMethodType.Key)
            || PaymentMethodKeys.DebitCard.Equals(x.PaymentMethod.PaymentMethodType.Key))
            && (x.PaymentMethod.IsFeeConfirm ?? false) == false).ToList();
            if (chkFeeComfirm.Count > 0)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0120").FirstAsync(); 
                var msg = "โปรดตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก";
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public static async Task ValidateChequeConfirmAsync(DatabaseContext DB, List<DepositDetailDTO> Model)
        {
            ValidateException ex = new ValidateException();

            var PaymentMethodType = Model.Select(o => o.PaymentMethod.PaymentMethodType).GroupBy(o => o.Id).ToList() ?? new List<IGrouping<Guid, MasterCenterDropdownDTO>>();

            if (PaymentMethodType.Count > 1)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0090").FirstAsync();
                string desc = Model.FirstOrDefault().PaymentMethod.GetType().GetProperty(nameof(PaymentMethodDTO.PaymentMethodType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                var paymentType = PaymentMethodType.FirstOrDefault();
                if (paymentType != null)
                {
                    if (PaymentMethodKeys.PersonalCheque.Equals(paymentType.Key)
                        & PaymentMethodKeys.PostDateCheque.Equals(paymentType.Key)
                        & PaymentMethodKeys.CashierCheque.Equals(paymentType.Key)
                        )
                    {
                        var IschequeFalse = Model.Where(x => x.PaymentMethod.IsChequeConfirm == false).FirstOrDefault();
                        if (IschequeFalse != null)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0118").FirstAsync();
                            var msg = errMsg.Message;
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

    }

    public class DepositDetailQueryResult
    {
        public MasterCenter PaymentMethodType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Payment Payment { get; set; }
        public Booking Booking { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public Company Company { get; set; }
        public ReceiptTempHeader ReceiptTempHeader { get; set; }
        public DepositHeader DepositHeader { get; set; }
        public DepositDetail DepositDetail { get; set; }
        public BankAccount BankAccount { get; set; }
        public PostGLHeader PostGLHeader { get; set; }
        public int? IsDeposit { get; set; }

    }

    public class PaymentMethodToItemQueryResult
    {
        public Payment Payment { get; set; }
        public PaymentItem PaymentItem { get; set; }
        public MasterPriceItem MasterPriceItem { get; set; }
    }

    public class paymentTmpResult : BaseDTO
    {
        public Payment Payment { get; set; }
        public string DepositNo { get; set; }
        public DateTime? DepositDate { get; set; }
    }
}