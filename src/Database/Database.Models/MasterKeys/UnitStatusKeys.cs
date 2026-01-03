using System;
namespace Database.Models
{
    public static class UnitStatusKeys
    {
        /// <summary>
        /// รอจอง
        /// </summary>
        public static string Available = "0";
        /// <summary>
        /// รอยืนยันจอง
        /// </summary>
        public static string WaitingForConfirmBooking = "1";
        /// <summary>
        /// รอทำสัญญา
        /// </summary>
        public static string WaitingForAgreement = "2";
        /// <summary>
        /// รอโอนกรรมสิทธิ์
        /// </summary>
        public static string WaitingForTransfer = "3";
        /// <summary>
        /// โอนกรรมสิทธิ์แล้ว
        /// </summary>
        public static string Transfer = "4";
        /// <summary>
        /// โอนลอย
        /// </summary>
        public static string PreTransfer = "5";

        public static bool IsSold(string key)
        {
            return key == UnitStatusKeys.WaitingForConfirmBooking ||
                key == UnitStatusKeys.WaitingForAgreement ||
                key == UnitStatusKeys.Transfer ||
                key == UnitStatusKeys.WaitingForTransfer ||
                key == UnitStatusKeys.Transfer ||
                key == UnitStatusKeys.PreTransfer;
        }
    }
}
