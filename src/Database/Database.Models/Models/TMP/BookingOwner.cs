using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.TMP
{
    [Description("ข้อมูลผู้จอง")]
    [Table("BookingOwner", Schema = Schema.TEMP)]
    public class BookingOwner : BaseEntity
    {
        [Description("เลขที่จอง")]
        [MaxLength(50)]
        public string BookingNumber { get; set; }
        [Description("ลำดับ")]
        public int? nOrder { get; set; }
        [Description("รหัสลูกค้า")]
        public string CustomerID { get; set; }
        [Description("สถานะผู้จอง")]
        public bool? IsBooking { get; set; }
        [Description("สถานะผู้ทำสัญญา")]
        public bool? IsContract { get; set; }
        [Description("สถานะคนหลัก")]
        public bool? IsHeader { get; set; }
    }
}
