using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
    public class UnitMeterExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _unitNoIndex = 1;
        public const int _addressNoIndex = 2;
        public const int _electricMeterNoIndex = 3;
        public const int _waterMeterNoIndex = 4;

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
        /// เลขที่มิเตอร์ไฟฟ้า
        /// </summary>
        public string ElectricMeter { get; set; }
        /// <summary>
        /// เลขที่มิเตอร์น้ำ
        /// </summary>
        public string WaterMeter { get; set; }
        public static UnitMeterExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new UnitMeterExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
            result.HouseNo = dr[_addressNoIndex]?.ToString();
            result.ElectricMeter = dr[_electricMeterNoIndex]?.ToString();
            result.WaterMeter = dr[_waterMeterNoIndex]?.ToString();

            return result;
        }
        public void ToModel(ref Unit model)
        {
            if (model.ElectricMeter != this.ElectricMeter)
            {
                model.ElectrictMeterSaved = DateTime.Now;
            }
            if (model.WaterMeter != this.WaterMeter)
            {
                model.WaterMeterSaved = DateTime.Now;
            }
            model.ElectricMeter = this.ElectricMeter;
            model.WaterMeter = this.WaterMeter;
        }
    }
}
