using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
    public class HighRiseFeeExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _towerCodeIndex = 1;
        public const int _floorNameIndex = 2;
        public const int _unitNoIndex = 3;
        public const int _calculateParkAreaIndex = 4;
        public const int _estimatePriceAreaIndex = 5;
        public const int _estimatePriceUsageAreaIndex = 6;
        public const int _estimatePriceBalconyArea = 7;
        public const int _estimatePriceAirAreaIndex = 8;
        public const int _estimatePricePoolAreaIndex = 9;
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// รหัสตึก
        /// </summary>
        public string TowerCode { get; set; }
        /// <summary>
        /// ชื่อชั้น
        /// </summary>
        public string FloorName { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// คำนวณพื้นที่จอดรถ
        /// </summary>
        public string CalculateParkArea { get; set; }
        /// <summary>
        ///  ราคาประเมิณพื้นที่จอดรถ
        /// </summary>
        public double EstimatePriceArea { get; set; }
        /// <summary>
        ///  ราคาประเมิณพื้นที่ใช้สอย
        /// </summary>
        public double EstimatePriceUsageArea { get; set; }
        /// <summary>
        ///  ราคาประเมิณพื้นที่ใช้สอยระเบียง
        /// </summary>
        public double EstimatePriceBalconyArea { get; set; }
        /// <summary>
        ///  ราคาประเมิณพื้นที่วางแอร์
        /// </summary>
        public double EstimatePriceAirArea { get; set; }
        /// <summary>
        ///  ราคาประเมิณพื้นที่สระว่ายน้ำ
        /// </summary>
        public double EstimatePricePoolArea { get; set; }

        public static HighRiseFeeExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new HighRiseFeeExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.TowerCode = dr[_towerCodeIndex]?.ToString();
            result.FloorName = dr[_floorNameIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
            result.CalculateParkArea = dr[_calculateParkAreaIndex]?.ToString();

            double estimatePriceArea;
            if (double.TryParse(dr[_estimatePriceAreaIndex]?.ToString(), out estimatePriceArea))
            {
                result.EstimatePriceArea = estimatePriceArea;
            }
            double estimatePriceUsageArea;
            if (double.TryParse(dr[_estimatePriceUsageAreaIndex]?.ToString(), out estimatePriceUsageArea))
            {
                result.EstimatePriceUsageArea = estimatePriceUsageArea;
            }
            double estimatePriceBalconyArea;
            if (double.TryParse(dr[_estimatePriceBalconyArea]?.ToString(), out estimatePriceBalconyArea))
            {
                result.EstimatePriceBalconyArea = estimatePriceBalconyArea;
            }
            double estimatePriceAirArea;
            if (double.TryParse(dr[_estimatePriceAirAreaIndex]?.ToString(), out estimatePriceAirArea))
            {
                result.EstimatePriceAirArea = estimatePriceAirArea;
            }
            double estimatePricePoolArea;
            if (double.TryParse(dr[_estimatePricePoolAreaIndex]?.ToString(), out estimatePricePoolArea))
            {
                result.EstimatePricePoolArea = estimatePricePoolArea;
            }
            return result;
        }
        public void ToModel(ref HighRiseFee model)
        {
            model.EstimatePriceArea = Convert.ToDecimal(this.EstimatePriceArea);
            model.EstimatePriceUsageArea = Convert.ToDecimal(this.EstimatePriceUsageArea);
            model.EstimatePriceBalconyArea = Convert.ToDecimal(this.EstimatePriceBalconyArea);
            model.EstimatePriceAirArea = Convert.ToDecimal(this.EstimatePriceAirArea);
            model.EstimatePricePoolArea = Convert.ToDecimal(this.EstimatePricePoolArea);
        }
    }

}
