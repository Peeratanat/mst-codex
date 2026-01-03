using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("Material Item ใน Agreement")]
    [Table("PromotionMaterialItem", Schema = Schema.PROMOTION)]
    public class PromotionMaterialItem : BaseEntity
    {
        [Description("Agreement No.")]
        [MaxLength(100)]
        public string AgreementNo { get; set; }
        [Description("เลขที่สิ่งของ")]
        [MaxLength(100)]
        public string ItemNo { get; set; }
        [Description("Plant")]
        [MaxLength(50)]
        public string Plant { get; set; }
        [Description("ชื่อสิ่งของ (ภาษาไทย)")]
        [MaxLength(1000)]
        public string NameTH { get; set; }
        [Description("ชื่อสิ่งของ (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string NameEN { get; set; }

        [Description("Material Code")]
        [MaxLength(100)]
        public string MaterialCode { get; set; }
        [Description("Material Group Key")]
        [MaxLength(100)]
        public string MaterialGroupKey { get; set; }
        [Description("G/L Account Number (KONTS)")]
        [MaxLength(50)]
        public string GLAccountNo { get; set; }
        [Description("Master Material")]
        public Guid? PromotionMaterialID { get; set; }
        [ForeignKey("PromotionMaterialID")]
        public PromotionMaterial PromotionMaterial { get; set; }

        [Description("Brand (EN)")]
        [MaxLength(1000)]
        public string BrandEN { get; set; }
        [Description("Spec (EN)")]
        [MaxLength(5000)]
        public string SpecEN { get; set; }
        [Description("Remark (EN)")]
        [MaxLength(5000)]
        public string RemarkEN { get; set; }
        [Description("Brand (TH)")]
        [MaxLength(1000)]
        public string BrandTH { get; set; }
        [Description("Spec (TH)")]
        [MaxLength(5000)]
        public string SpecTH { get; set; }
        [Description("Remark (TH)")]
        [MaxLength(5000)]
        public string RemarkTH { get; set; }

        [Description("ราคา (หลัง VAT)")]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        [Description("ราคา (ก่อน VAT)")]
        [Column(TypeName = "Money")]
        public decimal BasePrice { get; set; }
        [Description("Vat (%)")]
        public double Vat { get; set; }
        [Description("หน่วย EN")]
        [MaxLength(1000)]
        public string UnitEN { get; set; }
        [Description("หน่วย TH")]
        [MaxLength(1000)]
        public string UnitTH { get; set; }

        [Description("สถานะ Active")]
        public bool IsActive { get; set; }
        [Description("สถานะ Material Item")]
        public Guid? MaterialItemStatusMasterCenterID { get; set; }
        [ForeignKey("MaterialItemStatusMasterCenterID")]
        public MST.MasterCenter MaterialItemStatus { get; set; }

        [Description("ลูกค้าจะได้รับเมื่อ?")]
        public Guid? WhenPromotionReceiveMasterCenterID { get; set; }
        [ForeignKey("WhenPromotionReceiveMasterCenterID")]
        public MST.MasterCenter WhenPromotionReceive { get; set; }
        [Description("การจัดซื้อ?")]
        public bool IsPurchasing { get; set; }
        [Description("แสดงในสัญญา")]
        public bool IsShowInContract { get; set; }

        [Description("รหัสบริษัทใน SAP")]
        [MaxLength(50)]
        public string SAPCompanyID { get; set; }
        [Description("SAP Purchasing Organization (EKORG)")]
        [MaxLength(50)]
        public string SAPPurchasingOrg { get; set; }
        [Description("SAP Purchasing Group (EKGRP)")]
        [MaxLength(50)]
        public string SAPPurchasingGroup { get; set; }
        [Description("SAP Base Unit of Measure (MEINS)")]
        [MaxLength(50)]
        public string SAPBaseUnit { get; set; }
        [Description("Account Number of Vendor or Creditor (LIFNR)")]
        [MaxLength(50)]
        public string SAPVendor { get; set; }
        [Description("EBELN + EBELP")]
        [MaxLength(50)]
        public string SAPVarKey { get; set; }
        [Description("SAP Sale Tax Code (SLTAX), PromotionVatRate.Code")]
        [MaxLength(50)]
        public string SAPSaleTaxCode { get; set; }
        [Description("Terms of Payment Key (ZTERM)")]
        [MaxLength(50)]
        public string SAPTermPaymentKey { get; set; }
        [Description("Deletion Indicator in Purchasing Document (LOEKZ)")]
        [MaxLength(50)]
        public string SAPDeleteIndicator { get; set; }
        [Description("Condition record number (KNUMH)")]
        [MaxLength(50)]
        public string SAPConditionRecordNo { get; set; }
        [Description("SAP Created Time")]
        public DateTime? SAPCreatedTime { get; set; }
        [Description("Document Type")]
        [MaxLength(50)]
        public string DocType { get; set; }

        [Description("วันเริ่มต้นใช้งาน")]
        public DateTime? StartDate { get; set; }
        [Description("วันหมดอายุ")]
        public DateTime? ExpireDate { get; set; }

        [Description("สถานะว่าต้องทำ Auto PR เมื่อให้ลูกค้า")]
        public bool IsAutoPR { get; set; }
        [Description("สถานะข้อมูล (1: Active, 0:Non)")]
        public bool IsUseMasterInPR { get; set; }

        [Description("Agreement Expire Date")]
        public DateTime? AgreementExpireDate { get; set; }
    }
}
