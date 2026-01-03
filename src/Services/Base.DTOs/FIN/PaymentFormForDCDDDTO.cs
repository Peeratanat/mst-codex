using Base.DTOs.MST;
using System;
using System.ComponentModel;

namespace Base.DTOs.FIN
{
    public class PaymentFormForDCDDDTO
    {
        public PaymentFormType PaymentFormType { get; set; }

        public Guid PaymentID { get; set; } = Guid.NewGuid();

        public Guid BookingID { get; set; }

        /// <summary>
        /// วิธีการชำระ
        /// </summary>
        public PaymentMethodDTO PaymentMethod { get; set; }

        /// <summary>
        /// หมายเหตุ (บันทึกข้อความ)
        /// </summary>
        public string Remark { get; set; }

        public Guid? RefID { get; set; }
    }
}
