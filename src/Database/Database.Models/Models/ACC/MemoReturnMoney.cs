using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("Memo คืนเงินลูกค้ายกเลิก")]
    [Table("MemoReturnMoney", Schema = Schema.ACCOUNT)]
    public class MemoReturnMoney : BaseEntity
    {
        [Description("อ้างอิงข้อมูลการจอง,สัญญา ที่บันทึกยกเลิกแล้ว")]
        public Guid? BookingID { get; set; }

        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("วันที่นำจ่ายเงินคืนลูกค้า")]
        public DateTime? ReturnDate { get; set; }

        [Description("สถานะนำจ่ายเงินคืนลูกค้า 1=คืนแล้ว  0=รอคืน")]
        public bool IsReturnComplete { get; set; }

        [Description("ID บริษัท ที่สั่งจ่าย")]
        public Guid CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }
        
        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์")]
        public bool IsPrint { get; set; }

        [Description("ผู้ที่พิมพ์ ล่าสุด")]
        public USR.User PrintBy { get; set; }

        [Description("วันที่พิมพ์ ล่าสุด")]
        public DateTime? PrintDate { get; set; }
    }
}
