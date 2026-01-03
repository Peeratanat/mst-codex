using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("SAP ZRFCMM02 - Agreement")]
    [Table("SAP_ZRFCMM02", Schema = Schema.PROMOTION)]
    public class SAP_ZRFCMM02 : BaseEntityWithoutKey
    {
        [Description("Company Code (1000)")]
        [MaxLength(4)]
        public string BUKRS { get; set; }
        [Description("Account Number of Vendor or Creditor (107494)")]
        [MaxLength(10)]
        public string LIFNR { get; set; }
        [Description("Purchasing Organization (HO02)")]
        [MaxLength(4)]
        public string EKORG { get; set; }
        [Description("Purchasing Group (P04)")]
        [MaxLength(3)]
        public string EKGRP { get; set; }
        [Description("Terms of Payment Key (Z001)")]
        [MaxLength(4)]
        public string ZTERM { get; set; }
        [Description("Start of Validity Period (20190423)")]
        [MaxLength(8)]
        public string KDATB { get; set; }
        [Description("End of Validity Period (20200430)")]
        [MaxLength(8)]
        public string KDATE { get; set; }
        [Description("Plant (1001)")]
        [MaxLength(4)]
        public string WERKS { get; set; }
        [Description("Purchasing Document Number (3900078875)")]
        [MaxLength(10)]
        public string EBELN { get; set; }
        [Description("Item Number of Purchasing Document (00010)")]
        [MaxLength(5)]
        public string EBELP { get; set; }
        [Description("Material Number (71701-000000)")]
        [MaxLength(18)]
        public string MATNR { get; set; }
        [Description("Material Description (ค่าวัสดุวอลล์เปเปอร์ แปลง D06)")]
        [MaxLength(100)]
        public string MAKTX { get; set; }
        [Description("Variable key 100 bytes (390007887500010)")]
        [MaxLength(100)]
        public string VAKEY { get; set; }
        [Description("Condition record number (0000636480)")]
        [MaxLength(10)]
        public string KNUMH { get; set; }
        [Description("Date on Which Record Was Created (20190611)")]
        [MaxLength(8)]
        public string ERDAT { get; set; }
        [Description("Valid-From Date (20190423)")]
        [MaxLength(8)]
        public string DATAB { get; set; }
        [Description("Valid To Date (99991231)")]
        [MaxLength(8)]
        public string DATBI { get; set; }
        [Description("Rate (condition amount or percentage) (7,360.00)")]
        [MaxLength(20)]
        public string KBETR { get; set; }
        [Description("Condition unit (J02)")]
        [MaxLength(3)]
        public string KMEIN { get; set; }
        [Description("Base Unit of Measure (J01)")]
        [MaxLength(3)]
        public string MEINS { get; set; }
        [Description("Deletion Indicator in Purchasing Document ถ้ามี Delete จะเป็น L")]
        [MaxLength(1)]
        public string LOEKZ { get; set; }
        [Description("Sales Tax Code (VX)")]
        [MaxLength(2)]
        public string SLTAX { get; set; }
        [Description("Unit of Measurement Text (งาน)")]
        [MaxLength(10)]
        public string THUNT { get; set; }
    }
}
