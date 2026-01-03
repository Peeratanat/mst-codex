using System.Collections.Generic;
using System.Data;

namespace PRJ_Unit.Services.Excels
{
    public class PriceListExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _projectNameIndex = 1;
        public const int _wbsCodeIndex = 2;
        public const int _unitNoIndex = 3;
        public const int _sellPriceIndex = 4;
        public const int _areaPricePerUnitIndex = 5;
        public const int _locationPriceIndex = 6;
        public const int _bookingAmountIndex = 7;
        public const int _contractAmountIndex = 8;
        public const int _downPercentIndex = 9;
        public const int _downInstallmentIndex = 10;
        public const int _specialDownInstallmensIndex = 11;
        public const int _specialDownInstallmentAmountsIndex = 12;
        public const int _documentDateIndex = 13;

        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// ชื่อโครงการ
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// WBS Code
        /// </summary>
        public string WBSCode { get; set; }
        /// <summary>
        /// เลขที่แปลง  
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// ราคาขาย
        /// </summary>
        public double SellPrice { get; set; }
        /// <summary>
        /// ราคาพื้นที่ต่อหน่วย
        /// </summary>
        public double AreaPricePerUnit { get; set; }
        /// <summary>
        /// ค่าทำเล
        /// </summary>
        public double LocationPrice { get; set; }
        /// <summary>
        /// เงินจอง
        /// </summary>
        public double BookingAmount { get; set; }
        /// <summary>
        /// เงินทำสัญญา
        /// </summary>
        public double ContractAmount { get; set; }
        /// <summary>
        /// %เงินดาวน์
        /// </summary>
        public double DownPercent { get; set; }
        /// <summary>
        /// งวดดาวน์
        /// </summary>
        public int DownInstallment { get; set; }
        /// <summary>
        /// งวดดาวน์พิเศษ
        /// </summary>
        public List<int> SpecialDownInstallments { get; set; }
        /// <summary>
        /// เงินดาวน์พิเศษ
        /// </summary>
        public List<double> SpecialDownInstallmentAmounts { get; set; }
        /// <summary>
        /// วันที่ Version เอกสาร
        /// </summary>
        public string DocumentDate { get; set; }
        public static PriceListExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new PriceListExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.ProjectName = dr[_projectNameIndex]?.ToString();
            result.WBSCode = dr[_wbsCodeIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
            double sellPrice;
            if (double.TryParse(dr[_sellPriceIndex]?.ToString(), out sellPrice))
            {
                result.SellPrice = sellPrice;
            }
            double areaPricePerUnit;
            if (double.TryParse(dr[_areaPricePerUnitIndex].ToString(), out areaPricePerUnit))
            {
                result.AreaPricePerUnit = areaPricePerUnit;
            }
            double locationPrice;
            if (double.TryParse(dr[_locationPriceIndex].ToString(), out locationPrice))
            {
                result.LocationPrice = locationPrice;
            }
            double bookingAmount;
            if (double.TryParse(dr[_bookingAmountIndex]?.ToString(), out bookingAmount))
            {
                result.BookingAmount = bookingAmount;
            }
            double contractAmount;
            if (double.TryParse(dr[_contractAmountIndex]?.ToString(), out contractAmount))
            {
                result.ContractAmount = contractAmount;
            }
            double downPercent;
            if (double.TryParse(dr[_downPercentIndex]?.ToString(), out downPercent))
            {
                result.DownPercent = downPercent;
            }
            int downInstallment;
            if (int.TryParse(dr[_downInstallmentIndex]?.ToString(), out downInstallment))
            {
                result.DownInstallment = downInstallment;
            }

            string[] specialDownInstallmentPeriods = dr[_specialDownInstallmensIndex]?.ToString()?.Split(',');
            result.SpecialDownInstallments = new List<int>();
            foreach (var item in specialDownInstallmentPeriods)
            {
                int specialDownInstallment;
                if (int.TryParse(item, out specialDownInstallment))
                {
                    result.SpecialDownInstallments.Add(specialDownInstallment);
                }
            }

            string[] specialDownInstallmentAmounts = dr[_specialDownInstallmentAmountsIndex]?.ToString()?.Split(',');
            result.SpecialDownInstallmentAmounts = new List<double>();
            foreach (var item in specialDownInstallmentAmounts)
            {
                double specialDownInstallmentAmount;
                if (double.TryParse(item, out specialDownInstallmentAmount))
                {
                    result.SpecialDownInstallmentAmounts.Add(specialDownInstallmentAmount);
                }
            }

            result.DocumentDate = dr[_documentDateIndex]?.ToString();

            return result;
        }
    }
}