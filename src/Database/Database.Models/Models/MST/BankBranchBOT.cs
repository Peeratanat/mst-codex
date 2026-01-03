using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("สาขาธนาคาร แบงค์ชาติ")]
    [Table("BankBranchBOT", Schema = Schema.MASTER)]
    public class BankBranchBOT : BaseEntity
    {
        [Description("ธนาคาร")]
        public Guid BankID { get; set; }
        [ForeignKey("BankID")]
        public Bank Bank { get; set; }

        [Description("รหัสสถาบันการเงิน")]
        [MaxLength(10)]
        public string BankCode { get; set; }

        [Description("ชื่อสถาบันการเงิน")]
        [MaxLength(1000)]
        public string BankName { get; set; }

        [Description("รหัสจุดให้บริการ")]
        [MaxLength(50)]
        public string BankBranchCode { get; set; }

        [Description("รหัสสาขา")]
        [MaxLength(5)]
        public string BankServiceCode { get; set; }

        [Description("ชื่อสาขา")]
        [MaxLength(1000)]
        public string BankBranchName { get; set; }

        [Description("ชื่อสาขา EN")]
        [MaxLength(1000)]
        public string BankBranchNameEN { get; set; }

        [Description("เลขที่")]
        [MaxLength(500)]
        public string AddressNo { get; set; }

        [Description("อาคาร")]
        [MaxLength(1000)]
        public string AddressBuilding { get; set; }

        [Description("หมู่")]
        [MaxLength(50)]
        public string AddressMoo { get; set; }

        [Description("ซอย")]
        [MaxLength(1000)]
        public string AddressSoi { get; set; }

        [Description("ถนน")]
        [MaxLength(1000)]
        public string AddressRoad { get; set; }

        [Description("ตำบล/แขวง")]
        [MaxLength(1000)]
        public string AddressSubDistrict { get; set; }

        [Description("อำเภอ/เขต")]
        [MaxLength(1000)]
        public string AddressDistrict { get; set; }

        [Description("จังหวัด")]
        [MaxLength(1000)]
        public string AddressProvince { get; set; }

        [Description("รหัสไปรษณีย์")]
        [MaxLength(1000)]
        public string AddressPostCode { get; set; }

        [Description("เบอร์โทรศัพท์")]
        [MaxLength(1000)]
        public string AddressTelNo { get; set; }

        [Description("วันที่เปิดสาขา/จุดให้บริการ")]
        [MaxLength(1000)]
        public string BankBranchStartDate { get; set; }

        [Description("วันทำการ")]
        [MaxLength(1000)]
        public string BankBranchWorkingDate { get; set; }

        [Description("วันทำการอื่น ๆ")]
        [MaxLength(1000)]
        public string BankBranchWorkingOtherDate { get; set; }

        [Description("เวลาทำการ")]
        [MaxLength(1000)]
        public string BankBranchWorkingTime { get; set; }

        [Description("เวลาทำการอื่น ๆ")]
        [MaxLength(1000)]
        public string BankBranchWorkingOtherTime { get; set; }
    }
}

