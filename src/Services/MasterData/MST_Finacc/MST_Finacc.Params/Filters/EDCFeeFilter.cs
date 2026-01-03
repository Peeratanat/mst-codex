using System;
namespace MST_Finacc.Params.Filters
{
    public class EDCFeeFilter
    {
        public string PaymentCardTypeKey { get; set; }
        public string CreditCardTypeKey { get; set; }
        public string CreditCardPaymentTypeKey { get; set; }
        public double? FeeFrom { get; set; }
        public double? FeeTo { get; set; }
        public bool? IsEDCBankCreditCard { get; set; }
        public Guid? BankID { get; set; }
        public string CreditCardPromotionName { get; set; }
    }
}
