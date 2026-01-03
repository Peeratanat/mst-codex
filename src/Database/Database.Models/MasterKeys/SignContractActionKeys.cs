using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class SignContractActionKeys
    {
        /// <summary>
        /// ยืนยันลงนามสัญญา
        /// </summary>
        public static string ConfirmSignContract = "1";
        /// <summary>
        /// Sign Contract
        /// </summary>
        public static string SignContract = "2";
        /// <summary>
        /// Approve Sign Contract
        /// </summary>
        public static string ApproveSignContract = "3";
        /// <summary>
        /// Reject Sign Contract
        /// </summary>
        public static string RejectSignContract = "4";
        /// <summary>
        /// ยกเลิก Approve
        /// </summary>
        public static string CancelApproveSignContract = "5";

    }
}
