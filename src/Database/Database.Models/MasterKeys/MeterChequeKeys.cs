using System;
namespace Database.Models.MasterKeys
{
    public class MeterChequeKeys
    {
        //รวมในค่าบ้าน	1
        //ไม่รวมในค่าบ้าน	2
        //ฟรี	3
        //ไม่ระบุ	0

        /// <summary>
        ///รวมในค่าบ้าน
        /// </summary>
        public static string TotalInHouseValue = "1";
        /// <summary>
        ///ไม่รวมในค่าบ้าน
        /// </summary>
        public static string NotTotalInHouseValue = "2";
        /// <summary>
        ///ฟรี
        /// </summary>
        public static string Free = "3";
        /// <summary>
        ///ไม่ระบุ
        /// </summary>
        public static string None = "0"; 
    }
}
