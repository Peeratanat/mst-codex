using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
    public class TitledeedDetailExcelModel
    {
        //public const int _projectNoIndex = 0;
        //public const int _wbsNoIndex = 1;
        //public const int _unitNoIndex = 2;
        //public const int _modelNameIndex = 3;
        //public const int _houseNoIndex = 4;
        //public const int _houseNoReceivedYear = 5;
        //public const int _titledeedNoIndex = 6;
        //public const int _landNoIndex = 7;
        //public const int _landSurveyAreaIndex = 8;
        //public const int _landPortionNoIndex = 9;
        //public const int _bookNoIndex = 10;
        //public const int _pageNoIndex = 11;
        //public const int _titledeedAreaIndex = 12;
        //public const int _estimatePriceIndex = 13;
        //public const int _remarkIndex = 14;

        public const int _projectNoIndex = 0;  //รหัสโครงการ
        public const int _wbsNoIndex = 1; //WBS Code
        public const int _unitNoIndex = 2; //เลขที่แปลง
        public const int _modelNameIndex = 3; //ชื่อแบบบ้าน

        public const int _floorNoIndex = 4; //ชั้น

        public const int _houseNoIndex = 5; //บ้านเลขที่/ห้องชุดเลขที่
        public const int _houseNoReceivedYear = 6; //ปีที่ได้บ้านเลขที่
        public const int _titledeedNoIndex = 7; //เลขที่โฉนด
        public const int _landNoIndex = 8; //เลขที่ดิน
        public const int _landSurveyAreaIndex = 9; //หน้าสำรวจ
        public const int _landPortionNoIndex = 10; //เลขระวาง
        public const int _bookNoIndex = 11; //เล่มที่
        public const int _pageNoIndex = 12; //หน้า
        public const int _titledeedAreaIndex = 13; //เนื้อที่ (ตรว#)/พื้นที่ห้องชุด


        public const int _usedAreaIndex = 14;//พื้นที่ใช้สอย
        public const int _fenceAreaIndex = 15;//รั้วคอนกรีต
        public const int _fenceIronAreaIndex = 16;//รั้วเหล็กดัด
        public const int _balconyAreaIndex = 17;//พื้นที่ระเบียง
        public const int _airAreaIndex = 18;//พื้นที่วางแอร์
        public const int _poolAreaIndex = 19;//พื้นที่สระว่ายน้ำ
        public const int _parkingAreaIndex = 20;//พื้นที่จอดรถ
        public const int _totalAreaIndex = 21;//พื้นที่รวม


        public const int _estimatePriceIndex = 22; //ราคาประเมิน
        public const int _remarkIndex = 23; //หมายเหตุ

        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// WBS Code
        /// </summary>
        public string WBSNo { get; set; }
        /// <summary>
        /// เลขที่แปลง  
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// ชื่อแบบบ้าน
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        /// ชั้นที่
        /// </summary>
        public string FloorNo { get; set; }
        /// <summary>
        /// บ้านเลขที่/ห้องชุดเลขที่
        /// </summary>
        public string HouseNo { get; set; }
        /// <summary>
        ///  ปีที่ได้บ้านเลขที่
        /// </summary>
        public int HouseNoReceivedYear { get; set; }
        /// <summary>
        /// เลขที่โฉนด
        /// </summary>
        public string TitledeedNo { get; set; }
        /// <summary>
        /// เลขที่ดิน
        /// </summary>
        public string LandNo { get; set; }
        /// <summary>
        /// หน้าสำรวจ
        /// </summary>
        public string LandSurveyArea { get; set; }
        /// <summary>
        /// เลขระวาง
        /// </summary>
        public string LandPortionNo { get; set; }
        /// <summary>
        /// เล่มที่
        /// </summary>
        public string BookNo { get; set; }
        /// <summary>
        /// หน้า
        /// </summary>
        public string PageNo { get; set; }
        /// <summary>
        /// พื้นที่โฉนด/เนื้อที่(ตรว)/พื้นที่ห้องชุด
        /// </summary>
        public double TitledeedArea { get; set; }


        /// <summary>
        /// พื้นที่ใช้สอย
        /// </summary>
        public double UsedArea { get; set; }
        /// <summary>
        /// รั้วคอนกรีต
        /// </summary>
        public double FenceArea { get; set; }
        /// <summary>
        /// รั้วเหล็กดัด
        /// </summary>
        public double FenceIronArea { get; set; }
        /// <summary>
        /// พื้นที่ระเบียง
        /// </summary>
        public double BalconyArea { get; set; }
        /// <summary>
        /// พื้นที่วางแอร์
        /// </summary>
        public double AirArea { get; set; }
        /// <summary>
        /// พื้นที่สระว่ายน้ำ
        /// </summary>
        public double PoolArea { get; set; }
        /// <summary>
        /// พื้นที่จอดรถ
        /// </summary>
        public double ParkingArea { get; set; }
        /// <summary>
        /// พื้นที่รวม
        /// </summary>
        public double TotalArea { get; set; }

        /// <summary>
        /// %เงินดาวน์
        /// </summary>
        public double EstimatePrice { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        public static TitledeedDetailExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new TitledeedDetailExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.WBSNo = dr[_wbsNoIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
            result.ModelName = dr[_modelNameIndex]?.ToString();

            result.FloorNo = dr[_floorNoIndex]?.ToString();

            result.TitledeedNo = dr[_titledeedNoIndex]?.ToString();
            result.LandNo = dr[_landNoIndex]?.ToString();
            result.HouseNo = dr[_houseNoIndex]?.ToString();
            int houseNoReceivedYear;
            if (int.TryParse(dr[_houseNoReceivedYear]?.ToString(), out houseNoReceivedYear))
            {
                result.HouseNoReceivedYear = houseNoReceivedYear;
            }
            result.LandSurveyArea = dr[_landSurveyAreaIndex]?.ToString();
            result.LandPortionNo = dr[_landPortionNoIndex]?.ToString();
            result.BookNo = dr[_bookNoIndex]?.ToString();
            result.PageNo = dr[_pageNoIndex]?.ToString();
            double titledeedArea;
            if (double.TryParse(dr[_titledeedAreaIndex]?.ToString(), out titledeedArea))
            {
                result.TitledeedArea = titledeedArea;
            }

            double usedArea;
            if (double.TryParse(dr[_usedAreaIndex]?.ToString(), out usedArea))
            {
                result.UsedArea = usedArea;
            }
            double fenceArea;
            if (double.TryParse(dr[_fenceAreaIndex]?.ToString(), out fenceArea))
            {
                result.FenceArea = fenceArea;
            }
            double fenceIronArea;
            if (double.TryParse(dr[_fenceIronAreaIndex]?.ToString(), out fenceIronArea))
            {
                result.FenceIronArea = fenceIronArea;
            }
            double balconyArea;
            if (double.TryParse(dr[_balconyAreaIndex]?.ToString(), out balconyArea))
            {
                result.BalconyArea = balconyArea;
            }
            double airArea;
            if (double.TryParse(dr[_airAreaIndex]?.ToString(), out airArea))
            {
                result.AirArea = airArea;
            }
            double poolArea;
            if (double.TryParse(dr[_poolAreaIndex]?.ToString(), out poolArea))
            {
                result.PoolArea = poolArea;
            }
            double parkingArea;
            if (double.TryParse(dr[_parkingAreaIndex]?.ToString(), out parkingArea))
            {
                result.ParkingArea = parkingArea;
            }
            double totalArea;
            if (double.TryParse(dr[_totalAreaIndex]?.ToString(), out totalArea))
            {
                result.TotalArea = totalArea;
            }

            double estimatePrice;
            if (double.TryParse(dr[_estimatePriceIndex]?.ToString(), out estimatePrice))
            {
                result.EstimatePrice = estimatePrice;
            }
            result.Remark = dr[_remarkIndex]?.ToString();
            return result;
        }
        public void ToModel(ref TitledeedDetail model)
        {
            model.TitledeedNo = this.TitledeedNo;
            model.LandNo = this.LandNo; //เลขที่ดิน
            model.LandSurveyArea = this.LandSurveyArea;
            model.LandPortionNo = this.LandPortionNo;
            model.PageNo = this.PageNo;
            model.BookNo = this.BookNo;
            model.TitledeedArea = this.TitledeedArea;
            model.Remark = this.Remark;
            model.EstimatePrice = Convert.ToDecimal(this.EstimatePrice);
        }
        public void ToModel(ref TitleDeedPublicUtility model)
        {
            model.TitledeedNo = this.TitledeedNo;  //เลขที่โฉนด
            model.LandNo = this.LandNo; //เลขที่ดิน
            model.LandSurveyArea = this.LandSurveyArea; //หน้าสำรวจ
            model.LandPortionNo = this.LandPortionNo; //เลขระวาง
            model.PageNo = this.PageNo; //หน้า
            model.BookNo = this.BookNo; //เล่ม
            model.TitledeedArea = this.TitledeedArea; //พื้นที่โฉนด
        }
    }
}
