using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ราคาโครงการ")]
    [Table("UnitPrice", Schema = Schema.SALE)]
    public class UnitPrice : BaseEntity
    {
        [Description("ผูกใบจอง")]
        public Guid BookingID { get; set; }

        [ForeignKey("BookingID")]
        public Booking Booking { get; set; }

        [Description("ขั้นตอนของการซื้อแปลง")]
        public Guid UnitPriceStageMasterCenterID { get; set; }
        [ForeignKey("UnitPriceStageMasterCenterID")]
        public MST.MasterCenter UnitPriceStage { get; set; }

        [Description("ราคาที่ใช้จริง")]
        public bool IsActive { get; set; }

        [Description("ลำดับ")]
        public int Order { get; set; }


        [Description("ราคาขาย")]
        public decimal? SellingPrice { get; set; }
        [Description("ราคา Min Price")]
        public decimal? MinPrice { get; set; }
        [Description("ส่วนลดเงินสด")]
        public decimal? CashDiscount { get; set; }
        [Description("ราคาหน้าสัญญา ([ราคาขาย]-[ส่วนลดเงินสด])")]
        public decimal? AgreementPrice { get; set; }
        [Description("ส่วนลด ณ​ วันโอน")]
        public decimal? TransferDiscount { get; set; }
        [Description("ราคาขายหลังหักส่วนลด ([ราคาขาย]-[ส่วนลดเงินสด]-[ส่วนลด ณ​ วันโอน])")]
        public decimal? TotalPrice { get; set; }
        [Description("ส่วนลด Friend get Friend")]
        public decimal? FGFDiscount { get; set; }
        [Description("รวมมูลค่าโปรโมชั่น ของแถม")]
        public decimal? ExpensePromotionAmount { get; set; }
        [Description("รวมมูลค่าโปรโมชั่น ฟรีค่าใช้จ่าย")]
        public decimal? FeePromotionAmount { get; set; }
        [Description("รวมมูลค่าโปรโมชั่นที่ลูกค้าได้รับ ([ExpensePromotionAmount]+[FeePromotionAmount]+[FGFDiscount]+[CashDiscount]+[TransferDiscount])")]
        public decimal? TotalPromotionAmount { get; set; }
        [Description("ราคาขายสุทธิ ([ราคาขาย]-[มูลค่ารวมโปรโมชั่นที่ลูกค้าได้รับ])")]
        public decimal? NetPrice { get; set; }
        [Description("ส่วนลด Freedown")]
        public decimal? FreedownDiscount { get; set; }
        [Description("% ฟรีดาวน์")]
        public double? PercentFreeDown { get; set; }
        [Description("จำนวนเงินฟรีดาวน์สูงสุด")]
        public decimal? MaxFreeDownAmount { get; set; }
        [Description("มูลค่าบ้านสุทธิหลังหักส่วนลดทุกอย่างแล้ว ([ราคาขาย]-[มูลค่ารวมโปรโมชั่นที่ลูกค้าได้รับ]-[ส่วนลด Freedown])")]
        public decimal? RevenueAmount { get; set; }
        [Description("เงินค่าจอง")]
        public decimal? BookingAmount { get; set; }
        [Description("เงินค่าทำสัญญา")]
        public decimal? AgreementAmount { get; set; }
        [Description("รวมมูลค่าเงินดาวน์")]
        public decimal? TotalInstallmentAmount { get; set; }
        [Description("%เงินดาวน์")]
        public double? InstallmentPercent { get; set; }
        [Description("จำนวนงวดงวดดาน์")]
        public int? Installment { get; set; }
        [Description("ราคาเงินดาวน์ต่องวด(งวดปกติ)")]
        public decimal? InstallmentAmount { get; set; }
        [Description("จำนวนงวดดาวน์ปกติ")]
        public int? NormalInstallment { get; set; }
        [Description("จำนวนงวดดาวน์พิเศษ")]
        public int? SpecialInstallment { get; set; }
        [Description("วันที่จ่ายงวดดาวน์ (วันที่ 1 หรือ15)")]
        public int? InstallmentPayDate { get; set; }
        [Description("วันที่เริ่มชำระงวดดาวน์")]
        public DateTime? InstallmentStartDate { get; set; }
        [Description("วันที่สิ้นสุดชำระงวดดาวน์")]
        public DateTime? InstallmentEndDate { get; set; }

        [Description("เงินโอนกรรมสิทธิ์")]
        public decimal? TransferAmount { get; set; }
    }
}
