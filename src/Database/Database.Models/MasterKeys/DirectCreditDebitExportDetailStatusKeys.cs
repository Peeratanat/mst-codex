using System;
namespace Database.Models
{
    public class DirectCreditDebitExportDetailStatusKeys
    {

        /// <summary>
        /// รอ Import Text File
        /// </summary>
        public static string Wait = "Wait";

        /// <summary>
        /// ข้อมูลถูกต้อง
        /// </summary>
        public static string Complete = "Complete";

        /// <summary>
        /// ไม่สามารถตัดเงินได้
        /// </summary>
        public static string Fail = "Fail";

        /// <summary>
        /// ห้องโอนกรรมสิทธิ์แล้ว
        /// </summary>
        public static string TransferUnit = "TransferUnit";

        /// <summary>
        /// ข้อมูลถูกต้อง
        /// </summary>
        public static string Cancel = "Cancel";

        /// <summary>
        /// ห้องยกเลิก
        /// </summary>
        public static string CancelUnit = "CancelUnit";

        /// <summary>
        /// ตั้งพัก ฉ.2,3
        /// </summary>
        public static string CTMLetter = "CTMLetter";
    }
}
