using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Database.Models.CTM;
using System.Collections.Generic;
using Database.Models.USR;
using Database.Models.DbQueries.FIN;
using Database.Models.MasterKeys;

namespace Base.DTOs.FIN
{
    public class FeeChequeDTO : BaseDTO
    {
        /// <summary>
        /// บริษัท
        /// </summary>
        public MST.CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// สถานะตรวจสอบ
        /// </summary>
        public bool FeeConfirmStatus { get; set; }

        /// <summary>
        /// เลขที่นำฝาก
        /// </summary>
        public string DepositNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// ธนาคาร  
        /// </summary>
        [Description("ธนาคาร")]
        public MST.BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// ประเภทบัตร (Visa, Master, JCB)
        /// </summary>
        //public MST.MasterCenterDropdownDTO CreditCardTypeMasterCenterID { get; set; }

        /// <summary>
        /// เช็คส่วนตัว,แคชเชียร์เช็ค
        /// </summary>
        [Description("เช็คส่วนตัว,แคชเชียร์เช็ค")]
        public MST.MasterCenterDropdownDTO ReceiveType { get; set; }

        /// <summary>
        /// เลขที่บัตร
        /// </summary>]
        [Description("หมายเลขเช็ค")]
        public string ChequeNo { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงินชั่วคราว
        /// </summary>
        public string ReceiveNo { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// % ค่าธรรมเนียม
        /// </summary>
        [Description("เปอร์เซ็นต์ค่าธรรมเนียม")]
        public decimal? FeePercent { get; set; }

        /// <summary>
        /// มูลค่าธรรมเนียม
        /// </summary>
        [Description("ค่าธรรมเนียม")]
        public decimal? FeeAmount { get; set; }

        /// <summary>
        /// มูลค่า Vat
        /// </summary>
        //public double? Vat { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal? PayAmount { get; set; }

        /// <summary>
        /// มูลค่าสุทธิ
        /// </summary>
        [Description("มูลค่าสุทธิ")]
        public decimal? FeeIncludingVat { get; set; }

        /// <summary>
        /// สถานะนำฝาก
        /// </summary>
        [Description("สถานะนำฝาก")]
        public bool DepositStatus { get; set; }

        /// <summary>
        /// Key Type + ID
        /// </summary>
       // public string KeyID { get; set; }


        public PostPIFeeDTO PostPI { get; set; }
        public class FFeeChequeQueryResult
        {
            //public PaymentCreditCard PaymentCreditCard { get; set; }
            //public PaymentDebitCard PaymentDebitCard { get; set; }
            public Company Company { get; set; }
            public Project Project { get; set; }
            public Unit Unit { get; set; }

            [Description("ประเภทของเช็ค")]
            public MasterCenter ChequeType { get; set; }
            public ReceiptTempHeader ReceiptHeader { get; set; }

            //[Description("ผูกช่องทางการชำระเงิน")]
            //public PaymentMethod PaymentMethod { get; set; }
            [Description("วันที่หน้าเช็ค")]
            public DateTime? ChequeDate { get; set; }
            [Description("เลขที่เช็ค")]
            public string ChequeNo { get; set; }
            [Description("เปอร์เซ็นต์ธรรมเนียม")]
            public decimal? FeePercent { get; set; }
            public decimal? FeeIncludingVat { get; set; }
            [Description("ค่าธรรมเนียม")]
            public decimal? Fee { get; set; }
            [Description("สั่งจ่ายให้บริษัท")]
            public Company PayToCompany { get; set; }
            [Description("สั่งจ่ายผิดบริษัท")]
            public bool? IsWrongCompany { get; set; }
            [Description("ธนาคาร")]
            public Bank Bank { get; set; }
            [Description("สาขาธนาคาร")]
            public string BankBranch { get; set; }
            //สถานะตรวจสอบค่าธรรมเนียม
            [Description("สถานะตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
            public bool? IsFeeConfirm { get; set; }
            [Description("วันที่ ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
            public DateTime? FeeConfirmDate { get; set; }
            [Description("ผู้ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
            public User FeeConfirmByUser { get; set; }

            public string DepositNo { get; set; }
            public bool DepositStatus { get; set; }
            public Guid ID { get; set; }
            public DateTime? Updated { get; set; }
            // public User UpdatedBy { get; set; }
            public User UpdatedBy { get; set; }
            public string PostPI { get; set; }
            public bool PostPIStatus { get; set; }

            [Description("จำนวนเงินที่จ่าย")]
            public decimal PayAmount { get; set; }
        }

        public static void SortBy(FeeChequeSortByParam sortByParam, ref IQueryable<FFeeChequeQueryResult> query)
        {

            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case FeeChequeSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNameTH);
                        else query = query.OrderByDescending(o => o.Project.ProjectNameTH);
                        break;
                    case FeeChequeSortBy.ReceiveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ReceiptHeader.ReceiveDate);
                        else query = query.OrderByDescending(o => o.ReceiptHeader.ReceiveDate);
                        break;
                    //case FeeChequeSortBy.EDC:
                    //    if (sortByParam.Ascending) query = query.OrderBy(o => o.EDC.Bank.NameTH);
                    //    else query = query.OrderByDescending(o => o.EDC.Bank.NameTH);
                    //    break;
                    case FeeChequeSortBy.ChequeType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ChequeType);
                        else query = query.OrderByDescending(o => o.ChequeType);
                        break;
                    case FeeChequeSortBy.ChequeNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ChequeNo);
                        else query = query.OrderByDescending(o => o.ChequeNo);
                        break;
                    case FeeChequeSortBy.RecevieNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ReceiptHeader.ReceiptTempNo);
                        else query = query.OrderByDescending(o => o.ReceiptHeader.ReceiptTempNo);
                        break;
                    case FeeChequeSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    //case FeeChequeSortBy.FeePercent:
                    //    if (sortByParam.Ascending) query = query.OrderBy(o => o.FeePercent);
                    //    else query = query.OrderByDescending(o => o.FeePercent);
                    //    break;
                    case FeeChequeSortBy.FeeAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Fee);
                        else query = query.OrderByDescending(o => o.Fee);
                        break;
                    case FeeChequeSortBy.PayAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PayAmount);
                        else query = query.OrderByDescending(o => o.PayAmount);
                        break;
                    //case FeeChequeSortBy.NetAmount:
                    //    if (sortByParam.Ascending) query = query.OrderBy(o => o.FeeIncludingVat);
                    //    else query = query.OrderByDescending(o => o.FeeIncludingVat);
                    //    break;
                    case FeeChequeSortBy.UpdatedDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Updated);
                        else query = query.OrderByDescending(o => o.Updated);
                        break;
                    case FeeChequeSortBy.UpdatedByName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case FeeChequeSortBy.DepositStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.DepositStatus);
                        else query = query.OrderByDescending(o => o.DepositStatus);
                        break;
                    case FeeChequeSortBy.DepositNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.DepositNo);
                        else query = query.OrderByDescending(o => o.DepositNo);
                        break;
                    default:
                        query = query.OrderBy(o => o.ReceiptHeader.ReceiveDate).ThenBy(x => x.ReceiptHeader.ReceiptTempNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.ReceiptHeader.ReceiveDate).ThenBy(x => x.ReceiptHeader.ReceiptTempNo);

            }
        }

        public async Task ValidateAsync(DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();

            //var PaymentMethod = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod).ToList();
            //var PaymentCashierCheque = PaymentMethod.Where(x => x.Key == PaymentMethodKeys.CashierCheque).FirstOrDefault();
            //var PaymentPersonalCheque = PaymentMethod.Where(x => x.Key == PaymentMethodKeys.PersonalCheque).FirstOrDefault();

            //if (this.ReceiveType == null)
            //{
            //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(FeeChequeDTO.ReceiveType)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //else if (this.ReceiveType.Id == PaymentCashierCheque.ID)
            //{
            //    var masterCenterModel = DB.MasterCenters.Where(o => o.ID == this.ReceiveType.Id).ToList() ?? new List<MasterCenter>();
            //    if (!masterCenterModel.Any())
            //    {
            //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(FeeChequeDTO.ReceiveType)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            //else if (this.ReceiveType.Id == PaymentPersonalCheque.ID)
            //{
            //    var masterCenterModel = DB.MasterCenters.Where(o => o.ID == this.ReceiveType.Id).ToList() ?? new List<MasterCenter>();
            //    if (!masterCenterModel.Any())
            //    {
            //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(FeeChequeDTO.ReceiveType)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            //else
            //{
            //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(FeeChequeDTO.ReceiveType)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            var bank = DB.Banks.Where(o => o.ID == this.Bank.Id).ToList() ?? new List<Bank>();
            if (!bank.Any())
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(FeeChequeDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.ChequeNo == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(FeeChequeDTO.ChequeNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.FeeAmount == null || this.FeeAmount < 0)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(FeeChequeDTO.FeeAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc );
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.FeeIncludingVat == null || this.FeeIncludingVat < 0)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(FeeChequeDTO.FeeIncludingVat)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.FeePercent == null || this.FeePercent < 0)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(FeeChequeDTO.FeePercent)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc  );
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (this.DepositStatus == true)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(FeeChequeDTO.DepositStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
             
            //await DB.CheckCalendarThrowErrorAsync(this.Company.Id  , this.ReceiveDate ?? DateTime.Now ,2 );
             
            if (ex.HasError)
            {
                throw ex;
            }
        }


        public static FeeChequeDTO CreateFromModel(FFeeChequeQueryResult model)
        {
            if (model != null)
            {
                FeeChequeDTO result = new FeeChequeDTO()
                {
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    ReceiveDate = model.ReceiptHeader.ReceiveDate,
                    ReceiveType = MasterCenterDropdownDTO.CreateFromModel(model.ChequeType),
                    ChequeNo = model.ChequeNo ?? null,
                    UnitNo = model.Unit.UnitNo ?? null,
                    FeePercent = model.FeePercent,
                    FeeIncludingVat = model.FeeIncludingVat,
                    FeeAmount = model.Fee,
                    PayAmount = model.PayAmount,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy != null ? model.UpdatedBy.DisplayName : null,
                    DepositStatus = model.DepositStatus,
                    DepositNo = model.DepositNo,
                    FeeConfirmStatus = model.IsFeeConfirm ?? false,
                    ReceiveNo = model.ReceiptHeader.ReceiptTempNo,
                    Id = model.ID,
                   // KeyID = model.ChequeType.Key + "-" + model.ID.ToString(),
                    PostPI = new PostPIFeeDTO(),
                };

                if (model.PostPIStatus)
                {
                    result.PostPI.PostPIStatus = true;
                    result.PostPI.PostPINo = model.PostPI;
                }

                if (model.ReceiptHeader.ReceiptTempNo != null)
                    result.ReceiveDate = model.ReceiptHeader.ReceiveDate;
                else
                    result.ReceiveDate = null;
                return result;
            }
            else
            {
                return null;
            }
        }


        public static FeeChequeDTO CreateFromModelSP(dbqFeeChequeSP model)
        {
            if (model != null)
            {
                FeeChequeDTO result = new FeeChequeDTO();
                result.Id = model.ID;
                result.Bank = new BankDropdownDTO();
                result.Bank.Id = model.BankID ?? Guid.Empty;
                result.Bank.NameTH = model.BankName;
                result.Company = new CompanyDropdownDTO();
                result.Company.Id = model.CompanyID ?? Guid.Empty;
                result.Company.NameTH = model.CompanyName;
                result.Project = new ProjectDropdownDTO();
                result.Project.Id = model.ProjectID ?? Guid.Empty;
                result.Project.ProjectNameTH = model.ProjectName;
                result.ReceiveDate = model.ReceiveDate;
                result.ReceiveType = new MasterCenterDropdownDTO(); 
                result.ReceiveType.Id = Guid.Empty;
                result.ReceiveType.Name = model.ChequeTypeName;
                result.ChequeNo = model.ChequeNumber;
                result.UnitNo = model.UnitNo;
                result.FeePercent = model.FeePercent;
                result.FeeIncludingVat = model.NetAmount;
                result.FeeAmount = model.Fee;
                result.PayAmount = model.Amount;
                result.Updated = model.UpdatedDate;
                result.UpdatedBy = model.UpdatedBy;
                result.DepositStatus = model.DepositStatus ?? false;
                result.DepositNo = model.DepositNo;
                result.FeeConfirmStatus = model.IsFeeConfirm ?? false;
                result.ReceiveNo = model.ReceiptTempNo;
                result.PostPI = new PostPIFeeDTO();
                result.PostPI.PostPIStatus = model.IsPostPI ?? false;
                result.PostPI.PostPINo = model.PINumber;
                
                //if (model.PostPIStatus)
                //{
                //    result.PostPI.PostPIStatus = true;
                //    result.PostPI.PostPINo = model.PostPI;
                //}

                //if (model.ReceiptHeader.ReceiptTempNo != null)
                //    result.ReceiveDate = model.ReceiptHeader.ReceiveDate;
                //else
                //    result.ReceiveDate = null;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
