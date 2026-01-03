using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("การนำเข้า Bill Payment จากธนาคาร")]
    [Table("BillPayment", Schema = Schema.FINANCE)]
    public class BillPayment : BaseEntity
    {
        [Description("รหัสนำเข้า")]
        [MaxLength(50)]
        public string BatchID { get; set; }

        [Description("ID บัญชีบริษัท")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        [Description("ชื่อ Text file ที่ Import")]
        [MaxLength(100)]
        public string ImportFileName { get; set; }

        [Description("Paht ที่เก็บ file บน MinIO")]
        [MaxLength(100)]
        public string FilePath { get; set; }

        //[Description("วันที่-เวลา Import File")]//ใช้ CreateDate
        //public DateTime? ImportDate { get; set; }

        [Description("วิธีการ Import Bill Payment")]
        public Guid? BillPaymentImportTypeMasterCenterID { get; set; }
        [ForeignKey("BillPaymentImportTypeMasterCenterID")]
        public MST.MasterCenter BillPaymentImportType { get; set; }

        [Description("จำนวนเงินรวม")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        [Description("รวมจำนวนรายการ")]
        public int TotalRecord { get; set; }

        [Description("รวมจำนวนรายการรอการยืนยันข้อมูล")]
        public int TotalInprogressRecord { get; set; }

        [Description("รวมจำนวนรายการยืนยันข้อมูลเรียบร้อย")]
        public int TotalSuccessRecord { get; set; }

        [Description("วันที่เงินเข้า/วันที่ชำระเงิน")]
        public DateTime ReceiveDate { get; set; }
    }
}
