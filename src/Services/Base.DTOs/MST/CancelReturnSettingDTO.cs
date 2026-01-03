using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Base.DTOs.MST
{
    public class CancelReturnSettingDTO : BaseDTO
    {
        /// <summary>
        /// Chief อนุมัติเมื่อคืนเงินน้อยกว่า (%)
        /// </summary>
        [Description("Chief อนุมัติเมื่อคืนเงินน้อยกว่า (%)")]
        public double ChiefReturnLessThanPercent { get; set; }


        /// <summary>
        /// หักค่าดำเนินการ (บาท)
        /// </summary>
        [Description("หักค่าดำเนินการ (บาท)")]
        public decimal HandlingFee { get; set; }

        public static CancelReturnSettingDTO CreateFromModel(CancelReturnSetting model)
        {
            if (model != null)
            {
                var result = new CancelReturnSettingDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    ChiefReturnLessThanPercent = model.ChiefReturnLessThanPercent,
                    HandlingFee = model.HandlingFee,
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public void ToModel(ref CancelReturnSetting model)
        {
            model.ChiefReturnLessThanPercent = this.ChiefReturnLessThanPercent;
            model.HandlingFee = this.HandlingFee;
        }
    }
}
