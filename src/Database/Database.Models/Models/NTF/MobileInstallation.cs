using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.NTF
{
    [Description("ข้อมูลการติดตั้ง App บน Device")]
    [Table("MobileInstallation", Schema = Schema.NOTIFICATION)]
    public class MobileInstallation : BaseEntity
    {
        [Description("ผู้ใช้ที่รับแจ้งเตือน")]
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public USR.User User { get; set; }

        [Description("ID ของ Mobile App ที่ติดตั้งใน Device")]
        [MaxLength(100)]
        public string InstallationID { get; set; }
        [Description("ระบบปฏิบัติการ")]
        public MobileDeviceType DeviceType { get; set; }

    }
}
