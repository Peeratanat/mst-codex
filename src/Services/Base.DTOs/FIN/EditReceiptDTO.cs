using Database.Models.MST;
using Database.Models.USR;
using System;
using System.ComponentModel;
using System.Linq;
using Database.Models.ACC;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.FIN;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using System.Threading.Tasks;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;

namespace Base.DTOs.FIN
{
    public class EditReceiptDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ ใบเสร็จ
        /// </summary>
        [Description("เลขที่ ใบเสร็จ")]
        public string ReceiptNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        [Description("วันที่ใบเสร็จ")]
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// บัญชีธนาคาร filter ตามข้อมูลบริษัท
        /// </summary>
        [Description("บัญชีธนาคาร")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal PayAmount { get; set; }

        /// <summary>
        /// เลขที่ RV
        /// </summary>
        [Description("เลขที่ RV")]
        public string RVNumber { get; set; }

        /// <summary>
        /// เลขที่ PI
        /// </summary>
        [Description("เลขที่ PI")]
        public string PINumber { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        [Description("ชื่อลูกค้า")]
        public string CustomerName { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        /// <summary>
        /// File แนบ
        /// </summary>
        [Description("File แนบ")]
        public string AttachFile { get; set; }

        /// <summary>
        /// สถานะ Active
        /// </summary>
        [Description("สถานะ Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// รายละเอียดประเภทการชำระเงินในใบเสร็จ
        /// </summary>
        [Description("รายละเอียดประเภทการชำระเงินในใบเสร็จ")]
        public List<EditReceiptDetailDTO> EditReceiptDetail { get; set; }
    }
}
