using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class VisitorProjectDTO
    {
        /// <summary>
        /// จำนวนผู้เข้าออก
        /// </summary>
        public int VisitInOutCount { get; set; }
        /// <summary>
        /// จำนวน Visitor ที่ต้อนรับแล้ว
        /// </summary>
        public int VisitorWelcomeCount { get; set; }
    }
}
