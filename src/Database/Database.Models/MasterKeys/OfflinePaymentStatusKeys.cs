using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public static class OfflinePaymentStatusKeys
    {
        /// <summary>
        ///ยกเลิก
        /// </summary>
        public static string Cancel = "Cancel";
        /// <summary>
        ///รอยืนยัน
        /// </summary>
        public static string Wait = "Wait";
        /// <summary>
        ///ยืนยันแล้ว
        /// </summary>
        public static string Approved = "Approved";
        public static string CancelReceipt = "CancelReceipt";
    }
}
