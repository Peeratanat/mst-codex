using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("บัญชีธนาคาร")]
    [Table("BankAccount", Schema = Schema.MASTER)]
    public class BankAccount : BaseEntity
    {
        [Description("ประเภทของคู่บัญชี")]
        public Guid? GLAccountTypeID { get; set; }
        [ForeignKey("GLAccountTypeID")]
        public MST.GLAccountType GLAccountType { get; set; }

        [Description("ประเภท GL => vendor คืนเงินลูกค้า,ทั่วไป,vendor โครงการ,บัญชีพัก,ค่าใช้จ่าย,เงินดาวน์,รายได้")]
        public Guid? GLAccountCategoryMasterCenterID { get; set; }
        [ForeignKey("GLAccountCategoryMasterCenterID")]
        public MST.MasterCenter GLAccountCategory { get; set; }

        //[Description("ประเภทของคู่บัญชี")]
        //public Guid? GLAccountTypeID { get; set; }
        //[ForeignKey("GLAccountTypeID")]
        //public MST.GLAccountType GLAccountType { get; set; }

        [Description("ชื่อบัญชี")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Description("บริษัท")]
        public Guid? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("ธนาคาร")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("จังหวัด")]
        public Guid? ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public MST.Province Province { get; set; }

        [Description("สาขาธนาคาร")]
        public Guid? BankBranchID { get; set; }
        [ForeignKey("BankBranchID")]
        public MST.BankBranch BankBranch { get; set; }

        [Description("ประเภทบัญชี")]
        public Guid? BankAccountTypeMasterCenterID { get; set; }
        [ForeignKey("BankAccountTypeMasterCenterID")]
        public MST.MasterCenter BankAccountType { get; set; }

        [Description("เลขที่บัญชี")]
        [MaxLength(100)]
        public string BankAccountNo { get; set; }
        [Description("เลขที่บัญชี GL")]
        [MaxLength(100)]
        public string GLAccountNo { get; set; }
        [Description("เลข Running No GL")]
        [MaxLength(50)]
        public string GLRefCode { get; set; }

        [Description("บัญชีเงินโอนผ่านธนาคาร")]
        public bool IsTransferAccount { get; set; }
        [Description("บัญชี Direct Debit")]
        public bool IsDirectDebit { get; set; }
        [Description("บัญชี Direct Credit")]
        public bool IsDirectCredit { get; set; }
        [Description("บัญชีนำฝาก")]
        public bool IsDepositAccount { get; set; }
        [Description("P.Card กระทรวงการคลัง")]
        public bool IsPCard { get; set; }
        [Description("รหัส GL Account ของ P.Card")]
        public string PCardGLAccountNo { get; set; }
        [Description("เป็นบัญชีเงินโอนสำหรับ ลูกค้า ตปท")]
        public bool IsForeignTransfer { get; set; }
        [Description("เป็นบัญชีเงินโอน QR Code")]
        public bool IsQRCode { get; set; }

        [Description("Service Code สำหรับ Direct Debit")]
        [MaxLength(100)]
        public string DRServiceCode { get; set; }

        [Description("Service Code")]
        [MaxLength(100)]
        public string ServiceCode { get; set; }
        [Description("Merchant ID")]
        [MaxLength(100)]
        public string MerchantID { get; set; }
        [Description("สถานะ Acitve")]
        public bool IsActive { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("ภาษีมูลค่าเพิ่ม")]
        public bool HasVat { get; set; }

        [Description("TaxCode ใช้สำหรับตอน Post GL")]
        [MaxLength(50)]
        public string TaxCode { get; set; }

        [Description("ชื่อย่อ บัญชีบริษัทสำหรับแสดงในการเรียกใช้งาน")]
        [MaxLength(100)]
        public string DisplayName { get; set; }


        [Description("บัญชีนี้สำหรับ BillPayment")]
        public bool IsBillPayment { get; set; }

        [Description("Biller ID สำหรับการรับเงิน Bill Payment")]
        [MaxLength(50)]
        public string BillerID { get; set; }

        [Description("รหัส com สำหรับการรับเงิน Bill Payment")]
        [MaxLength(50)]
        public string CompanyCode { get; set; }

        [Description("รหัส MerchantID สำหรับการรับเงินผ่าน Mobile")]
        [MaxLength(50)]
        public string MobileMerchantID { get; set; }        

        public string BankAddressEng { get; set; }
    }
}
