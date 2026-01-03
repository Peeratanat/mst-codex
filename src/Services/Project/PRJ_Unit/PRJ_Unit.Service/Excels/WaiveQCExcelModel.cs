using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
    public class WaiveQCExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _wbsNoIndex = 1;
        public const int _unitNoIndex = 2;
        public const int _waiveQCDateIndex = 3;
        public const int _unitStatus = 4;
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
        /// วันที่ Waive QC
        /// </summary>
        public DateTime? WaiveQCDate { get; set; }

        public static WaiveQCExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new WaiveQCExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.WBSNo = dr[_wbsNoIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();

            DateTime waiveQCDate;
            if (DateTime.TryParseExact(dr[_waiveQCDateIndex]?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out waiveQCDate))
            {
                result.WaiveQCDate = waiveQCDate;
            }
          
            return result;
        }

        public void ToModel(ref WaiveQC model)
        {
            model.WaiveQCDate = this.WaiveQCDate;
        }
    }
}
