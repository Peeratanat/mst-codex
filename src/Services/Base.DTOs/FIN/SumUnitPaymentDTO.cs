
namespace Base.DTOs.FIN
{
    public class SumUnitPaymentDTO
    {
        /// <summary>
        /// รวมเงินชำระ
        /// </summary>
        public decimal SumPayment { get; set; }

        /// <summary>
        /// จำนวนเงินที่ค้างชำระ
        /// </summary>
        public decimal SumWaitingForPayment { get; set; }

        /// <summary>
        /// จำนวนเงินที่ค้างชำระ Overdue
        /// </summary>
        public decimal SumOverdueBalance { get; set; }

        /// <summary>
        /// จำนวนงวดที่ค้างชำระ
        /// </summary>
        public int SumOverduePeriod { get; set; }

        /// <summary>
        /// เงินทราบผู้โอน
        /// </summary>
        public decimal OtherPayment { get; set; }
    }

}
