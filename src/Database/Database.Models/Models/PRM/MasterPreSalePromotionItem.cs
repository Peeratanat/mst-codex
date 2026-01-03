using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Master รายการโปรโมชั่นก่อนขาย")]
    [Table("MasterPreSalePromotionItem", Schema = Schema.PROMOTION)]
    public class MasterPreSalePromotionItem : BaseEntity
    {
        public Guid MasterPreSalePromotionID { get; set; }
        [ForeignKey("MasterPreSalePromotionID")]
        public MasterPreSalePromotion MasterPreSalePromotion { get; set; }

        [Description("ชื่อผลิตภัณฑ์ (TH)")]
        [MaxLength(1000)]
        public string NameTH { get; set; }
        [Description("ชื่อผลิตภัณฑ์ (EN)")]
        [MaxLength(1000)]
        public string NameEN { get; set; }
        [Description("จำนวน")]
        public int Quantity { get; set; }
        [Description("หน่วย (TH)")]
        [MaxLength(100)]
        public string UnitTH { get; set; }
        [Description("หน่วย (EN)")]
        [MaxLength(100)]
        public string UnitEN { get; set; }
        [Description("ราคาต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal PricePerUnit { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("วันที่ได้รับ")]
        public int? ReceiveDays { get; set; }
        [Description("ลูกค้าได้รับเมื่อ?")]
        public Guid? WhenPromotionReceiveMasterCenterID { get; set; }
        [ForeignKey("WhenPromotionReceiveMasterCenterID")]
        public MST.MasterCenter WhenPromotionReceive { get; set; }
        [Description("การจัดซื้อ?")]
        public bool IsPurchasing { get; set; }
        [Description("แสดงในสัญญา")]
        public bool IsShowInContract { get; set; }
        [Description("สถานะ")]
        public Guid? PromotionItemStatusMasterCenterID { get; set; }
        [ForeignKey("PromotionItemStatusMasterCenterID")]
        public MST.MasterCenter PromotionItemStatus { get; set; }
        [Description("วันเริ่มต้นใช้งาน")]
        public DateTime? StartDate { get; set; }
        [Description("วันที่หมดอายุ")]
        public DateTime? ExpireDate { get; set; }

        [Description("ID ของ Promotion หลัก (กรณี Item นี้เป็น Promotion ย่อย)")]
        public Guid? MainPromotionItemID { get; set; }

        [Description("สร้างมาจาก Material ไหน")]
        public Guid? PromotionMaterialItemID { get; set; }
        [ForeignKey("PromotionMaterialItemID")]
        public PromotionMaterialItem PromotionMaterialItem { get; set; }

        [Description("ลำดับของ Item")]
        public int Order { get; set; }
        [Description("รหัส Item")]
        public string PromotionItemNo { get; set; }

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

        [Description("Plant")]
        [MaxLength(50)]
        public string Plant { get; set; }
        [Description("Plant")]
        [MaxLength(50)]
        public string SAPCompanyID { get; set; }
        [Description("Agreement No.")]
        [MaxLength(100)]
        public string AgreementNo { get; set; }
        [Description("เลขที่สิ่งของ")]
        [MaxLength(100)]
        public string ItemNo { get; set; }
        [Description("SAP Purchasing Organization (EKORG)")]
        [MaxLength(50)]
        public string SAPPurchasingOrg { get; set; }
        [Description("Material Group Key")]
        [MaxLength(100)]
        public string MaterialGroupKey { get; set; }
        [Description("Document Type")]
        [MaxLength(50)]
        public string DocType { get; set; }
        [Description("G/L Account Number (KONTS)")]
        [MaxLength(50)]
        public string GLAccountNo { get; set; }
        [Description("ราคา (หลัง VAT)")]
        [Column(TypeName = "Money")]
        public decimal MaterialPrice { get; set; }
        [Description("ราคา (ก่อน VAT)")]
        [Column(TypeName = "Money")]
        public decimal MaterialBasePrice { get; set; }
        [Description("Vat (%)")]
        public double Vat { get; set; }
        [Description("SAP Sale Tax Code (SLTAX), PromotionVatRate.Code")]
        [MaxLength(50)]
        public string SAPSaleTaxCode { get; set; }
        [Description("SAP Base Unit of Measure (MEINS)")]
        [MaxLength(50)]
        public string SAPBaseUnit { get; set; }
        [Description("Account Number of Vendor or Creditor (LIFNR)")]
        [MaxLength(50)]
        public string SAPVendor { get; set; }
        [Description("SAP Purchasing Group (EKGRP)")]
        [MaxLength(50)]
        public string SAPPurchasingGroup { get; set; }
        [Description("สถานะการนำไปใช้งาน")]
        public bool IsUsed { get; set; }
        [Description("วันที่ถูกนำไปใช้งาน")]
        public DateTime? UsedDate { get; set; }
        [Description("สถานะ Active")]
        public bool IsActive { get; set; }
        [Description("Material Code")]
        [MaxLength(100)]
        public string MaterialCode { get; set; }
        [Description("Material Code")]
        [MaxLength(1000)]
        public string MaterialName { get; set; }

    }
}
