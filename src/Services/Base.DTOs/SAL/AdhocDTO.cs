using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class AdhocDTO
    {
        /// <summary>
        /// Adhoc Charge Booking
        /// </summary>
        public decimal? AdhocChargeBooking { get; set; }
        /// <summary>
        /// Adhoc Charge Transfer
        /// </summary>
        public decimal? AdhocChargeTransfer { get; set; }
        /// <summary>
        /// Adhoc Charge Total
        /// </summary>
        public decimal? AdhocChargeTotal { get; set; }
        /// <summary>
        /// ประมาณการใช้ครั้งนี้ Booking
        /// </summary>
        public decimal? UsedBooking { get; set; }
        /// <summary>
        /// ประมาณการใช้ครั้งนี้ Transfer
        /// </summary>
        public decimal? UsedTransfer { get; set; }
        /// <summary>
        /// ประมาณการใช้ครั้งนี้ Total
        /// </summary>
        public decimal? UsedTotal { get; set; }
    }
}
