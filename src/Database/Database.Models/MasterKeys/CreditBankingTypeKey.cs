using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class CreditBankingTypeKey
    {
        //โอนสด
        public static string TransferbyCustomer = "1";
        //กู้เอง
        public static string LoanbyCustomer = "2";
        //กู้ผ่านโครงการ
        public static string LoanbyProject = "3";

        //ไม่ตัดสินใจ
        public static string Undecided = "4";

        //รอลูกค้ายกเลิก
        public static string WaitingForcancel = "5";

        //ยังไม่ถึงดิวโอน
        public static string NotDueTransfer = "6";

        //ขายต่อ
        public static string Resell = "7";
        //ขายต่อ (BC)
        public static string ResellBC = "8";
       
    }
}
