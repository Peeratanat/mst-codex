using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class ContactStatusKeys
    {
        /// <summary>
        /// ลูกค้าใหม่
        /// </summary>
        public static string New = "C";
        /// <summary>
        /// ลูกค้าเก่า
        /// </summary>
        public static string Old = "M";
        /// <summary>
        /// พนักงาน
        /// </summary>
        public static string Staff = "A";
        /// <summary>
        /// พนักงานขับรถ
        /// </summary>
        public static string Chauffeur = "D";
        /// <summary>
        /// ญาติ/ครอบครัว
        /// </summary>
        public static string Family = "F";
        /// <summary>
        /// คนส่งของ
        /// </summary>
        public static string Freighter = "P";
        /// <summary>
        /// ผู้รับเหมา
        /// </summary>
        public static string Contractor = "V";
        /// <summary>
        /// ลูกค้าเซ็นต์เอกสาร
        /// </summary>
        public static string Other = "O";
    }
}
