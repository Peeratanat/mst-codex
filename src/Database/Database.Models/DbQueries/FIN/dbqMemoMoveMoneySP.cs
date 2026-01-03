using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqMemoMoveMoneySP : BaseDbQueries
    {
        public Guid? ID {get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }
        public string Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }

        /// <summary>
        /// เงินตั้งพัก
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ วันที่รับเงิน
        /// </summary>
        public DateTime? ReceiveDate { get; set; }


        /// <summary>
        /// ช่องทางการรับเงิน/ประเภทชำระเงิน
        /// </summary>
        public Guid? PaymentMethodID { get; set; }
        public string PaymentMethod { get; set; }

        public Guid? BankID { get; set; }
        public string Bank { get; set; }

        /// <summary>
        /// บัญชีธนาคารที่รับเงินผิด
        /// </summary>
        public Guid? BankAccountID { get; set; }
        public string BankAccount { get; set; }
        //public string BankAccountNo { get; set; }

        /// <summary>
        /// บริษัท ที่สั่งจ่าย/บริษัทที่รับย้ายเงินใน Memo
        /// </summary>
        public Guid? DestinationCompanyID { get; set; }
        public string DestinationCompany { get; set; }

        /// <summary>
        /// บริษัทที่โอนเงินผิด
        /// </summary>
        public Guid? CompanyID { get; set; }
        public string Company { get; set; }

        /// <summary>
        /// วันที่พิมพ์ ล่าสุด
        /// </summary>
        public DateTime? PrintDate { get; set; }
        public bool? PaymentIsCancel { get; set; }

        /// <summary>
        /// ผู้ที่พิมพ์ ล่าสุด
        /// </summary>
        public string PrintBy { get; set; }

        /// <summary>
        /// สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์
        /// </summary>
        public bool? IsPrint { get; set; }

        /// <summary>
        /// วัตถุประสงค์
        /// </summary>
        public Guid? MoveMoneyReasonMasterCenterID { get; set; }
        public string MoveMoneyReason { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        public string PaymentFrom { get; set; }

    }
}
