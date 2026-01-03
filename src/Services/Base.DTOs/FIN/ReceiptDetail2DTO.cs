using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.USR;
namespace Base.DTOs.FIN
{
    public class ReceiptDetail2DTO : BaseDTO
    {
        /// <summary>
        /// ข้อมูล Header
        /// </summary>
        public ReceiptHeaderDTO ReceiptHeader { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string ReceiptNo { get; set; }

        /// <summary>
        /// วันที่นำส่งโรงพิมพ์
        /// </summary>
        public DateTime? SendPrintingDate { get; set; }

        /// <summary>
        /// รับชำระเงินค่าอะไร
        /// </summary>
        public PaymentItem PaymentUnitPriceItem { get; set; }

        /// <summary>
        /// รายละเอียด
        /// </summary>
        [Description("รายละเอียด")]
        public string Description { get; set; }

        /// <summary>
        /// รายละเอียด (ภาษาอังกฤษ)
        /// </summary>
        [Description("รายละเอียด (ภาษาอังกฤษ)")]
        public string DescriptionEN { get; set; }

        /// <summary>
        /// จำนวนเงินที่ชำระ
        /// </summary>
        [Description("จำนวนเงินที่ชำระ")]
        public decimal Amount { get; set; }

    }
}
