using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class HighRiseConstructionStatusKeys
    {
        /// <summary>
        /// ได้ก่อสร้างแล้วเสร็จ อยู่ระหว่างการนำไปจดทะเบียนอาคารชุด
        /// </summary>
        public static string BuildingCompleted = "1";
        /// <summary>
        /// อยู่ในระหว่างการก่อสร้าง เมื่อได้ก่อสร้างแล้วเสร็จจะนำไปจดทะเบียนเป็นอาคารชุด
        /// </summary>
        public static string BuildingInProgress = "2";
    }
}
