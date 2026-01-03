using System;
using System.Collections.Generic;

namespace Base.DTOs.PRJ
{
    public class BudgetMinPriceTransferDTO
    {
        /// <summary>
        /// Total Budget
        /// </summary>
        public List<BudgetMinPriceDTO> BudgetMinPrices { get; set; }
        /// <summary>
        /// Total Budget
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// จำนวนข้อมูลที่ถูก
        /// </summary>
        public int TotalSuccess { get; set; }
        /// <summary>
        /// จำนวนข้อมูลที่ผิด
        /// </summary>
        public int TotalError { get; set; }

        /// <summary>
        /// Msg Error
        /// </summary>
        public ChkErrorDTO ChkError { get; set; }
    }
}
