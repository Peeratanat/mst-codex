using Database.Models;
using Database.Models.MST;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class BOConfigurationDTO : BaseDTO
    {
        /// <summary>
        /// ภาษีมูลค่าเพิ่ม (%)
        /// </summary>
        [Description("ภาษีมูลค่าเพิ่ม (%)")]
        public double? Vat { get; set; }
        /// <summary>
        /// มูลค่าที่ไม่คิดภาษี
        /// </summary>
        [Description("มูลค่าที่ไม่คิดภาษี")]
        public double? BOIAmount { get; set; }
        /// <summary>
        /// ภาษีเงินได้ (%)
        /// </summary>
        [Description("ภาษีเงินได้นิติบุคคล (%)")]
        public double? IncomeTaxPercent { get; set; }
        /// <summary>
        /// ภาษีธุรกิจเฉพาะ (%)
        /// </summary>
        [Description("ภาษีธุรกิจเฉพาะ (%)")]
        public double? BusinessTaxPercent { get; set; }
        /// <summary>
        /// ภาษีท้องถิ่น (%)
        /// </summary>
        [Description("ภาษีท้องถิ่น (%)")]
        public double? LocalTaxPercent { get; set; }
        /// <summary>
        /// เบี้ยปรับย้ายห้อง
        /// </summary>
        [Description("เบี้ยปรับย้ายห้อง")]
        public double? UnitTransferFee { get; set; }
        /// <summary>
        /// บัญชีเงินขาดเกิด
        /// </summary>
        [Description("บัญชีเงินขาดเกิด")]
        public double? AdjustAccount { get; set; }
        /// <summary>
        /// บัญชีภาษีขาย
        /// </summary>
        [Description("บัญชีภาษีขาย")]
        public double? TaxAccount { get; set; }
        /// <summary>
        /// อัตราค่าเสื่อมราคาต่อปี
        /// </summary>
        [Description("อัตราค่าเสื่อมราคาต่อปี")]
        public double? DepreciationYear { get; set; }
        /// <summary>
        /// ยกเลิกแบบคืนเงิน
        /// </summary>
        [Description("ยกเลิกแบบคืนเงิน")]
        public double? VoidRefund { get; set; }
        /// <summary>
        /// อัตราค่าธรรมเนียมโอน
        /// </summary>
        [Description("อัตราค่าธรรมเนียมโอน")]
        public double? TransferFeeRate { get; set; }

        public static BOConfigurationDTO CreateFromModel(BOConfiguration model)
        {
            if (model != null)
            {
                var result = new BOConfigurationDTO()
                {
                    Id = model.ID,
                    Vat = model.Vat,
                    BOIAmount = model.BOIAmount,
                    IncomeTaxPercent = model.IncomeTaxPercent,
                    BusinessTaxPercent = model.BusinessTaxPercent,
                    LocalTaxPercent = model.LocalTaxPercent,
                    UnitTransferFee = model.UnitTransferFee,
                    AdjustAccount = model.AdjustAccount,
                    TaxAccount = model.TaxAccount,
                    DepreciationYear = model.DepreciationYear,
                    VoidRefund = model.VoidRefund,
                    TransferFeeRate = model.TransferFeeRate,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (this.Vat == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.Vat)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }
        public async Task ValidateByFieldAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            if (this.Vat < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.Vat)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
            if (this.BOIAmount < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.BOIAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
            if (this.IncomeTaxPercent < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.IncomeTaxPercent)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
            if (this.BusinessTaxPercent < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.BusinessTaxPercent)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
            if (this.LocalTaxPercent < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.LocalTaxPercent)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
            if (this.UnitTransferFee < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.UnitTransferFee)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
            if (this.AdjustAccount < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.AdjustAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
            if (this.TaxAccount < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.TaxAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
            if (this.DepreciationYear < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.DepreciationYear)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
            if (this.VoidRefund < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.VoidRefund)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
            if (this.TransferFeeRate < 0.01)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0141").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.TransferFeeRate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                if (ex.HasError)
                    throw ex;
            }
        }

        public void ToModel(ref BOConfiguration model)
        {
            model.Vat = this.Vat.Value;
            model.BOIAmount = this.BOIAmount;
            model.IncomeTaxPercent = this.IncomeTaxPercent;
            model.BusinessTaxPercent = this.BusinessTaxPercent;
            model.LocalTaxPercent = this.LocalTaxPercent;
            model.UnitTransferFee = this.UnitTransferFee;
            model.AdjustAccount = this.AdjustAccount;
            model.TaxAccount = this.TaxAccount;
            model.DepreciationYear = this.DepreciationYear;
            model.VoidRefund = this.VoidRefund;
            model.TransferFeeRate = this.TransferFeeRate;
        }
    }
}
