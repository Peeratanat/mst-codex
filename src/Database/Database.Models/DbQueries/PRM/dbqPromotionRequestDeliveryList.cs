using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.PRM
{
    public class dbqPromotionRequestDeliveryList : BaseDbQueries
    {
        /// <summary>
        /// แปลง
        /// </summary>
        public Guid UnitID { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid ProjectID { get; set; }

        /// <summary>
        /// ชื่อผู้ทำสัญญา
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// ใบจอง
        /// </summary>
        public Guid? BookingID { get; set; }

        /// <summary>
        /// ใบสัญญา
        /// </summary>
        public Guid? AgreementID { get; set; }

        /// <summary>
        /// เลขที่โอน
        /// </summary>
        public Guid? TransferID { get; set; }

        /// <summary>
        /// จำนวนโปรขาย
        /// </summary>
        public int? SalePromotionAmount { get; set; }

        /// <summary>
        /// จำนวนโปรโอน
        /// </summary>
        public int? TransferPromotionAmount { get; set; }

        /// <summary>
        /// จำนวนเบิกโปรขาย
        /// </summary>
        public int? SalePromotionRequestAmount { get; set; }

        /// <summary>
        /// จำนวนเบิกโปรโอน
        /// </summary>
        public int? TransferPromotionRequestAmount { get; set; }

        /// <summary>
        /// จำนวนส่งมอบโปรขาย
        /// </summary>
        public int? SalePromotionDeliveryAmount { get; set; }

        /// <summary>
        /// จำนวนส่งมอบโปรโอน
        /// </summary>
        public int? TransferPromotionDeliveryAmount { get; set; }

        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }

        /// <summary>
        /// วันที่โอน
        /// </summary>
        public DateTime? TransferDate { get; set; }
    }
}
