using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("สาขาธนาคาร")]
    [Table("BankBranch", Schema = Schema.MASTER)]
    public class BankBranch : BaseEntity
    {
        [Description("ธนาคาร")]
        public Guid BankID { get; set; }
        [ForeignKey("BankID")]
        public Bank Bank { get; set; }

        [Description("ชื่อสาขา")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Description("ที่อยู่")]
        [MaxLength(1000)]
        public string Address { get; set; }
        [Description("ตึก")]
        [MaxLength(1000)]
        public string Building { get; set; }
        [Description("ซอย")]
        [MaxLength(1000)]
        public string Soi { get; set; }
        [Description("ถนน")]
        [MaxLength(1000)]
        public string Road { get; set; }

        public Guid? ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public Province Province { get; set; }
        [Description("จังหวัดอื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherProvinceTH { get; set; }

        public Guid? DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public District District { get; set; }
        [Description("อำเภออื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherDistrictTH { get; set; }

        public Guid? SubDistrictID { get; set; }
        [ForeignKey("SubDistrictID")]
        public SubDistrict SubDistrict { get; set; }
        [Description("ตำบลอื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherSubDistrictTH { get; set; }

        [Description("รหัสไปรษณีย์")]
        [MaxLength(50)]
        public string PostalCode { get; set; }
        [Description("เบอร์โทร")]
        [MaxLength(50)]
        public string Telephone { get; set; }
        [Description("เบอร์แฟ๊กซ์")]
        [MaxLength(50)]
        public string Fax { get; set; }
        public bool IsCreditBank { get; set; }
        [Description("บัญชี Direct Debit")]
        public bool IsDirectDebit { get; set; }
        [Description("บัญชี Direct Credit")]
        public bool IsDirectCredit { get; set; }
        [Description("รหัสสาขา")]
        [MaxLength(50)]
        public string AreaCode { get; set; }
        [Description("รหัส ID แบงค์อันเก่า")]
        [MaxLength(50)]
        public string OldBankID { get; set; }
        [Description("รหัสสาขาอันเก่า")]
        [MaxLength(50)]
        public string OldBranchID { get; set; }
        [Description("สถานะ Active")]
        public bool IsActive { get; set; }

    }
}
