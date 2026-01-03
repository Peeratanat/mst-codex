using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class ReceiptInfoSPDTO : BaseDTOFromQuery
    {
        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// โครงการ ProjectTH (List = string ต่อ ,)
        /// </summary>
        public string Project { get; set; }

        /// <summary> 
        /// แปลง  UnitNo (List = string ต่อ ,)
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// เลขที่บัญชี (List = string ต่อ ,)
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// ประเภทการชำระ (List = string ต่อ ,)
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// ค่าใช้จ่าย  (List = string ต่อ ,)
        /// </summary>
        public string ReceiptDescription { get; set; }

        /// <summary>
        /// จำนวนเงิน (sum ReceiptTempDetail.Amount)
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// เลขที่นำฝาก  (List = string ต่อ ,)
        /// </summary>
        public string DepositNo { get; set; }

        /// <summary>
        /// เลขที่ RV (List = string ต่อ ,)
        /// </summary>
        public string RVNumber { get; set; }

        /// <summary>
        /// สถานะใบเสร็จ
        /// </summary>
        public bool? ReceiptStatus { get; set; }

        /// <summary>
        /// ชื่อของผู้จ่าย model.ReceiptTempHeader?.ContactFirstNameTH != null ? model.ReceiptTempHeader?.ContactFirstNameTH + " " + model.ReceiptTempHeader?.ContactMiddleNameTH + " " + model.ReceiptTempHeader?.ContactLastNameTH : model.ReceiptTempHeader?.ContactFirstNameEN + " " + model.ReceiptTempHeader?.ContactMiddleNameEN + " " + model.ReceiptTempHeader?.ContactLastNameEN
        /// </summary>
        public string CustomerName { get; set; }


    }
}
