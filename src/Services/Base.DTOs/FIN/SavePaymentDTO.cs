using Database.Models.FIN;
using System.Collections.Generic;

namespace Base.DTOs.FIN
{
    public class SavePaymentDTO
    {
        public Payment Payment { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<PaymentItem> PaymentItems { get; set; }
        public List<PaymentMethodToItem> PaymentMethodToItems { get; set; }

        public ReceiptTempHeader ReceiptTempHeader { get; set; }
        public List<ReceiptTempDetail> ReceiptTempDetails { get; set; }

        public string ErrMsg { get; set; }
    }
}
