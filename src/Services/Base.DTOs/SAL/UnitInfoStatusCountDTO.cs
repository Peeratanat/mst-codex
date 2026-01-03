using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class UnitInfoStatusCountDTO
    {
        /// <summary>
        /// ทั้งหมด
        /// </summary>
        public int All { get; set; }
        /// <summary>
        /// รอจอง
        /// </summary>
        public int Available { get; set; }
        /// <summary>
        /// รอยืนยันจอง
        /// </summary>
        public int WaitingForConfirmBooking { get; set; }
        /// <summary>
        /// รอทำสัญญา
        /// </summary>
        public int WaitingForAgreement { get; set; }
        /// <summary>
        /// รอโอนกรรมสิทธิ์
        /// </summary>
        public int WaitingForTransfer { get; set; }
        /// <summary>
        /// โอนกรรมสิทธิ์แล้ว
        /// </summary>
        public int Transfer { get; set; }
        /// <summary>
        /// โอนลอย
        /// </summary>
        public int PreTransfer { get; set; }


        public int Open { get; set; }
    }
}
