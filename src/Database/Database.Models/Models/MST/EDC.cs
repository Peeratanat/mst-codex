using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("เครื่องรูดบัตร")]
    [Table("EDC", Schema = Schema.MASTER)]
    public class EDC : BaseEntity
    {
        [Description("ธนาคาร")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("โครงการที่ใช้งาน")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("รหัสเครื่องรูดบัตร")]
        [MaxLength(100)]
        public string Code { get; set; }
        //Mobile, LAN, EDC
        [Description("ชนิดของเครื่องรูดบัตร (Mobile, LAN, EDC)")]
        public Guid? CardMachineTypeMasterCenterID { get; set; }
        [ForeignKey("CardMachineTypeMasterCenterID")]
        public MST.MasterCenter CardMachineType { get; set; }

        [Description("เบอร์โทรศัพท์ติดต่อ")]
        [MaxLength(100)]
        public string TelNo { get; set; }

        [Description("สถานะเครื่อง")]
        public Guid? CardMachineStatusMasterCenterID { get; set; }
        [ForeignKey("CardMachineStatusMasterCenterID")]
        public MST.MasterCenter CardMachineStatus { get; set; }

        [Description("บริษัท")]
        public Guid? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("บัญชีธนาคาร")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }
        [Description("ผู้รับเครื่อง")]
        [MaxLength(1000)]
        public string ReceiveBy { get; set; }
        [Description("วันที่รับเครื่อง")]
        public DateTime? ReceiveDate { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

    }
}
