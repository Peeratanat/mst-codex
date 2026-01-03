using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
    public class WaiveCustomerSignExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _wbsNoIndex = 1;
        public const int _unitNoIndex = 2;
        public const int _waiveSignDateIndex = 3;
        public const int _unitStatusIndex = 4;
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// Sap WBS Number
        /// </summary>
        public string WBSNo { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        ///  วันที่ Waive Sign
        /// </summary>
        public DateTime? WaiveSignDate { get; set; }

        public static WaiveCustomerSignExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new WaiveCustomerSignExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.WBSNo = dr[_wbsNoIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();

            DateTime waiveSignDate;
            if (DateTime.TryParseExact(dr[_waiveSignDateIndex]?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out waiveSignDate))
            {
                result.WaiveSignDate = waiveSignDate;
            }

            return result;
        }

        public void ToModel(ref WaiveQC model)
        {
            model.WaiveSignDate = this.WaiveSignDate;
        }
    }
}
