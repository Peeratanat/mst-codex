using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
    public class MinPriceExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _wbsCodeIndex = 1;
        public const int _unitNoIndex = 2;
        public const int _minimumPrice = 3;
        public const int _minimumPriceType = 4; 
        public const int _unitStatus = 5;
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// WBS Code
        /// </summary>
        public string WBSCode { get; set; }
        /// <summary>
        /// เลขที่แปลง  
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// MinimumPrice
        /// </summary>
        public double MinimumPrice { get; set; }
        /// <summary>
        /// MinimumPriceType
        /// </summary>
        public string MinimumPriceType { get; set; }
        public static MinPriceExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new MinPriceExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.WBSCode = dr[_wbsCodeIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
            result.MinimumPriceType = dr[_minimumPriceType]?.ToString();
            double minimumPrice;
            if (double.TryParse(dr[_minimumPrice]?.ToString(), out minimumPrice))
            {
                result.MinimumPrice = minimumPrice;
            }
            return result;
        }
    }
}
