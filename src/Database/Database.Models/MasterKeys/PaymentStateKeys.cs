using System;
namespace Database.Models
{
    public static class PaymentStateKeys
    {
        /// <summary>
        ///จอง
        /// </summary>
        public static string Booking = "Booking";
        /// <summary>
        ///โอนกรรมสิทธิ์
        /// </summary>
        public static string Transfer = "Transfer";
        /// <summary>
        ///สัญญา
        /// </summary>
        public static string Agreement = "Agreement";
        /// <summary>
        ///รับเงินก่อนโอน
        /// </summary>
        public static string BeforeTransfer = "BeforeTransfer";
        /// <summary>
        ///เงินทอน
        /// </summary>
        public static string Return = "Return";

    }
}
