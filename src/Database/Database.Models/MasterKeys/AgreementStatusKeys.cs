using System;
namespace Database.Models
{
    public class AgreementStatusKeys
    {
        /// <summary>
        /// รอทำสัญญา
        /// </summary>
        public static string WaitingForContract = "1";
        /// <summary>
        /// รอลงนามสัญญา
        /// </summary>
        public static string WaitingForSignContract = "2";
        /// <summary>
        /// รอโอนกรรมสิทธิ์
        /// </summary>
        public static string WaitingForTransfer = "3";
        /// <summary>
        /// โอนกรรมสิทธิ์แล้ว
        /// </summary>
        public static string Transfer = "4";
        /// <summary>
        /// รออนุมัติย้ายแปลง
        /// </summary>
        public static string WaitingForApproveUnit = "5";
        /// <summary>
        /// รออนุมัติแก้ไข Price List
        /// </summary>
        public static string WaitingForApprovePriceList = "6";
        /// <summary>
        /// รออนุมัติ Min Price
        /// </summary>
        public static string WaitingForApproveMinPrice = "7";
        /// <summary>
        /// รออนุมัติ budgetPromotion
        /// </summary>
        public static string WaitingForApproveBudgetPromotion = "8";
        /// <summary>
        /// รออนุมัติ Min Price และ budgetPromotion
        /// </summary>
        public static string WaitingForApproveMinPriceAndBudgetPromotion = "9";
        /// <summary>
        /// รออนุมัติเปลียนแปลงโปร
        /// </summary>
        public static string WaitingForApproveChangePromotion = "10";
    }
}
