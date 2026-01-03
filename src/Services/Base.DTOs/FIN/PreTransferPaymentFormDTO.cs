using Base.DTOs.MST;
using System;
using System.Collections.Generic;

namespace Base.DTOs.FIN
{
    public class PreTransferPaymentFormDTO
    {
        /// <summary>
        /// วันที่ชำระ
        /// </summary>
        public DateTime ReceiveDate { get; set; }

        public bool IsFromLC { get; set; } = false;
        public FileDTO AttachFile { get; set; }

        public List<PreTransferPayments> PaymentForms { get; set; }
    }

    public class PreTransferPayments
    {
        /// <summary>
        /// วิธีการชำระ
        /// </summary>
        public PaymentMethodDTO PaymentMethod { get; set; }

        /// <summary>
        /// รายการที่ต้องชำระ
        /// </summary>
        public List<PreTransferPaymentItem> PaymentItems { get; set; }
    }

    public class PreTransferPaymentItem
    {
        public MasterPriceItemDTO MasterPriceItem { get; set; }

        public decimal PaidAmount { get; set; }
    }
}