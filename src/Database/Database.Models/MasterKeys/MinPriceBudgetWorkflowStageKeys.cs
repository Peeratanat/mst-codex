using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public static class MinPriceBudgetWorkflowStageKeys
    {
        // จอง
        public static string Booking = "1";
        // จอง-ย้ายแปลง
        public static string ChangeUnitBooking = "2";
        // สัญญา
        public static string Contract = "3";
        // สัญญา-ย้ายแปลง
        public static string ChangeUnitContract = "4";
        // โปรโมชั่นโอน
        public static string PromotionTransfer = "5";
    }
}
