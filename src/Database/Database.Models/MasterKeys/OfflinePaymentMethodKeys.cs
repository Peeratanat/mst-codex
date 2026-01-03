using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public static class OfflinePaymentMethodKeys
    {
        /// <summary>
        ///เงินสด
        /// </summary>
        public static string Cash = "Cash";
        /// <summary>
        ///แคชเชียร์เช็ค
        /// </summary>
        public static string CashierCheque = "CashierCheque";
        /// <summary>
        ///บัตรเคดิต
        /// </summary>
        public static string CreditCard = "CreditCard";
        /// <summary>
        ///เชค
        /// </summary>
        public static string Cheque = "Cheque";
        /// <summary>
        ///เงิินโอนผ่านธนาคาร
        /// </summary>
        public static string Transfer = "Transfer";
    }
}
