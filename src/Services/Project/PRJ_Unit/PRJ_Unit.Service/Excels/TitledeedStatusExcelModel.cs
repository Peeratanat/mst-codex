using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
    public class TitledeedStatusExcelModel
    {


        public const int _projectnoIndex = 0;  //รหัสโครงการ
        public const int _projectnameIndex = 1; //ชื่อโครงการ
        public const int _unitnoIndex = 2; //เลขที่แปลง
        public const int _titledeednoIndex = 3; //เลขที่โฉนด
        public const int _titledeedareaIndex = 4; //เนื้อที่
        public const int _allownernameIndex = 5; //ผู้ซื้อ
        public const int _totalpriceIndex = 6; //ราคาขายสุทธิ (รวมพื้นที่เพิ่ม/ลด)
        public const int _repayloanIndex = 7; //คืนเงินกู้

        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string UnitNo { get; set; }
        public string TitledeedNo { get; set; }
        public decimal? TitledeedArea { get; set; }
        public string AllOwnerName { get; set; }
        public double? TotalPrice { get; set; }
        public double? RepayLoan { get; set; }

    }
}
