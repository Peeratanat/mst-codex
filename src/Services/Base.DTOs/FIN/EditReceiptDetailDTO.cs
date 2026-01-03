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
    public class EditReceiptDetailDTO : BaseDTO
    {
        /// <summary>
        /// รายละเอียด รับเงินค่า
        /// </summary>
        [Description("รายละเอียด รับเงินค่า")]
        public string ReceiptDescription { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal PayAmount { get; set; }

        /// <summary>
        /// วิธีการชำระเงิน/ประเภทการชำระเงิน
        /// </summary>
        [Description("วิธีการชำระเงิน/ประเภทการชำระเงิน")]
        public PaymentMethodDTO PaymentMethod { get; set; }

        ////////////////////////////////////////////////////////////////
        ///**********Credit Card
        ////////////////////////////////////////////////////////////////
        /// <summary>
        /// เป็นบัตรต่างประเทศหรือไม่ For บัตรเครดิต
        /// </summary>
        [Description("เป็นบัตรต่างประเทศหรือไม่ For บัตรเครดิต")]
        public bool? IsForeignCreditCard { get; set; }
        /// <summary>
        /// ค่าธรรมเนียม For บัตรเครดิต,บัตรเดบิต,โอนต่างประเทศ
        /// Master/api/EDCs/Fees/Calculate
        /// </summary>
        [Description("ค่าธรรมเนียม For บัตรเครดิต,บัตรเดบิต,โอนต่างประเทศ")]
        public decimal? Fee { get; set; }
        /// <summary>
        /// เลขที่บัตร For บัตรเครดิต,บัตรเดบิต
        /// </summary>
        [Description("เลขที่บัตร For บัตรเครดิต,บัตรเดบิต")]
        public string CardNo { get; set; }
        /// <summary>
        /// รูปแบบการจ่ายเงิน (รูดเต็ม หรือ ผ่อน) For บัตรเครดิต
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditCardPaymentType
        /// </summary>
        [Description("รูปแบบการจ่ายเงิน (รูดเต็ม หรือ ผ่อน) For บัตรเครดิต")]
        public MST.MasterCenterDropdownDTO CreditCardPaymentType { get; set; }
        /// <summary>
        /// ประเภทบัตร (Visa, Master, JCB) For บัตรเครดิต
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditCardType
        /// </summary>
        [Description("ประเภทบัตร (Visa, Master, JCB) For บัตรเครดิต")]
        public MST.MasterCenterDropdownDTO CreditCardType { get; set; }

        /// <summary>
        /// ธนาคารเจ้าของบัตร For บัตรเครดิต,บัตรเดบิต,เช็คส่วนตัว,โอนต่างประเทศ,QR Code
        /// Master/api/Banks/DropdownList
        /// </summary>
        [Description("ธนาคารเจ้าของบัตร For บัตรเครดิต,บัตรเดบิต,เช็คส่วนตัว,โอนต่างประเทศ,QR Code")]
        public MST.BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// เครื่องรูดบัตร For บัตรเครดิต,บัตรเดบิต
        /// Master/api/EDCs/DropdownList?projectID={projectID}
        /// </summary>
        [Description("เครื่องรูดบัตร For บัตรเครดิต,บัตรเดบิต")]
        public MST.EDCDropdownDTO EDC { get; set; }
        /// <summary>
        /// ผิดบัญชี For บัตรเครดิต,บัตรเดบิต,โอนผ่านธนาคาร,โอนต่างประเทศ,QR Code
        /// </summary>
        [Description("เครื่องรูดบัตร For บัตรเครดิต,บัตรเดบิต,โอนผ่านธนาคาร,โอนต่างประเทศ,QR Code")]
        public bool IsWrongAccount { get; set; }
        //////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////
        ///**********PersonalCheque,CashierCheque
        //////////////////////////////////////////////////
        /// <summary>
        /// วันที่หน้าเช็ค
        /// </summary>
        [Description("วันที่หน้าเช็ค")]
        public DateTime ChequeDate { get; set; }
        /// <summary>
        /// เลขที่เช็ค
        /// </summary>
        [Description("เลขที่เช็ค")]
        public string ChequeNo { get; set; }
        /// <summary>
        /// สั่งจ่ายบริษัท
        /// </summary>
        [Description("สั่งจ่ายบริษัท")]
        public MST.CompanyDropdownDTO PayToCompany { get; set; }
        /// <summary>
        /// สั่งจ่ายผิดบริษัท
        /// </summary>
        public bool IsWrongCompany { get; set; }
        /// <summary>
        /// สาขาธนาคาร
        /// </summary>
        [Description("สาขาธนาคาร")]
        public MST.BankBranchDropdownDTO BankBranch { get; set; }
        //////////////////////////////////////////////////
        

        ////////////////////////////////////////////////////////////////
        ///**********โอนผ่านธนาคาร
        //////////////////////////////////////////////////
        /// <summary>
        /// บัญชีธนาคาร
        /// Master/api/BankAccounts/DropdownList?bankAccountTypeKey="2"
        /// </summary>
        [Description("บัญชีธนาคาร")]
        public MST.BankAccountDropdownDTO BankAccount { get; set; }


        ////////////////////////////////////////////////////////////////
        ///**********โอนต่างประเทศ
        //////////////////////////////////////////////////
        /// <summary>
        /// ธนาคารที่รับเงินก่อนเข้า AP
        /// </summary>
        public MST.BankDropdownDTO ForeignBank { get; set; }
        /// <summary>
        /// ประเภทการโอนเงินต่างประเทศ
        /// </summary>
        public MST.MasterCenterDropdownDTO ForeignTransferType { get; set; }
        /// <summary>
        /// IR
        /// </summary>
        public string IR { get; set; }
        /// <summary>
        /// ชื่อผู้โอน
        /// </summary>
        [Description("ชื่อผู้โอน")]
        public string TransferorName { get; set; }
        /// <summary>
        /// ต้องขอ FET หรือไม่
        /// </summary>
        public bool IsRequestFET { get; set; }
        /// <summary>
        /// แจ้งแก้ไข FET
        /// </summary>
        public bool IsNotifyFET { get; set; }
        /// <summary>
        /// ข้อความแจ้งเตือน
        /// </summary>
        public string NotifyFETMemo { get; set; }
    }
}
