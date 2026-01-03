using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MST_Unitmeter.Services.Excels
{
    public class UnitMeterStatusExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _unitNoIndex = 1;
        public const int _addressNoIndex = 2;
        public const int _isTransferElectricMeter = 3;
        public const int _electricMeterTransferDateIndex = 4;
        public const int _electricMeterTopic = 5;
        public const int _electricMeterRemark = 6;
        public const int _isTransferWaterMeter = 7;
        public const int _waterMeterTransferDate = 8;
        public const int _waterMeterTopic = 9;
        public const int _waterMeterRemark = 10;

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
        /// สถานะโอนมิเตอร์ไฟฟ้า
        /// </summary>
        public string IsTransferElectricMeter { get; set; }
        /// <summary>
        /// วันที่โอนมิเตอร์ไฟฟ้า
        /// </summary>
        public string ElectricMeterTransferDate { get; set; }
        /// <summary>
        /// หัวข้อมิเตอร์ไฟฟ้า
        /// </summary>
        public string ElectricMeterTopic { get; set; }
        /// <summary>
        /// หมายเหตุมิเตอร์ไฟฟ้า
        /// </summary>
        public string ElectricMeterRemark { get; set; }
        /// <summary>
        /// สถานะโอนมิเตอร์น้ำ
        /// </summary>
        public string IsTransferWaterMeter { get; set; }
        /// <summary>
        /// วันที่โอนมิเตอร์น้ำ
        /// </summary>
        public string WaterMeterTransferDate { get; set; }
        /// <summary>
        /// หัวข้อมิเตอร์น้ำ
        /// </summary>
        public string WaterMeterTopic { get; set; }
        /// <summary>
        /// หมายเหตุมิเตอร์น้ำ
        /// </summary>
        public string WaterMeterRemark { get; set; }
        public static UnitMeterStatusExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new UnitMeterStatusExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
            result.HouseNo = dr[_addressNoIndex]?.ToString();
            result.ElectricMeterTransferDate = dr[_electricMeterTransferDateIndex]?.ToString();
            result.ElectricMeterTopic = dr[_electricMeterTopic]?.ToString();
            result.ElectricMeterRemark = dr[_electricMeterRemark]?.ToString();
            result.WaterMeterTransferDate = dr[_waterMeterTransferDate]?.ToString();
            result.WaterMeterTopic = dr[_waterMeterTopic]?.ToString();
            result.WaterMeterRemark = dr[_waterMeterRemark]?.ToString();
            result.IsTransferElectricMeter = dr[_isTransferElectricMeter]?.ToString();
            result.IsTransferWaterMeter = dr[_isTransferWaterMeter]?.ToString();
            return result;
        }
    }
}
