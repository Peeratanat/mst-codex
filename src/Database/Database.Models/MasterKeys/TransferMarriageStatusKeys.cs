using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class TransferMarriageStatusKeys
    {
        /// <summary>
        /// โสด
        /// </summary>
        public static string Single = "1";
        /// <summary>
        /// สมรส จดทะเบียน
        /// </summary>
        public static string MarryRegis = "2";
        /// <summary>
        /// สมรส ไม่จดทะเบียน
        /// </summary>
        public static string MarryNotRegis = "3";
        /// <summary>
        /// หย่าร้าง(หม้าย)
        /// </summary>
        public static string Divorce = "4";
    }
}
