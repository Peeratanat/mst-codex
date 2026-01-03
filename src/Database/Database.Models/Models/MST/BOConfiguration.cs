using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("BOConfiguration")]
    [Table("BOConfiguration", Schema = Schema.MASTER)]
    public class BOConfiguration : BaseEntity
    {
        [Description("ภาษีมูลค่าเพิ่ม (%)")]
        public double Vat { get; set; }
        [Description("BOI")]
        public double? BOIAmount { get; set; }
        [Description("ภาษีเงินได้ (%)")]
        public double? IncomeTaxPercent { get; set; }
        [Description("ภาษีธุรกิจเฉพาะ (%)")]
        public double? BusinessTaxPercent { get; set; }
        [Description("ภาษีท้องถิ่น (%)")]
        public double? LocalTaxPercent { get; set; }
        [Description("เบี้ยปรับย้ายห้อง")]
        public double? UnitTransferFee { get; set; }
        [Description("บัญชีเงินขาดเกิด")]
        public double? AdjustAccount { get; set; }
        [Description("บัญชีภาษีขาย")]
        public double? TaxAccount { get; set; }
        [Description("อัตราค่าเสื่อมราคาต่อปี")]
        public double? DepreciationYear { get; set; }
        [Description("ยกเลิกแบบคืนเงิน")]
        public double? VoidRefund { get; set; }
        [Description("อัตราค่าธรรมเนียมโอน")]
        public double? TransferFeeRate { get; set; }
        [Description("อัตราค่าจดจำนอง")]
        public double? MortgageRate { get; set; }
        [Description("วันหมดมาตรการรัฐ")]
        public DateTime? ExpireDate { get; set; }
        [Description("อัตราค่าธรรมเนียมโอนตามมาตรการรัฐ")]
        public double? NewTransferFeeRate { get; set; }
        [Description("อัตราค่าจดจำนองตามมาตรการรัฐ")]
        public double? NewMortgageRate { get; set; }
        [Description("ราคาบ้านที่เข้าตามมาตรการรัฐ")]
        public decimal? SalePrice { get; set; }
        [Description("เงินกู้ที่เข้าเกณฑ์ตามมาตรการรัฐ")]
        public decimal? LoadThresholds { get; set; }
    }
}
