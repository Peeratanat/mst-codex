using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
    public class MinPriceSAPExcelModel
    {
        public const int _projectCodeIndex = 0;
        public const int _wbsCodeIndex = 1;
        public const int _objectCodeIndex = 2;
        public const int _companyCodeIndex = 3;
        public const int _boqStyleIndex = 4; 
        public const int _homeTyleIndex = 5;
        public const int _wbsStatusIndex = 6;
        public const int _minimumPriceIndex = 7;
        public const int _saleAreaIndex = 8;

        public string ProjectCode { get; set; }
        public string WBSCode { get; set; }
        public string ObjectCode { get; set; }
        public string CompanyCode { get; set; }
        public string BoqStyle { get; set; }
        public string HomeStyle { get; set; }
        public string WbsStatus { get; set; }
        public decimal? MinimumPrice { get; set; }
        public double? SaleArea { get; set; }

        public static MinPriceSAPExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new MinPriceSAPExcelModel();
            result.ProjectCode = dr[_projectCodeIndex]?.ToString();
            result.WBSCode = dr[_wbsCodeIndex]?.ToString();
            result.ObjectCode = dr[_objectCodeIndex]?.ToString();
            result.CompanyCode = dr[_companyCodeIndex]?.ToString();
            result.BoqStyle = dr[_boqStyleIndex]?.ToString();
            result.HomeStyle = dr[_homeTyleIndex]?.ToString();
            result.WbsStatus = dr[_wbsStatusIndex]?.ToString();
            decimal minimumPrice;
            if (decimal.TryParse(dr[_minimumPriceIndex]?.ToString(), out minimumPrice))
            {
                result.MinimumPrice = minimumPrice;
            }
            double saleArea;
            if (double.TryParse(dr[_saleAreaIndex]?.ToString(), out saleArea))
            {
                result.SaleArea = saleArea;
            }
            return result;
        }
    }
}
