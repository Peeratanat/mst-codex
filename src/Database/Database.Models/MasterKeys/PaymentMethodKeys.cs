using System.Collections.Generic;

namespace Database.Models
{
    public static class PaymentMethodKeys
    {
        /// <summary>
        /// เงินสด
        /// </summary>
        public static string Cash = "Cash";
        /// <summary>
        /// แคชเชียร์เช็ค
        /// </summary>
        public static string CashierCheque = "CashierCheque";
        /// <summary>
        /// บัตรเครดิต
        /// </summary>
        public static string CreditCard = "CreditCard";
        /// <summary>
        /// เช็คส่วนตัว
        /// </summary>
        public static string PersonalCheque = "PersonalCheque";
        /// <summary>
        /// เช็ค(ล่วงหน้า)
        /// </summary>
        public static string PostDateCheque = "PostDateCheque";
        /// <summary>
        /// โอนผ่านธนาคาร
        /// </summary>
        public static string BankTransfer = "BankTransfer";
        /// <summary>
        /// การ์ดลูกหนี้
        /// </summary>
        public static string BillPayment = "BillPayment";
        /// <summary>
        /// หักผ่านบัญชี
        /// </summary>
        public static string DirectDebit = "DirectDebit";
        /// <summary>
        /// หักผ่านบัตรเครดิต
        /// </summary>
        public static string DirectCredit = "DirectCredit";
        /// <summary>
        /// เงินจากสัญญาเดิม
        /// </summary>
        public static string ChangeContract = "OldContract";
        /// <summary>
        /// เงินพักลูกค้า
        /// </summary>
        public static string CustomerWallet = "CustomerWallet";
        /// <summary>
        /// เงินโอนต่างประเทศ
        /// </summary>
        public static string ForeignBankTransfer = "FBankTransfer";
        /// <summary>
        /// บัตรเดบิต
        /// </summary>
        public static string DebitCard = "DebitCard";
        /// <summary>
        /// QR Code
        /// </summary>
        public static string QRCode = "QR";
        /// <summary>
        /// OnlinePayment
        /// </summary>
        public static string OnlinePayment = "OnlinePayment";
        /// <summary>
        /// UnknowPayment
        /// </summary>
        public static string UnknowPayment = "Unknown";

        /// <summary>
        /// นำฝาก
        /// </summary>
        public static string Deposit = "Deposit";

        /// <summary>
        /// การชำระเงินที่ต้องนำฝาก
        /// </summary>
        public static List<string> NeedToDepositKeys = new List<string> 
        { 
            PaymentMethodKeys.Cash,
            PaymentMethodKeys.CreditCard,
            PaymentMethodKeys.DebitCard,
            PaymentMethodKeys.BankTransfer,
            PaymentMethodKeys.ForeignBankTransfer,
            PaymentMethodKeys.PersonalCheque,
            PaymentMethodKeys.CashierCheque,
            PaymentMethodKeys.QRCode,
            PaymentMethodKeys.OnlinePayment
        };

        public static List<string> PostGLMethodCash = new List<string>
        {
            PaymentMethodKeys.Cash
        };

        public static List<string> PostGLMethodCheque = new List<string>
        {
            PaymentMethodKeys.CashierCheque,
            PaymentMethodKeys.PersonalCheque,
            PaymentMethodKeys.PostDateCheque
        };

        public static List<string> PostGLMethodCreditCard = new List<string>
        {
            PaymentMethodKeys.DebitCard,
            PaymentMethodKeys.CreditCard
        };

        public static List<string> PostGLMethodUnknowPayment = new List<string>
        {
            PaymentMethodKeys.UnknowPayment
        };

        public static List<string> PostGLMethodTransfer = new List<string>
        {
            PaymentMethodKeys.BillPayment,
            PaymentMethodKeys.DirectCredit,
            PaymentMethodKeys.DirectDebit,
            PaymentMethodKeys.BankTransfer,
            PaymentMethodKeys.ForeignBankTransfer,
            PaymentMethodKeys.QRCode,
            PaymentMethodKeys.OnlinePayment
        };


        public static List<string> IsDepositMethodType = new List<string>
        {
            PaymentMethodKeys.Cash,
            PaymentMethodKeys.CashierCheque,
            PaymentMethodKeys.CreditCard,
            PaymentMethodKeys.PersonalCheque,
            PaymentMethodKeys.PostDateCheque,
            PaymentMethodKeys.BankTransfer,
            PaymentMethodKeys.DebitCard,
            PaymentMethodKeys.ForeignBankTransfer,
            PaymentMethodKeys.QRCode,
            PaymentMethodKeys.OnlinePayment
        };
    }
}
