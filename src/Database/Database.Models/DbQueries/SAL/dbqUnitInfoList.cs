using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqUnitInfoList : BaseDbQueries
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
        /// ชื่อจริง (ผู้จอง/ผู้ทำสัญญา)
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// นามสกุล (ผู้จอง/ผู้ทำสัญญา)
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// ใบจอง
        /// </summary>
        public Guid? BookingID { get; set; }

        /// <summary>
        /// ใบสัญญา
        /// </summary>
        public Guid? AgreementID { get; set; }

        /// <summary>
        /// โปรโอน
        /// </summary>
        public Guid? TransferPromotionID { get; set; }

        /// <summary>
        /// ธนาคารที่ขอสินเชื่อ
        /// </summary>
        public Guid? BankID { get; set; }

        /// <summary>
        /// ธนาคารที่ขอสินเชื่อ
        /// </summary>
        public Guid? CreditBankingID { get; set; }

        /// <summary>
        /// โอนกรรมสิทธิ์
        /// </summary>
        public Guid? TransferID { get; set; }

        /// <summary>
        /// LC ผู้รับผิดชอบ
        /// </summary>
        public Guid? LCOwnerID { get; set; }
    }

    public class dbqUnitInfoListByContact : BaseDbQueries
    {
        public Guid UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string BookingNo { get; set; }
        public string AgreementNo { get; set; }
        public string TransferNo { get; set; }
        public string UnitStatusName { get; set; }
        public Guid TransferID { get; set; }
 
    }

}
