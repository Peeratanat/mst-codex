using System;

namespace Database.Models
{
    public class RefundMemoTypeKey
    {
        /// <summary>
        /// โอนเงินคืนลูกค้าและคืนนิติฯ
        /// </summary>
        public static string RefundCustomerAndLegalEntity = "1";
        /// <summary>
        /// โอนเงินคืนลูกค้า(สั่งจ่าย AP)
        /// </summary>
        public static string RefundCustomerByAP = "2";
        /// <summary>
        /// โอนเงินคืนนิติ
        /// </summary>
        public static string RefundLegalEntity = "3";
        /// <summary>
        /// โอนเงินคืนลูกค้า(สั่งจ่ายนิติ)
        /// </summary>
        public static string RefundCustomerByLegalEntity = "4";
    }
}
