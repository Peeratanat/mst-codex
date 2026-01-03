using System;
namespace Database.Models
{
    public class TransferStatusKeys
    {
        /// <summary>
        /// รอตั้งเรื่องโอน
        /// </summary>
        public static string WaitingForTransfer = "1";
        /// <summary>
        /// ตั้งเรื่องโอนแล้ว
        /// </summary>
        public static string Transfered = "2";
        /// <summary>
        /// พร้อมโอน
        /// </summary>
        public static string ReadyToTransfer = "3";
        /// <summary>
        /// โอนกรรมสิทธิ์แล้ว
        /// </summary>
        public static string TransferConfirmed = "4";
        /// <summary>
        /// ยืนยันการชำระแล้ว
        /// </summary>
        public static string PaymentConfirmed = "5";
        /// <summary>
        /// สิ้นสุดการโอน
        /// </summary>
        public static string TransferComplete = "6";
    }
}