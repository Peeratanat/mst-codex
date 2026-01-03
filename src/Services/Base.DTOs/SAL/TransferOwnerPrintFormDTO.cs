using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class TransferOwnerPrintFormDTO
    {
        /// <summary>
        /// โอนกรรมสิทธิ์
        /// </summary>
        public Guid ProjectID { get; set; }

        /// <summary>
        /// วันที่โอนเริ่มต้น
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// วันที่โอนสิ้นสุด
        /// </summary>
        public DateTime DateEnd { get; set; }
        ///// <summary>
        ///// แนวราบ
        ///// </summary>
        //public bool PF_RP_TR_ALL_H { get; set; }
        ///// <summary>
        ///// แนวสูง
        ///// </summary>
        //public bool PF_RP_TR_ALL_CD { get; set; }
        /// <summary>
        /// รายงานการคีย์หลังโอน (2ชุด)
        /// </summary>
        public string Layer1 { get; set; }
        public string Layer2 { get; set; }
        /// <summary>
        /// รายงานรายละเอียดการรับชำระเงินโอน (2ชุด)
        /// </summary>
        public string Layer3 { get; set; }
        public string Layer4 { get; set; }
        /// <summary>
        /// ใบนำส่งเอกสารหลังโอนกรรมสิทธิ์ (2
        /// </summary>
        public string Layer5 { get; set; }
        public string Layer6 { get; set; }
        /// <summary>
        /// รายละเอียดการรับเงินในการโอน (3ชุด)
        /// </summary>
        public string Layer7 { get; set; }
        public string Layer8 { get; set; }
        public string Layer10 { get; set; }
        /// <summary>
        /// รายละเอียดค่าใช้จ่ายในการโอน  (1ชุด)
        /// </summary>
        public string Layer9 { get; set; }

    }
}
