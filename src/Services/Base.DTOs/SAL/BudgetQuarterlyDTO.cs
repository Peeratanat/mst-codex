using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class BudgetQuarterlyDTO
    {
        /// <summary>
        /// งบประมาณทั้งหมด Booking
        /// </summary>
        public decimal? TotalBookingBudget { get; set; }
        /// <summary>
        /// งบประมาณที่ใช้แล้ว Booking
        /// </summary>
        public decimal? UsedBookingBudget { get; set; }
        /// <summary>
        /// งบประมาณคงเหลือ Booking
        /// </summary>
        public decimal? RemainBookingBudget { get; set; }
        /// <summary>
        /// ประมาณการใช้ครั้งนี้ Booking
        /// </summary>
        public decimal? CurrentBookingBudget { get; set; }
        /// <summary>
        /// งบประมาณทั้งหมด Transfer
        /// </summary>
        public decimal? TotalTransferBudget { get; set; }
        /// <summary>
        /// งบประมาณที่ใช้แล้ว Transfer
        /// </summary>
        public decimal? UsedTransferBudget { get; set; }
        /// <summary>
        /// งบประมาณคงเหลือ Transfer
        /// </summary>
        public decimal? RemainTransferBudget { get; set; }
        /// <summary>
        /// ประมาณการใช้ครั้งนี้ Transfer
        /// </summary>
        public decimal? CurrentTransferBudget { get; set; }
    }
}
