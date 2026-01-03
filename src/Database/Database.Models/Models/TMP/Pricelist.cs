using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.TMP
{
    [Description("ข้อมูล Pricelist")]
    [Table("Pricelist", Schema = Schema.TEMP)]
    public class Pricelist : BaseEntity
    {
        [Description("โครงการ")]
        public string ProjectID { get; set; }
        [Description("แปลง")]
        public string UnitNumber { get; set; }
        [Description("วันที่ active")]
        public DateTime? ActiveDate { get; set; }
        [Description("ราคาขายตั้งต้น")]
        [Column(TypeName = "Money")]
        public decimal? SellPrice { get; set; }
        [Description("ราคาขายต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal? PricePerUnit { get; set; }
        [Description("ราคาเฟอร์นิเจอร์")]
        [Column(TypeName = "Money")]
        public decimal? FurniturePrice { get; set; }
        [Description("เงินจอง")]
        [Column(TypeName = "Money")]
        public decimal? BookingAmount { get; set; }
        [Description("เงินทำสัญญา")]
        [Column(TypeName = "Money")]
        public decimal? ContractAmount { get; set; }
        [Description("%เงินดาวน์")]
        [Column(TypeName = "Money")]
        public decimal? PercentDown { get; set; }
        [Description("จำนวนงวดดาวน์")]
        public int? DownTime { get; set; }
        [Description("งวดดาวน์พิเศษ")]
        public string SpecialDownTime { get; set; }
        [Description("เงินงวดดาวน์พิเศษ")]
        [Column(TypeName = "Money")]
        public decimal? SpecialDownAmount { get; set; }
        [Description("เงินดาวน์ต่องวด")]
        [Column(TypeName = "Money")]
        public decimal? DownPerMonth { get; set; }
        [Description("เงินดาวน์ทั้งหมด")]
        [Column(TypeName = "Money")]
        public decimal? DownAmount { get; set; }
        [Description("เงินโอนงวดสุดท้าย")]
        [Column(TypeName = "Money")]
        public decimal? TransferAmount { get; set; }
        [Description("สถานะ")]
        public string UpdateStatus { get; set; }

    }
}
