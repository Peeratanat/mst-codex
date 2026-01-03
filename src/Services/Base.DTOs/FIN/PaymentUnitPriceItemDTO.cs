using System;

namespace Base.DTOs.FIN
{
    /// <summary>
    /// รายการรับชำระเงิน
    /// Model: UnitPriceItem
    /// </summary>
    public class PaymentUnitPriceItemDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อรายการ
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// จำนวนเงินที่ต้องชำระ
        /// </summary>
        public decimal ItemAmount { get; set; }

        /// <summary>
        /// จำนวนเงินที่ชำระแล้ว
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// วันที่ชำระ (Max ReceiveDate)
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// จำนวนเงินคงเหลือ
        /// </summary>
        public decimal RemainAmount { get; set; }

        /// <summary>
        /// เงินที่จะชำระ
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Master Price
        /// </summary>
        public MST.MasterPriceItemDTO MasterPriceItem { get; set; }

        /// <summary>
        /// งวดที่
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// กำหนดชำระ
        /// </summary>
        public DateTime? DueDate { get; set; }

        public Guid? UnitPriceID { get; set; }

        /// <summary>
        /// เงินที่ต้องจ่าย
        /// </summary>
        public decimal AmountToPaid { get; set; }


        public bool? IsPaidAll { get; set; }

        public bool? IsPaidPartial { get; set; }

        public Guid? PaymentMethodTypeID { get; set; }

        public string PaymentMethodKey { get; set; }
  
        public string PaymentMethodName { get; set; }
    }

}
