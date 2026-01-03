using System;
namespace Database.Models.MasterKeys
{
    public class BillPaymentStatusKey
    {
        public static string Wait = "Wait";
        public static string Complete = "Complete";
        public static string NotFound = "NotFound";
        public static string UnitTransfered = "UnitTransfered";
        public static string UnitCancel = "UnitCancel";
        public static string Duplicate = "Duplicate";
        public static string Cancel = "Cancel";
        public static string Split = "Split";
        public static string CheckDupPayment = "CheckDupPayment";
        public static string OverPayment = "OverPayment";
        public static string RefConflict = "RefConflict";
        public static string CancelAgreement = "CancelAgreement";
        public static string CTMLetter = "CTMLetter";
    }
}
