using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class PriceListInstallmentItemDTO : BaseDTO
    {
        public int Period { get; set; }
        /// <summary>
        /// งวดพิเศษ
        /// </summary>
        public bool IsSpecial { get; set; }
        /// <summary>
        /// ราคารวม
        /// </summary>
        public decimal Amount { get; set; }

        //public static PriceListInstallmentItemDTO CreateFromModel(PriceListInstallmentItem model)
        //{
        //    if (model != null)
        //    {
        //        var result = new PriceListInstallmentItemDTO()
        //        {
        //            Id=model.ID,
        //            Period = model.Period,
        //            IsSpecial = model.IsSpecial,
        //            Amount = model.Amount,
        //        };

        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
