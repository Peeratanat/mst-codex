
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using Database.Models.SAL;

namespace Base.DTOs.FIN
{
    public class UnitStatusDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public ProjectDTO Project { get; set; }

        /// <summary>
        /// Unit ทั้งหมด
        /// </summary>
        public int TotalUnit { get; set; }

        /// <summary>
        /// แปลงว่าง
        /// </summary>
        public int Unsold { get; set; }

        /// <summary>
        /// จอง
        /// </summary>
        public int Booking { get; set; }

        /// <summary>
        /// สัญญา
        /// </summary>
        public int Agreement { get; set; }

        /// <summary>
        /// ตั้งโอน
        /// </summary>
        public int ReadyToTransfer { get; set; }

        /// <summary>
        /// โอนแล้ว
        /// </summary>
        public int Transferred { get; set; }
    }

    public class UnitStatusQueryResult
    {
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public Booking Booking { get; set; }
        public Agreement Agreement { get; set; }
        public Transfer Transfer { get; set; }
    }

}
