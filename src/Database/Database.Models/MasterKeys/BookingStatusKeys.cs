using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class BookingStatusKeys
    {
        /// <summary>
        /// รออนุมัติแก้ไข Price List
        /// </summary>
        public static string WaitingForApprovePriceList = "0";
        /// <summary>
        /// รอยืนยันจอง
        /// </summary>
        public static string WaitForBookingConfirmation = "1";
        /// <summary>
        /// จอง
        /// </summary>
        public static string Booking = "2";
        /// <summary>
        /// สัญญา
        /// </summary>
        public static string Contract = "3";
        /// <summary>
        /// โอนกรรมสิทธิ์
        /// </summary>
        public static string TransferOwnership = "4";
        /// <summary>
        /// รออนุมัติ Min Price
        /// </summary>
        public static string WaitingForApproveMinPrice = "5";
        /// <summary>
        /// รออนุมัติย้ายแปลง
        /// </summary>
        public static string WaitingForApproveUnit = "6";
        /// <summary>
        /// รออนุมัติ budgetPromotion
        /// </summary>
        public static string WaitingForApproveBudgetPromotion = "7";
        /// <summary>
        /// รออนุมัติเปลียนแปลงโปร
        /// </summary>
        public static string WaitingForApproveChangePromotion = "8";

        /// <summary>
        /// รออนุมัติ Min Price และ budgetPromotion
        /// </summary>
        public static string WaitingForApproveMinPriceAndBudgetPromotion = "9";

        public static List<string> CanPay = new List<string> {
            //Booking, เช็คแค่ BookingNo
            Contract,
            TransferOwnership
        };
    }
}
