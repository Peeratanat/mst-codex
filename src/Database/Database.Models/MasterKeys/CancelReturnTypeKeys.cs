using System;
namespace Database.Models
{
    public class CancelReturnTypeKeys
    {
        /// <summary>
        /// คืนเงินทั้งหมด
        /// </summary>
        public static string ReturnAll = "1";
        /// <summary>
        /// ไม่คืนเงิน
        /// </summary>
        public static string NoReturn = "2";
        /// <summary>
        /// คืนเงินบางส่วน
        /// </summary>
        public static string ReturnSome = "3";
    }
}
