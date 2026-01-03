using System;
namespace Base.DTOs.SAL
{
    public class SpecialInstallmentDTO
    {
        /// <summary>
        /// งวดที่
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// ค่างวดพิเศษ
        /// </summary>
        public decimal Amount { get; set; }
    }
}
