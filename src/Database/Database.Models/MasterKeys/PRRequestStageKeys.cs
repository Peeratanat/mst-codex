using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public static class PRRequestStageKeys
    {
        /// <summary>
        /// ก่อนขาย
        /// </summary>
        public static string Presale = "1";
        /// <summary>
        /// ขาย
        /// </summary>
        public static string Sale = "2";
        /// <summary>
        /// โอน
        /// </summary>
        public static string Transfer = "3";
        /// <summary>
        /// ค่าใช้จ่าย
        /// </summary>
        public static string Expense = "4";
    }
}
