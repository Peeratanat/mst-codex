using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class PromotionRequestPRStatusKeys
    {
        /// <summary>
        /// Gen PR สำเร็จ
        /// </summary>
        public static string Approve = "1";
        /// <summary>
        /// Gen PR สำเร็จ บางรายการ
        /// </summary>
        public static string ApproveSomeUnit = "2";
        /// <summary>
        /// อยู่ระหว่าง Gen PR กันงบ
        /// </summary>
        public static string WaitApprove = "3";
        /// <summary>
        /// Gen PR ไม่สำเร็จ
        /// </summary>
        public static string Reject = "4";
        /// <summary>
        /// ยกเลิก PR
        /// </summary>
        public static string Cancel = "5";
        /// <summary>
        /// อยู่ระหว่าง ยกเลิก PR
        /// </summary>
        public static string WaitCancel = "6";
    }
}
