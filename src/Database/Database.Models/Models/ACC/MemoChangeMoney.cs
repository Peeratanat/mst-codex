using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("Memo คืนเงินลูกค้าหลังโอนกรรมสิทธิ์")]
    [Table("MemoChangeMoney", Schema = Schema.ACCOUNT)]
    public class MemoChangeMoney : BaseEntity
    {
        [Description("อ้างอิงข้อมูลการโอนกรรมสิทธิ์ ที่บัญชีอนุมัติ แล้ว")]
        public Guid? BookingID { get; set; }

        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("วันที่คืนเงินลูกค้า")]
        public DateTime? ChangeDate { get; set; }

        [Description("Lock วันที่คืนเงินลูกค้า")]
        public bool IsLock { get; set; }


        [Description("ID บริษัท ที่สั่งจ่าย")]
        public Guid CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("วัตถุประสงค์")]
        public Guid MoveMoneyReasonMasterCenterID { get; set; }
        [ForeignKey("MoveMoneyReasonMasterCenterID")]
        public MST.MasterCenter MoveMoneyReason { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์")]
        public bool IsPrint { get; set; }

        [Description("ผู้ที่พิมพ์ ล่าสุด")]
        public USR.User PrintBy { get; set; }

        [Description("วันที่พิมพ์ ล่าสุด")]
        public DateTime? PrintDate { get; set; }

        [Description("บัญชีธนาคารที่โอนผิด")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }
    }
}
