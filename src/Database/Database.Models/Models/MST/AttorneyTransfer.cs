using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ผู้รับมอบอำนาจ ในเอกสารโอน")]
    [Table("AttorneyTransfer", Schema = Schema.MASTER)]
    public class AttorneyTransfer : BaseEntity
    {
        [Description("ลำดับ")]
        public int Seqn_No { get; set; }
        [Description("ชื่อ - นามสกุล")]
        [MaxLength(250)]
        public string Atty_FullName { get; set; }
        [Description("รหัสพนักงาน")]
        [MaxLength(20)]
        public string Atty_EmployeeCode { get; set; }
        [Description("วันเกิด")]
        public DateTime Atty_DateOfBirth { get; set; }
        [Description("อายุ")]
        public int Atty_Ages { get; set; }
        [Description("สัญชาติ")]
        [MaxLength(100)]
        public string Atty_Nationality { get; set; }
        [Description("ชื่อบิดา - มารดา")]
        [MaxLength(250)]
        public string Atty_Parent { get; set; }
        [Description("ที่อยู่")]
        [MaxLength(1000)]
        public string Atty_Address { get; set; }

        [Description("หมู่บ้าน")]
        [MaxLength(1000)]
        public string Atty_Village { get; set; }
        [Description("บ้านเลขที่")]
        [MaxLength(1000)]
        public string Atty_HouseNo { get; set; }
        [Description("หมู่ที่")]
        [MaxLength(1000)]
        public string Atty_Moo { get; set; }
        [Description("ซอย")]
        [MaxLength(1000)]
        public string Atty_Soi { get; set; }
        [Description("ถนน")]
        [MaxLength(1000)]
        public string Atty_Road { get; set; }
        [Description("ตำบล")]
        [MaxLength(1000)]
        public string Atty_Subdistrict { get; set; }
        [Description("อำเภอ")]
        [MaxLength(1000)]
        public string Atty_District { get; set; }
        [Description("จังหวัด")]
        [MaxLength(1000)]
        public string Atty_Province { get; set; }
    }
}
