using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class AgreementInstallmentDTO
    {
        //Installment
        /// <summary>
        /// ผ่อนค่าอะไร
        /// </summary>
        public string InstallmentOfUnitPriceItem { get; set; }

        /// <summary>
        /// งวดที่
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// เป็นงวดพิเศษ
        /// </summary>
        public bool IsSpecialInstallment { get; set; }

        /// <summary>
        /// กำหนดชำระ
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// วันที่ชำระ
        /// </summary>
        public DateTime? PayDate { get; set; }

        /// <summary>
        /// จำนวนเงินที่ต้องชำระ
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// เงินชำระมาแล้ว
        /// </summary>
        public decimal PaidAmount { get; set; }


        /// <summary>
        /// จำนวนเงินคงเหลือ
        /// </summary>
        public decimal RemainAmount { get; set; }

    }
}
