using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Budget.Services.Excels
{
    public class BudgetPromotionExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _unitNoIndex = 1;
        public const int _houseNoIndex = 2;
        public const int _houseTypeIndex = 3;
        public const int _wbsNoIndex = 4;
        public const int _promotionPriceIndex = 5;
        public const int _promotionTransferPriceIndex = 6;
        public const int _totalPriceIndex = 7;
        public const int _UnitStatus = 8;
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// เลขที่บ้าน  
        /// </summary>
        public string HouseNo { get; set; }
        /// <summary>
        /// ชนิดบ้าน
        /// </summary>
        public string HouseType { get; set; }
        /// <summary>
        /// WBS Number
        /// </summary>
        public string WBSNo { get; set; }
        /// <summary>
        /// โปรขาย
        /// </summary>
        public double PromotionPrice { get; set; }
        /// <summary>
        /// โปรโอน
        /// </summary>
        public double PromotionTransferPrice { get; set; }
        public double TotalPrice { get; set; }
        public static BudgetPromotionExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new BudgetPromotionExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
            result.HouseNo = dr[_houseNoIndex]?.ToString();         
            result.HouseType = dr[_houseTypeIndex]?.ToString();
            result.WBSNo = dr[_wbsNoIndex]?.ToString();

            double promotionPrice;
            if (double.TryParse(dr[_promotionPriceIndex]?.ToString(), out promotionPrice))
            {
                result.PromotionPrice = promotionPrice;
            }
            double promotionTransferPrice;
            if (double.TryParse(dr[_promotionTransferPriceIndex]?.ToString(), out promotionTransferPrice))
            {
                result.PromotionTransferPrice = promotionTransferPrice;
            }
            double totalPrice;
            if (double.TryParse(dr[_totalPriceIndex]?.ToString(), out totalPrice))
            {
                result.TotalPrice = totalPrice;
            }
            return result;
        }
        public void ToModelSale(ref BudgetPromotion model)
        {
            model.Budget = Convert.ToDecimal(this.PromotionPrice);
            model.ActiveDate = DateTime.Now;
            //model.BudgetPromotionType = BudgetPromotionType.Sale;
        }
        public void ToModelTransfer(ref BudgetPromotion model)
        {
            model.Budget = Convert.ToDecimal(this.PromotionTransferPrice);
            model.ActiveDate = DateTime.Now;
            //model.BudgetPromotionType = BudgetPromotionType.Transfer;
        }
    }
}
