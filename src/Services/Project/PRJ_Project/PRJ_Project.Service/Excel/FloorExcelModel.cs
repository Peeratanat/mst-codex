using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Project.Services.Excels
{
    public class FloorExcelModel
    {
        public const int _towerCodeIndex = 0;
        public const int _floorNameTHIndex = 1;
        public const int _floorNameENIndex = 2;
        public const int _dueTransferDateIndex = 3;

        /// <summary>
        /// รหัสตึก
        /// </summary>
        public string TowerCode { get; set; }
        /// <summary>
        /// ชื่อชั้น-TH
        /// </summary>
        public string FloorNameTH { get; set; }
        /// <summary>
        /// ชื่อชั้น-EN
        /// </summary>
        public string FloorNameEN { get; set; }
        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? DueTransferDate { get; set; }

        public static FloorExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new FloorExcelModel();
            result.TowerCode = dr[_towerCodeIndex]?.ToString();
            result.FloorNameTH = dr[_floorNameTHIndex]?.ToString();
            result.FloorNameEN = dr[_floorNameENIndex]?.ToString();

            DateTime dueTransferDate;
            if (DateTime.TryParseExact(dr[_dueTransferDateIndex]?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out dueTransferDate))
            {
                result.DueTransferDate = dueTransferDate;
            }

            return result;
        }

        public void ToModel(ref Floor model)
        {
            model.DueTransferDate = this.DueTransferDate;
        }
    }
}