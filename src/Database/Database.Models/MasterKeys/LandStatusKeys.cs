using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class LandStatusKeys
    {
        //อยู่ที่ธนาคาร	1
        //ขอปลอด	2
        //อยู่ที่บริษัท	3
        //อยู่ที่โอนกรรมสิทธิ์	4
        //อยู่ที่ลูกค้า	5

        /// <summary>
        /// อยู่ที่ธนาคาร
        /// </summary>
        public static string AtBank = "1";
        /// <summary>
        /// ขอปลอด
        /// </summary>
        public static string ReqFree = "2";
        /// <summary>
        /// อยู่ที่บริษัท
        /// </summary>
        public static string AtCompany = "3";
        /// <summary>
        /// อยู่ที่โอนกรรมสิทธิ์
        /// </summary>
        public static string AtTransfer = "4";
        /// <summary>
        /// อยู่ที่ลูกค้า
        /// </summary>
        public static string AtCustomer = "5";
    }
}
