using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class TypeofloanKeys
    {
        /// <summary>
        /// รอขอสินเชื่อ
        /// </summary>
        public static string WaitingforLoan = "waitingforLoan";
        /// <summary>
        /// กู้เอง
        /// </summary>
        public static string LoanbyCustomer = "loanbyCustomer";
        /// <summary>
        /// กู้ผ่านโครงการ
        /// </summary>
        public static string LoanbyProject = "loanbyProject";
        /// <summary>
        /// โอนสด
        /// </summary>
        public static string TranferbyCustomer = "tranferbyCustomer";
    }
}
