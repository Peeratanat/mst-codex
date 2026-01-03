using System;
using System.Collections.Generic;

namespace Database.Models.MasterKeys
{
    public static class MasterCombineStatusKeys
    {
        /// <summary>
        // ไม่อนุมัติ
        /// <summary>
        public static string Reject = "4";
        /// <summary>
        // อนุมัติ
        /// <summary>
        public static string Approve = "3";
        /// <summary>
        // รออนมุัติ
        /// <summary>
        public static string WaitApprove = "2";
        /// <summary>
        // รอดำเนินการ
        /// <summary>
        public static string Wait = "1";
    }
}
