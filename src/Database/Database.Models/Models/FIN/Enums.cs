using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.FIN
{
    public enum PaymentMethodType
    {
        CustomerWallet = 0,
        Cash = 1,
        CreditCard = 2,
        PersonalCheque = 3,
        CashierCheque = 4,
        BankTransfer = 5,
        QRCode = 6,
        ForeignBankTransfer  = 7,
        DirectCreditDebit = 8,
        BillPayment = 9
    }



}
