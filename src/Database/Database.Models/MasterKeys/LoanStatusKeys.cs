using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class LoanStatusKeys
    {
        /// <summary>
        /// อนุมัติ
        /// </summary>
        public static string Approve = "1";
        /// <summary>
        /// ปฏิเสธ
        /// </summary>
        public static string Reject = "2";
        /// <summary>
        /// รอผล
        /// </summary>
        public static string Pending = "3";
    }
}
