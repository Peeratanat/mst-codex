using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("งานขอสร้าง PR จาก SAP")]
    [Table("PRRequestJob", Schema = Schema.PROMOTION)]
    public class PRRequestJob : BaseEntity
    {
        [Description("ชื่อไฟล์ที่ส่งให้ SAP")]
        [MaxLength(1000)]
        public string FileName { get; set; }
        [Description("ชื่อไฟล์ที่รับผลจาก SAP")]
        [MaxLength(1000)]
        public string SAPResultFileName { get; set; }

        [Description("สถานะการทำงานของ Job")]
        public BackgroundJobStatus Status { get; set; }

        [Description("ข้อผิดพลาด")]
        public string ErrorMessage { get; set; }

        [Description("ขั้นตอนของการสร้าง PR")]
        public Guid? PRRequestStageID { get; set; }
        [ForeignKey("PRRequestStageID")]
        public MST.MasterCenter PRRequestStage { get; set; }

        [Description("ใบจอง")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public Booking Booking { get; set; }

        [Description("โปรก่อนขายที่ขอเบิก")]
        public Guid? PreSalePromotionRequestUnitID { get; set; }
        [ForeignKey("PreSalePromotionRequestUnitID")]
        public PreSalePromotionRequestUnit PreSalePromotionRequestUnit { get; set; }

        [Description("โปรขายที่ขอเบิก")]
        public Guid? SalePromotionRequestID { get; set; }
        [ForeignKey("SalePromotionRequestID")]
        public SalePromotionRequest SalePromotionRequest { get; set; }

        [Description("โปรโอนที่ขอเบิก")]
        public Guid? TransferPromotionRequestID { get; set; }
        [ForeignKey("TransferPromotionRequestID")]
        public TransferPromotionRequest TransferPromotionRequest { get; set; }
    }
}
