using Database.Models;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRM;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    /// <summary>
    /// ค่าธรรมเนียมเครื่องรูดบัตร
    /// Model = EDCFee
    /// </summary>
    public class EDCFeeDTO : BaseDTO
    {
        /// <summary>
        /// เครื่องรูดบัตรธนาคาร
        /// Master/api/Banks/DropdownList
        /// </summary>
        [Description("เครื่องรูดบัตรธนาคาร")]
        public MST.BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// ชนิดบัตร
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PaymentCardType
        /// </summary>
        [Description("ชนิดบัตร")]
        public MST.MasterCenterDropdownDTO PaymentCardType { get; set; }
        /// <summary>
        /// ประเภทบัตร
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditCardType
        /// </summary>
        [Description("ประเภทบัตร")]
        public MST.MasterCenterDropdownDTO CreditCardType { get; set; }
        /// <summary>
        /// บัตรที่รูด (true = แสดงชื่อธนาคาร)
        /// </summary>
        [Description("บัตรที่รูด")]
        public bool IsEDCBankCreditCard { get; set; }
        /// <summary>
        /// รูปแบบการรูด
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditCardPaymentType
        /// </summary>
        [Description("รูปแบบการรูด")]
        public MST.MasterCenterDropdownDTO CreditCardPaymentType { get; set; }
        /// <summary>
        /// ค่าธรรมเนียม (%)
        /// </summary>
        [Description("ค่าธรรมเนียม (%)")]
        public double Fee { get; set; }
        /// <summary>
        /// ชื่อโปรโมชั่น (บัตรเครดิต)
        /// </summary>
        public string CreditCardPromotionName { get; set; }

        public static EDCFeeDTO CreateFromModel(EDCFee model)
        {
            if (model != null)
            {
                var result = new EDCFeeDTO()
                {
                    Id = model.ID,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    PaymentCardType = MasterCenterDropdownDTO.CreateFromModel(model.PaymentCardType),
                    CreditCardType = MasterCenterDropdownDTO.CreateFromModel(model.CreditCardType),
                    IsEDCBankCreditCard = model.IsEDCBankCreditCard,
                    CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(model.CreditCardPaymentType),
                    Fee = model.Fee,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                result.CreditCardPromotionName = $"{result.CreditCardPaymentType?.Name} {result.CreditCardType?.Name}";
                if (result.IsEDCBankCreditCard)
                {
                    result.CreditCardPromotionName = $"{result.Bank?.Alias} " + result.CreditCardPromotionName;
                }
                return result;
            }
            else
            {
                return null;
            }
        }
        public static EDCFeeDTO CreateFromQueryResult(EDCFeeQueryResult model)
        {
            if (model != null)
            {
                var result = new EDCFeeDTO()
                {
                    Id = model.EDCFee.ID,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    PaymentCardType = MasterCenterDropdownDTO.CreateFromModel(model.PaymentCardType),
                    CreditCardType = MasterCenterDropdownDTO.CreateFromModel(model.CreditCardType),
                    IsEDCBankCreditCard = model.EDCFee.IsEDCBankCreditCard,
                    CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(model.CreditCardPaymentType),
                    Fee = model.EDCFee.Fee,
                    Updated = model.EDCFee.Updated,
                    UpdatedBy = model.EDCFee.UpdatedBy?.DisplayName
                };
                result.CreditCardPromotionName = $"{result.CreditCardPaymentType?.Name} {result.CreditCardType?.Name}";
                if (result.IsEDCBankCreditCard)
                {
                    result.CreditCardPromotionName = $"{result.Bank?.Alias} " + result.CreditCardPromotionName;
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(EDCFeeSortByParam sortByParam, ref List<EDCFeeDTO> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case EDCFeeSortBy.PaymentCardType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PaymentCardType?.Name)?.ToList();
                        else query = query.OrderByDescending(o => o.PaymentCardType?.Name)?.ToList();
                        break;
                    case EDCFeeSortBy.CreditCardType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CreditCardType?.Name)?.ToList();
                        else query = query.OrderByDescending(o => o.CreditCardType?.Name)?.ToList();
                        break;
                    case EDCFeeSortBy.CreditCardPaymentType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CreditCardPaymentType?.Name)?.ToList();
                        else query = query.OrderByDescending(o => o.CreditCardPaymentType?.Name)?.ToList();
                        break;
                    case EDCFeeSortBy.Fee:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Fee)?.ToList();
                        else query = query.OrderByDescending(o => o.Fee)?.ToList();
                        break;
                    case EDCFeeSortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank?.NameTH)?.ToList();
                        else query = query.OrderByDescending(o => o.Bank?.NameTH)?.ToList();
                        break;
                    case EDCFeeSortBy.CreditCardPromotionName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CreditCardPromotionName)?.ToList();
                        else query = query.OrderByDescending(o => o.CreditCardPromotionName)?.ToList();
                        break;
                    case EDCFeeSortBy.IsEDCBankCreditCard:
                        if (sortByParam.Ascending) query = query.OrderByDescending(o => o.IsEDCBankCreditCard)?.ToList();
                        else query = query.OrderBy(o => o.IsEDCBankCreditCard)?.ToList();
                        break;
                    default:
                        query = query.OrderBy(o => o.PaymentCardType?.Name)?.ToList();
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.PaymentCardType?.Name)?.ToList();
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            //if (this.Bank == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(EDCFeeDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //// ชนิดบัตร
            //if (this.PaymentCardType == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(EDCFeeDTO.PaymentCardType)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //// ประเภทบัตร
            //if (this.CreditCardType == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(EDCFeeDTO.CreditCardType)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //// รูปแบบการรูด
            //if (this.CreditCardPaymentType == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(EDCFeeDTO.CreditCardPaymentType)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //// ค่าธรรมเนียม (%)
            //if (this.Fee == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(EDCFeeDTO.Fee)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            //unique 4 fields
            bool isExisted = false;

            if (CreditCardPaymentType != null && !CreditCardPaymentTypeKeys.Free.Equals(this.CreditCardPaymentType.Key))
            {
                isExisted = await db.EDCFees.Where(o =>
                 o.CreditCardPaymentTypeMasterCenterID == this.CreditCardPaymentType.Id &&
                 (!CreditCardPaymentTypeKeys.Free.Equals(this.CreditCardPaymentType.Key) && o.CreditCardTypeMasterCenterID == this.CreditCardType.Id)
                 &&
                 o.PaymentCardTypeMasterCenterID == this.PaymentCardType.Id &&
                 o.IsEDCBankCreditCard == this.IsEDCBankCreditCard &&
                 //o.Fee == this.Fee &&
                 o.BankID == this.Bank.Id &&
                 o.ID != this.Id).AnyAsync();
            }else
            {
                var CreditCardPaymentTypeKeyFree = await db.MasterCenters.GetIDAsync(MasterCenterGroupKeys.CreditCardPaymentType, CreditCardPaymentTypeKeys.Free);
                isExisted = await db.EDCFees.Where(o =>
                 o.CreditCardPaymentTypeMasterCenterID == this.CreditCardPaymentType.Id
                 &&
                 o.PaymentCardTypeMasterCenterID == this.PaymentCardType.Id &&
                 o.IsEDCBankCreditCard == this.IsEDCBankCreditCard &&
                 o.BankID == this.Bank.Id &&
                 o.CreditCardPaymentTypeMasterCenterID == CreditCardPaymentTypeKeyFree  &&
                 o.ID != this.Id).AnyAsync();
            }
            if (isExisted)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                var msg = errMsg.Message.Replace("[field]", "ค่าธรรมเนียม");
                msg = msg.Replace("[value]", "ค่าธรรมเนียม");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref EDCFee model)
        {
            model.BankID = this.Bank?.Id;
            model.PaymentCardTypeMasterCenterID = this.PaymentCardType?.Id;
            if (!CreditCardPaymentTypeKeys.Free.Equals(this.CreditCardPaymentType.Key))
            {
                model.CreditCardTypeMasterCenterID = this.CreditCardType?.Id;
            }
            model.IsEDCBankCreditCard = this.IsEDCBankCreditCard;
            model.CreditCardPaymentTypeMasterCenterID = this.CreditCardPaymentType?.Id;
            model.Fee = this.Fee;
        }
        public void ToMasterSalePromotionCreditCardItemModel(ref MasterSalePromotionCreditCardItem model)
        {
            model.BankID = this.Bank?.Id;
            model.EDCFeeID = this.Id;
            model.Quantity = 1;
            model.UnitTH = "บาท";
            model.UnitEN = "Baht";
            model.Fee = this.Fee;
            model.NameTH = this.Bank?.Alias + " " + this.CreditCardPaymentType?.Name + " " + this.CreditCardType?.Name;
            model.NameEN = this.Bank?.Alias + " " + this.CreditCardPaymentType?.Name + " " + this.CreditCardType?.Name;
        }
        public void ToMasterTransferCreditCardItemModel(ref MasterTransferPromotionCreditCardItem model)
        {
            model.BankID = this.Bank?.Id;
            model.EDCFeeID = this.Id;
            model.Quantity = 1;
            model.UnitTH = "บาท";
            model.UnitEN = "Baht";
            model.Fee = this.Fee;
            model.NameTH = this.Bank?.Alias + " " + this.CreditCardPaymentType?.Name + " " + this.CreditCardType?.Name;
            model.NameEN = this.Bank?.Alias + " " + this.CreditCardPaymentType?.Name + " " + this.CreditCardType?.Name;
        }


    }

    public class EDCFeeQueryResult
    {
        public EDCFee EDCFee { get; set; }
        public Bank Bank { get; set; }
        public MasterCenter PaymentCardType { get; set; }
        public MasterCenter CreditCardType { get; set; }
        public MasterCenter CreditCardPaymentType { get; set; }
    }
}
