using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class TransferPromotionStatusTypeKeys
    {

        /// <summary>
        /// เสนอโปรโมชั่นโอน
        /// </summary>
        public static string WaitTransferPromotion = "1";
        /// <summary>
        /// รออนุมัติ Budget Promotion
        /// </summary>
        public static string WaitBudgetPromotion = "2";
        /// <summary>
        /// รออนุมัติ Min Price
        /// </summary>
        public static string WaitMinPrice = "3";
        /// <summary>
        /// รออนุมัติ Min Price และ Budget Promotion
        /// </summary>
        public static string WaitBudgetPromotionAndMinPrice = "4";
        /// <summary>
        /// อนุมัติ
        /// </summary>
        public static string Approved = "5";
        /// <summary>
        /// รออนุมัติ
        /// </summary>
        public static string WaitApprove = "6";
        /// <summary>
        /// LCM ไม่อนุมัติ
        /// </summary>
        public static string Reject = "7";

    }
}
