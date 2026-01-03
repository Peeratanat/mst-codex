using System;
namespace Base.DTOs.FIN
{
    /// <summary>
    /// ช่องทางการชำระเงิน
    /// </summary>
    public class PaymentMethodListDTO
    {
        /// <summary>
        /// ชนิดของช่องทางชำระ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PaymentMethod
        /// </summary>
        public MST.MasterCenterDropdownDTO PaymentMethodType { get; set; }

        /// <summary>
        /// จำนวนเงินที่จ่าย
        /// </summary>
        public decimal PayAmount { get; set; }
    }
}
